using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Survey.Repository;
using Oqtane.Survey.Migrations.EntityBuilders;

namespace Oqtane.Survey.Migrations.Migrations
{
    [DbContext(typeof(SurveyContext))]
    [Migration("Survey.01.00.00.00")]
    public class InitializeModule : MultiDatabaseMigration
    {
        public InitializeModule(IDatabase database) : base(database)
        {
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var oqtaneSurveyBuilder = new OqtaneSurveyEntityBuilder(migrationBuilder, ActiveDatabase);
            oqtaneSurveyBuilder.Create();

            var oqtaneSurveyItemBuilder = new OqtaneSurveyItemEntityBuilder(migrationBuilder, ActiveDatabase);
            oqtaneSurveyItemBuilder.Create();

            var oqtaneSurveyAnswerBuilder = new OqtaneSurveyAnswerEntityBuilder(migrationBuilder, ActiveDatabase);
            oqtaneSurveyAnswerBuilder.Create();

            var oqtaneSurveyItemOptionBuilder = new OqtaneSurveyItemOptionEntityBuilder(migrationBuilder, ActiveDatabase);
            oqtaneSurveyItemOptionBuilder.Create();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var oqtaneSurveyItemOptionBuilder = new OqtaneSurveyItemOptionEntityBuilder(migrationBuilder, ActiveDatabase);
            oqtaneSurveyItemOptionBuilder.Drop();

            var oqtaneSurveyAnswerBuilder = new OqtaneSurveyAnswerEntityBuilder(migrationBuilder, ActiveDatabase);
            oqtaneSurveyAnswerBuilder.Drop();

            var oqtaneSurveyItemBuilder = new OqtaneSurveyItemEntityBuilder(migrationBuilder, ActiveDatabase);
            oqtaneSurveyItemBuilder.Drop();

            var oqtaneSurveyBuilder = new OqtaneSurveyEntityBuilder(migrationBuilder, ActiveDatabase);
            oqtaneSurveyBuilder.Drop();
        }
    }
}
