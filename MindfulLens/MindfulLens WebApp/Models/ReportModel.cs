using System.ComponentModel.DataAnnotations;

namespace MindfulLens_WebApp.Models
{
    public class ReportModel
    {
        [Required]
        [MinLength(5, ErrorMessage = "Report text length must be at least 5 characters long")]
        public string Text { get; set; }

        [Required]
        [Length(5, 500, ErrorMessage = "Report reason length must be between 5 and 500 characters long")]
        public string Reason { get; set; }

        public ReportModel() { }
    }
}
