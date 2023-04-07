using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiEquipment.Migrations
{
    /// <inheritdoc />
    public partial class V : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EquipmentHistories",
                keyColumn: "EquipmentHistoryId",
                keyValue: 1,
                column: "BorrowedDate",
                value: new DateTime(2023, 3, 27, 1, 45, 21, 316, DateTimeKind.Utc).AddTicks(8927));

            migrationBuilder.UpdateData(
                table: "EquipmentHistories",
                keyColumn: "EquipmentHistoryId",
                keyValue: 2,
                column: "BorrowedDate",
                value: new DateTime(2023, 3, 27, 1, 45, 21, 316, DateTimeKind.Utc).AddTicks(8928));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EquipmentHistories",
                keyColumn: "EquipmentHistoryId",
                keyValue: 1,
                column: "BorrowedDate",
                value: new DateTime(2023, 3, 27, 0, 19, 38, 929, DateTimeKind.Utc).AddTicks(919));

            migrationBuilder.UpdateData(
                table: "EquipmentHistories",
                keyColumn: "EquipmentHistoryId",
                keyValue: 2,
                column: "BorrowedDate",
                value: new DateTime(2023, 3, 27, 0, 19, 38, 929, DateTimeKind.Utc).AddTicks(920));
        }
    }
}
