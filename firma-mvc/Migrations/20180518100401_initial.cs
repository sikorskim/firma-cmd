using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace firmamvc.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalValue",
                table: "Invoice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalValue",
                table: "Invoice",
                nullable: false,
                defaultValue: 0m);
        }
    }
}