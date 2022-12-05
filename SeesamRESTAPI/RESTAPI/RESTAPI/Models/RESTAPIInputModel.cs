using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTAPI.Models
{
    public class RESTAPIInputModel
    {
        public string txtBankName { get; set; }
        public string txtPersnr { get; set; }
        public string txtkundnr { get; set; }

        public string getTogether()
        {
            return txtBankName + " " + txtPersnr + " " + txtkundnr;
        }
    }
}