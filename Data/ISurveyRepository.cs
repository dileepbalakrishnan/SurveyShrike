using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace NewBlazor.Data
{
    public interface ISurveyRepository
    {
        Task<IEnumerable<Survey>> GetSurveyForUserAsync(string userId);
        Task SaveSurveyAsync(Survey survey);
        Task DeleteSurveyAsync(Guid surveyId);
        Task<Survey> GetSurveyAsync(Guid surveyId);
        Task<IEnumerable<SurveyQuestion>> GetSurveyQuestionsAsync(Guid surveyId);
        Task SaveSurveyResultsAsync(IEnumerable<SurveyResult> surveyResults);
        Task<IEnumerable<SurveyInfo>> GetAllSurveyInfosForUserAsync(string userId);
        Task<IEnumerable<SurveyResult>> GetAllResultsForSurvey(Guid surveyId);
        Task<IEnumerable<SurveyResultInfo>> GetAllResultsInfosForSurvey(Guid surveyId);
    }
}
