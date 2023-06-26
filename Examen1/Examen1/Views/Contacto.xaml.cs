using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Examen1.Models;
using Examen1.Controllers;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace Examen1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Contacto : ContentPage
    {
        Plugin.Media.Abstractions.MediaFile photo = null;
        Location location = null;

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    geoloc.Text = $"Latitud: {location.Latitude}, Longitud: {location.Longitude}";
                }
            }
            catch (Exception ex)
            {
                location = new Location(latitude: 0.0, longitude: 0.0);
                await DisplayAlert("Aviso", $"Ha ocurrido un error al cargar geolocalizacion.. {ex.Message}", "OK");
            }
        }

        public byte[] GetimageBytes()
        {
            if (photo != null)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    Stream stream = photo.GetStream();
                    stream.CopyTo(memory);
                    byte[] fotobyte = memory.ToArray();

                    return fotobyte;
                }

            }

            return null;
        }

        public Contacto()
        {
            InitializeComponent();
        }

        private async void btnsalvar_Clicked(object sender, EventArgs e)
        {
            int edadDada;
            string paisSeleccionado;
            try
            {
                paisSeleccionado = pais.SelectedItem.ToString();
            }
            catch (Exception)
            {
                paisSeleccionado = "";
            }

            try
            {
                edadDada = int.Parse(edad.Text);
            }
            catch (Exception)
            {
                edadDada = -1;
            }

            if (nombres.Text == null)
            {
                await DisplayAlert("Aviso", "Favor agregue un nombre..", "OK");
                return;
            }

            if (telefono.Text == null)
            {
                await DisplayAlert("Aviso", "Favor agregue un telefono..", "OK");
                return;
            }

            if (edadDada < 0)
            {
                await DisplayAlert("Aviso", "Favor agregue una edad..", "OK");
                return;
            }

            if (paisSeleccionado == string.Empty)
            {
                await DisplayAlert("Aviso", "Favor agregue un pais..", "OK");
                return;
            }

            if (nota.Text == null)
            {
                await DisplayAlert("Aviso", "Favor agregue una nota..", "OK");
                return;
            }

            var contactos = new Models.Contactos
            {
                nombres = nombres.Text,
                apellidos = apellidos.Text,
                telefono = telefono.Text,
                edad = edadDada,
                pais = paisSeleccionado,
                nota = nota.Text,
                latitud = location.Latitude,
                longitud = location.Longitude,

                foto = GetimageBytes()
            };

            if (photo != null)
            {
                if (await App.DataBase.AddContacto(contactos) > 0)
                {
                    await DisplayAlert("Aviso", "Contacto ingresado con exito", "OK");
                }
                else
                {
                    await DisplayAlert("Aviso", "Ha ocurrido un error..", "OK");
                }
            }
            else
            {
                await DisplayAlert("Aviso", "Favor tome una foto primero", "OK");
            }

        }

        private async void btnfoto_Clicked(object sender, EventArgs e)
        {
            photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "APP",
                Name = "Foto.jpg",
                SaveToAlbum = true

            });

            if (photo != null)
            {
                foto.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
            }

        }
    }
}