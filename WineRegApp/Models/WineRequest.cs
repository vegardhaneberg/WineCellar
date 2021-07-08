using System;
namespace WineRegApp.Models
{
    public class WineRequest
    {
        public string SortValue { get; }
        public WineRequest(string SortValue)
        {
            this.SortValue = SortValue;
        }
    }
}
