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

        private ObservableCollection<Wine> wines;
        public ICommand FilterCommand { private set; get; }
        public ICommand CloseFilterCommand { private set; get; }
        public ICommand DeleteAllCommand { private set; get; }
        public ICommand AddWineCommand { private set; get; }
        public ICommand LogOutCommand { private set; get; }
        public ICommand FillDummyDataCommand { private set; get; }
        private bool filterIsOpen;
        private bool menuButtonIsOpen;
        public SortViewModel SortViewModel { get; set; }

        public MainPageViewModel()
        {

            filterIsOpen = false;
            menuButtonIsOpen = false;
            wines = new ObservableCollection<Wine>();

            SortViewModel = new SortViewModel(this);

            AddWineCommand = new Command(async () =>
            {
                MenuButtonIsOpen = false;
                Page addWinePage = new AddWinePage();
                addWinePage.BindingContext = new AddWineViewModel(this);
                await App.Current.MainPage.Navigation.PushAsync(addWinePage);
            });
            DeleteAllCommand = new Command(async () =>
            {
                await App.Database.DeleteAllWinesAsync();
                Wines = new ObservableCollection<Wine>();
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
            FillDummyDataCommand = new Command(() =>
            {

            });

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
            List<Wine> winesList = await App.Database.GetAllWinesAsync();

            foreach (Wine w in winesList)
            {
                wines.Add(w);
            }
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
