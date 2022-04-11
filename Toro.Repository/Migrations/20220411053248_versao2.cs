using Microsoft.EntityFrameworkCore.Migrations;

namespace Toro.Repository.Migrations
{
    public partial class versao2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AssetXPatrimony",
                table: "AssetXPatrimony");

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Investor",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AssetXPatrimony",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssetXPatrimony",
                table: "AssetXPatrimony",
                columns: new[] { "PatrimonyId", "AssetId", "Id" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AssetXPatrimony",
                table: "AssetXPatrimony");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AssetXPatrimony");

            migrationBuilder.AlterColumn<int>(
                name: "Cpf",
                table: "Investor",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssetXPatrimony",
                table: "AssetXPatrimony",
                columns: new[] { "PatrimonyId", "AssetId" });
        }
    }
}
