using System;
using System.Windows.Input;
using Xamarin.Forms;
using WineRegApp.Models;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using WineRegApp.ViewModels;
using System.Collections.Generic;
using WineRegApp.Views;
using ZXing;


namespace WineRegApp.ViewModels
{
    public class AddWineViewModel : INotifyPropertyChanged
    {
        public ICommand AddWineCommand { get; set; }
        private string newName;
        private string newYear;
        private string newPrice;
        private string newPlace;
        private string newRegion;
        private List<string> wineTypes;
        private string selectedWineType;
        private string minStoreYears;
        private string maxStoreYears;
        private string newCountry;

        public AddWineViewModel(string barCode)
        {
            newName = "";
            newYear = "";
            newPrice = "";
            newPlace = "";
            newRegion = "";
            minStoreYears = "";
            maxStoreYears = "";
            selectedWineType = "";
            newCountry = "";
            //else
            //{
            //    NewName = wine.Name;
            //    NewYear = wine.Year.ToString();
            //    NewPrice = wine.Price.ToString();
            //    NewPlace = wine.Place;
            //    NewRegion = wine.Region;
            //    MinStoreYears = (wine.CanDrinkFromDate.Year - DateTime.Now.Year).ToString();
            //    MaxStoreYears = (wine.CanDrinkToDate.Year - DateTime.Now.Year).ToString();
            //    SelectedWineType = wine.WineType.ToString();
            //    NewCountry = wine.Country;
            //}
            
            wineTypes = new List<string>
            {
                "Red",
                "White",
                "Rose"
            };

            AddWineCommand = new Command(async () =>
            {
                WineType wineType = WineType.Red;
                if (selectedWineType == "White") wineType = WineType.White;
                if (selectedWineType == "Rose") wineType = WineType.Rose;
                try
                {
                    Wine newWine = new Wine
                    {
                        Name = NewName,
                        Year = int.Parse(NewYear),
                        Price = double.Parse(NewPrice, System.Globalization.CultureInfo.InvariantCulture),
                        Place = NewPlace,
                        Region = NewRegion,
                        Country = NewCountry,
                        WineType = wineType,
                        StoredDate = DateTime.Now,
                        CanDrinkFromDate = new DateTime(DateTime.Now.Year + int.Parse(minStoreYears),
                                                        DateTime.Now.Month,
                                                        DateTime.Now.Day),
                        CanDrinkToDate = new DateTime(DateTime.Now.Year + int.Parse(maxStoreYears),
                                                      DateTime.Now.Month,
                                                      DateTime.Now.Day),
                        BarCode = barCode,
                    };

                    await App.Database.SaveWineAsync(newWine);

                    Page mainPage = new MainPage();
                    MainPageViewModel mainPageVM = new MainPageViewModel();
                    mainPage.BindingContext = mainPageVM;
                    mainPageVM.Initialize();
                    await App.Current.MainPage.Navigation.PushAsync(mainPage);

                }
                catch (Exception)
                {
                    await App.Current.MainPage.DisplayAlert("Invalid Wine Entry",
                        "Make sure year and price are numbers",
                        "OK");
                }
            });
        }

        public string NewCountry
        {
            get { return newCountry; }
            set
            {
                newCountry = value;
                NotifyPropertyChanged();
            }
        }
        public string MaxStoreYears
        {
            get { return maxStoreYears; }
            set
            {
                maxStoreYears = value;
                NotifyPropertyChanged();
            }
        }

        public string MinStoreYears
        {
            get { return minStoreYears; }
            set
            {
                minStoreYears = value;
                NotifyPropertyChanged();
            }
        }

        public List<string> WineTypes
        {
            get { return wineTypes; }
            set
            {
                wineTypes = value;
                NotifyPropertyChanged();
            }
        }

        public string SelectedWineType
        {
            get { return selectedWineType; }
            set
            {
                selectedWineType = value;
                NotifyPropertyChanged();
            }
        }

        public string NewName
        {
            get { return newName; }
            set
            {
                newName = value;
                NotifyPropertyChanged();
            }
        }
        public string NewYear
        {
            get { return newYear; }
            set
            {
                newYear = value;
                NotifyPropertyChanged();
            }
        }
        public string NewPrice
        {
            get { return newPrice; }
            set
            {
                newPrice = value;
                NotifyPropertyChanged();
            }
        }
        public string NewPlace
        {
            get { return newPlace; }
            set
            {
                newPlace = value;
                NotifyPropertyChanged();
            }
        }
        public string NewRegion
        {
            get { return newRegion; }
            set
            {
                newRegion = value;
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
