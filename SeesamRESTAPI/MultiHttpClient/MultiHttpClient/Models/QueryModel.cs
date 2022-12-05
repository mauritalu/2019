using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MultiHttpClient.Models
{
    public class CatData
    {
        [Required(ErrorMessage = "Sisesta nimi")]
        [DisplayName("BankName")]
        public string BankName { get; set; }

        [DisplayName("Persnr")]
        public string Persnr { get; set; }

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

    public class LiikData
    {
        [DisplayName("LiikId")]
        public string LiikId { get; set; }

        [DisplayName("LiikName")]
        public string LiikName { get; set; }
    }

    public class ResponseLiik
    {
        [DisplayName("Success")]
        public bool Success { get; set; }

        [DisplayName("SuccessMessage")]
        public string SuccessMessage { get; set; }

        public List<LiikData> LiikData { get; set; }
    }
}