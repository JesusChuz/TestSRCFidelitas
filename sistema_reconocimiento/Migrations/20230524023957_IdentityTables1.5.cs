using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sistema_reconocimiento.Migrations
{
    public partial class IdentityTables15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

        }
    }
}
