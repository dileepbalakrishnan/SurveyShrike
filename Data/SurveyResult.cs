using System;
using System.ComponentModel.DataAnnotations;
namespace NewBlazor.Data
{
    public class SurveyResult
    {
        [Key]
        public Guid Id { get; set; }
        public Guid SurveyId { get; set; }
        public Guid QuestionId { get; set; }
        public PossibleAnswer Answer { get; set; }
    }
}
