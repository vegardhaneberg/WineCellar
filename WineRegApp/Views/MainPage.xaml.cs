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
            MainPageViewModel mainPageVM = new MainPageViewModel();
            mainPageVM.Initialize();
            BindingContext = mainPageVM;
        }

        void AscendingButton_Clicked(System.Object sender, System.EventArgs e)
        {
            AscendingButton.BorderColor = Color.Blue;
            DescendingButton.BorderColor = Color.Transparent;

        }

        void DescendingButton_Clicked(System.Object sender, System.EventArgs e)
        {
            AscendingButton.BorderColor = Color.Transparent; 
            DescendingButton.BorderColor = Color.Blue;
        }
    }
}
