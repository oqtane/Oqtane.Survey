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

         private string Apiurl => CreateApiUrl("SurveyItem", _siteState.Alias);

        public async Task<List<Models.SurveyItem>> GetSurveyItemsAsync(int ModuleId)
        {
            List<Models.SurveyItem> Surveys = await GetJsonAsync<List<Models.SurveyItem>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", EntityNames.Module, ModuleId));
            return Surveys.OrderBy(item => item.Position).ToList();
        }

        public async Task<Models.SurveyItem> GetSurveyItemAsync(int ModuleId)
        {
            return await GetJsonAsync<Models.SurveyItem>(CreateAuthorizationPolicyUrl($"{Apiurl}/{ModuleId}", EntityNames.Module, ModuleId));
        }

        public async Task<Models.SurveyItem> AddSurveyItemAsync(Models.SurveyItem SurveyItem)
        {
            return await PostJsonAsync<Models.SurveyItem>(CreateAuthorizationPolicyUrl($"{Apiurl}", EntityNames.Module, SurveyItem.ModuleId), SurveyItem);
        }
        public async Task<Models.SurveyItem> MoveSurveyItemAsync(string MoveType, Models.SurveyItem SurveyItem)
        {
            return await PostJsonAsync<Models.SurveyItem>(CreateAuthorizationPolicyUrl($"{Apiurl}/{MoveType}", EntityNames.Module, SurveyItem.ModuleId), SurveyItem);
        }

        public async Task<Models.SurveyItem> UpdateSurveyItemAsync(Models.SurveyItem SurveyItem)
        {
            return await PutJsonAsync<Models.SurveyItem>(CreateAuthorizationPolicyUrl($"{Apiurl}/{SurveyItem.Id}", EntityNames.Module, SurveyItem.ModuleId), SurveyItem);
        }

        public async Task DeleteSurveyItemAsync(Models.SurveyItem SurveyItem)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{SurveyItem.Id}", EntityNames.Module, SurveyItem.ModuleId));
        }
    }
}
