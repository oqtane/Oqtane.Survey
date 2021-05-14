using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace Oqtane.Survey.Models
{
    public class Survey
    {
        public int SurveyId { get; set; }
        public int ModuleId { get; set; }
        public string SurveyName { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int? UserId { get; set; }
        public List<SurveyItem> SurveyItem { get; set; }
    }
    public class SurveyItem
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public string ItemLabel { get; set; }
        public string ItemType { get; set; }
        public string ItemValue { get; set; }
        public int Position { get; set; }
        public int Required { get; set; }
        public int? SurveyChoiceId { get; set; }
        public string AnswerValueString { get; set; }
        public IEnumerable<string> AnswerValueList { get; set; }
        public DateTime? AnswerValueDateTime { get; set; }
        public List<SurveyItemOption> SurveyItemOption { get; set; }
        public List<AnswerResponse> AnswerResponses { get; set; }
    }
    public partial class SurveyItemOption
    {
        public int Id { get; set; }
        public string OptionLabel { get; set; }
    }
    public class AnswerResponse
    {
        public string OptionLabel { get; set; }
        public double Responses { get; set; }
    }

    public class SurveyAnswer
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public int SurveyItemId { get; set; }
        public string AnswerValue { get; set; }
        public DateTime AnswerValueDateTime { get; set; }
        public int? UserId { get; set; }
    }
}
