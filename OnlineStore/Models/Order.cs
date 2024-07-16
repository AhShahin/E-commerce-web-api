using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace OnlineStore.Models
{
    public class Order : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        [Column(TypeName = "decimal(6,2)")]
        public decimal FinalePrice { get; set; }
        public string Comments { get; set; }
        public byte[] Invoice { get; set; }
        public DateTime DueDate { get; set; }
        public User? User { get; set; }
        public int? UserId { get; set; }
        public string? GuestId { get; set; }
        public UserPaymentMethod? UserPaymentMethod { get; set; }   
        public int UserPaymentMethodId { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public OrderStatus? orderStatus { get; set; }
        public int OrderStatusId { get; set; }
        public ShippingMethod? shippingMethod { get; set; }
        public int ShippingMethodId { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}