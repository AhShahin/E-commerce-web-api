using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Dtos
{
    public class OrderForUpdateDto
    {
        public string PaymentMethod { get; set; }
        public string Type { get; set; }
        public string Owner { get; set; }
        public string Number { get; set; }
        public string Expires { get; set; }
        public decimal ShippingCost { get; set; }
        public string OrdersStatus { get; set; }
        public string ShippingMethod { get; set; }
        public string Comments { get; set; }
    }
}
