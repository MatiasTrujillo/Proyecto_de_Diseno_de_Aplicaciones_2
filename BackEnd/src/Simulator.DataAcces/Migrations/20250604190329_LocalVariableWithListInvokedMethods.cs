using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class LocalVariableWithListInvokedMethods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocalVariables_InvokedMethods_InvokedMethodId",
                table: "LocalVariables");

            migrationBuilder.DropIndex(
                name: "IX_LocalVariables_InvokedMethodId",
                table: "LocalVariables");

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("03f60b14-b0a4-4dda-a860-1792f032082b"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("6b983ca0-6b35-4e35-8ab7-47d55ca40c69"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("b6a0ab92-18e6-4086-8b97-1d376caa6ee8"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("d4c6e771-d64a-4cc1-b3d1-95dfcd9e4e5a"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("e3c7a9f1-abdb-4cf3-8a61-9830c80ad18a"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("1c4c114e-b0be-4947-ab13-9b4eff3a965d"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("2569a78c-91f9-4545-8cc1-f37eeed28004"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("29762481-4c55-46e4-ad14-8a7a29e95236"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("34efa5d3-47ca-487c-bc23-c535baacdce7"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("402f2e6e-0081-43dc-99c8-e43bbc2e7bf5"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("4cec63db-d5e3-4f10-a26d-ae429c2a42a5"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("7e87aa3e-6ebd-4566-ac6a-34e6407db78f"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("8d8a8d53-a0e3-41c1-8b4b-8eec79bccf47"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("ac7bab45-622d-4b21-a517-16fd3e4fa616"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("bd7487dd-592f-4793-b0e8-9f05ffff40a8"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("d3be0866-4616-4bbc-8379-1b33fe4e6170"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("e3526005-1da3-415f-a20e-050867a15cbd"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("f536d944-5867-4ccc-8f9c-40558ead52cb"));

            migrationBuilder.DropColumn(
                name: "InvokedMethodId",
                table: "LocalVariables");

            migrationBuilder.CreateTable(
                name: "LocalVariableInvokedMethods",
                columns: table => new
                {
                    LocalVariableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvokedMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalVariableInvokedMethods", x => new { x.LocalVariableId, x.InvokedMethodId });
                    table.ForeignKey(
                        name: "FK_LocalVariableInvokedMethods_InvokedMethods_InvokedMethodId",
                        column: x => x.InvokedMethodId,
                        principalTable: "InvokedMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocalVariableInvokedMethods_LocalVariables_LocalVariableId",
                        column: x => x.LocalVariableId,
                        principalTable: "LocalVariables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "MethodId", "MethodSignatureId", "Name", "ParameterTypeId" },
                values: new object[,]
                {
                    { new Guid("0bae6159-4191-4ae0-9614-748f9493ff05"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("30f00c60-eb43-456d-8888-948f073bf6ee"), new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), null, "StrIsNullOrEmpty", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("33c7d113-465e-4833-b62d-47acfda4c038"), new Guid("44444444-4444-4444-4444-444444444444"), null, "StrToParse", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("a6df7a65-cf52-486b-888c-ab1d43e435c1"), new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("bf088c06-a045-47ad-8f31-43e73064c437"), new Guid("77777777-7777-7777-7777-777777777777"), null, "StrToCompare", new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("04af3bde-7ba7-40cd-9108-7ce13de1a322"), "Primitive", "ulong", 12 },
                    { new Guid("1474fc81-811c-408a-996b-acb344571f24"), "Primitive", "uint", 8 },
                    { new Guid("1dde1080-966b-4cfd-a326-ddd76987ff8e"), "Primitive", "nint", 9 },
                    { new Guid("4aa335f8-9574-49bb-84e9-788973d0b817"), "Primitive", "float", 6 },
                    { new Guid("56c546db-a772-4583-a6db-310a201e6f7b"), "Primitive", "byte", 1 },
                    { new Guid("8218ee2b-4db6-4ff2-b5ff-8b67c0476f36"), "Primitive", "double", 5 },
                    { new Guid("a16f44c1-bc9d-4114-bbe3-58ee4464fe87"), "Primitive", "char", 3 },
                    { new Guid("a5b7cf38-2058-4fe7-86f8-279a39b15e40"), "Primitive", "sByte", 2 },
                    { new Guid("b1167eaf-baa2-468c-aae2-6fd8691580e5"), "Primitive", "long", 11 },
                    { new Guid("cdbbda7e-71f6-4175-abb2-52b253da0343"), "Primitive", "decimal", 4 },
                    { new Guid("d1e71c6c-cf5d-46be-aff5-99eaf21217d9"), "Primitive", "ushort", 14 },
                    { new Guid("e4183b88-0266-48d3-a8c5-500f096d3679"), "Primitive", "short", 13 },
                    { new Guid("e6f9f4bb-0483-479b-84a9-a77156e2eb79"), "Primitive", "nuint", 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocalVariableInvokedMethods_InvokedMethodId",
                table: "LocalVariableInvokedMethods",
                column: "InvokedMethodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalVariableInvokedMethods");

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("0bae6159-4191-4ae0-9614-748f9493ff05"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("30f00c60-eb43-456d-8888-948f073bf6ee"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("33c7d113-465e-4833-b62d-47acfda4c038"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("a6df7a65-cf52-486b-888c-ab1d43e435c1"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("bf088c06-a045-47ad-8f31-43e73064c437"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("04af3bde-7ba7-40cd-9108-7ce13de1a322"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("1474fc81-811c-408a-996b-acb344571f24"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("1dde1080-966b-4cfd-a326-ddd76987ff8e"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("4aa335f8-9574-49bb-84e9-788973d0b817"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("56c546db-a772-4583-a6db-310a201e6f7b"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("8218ee2b-4db6-4ff2-b5ff-8b67c0476f36"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("a16f44c1-bc9d-4114-bbe3-58ee4464fe87"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("a5b7cf38-2058-4fe7-86f8-279a39b15e40"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("b1167eaf-baa2-468c-aae2-6fd8691580e5"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("cdbbda7e-71f6-4175-abb2-52b253da0343"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("d1e71c6c-cf5d-46be-aff5-99eaf21217d9"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("e4183b88-0266-48d3-a8c5-500f096d3679"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("e6f9f4bb-0483-479b-84a9-a77156e2eb79"));

            migrationBuilder.AddColumn<Guid>(
                name: "InvokedMethodId",
                table: "LocalVariables",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "MethodId", "MethodSignatureId", "Name", "ParameterTypeId" },
                values: new object[,]
                {
                    { new Guid("03f60b14-b0a4-4dda-a860-1792f032082b"), new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), null, "StrIsNullOrEmpty", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("6b983ca0-6b35-4e35-8ab7-47d55ca40c69"), new Guid("77777777-7777-7777-7777-777777777777"), null, "StrToCompare", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("b6a0ab92-18e6-4086-8b97-1d376caa6ee8"), new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("d4c6e771-d64a-4cc1-b3d1-95dfcd9e4e5a"), new Guid("44444444-4444-4444-4444-444444444444"), null, "StrToParse", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("e3c7a9f1-abdb-4cf3-8a61-9830c80ad18a"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") }
                });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("1c4c114e-b0be-4947-ab13-9b4eff3a965d"), "Primitive", "double", 5 },
                    { new Guid("2569a78c-91f9-4545-8cc1-f37eeed28004"), "Primitive", "ulong", 12 },
                    { new Guid("29762481-4c55-46e4-ad14-8a7a29e95236"), "Primitive", "nuint", 10 },
                    { new Guid("34efa5d3-47ca-487c-bc23-c535baacdce7"), "Primitive", "byte", 1 },
                    { new Guid("402f2e6e-0081-43dc-99c8-e43bbc2e7bf5"), "Primitive", "decimal", 4 },
                    { new Guid("4cec63db-d5e3-4f10-a26d-ae429c2a42a5"), "Primitive", "sByte", 2 },
                    { new Guid("7e87aa3e-6ebd-4566-ac6a-34e6407db78f"), "Primitive", "uint", 8 },
                    { new Guid("8d8a8d53-a0e3-41c1-8b4b-8eec79bccf47"), "Primitive", "float", 6 },
                    { new Guid("ac7bab45-622d-4b21-a517-16fd3e4fa616"), "Primitive", "short", 13 },
                    { new Guid("bd7487dd-592f-4793-b0e8-9f05ffff40a8"), "Primitive", "ushort", 14 },
                    { new Guid("d3be0866-4616-4bbc-8379-1b33fe4e6170"), "Primitive", "char", 3 },
                    { new Guid("e3526005-1da3-415f-a20e-050867a15cbd"), "Primitive", "long", 11 },
                    { new Guid("f536d944-5867-4ccc-8f9c-40558ead52cb"), "Primitive", "nint", 9 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocalVariables_InvokedMethodId",
                table: "LocalVariables",
                column: "InvokedMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalVariables_InvokedMethods_InvokedMethodId",
                table: "LocalVariables",
                column: "InvokedMethodId",
                principalTable: "InvokedMethods",
                principalColumn: "Id");
        }
    }
}
