using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Oqtane.Survey.Models;
using Oqtane.Survey.Repository;

namespace Oqtane.Survey.Controllers
{
    [Route(ControllerRoutes.Default)]
    public class SurveyController : Controller
    {
        private readonly ISurveyRepository _SurveyRepository;
        private readonly ILogManager _logger;
        protected int _entityId = -1;

        public SurveyController(ISurveyRepository SurveyRepository, ILogManager logger, IHttpContextAccessor accessor)
        {
            _SurveyRepository = SurveyRepository;
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
            return _SurveyRepository.GetSurveys(int.Parse(moduleid));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public Models.Survey Get(int id)
        {
            Models.Survey Survey = _SurveyRepository.GetSurvey(id);
            if (Survey != null && Survey.ModuleId != _entityId)
            {
                Survey = null;
            }
            return Survey;
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.Survey Post([FromBody] Models.Survey Survey)
        {
            if (ModelState.IsValid && Survey.ModuleId == _entityId)
            {
                Survey = _SurveyRepository.AddSurvey(Survey);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "Survey Added {Survey}", Survey);
            }
            return Survey;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.Survey Put(int id, [FromBody] Models.Survey Survey)
        {
            if (ModelState.IsValid && Survey.ModuleId == _entityId)
            {
                Survey = _SurveyRepository.UpdateSurvey(Survey);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "Survey Updated {Survey}", Survey);
            }
            return Survey;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public void Delete(int id)
        {
            Models.Survey Survey = _SurveyRepository.GetSurvey(id);
            if (Survey != null && Survey.ModuleId == _entityId)
            {
                _SurveyRepository.DeleteSurvey(id);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "Survey Deleted {SurveyId}", id);
            }
        }
    }
}
