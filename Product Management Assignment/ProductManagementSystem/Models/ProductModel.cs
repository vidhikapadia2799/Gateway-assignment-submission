using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductManagementSystem.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Product Category")]
        public string ProductCategory { get; set; }

        [Required(ErrorMessage = "Please enter Product Name")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Please enter Product Price")]
        public string ProductPrice { get; set; }

        [Required(ErrorMessage = "Please enter Product Quantity")]
        [Range(0, 99, ErrorMessage = "Quantity must be numeric")]
        public int ProductQuantity { get; set; }

        [Required(ErrorMessage = "Please enter Short Product Description")]
        public string ShortDesc { get; set; }

        [Required(ErrorMessage = "Please enter Long Product Description")]
        public string LongDesc { get; set; }

        [Required(ErrorMessage = "Please upload Image file")]
        public string SmallImage { get; set; }

        [Required(ErrorMessage = "Please upload Image file")]
        public string LargeImage { get; set; }
    }
}
