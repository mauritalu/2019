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

namespace ABLAPI.Models
{
    public class FillResponse
    {
        public FillResponse()
        {
            HttpClient client = new HttpClient();
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:64195/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}