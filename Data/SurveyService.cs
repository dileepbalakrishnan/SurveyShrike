using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace NewBlazor.Data
{
    public class SurveyService 
    {
        ISurveyRepository _surveyRepository;
        SurveyEmailSender _surveyEmailSender;
        public SurveyService(ISurveyRepository surveyRepository, SurveyEmailSender surveyEmailSender)
        {
            _surveyRepository = surveyRepository ?? throw new ArgumentNullException(nameof(surveyRepository));
            _surveyEmailSender = surveyEmailSender ?? throw new ArgumentNullException(nameof(surveyEmailSender));
        }

        public async Task DeleteSurveyAsync(Guid surveyId)
        {
            await _surveyRepository.DeleteSurveyAsync(surveyId);
        }

        public async Task<List<Survey>> GetSurveyForUserAsync(string userId)
        {
            var surveys = await _surveyRepository.GetSurveyForUserAsync(userId);
            return surveys.ToList();
        }

        public async Task SaveSurveyAsync(Survey survey)
        {
            await _surveyRepository.SaveSurveyAsync(survey);
        }

        public async Task<Survey> GetSurveyAsync(Guid surveyId)
        {
            return await _surveyRepository.GetSurveyAsync(surveyId);
        }

        public async Task<List<SurveyQuestion>> GetSurveyQuestions(Guid surveyId)
        {
            var questions = await _surveyRepository.GetSurveyQuestionsAsync(surveyId);
            return questions.ToList();
        }
        public async Task SaveSurveyResultsAsync(IEnumerable<SurveyResult> surveyResults)
        {
            await _surveyRepository.SaveSurveyResultsAsync(surveyResults);
        }
        public async Task<List<SurveyInfo>> GetAllSurveyInfosForUserAsync(string userId)
        {
            var result = await _surveyRepository.GetAllSurveyInfosForUserAsync(userId);
            return result.ToList();
        }
        public async Task SendSurveyAsync(SurveyDispatchInfo surveyDispatchInfo)
        {
            await _surveyEmailSender.SendSurvey(surveyDispatchInfo);
        }
        public async Task<List<SurveyResult>> GetAllResultsForSurvey(Guid surveyId)
        {
            var result =  await _surveyRepository.GetAllResultsForSurvey(surveyId);
            return result.ToList();
        }
        public async Task<List<SurveyResultInfo>> GetAllResultsInfosForSurvey(Guid surveyId)
        {
            var result =  await _surveyRepository.GetAllResultsInfosForSurvey(surveyId);
            return result.ToList();
        }
    }
}
