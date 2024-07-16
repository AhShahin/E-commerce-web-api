using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Dtos
{
    public class AddressForListDto
    {
        public int Id { get; set; }
        public string StreetAddress { get; set; }
        public string Postcode { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public bool IsShippingAddress { get; set; }
        public bool IsBillingAddress { get; set; }
    }
}
