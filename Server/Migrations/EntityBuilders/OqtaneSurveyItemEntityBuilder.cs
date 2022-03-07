using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace Oqtane.Survey.Migrations.EntityBuilders
{
    public class OqtaneSurveyItemEntityBuilder : AuditableBaseEntityBuilder<OqtaneSurveyItemEntityBuilder>
    {
        private const string _entityTableName = "OqtaneSurveyItem";
        private readonly PrimaryKey<OqtaneSurveyItemEntityBuilder> _primaryKey = new("PK_OqtaneSurveyItem", x => x.Id);
        private readonly ForeignKey<OqtaneSurveyItemEntityBuilder> _oqtaneqtaneSurveyItemForeignKey = new("FK_OqtaneSurveyItem_OqtaneSurvey", x => x.Survey, "OqtaneSurvey", "SurveyId", ReferentialAction.SetNull);

        public OqtaneSurveyItemEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_oqtaneqtaneSurveyItemForeignKey);
        }

        protected override OqtaneSurveyItemEntityBuilder BuildTable(ColumnsBuilder table)
        {
            Id = AddAutoIncrementColumn(table, "Id");
            Survey = AddIntegerColumn(table, "Survey");
            ItemLabel = AddMaxStringColumn(table, "ItemLabel");
            ItemType = AddMaxStringColumn(table, "ItemType");
            ItemValue = AddMaxStringColumn(table, "ItemValue");
            Position = AddIntegerColumn(table, "Position");
            Required = AddIntegerColumn(table, "Required");
            SurveyChoiceId = AddIntegerColumn(table, "SurveyChoiceId");
            return this;
        }

        public OperationBuilder<AddColumnOperation> Id { get; set; }
        public OperationBuilder<AddColumnOperation> Survey { get; set; }
        public OperationBuilder<AddColumnOperation> ItemLabel { get; set; }
        public OperationBuilder<AddColumnOperation> ItemType { get; set; }
        public OperationBuilder<AddColumnOperation> ItemValue { get; set; }
        public OperationBuilder<AddColumnOperation> Position { get; set; }
        public OperationBuilder<AddColumnOperation> Required { get; set; }
        public OperationBuilder<AddColumnOperation> SurveyChoiceId { get; set; }
    }
}
