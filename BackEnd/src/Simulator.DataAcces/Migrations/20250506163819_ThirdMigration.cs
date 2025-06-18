using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_InvokedMethods_InvokedMethodId",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalVariables_InvokedMethods_InvokedMethodId",
                table: "LocalVariables");

            migrationBuilder.DropForeignKey(
                name: "FK_Parameters_InvokedMethods_InvokedMethodId",
                table: "Parameters");

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("f3823f23-801b-41ec-94b9-c47c9c55931a"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("09d7b371-34fd-4af4-b14b-e286b06ba0c0"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("1b09d8f3-7186-496c-82d3-ee6cbec3903b"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("24ec5780-ea54-4a0b-9d27-987ba8885dc8"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("33545d42-4251-4063-971e-544c8da9b665"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("66b744e8-0738-4fcf-b208-de685d342a6e"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("7b5f9c73-739a-45f9-8f9b-0a442704607e"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("95a689ff-a22c-4a8f-be57-d2bf36e09543"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("a37591c2-25c6-4f38-a8c6-3047eee41a17"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("b91a124f-123b-4972-bfdd-06339f658a80"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("bee6c5fd-de63-4862-987a-33d2789309a5"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("dead961b-1977-4587-9a4d-c14a0fabff52"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("e61e9302-6c22-48a8-a0b0-90ae04795f0d"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("f6f50d9f-fd26-40c6-a450-e436aaf07501"));

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
                table: "Methods",
                columns: new[] { "Id", "Accessibility", "Name", "ReturnTypeId", "Stereotype" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-444444444444"), 0, "Parse", new Guid("33333333-3333-3333-3333-333333333333"), null },
                    { new Guid("55555555-5555-5555-5555-555555555555"), 0, "MaxValue", new Guid("33333333-3333-3333-3333-333333333333"), null },
                    { new Guid("66666666-6666-6666-6666-666666666666"), 0, "MinValue", new Guid("33333333-3333-3333-3333-333333333333"), null },
                    { new Guid("77777777-7777-7777-7777-777777777777"), 0, "CompareTo", new Guid("33333333-3333-3333-3333-333333333333"), null },
                    { new Guid("88888888-8888-8888-8888-888888888888"), 0, "ToLowerCase", new Guid("22222222-2222-2222-2222-222222222222"), null },
                    { new Guid("99999999-9999-9999-9999-999999999999"), 0, "ToUpperCase", new Guid("22222222-2222-2222-2222-222222222222"), null },
                    { new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), 0, "IsNullOrEmpty", new Guid("11111111-1111-1111-1111-111111111111"), null },
                    { new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), 0, "Equals", new Guid("11111111-1111-1111-1111-111111111111"), null },
                    { new Guid("cccccccc-3333-3333-3333-cccccccccccc"), 0, "ToString", new Guid("22222222-2222-2222-2222-222222222222"), null }
                });

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InvokedMethodId", "MethodId", "Name", "ParameterTypeId" },
                values: new object[] { new Guid("012c129a-ed43-46c9-876a-8acb6bfa5949"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("11b73714-df0c-4457-b42a-6a94108f6dc9"), "Primitive", "nuint", 10 },
                    { new Guid("24332cb3-421d-4fcf-b075-0fa93f9d3d71"), "Primitive", "char", 3 },
                    { new Guid("2fa9febb-1a2c-4257-a043-572c64f253af"), "Primitive", "sByte", 2 },
                    { new Guid("30aea049-9555-4717-b5ef-d2329ad58ede"), "Primitive", "long", 11 },
                    { new Guid("3806e85e-c2c9-4fb6-9633-e2c1858f4986"), "Primitive", "ushort", 14 },
                    { new Guid("4d865a06-0b53-4264-9bcf-7a911906151a"), "Primitive", "nint", 9 },
                    { new Guid("7095a344-8520-4b27-b2f4-09a57abcfd61"), "Primitive", "ulong", 12 },
                    { new Guid("7864c9ef-85d7-4f8d-b3f8-09487170df05"), "Primitive", "uint", 8 },
                    { new Guid("7a6dc2f8-f117-4733-a349-7f5aaf35a148"), "Primitive", "byte", 1 },
                    { new Guid("9832e0ee-9ebb-492e-82d2-60f4dd657549"), "Primitive", "double", 5 },
                    { new Guid("ab55ee74-396b-4e4c-a198-3009ebd12323"), "Primitive", "float", 6 },
                    { new Guid("b2c0bf64-010a-4d67-81a4-887a561221fd"), "Primitive", "short", 13 },
                    { new Guid("ca76d4e4-3645-4384-a683-03c1d25cc9cd"), "Primitive", "decimal", 4 }
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
                    { new Guid("44444444-4444-4444-4444-444444444444"), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("66666666-6666-6666-6666-666666666666"), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("cccccccc-3333-3333-3333-cccccccccccc"), new Guid("33333333-3333-3333-3333-333333333333") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrimitiveMethod_MethodsId",
                table: "PrimitiveMethod",
                column: "MethodsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_InvokedMethods_InvokedMethodId",
                table: "Attributes",
                column: "InvokedMethodId",
                principalTable: "InvokedMethods",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalVariables_InvokedMethods_InvokedMethodId",
                table: "LocalVariables",
                column: "InvokedMethodId",
                principalTable: "InvokedMethods",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parameters_InvokedMethods_InvokedMethodId",
                table: "Parameters",
                column: "InvokedMethodId",
                principalTable: "InvokedMethods",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_InvokedMethods_InvokedMethodId",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalVariables_InvokedMethods_InvokedMethodId",
                table: "LocalVariables");

            migrationBuilder.DropForeignKey(
                name: "FK_Parameters_InvokedMethods_InvokedMethodId",
                table: "Parameters");

            migrationBuilder.DropTable(
                name: "PrimitiveMethod");

            migrationBuilder.DeleteData(
                table: "Methods",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Methods",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "Methods",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.DeleteData(
                table: "Methods",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"));

            migrationBuilder.DeleteData(
                table: "Methods",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"));

            migrationBuilder.DeleteData(
                table: "Methods",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"));

            migrationBuilder.DeleteData(
                table: "Methods",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Methods",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "Methods",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-3333-3333-3333-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("012c129a-ed43-46c9-876a-8acb6bfa5949"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("11b73714-df0c-4457-b42a-6a94108f6dc9"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("24332cb3-421d-4fcf-b075-0fa93f9d3d71"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("2fa9febb-1a2c-4257-a043-572c64f253af"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("30aea049-9555-4717-b5ef-d2329ad58ede"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("3806e85e-c2c9-4fb6-9633-e2c1858f4986"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("4d865a06-0b53-4264-9bcf-7a911906151a"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("7095a344-8520-4b27-b2f4-09a57abcfd61"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("7864c9ef-85d7-4f8d-b3f8-09487170df05"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("7a6dc2f8-f117-4733-a349-7f5aaf35a148"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("9832e0ee-9ebb-492e-82d2-60f4dd657549"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("ab55ee74-396b-4e4c-a198-3009ebd12323"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("b2c0bf64-010a-4d67-81a4-887a561221fd"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("ca76d4e4-3645-4384-a683-03c1d25cc9cd"));

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InvokedMethodId", "MethodId", "Name", "ParameterTypeId" },
                values: new object[] { new Guid("f3823f23-801b-41ec-94b9-c47c9c55931a"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("09d7b371-34fd-4af4-b14b-e286b06ba0c0"), "Primitive", "decimal", 4 },
                    { new Guid("1b09d8f3-7186-496c-82d3-ee6cbec3903b"), "Primitive", "ushort", 14 },
                    { new Guid("24ec5780-ea54-4a0b-9d27-987ba8885dc8"), "Primitive", "ulong", 12 },
                    { new Guid("33545d42-4251-4063-971e-544c8da9b665"), "Primitive", "char", 3 },
                    { new Guid("66b744e8-0738-4fcf-b208-de685d342a6e"), "Primitive", "nuint", 10 },
                    { new Guid("7b5f9c73-739a-45f9-8f9b-0a442704607e"), "Primitive", "short", 13 },
                    { new Guid("95a689ff-a22c-4a8f-be57-d2bf36e09543"), "Primitive", "byte", 1 },
                    { new Guid("a37591c2-25c6-4f38-a8c6-3047eee41a17"), "Primitive", "uint", 8 },
                    { new Guid("b91a124f-123b-4972-bfdd-06339f658a80"), "Primitive", "float", 6 },
                    { new Guid("bee6c5fd-de63-4862-987a-33d2789309a5"), "Primitive", "long", 11 },
                    { new Guid("dead961b-1977-4587-9a4d-c14a0fabff52"), "Primitive", "double", 5 },
                    { new Guid("e61e9302-6c22-48a8-a0b0-90ae04795f0d"), "Primitive", "sByte", 2 },
                    { new Guid("f6f50d9f-fd26-40c6-a450-e436aaf07501"), "Primitive", "nint", 9 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_InvokedMethods_InvokedMethodId",
                table: "Attributes",
                column: "InvokedMethodId",
                principalTable: "InvokedMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_LocalVariables_InvokedMethods_InvokedMethodId",
                table: "LocalVariables",
                column: "InvokedMethodId",
                principalTable: "InvokedMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Parameters_InvokedMethods_InvokedMethodId",
                table: "Parameters",
                column: "InvokedMethodId",
                principalTable: "InvokedMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
