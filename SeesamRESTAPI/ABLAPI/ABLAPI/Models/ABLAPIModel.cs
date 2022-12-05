using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ABLAPI.Models
{
    public class CatData
    {
        [Required(ErrorMessage = "Sisesta nimi")]
        [StringLength(20, MinimumLength = 3)]
        [DisplayName("BankName")]
        public string BankName { get; set; }
        [DisplayName("Persnr")]
        [Required(ErrorMessage = "Vali liik")]
        public string Persnr { get; set; }
        [DisplayName("Kundnr")]
        public int kundnr { get; set; }
        [Required(ErrorMessage = "Sisesta kaal")]
        [Range(0.1, 20, ErrorMessage = "Väärtus {0} peab olema decimal/number vahemikus {1} ja {2}.")]
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

    public class LiikData
    {
        public string LiikId { get; set; }
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

