using System.Collections.Generic;
using System.Threading.Tasks;
using Oqtane.Survey.Models;
using Radzen;

namespace Oqtane.Survey.Services
{
    public interface ISurveyAnswersService
    {
        Task<List<Models.SurveyItem>> SurveyResultsDataAsync(int SelectedSurveyId, LoadDataArgs args);
        Task CreateSurveyAnswersAsync(Models.Survey Survey);
    }
}