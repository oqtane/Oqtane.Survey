using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Modules;
using Oqtane.Services;
using Oqtane.Shared;
using Oqtane.Survey.Models;
using Radzen;

namespace Oqtane.Survey.Services
{
    public class SurveyAnswersService : ServiceBase, ISurveyAnswersService, IService
    {
        private readonly SiteState _siteState;

        public SurveyAnswersService(HttpClient http, SiteState siteState) : base(http)
        {
            _siteState = siteState;
        }

        private string Apiurl => CreateApiUrl("SurveyAnswers", _siteState.Alias);

        public async Task<List<Models.SurveyItem>> SurveyResultsDataAsync(int ModuleId, int SelectedSurveyId, LoadDataArgs args)
        {
            return await PostJsonAsync<LoadDataArgs,List<Models.SurveyItem>>(
                CreateAuthorizationPolicyUrl($"{Apiurl}/{SelectedSurveyId}", EntityNames.Module, ModuleId), 
                args);
        }

        public async Task CreateSurveyAnswersAsync(Models.Survey Survey)
        {
            await PostJsonAsync(CreateAuthorizationPolicyUrl($"{Apiurl}", EntityNames.Module, Survey.ModuleId), Survey);
        }
    }
}
