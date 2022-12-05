using Cat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Cat.Controllers
{
    public class CatController : Controller
    {
        public ActionResult Index()
        {
            CatClient pc = new CatClient();
            ViewBag.listCat = pc.findAll();
            
            return View();
        }
    }
}