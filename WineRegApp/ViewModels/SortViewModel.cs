using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WineRegApp.ViewModels
{
    public class SortViewModel : INotifyPropertyChanged

    {
        private MainPageViewModel parentViewModel;
        private List<string> sortCategories;
        private string selectedSortCategory;

        public SortViewModel(MainPageViewModel parentViewModel)
        {
            this.parentViewModel = parentViewModel;
            this.sortCategories = new List<string>
            {
                "Name",
                "Year",
                "Price",
                "Country",
                "Wine Type",
            };
        }

        public string SelectedSortCategory
        {
            get { return selectedSortCategory; }
            set
            {
                selectedSortCategory = value;
                NotifyPropertyChanged();
            }
        }

        public List<string> SortCategories
        {
            get { return sortCategories; }
            set
            {
                sortCategories = value;
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
