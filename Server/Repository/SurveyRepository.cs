using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;
using Oqtane.Survey.Models;

namespace Oqtane.Survey.Repository
{
    public class SurveyRepository : ISurveyRepository, IService
    {
        private readonly SurveyContext _db;

        public SurveyRepository(SurveyContext context)
        {
            _db = context;
        }

        public IEnumerable<Models.Survey> GetSurveys(int ModuleId)
        {
            return _db.Survey.Where(item => item.ModuleId == ModuleId);
        }

        public Models.Survey GetSurvey(int SurveyId)
        {
            return _db.Survey.Find(SurveyId);
        }

        public Models.Survey AddSurvey(Models.Survey Survey)
        {
            _db.Survey.Add(Survey);
            _db.SaveChanges();
            return Survey;
        }

        public Models.Survey UpdateSurvey(Models.Survey Survey)
        {
            _db.Entry(Survey).State = EntityState.Modified;
            _db.SaveChanges();
            return Survey;
        }

        public void DeleteSurvey(int SurveyId)
        {
            Models.Survey Survey = _db.Survey.Find(SurveyId);
            _db.Survey.Remove(Survey);
            _db.SaveChanges();
        }
    }
}
