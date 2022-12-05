using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace Cat.Models
{
    public class CatClient
    {
        private string BASE_URL = "http://10.103.0.60:8810/wb/rest/wbService/"; 

        public IEnumerable<Cat>findAll()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BASE_URL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("testCat").Result;

                string jsonResult = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(jsonResult);

                if (response.IsSuccessStatusCode) { 
                    var data = response.Content.ReadAsAsync<IEnumerable<Cat>>().Result;
                    return response.Content.ReadAsAsync<IEnumerable<Cat>>().Result;
                }    
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}