using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Google.Models
{
    //public class SearchResult
    //{
    //    public bool success { get; set; }
    //    public string successmessage { get; set; }
    //    public string BankName { get; set; }
    //    public string Persnr { get; set; }
    //    public int kundnr { get; set; }
    //    public double ExpectedAmount { get; set; }
    //    public IList<string> getCatData { get; set; }
    //}

    public class CatData
    {
        [Required(ErrorMessage = "Sisesta midagi")]
        [DisplayName("BankName")]
        public string BankName { get; set; }

        [Required]
        [DisplayName("Persnr")]
        public string Persnr { get; set; }

        [Required]
        [DisplayName("Kundnr")]
        public int kundnr { get; set; }

        [Required]
        [Range(0.0, 20, ErrorMessage = "Value {0} must be a decimal/number between {1} and {2}.")]
        [DisplayName("ExpectedAmount")]
        public double ExpectedAmount { get; set; }
    }

    public class Response
    {
        [DisplayName("Success")]
        public bool Success { get; set; }

        [DisplayName("SuccessMessage")]
        public string SuccessMessage { get; set; }

        public List<CatData> CatData { get; set; }
    }





    public class Create_Response
    {
        [DisplayName("Success")]
        public bool Success { get; set; }

        [DisplayName("SuccessMessage")]
        public string SuccessMessage { get; set; }

        [DisplayName("kundnr_out")]
        public int kundnr_out { get; set; }

    }
}