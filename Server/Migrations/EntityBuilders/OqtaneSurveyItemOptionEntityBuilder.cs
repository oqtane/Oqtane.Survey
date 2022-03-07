using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace Oqtane.Survey.Migrations.EntityBuilders
{
    public class OqtaneSurveyItemOptionEntityBuilder : AuditableBaseEntityBuilder<OqtaneSurveyItemOptionEntityBuilder>
    {
        private const string _entityTableName = "OqtaneSurveyItemOption";
        private readonly PrimaryKey<OqtaneSurveyItemOptionEntityBuilder> _primaryKey = new("PK_OqtaneSurveyItemOption", x => x.Id);
        private readonly ForeignKey<OqtaneSurveyItemOptionEntityBuilder> _oqtaneSurveyItemOptionForeignKey = new("FK_OqtaneSurveyItemOption_SurveyItem", x => x.SurveyItem, "OqtaneSurveyItem", "Id", ReferentialAction.Cascade);

        public OqtaneSurveyItemOptionEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_oqtaneSurveyItemOptionForeignKey);
        }

        protected override OqtaneSurveyItemOptionEntityBuilder BuildTable(ColumnsBuilder table)
        {
            Id = AddAutoIncrementColumn(table, "Id");
            SurveyItem = AddIntegerColumn(table, "SurveyItem");
            OptionLabel = AddMaxStringColumn(table, "OptionLabel");
            return this;
        }

        public OperationBuilder<AddColumnOperation> Id { get; set; }
        public OperationBuilder<AddColumnOperation> SurveyItem { get; set; }
        public OperationBuilder<AddColumnOperation> OptionLabel { get; set; }
    }
}
