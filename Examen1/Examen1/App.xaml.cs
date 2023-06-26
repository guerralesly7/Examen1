using Examen1.Controllers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Examen1.Models;
using System.IO;

namespace Examen1
{
    public partial class App : Application
    {
        static DataBase database;

        public static DataBase DataBase
        {
            get
            {
                var dbpath = String.Empty;
                var namedb = String.Empty;
                var fullpath = String.Empty;

                dbpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                namedb = "DBContactos.db3";
                fullpath = Path.Combine(dbpath, namedb);

                if (database == null)
                {
                    database = new DataBase(fullpath);
                }

                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.ContactosView());
            //new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
