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

         private string Apiurl => CreateApiUrl(_siteState.Alias, "Survey");

        public async Task<List<Models.Survey>> GetSurveysAsync(int ModuleId)
        {
            List<Models.Survey> Surveys = await GetJsonAsync<List<Models.Survey>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", ModuleId));
            return Surveys.OrderBy(item => item.SurveyName).ToList();
        }

        public async Task<Models.Survey> GetSurveyAsync(int SurveyId, int ModuleId)
        {
            return await GetJsonAsync<Models.Survey>(CreateAuthorizationPolicyUrl($"{Apiurl}/{SurveyId}", ModuleId));
        }

        public async Task<Models.Survey> AddSurveyAsync(Models.Survey Survey)
        {
            return await PostJsonAsync<Models.Survey>(CreateAuthorizationPolicyUrl($"{Apiurl}", Survey.ModuleId), Survey);
        }

        public async Task<Models.Survey> UpdateSurveyAsync(Models.Survey Survey)
        {
            return await PutJsonAsync<Models.Survey>(CreateAuthorizationPolicyUrl($"{Apiurl}/{Survey.SurveyId}", Survey.ModuleId), Survey);
        }

        public async Task DeleteSurveyAsync(int SurveyId, int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{SurveyId}", ModuleId));
        }
    }
}
