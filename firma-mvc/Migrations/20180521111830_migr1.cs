using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace firmamvc.Migrations
{
    public partial class migr1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaxBookItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    BuysSideEffects = table.Column<decimal>(nullable: false),
                    Column15 = table.Column<decimal>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    CostDescription = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    GoodsBuys = table.Column<decimal>(nullable: false),
                    InvoiceNumber = table.Column<string>(nullable: true),
                    NIP = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: false),
                    OtherCosts = table.Column<decimal>(nullable: false),
                    OtherIncome = table.Column<decimal>(nullable: false),
                    ResearchCostValue = table.Column<decimal>(nullable: false),
                    Salary = table.Column<decimal>(nullable: false),
                    SellValue = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxBookItem", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxBookItem");
        }
    }
}
