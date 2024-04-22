using System.ComponentModel.DataAnnotations;

namespace Tool.Web.Models
{
    public class DataModel
    {
        public string __RequestVerificationToken { get; set; }
        [Required(ErrorMessage = "Please enter the valid URL")]
        public string URL { get; set; }
    }
}