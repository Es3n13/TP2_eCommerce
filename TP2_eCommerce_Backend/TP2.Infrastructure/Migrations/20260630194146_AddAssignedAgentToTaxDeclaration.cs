using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP2.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAssignedAgentToTaxDeclaration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReviewedBy",
                table: "TaxDeclarations",
                newName: "AssignedAgentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AssignedAgentId",
                table: "TaxDeclarations",
                newName: "ReviewedBy");
        }
    }
}
