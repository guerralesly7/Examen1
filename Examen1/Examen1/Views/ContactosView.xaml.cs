using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Examen1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactosView : ContentPage
    {
        public ContactosView()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Cargar la lista de estudiantes antes de cargar botones y toda la view
            listcontactos.ItemsSource = await App.DataBase.ObtenerListaContactos();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.Contacto());
        }
    }
}