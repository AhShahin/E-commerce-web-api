using System;
using System.Collections.Generic;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Models
{
    public class PaymentType : BaseEntity
    {
        public int Id { get; set; }
        public PaymentTypes Name { get; set; }
        public ICollection<UserPaymentMethod> UserPaymentMethods { get; set; }
    }
}
