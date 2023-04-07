using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiEquipment.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvatarUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsBan = table.Column<bool>(type: "bit", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentTypes",
                columns: table => new
                {
                    EquipmentTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentTypes", x => x.EquipmentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    EquipmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EquipmentTypeId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.EquipmentId);
                    table.ForeignKey(
                        name: "FK_Equipments_EquipmentTypes_EquipmentTypeId",
                        column: x => x.EquipmentTypeId,
                        principalTable: "EquipmentTypes",
                        principalColumn: "EquipmentTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeRoles",
                columns: table => new
                {
                    EmployeeRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRoles", x => x.EmployeeRoleId);
                    table.ForeignKey(
                        name: "FK_EmployeeRoles_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentHistories",
                columns: table => new
                {
                    EquipmentHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsAproved = table.Column<bool>(type: "bit", nullable: false),
                    BorrowedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    EquipmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentHistories", x => x.EquipmentHistoryId);
                    table.ForeignKey(
                        name: "FK_EquipmentHistories_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentHistories_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "EquipmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "AvatarUrl", "Birthdate", "Email", "IsBan", "Name", "Password", "RefreshToken", "RefreshTokenExpiryTime" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2001, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", false, "Hà Chánh Nam", "21232F297A57A5A743894A0E4A801FC3", null, null },
                    { 2, null, new DateTime(2001, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "hahaha@gmail.com", false, "Ha Ha Ha", "AB56B4D92B40713ACC5AF89985D4B786", null, null },
                    { 3, null, new DateTime(2001, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "namnamnam@gmail.com", false, "Nam Nam Nam", "AB56B4D92B40713ACC5AF89985D4B786", null, null },
                    { 4, null, new DateTime(2001, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "namchanhha1@gmail.com", false, "Nam Chanh Ha", "AB56B4D92B40713ACC5AF89985D4B786", null, null }
                });

            migrationBuilder.InsertData(
                table: "EquipmentTypes",
                columns: new[] { "EquipmentTypeId", "TypeName" },
                values: new object[,]
                {
                    { 1, "PC" },
                    { 2, "Laptop" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { 1, "User" },
                    { 2, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "EmployeeRoles",
                columns: new[] { "EmployeeRoleId", "EmployeeId", "RoleId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 2, 1 },
                    { 4, 3, 1 },
                    { 5, 4, 2 }
                });

            migrationBuilder.InsertData(
                table: "Equipments",
                columns: new[] { "EquipmentId", "Description", "EquipmentTypeId", "ImageUrl", "IsAvailable", "Name" },
                values: new object[,]
                {
                    { 1, "PC mới nhất hãng Asus", 1, null, true, "XAZCD" },
                    { 2, "Laptop mới nhất hãng HP", 2, null, false, "HPNEWXZ" },
                    { 3, "Laptop mới nhất hãng MSI", 2, null, true, "MSIQAX" },
                    { 4, "PC mới nhất hãng MSI", 1, null, true, "MSIQAX" },
                    { 5, "PC mới nhất hãng MSI", 1, null, true, "MSIQAX" }
                });

            migrationBuilder.InsertData(
                table: "EquipmentHistories",
                columns: new[] { "EquipmentHistoryId", "BorrowedDate", "EmployeeId", "EquipmentId", "IsAproved", "ReturnedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 3, 27, 0, 19, 38, 929, DateTimeKind.Utc).AddTicks(919), 1, 1, false, null },
                    { 2, new DateTime(2023, 3, 27, 0, 19, 38, 929, DateTimeKind.Utc).AddTicks(920), 3, 2, true, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRoles_EmployeeId",
                table: "EmployeeRoles",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRoles_RoleId",
                table: "EmployeeRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentHistories_EmployeeId",
                table: "EquipmentHistories",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentHistories_EquipmentId",
                table: "EquipmentHistories",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_EquipmentTypeId",
                table: "Equipments",
                column: "EquipmentTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeRoles");

            migrationBuilder.DropTable(
                name: "EquipmentHistories");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "EquipmentTypes");
        }
    }
}
