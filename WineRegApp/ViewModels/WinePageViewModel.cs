using System;
using WineRegApp.Models;

namespace WineRegApp.ViewModels
{
    public class WinePageViewModel
    {
        public Wine Wine { get; set; }

        public WinePageViewModel(Wine wine)
        {
            this.Wine = wine;
        }
    }
}
