using System;
using System.Collections.Generic;

namespace OnlineStore.Models
{
    public class UserPaymentMethod : BaseEntity
    {
        public int Id { get; set; }
        public int? AccountNumber { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public PaymentType paymentType { get; set; }
        public int PaymentTypeId { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
