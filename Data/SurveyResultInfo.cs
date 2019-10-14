using System;
namespace NewBlazor.Data
{
    public class SurveyResultInfo
    {
        public string  Answer { get; set; }
        public string Question { get; set; }
        public Guid SurveyId { get; set; }
    }
}
