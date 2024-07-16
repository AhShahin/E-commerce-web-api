using System.Linq;
using System;

namespace OnlineStore.Dtos
{
    public class CartItemForUpdateDto
    {
        public string Id { get; set; }
        public Changes changes { get; set; }


        public class Changes
        {
            public int Quantity { get; set; }
        }
    }
}
