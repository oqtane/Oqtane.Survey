using System;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace Oqtane.Survey.Models
{
    [Table("OqtaneSurvey")]
    public class Survey : IAuditable
    {
        public int SurveyId { get; set; }
        public int UserId { get; set; }
        public int ModuleId { get; set; }
        public string SurveyName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    [Table("OqtaneSurveyAnswer")]
    public class SurveyAnswer
    {
        public int Id { get; set; }
        public int SurveyItemId { get; set; }
        public string AnswerValue { get; set; }
        public DateTime AnswerValueDateTime { get; set; }
        public int UserId { get; set; }
    }

    [Table("OqtaneSurveyItem")]
    public class SurveyItem
    {
        public int SurveyId { get; set; }
        public int Id { get; set; }
        public int Survey { get; set; }
        public string ItemLabel { get; set; }
        public string ItemType { get; set; }
        public string ItemValue { get; set; }
        public int Position { get; set; }
        public int Required { get; set; }
        public int SurveyChoiceId { get; set; }
    }

    [Table("OqtaneSurveyItemOption")]
    public class SurveyItemOption
    {
        public int Id { get; set; }
        public int SurveyItem { get; set; }
        public string OptionLabel { get; set; }
    }
}
