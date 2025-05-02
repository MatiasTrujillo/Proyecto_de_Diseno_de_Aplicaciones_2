using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class SeedObjectClassAndMethods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrimitiveType",
                table: "Type",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClassMethod",
                columns: table => new
                {
                    ClassesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MethodsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassMethod", x => new { x.ClassesId, x.MethodsId });
                    table.ForeignKey(
                        name: "FK_ClassMethod_Methods_MethodsId",
                        column: x => x.MethodsId,
                        principalTable: "Methods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassMethod_Type_ClassesId",
                        column: x => x.ClassesId,
                        principalTable: "Type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Primitive", "Bool", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Primitive", "String", 15 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Primitive", "Int", 7 }
                });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "HierarchyClassId", "Name", "Stereotype" },
                values: new object[] { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), "Class", null, "Object", null });

            migrationBuilder.InsertData(
                table: "Methods",
                columns: new[] { "Id", "Accessibility", "ClassId", "Name", "ReturnTypeId", "Stereotype" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 0, null, "ToString", new Guid("22222222-2222-2222-2222-222222222222"), null },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 0, null, "Equals", new Guid("11111111-1111-1111-1111-111111111111"), null },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), 0, null, "GetHashCode", new Guid("33333333-3333-3333-3333-333333333333"), null }
                });

            migrationBuilder.InsertData(
                table: "ClassMethod",
                columns: new[] { "ClassesId", "MethodsId" },
                values: new object[,]
                {
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb") },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassMethod_MethodsId",
                table: "ClassMethod",
                column: "MethodsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassMethod");

            migrationBuilder.DeleteData(
                table: "Methods",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Methods",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "Methods",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DropColumn(
                name: "PrimitiveType",
                table: "Type");
        }
    }
}
