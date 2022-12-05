using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace JSONTestURI.Models
{

    class Employee2
    {
        public List<Item> response { get; set; }
    }
    class Item
    {
        public string BankName { get; set; }
        public string Persnr { get; set; }
        public int kundnr { get; set; }
        public double ExpectedAmount { get; set; }

    }
}


//{
//    "response": {
//        "success": true,
//        "successmessage": "Service found!"
//    }
//}