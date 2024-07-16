using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Dtos
{
    public class OrderForListDto
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
        public string Number { get; set; }
        public decimal FinalePrice { get; set; }
        public string OrderStatus { get; set; }
        public string ShippingMethod { get; set; }
        public AddressForListDto Address { get; set; }
        public ICollection<OrderProductForListDto> OrderProducts { get; set; }


        //public string Comments { get; set; }
    }
}
