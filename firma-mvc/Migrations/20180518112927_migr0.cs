using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace firmamvc.Migrations
{
    public partial class migr0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalValueInclVat",
                table: "Invoice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalValueInclVat",
                table: "Invoice",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
