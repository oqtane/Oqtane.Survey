using System.Collections.Generic;
using System.Threading.Tasks;
using Oqtane.Survey.Models;

namespace Oqtane.Survey.Services
{
    public interface ISurveyItemService 
    {
        Task<List<Models.SurveyItem>> GetSurveyItemsAsync(int ModuleId);

        Task<Models.SurveyItem> GetSurveyItemAsync(int ModuleId);

        Task<Models.SurveyItem> AddSurveyItemAsync(Models.SurveyItem SurveyItem);

        Task<Models.SurveyItem> UpdateSurveyItemAsync(Models.SurveyItem SurveyItem);

        Task DeleteSurveyItemAsync(int ModuleId);
    }
}
