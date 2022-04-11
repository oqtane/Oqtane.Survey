using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Modules;
using Oqtane.Services;
using Oqtane.Shared;
using Oqtane.Survey.Models;

namespace Oqtane.Survey.Services
{
    public class SurveyService : ServiceBase, ISurveyService, IService
    {
        private readonly SiteState _siteState;

        public SurveyService(HttpClient http, SiteState siteState) : base(http)
        {
            _siteState = siteState;
        }

         private string Apiurl => CreateApiUrl("Survey", _siteState.Alias);

        public async Task<List<Models.Survey>> GetSurveysAsync(int ModuleId)
        {
            List<Models.Survey> Surveys = await GetJsonAsync<List<Models.Survey>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", EntityNames.Module, ModuleId));
            return Surveys.OrderBy(item => item.SurveyName).ToList();
        }

        public async Task<Models.Survey> GetSurveyAsync(int ModuleId)
        {
            return await GetJsonAsync<Models.Survey>(CreateAuthorizationPolicyUrl($"{Apiurl}/{ModuleId}", EntityNames.Module, ModuleId));
        }

        public async Task<Models.Survey> AddSurveyAsync(Models.Survey Survey)
        {
            return await PostJsonAsync<Models.Survey>(CreateAuthorizationPolicyUrl($"{Apiurl}", EntityNames.Module, Survey.ModuleId), Survey);
        }

        public async Task<Models.Survey> UpdateSurveyAsync(Models.Survey Survey)
        {
            return await PutJsonAsync<Models.Survey>(CreateAuthorizationPolicyUrl($"{Apiurl}/{Survey.ModuleId}", EntityNames.Module, Survey.ModuleId), Survey);
        }

        public async Task DeleteSurveyAsync(int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{ModuleId}", EntityNames.Module, ModuleId));
        }
    }
}
