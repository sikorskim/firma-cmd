using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace firmamvc.Migrations
{
    public partial class migr2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Contractor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Contractor",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Contractor");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Contractor");
        }
    }
}
