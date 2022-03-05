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
        public bool IsScanning { get; set; }
        public bool IsAnalyzing { get; set; }
        private string barCode { get; set; }
        public ICommand ScanCommand { get; set; }

        public ScanPageViewModel()
        {
            IsScanning = true;
            IsAnalyzing = true;

            ScanCommand = new Command(() =>
            {
                IsScanning = false;
                IsAnalyzing = false;
                BarCode = Result.Text;
                IsScanning = true;
                IsAnalyzing = true;
            }); 
        }
        public string BarCode
        {
            get { return barCode; }
            set
            {
                barCode = value;
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
