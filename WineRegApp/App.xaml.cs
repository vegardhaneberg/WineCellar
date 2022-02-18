using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WineRegApp.Services;
using System.IO;

namespace WineRegApp    
{
    public partial class App : Application
    {
        static WineDatabase database;

        public static WineDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new WineDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            DIPS.Xamarin.UI.Library.Initialize();

            MainPage =  new NavigationPage(new MainPage());

            bool IsLoggedIn = Current.Properties.ContainsKey("IsLoggedIn") ? Convert.ToBoolean(Current.Properties["IsLoggedIn"]) : false;

            if (IsLoggedIn)
            {
                // load user database based on their unique username.
            }
            else
            {
                // Create unique username and load their wine data. 
            }

            Application.Current.Properties["IsLoggedIn"] = Boolean.TrueString;

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
