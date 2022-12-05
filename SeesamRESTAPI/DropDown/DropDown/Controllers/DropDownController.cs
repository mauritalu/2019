using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DropDown;

namespace DropDown.Controllers
{
    public class DropDownController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            OrderViewModel model = new OrderViewModel();
            ConfigureViewModel(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ConfigureViewModel(model);
                return View(model);
            }
            // save and redirect
            // but for testing purposes
            ConfigureViewModel(model);
            return View(model);
        }

        private void ConfigureViewModel(OrderViewModel model)
        {
            IEnumerable<Product> products = Repository.FetchProducts();
            model.ProductList = new SelectList(products, "ID", "Name");
        }
    }
}