using System;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing;
using WineRegApp.Models;
using WineRegApp.Views;
using WineRegApp.ViewModels;

namespace WineRegApp.ViewModels
{
    public class ScanPageViewModel : INotifyPropertyChanged
    {
        public Result Result { get; set; }
        public ICommand ScanCommand { get; set; }
        public bool isScanning { get; set; }
        public bool isAnalyzing { get; set; }

        public ScanPageViewModel()
        {
            IsScanning = true;
            IsAnalyzing = true;

            ScanCommand = new Command(() =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsAnalyzing = false;
                    //await App.Current.MainPage.DisplayAlert("Scan Completed", Result.Text, "Ok");
                    Wine wine = await App.Database.GetWineByBarCodeAsync(Result.Text);

                    if (wine is null)
                    {
                        AddWinePage addWinePage = new AddWinePage();
                        addWinePage.BindingContext = new AddWineViewModel(Result.Text);
                        await App.Current.MainPage.Navigation.PushAsync(addWinePage);
                    }
                    else
                    {
                        // Navigate to some Wine Page
                    }

                    
                });
            }); 
        }
        public bool IsScanning
        {
            get { return isScanning; }
            set
            {
                isScanning = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsAnalyzing
        {
            get { return isAnalyzing; }
            set
            {
                isAnalyzing = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
