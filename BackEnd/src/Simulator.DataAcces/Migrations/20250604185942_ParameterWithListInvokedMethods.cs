using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class ParameterWithListInvokedMethods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parameters_InvokedMethods_InvokedMethodId",
                table: "Parameters");

            migrationBuilder.DropIndex(
                name: "IX_Parameters_InvokedMethodId",
                table: "Parameters");

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("2a720504-a148-4105-9bfc-46c405e05d26"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("37e52f34-b98e-4c0d-966e-627204e0a845"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("8ccc02c2-2bfd-4040-9e54-dd45831e9974"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("8e63ec95-e792-48b7-9517-2300287d3b3f"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("d775694b-fe70-419f-97a7-23eb9b738f00"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("090ebc78-49f6-4f32-828d-15082dbbf4cf"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("09209d23-9c31-427c-ac1a-89db0fa60929"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("183f5b94-2612-4598-847c-8015ec72c47f"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("2a20f1bf-91fa-4a2e-b10d-390979da5c17"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("5a188952-c94a-4f3d-bd52-b26dbd1d4070"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("61aee306-e545-4b54-9457-2b413f121495"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("64130187-be6d-449c-91ca-55ef07927438"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("7664da91-38c0-4927-b398-510d73d43db5"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("77673fd4-aa6e-4b25-a40c-3720c1adc217"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("b6003fb9-194c-4b91-b538-7e0cea5eff30"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("b680807d-db86-4c70-a12a-9b986de0abdc"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("ded0109b-344b-473f-a5fd-1be06134d61c"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("ef2c1ef0-e40e-4ecb-bf2b-766fed44216d"));

            migrationBuilder.DropColumn(
                name: "InvokedMethodId",
                table: "Parameters");

            migrationBuilder.CreateTable(
                name: "ParameterInvokedMethods",
                columns: table => new
                {
                    ParameterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvokedMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterInvokedMethods", x => new { x.ParameterId, x.InvokedMethodId });
                    table.ForeignKey(
                        name: "FK_ParameterInvokedMethods_InvokedMethods_InvokedMethodId",
                        column: x => x.InvokedMethodId,
                        principalTable: "InvokedMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParameterInvokedMethods_Parameters_ParameterId",
                        column: x => x.ParameterId,
                        principalTable: "Parameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_ParameterInvokedMethods_InvokedMethodId",
                table: "ParameterInvokedMethods",
                column: "InvokedMethodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParameterInvokedMethods");

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

            migrationBuilder.AddColumn<Guid>(
                name: "InvokedMethodId",
                table: "Parameters",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InvokedMethodId", "MethodId", "MethodSignatureId", "Name", "ParameterTypeId" },
                values: new object[,]
                {
                    { new Guid("2a720504-a148-4105-9bfc-46c405e05d26"), null, new Guid("77777777-7777-7777-7777-777777777777"), null, "StrToCompare", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("37e52f34-b98e-4c0d-966e-627204e0a845"), null, new Guid("44444444-4444-4444-4444-444444444444"), null, "StrToParse", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("8ccc02c2-2bfd-4040-9e54-dd45831e9974"), null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("8e63ec95-e792-48b7-9517-2300287d3b3f"), null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), null, "StrIsNullOrEmpty", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("d775694b-fe70-419f-97a7-23eb9b738f00"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") }
                });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("090ebc78-49f6-4f32-828d-15082dbbf4cf"), "Primitive", "char", 3 },
                    { new Guid("09209d23-9c31-427c-ac1a-89db0fa60929"), "Primitive", "nint", 9 },
                    { new Guid("183f5b94-2612-4598-847c-8015ec72c47f"), "Primitive", "long", 11 },
                    { new Guid("2a20f1bf-91fa-4a2e-b10d-390979da5c17"), "Primitive", "float", 6 },
                    { new Guid("5a188952-c94a-4f3d-bd52-b26dbd1d4070"), "Primitive", "uint", 8 },
                    { new Guid("61aee306-e545-4b54-9457-2b413f121495"), "Primitive", "nuint", 10 },
                    { new Guid("64130187-be6d-449c-91ca-55ef07927438"), "Primitive", "ushort", 14 },
                    { new Guid("7664da91-38c0-4927-b398-510d73d43db5"), "Primitive", "short", 13 },
                    { new Guid("77673fd4-aa6e-4b25-a40c-3720c1adc217"), "Primitive", "double", 5 },
                    { new Guid("b6003fb9-194c-4b91-b538-7e0cea5eff30"), "Primitive", "ulong", 12 },
                    { new Guid("b680807d-db86-4c70-a12a-9b986de0abdc"), "Primitive", "sByte", 2 },
                    { new Guid("ded0109b-344b-473f-a5fd-1be06134d61c"), "Primitive", "decimal", 4 },
                    { new Guid("ef2c1ef0-e40e-4ecb-bf2b-766fed44216d"), "Primitive", "byte", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_InvokedMethodId",
                table: "Parameters",
                column: "InvokedMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parameters_InvokedMethods_InvokedMethodId",
                table: "Parameters",
                column: "InvokedMethodId",
                principalTable: "InvokedMethods",
                principalColumn: "Id");
        }
    }
}
