using System;
using System.Collections.Generic;
using System.Text;

namespace BabySiteApp.DTO
{
    public class StreetDTO
    {
        public int id { get; set; }
        public int city_symbol { get; set; }
        public string city_name { get; set; }
        public int street_symbol { get; set; }
        public string street_name { get; set; }

        public override string ToString()
        {
            return this.street_name;
        }
    }
}
