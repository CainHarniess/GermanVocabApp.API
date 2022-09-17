using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GermanVocabApp.DataAccess.EntityFramework.Migrations
{
    public partial class VocabListItem_FixedPluralityAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FixedPlurality",
                table: "VocabListItem",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FixedPlurality",
                table: "VocabListItem");
        }
    }
}
