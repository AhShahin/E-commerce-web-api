using OnlineStore.Models;
using static OnlineStore.Helpers.Enums;
using System.Collections.Generic;
using System;

namespace OnlineStore.Dtos
{
    public class OrderStatusForListDto
    {
        public int Id { get; set; }
        public Order_Status Name { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
