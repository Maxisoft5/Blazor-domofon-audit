using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class HomeNumberDataTypeUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "HomeNumber",
                table: "AddressInfos",
                type: "nvarchar(max)",
                maxLength: 99999,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 99999);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "HomeNumber",
                table: "AddressInfos",
                type: "int",
                maxLength: 99999,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 99999);
        }
    }
}
