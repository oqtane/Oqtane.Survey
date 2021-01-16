using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Oqtane.Modules;
using Oqtane.Models;
using Oqtane.Infrastructure;
using Oqtane.Repository;
using Oqtane.Survey.Models;
using Oqtane.Survey.Repository;

namespace Oqtane.Survey.Manager
{
    public class SurveyManager : IInstallable, IPortable
    {
        private ISurveyRepository _SurveyRepository;
        private ISqlRepository _sql;

        public SurveyManager(ISurveyRepository SurveyRepository, ISqlRepository sql)
        {
            _SurveyRepository = SurveyRepository;
            _sql = sql;
        }

        public bool Install(Tenant tenant, string version)
        {
            return _sql.ExecuteScript(tenant, GetType().Assembly, "Oqtane.Survey." + version + ".sql");
        }

        public bool Uninstall(Tenant tenant)
        {
            return _sql.ExecuteScript(tenant, GetType().Assembly, "Oqtane.Survey.Uninstall.sql");
        }

        public string ExportModule(Module module)
        {
            string content = "";
            List<Models.Survey> Surveys = _SurveyRepository.GetSurveys(module.ModuleId).ToList();
            if (Surveys != null)
            {
                content = JsonSerializer.Serialize(Surveys);
            }
            return content;
        }

        public void ImportModule(Module module, string content, string version)
        {
            List<Models.Survey> Surveys = null;
            if (!string.IsNullOrEmpty(content))
            {
                Surveys = JsonSerializer.Deserialize<List<Models.Survey>>(content);
            }
            if (Surveys != null)
            {
                foreach(var Survey in Surveys)
                {
                    _SurveyRepository.AddSurvey(new Models.Survey { ModuleId = module.ModuleId, SurveyName = Survey.SurveyName });
                }
            }
        }
    }
}