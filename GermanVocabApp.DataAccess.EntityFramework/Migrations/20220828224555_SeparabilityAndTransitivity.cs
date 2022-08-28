using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GermanVocabApp.DataAccess.EntityFramework.Migrations
{
    public partial class SeparabilityAndTransitivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSeparable",
                table: "VocabListItem");

            migrationBuilder.DropColumn(
                name: "IsTransitive",
                table: "VocabListItem");

            migrationBuilder.AddColumn<string>(
                name: "Separability",
                table: "VocabListItem",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Transitivity",
                table: "VocabListItem",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "VocabList",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Separability",
                table: "VocabListItem");

            migrationBuilder.DropColumn(
                name: "Transitivity",
                table: "VocabListItem");

            migrationBuilder.AddColumn<bool>(
                name: "IsSeparable",
                table: "VocabListItem",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsTransitive",
                table: "VocabListItem",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "VocabList",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
