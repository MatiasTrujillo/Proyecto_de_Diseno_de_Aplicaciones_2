using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class SixthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MethodInvokedMethods");

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("d51ec3a4-eb93-4312-8dc5-9c0b1a3fe1b2"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("03edf7c2-c022-4310-957d-b67dcdd53311"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("1f9ecbf0-01c8-4206-9a11-60a3d31811aa"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("1fdf1e6e-4287-4ac2-a773-895b0318b44d"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("31011703-063b-4caf-834f-4c0b745bcc55"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("40b743f5-3dd4-43cf-afa8-298b116572bd"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("4dc777c5-431f-489d-9eb3-b55f54a6019a"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("55e4c39a-fd6f-4840-acbc-9327c800d57c"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("971715ca-7181-4163-a4a8-4115aa746e5d"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("9d8be925-e45e-42a7-a841-9608038b638b"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("a8ed71d3-608a-442c-9429-50801ba0f0fc"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("b4dbb59d-79cc-4e0d-a6b1-c6bbbc63d26c"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("d56ac69d-08bb-4dc0-b55c-4ade6578828f"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("e65db76b-5414-497b-8a61-495068fb1aa1"));

            migrationBuilder.InsertData(
                table: "Methods",
                columns: new[] { "Id", "Accessibility", "Name", "ReturnTypeId", "Stereotype" },
                values: new object[] { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), 0, "ToString", new Guid("22222222-2222-2222-2222-222222222222"), null });

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InvokedMethodId", "MethodId", "Name", "ParameterTypeId" },
                values: new object[,]
                {
                    { new Guid("9c177e3a-5a7f-43d8-a238-294121f17f1c"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("9ed05baa-d31b-4d77-8238-7d1fcf1c2884"), null, new Guid("44444444-4444-4444-4444-444444444444"), "StrToParse", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("b2f95d64-713d-4a89-ba98-47f906e22e90"), null, new Guid("77777777-7777-7777-7777-777777777777"), "StrToCompare", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("c929a99a-834d-4b8a-98b0-6502cbd4b40e"), null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), "StrIsNullOrEmpty", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("d4556262-2f29-42b2-89ec-66a55a0c9f87"), null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") }
                });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("448c3075-83e0-4c4c-af18-3276d4a15bd9"), "Primitive", "char", 3 },
                    { new Guid("5a589fd8-bf5d-401d-aa8e-b9cfe117fa7e"), "Primitive", "nint", 9 },
                    { new Guid("8a3c450b-6993-42bd-a6f2-ed71e427d759"), "Primitive", "float", 6 },
                    { new Guid("9f4001af-13b2-4c48-aae5-58f7d830fc01"), "Primitive", "long", 11 },
                    { new Guid("a75dc1e3-388a-4371-a6f1-fcf591e6b700"), "Primitive", "byte", 1 },
                    { new Guid("ade0feb4-f0b9-466a-8f25-f1d950fcb704"), "Primitive", "nuint", 10 },
                    { new Guid("b190d95b-41e3-42a5-92aa-9ff1dda70182"), "Primitive", "double", 5 },
                    { new Guid("b3078fa9-130e-49c0-a3f4-78a307e27a22"), "Primitive", "uint", 8 },
                    { new Guid("beafdc16-a66c-455c-a240-bd3ca13fd69e"), "Primitive", "short", 13 },
                    { new Guid("ca6d6930-3bb7-47bf-bb33-cee47ac932a1"), "Primitive", "decimal", 4 },
                    { new Guid("e634eff2-b8d4-4dfa-95c5-9d4b9427ad73"), "Primitive", "ulong", 12 },
                    { new Guid("ee8d40ef-ae2f-4b79-81c2-09471093663b"), "Primitive", "ushort", 14 },
                    { new Guid("f507c28b-22b0-4f08-90bf-9cfd92d530b1"), "Primitive", "sByte", 2 }
                });

            migrationBuilder.InsertData(
                table: "PrimitiveMethod",
                columns: new[] { "MethodsId", "PrimitiveId" },
                values: new object[] { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new Guid("22222222-2222-2222-2222-222222222222") });

            migrationBuilder.CreateIndex(
                name: "IX_InvokedMethods_MethodId",
                table: "InvokedMethods",
                column: "MethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvokedMethods_Methods_MethodId",
                table: "InvokedMethods",
                column: "MethodId",
                principalTable: "Methods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvokedMethods_Methods_MethodId",
                table: "InvokedMethods");

            migrationBuilder.DropIndex(
                name: "IX_InvokedMethods_MethodId",
                table: "InvokedMethods");

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("9c177e3a-5a7f-43d8-a238-294121f17f1c"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("9ed05baa-d31b-4d77-8238-7d1fcf1c2884"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("b2f95d64-713d-4a89-ba98-47f906e22e90"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("c929a99a-834d-4b8a-98b0-6502cbd4b40e"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("d4556262-2f29-42b2-89ec-66a55a0c9f87"));

            migrationBuilder.DeleteData(
                table: "PrimitiveMethod",
                keyColumns: new[] { "MethodsId", "PrimitiveId" },
                keyValues: new object[] { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new Guid("22222222-2222-2222-2222-222222222222") });

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("448c3075-83e0-4c4c-af18-3276d4a15bd9"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("5a589fd8-bf5d-401d-aa8e-b9cfe117fa7e"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("8a3c450b-6993-42bd-a6f2-ed71e427d759"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("9f4001af-13b2-4c48-aae5-58f7d830fc01"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("a75dc1e3-388a-4371-a6f1-fcf591e6b700"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("ade0feb4-f0b9-466a-8f25-f1d950fcb704"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("b190d95b-41e3-42a5-92aa-9ff1dda70182"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("b3078fa9-130e-49c0-a3f4-78a307e27a22"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("beafdc16-a66c-455c-a240-bd3ca13fd69e"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("ca6d6930-3bb7-47bf-bb33-cee47ac932a1"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("e634eff2-b8d4-4dfa-95c5-9d4b9427ad73"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("ee8d40ef-ae2f-4b79-81c2-09471093663b"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("f507c28b-22b0-4f08-90bf-9cfd92d530b1"));

            migrationBuilder.DeleteData(
                table: "Methods",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"));

            migrationBuilder.CreateTable(
                name: "MethodInvokedMethods",
                columns: table => new
                {
                    MethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvokedMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MethodInvokedMethods", x => new { x.MethodId, x.InvokedMethodId });
                    table.ForeignKey(
                        name: "FK_MethodInvokedMethods_InvokedMethods_InvokedMethodId",
                        column: x => x.InvokedMethodId,
                        principalTable: "InvokedMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MethodInvokedMethods_Methods_MethodId",
                        column: x => x.MethodId,
                        principalTable: "Methods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InvokedMethodId", "MethodId", "Name", "ParameterTypeId" },
                values: new object[] { new Guid("d51ec3a4-eb93-4312-8dc5-9c0b1a3fe1b2"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("03edf7c2-c022-4310-957d-b67dcdd53311"), "Primitive", "ulong", 12 },
                    { new Guid("1f9ecbf0-01c8-4206-9a11-60a3d31811aa"), "Primitive", "long", 11 },
                    { new Guid("1fdf1e6e-4287-4ac2-a773-895b0318b44d"), "Primitive", "double", 5 },
                    { new Guid("31011703-063b-4caf-834f-4c0b745bcc55"), "Primitive", "ushort", 14 },
                    { new Guid("40b743f5-3dd4-43cf-afa8-298b116572bd"), "Primitive", "float", 6 },
                    { new Guid("4dc777c5-431f-489d-9eb3-b55f54a6019a"), "Primitive", "short", 13 },
                    { new Guid("55e4c39a-fd6f-4840-acbc-9327c800d57c"), "Primitive", "sByte", 2 },
                    { new Guid("971715ca-7181-4163-a4a8-4115aa746e5d"), "Primitive", "byte", 1 },
                    { new Guid("9d8be925-e45e-42a7-a841-9608038b638b"), "Primitive", "nint", 9 },
                    { new Guid("a8ed71d3-608a-442c-9429-50801ba0f0fc"), "Primitive", "uint", 8 },
                    { new Guid("b4dbb59d-79cc-4e0d-a6b1-c6bbbc63d26c"), "Primitive", "nuint", 10 },
                    { new Guid("d56ac69d-08bb-4dc0-b55c-4ade6578828f"), "Primitive", "decimal", 4 },
                    { new Guid("e65db76b-5414-497b-8a61-495068fb1aa1"), "Primitive", "char", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MethodInvokedMethods_InvokedMethodId",
                table: "MethodInvokedMethods",
                column: "InvokedMethodId");
        }
    }
}
