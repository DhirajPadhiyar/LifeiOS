using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LifeiOS.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTaskItemModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "TaskItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TaskItems",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "TaskItems");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TaskItems");
        }
    }
}
