using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace JSONTest.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public float Price { get; set; }
    }

    public class SampleAPIClient
    {
        private const string ApiUri = "http://10.103.0.60:8810/wb/rest/wbService/testCat";

        public async Task<IEnumerable<Car>> GetCarsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(ApiUri);

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<IEnumerable<Car>>();
            }
        }
    }
}