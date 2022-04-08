using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagement.Migrations
{
    public partial class hashpassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "EmployeeId",
                keyValue: "c18afdc8-3f47-4511-81c4-b25449dacf68");

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "EmployeeId", "Email", "Password", "PersonId", "RoleId" },
                values: new object[] { "1e3ae788-4ee2-417b-8bce-ef87cf438f9b", "admin@mail.com", "SKuMvgo1qvkaCrAitMDDT+SrfOPqgJS7cNPrf9dAW8s=", "Person-01", "Role-01" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "EmployeeId",
                keyValue: "1e3ae788-4ee2-417b-8bce-ef87cf438f9b");

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "EmployeeId", "Email", "Password", "PersonId", "RoleId" },
                values: new object[] { "c18afdc8-3f47-4511-81c4-b25449dacf68", "admin@mail.com", "eH5+JMw6ifq1JvWBwkxYdt3sk0KOaT0jGiYLTS+kLmM=", "Person-01", "Role-01" });
        }
    }
}
