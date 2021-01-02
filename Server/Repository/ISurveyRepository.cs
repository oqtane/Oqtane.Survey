using System.Collections.Generic;
using Oqtane.Survey.Models;

namespace Oqtane.Survey.Repository
{
    public interface ISurveyRepository
    {
        IEnumerable<Models.Survey> GetSurveys(int ModuleId);
        Models.Survey GetSurvey(int SurveyId);
        Models.Survey AddSurvey(Models.Survey Survey);
        Models.Survey UpdateSurvey(Models.Survey Survey);
        void DeleteSurvey(int SurveyId);
    }
}
