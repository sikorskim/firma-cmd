using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace firmamvc.Migrations
{
    public partial class migr2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_InvoiceHeader_InvoiceHeaderId",
                table: "Invoice");

            migrationBuilder.DropTable(
                name: "InvoiceHeader");

            migrationBuilder.RenameColumn(
                name: "InvoiceHeaderId",
                table: "Invoice",
                newName: "PaymentMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_Invoice_InvoiceHeaderId",
                table: "Invoice",
                newName: "IX_Invoice_PaymentMethodId");

            migrationBuilder.AddColumn<int>(
                name: "ContractorId",
                table: "Invoice",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfIssue",
                table: "Invoice",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ItemsCount",
                table: "Invoice",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Invoice",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalValue",
                table: "Invoice",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalValueInclVat",
                table: "Invoice",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_ContractorId",
                table: "Invoice",
                column: "ContractorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Contractor_ContractorId",
                table: "Invoice",
                column: "ContractorId",
                principalTable: "Contractor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_PaymentMethod_PaymentMethodId",
                table: "Invoice",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Contractor_ContractorId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_PaymentMethod_PaymentMethodId",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_ContractorId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "ContractorId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "DateOfIssue",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "ItemsCount",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "TotalValue",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "TotalValueInclVat",
                table: "Invoice");

            migrationBuilder.RenameColumn(
                name: "PaymentMethodId",
                table: "Invoice",
                newName: "InvoiceHeaderId");

            migrationBuilder.RenameIndex(
                name: "IX_Invoice_PaymentMethodId",
                table: "Invoice",
                newName: "IX_Invoice_InvoiceHeaderId");

            migrationBuilder.CreateTable(
                name: "InvoiceHeader",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContractorId = table.Column<int>(nullable: false),
                    DateOfIssue = table.Column<DateTime>(nullable: false),
                    ItemsCount = table.Column<int>(nullable: false),
                    Number = table.Column<string>(nullable: true),
                    PaymentMethodId = table.Column<int>(nullable: false),
                    TotalValue = table.Column<decimal>(nullable: false),
                    TotalValueInclVat = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceHeader_Contractor_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceHeader_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceHeader_ContractorId",
                table: "InvoiceHeader",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceHeader_PaymentMethodId",
                table: "InvoiceHeader",
                column: "PaymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_InvoiceHeader_InvoiceHeaderId",
                table: "Invoice",
                column: "InvoiceHeaderId",
                principalTable: "InvoiceHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
