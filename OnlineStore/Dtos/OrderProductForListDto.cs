using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnlineStore.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.Generic;

namespace OnlineStore.Dtos
{
    public class OrderProductForListDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        //public int ProductOptionsId { get; set; }
        public ProductOptionForListDto ProductOption { get; set; }
    }
}
