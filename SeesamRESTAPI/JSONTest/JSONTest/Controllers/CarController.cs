using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JSONTest.Models;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace JSONTest.Controllers
{
    public class CarController : Controller
    {
        // GET: Car
        public async Task<ViewResult> CarsAsync()
        {
            SampleAPIClient client = new SampleAPIClient();
            var cars = await client.GetCarsAsync();

            return View();
        }

        //public ViewResult CarsAsync()
        //{
        //    SampleAPIClient client = new SampleAPIClient();
        //    var cars = client.GetCarsAsync().Result;

        //    return View("Index", model: cars);
        //}

    }
}