using OnlineStore.Models;
using static OnlineStore.Helpers.Enums;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Dtos
{
    public class UserForCreationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string TelephoneCell { get; set; }
        public string TelephoneHome { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime DoB { get; set; }
        public string KnownAs { get; set; }
        public string Type { get; set; }
        public DateTime LastActive { get; set; }
        public int NumberOfLogons {get; set;}
    }
}
