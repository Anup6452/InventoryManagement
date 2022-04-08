using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagement.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "EmployeeId",
                keyValue: "cd16316f-acc9-4dae-8298-c1ecfe6c7a35");

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "EmployeeId", "Email", "Password", "PersonId", "RoleId" },
                values: new object[] { "c18afdc8-3f47-4511-81c4-b25449dacf68", "admin@mail.com", "eH5+JMw6ifq1JvWBwkxYdt3sk0KOaT0jGiYLTS+kLmM=", "Person-01", "Role-01" });

            migrationBuilder.InsertData(
                table: "ListItem",
                columns: new[] { "ListItemId", "ListItemCategoryId", "ListItemName" },
                values: new object[] { "ListItem-02", "Cat-01", "Female" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "EmployeeId",
                keyValue: "c18afdc8-3f47-4511-81c4-b25449dacf68");

            migrationBuilder.DeleteData(
                table: "ListItem",
                keyColumn: "ListItemId",
                keyValue: "ListItem-02");

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "EmployeeId", "Email", "Password", "PersonId", "RoleId" },
                values: new object[] { "cd16316f-acc9-4dae-8298-c1ecfe6c7a35", "admin@mail.com", "admin", "Person-01", "Role-01" });
        }
    }
}
