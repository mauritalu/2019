using Dog.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Dog.Controllers
{
    //public class DogController : Controller
    //{
    //    // GET: Dog
    //    public ActionResult Index()
    //    {
    //        return View();
    //    }
    //}
}



namespace Dog.Controllers
{
    public class DogController : Controller
    {
        //Hosted web api REST Service base url
        string Baseurl = "http://10.103.0.60:8810/wb/rest/wbService/";
        public async Task<ActionResult> Index()
        {
            List<CatData> EmpInfo = new List<CatData>();

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("testCat");

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response received from web api and storing into the Employee list
                    //EmpInfo = JsonConvert.DeserializeObject<List<CatData>>(EmpResponse);


                    var jsonString = JsonConvert.SerializeObject(EmpResponse);

                    ViewBag.listCat = jsonString;

                    string path = "c:\\temp\\export.txt";
                    System.IO.File.WriteAllText(path, jsonString);

                   
                }

                //return null;
                return View();
            }
        }
    }
}