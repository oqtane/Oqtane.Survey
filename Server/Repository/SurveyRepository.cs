using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;
using Oqtane.Survey.Models;
using System.Threading.Tasks;
using System;
using Oqtane.Survey.Server.Repository;

namespace Oqtane.Survey.Repository
{
    public class SurveyRepository : ISurveyRepository, IService
    {
        private readonly SurveyContext _db;

        public SurveyRepository(SurveyContext context)
        {
            _db = context;
        }

        // Survey

        #region public async Task<List<OqtaneSurvey>> GetAllSurveysAsync()
        public async Task<List<OqtaneSurvey>> GetAllSurveysAsync()
        {
            return await _db.OqtaneSurvey
                .Include(x => x.OqtaneSurveyItem)
                .ThenInclude(x => x.OqtaneSurveyItemOption)
                .AsNoTracking()
                .OrderBy(x => x.SurveyName)
                .ToListAsync();
        }
        #endregion

        #region public List<OqtaneSurvey> GetAllSurveysByModule(int ModuleId)
        public List<OqtaneSurvey> GetAllSurveysByModule(int ModuleId)
        {
            return _db.OqtaneSurvey
                .Where(x => x.ModuleId == ModuleId)
                .Include(x => x.OqtaneSurveyItem)
                .ThenInclude(x => x.OqtaneSurveyItemOption)
                .AsNoTracking()
                .OrderBy(x => x.SurveyName)
                .ToList();
        }
        #endregion

        #region public OqtaneSurvey GetSurvey(int Id)
        public OqtaneSurvey GetSurvey(int Id)
        {
            return _db.OqtaneSurvey
                .Include(x => x.OqtaneSurveyItem)
                .ThenInclude(x => x.OqtaneSurveyItemOption)
                .Where(x => x.SurveyId == Id)
                .FirstOrDefault();
        }
        #endregion

        #region public OqtaneSurvey CreateSurvey(Models.Survey NewSurvey)
        public OqtaneSurvey CreateSurvey(Models.Survey NewSurvey)
        {
            try
            {
                OqtaneSurvey objSurvey = new OqtaneSurvey();

                objSurvey.SurveyId = 0;
                objSurvey.ModuleId = NewSurvey.ModuleId;
                objSurvey.SurveyName = NewSurvey.SurveyName;
                objSurvey.UserId = NewSurvey.UserId;
                objSurvey.CreatedOn = DateTime.Now;

                _db.OqtaneSurvey.Add(objSurvey);
                _db.SaveChanges();

                return objSurvey;
            }
            catch
            {
                DetachAllEntities();
                throw;
            }
        }
        #endregion

        #region public Task<OqtaneSurvey> UpdateSurveyAsync(OqtaneSurvey objExistingSurvey)
        public Task<OqtaneSurvey> UpdateSurveyAsync(OqtaneSurvey objExistingSurvey)
        {
            try
            {
                var ExistingSurvey = _db.OqtaneSurvey
                                    .Where(x => x.SurveyId == objExistingSurvey.SurveyId)
                                    .FirstOrDefault();

                ExistingSurvey.SurveyName = objExistingSurvey.SurveyName;

                _db.SaveChanges();

                return Task.FromResult(ExistingSurvey);
            }
            catch
            {
                DetachAllEntities();
                throw;
            }
        }
        #endregion

        #region public Task<bool> DeleteSurveyAsync(OqtaneSurvey objExistingSurvey)
        public Task<bool> DeleteSurveyAsync(OqtaneSurvey objExistingSurvey)
        {
            var ExistingSurvey =
                _db.OqtaneSurvey
                .Where(x => x.SurveyId == objExistingSurvey.SurveyId)
                .FirstOrDefault();

            if (ExistingSurvey != null)
            {
                _db.OqtaneSurvey.Remove(ExistingSurvey);
                _db.SaveChanges();
            }
            else
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
        #endregion

        // Survey Item

        #region public async Task<List<OqtaneSurveyItem>> GetAllSurveyItemsAsync(int SurveyId)
        public async Task<List<OqtaneSurveyItem>> GetAllSurveyItemsAsync(int SurveyId)
        {
            return await _db.OqtaneSurveyItem
                .AsNoTracking()
                .Where(x => x.SurveyNavigation.SurveyId == SurveyId)
                .OrderBy(x => x.Id)
                .ToListAsync();
        }
        #endregion

        #region public Task<OqtaneSurveyItem> GetSurveyItemAsync(int SurveyItemId)
        public Task<OqtaneSurveyItem> GetSurveyItemAsync(int SurveyItemId)
        {
            return Task.FromResult(_db.OqtaneSurveyItem
                .Where(x => x.Id == SurveyItemId)
                .Include(x => x.OqtaneSurveyItemOption)
                .FirstOrDefault());
        }
        #endregion

        #region public Task<OqtaneSurveyItem> CreateSurveyItemAsync(OqtaneSurveyItem NewSurveyItem)
        public Task<OqtaneSurveyItem> CreateSurveyItemAsync(OqtaneSurveyItem NewSurveyItem)
        {
            try
            {
                OqtaneSurveyItem objSurveyItem = new OqtaneSurveyItem();

                objSurveyItem.OqtaneSurveyAnswer = new List<OqtaneSurveyAnswer>();

                objSurveyItem.SurveyNavigation =
                    _db.OqtaneSurvey
                    .Where(x => x.SurveyId == NewSurveyItem.SurveyNavigation.SurveyId)
                    .FirstOrDefault();

                objSurveyItem.Id = 0;
                objSurveyItem.ItemLabel = NewSurveyItem.ItemLabel;
                objSurveyItem.ItemType = NewSurveyItem.ItemType;
                objSurveyItem.ItemValue = NewSurveyItem.ItemValue;
                objSurveyItem.Required = NewSurveyItem.Required;
                objSurveyItem.Position = 0;

                if (NewSurveyItem.OqtaneSurveyItemOption != null)
                {
                    objSurveyItem.OqtaneSurveyItemOption = NewSurveyItem.OqtaneSurveyItemOption;
                }

                _db.OqtaneSurveyItem.Add(objSurveyItem);
                _db.SaveChanges();

                // Set position
                int CountOfSurveyItems =
                    _db.OqtaneSurveyItem
                    .Where(x => x.SurveyNavigation.SurveyId == NewSurveyItem.SurveyNavigation.SurveyId)
                    .Count();

                objSurveyItem.Position = CountOfSurveyItems;
                _db.SaveChanges();

                return Task.FromResult(objSurveyItem);
            }
            catch
            {
                DetachAllEntities();
                throw;
            }
        }
        #endregion

        #region public Task<OqtaneSurveyItem> UpdateSurveyItemAsync(OqtaneSurveyItem objExistingSurveyItem)
        public Task<OqtaneSurveyItem> UpdateSurveyItemAsync(OqtaneSurveyItem objExistingSurveyItem)
        {
            try
            {
                var ExistingSurveyItem = _db.OqtaneSurveyItem
                    .Where(x => x.Id == objExistingSurveyItem.Id)
                    .Include(x => x.OqtaneSurveyItemOption)
                    .FirstOrDefault();

                ExistingSurveyItem.ItemLabel = objExistingSurveyItem.ItemLabel;
                ExistingSurveyItem.ItemType = objExistingSurveyItem.ItemType;
                ExistingSurveyItem.ItemValue = objExistingSurveyItem.ItemValue;
                ExistingSurveyItem.Required = objExistingSurveyItem.Required;

                ExistingSurveyItem.OqtaneSurveyItemOption = objExistingSurveyItem.OqtaneSurveyItemOption;

                _db.SaveChanges();

                return Task.FromResult(ExistingSurveyItem);
            }
            catch
            {
                DetachAllEntities();
                throw;
            }
        }
        #endregion

        #region public Task<bool> DeleteSurveyItemAsync(SurveyItem objExistingSurveyItem)
        public Task<bool> DeleteSurveyItemAsync(SurveyItem objExistingSurveyItem)
        {
            var ExistingSurveyItem =
                _db.OqtaneSurveyItem
                .Where(x => x.Id == objExistingSurveyItem.Id)
                .FirstOrDefault();

            if (ExistingSurveyItem != null)
            {
                _db.OqtaneSurveyItem.Remove(ExistingSurveyItem);
                _db.SaveChanges();
            }
            else
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
        #endregion

        // Survey Answers

        #region public Task<bool> CreateSurveyAnswersAsync(Models.Survey paramDTOSurvey)
        public Task<bool> CreateSurveyAnswersAsync(Models.Survey paramDTOSurvey)
        {
            try
            {
                List<SurveyAnswer> SurveyAnswers = new List<SurveyAnswer>();

                foreach (var SurveyItem in paramDTOSurvey.SurveyItem)
                {
                    // Delete possible existing answer
                    var ExistingAnswers = _db.OqtaneSurveyAnswer
                        .Where(x => x.SurveyItemId == SurveyItem.Id)
                        .Where(x => x.UserId == paramDTOSurvey.UserId)
                        .ToList();

                    if (ExistingAnswers != null)
                    {
                        _db.OqtaneSurveyAnswer.RemoveRange(ExistingAnswers);
                        _db.SaveChanges();
                    }

                    // Save Answer

                    if (SurveyItem.ItemType != "Multi-Select Dropdown")
                    {
                        OqtaneSurveyAnswer NewSurveyAnswer = new OqtaneSurveyAnswer();

                        NewSurveyAnswer.AnswerValue = SurveyItem.AnswerValueString;

                        if (SurveyItem.AnswerValueDateTime != null)
                        {
                            NewSurveyAnswer.AnswerValueDateTime = Convert.ToDateTime(SurveyItem.AnswerValueDateTime);
                        }

                        NewSurveyAnswer.SurveyItemId = SurveyItem.Id;
                        NewSurveyAnswer.UserId = paramDTOSurvey.UserId;

                        _db.OqtaneSurveyAnswer.Add(NewSurveyAnswer);
                        _db.SaveChanges();
                    }

                    if (SurveyItem.AnswerValueList != null)
                    {
                        foreach (var item in SurveyItem.AnswerValueList)
                        {
                            OqtaneSurveyAnswer NewSurveyAnswerValueList = new OqtaneSurveyAnswer();

                            NewSurveyAnswerValueList.AnswerValue = item;
                            NewSurveyAnswerValueList.SurveyItemId = SurveyItem.Id;
                            NewSurveyAnswerValueList.UserId = paramDTOSurvey.UserId;

                            _db.OqtaneSurveyAnswer.Add(NewSurveyAnswerValueList);
                            _db.SaveChanges();
                        }
                    }
                }

                return Task.FromResult(true);
            }
            catch
            {
                DetachAllEntities();
                throw;
            }
        }
        #endregion

        // Utility       

        #region public async Task ExecuteSqlRaw(string sql)
        public async Task ExecuteSqlRaw(string sql)
        {
            await _db.Database.ExecuteSqlRawAsync(sql);
        }
        #endregion

        #region public void DetachAllEntities()
        public void DetachAllEntities()
        {
            var changedEntriesCopy = _db.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();
            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }
        #endregion
    }
}
