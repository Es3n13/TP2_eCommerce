using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP2.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNoticeOfAssessmentSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalAmount",
                table: "NoticesOfAssessment");

            migrationBuilder.DropColumn(
                name: "IsAutomated",
                table: "NoticesOfAssessment");

            migrationBuilder.AddColumn<string>(
                name: "DecisionResult",
                table: "NoticesOfAssessment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "NoticesOfAssessment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DecisionResult",
                table: "NoticesOfAssessment");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "NoticesOfAssessment");

            migrationBuilder.AddColumn<decimal>(
                name: "FinalAmount",
                table: "NoticesOfAssessment",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsAutomated",
                table: "NoticesOfAssessment",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
