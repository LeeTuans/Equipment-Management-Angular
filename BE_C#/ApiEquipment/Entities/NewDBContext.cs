using ApiEquipment.Entities.Data;
using ApiEquipment.Entities.DataSeed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace ApiEquipment.Entities
{
    public class NewDBContext : DbContext
    {
        public NewDBContext(DbContextOptions otps) : base(otps) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<EmployeeRole> EmployeeRoles { get; set; }

        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }

        public DbSet<EquipmentHistory> EquipmentHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleSeed());
            modelBuilder.ApplyConfiguration(new EmployeeSeed());
            modelBuilder.ApplyConfiguration(new EmployeeRoleSeed());
            modelBuilder.ApplyConfiguration(new EquipmentTypeSeed());
            modelBuilder.ApplyConfiguration(new EquipmentSeed());
            modelBuilder.ApplyConfiguration(new EquipmentHistorySeed());

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Email)
                .IsUnique();
            //modelBuilder.Entity<Employee>()
            //    .Property(e => e.Birthdate)
            //    .HasColumnType("date");
            // modelBuilder.Entity<Role>().HasData(
            //     new Role
            //     {
            //         RoleId = 1,
            //         RoleName = "User",

            //     },
            //     new Role
            //     {
            //         RoleId = 2,
            //         RoleName = "Admin",

            //     }
            // );
            // modelBuilder.Entity<Employee>().HasData(
            //     new Employee
            //     {
            //         EmployeeId = 1,
            //         Name = "Hà Chánh Nam",
            //         Email = "hachanhnam10@gmail.com",
            //         Password = "abcde",
            //         Birthdate = new DateTime(2001, 10, 19)
            //     },
            //     new Employee
            //     {
            //         EmployeeId = 2,
            //         Name = "Ha Ha Ha",
            //         Email = "hahaha@gmail.com",
            //         Password = "abcde",
            //         Birthdate = new DateTime(2001, 10, 19)
            //     },
            //     new Employee
            //     {
            //         EmployeeId = 3,
            //         Name = "Nam Nam Nam",
            //         Email = "namnamnam@gmail.com",
            //         Password = "abcde",
            //         Birthdate = new DateTime(2001, 10, 19)

            //     },
            //     new Employee
            //     {
            //         EmployeeId = 4,
            //         Name = "Nam Chanh Ha",
            //         Email = "namchanhha1@gmail.com",
            //         Password = "abcde",
            //         Birthdate = new DateTime(2001, 10, 19),
            //     }
            // );
            // modelBuilder.Entity<EmployeeRole>().HasData(
            //     new EmployeeRole
            //     {
            //         EmployeeRoleId = 1,
            //         EmployeeId = 1,
            //         RoleId = 1,
            //     },
            //     new EmployeeRole
            //     {
            //         EmployeeRoleId = 2,
            //         EmployeeId = 1,
            //         RoleId = 2,
            //     },
            //     new EmployeeRole
            //     {
            //         EmployeeRoleId = 3,
            //         EmployeeId = 2,
            //         RoleId = 1,
            //     },
            //     new EmployeeRole
            //     {
            //         EmployeeRoleId = 4,
            //         EmployeeId = 3,
            //         RoleId = 1,
            //     },
            //     new EmployeeRole
            //     {
            //         EmployeeRoleId = 5,
            //         EmployeeId = 4,
            //         RoleId = 2,
            //     }
            // );
            // modelBuilder.Entity<StatusType>().HasData(
            //     new StatusType
            //     {
            //         EquipmentStatusId = 1,
            //         StatusName = "Available",

            //     },
            //     new StatusType
            //     {
            //         EquipmentStatusId = 2,
            //         StatusName = "Borrowed",
            //     },
            //     new StatusType
            //     {
            //         EquipmentStatusId = 3,
            //         StatusName = "Maintenance",
            //     },
            //     new StatusType
            //     {
            //         EquipmentStatusId = 4,
            //         StatusName = "Lost",
            //     }
            // );
            // modelBuilder.Entity<EquipmentType>().HasData(
            //    new EquipmentType
            //    {
            //        EquipmentTypeId = 1,
            //        TypeName = "PC",
            //    },
            //    new EquipmentType
            //    {
            //        EquipmentTypeId = 2,
            //        TypeName = "Laptop",
            //    }
            //    );
            //modelBuilder.Entity<Equipment>().HasData(
            //     new Equipment
            //     {
            //         EquipmentId = 1,
            //         Name = "XAZCD",
            //         Description = "PC mới nhất hãng Asus",
            //         StatusTypeID = 1,
            //         EquipmentTypeId = 1,
            //     },
            //     new Equipment
            //     {
            //         EquipmentId = 2,
            //         Name = "HPNEWXZ",
            //         Description = "Laptop mới nhất hãng HP",
            //         StatusTypeID = 2,
            //         EquipmentTypeId = 2,

            //     },
            //     new Equipment
            //     {
            //         EquipmentId = 3,
            //         Name = "MSIQAX",
            //         Description = "Laptop mới nhất hãng MSI",
            //         StatusTypeID = 3,
            //         EquipmentTypeId = 2,

            //     },
            //     new Equipment
            //     {
            //         EquipmentId = 4,
            //         Name = "MSIQAX",
            //         Description = "PC mới nhất hãng MSI",
            //         StatusTypeID = 1,
            //         EquipmentTypeId = 1,

            //     },
            //     new Equipment
            //     {
            //         EquipmentId = 5,
            //         Name = "MSIQAX",
            //         Description = "PC mới nhất hãng MSI",
            //         StatusTypeID = 4,
            //         EquipmentTypeId = 1,

            //     }
            // );
            // modelBuilder.Entity<EquipmentHistory>().HasData(
            //     new EquipmentHistory
            //     {
            //         EquipmentHistoryId = 1,
            //         EmployeeId = 1,
            //         EquipmentId = 1,
            //     },
            //     new EquipmentHistory
            //     {
            //         EquipmentHistoryId = 2,
            //         EmployeeId = 3,
            //         EquipmentId = 2,
            //         IsAproved = true,
            //     }

            // );
        }
    }
}
