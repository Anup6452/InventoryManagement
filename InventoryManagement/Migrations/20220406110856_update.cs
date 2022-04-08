using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagement.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "EmployeeId",
                keyValue: "1e3ae788-4ee2-417b-8bce-ef87cf438f9b");

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "EmployeeId", "Email", "Password", "PersonId", "RoleId" },
                values: new object[] { "a52e36ad-8c81-4694-8e84-913a20bd615e", "admin@mail.com", "SKuMvgo1qvkaCrAitMDDT+SrfOPqgJS7cNPrf9dAW8s=", "Person-01", "Role-01" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "EmployeeId",
                keyValue: "a52e36ad-8c81-4694-8e84-913a20bd615e");

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "EmployeeId", "Email", "Password", "PersonId", "RoleId" },
                values: new object[] { "1e3ae788-4ee2-417b-8bce-ef87cf438f9b", "admin@mail.com", "SKuMvgo1qvkaCrAitMDDT+SrfOPqgJS7cNPrf9dAW8s=", "Person-01", "Role-01" });
        }
    }
}
