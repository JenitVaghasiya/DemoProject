
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoProject.Model
{
    public class Product
    {
        [Key]
        public Nullable<int> productId { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
    }
}
