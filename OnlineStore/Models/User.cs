using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Models
{
    public class User : BaseEntity
    {
        public int Id { get; set; }
        public string? GuestId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }      
        public string Email { get; set; }       
        public string TelephoneCell { get; set; }
        public string TelephoneHome { get; set; }
        public DateTime DoB { get; set; }
        public string? KnownAs { get; set; }
        public string Type { get; set; }
        public int? NumberOfLogons { get; set; }
        public DateTime LastActive { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<UserPaymentMethod> UserPaymentMethods { get; set; }
    }
}