using System;
namespace WineRegApp.Models
{
    public class SortCategory
    {
        public string Name { get; set; }
        public WineSortCategory WineCategoryEnum { get; set; }

        public SortCategory(string name)
        {
            Name = name;
            if (name == "Name")
            {
                WineCategoryEnum = WineSortCategory.Name;
            }
            else if (name == "Year")
            {
                WineCategoryEnum = WineSortCategory.Year;
            }
            else if (name == "Drink Urgency")
            {
                WineCategoryEnum = WineSortCategory.DrinkUrgency;
            }
            else if (name == "Price")
            {
                WineCategoryEnum = WineSortCategory.Price;
            }
        }
    }
    public enum WineSortCategory
    {
        Name,
        Year,
        DrinkUrgency,
        Price,
    }
}
