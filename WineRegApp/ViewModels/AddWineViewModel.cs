using System;
using System.Windows.Input;
using Xamarin.Forms;
using WineRegApp.Models;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using WineRegApp.ViewModels;
using System.Collections.Generic;

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

        private MainPageViewModel parentViewModel;

        public AddWineViewModel(MainPageViewModel parentViewModel)
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
            wineTypes = new List<string>
            {
                "Red",
                "White",
                "Rose"
            };

            this.parentViewModel = parentViewModel;

            AddWineCommand = new Command(() =>
            {
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
                        WineType = SelectedWineType,
                        StoredDate = DateTime.Now,
                        CanDrinkFromDate = new DateTime(DateTime.Now.Year + int.Parse(minStoreYears),
                                                        DateTime.Now.Month,
                                                        DateTime.Now.Day),
                        CanDrinkToDate = new DateTime(DateTime.Now.Year + int.Parse(maxStoreYears),
                                                      DateTime.Now.Month,
                                                      DateTime.Now.Day)
                    };

                    parentViewModel.Wines.Add(newWine);
                    App.Database.SaveWineAsync(newWine);
                    Application.Current.MainPage.Navigation.PopAsync();
                }
                catch (Exception)
                {
                    App.Current.MainPage.DisplayAlert("Invalid Wine Entry",
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
