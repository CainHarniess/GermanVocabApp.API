using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GermanVocabApp.DataAccess.EntityFramework.Migrations
{
    public partial class VocabListItemProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Term",
                table: "VocabListItem");

            migrationBuilder.DropColumn(
                name: "Translation",
                table: "VocabListItem");

            migrationBuilder.AlterColumn<string>(
                name: "WordType",
                table: "VocabListItem",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "AuxiliaryVerb",
                table: "VocabListItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comparative",
                table: "VocabListItem",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "English",
                table: "VocabListItem",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "VocabListItem",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "German",
                table: "VocabListItem",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.AddColumn<bool>(
                name: "IsWeakMasculineNoun",
                table: "VocabListItem",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Perfect",
                table: "VocabListItem",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Plural",
                table: "VocabListItem",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Preposition",
                table: "VocabListItem",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrepositionCase",
                table: "VocabListItem",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReflexiveCase",
                table: "VocabListItem",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Superlative",
                table: "VocabListItem",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThirdPersonImperfect",
                table: "VocabListItem",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThirdPersonPresent",
                table: "VocabListItem",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "VocabList",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuxiliaryVerb",
                table: "VocabListItem");

            migrationBuilder.DropColumn(
                name: "Comparative",
                table: "VocabListItem");

            migrationBuilder.DropColumn(
                name: "English",
                table: "VocabListItem");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "VocabListItem");

            migrationBuilder.DropColumn(
                name: "German",
                table: "VocabListItem");

            migrationBuilder.DropColumn(
                name: "IsSeparable",
                table: "VocabListItem");

            migrationBuilder.DropColumn(
                name: "IsTransitive",
                table: "VocabListItem");

            migrationBuilder.DropColumn(
                name: "IsWeakMasculineNoun",
                table: "VocabListItem");

            migrationBuilder.DropColumn(
                name: "Perfect",
                table: "VocabListItem");

            migrationBuilder.DropColumn(
                name: "Plural",
                table: "VocabListItem");

            migrationBuilder.DropColumn(
                name: "Preposition",
                table: "VocabListItem");

            migrationBuilder.DropColumn(
                name: "PrepositionCase",
                table: "VocabListItem");

            migrationBuilder.DropColumn(
                name: "ReflexiveCase",
                table: "VocabListItem");

            migrationBuilder.DropColumn(
                name: "Superlative",
                table: "VocabListItem");

            migrationBuilder.DropColumn(
                name: "ThirdPersonImperfect",
                table: "VocabListItem");

            migrationBuilder.DropColumn(
                name: "ThirdPersonPresent",
                table: "VocabListItem");

            migrationBuilder.AlterColumn<string>(
                name: "WordType",
                table: "VocabListItem",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<string>(
                name: "Term",
                table: "VocabListItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Translation",
                table: "VocabListItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "VocabList",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);
        }
    }
}
