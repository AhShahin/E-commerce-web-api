using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Models
{
    public class Address : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string Postcode { get; set; }
        public City City { get; set; }
        public int CityId { get; set; }
        public Country Country { get; set; }
        public int CountryId { get; set; }
        [Required]
        public bool IsShippingAddress { get; set; }
        [Required]
        public bool IsBillingAddress { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}