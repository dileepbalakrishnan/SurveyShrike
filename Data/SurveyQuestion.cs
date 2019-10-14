using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBlazor.Data
{
    public class SurveyQuestion
    {
        [Key]
        public Guid Id { get; set; }
        [NotMapped]
        public int QuestionNumber { get; set; }
        [Required(ErrorMessage = "Text is required")]
        [StringLength(500, ErrorMessage = "Text is too long.")]
        [MaxLength(500)]
        public string Text { get; set; }
        [Required]
        public AnswerType AnswerType { get; set; }
        public List<PossibleAnswer> PossibleAnswers { get; set; } = new List<PossibleAnswer>();
        [NotMapped]
        public string AnswerString { get; set; }
    }
}
