using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyEmployee.Migrations
{
    public partial class RolesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b93fca86-6363-4247-a9ed-d573e7d3787e", "02bef14d-1504-4323-bdaa-a58e1f30085b", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f23ac82a-78da-4e82-920b-b554030d713f", "35da0e2b-c6c5-4df6-8d54-2160c7b42d26", "Manager", "MANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b93fca86-6363-4247-a9ed-d573e7d3787e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f23ac82a-78da-4e82-920b-b554030d713f");
        }
    }
}
