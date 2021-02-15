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
    public class SurveyItemService : ServiceBase, ISurveyItemService, IService
    {
        private readonly SiteState _siteState;

        public SurveyItemService(HttpClient http, SiteState siteState) : base(http)
        {
            _siteState = siteState;
        }

         private string Apiurl => CreateApiUrl(_siteState.Alias, "SurveyItem");

        public async Task<List<Models.SurveyItem>> GetSurveyItemsAsync(int ModuleId)
        {
            List<Models.SurveyItem> Surveys = await GetJsonAsync<List<Models.SurveyItem>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", ModuleId));
            return Surveys.OrderBy(item => item.Position).ToList();
        }

        public async Task<Models.SurveyItem> GetSurveyItemAsync(int ModuleId)
        {
            return await GetJsonAsync<Models.SurveyItem>(CreateAuthorizationPolicyUrl($"{Apiurl}/{ModuleId}", ModuleId));
        }

        public async Task<Models.SurveyItem> AddSurveyItemAsync(Models.SurveyItem SurveyItem)
        {
            return await PostJsonAsync<Models.SurveyItem>(CreateAuthorizationPolicyUrl($"{Apiurl}", SurveyItem.ModuleId), SurveyItem);
        }

        public async Task<Models.SurveyItem> UpdateSurveyItemAsync(Models.SurveyItem SurveyItem)
        {
            return await PutJsonAsync<Models.SurveyItem>(CreateAuthorizationPolicyUrl($"{Apiurl}/{SurveyItem.ModuleId}", SurveyItem.ModuleId), SurveyItem);
        }

        public async Task DeleteSurveyItemAsync(int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{ModuleId}", ModuleId));
        }
    }
}
