using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Models
{
    public class Image : BaseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Url { get; set; }
        public int ProductOptionId { get; set; }
        public ProductOptions? ProductOption { get; set;}
    }
}