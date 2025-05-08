using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class SeventhMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "MethodInvokedMethod",
                columns: table => new
                {
                    MethodsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvokedMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MethodInvokedMethod", x => new { x.MethodsId, x.InvokedMethodId });
                    table.ForeignKey(
                        name: "FK_MethodInvokedMethod_InvokedMethods_InvokedMethodId",
                        column: x => x.InvokedMethodId,
                        principalTable: "InvokedMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MethodInvokedMethod_Methods_MethodsId",
                        column: x => x.MethodsId,
                        principalTable: "Methods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InvokedMethodId", "MethodId", "Name", "ParameterTypeId" },
                values: new object[,]
                {
                    { new Guid("19550e78-88b0-4cb6-9b6a-28d26384ab0c"), null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), "StrIsNullOrEmpty", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("2afb59ce-db41-4971-b660-5c42177d7ef1"), null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("807000b0-0923-401f-b967-1ac1ba09bbb8"), null, new Guid("77777777-7777-7777-7777-777777777777"), "StrToCompare", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("a4f1f582-dc7a-4788-b7db-27601ec3c2c9"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("def3a789-be4c-47ab-8c48-ef45e5110e00"), null, new Guid("44444444-4444-4444-4444-444444444444"), "StrToParse", new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("0385bc4e-7620-4801-a87e-5a15f96e37ed"), "Primitive", "double", 5 },
                    { new Guid("076b6468-6d40-4f5a-8f1d-53f6720d9ef6"), "Primitive", "sByte", 2 },
                    { new Guid("33598b58-b57b-4022-a196-969db7bd261d"), "Primitive", "decimal", 4 },
                    { new Guid("43a592d1-d9fe-4f59-be1c-1db17a6e305b"), "Primitive", "ushort", 14 },
                    { new Guid("44ab1fcc-8de7-4410-81a1-c256c5966258"), "Primitive", "long", 11 },
                    { new Guid("67a4f36a-6a29-4155-a9b3-8fd6a72e7436"), "Primitive", "float", 6 },
                    { new Guid("721a2ce2-b2a4-4c78-be74-81da6f75e039"), "Primitive", "byte", 1 },
                    { new Guid("8dcd555e-50d2-4f9a-9c9b-2eac411bd86d"), "Primitive", "nint", 9 },
                    { new Guid("98e5256d-ccdc-4449-be0b-9a16093f2b7f"), "Primitive", "char", 3 },
                    { new Guid("c1fcf63f-009f-4641-bace-2a8047cb2bb7"), "Primitive", "ulong", 12 },
                    { new Guid("c9dbd344-3b4c-4080-a046-bb18814ad089"), "Primitive", "short", 13 },
                    { new Guid("cbca365a-0d4f-487b-813d-1e6e8dc62b90"), "Primitive", "nuint", 10 },
                    { new Guid("e3c8fcf6-f81b-4836-960e-15b54caa010d"), "Primitive", "uint", 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MethodInvokedMethod_InvokedMethodId",
                table: "MethodInvokedMethod",
                column: "InvokedMethodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MethodInvokedMethod");

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("19550e78-88b0-4cb6-9b6a-28d26384ab0c"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("2afb59ce-db41-4971-b660-5c42177d7ef1"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("807000b0-0923-401f-b967-1ac1ba09bbb8"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("a4f1f582-dc7a-4788-b7db-27601ec3c2c9"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("def3a789-be4c-47ab-8c48-ef45e5110e00"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("0385bc4e-7620-4801-a87e-5a15f96e37ed"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("076b6468-6d40-4f5a-8f1d-53f6720d9ef6"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("33598b58-b57b-4022-a196-969db7bd261d"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("43a592d1-d9fe-4f59-be1c-1db17a6e305b"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("44ab1fcc-8de7-4410-81a1-c256c5966258"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("67a4f36a-6a29-4155-a9b3-8fd6a72e7436"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("721a2ce2-b2a4-4c78-be74-81da6f75e039"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("8dcd555e-50d2-4f9a-9c9b-2eac411bd86d"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("98e5256d-ccdc-4449-be0b-9a16093f2b7f"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("c1fcf63f-009f-4641-bace-2a8047cb2bb7"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("c9dbd344-3b4c-4080-a046-bb18814ad089"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("cbca365a-0d4f-487b-813d-1e6e8dc62b90"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("e3c8fcf6-f81b-4836-960e-15b54caa010d"));

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
    }
}
