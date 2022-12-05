using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections.Generic;

namespace DropDown
{
    public class OrderViewModel
    {
        [Display(Name = "Order number")]
        public int OrderNumber { set; get; }
        [Display(Name = "Product")]
        [Required(ErrorMessage = "Please select a product")]
        public int SelectedProductId { get { return 2; } }
        public SelectList ProductList { get; set; }
    }

    public class Product
    {
        public int ID { set; get; }
        public string Name { set; get; }
    }

    public static class Repository
    {
        public static IEnumerable<Product> FetchProducts()
        {
            return new List<Product>()
            {
                new Product(){ ID = 1, Name = "Mobile" },
                new Product(){ ID = 2, Name = "Laptop" },
                new Product(){ ID = 3, Name = "IPad" }
            };
        }
    }
}