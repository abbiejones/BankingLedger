using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankingLedger.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    accountNumber = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    userId = table.Column<int>(nullable: false),
                    balance = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.accountNumber);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    transactionNo = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    transactionTime = table.Column<DateTime>(nullable: false, defaultValueSql: "datetime('now')"),
                    userId = table.Column<int>(nullable: false),
                    type = table.Column<int>(nullable: false),
                    amount = table.Column<decimal>(nullable: false),
                    accountNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.transactionNo);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    firstName = table.Column<string>(nullable: false),
                    lastName = table.Column<string>(nullable: false),
                    userName = table.Column<string>(nullable: false),
                    passwordSalt = table.Column<byte[]>(nullable: false),
                    passwordHash = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userId);
                    table.UniqueConstraint("AK_Users_userName", x => x.userName);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
