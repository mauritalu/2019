using JSONTestURI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace JSONTestURI.Controllers
{
    public class HomeController : Controller
    {
        //Hosted web api REST Service base url
        string Baseurl = "http://10.103.0.60:8810/"; 
        public async Task<ActionResult> Index()
        {
            List<Employee2> EmpInfo = new List<Employee2>();

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("wb/rest/wbService/getAllCats");

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details received from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response received from web api and storing into the Employee list
                    EmpInfo = JsonConvert.DeserializeObject<List<Employee2>>(EmpResponse);
                }

                return null;
                //return View();
            }
        }
    }
}

//MyClass myObject = new MyClass();
//myObject.level = 1;
//myObject.timeElapsed = 47.5f;
//myObject.playerName = "Dr Charles Francis";
//string json = JsonUtility.ToJson(myObject);
//{"level":1,"timeElapsed":47.5,"playerName":"Dr Charles Francis"}
//myObject = JsonUtility.FromJson<MyClass>(json);

// Convert Employee object to JSON format (Serialization) 
//string jsonString = JsonConvert.SerializeObject(emp);
// Convert JSON text to Employee object (Deserialization) 
//Employee empObj = JsonConvert.DeserializeObject<Employee>(jsonString);

