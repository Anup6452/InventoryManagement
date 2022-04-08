using InventoryManagement.Entity;
using InventoryManagement.Services;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Data
{
    public class InventoryDBContext : DbContext
    {
        public InventoryDBContext(DbContextOptions<InventoryDBContext> options) : base(options) { }
        public DbSet<Person> Person { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<ListItem> ListItem { get; set; }
        public DbSet<ListItemCategory> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Seeding

            modelBuilder.Entity<Role>().HasData(new Role
            {
                RoleId = "Role-01",
                RoleName = "SuperAdmin",
            });

            modelBuilder.Entity<ListItem>().HasData(
                new ListItem{ListItemId = "ListItem-01",ListItemCategoryId = "Cat-01",ListItemName = "Male"},
                new ListItem{ListItemId = "ListItem-02",ListItemCategoryId = "Cat-01",ListItemName = "Female"}
                );

            modelBuilder.Entity<ListItemCategory>().HasData(
                new ListItemCategory{ ListItemCategoryId = "Cat-01", ListItemCategoryName = "Gender" });

            modelBuilder.Entity<Person>().HasData(new Person
            {
                PersonId = "Person-01",
                FirstName = "Admin",
                MiddleName = "",
                LastName = "",
                Address = "",
                ContactNo = "",
                GenderListItemId = "ListItem-01"
            });


            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                PersonId = "Person-01",
                EmployeeId = Guid.NewGuid().ToString(),
                Email = "admin@mail.com",
                Password = Hash.GetHash("admin"),
                RoleId = "Role-01"
            });

            
        }
    }
}
