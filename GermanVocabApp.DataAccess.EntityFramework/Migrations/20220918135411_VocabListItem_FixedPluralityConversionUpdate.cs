using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GermanVocabApp.DataAccess.EntityFramework.Migrations
{
    public partial class VocabListItem_FixedPluralityConversionUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FixedPlurality",
                table: "VocabListItem",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FixedPlurality",
                table: "VocabListItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);
        }
    }
}
