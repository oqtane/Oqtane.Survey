using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Repository;
using Oqtane.Survey.Models;

namespace Oqtane.Survey.Repository
{
    public class SurveyContext : DBContextBase, IService
    {
        public virtual DbSet<Models.Survey> Survey { get; set; }

        public SurveyContext(ITenantResolver tenantResolver, IHttpContextAccessor accessor) : base(tenantResolver, accessor)
        {
            // ContextBase handles multi-tenant database connections
        }
    }
}
