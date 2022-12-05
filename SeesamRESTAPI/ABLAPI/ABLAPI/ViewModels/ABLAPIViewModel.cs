using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ABLAPI.Models;
using System.Web.Mvc;

namespace ABLAPI.ViewModels
{
    public class ABLAPIViewModel
    {
        public CatData CatData { get; set; }
        public Response Response { get; set; }
        public Create_Response Create_Response { get; set; }
        public LiikData LiikData { get; set; }
        public ResponseLiik ResponseLiik { get; set; }
    }
}