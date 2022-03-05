using System;
using WineRegApp.Models;

namespace WineRegApp.ViewModels
{
    public class WinePageViewModel
    {
        public Wine Wine { get; set; }

        public WinePageViewModel(WinePageViewModel wine)
        {
            this.Wine = wine;
        }
    }
}
