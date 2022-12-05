using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MultiHttpClient.Models;

namespace MultiHttpClient.ViewModel
{
    public class MultiHttpClientViewModel
    {
        public CatData CatData { get; set; }
        public Response Response { get; set; }

        public LiikData LiikData { get; set; }
        public ResponseLiik ResponseLiik { get; set; }
    }
}