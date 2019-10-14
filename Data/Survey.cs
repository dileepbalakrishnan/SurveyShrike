using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace NewBlazor.Data
{
    public class Survey
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title is too long.")]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        public string Creator { get; set; }
        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Today;
        [Required]
        public List<SurveyQuestion> Questions { get; set; } = new List<SurveyQuestion>();
        public List<SurveyResult> SurveyResults { get; set; } = new List<SurveyResult>();
    }
}
