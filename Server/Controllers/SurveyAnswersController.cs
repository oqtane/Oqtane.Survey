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
using Radzen;
using Oqtane.Controllers;

namespace Oqtane.Survey.Controllers
{
    [Route(ControllerRoutes.ApiRoute)]
    public class SurveyAnswersController : ModuleControllerBase
    {
        private readonly ISurveyRepository _SurveyRepository;
        private readonly IUserRepository _users;

        public SurveyAnswersController(ISurveyRepository SurveyRepository, IUserRepository users, ILogManager logger, IHttpContextAccessor accessor) : base(logger, accessor)
        {
            _SurveyRepository = SurveyRepository;
            _users = users;
        }

        // POST api/<controller>/1
        [HttpPost("{SelectedSurveyId}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public List<Models.SurveyItem> Post(int SelectedSurveyId, [FromBody] LoadDataArgs args)
        {
            List<Models.SurveyItem> Response = new List<Models.SurveyItem>();

            Response = _SurveyRepository.SurveyResultsData(SelectedSurveyId, args);

            return Response;
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public void Post([FromBody] Models.Survey Survey)
        {
            if (ModelState.IsValid && Survey.ModuleId == _authEntityId[EntityNames.Module])
            {
                // Get User
                if (this.User.Identity.IsAuthenticated)
                {
                    var User = _users.GetUser(this.User.Identity.Name);

                    // Add User to Survey object
                    Survey.UserId = User.UserId;
                }
                else
                {
                    // The AnonymousCookie was passed by the Client
                    Survey.UserId = null;
                }

                bool boolResult = _SurveyRepository.CreateSurveyAnswers(Survey);

                _logger.Log(LogLevel.Information, this, LogFunction.Create, "Survey Answers Added {Survey}", Survey);
            }
        }

        // Utility

        #region private IEnumerable<Models.SurveyItem> ConvertToSurveyItems(List<OqtaneSurveyItem> colOqtaneSurveyItems)
        private IEnumerable<Models.SurveyItem> ConvertToSurveyItems(List<OqtaneSurveyItem> colOqtaneSurveyItems)
        {
            List<Models.SurveyItem> colSurveyItemCollection = new List<Models.SurveyItem>();

            foreach (var objOqtaneSurveyItem in colOqtaneSurveyItems)
            {
                // Convert to SurveyItem
                Models.SurveyItem objAddSurveyItem = ConvertToSurveyItem(objOqtaneSurveyItem);

                // Add to Collection
                colSurveyItemCollection.Add(objAddSurveyItem);
            }

            return colSurveyItemCollection;
        }
        #endregion

        #region private Models.SurveyItem ConvertToSurveyItem(OqtaneSurveyItem objOqtaneSurveyItem)
        private Models.SurveyItem ConvertToSurveyItem(OqtaneSurveyItem objOqtaneSurveyItem)
        {
            if (objOqtaneSurveyItem == null)
            {
                return new Models.SurveyItem();
            }

            // Create new Object
            Models.SurveyItem objSurveyItem = new SurveyItem();

            objSurveyItem.Id = objOqtaneSurveyItem.Id;
            objSurveyItem.ItemLabel = objOqtaneSurveyItem.ItemLabel;
            objSurveyItem.ItemType = objOqtaneSurveyItem.ItemType;
            objSurveyItem.ItemValue = objOqtaneSurveyItem.ItemValue;
            objSurveyItem.Position = objOqtaneSurveyItem.Position;
            objSurveyItem.Required = objOqtaneSurveyItem.Required;
            objSurveyItem.SurveyChoiceId = objOqtaneSurveyItem.SurveyChoiceId;

            // Create new Collection
            objSurveyItem.SurveyItemOption = new List<SurveyItemOption>();

            foreach (OqtaneSurveyItemOption objOqtaneSurveyItemOption in objOqtaneSurveyItem.OqtaneSurveyItemOption)
            {
                // Create new Object
                Models.SurveyItemOption objAddSurveyItemOption = new SurveyItemOption();

                objAddSurveyItemOption.Id = objOqtaneSurveyItemOption.Id;
                objAddSurveyItemOption.OptionLabel = objOqtaneSurveyItemOption.OptionLabel;

                // Add to Collection
                objSurveyItem.SurveyItemOption.Add(objAddSurveyItemOption);
            }

            return objSurveyItem;
        }
        #endregion
    }
}
