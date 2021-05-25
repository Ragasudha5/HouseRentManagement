using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineHouseRentManagementSystem.Models
{
    public class HouseDetailcs
    {
        public int IdUs { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Location { get; set; }
        public string Bedrooms { get; set; }
        public string HouseRent { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
    }
}