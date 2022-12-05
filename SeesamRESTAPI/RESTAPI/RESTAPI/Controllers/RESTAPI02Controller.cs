using RESTAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RESTAPI.Controllers
{
    public class RESTAPI02Controller : Controller
    {
        // GET: RESTAPI02
        public ActionResult Index()
        {
            //return View(new RESTAPIInputModel());
            return View();
        }

        [HttpPost]
        public ActionResult Index(RESTAPIInputModel m)
        {
            ViewData["Result"] = m.txtBankName;
            //m.txtkundnr = "0";
            //ModelState.Clear();
            return View(m);
        }

        // https://www.youtube.com/watch?v=EPSjxg4Rzs8

    }
}