using System;
using System.Collections.Generic;

namespace NewBlazor.Data
{
    public class SurveyDispatchInfo
    {
        public Guid SelectedSurvey { get; set; }
        public List<string> RecipientList { get; set; } = new List<string>();
        public string Title { get; set; }
    }
}
