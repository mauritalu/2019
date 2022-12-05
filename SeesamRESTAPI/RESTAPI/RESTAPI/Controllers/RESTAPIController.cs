using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RESTAPI.Controllers
{
    public class RESTAPIController : Controller
    {
        // GET: RESTAPI
        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Index(FormCollection collection)
        //{
        //    var txtBankName = collection.Get("txtBankName");
        //    var txtPersnr = collection.Get("txtPersnr");
        //    var txtkundnr = collection.Get("txtkundnr");
        //
        //    ViewData["Result"] = txtBankName;
        //    return View();
        //}



        [HttpPost]
        public ActionResult Index(string txtBankName, string txtPersnr, string txtkundnr)
        {
            ViewData["Result"] = txtBankName;
            return View();
        }

    }
}