using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace BlazorHero.CleanArchitecture.Application.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrganizationID",
                table: "users");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationDetailsId",
                table: "users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organization_AddressInfo_AddressId",
                        column: x => x.AddressId,
                        principalTable: "AddressInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_AddressId",
                table: "users",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_users_OrganizationDetailsId",
                table: "users",
                column: "OrganizationDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_AddressId",
                table: "Organization",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_AddressInfo_AddressId",
                table: "users",
                column: "AddressId",
                principalTable: "AddressInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_users_Organization_OrganizationDetailsId",
                table: "users",
                column: "OrganizationDetailsId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_AddressInfo_AddressId",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "FK_users_Organization_OrganizationDetailsId",
                table: "users");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropIndex(
                name: "IX_users_AddressId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_OrganizationDetailsId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "OrganizationDetailsId",
                table: "users");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationID",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
