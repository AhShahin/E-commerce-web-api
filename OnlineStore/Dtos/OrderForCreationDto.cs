using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Dtos
{
    public class OrderForCreationDto
    {
        public int? UserId { get; set; }
        public int BillingAddressId { get; set; }
        public int ShippingAddressId { get; set; }
        public int UserPaymentMethodId { get; set; }
        public int ShippingMethodId { get; set; }
        public string? Comments { get; set; }
        public DateTime DueDate { get; set; }

    }
}
