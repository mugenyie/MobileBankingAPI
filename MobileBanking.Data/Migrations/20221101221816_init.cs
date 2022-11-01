using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MobileBanking.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOnUTC = table.Column<DateTime>(nullable: false),
                    UpdatedOnUTC = table.Column<DateTime>(nullable: false),
                    AccountNumber = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    OpeningBalance = table.Column<decimal>(type: "decimal(15,3)", nullable: false),
                    NewBalance = table.Column<decimal>(type: "decimal(15,3)", nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOnUTC = table.Column<DateTime>(nullable: false),
                    UpdatedOnUTC = table.Column<DateTime>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "LogData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOnUTC = table.Column<DateTime>(nullable: false),
                    UpdatedOnUTC = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MetaData = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceProviders",
                columns: table => new
                {
                    ServiceProviderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOnUTC = table.Column<DateTime>(nullable: false),
                    UpdatedOnUTC = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceProviders", x => x.ServiceProviderId);
                });

            migrationBuilder.CreateTable(
                name: "TransactionLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOnUTC = table.Column<DateTime>(nullable: false),
                    UpdatedOnUTC = table.Column<DateTime>(nullable: false),
                    AccountId = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(15,3)", nullable: false),
                    NewBalance = table.Column<decimal>(type: "decimal(15,3)", nullable: false),
                    Description = table.Column<string>(nullable: true),
                    RecipientId = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    ServiceProviderId = table.Column<int>(nullable: false),
                    PaymentStatus = table.Column<int>(nullable: false),
                    PaymentReference = table.Column<string>(nullable: true),
                    PaymentStatusMetaData = table.Column<string>(nullable: true),
                    OrderStatus = table.Column<int>(nullable: false),
                    OrderReference = table.Column<string>(nullable: true),
                    OrderStatusMetaData = table.Column<string>(nullable: true),
                    TransactionStatus = table.Column<int>(nullable: false),
                    TransactionStatusMessage = table.Column<string>(nullable: true),
                    TransactionMetaData = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOnUTC = table.Column<DateTime>(nullable: false),
                    UpdatedOnUTC = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    AccountId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerAccounts_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerAccounts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOnUTC = table.Column<DateTime>(nullable: false),
                    UpdatedOnUTC = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    ServiceProviderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ServiceProviders_ServiceProviderId",
                        column: x => x.ServiceProviderId,
                        principalTable: "ServiceProviders",
                        principalColumn: "ServiceProviderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountNumber",
                table: "Accounts",
                column: "AccountNumber",
                unique: true,
                filter: "[AccountNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccounts_AccountId",
                table: "CustomerAccounts",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccounts_CustomerId",
                table: "CustomerAccounts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ServiceProviderId",
                table: "Products",
                column: "ServiceProviderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerAccounts");

            migrationBuilder.DropTable(
                name: "LogData");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "TransactionLogs");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "ServiceProviders");
        }
    }
}
