using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WineRegApp.Models;
using Xamarin.Forms;
using WineRegApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using WineRegApp.Views;

namespace WineRegApp.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public ICommand FilterCommand { private set; get; }
        public ICommand CloseFilterCommand { private set; get; }
        public ICommand DeleteAllCommand { private set; get; }
        public ICommand AddWineCommand { private set; get; }
        public ICommand LogOutCommand { private set; get; }
        public ICommand FillDummyDataCommand { private set; get; }
        public ICommand SortOpenCommand { private set; get; }
        public ICommand SortCloseCommand { private set; get; }
        public ICommand AscendingCommand { private set; get; }
        public ICommand DescendingCommand { private set; get; }

        private bool filterIsOpen;
        private bool menuButtonIsOpen;
        private bool sortAscending;

        private int redCount;
        private int roseCount;
        private int whiteCount;

        private ObservableCollection<Wine> wines;
        private ObservableCollection<SortCategory> sortCategories;

        private SortCategory selectedSortCategory;

        public MainPageViewModel()
        {
            filterIsOpen = false;
            menuButtonIsOpen = false;
            sortAscending = true;

            wines = new ObservableCollection<Wine>();
            sortCategories = new ObservableCollection<SortCategory>();

            AddWineCommand = new Command(async () =>
            {
                MenuButtonIsOpen = false;
                Page addWinePage = new AddWinePage();
                addWinePage.BindingContext = new AddWineViewModel(this);
                await App.Current.MainPage.Navigation.PushAsync(addWinePage);
            });
            DeleteAllCommand = new Command(async () =>
            {
                string action = await App.Current.MainPage.DisplayActionSheet("Are you sure?", "Cancel", "Yes", "No");
                Console.WriteLine(action);
                Console.WriteLine(action == "Yes");
                if (action == "Yes")
                {
                    await App.Database.DeleteAllWinesAsync();
                    Wines = new ObservableCollection<Wine>();
                    MenuButtonIsOpen = false;
                }
            });
            FilterCommand = new Command(() =>
            {
                FilterIsOpen = true;
            });
            CloseFilterCommand = new Command(() =>
            {
                FilterIsOpen = false;
            });
            LogOutCommand = new Command(() =>
            {
                App.Current.Properties["IsLoggedIn"] = Boolean.FalseString;
            });
            FillDummyDataCommand = new Command(async () =>
            {
                List<Wine> wineList = await App.Database.InsertDummyData();

                if (wineList is null)
                {
                    await App.Current.MainPage.DisplayAlert(String.Empty, "You already have wines registered", "Ok");
                }
                else
                {
                    foreach (Wine w in wineList)
                    {
                        Wines.Add(w);
                    }
                }
                UpdateWineCounts();
                MenuButtonIsOpen = false;
            });
            SortOpenCommand = new Command(() =>
            {

            });
            SortCloseCommand = new Command(async () =>
            {
                List<Wine> receivedWines = await App.Database.GetAllWinesAsync(new WineRequest(SelectedSortCategory.WineCategoryEnum, SortAscending));
                Wines = new ObservableCollection<Wine>();
                foreach (Wine w in receivedWines)
                {
                    Wines.Add(w);
                }
                UpdateWineCounts();
            });
            AscendingCommand = new Command(() =>
            {
                SortAscending = true;
            });
            DescendingCommand = new Command(() =>
            {
                SortAscending = false;
            });
        }

        public bool SortAscending
        {
            get { return sortAscending; }
            set
            {
                sortAscending = value;
                NotifyPropertyChanged();
            }
        }

        public SortCategory SelectedSortCategory
        {
            get { return selectedSortCategory; }
            set
            {
                selectedSortCategory = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<SortCategory> SortCategories
        {
            get { return sortCategories; }
            set
            {
                sortCategories = value;
                NotifyPropertyChanged();
            }
        }

        private int getTypeCount(WineType wt)
        {
            int redCount = 0;
            foreach (Wine w in Wines)
            {
                if (w.WineType == wt)
                {
                    redCount++;
                }
            }
            return redCount;
        }

        public int RedCount
        {
            get { return redCount; }
            set
            {
                redCount = value;
                NotifyPropertyChanged();
            }
        }
        public int WhiteCount
        {
            get { return whiteCount; }
            set
            {
                whiteCount = value;
                NotifyPropertyChanged();
            }
        }
        public int RoseCount
        {
            get { return roseCount; }
            set
            {
                roseCount = value;
                NotifyPropertyChanged();
            }
        }

        public bool MenuButtonIsOpen
        {
            get { return menuButtonIsOpen; }
            set
            {
                menuButtonIsOpen = value;
                NotifyPropertyChanged();
            }
        }

        public async void Initialize()
        {
            SortCategory sortCat1 = new SortCategory("Name");
            SortCategory sortCat2 = new SortCategory("Year");
            SortCategory sortCat3 = new SortCategory("Drink Urgency");

            SortCategories = new ObservableCollection<SortCategory>
            {
                sortCat1,
                sortCat2,
                sortCat3,
            };
            SelectedSortCategory = sortCat1;

            WineRequest wineRequest = new WineRequest(SelectedSortCategory.WineCategoryEnum, SortAscending);

            List<Wine> winesList = await App.Database.GetAllWinesAsync(wineRequest);

            foreach (Wine w in winesList)
            {
                wines.Add(w);
            }
            UpdateWineCounts();
        }

        public bool FilterIsOpen
        {
            get { return filterIsOpen; }
            set
            {
                filterIsOpen = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Wine> Wines
        {
            get { return wines; }
            set
            {
                wines = value;
                UpdateWineCounts();
                NotifyPropertyChanged();
            }
        }

        public void UpdateWineCounts()
        {
            RedCount = getTypeCount(WineType.Red);
            WhiteCount = getTypeCount(WineType.White);
            RoseCount = getTypeCount(WineType.Rose);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
