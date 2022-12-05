using MultiHttpClient.Models;
using MultiHttpClient.ViewModel;
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

namespace MultiHttpClient.Controllers
{
    public class HomeController : Controller
    {

        string baseUrl = "http://10.103.0.60:8810/wb/rest/wbService/";
        HttpClient client = new HttpClient();

        public HomeController()
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync("getAllCats");
            if (!response.IsSuccessStatusCode)
            {

            }

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

            HttpResponseMessage response2 = await client.GetAsync("getLiik");
            if (!response2.IsSuccessStatusCode)
            {

            }

            var jsonString2 = response2.Content.ReadAsStringAsync().Result;
            var jsonResponse2 = JObject.Parse(jsonString2);

            var liikIdString = jsonResponse2["response"]["getLiik"]["getLiik"].ToString();
            var liikIdaList = JsonConvert.DeserializeObject<List<LiikData>>(liikIdString);

            var successLiik = bool.Parse(jsonResponse2["response"]["success"].ToString());
            var successMsgLiik = jsonResponse2["response"]["successmessage"].ToString();

            var modelLiik = new ResponseLiik
            {
                Success = successLiik,
                SuccessMessage = successMsgLiik,
                LiikData = liikIdaList
            };

            var vmodel = new MultiHttpClientViewModel
            {
                Response = model,
                ResponseLiik = modelLiik
            };

            return View(vmodel);

        }

        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage response = await client.GetAsync("getCat/" + id.ToString());
            if (!response.IsSuccessStatusCode)
            {

            }

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

            HttpResponseMessage response2 = await client.GetAsync("getLiik");
            if (!response2.IsSuccessStatusCode)
            {

            }

            var jsonString2 = response2.Content.ReadAsStringAsync().Result;
            var jsonResponse2 = JObject.Parse(jsonString2);

            var liikIdString = jsonResponse2["response"]["getLiik"]["getLiik"].ToString();
            var liikIdaList = JsonConvert.DeserializeObject<List<LiikData>>(liikIdString);

            var successLiik = bool.Parse(jsonResponse2["response"]["success"].ToString());
            var successMsgLiik = jsonResponse2["response"]["successmessage"].ToString();

            var modelLiik = new ResponseLiik
            {
                Success = successLiik,
                SuccessMessage = successMsgLiik,
                LiikData = liikIdaList
            };

            var vmodel = new MultiHttpClientViewModel
            {
                Response = model,
                ResponseLiik = modelLiik
            };

            return View(vmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MultiHttpClientViewModel input)
        {
            string inputJson = JsonConvert.SerializeObject(new { request = input.Response.CatData[0]});
            HttpContent inputContent = new StringContent(inputJson, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response1 = client.PutAsync("updateCat/", inputContent).Result;
    
            if (response1.IsSuccessStatusCode)
            {
                var st = response1;
            }
            else
            {
                var st = response1.StatusCode;
            }
            //"{\"request\":{\"BankName\":\"Miizu\",\"Persnr\":\"Cat\",\"kundnr\":2,\"ExpectedAmount\":4.2}}"
            return RedirectToAction("Index");
        }

    }
}

//if (!ModelState.IsValid)
//    {
//        return BadRequest(ModelState);
//    }
