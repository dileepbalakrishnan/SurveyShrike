using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace NewBlazor.Data
{
    public class SurveyRepository : ISurveyRepository, IDisposable
    {
        private SurveyDbContext _context;

        public SurveyRepository(SurveyDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                GC.SuppressFinalize(this);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }

        public async Task<IEnumerable<Survey>> GetSurveyForUserAsync(string userId)
        {
            return await _context.Surveys.Where(s => s.Creator == userId).Include(s => s.Questions).ToListAsync();
        }

        public async Task SaveSurveyAsync(Survey survey)
        {
            _context.Surveys.Add(survey);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSurveyAsync(Guid surveyId)
        {
            var survey = await _context.Surveys.Where(s => s.Id == surveyId).Include(s => s.Questions).FirstOrDefaultAsync();
            _context.Surveys.Remove(survey);
            await _context.SaveChangesAsync();
        }

        public async Task<Survey> GetSurveyAsync(Guid surveyId)
        {
            var result = await _context.Surveys.Include(s => s.Questions).FirstOrDefaultAsync(s => s.Id == surveyId);
            if (result != null)
            {
                _context.Entry(result).Collection(s => s.Questions).Load();
            }
            return result;
        }

        public async Task<IEnumerable<SurveyQuestion>> GetSurveyQuestionsAsync(Guid surveyId)
        {
            var result = await _context.Surveys.Include(s => s.Questions).FirstOrDefaultAsync(s => s.Id == surveyId);
            IQueryable<SurveyQuestion> questions = null;
            if (result != null)
            {
                var questionIds = result.Questions.Select(q => q.Id).ToList();
                questions = _context.SurveyQuestions.Include(q => q.PossibleAnswers).Where(q => questionIds.Contains(q.Id));
            }
            if (questions != null)
            {
                return questions.ToList();
            }
            return new List<SurveyQuestion>();
        }

        public async Task SaveSurveyResultsAsync(IEnumerable<SurveyResult> surveyResults)
        {
            _context.SurveyResults.AddRange(surveyResults);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SurveyInfo>> GetAllSurveyInfosForUserAsync(string userId)
        {
            return await _context.Surveys.Select(s => new SurveyInfo { SurveyId = s.Id, Title = s.Title }).ToListAsync();
        }

        public async Task<IEnumerable<SurveyResult>> GetAllResultsForSurvey(Guid surveyId)
        {
            return await _context.SurveyResults.Include(s => s.Answer).Where(sr => sr.SurveyId == surveyId).ToListAsync();
        }

        public async Task<IEnumerable<SurveyResultInfo>> GetAllResultsInfosForSurvey(Guid surveyId)
        {
            List<SurveyResultInfo> surveyResultInfos = new List<SurveyResultInfo>();
            foreach (var item in await _context.SurveyResults.Include(s => s.Answer).Where(sr => sr.SurveyId == surveyId).ToListAsync())
            {
                var info = new SurveyResultInfo
                {
                    Answer = item.Answer.Answer,
                    SurveyId = item.SurveyId,
                    Question = _context.SurveyQuestions.First(q => q.Id == item.QuestionId).Text
                };
                surveyResultInfos.Add(info);
            }
            return surveyResultInfos;
        }

    }
}
