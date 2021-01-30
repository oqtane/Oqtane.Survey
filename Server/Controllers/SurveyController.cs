using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Oqtane.Survey.Models;
using Oqtane.Survey.Repository;
using Oqtane.Repository;

namespace Oqtane.Survey.Controllers
{
    [Route(ControllerRoutes.Default)]
    public class SurveyController : Controller
    {
        private readonly ISurveyRepository _SurveyRepository;
        private readonly IUserRepository _users;
        private readonly ILogManager _logger;
        protected int _entityId = -1;

        public SurveyController(ISurveyRepository SurveyRepository, IUserRepository users, ILogManager logger, IHttpContextAccessor accessor)
        {
            _SurveyRepository = SurveyRepository;
            _users = users;
            _logger = logger;

            if (accessor.HttpContext.Request.Query.ContainsKey("entityid"))
            {
                _entityId = int.Parse(accessor.HttpContext.Request.Query["entityid"]);
            }
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public IEnumerable<Models.Survey> Get(string moduleid)
        {
            // FIX return _SurveyRepository.GetSurveys(int.Parse(moduleid));
            return new List<Models.Survey>();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public Models.Survey Get(int id)
        {
            // FIX Models.Survey Survey = _SurveyRepository.GetSurvey(id);
            //if (Survey != null && Survey.ModuleId != _entityId)
            //{
            //    Survey = null;
            //}
            //return Survey;
            return new Models.Survey();
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.Survey Post([FromBody] Models.Survey Survey)
        {
            if (ModelState.IsValid && Survey.ModuleId == _entityId)
            {
                // Get User
                var User = _users.GetUser(this.User.Identity.Name);

                // Add User to Survey object
                Survey.UserId = User.UserId;

                // FIX Survey = _SurveyRepository.AddSurvey(Survey);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "Survey Added {Survey}", Survey);
            }
            return Survey;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.Survey Put(int id, [FromBody] Models.Survey Survey)
        {
            // FIX if (ModelState.IsValid && Survey.ModuleId == _entityId)
            //{
            //    Survey = _SurveyRepository.UpdateSurvey(Survey);
            //    _logger.Log(LogLevel.Information, this, LogFunction.Update, "Survey Updated {Survey}", Survey);
            //}
            return Survey;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public void Delete(int id)
        {
            // FIX Models.Survey Survey = _SurveyRepository.GetSurvey(id);
            //if (Survey != null && Survey.ModuleId == _entityId)
            //{
            //    _SurveyRepository.DeleteSurvey(id);
            //    _logger.Log(LogLevel.Information, this, LogFunction.Delete, "Survey Deleted {SurveyId}", id);
            //}
        }
    }
}
