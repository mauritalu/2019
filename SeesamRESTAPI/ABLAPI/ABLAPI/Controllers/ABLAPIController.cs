using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using ABLAPI.ViewModels;
using ABLAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace ABLAPI.Controllers
{
    public class ABLAPIController : Controller
    {
        string baseUrl = "http://10.103.0.60:8810/wb/rest/wbService/";
        HttpClient client = new HttpClient();

        public ABLAPIController()
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: ABLAPI/Error
        public ActionResult Error()
        {

            string path = "Andmeid ei leitud!";
            var qmodel = new Create_Response
            {
                SuccessMessage = path
            };

            var xModel = new ABLAPIViewModel
            {
                Create_Response = qmodel
            };

            return View(xModel);
        }

        // GET: ABLAPI
        public async Task<ActionResult> Index()
        {   
            HttpResponseMessage response = await client.GetAsync("getAllCats");
            if (!response.IsSuccessStatusCode)
            {
                return View(new Response { Success = false, SuccessMessage = "Päring ebaõnnestus (getAllCats)" });
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
                return View(new Response { Success = false, SuccessMessage = "Päring ebaõnnestus (getLiik)" });
            }

            FillResponse fillresponse = new FillResponse();

            var jsonString2 = response2.Content.ReadAsStringAsync().Result;
            var jsonResponse2 = JObject.Parse(jsonString2);

            var liikIdString = jsonResponse2["response"]["getLiik"]["getLiik"].ToString();
            var liikIdaList = JsonConvert.DeserializeObject<List<LiikData>>(liikIdString);

            var successLiik = bool.Parse(jsonResponse2["response"]["success"].ToString());
            var successMsgLiik = jsonResponse2["response"]["successmessage"].ToString();

            TempData["LiikList"] = liikIdaList;

            var modelLiik = new ResponseLiik
            {
                Success = successLiik,
                SuccessMessage = successMsgLiik,
                LiikData = liikIdaList
            };

            var vModel = new ABLAPIViewModel
            {
                Response = model,
                ResponseLiik = modelLiik
            };

            // Kui andmed olemas/puuduvad
            if (model.CatData.Count > 0)
            {
                return View(vModel);
            }
            else
            {
                string alertmessage = "Andmeid ei leitud! Lisa andmeid...";
                vModel.Create_Response.SuccessMessage = alertmessage;

                string CurrentController = (string)this.RouteData.Values["controller"];

                Response.AddHeader("Refresh", "3; url=" + CurrentController + "/Create");
                return View(vModel);
            }
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Index(string Otsi)
        //{
        //    Response model = PopulateResponseModel(Otsi);

        //    return View(model);
        //}

        //private static Response PopulateResponseModel(string Otsi)
        //{

        //}




        // GET: ABLAPI/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }



        // GET: ABLAPI/Create
        //public async Task<ActionResult> Create()
        public ActionResult Create()
        {
            var modelLiik = new ResponseLiik
            {
                LiikData = TempData["LiikList"] as List<LiikData>
            };
            TempData.Keep();

            var vModel = new ABLAPIViewModel
            {
                ResponseLiik = modelLiik
            };
            return View(vModel);
        }

        // POST: ABLAPI/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ABLAPIViewModel input)
        {
            if (ModelState.IsValid)
            {
                string inputJson = JsonConvert.SerializeObject(new { request = input.CatData });
                HttpContent inputContent = new StringContent(inputJson, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(baseUrl + "createCat", inputContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    Response.AddHeader("Refresh", "2; url=index");

                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var jsonResponse = JObject.Parse(jsonString);

                    var success = bool.Parse(jsonResponse["response"]["success"].ToString());
                    var successMsg = jsonResponse["response"]["successmessage"].ToString();
                    var kundnr_outMsg = int.Parse(jsonResponse["response"]["kundnr_out"].ToString());

                    var model = new Create_Response
                    {
                        Success = success,
                        SuccessMessage = successMsg + " ID:",
                        kundnr_out = kundnr_outMsg
                    };

                    var modelLiik = new ResponseLiik
                    {
                        LiikData = TempData["LiikList"] as List<LiikData>
                    };
                    TempData.Keep();

                    var vModel = new ABLAPIViewModel
                    {
                        ResponseLiik = modelLiik,
                        Create_Response = model
                    };

                    return View(vModel);
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            else
            {
                ModelState.AddModelError("", "Täida nõutud väljad!");
                var validationErrors = ModelState.Values.Where(E => E.Errors.Count > 0)
                                       .SelectMany(E => E.Errors)
                                       .Select(E => E.ErrorMessage)
                                       .ToList();
                ViewBag.ErrorList = validationErrors;

                var modelLiik = new ResponseLiik
                {
                    LiikData = TempData["LiikList"] as List<LiikData>
                };
                TempData.Keep();

                var vModel = new ABLAPIViewModel
                {
                    ResponseLiik = modelLiik
                };

                return View(vModel);
            }
        }



        // GET: ABLAPI/Edit/5
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

            var vmodel = new ABLAPIViewModel
            {
                Response = model,
                ResponseLiik = modelLiik
            };

            return View(vmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ABLAPIViewModel input)
        {
            string inputJson = JsonConvert.SerializeObject(new { request = input.Response.CatData[0] });
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
            return RedirectToAction("Index");
        }

        // POST: ABLAPI/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage response1 = await client.DeleteAsync(baseUrl + "deleteCat/" + id.ToString());

            if (response1.IsSuccessStatusCode)
            {
                //Response.AddHeader("Refresh", "3; url=index");

                var jsonString = response1.Content.ReadAsStringAsync().Result;
                var jsonResponse = JObject.Parse(jsonString);

                var success = bool.Parse(jsonResponse["response"]["success"].ToString());
                var successMsg = jsonResponse["response"]["successmessage"].ToString();
                var kundnr_outMsg = int.Parse(jsonResponse["response"]["kundnr_out"].ToString());

                var model = new Create_Response
                {
                    Success = success,
                    SuccessMessage = successMsg,
                    kundnr_out = kundnr_outMsg
                };

                var vModel = new ABLAPIViewModel
                {
                    Create_Response = model
                };

                TempData["success"] = success;
                TempData["successMsg"] = successMsg + " (ID:" + kundnr_outMsg + ")";
                TempData["kundnr_outMsg"] = kundnr_outMsg;

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

    }
}
