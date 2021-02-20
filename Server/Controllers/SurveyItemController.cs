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
    public class SurveyItemController : Controller
    {
        private readonly ISurveyRepository _SurveyRepository;
        private readonly IUserRepository _users;
        private readonly ILogManager _logger;
        protected int _entityId = -1;

        public SurveyItemController(ISurveyRepository SurveyRepository, IUserRepository users, ILogManager logger, IHttpContextAccessor accessor)
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
            if (ModelState.IsValid && SurveyItem.ModuleId == _entityId)
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
            if (ModelState.IsValid && SurveyItem.Id == _entityId)
            {
               // SurveyItem = ConvertToSurveyItem(_SurveyRepository.CreateSurveyItem(SurveyItem));
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "SurveyItem {SurveyItem} moved {MoveType}", SurveyItem);
            }
            return SurveyItem;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.SurveyItem Put(int id, [FromBody] Models.SurveyItem SurveyItem)
        {
            if (ModelState.IsValid && SurveyItem.Id == _entityId)
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

            if (SurveyItem != null && SurveyItem.Id == _entityId)
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
