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
using System.Linq;
using Oqtane.Controllers;

namespace Oqtane.Survey.Controllers
{
    [Route(ControllerRoutes.ApiRoute)]
    public class SurveyItemController : ModuleControllerBase
    {
        private readonly ISurveyRepository _SurveyRepository;
        private readonly IUserRepository _users;

        public SurveyItemController(ISurveyRepository SurveyRepository, IUserRepository users, ILogManager logger, IHttpContextAccessor accessor) : base(logger, accessor)
        {
            _SurveyRepository = SurveyRepository;
            _users = users;
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public IEnumerable<Models.SurveyItem> Get(string moduleid)
        {
            var colSurveyItems = _SurveyRepository.GetAllSurveyItems(int.Parse(moduleid));
            return ConvertToSurveyItems(colSurveyItems);
        }

        // GET api/<controller>?/5
        [HttpGet("{id}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public Models.SurveyItem Get(int id)
        {
            var objSurvey = _SurveyRepository.GetSurveyItem(id);

            Models.SurveyItem SurveyItem = ConvertToSurveyItem(objSurvey);

            return SurveyItem;
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.SurveyItem Post([FromBody] Models.SurveyItem SurveyItem)
        {
            if (ModelState.IsValid && SurveyItem.ModuleId == _authEntityId[EntityNames.Module])
            {
                SurveyItem = ConvertToSurveyItem(_SurveyRepository.CreateSurveyItem(SurveyItem));
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "SurveyItem Added {SurveyItem}", SurveyItem);
            }
            return SurveyItem;
        }

        // POST api/<controller>/Down
        [HttpPost("{MoveType}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.SurveyItem Post(string MoveType, [FromBody] Models.SurveyItem SurveyItem)
        {
            if (ModelState.IsValid && SurveyItem.ModuleId == _authEntityId[EntityNames.Module])
            {
                // Get the Survey (and all SurveyItems)
                var objSurvey = _SurveyRepository.GetSurvey(SurveyItem.ModuleId);

                if (MoveType == "Up")
                {
                    // Move Up
                    int DesiredPosition = (SurveyItem.Position - 1);

                    // Move the current element in that position
                    var CurrentSurveyItem =
                            objSurvey.OqtaneSurveyItem
                            .Where(x => x.Position == DesiredPosition)
                            .FirstOrDefault();

                    if (CurrentSurveyItem != null)
                    {
                        // Move it down
                        CurrentSurveyItem.Position = CurrentSurveyItem.Position + 1;

                        // Update it
                        _SurveyRepository.UpdateSurveyItem(ConvertToSurveyItem(CurrentSurveyItem));
                    }

                    // Move Item Up
                    var SurveyItemToMoveUp =
                         objSurvey.OqtaneSurveyItem
                            .Where(x => x.Id == SurveyItem.Id)
                            .FirstOrDefault();

                    if (SurveyItemToMoveUp != null)
                    {
                        // Move it up
                        SurveyItemToMoveUp.Position = SurveyItemToMoveUp.Position - 1;

                        // Update it
                        _SurveyRepository.UpdateSurveyItem(ConvertToSurveyItem(SurveyItemToMoveUp));
                    }
                }
                else
                {
                    // Move Down

                    int DesiredPosition = (SurveyItem.Position + 1);

                    // Move the current element in that position
                    var CurrentSurveyItem =
                            objSurvey.OqtaneSurveyItem
                            .Where(x => x.Position == DesiredPosition)
                            .FirstOrDefault();

                    if (CurrentSurveyItem != null)
                    {
                        // Move it down
                        CurrentSurveyItem.Position = CurrentSurveyItem.Position - 1;

                        // Update it
                        _SurveyRepository.UpdateSurveyItem(ConvertToSurveyItem(CurrentSurveyItem));
                    }

                    // Move Item Up
                    var SurveyItemToMoveUp =
                         objSurvey.OqtaneSurveyItem
                            .Where(x => x.Id == SurveyItem.Id)
                            .FirstOrDefault();

                    if (SurveyItemToMoveUp != null)
                    {
                        // Move it up
                        SurveyItemToMoveUp.Position = SurveyItemToMoveUp.Position + 1;

                        // Update it
                        _SurveyRepository.UpdateSurveyItem(ConvertToSurveyItem(SurveyItemToMoveUp));
                    }

                }

                _logger.Log(LogLevel.Information, this, LogFunction.Create, "SurveyItem {SurveyItem} moved", SurveyItem);
            }
            return SurveyItem;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.SurveyItem Put(int id, [FromBody] Models.SurveyItem SurveyItem)
        {
            if (ModelState.IsValid && SurveyItem.ModuleId == _authEntityId[EntityNames.Module])
            {
                SurveyItem = ConvertToSurveyItem(_SurveyRepository.UpdateSurveyItem(SurveyItem));
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "SurveyItem Updated {SurveyItem}", SurveyItem);
            }
            return SurveyItem;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public void Delete(int id)
        {
            var objSurveyItem = _SurveyRepository.GetSurveyItem(id);

            Models.SurveyItem SurveyItem = ConvertToSurveyItem(objSurveyItem);

            if (SurveyItem != null && SurveyItem.ModuleId == _authEntityId[EntityNames.Module])
            {
                bool boolResult = _SurveyRepository.DeleteSurveyItem(id);
                if (boolResult)
                {
                    _logger.Log(LogLevel.Information, this, LogFunction.Delete, "SurveyItem Deleted {Id}", id);
                }
                else
                {
                    _logger.Log(LogLevel.Information, this, LogFunction.Delete, "Error: SurveyItem *NOT* Deleted {Id}", id);
                }
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
