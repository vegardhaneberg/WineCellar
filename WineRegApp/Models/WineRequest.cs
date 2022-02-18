using System;
using WineRegApp.Models;

namespace WineRegApp.Models
{
    public class WineRequest
    {
        public WineSortCategory SortValue { get; }
        public bool Ascending { get; }

        public WineRequest(WineSortCategory wineSortCategory, bool ascending)
        {
            SortValue = wineSortCategory;
            Ascending = ascending;
        }
        public WineRequest(WineSortCategory wineSortCategory)
        {
            SortValue = wineSortCategory;
            Ascending = true;
        }
    }
}
