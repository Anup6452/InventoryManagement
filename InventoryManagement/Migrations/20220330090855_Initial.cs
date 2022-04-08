using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagement.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    ListItemCategoryId = table.Column<string>(type: "text", nullable: false),
                    ListItemCategoryName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.ListItemCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    RoleName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "ListItem",
                columns: table => new
                {
                    ListItemId = table.Column<string>(type: "text", nullable: false),
                    ListItemCategoryId = table.Column<string>(type: "text", nullable: false),
                    ListItemName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListItem", x => x.ListItemId);
                    table.ForeignKey(
                        name: "FK_ListItem_Category_ListItemCategoryId",
                        column: x => x.ListItemCategoryId,
                        principalTable: "Category",
                        principalColumn: "ListItemCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    PersonId = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    MiddleName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    ContactNo = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    GenderListItemId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_Person_ListItem_GenderListItemId",
                        column: x => x.GenderListItemId,
                        principalTable: "ListItem",
                        principalColumn: "ListItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(type: "text", nullable: false),
                    PersonId = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employee_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employee_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "ListItemCategoryId", "ListItemCategoryName" },
                values: new object[] { "Cat-01", "Gender" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[] { "Role-01", "SuperAdmin" });

            migrationBuilder.InsertData(
                table: "ListItem",
                columns: new[] { "ListItemId", "ListItemCategoryId", "ListItemName" },
                values: new object[] { "ListItem-01", "Cat-01", "Male" });

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "PersonId", "Address", "ContactNo", "FirstName", "GenderListItemId", "LastName", "MiddleName" },
                values: new object[] { "Person-01", "", "", "Admin", "ListItem-01", "", "" });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "EmployeeId", "Email", "Password", "PersonId", "RoleId" },
                values: new object[] { "cd16316f-acc9-4dae-8298-c1ecfe6c7a35", "admin@mail.com", "admin", "Person-01", "Role-01" });

            migrationBuilder.CreateIndex(
                name: "IX_Category_ListItemCategoryId",
                table: "Category",
                column: "ListItemCategoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_ListItemCategoryName",
                table: "Category",
                column: "ListItemCategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Email",
                table: "Employee",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_PersonId",
                table: "Employee",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_RoleId",
                table: "Employee",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ListItem_ListItemCategoryId",
                table: "ListItem",
                column: "ListItemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ListItem_ListItemId",
                table: "ListItem",
                column: "ListItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ListItem_ListItemName",
                table: "ListItem",
                column: "ListItemName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_ContactNo",
                table: "Person",
                column: "ContactNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_GenderListItemId",
                table: "Person",
                column: "GenderListItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_RoleName",
                table: "Role",
                column: "RoleName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "ListItem");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
