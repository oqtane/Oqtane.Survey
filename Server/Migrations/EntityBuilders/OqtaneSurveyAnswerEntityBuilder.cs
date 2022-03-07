using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace Oqtane.Survey.Migrations.EntityBuilders
{
    public class OqtaneSurveyAnswerEntityBuilder : AuditableBaseEntityBuilder<OqtaneSurveyAnswerEntityBuilder>
    {
        private const string _entityTableName = "OqtaneSurveyAnswer";
        private readonly PrimaryKey<OqtaneSurveyAnswerEntityBuilder> _primaryKey = new("PK_OqtaneSurveyAnswer", x => x.Id);
        private readonly ForeignKey<OqtaneSurveyAnswerEntityBuilder> _oqtaneSurveyForeignKey = new("FK_OqtaneSurveyAnswer_SurveyItem", x => x.SurveyItemId, "SurveyItem", "SurveyItemId", ReferentialAction.Cascade);

        public OqtaneSurveyAnswerEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_oqtaneSurveyForeignKey);
        }

        protected override OqtaneSurveyAnswerEntityBuilder BuildTable(ColumnsBuilder table)
        {
            Id = AddAutoIncrementColumn(table, "Id");
            SurveyItemId = AddIntegerColumn(table, "SurveyItemId");
            AnswerValue = AddMaxStringColumn(table, "AnswerValue");
            AnswerValueDateTime = AddDateTimeColumn(table, "AnswerValueDateTime");
            UserId = AddIntegerColumn(table, "UserId");
            return this;
        }

        public OperationBuilder<AddColumnOperation> Id { get; set; }
        public OperationBuilder<AddColumnOperation> SurveyItemId { get; set; }
        public OperationBuilder<AddColumnOperation> AnswerValue { get; set; }
        public OperationBuilder<AddColumnOperation> AnswerValueDateTime { get; set; }
        public OperationBuilder<AddColumnOperation> UserId { get; set; }
    }
}
