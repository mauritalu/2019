using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;


namespace Google.Controllers
{
    public class GoogleController : Controller
    {
        // GET: Google
        //public ActionResult Index()
        //public async Task<ActionResult> Index()
        //{           
        //    string Baseurl = "http://10.103.0.60:8810/";

        //    using (var client = new HttpClient())
        //    {
        //        //Passing service base url
        //        client.BaseAddress = new Uri(Baseurl);

        //        client.DefaultRequestHeaders.Clear();
        //        //Define request data format
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        //Sending request to find web api REST service resource GetAllEmployees using HttpClient
        //        HttpResponseMessage Res = await client.GetAsync("wb/rest/wbService/getAllCats");

        //        //Checking the response is successful or not which is sent using HttpClient
        //        //if (Res.IsSuccessStatusCode)
        //        // {
        //        //Storing the response details received from web api
        //        var EmpResponse = Res.Content.ReadAsStringAsync().Result;

        //        //PROOV 1
        //        //var jsonObj = JsonConvert.DeserializeObject<JObject>(EmpResponse).First.First;
        //        //var jsonObj1 = (jsonObj["successmessage"]);

        //        //PROOV 2

        //        JObject googleSearch = JObject.Parse(EmpResponse);

        //        // get JSON result objects into a list
        //        IList<JToken> results = googleSearch["response"]["getCatData"]["getCatData"].Children().ToList();


        //        // serialize JSON results into .NET objects
        //        IList<SearchResult> searchResults = new List<SearchResult>();

        //        foreach (JToken result in results)
        //        {
        //            // JToken.ToObject is a helper method that uses JsonSerializer internally
        //            SearchResult searchResult = result.ToObject<SearchResult>();
        //            searchResults.Add(searchResult);
        //        }

        //        var vaata = searchResults;

        //        return View(searchResults);
        //    }
        //}

        private void alert(string message)
        {
            Response.Write("<script>alert('" + message + "')</script>");
        }

        public async Task<ActionResult> Index()
        {
            string baseUrl = "http://10.103.0.60:8810/";
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(baseUrl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage response = await client.GetAsync("wb/rest/wbService/getAllCats");

                //Checking the response is successful or not which is sent using HttpClient
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var jsonResponse = JObject.Parse(jsonString);

                    var catDataString = jsonResponse["response"]["getCatData"]["getCatData"].ToString();
                    var catDataList = JsonConvert.DeserializeObject<List<CatData>>(catDataString);

                    var success = bool.Parse(jsonResponse["response"]["success"].ToString());
                    var successMsg = jsonResponse["response"]["successmessage"].ToString();

                    var model = new Response
                    {
                        Success = success,
                        SuccessMessage = successMsg,
                        CatData = catDataList
                    };
                    
                    return View(model);
                }
                else
                {
                    // mingi error handling siia?
                    return View(new Response { Success = false, SuccessMessage = "Päring ebaõnnestus" });
                }

            }
        }

        //POST: Google
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CatData newCatData)
        
        //public async Task<ActionResult> Create(CatData cd)
        {
            using (var client = new HttpClient())
            {
                string inputJson = JsonConvert.SerializeObject(new { request = newCatData });
                HttpContent inputContent = new StringContent(inputJson, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response1 = client.PostAsync("http://10.103.0.60:8810/wb/rest/wbService/createCat/", inputContent).Result;
                if (response1.IsSuccessStatusCode)
                {
                    Response.AddHeader("Refresh", "3; url=index");

                    var jsonString    = response1.Content.ReadAsStringAsync().Result;
                    var jsonResponse  = JObject.Parse(jsonString);

                    var success       = bool.Parse(jsonResponse["response"]["success"].ToString());
                    var successMsg    = jsonResponse["response"]["successmessage"].ToString();
                    var kundnr_outMsg = int.Parse(jsonResponse["response"]["kundnr_out"].ToString());

                    var model1 = new Create_Response
                    {
                        Success        = success,
                        SuccessMessage = successMsg,
                        kundnr_out     = kundnr_outMsg
                    };
                    TempData["success"] = success;
                    TempData["successMsg"] = successMsg + " (ID:" + kundnr_outMsg + ")";
                    TempData["kundnr_outMsg"] = kundnr_outMsg;
                    
                }
            }
            //Thread.Sleep(4000);
            //return RedirectToAction("Index");
            return null;
        }



        public async Task<ActionResult> Edit(int id)
        {
            string baseUrl = "http://10.103.0.60:8810/";
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(baseUrl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage response = await client.GetAsync("wb/rest/wbService/getCat/" + id.ToString());

                //Checking the response is successful or not which is sent using HttpClient
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var jsonResponse = JObject.Parse(jsonString);

                    var catDataString = jsonResponse["response"]["getCatData"]["getCatData"].ToString();
                    var catDataList = JsonConvert.DeserializeObject<List<CatData>>(catDataString);

                    var success = bool.Parse(jsonResponse["response"]["success"].ToString());
                    var successMsg = jsonResponse["response"]["successmessage"].ToString();

                    var model = new Response
                    {
                        Success = success,
                        SuccessMessage = successMsg,
                        CatData = catDataList
                    };

                    return View(model);
                }
                else
                {
                    // mingi error handling siia?
                    return View(new Response { Success = false, SuccessMessage = "Päring ebaõnnestus" });
                }

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Response input)

        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    string inputJson = JsonConvert.SerializeObject(new { request = input.CatData[0] });
                    HttpContent inputContent = new StringContent(inputJson, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response1 = client.PutAsync("http://10.103.0.60:8810/wb/rest/wbService/updateCat/", inputContent).Result;
                    if (response1.IsSuccessStatusCode)
                    {
                        var st = response1;
                    }
                    else
                    {
                        var st = response1.StatusCode;
                    }
                }
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "LogID already taken");

                var validationErrors = ModelState.Values.Where(E => E.Errors.Count > 0)
                                    .SelectMany(E => E.Errors)
                                    .Select(E => E.ErrorMessage)
                                    .ToList();
                // now you have got the list of errors, you will need to pass it to view you can use view model, viewbag etc
                ViewBag.ErrorList = validationErrors;

                return View(input);
            }
            //return RedirectToAction("Index");
           
        }


        public async Task<ActionResult> Delete(int id)
        {
            string baseUrl = "http://10.103.0.60:8810/";
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(baseUrl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage response1 = await client.DeleteAsync("http://10.103.0.60:8810/wb/rest/wbService/deleteCat/" + id.ToString());
                Response.Write("<script>alert('Tere')</script>");
                //Checking the response is successful or not which is sent using HttpClient
                if (response1.IsSuccessStatusCode)
                {

                    var jsonString = response1.Content.ReadAsStringAsync().Result;
                    var jsonResponse = JObject.Parse(jsonString);

                    var success = bool.Parse(jsonResponse["response"]["success"].ToString());
                    var successMsg = jsonResponse["response"]["successmessage"].ToString();
                    var kundnr_outMsg = int.Parse(jsonResponse["response"]["kundnr_out"].ToString());

                    TempData["success"] = success;
                    TempData["successMsg"] = successMsg + " (ID:" + kundnr_outMsg + ")";
                    TempData["kundnr_outMsg"] = kundnr_outMsg;
                }
            }
            //Response.Write("<script>alert('Inserted successfully!')</script>");
            //Response.Write("<script>alert('Inserted..');window.location = 'Index.cshtml';</script>");
            
            return RedirectToAction("Index");
        }

    }
}


//using System;
//using Newtonsoft.Json;
//using System.Collections.Generic;
//public class Program
//{
//    public static void Main()
//    {
//        //		string json="{ 'Status': 'SUCCESS', 'total': 3, 'results': { 'Platform': [{ 'type': 'AA', 'Id': '420', 'service': false, 'description': '', 'imageUrl': 'http://www.somelink.com/file.jpg', 'date': '1457608735000' }, { 'type': 'BB', 'Id': '307', 'service': false, 'description': '', 'imageUrl': 'http://www.somelink.com/file.jpg', 'date': '1457608782000' }, { 'type': 'CC', 'Id': '114', 'service': true, 'description': '', 'imageUrl': 'http://www.somelink.com/file.jpg', 'date': '1457611015000' }] } }";

//        string json = "{ 'success': true,'successmessage': 'Record(s) found!', 'getCatData': {'getCatData': [{'BankName': 'Kiizu','Persnr': 'Cat','kundnr': 1,'ExpectedAmount': 0.0}]}}";


//        response tmp = JsonConvert.DeserializeObject<response>(json);
//        response response = JsonConvert.DeserializeObject<response>(json);

//        Results r = new Results();
//        r = tmp.getCatData;
//        foreach (getCatData p in r.getCatData)
//        {
//            Console.WriteLine("BankName=" + p.BankName + "\t Persnr=" + p.Persnr + "\t kundnr=" + p.kundnr + "\t ExpectedAmount=" + p.ExpectedAmount);
//        }
//        Console.WriteLine(response.successmessage);

//    }

//}
//public class getCatData
//{
//    public string BankName { get; set; }
//    public string Persnr { get; set; }
//    public int kundnr { get; set; }
//    public double ExpectedAmount { get; set; }
//}

//public class Results
//{
//    public List<getCatData> getCatData { get; set; }
//}

//public class response
//{
//    public bool success { get; set; }
//    public string successmessage { get; set; }
//    public Results getCatData { get; set; }
//}

//public class RootObject
//{
//    public string response { get; set; }
//}


