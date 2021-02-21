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
using Oqtane.Survey.Server.Repository;
using System;

namespace Oqtane.Survey.Controllers
{
    [Route(ControllerRoutes.Default)]
    public class SurveyAnswersController : Controller
    {
        private readonly ISurveyRepository _SurveyRepository;
        private readonly IUserRepository _users;
        private readonly ILogManager _logger;
        protected int _entityId = -1;

        public SurveyAnswersController(ISurveyRepository SurveyRepository, IUserRepository users, ILogManager logger, IHttpContextAccessor accessor)
        {
            _SurveyRepository = SurveyRepository;
            _users = users;
            _logger = logger;

            if (accessor.HttpContext.Request.Query.ContainsKey("entityid"))
            {
                _entityId = int.Parse(accessor.HttpContext.Request.Query["entityid"]);
            }
        }       

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public void Post([FromBody] Models.Survey Survey)
        {
            if (ModelState.IsValid && Survey.ModuleId == _entityId)
            {
                // Get User
                var User = _users.GetUser(this.User.Identity.Name);

                // Add User to Survey object
                Survey.UserId = User.UserId;

                bool boolResult = _SurveyRepository.CreateSurveyAnswers(Survey);

                _logger.Log(LogLevel.Information, this, LogFunction.Create, "Survey Answers Added {Survey}", Survey);
            }
        }
    }
}
