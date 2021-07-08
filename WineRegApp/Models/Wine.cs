using System;
using SQLite;
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
        public string WineType { get; set; }
        public DateTime StoredDate { get; set; }
        public DateTime CanDrinkFromDate { get; set; }
        public DateTime CanDrinkToDate { get; set; }

    }
}
