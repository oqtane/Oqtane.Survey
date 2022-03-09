using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Models;
using Oqtane.Infrastructure;
using Oqtane.Enums;
using Oqtane.Survey.Models;
using Oqtane.Survey.Repository;
using Oqtane.Survey.Server.Repository;
using Oqtane.Migrations.Framework;
using Oqtane.Repository;
using Oqtane.Shared;

namespace Oqtane.Survey.Manager
{
    public class SurveyManager : MigratableModuleBase, IInstallable, IPortable
    {
        private ISurveyRepository _SurveyRepository;
        private readonly ITenantManager _tenantManager;
        private readonly IHttpContextAccessor _accessor;
        private ISqlRepository _sql;

        public SurveyManager(ISurveyRepository SurveyRepository, ITenantManager tenantManager, IHttpContextAccessor accessor, ISqlRepository sql)
        {
            _SurveyRepository = SurveyRepository;
            _tenantManager = tenantManager;
            _accessor = accessor;
            _sql = sql;
        }

        public bool Install(Tenant tenant, string version)
        {
            if (tenant.DBType == Constants.DefaultDBType && version == "2.0.0")
            {
                // prior versions used SQL scripts rather than migrations, so we need to seed the migration history table
                _sql.ExecuteNonQuery(tenant, MigrationUtils.BuildInsertScript("Survey.01.00.00.00"));
                _sql.ExecuteNonQuery(tenant, MigrationUtils.BuildInsertScript("Survey.01.00.02.00"));
            }
            return Migrate(new SurveyContext(_tenantManager, _accessor), tenant, MigrationType.Up);
        }

        public bool Uninstall(Tenant tenant)
        {
            return Migrate(new SurveyContext(_tenantManager, _accessor), tenant, MigrationType.Down);
        }

        public string ExportModule(Module module)
        {
            string content = "";
            List<OqtaneSurvey> Surveys = _SurveyRepository.GetAllSurveysByModule(module.ModuleId).ToList();
            if (Surveys != null)
            {
                content = JsonSerializer.Serialize(Surveys);
            }
            return content;
        }

        public void ImportModule(Module module, string content, string version)
        {
            List<OqtaneSurvey> Surveys = null;
            if (!string.IsNullOrEmpty(content))
            {
                Surveys = JsonSerializer.Deserialize<List<OqtaneSurvey>>(content);
            }
            if (Surveys != null)
            {
                foreach(var Survey in Surveys)
                {
                    _SurveyRepository.CreateSurvey(
                        new Models.Survey
                        { 
                            ModuleId = module.ModuleId, 
                            SurveyName = Survey.SurveyName,
                            UserId = Survey.UserId
                        });
                }
            }
        }
    }
}