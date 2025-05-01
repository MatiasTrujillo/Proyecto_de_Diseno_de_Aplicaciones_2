using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class MakeReturnTypeNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Methods_Type_ReturnTypeId",
                table: "Methods");

            migrationBuilder.DropIndex(
                name: "IX_Methods_ReturnTypeId",
                table: "Methods");

            migrationBuilder.DropColumn(
                name: "ReturnTypeId",
                table: "Methods");

            migrationBuilder.AddForeignKey(
                name: "FK_Methods_Type_Id",
                table: "Methods",
                column: "Id",
                principalTable: "Type",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Methods_Type_Id",
                table: "Methods");

            migrationBuilder.AddColumn<Guid>(
                name: "ReturnTypeId",
                table: "Methods",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Methods_ReturnTypeId",
                table: "Methods",
                column: "ReturnTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Methods_Type_ReturnTypeId",
                table: "Methods",
                column: "ReturnTypeId",
                principalTable: "Type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
