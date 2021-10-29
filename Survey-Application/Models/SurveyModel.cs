using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Survey_Application.Models
{
    public class SurveyModel
    {
        [Required(ErrorMessage = "CustomerID is required.")]
        public int CustomerID { get; set; }
        [Required(ErrorMessage = "QuestionID is required.")]
        public int QuestionID { get; set; }
        [Required(ErrorMessage = "Response is required.")]
        public int Response { get; set; }
    }
}