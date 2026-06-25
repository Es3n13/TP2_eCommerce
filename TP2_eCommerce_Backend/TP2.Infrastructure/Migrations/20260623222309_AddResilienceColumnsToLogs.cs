using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP2.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddResilienceColumnsToLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "CanadaIntegrationLogs");

            migrationBuilder.RenameColumn(
                name: "AttemptTime",
                table: "CanadaIntegrationLogs",
                newName: "AttemptDate");

            migrationBuilder.RenameColumn(
                name: "AttemptNumber",
                table: "CanadaIntegrationLogs",
                newName: "RetryCount");

            migrationBuilder.AlterColumn<string>(
                name: "ErrorMessage",
                table: "CanadaIntegrationLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSuccessful",
                table: "CanadaIntegrationLogs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSuccessful",
                table: "CanadaIntegrationLogs");

            migrationBuilder.RenameColumn(
                name: "RetryCount",
                table: "CanadaIntegrationLogs",
                newName: "AttemptNumber");

            migrationBuilder.RenameColumn(
                name: "AttemptDate",
                table: "CanadaIntegrationLogs",
                newName: "AttemptTime");

            migrationBuilder.AlterColumn<string>(
                name: "ErrorMessage",
                table: "CanadaIntegrationLogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "CanadaIntegrationLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
