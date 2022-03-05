using System;
using SQLite;
using Xamarin.Forms;

namespace WineRegApp.Models
{
    public class Wine
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [NotNull]
        public string Name { get; set; }
        public string Place { get; set; }
        public string Region { get; set; }
        public double Price { get; set; }
        public string Country { get; set; }
        public int Year { get; set; }
        public WineType WineType { get; set; }
        public DateTime StoredDate { get; set; }
        public DateTime CanDrinkFromDate { get; set; }
        public DateTime CanDrinkToDate { get; set; }
        public string BarCode { get; set; }

        public Color WineColor
        {
            get
            {
                if (this.WineType == WineType.Red) return Color.Red;
                if (this.WineType == WineType.Rose) return Color.Plum;
                return Color.Wheat;
            }
        }
    }
}
