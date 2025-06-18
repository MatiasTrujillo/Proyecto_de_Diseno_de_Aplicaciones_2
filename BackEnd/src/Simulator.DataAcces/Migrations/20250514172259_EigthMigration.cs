using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class EigthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassMethod");

            migrationBuilder.DropTable(
                name: "PrimitiveMethod");

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

            migrationBuilder.CreateTable(
                name: "TypeMethod",
                columns: table => new
                {
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MethodsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeMethod", x => new { x.TypeId, x.MethodsId });
                    table.ForeignKey(
                        name: "FK_TypeMethod_Methods_MethodsId",
                        column: x => x.MethodsId,
                        principalTable: "Methods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TypeMethod_Type_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InvokedMethodId", "MethodId", "Name", "ParameterTypeId" },
                values: new object[,]
                {
                    { new Guid("0fe48caf-d45c-40f4-8be2-b48fd661d97f"), null, new Guid("77777777-7777-7777-7777-777777777777"), "StrToCompare", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("436569d4-23b8-436a-9550-dbb6ff0744d0"), null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), "StrIsNullOrEmpty", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("548d565c-6988-498b-9220-93a61ca2be79"), null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("79d298b8-40fa-4a34-bdbb-7906ad6f869c"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("af0e1944-8cd2-4d05-9196-c8270202f310"), null, new Guid("44444444-4444-4444-4444-444444444444"), "StrToParse", new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("03b39ee5-305f-46d3-8007-ef3979d99738"), "Primitive", "byte", 1 },
                    { new Guid("07a5ec8e-348a-4879-b630-fbac5a4cf6da"), "Primitive", "double", 5 },
                    { new Guid("220663c0-aef4-48d8-bb40-e860e43e68e5"), "Primitive", "long", 11 },
                    { new Guid("25cffcc3-37b2-4047-82b9-33903fc96906"), "Primitive", "ushort", 14 },
                    { new Guid("3f6b5b13-19e2-49bf-961e-43014db463f5"), "Primitive", "short", 13 },
                    { new Guid("5e9f1d37-3676-4cb3-836a-86165f770377"), "Primitive", "nint", 9 },
                    { new Guid("6ccac1a8-a281-474a-859a-ca3f3af237b0"), "Primitive", "float", 6 },
                    { new Guid("72c76c45-f8d6-41ab-b9c6-424c92ece934"), "Primitive", "sByte", 2 },
                    { new Guid("73459a3e-cc60-4313-8add-1344b0371325"), "Primitive", "decimal", 4 },
                    { new Guid("8970636b-75c9-4cfb-9390-c3eba6f788da"), "Primitive", "uint", 8 },
                    { new Guid("9f6332b7-f46e-42cf-b381-e4738594b9aa"), "Primitive", "ulong", 12 },
                    { new Guid("c0d019e0-72ea-4286-a633-2615bfcf6b78"), "Primitive", "nuint", 10 },
                    { new Guid("f8a9f44a-cba5-4e78-8e49-cb8b5c5e1a66"), "Primitive", "char", 3 }
                });

            migrationBuilder.InsertData(
                table: "TypeMethod",
                columns: new[] { "MethodsId", "TypeId" },
                values: new object[,]
                {
                    { new Guid("88888888-8888-8888-8888-888888888888"), new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("99999999-9999-9999-9999-999999999999"), new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("66666666-6666-6666-6666-666666666666"), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("cccccccc-3333-3333-3333-cccccccccccc"), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TypeMethod_MethodsId",
                table: "TypeMethod",
                column: "MethodsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TypeMethod");

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("0fe48caf-d45c-40f4-8be2-b48fd661d97f"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("436569d4-23b8-436a-9550-dbb6ff0744d0"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("548d565c-6988-498b-9220-93a61ca2be79"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("79d298b8-40fa-4a34-bdbb-7906ad6f869c"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("af0e1944-8cd2-4d05-9196-c8270202f310"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("03b39ee5-305f-46d3-8007-ef3979d99738"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("07a5ec8e-348a-4879-b630-fbac5a4cf6da"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("220663c0-aef4-48d8-bb40-e860e43e68e5"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("25cffcc3-37b2-4047-82b9-33903fc96906"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("3f6b5b13-19e2-49bf-961e-43014db463f5"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("5e9f1d37-3676-4cb3-836a-86165f770377"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("6ccac1a8-a281-474a-859a-ca3f3af237b0"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("72c76c45-f8d6-41ab-b9c6-424c92ece934"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("73459a3e-cc60-4313-8add-1344b0371325"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("8970636b-75c9-4cfb-9390-c3eba6f788da"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("9f6332b7-f46e-42cf-b381-e4738594b9aa"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("c0d019e0-72ea-4286-a633-2615bfcf6b78"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("f8a9f44a-cba5-4e78-8e49-cb8b5c5e1a66"));

            migrationBuilder.CreateTable(
                name: "ClassMethod",
                columns: table => new
                {
                    ClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MethodsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassMethod", x => new { x.ClassId, x.MethodsId });
                    table.ForeignKey(
                        name: "FK_ClassMethod_Methods_MethodsId",
                        column: x => x.MethodsId,
                        principalTable: "Methods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassMethod_Type_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrimitiveMethod",
                columns: table => new
                {
                    PrimitiveId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MethodsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimitiveMethod", x => new { x.PrimitiveId, x.MethodsId });
                    table.ForeignKey(
                        name: "FK_PrimitiveMethod_Methods_MethodsId",
                        column: x => x.MethodsId,
                        principalTable: "Methods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrimitiveMethod_Type_PrimitiveId",
                        column: x => x.PrimitiveId,
                        principalTable: "Type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ClassMethod",
                columns: new[] { "ClassId", "MethodsId" },
                values: new object[,]
                {
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb") },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc") }
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
                table: "PrimitiveMethod",
                columns: new[] { "MethodsId", "PrimitiveId" },
                values: new object[,]
                {
                    { new Guid("88888888-8888-8888-8888-888888888888"), new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("99999999-9999-9999-9999-999999999999"), new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("66666666-6666-6666-6666-666666666666"), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("cccccccc-3333-3333-3333-cccccccccccc"), new Guid("33333333-3333-3333-3333-333333333333") }
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
                name: "IX_ClassMethod_MethodsId",
                table: "ClassMethod",
                column: "MethodsId");

            migrationBuilder.CreateIndex(
                name: "IX_PrimitiveMethod_MethodsId",
                table: "PrimitiveMethod",
                column: "MethodsId");
        }
    }
}
