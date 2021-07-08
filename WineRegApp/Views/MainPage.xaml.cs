using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WineRegApp.ViewModels;
using Xamarin.Forms;

namespace WineRegApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Title = "Your Wine Cellar";
            MainPageViewModel hei = new MainPageViewModel();
            hei.Initialize();
            BindingContext = hei;
        }
    }
}
