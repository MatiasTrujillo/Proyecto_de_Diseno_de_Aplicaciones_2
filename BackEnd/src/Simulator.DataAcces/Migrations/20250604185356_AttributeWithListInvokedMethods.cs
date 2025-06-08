using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class AttributeWithListInvokedMethods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_InvokedMethods_InvokedMethodId",
                table: "Attributes");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_InvokedMethodId",
                table: "Attributes");

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("1522d1fa-d8e1-4d6c-be25-5d1bb219eac8"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("2151c622-ae5d-4b58-9091-b943d1ec047c"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("29cfc2d6-ec7e-4689-a762-0821d9874a1d"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("9f4a90be-9f1b-4a73-bd92-951c8eee2e28"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("ca98f20c-e15b-480c-b809-35953a9aa9bf"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("4b67ac8e-f5c6-4117-bb1f-1c21fd96b387"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("4d15115c-d566-4c7c-82a1-de2bec5a7fb9"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("637b0b9a-2391-4cb4-800e-134e6d1899f4"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("7c75eabf-6321-4e92-a72f-64d4a9648cd6"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("8e4d82f4-83b0-4203-8480-00fdd2856f00"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("c43110f7-780c-4a67-9640-62a6eae1bee3"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("cbe761e0-ad74-45f1-b7d2-1e6557735983"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("d802b6bb-41ed-4628-b136-443d4e788730"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("dda23b92-2fb5-461a-8e48-36e52037b2e1"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("de431b6c-135c-4970-b316-eb1e5aa0fa67"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("e60579ef-1111-4a96-a1f0-6bb5f003c2ae"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("ef0ab172-1f45-4514-a86b-72f684509a02"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("fa7e86c6-c31c-47f0-9c7d-6bc99d57efa8"));

            migrationBuilder.DropColumn(
                name: "InvokedMethodId",
                table: "Attributes");

            migrationBuilder.CreateTable(
                name: "AttributeInvokedMethods",
                columns: table => new
                {
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvokedMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeInvokedMethods", x => new { x.AttributeId, x.InvokedMethodId });
                    table.ForeignKey(
                        name: "FK_AttributeInvokedMethods_Attributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeInvokedMethods_InvokedMethods_InvokedMethodId",
                        column: x => x.InvokedMethodId,
                        principalTable: "InvokedMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_AttributeInvokedMethods_InvokedMethodId",
                table: "AttributeInvokedMethods",
                column: "InvokedMethodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributeInvokedMethods");

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

            migrationBuilder.AddColumn<Guid>(
                name: "InvokedMethodId",
                table: "Attributes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InvokedMethodId", "MethodId", "MethodSignatureId", "Name", "ParameterTypeId" },
                values: new object[,]
                {
                    { new Guid("1522d1fa-d8e1-4d6c-be25-5d1bb219eac8"), null, new Guid("44444444-4444-4444-4444-444444444444"), null, "StrToParse", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("2151c622-ae5d-4b58-9091-b943d1ec047c"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("29cfc2d6-ec7e-4689-a762-0821d9874a1d"), null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("9f4a90be-9f1b-4a73-bd92-951c8eee2e28"), null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), null, "StrIsNullOrEmpty", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("ca98f20c-e15b-480c-b809-35953a9aa9bf"), null, new Guid("77777777-7777-7777-7777-777777777777"), null, "StrToCompare", new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("4b67ac8e-f5c6-4117-bb1f-1c21fd96b387"), "Primitive", "nint", 9 },
                    { new Guid("4d15115c-d566-4c7c-82a1-de2bec5a7fb9"), "Primitive", "double", 5 },
                    { new Guid("637b0b9a-2391-4cb4-800e-134e6d1899f4"), "Primitive", "ushort", 14 },
                    { new Guid("7c75eabf-6321-4e92-a72f-64d4a9648cd6"), "Primitive", "decimal", 4 },
                    { new Guid("8e4d82f4-83b0-4203-8480-00fdd2856f00"), "Primitive", "uint", 8 },
                    { new Guid("c43110f7-780c-4a67-9640-62a6eae1bee3"), "Primitive", "sByte", 2 },
                    { new Guid("cbe761e0-ad74-45f1-b7d2-1e6557735983"), "Primitive", "nuint", 10 },
                    { new Guid("d802b6bb-41ed-4628-b136-443d4e788730"), "Primitive", "long", 11 },
                    { new Guid("dda23b92-2fb5-461a-8e48-36e52037b2e1"), "Primitive", "ulong", 12 },
                    { new Guid("de431b6c-135c-4970-b316-eb1e5aa0fa67"), "Primitive", "short", 13 },
                    { new Guid("e60579ef-1111-4a96-a1f0-6bb5f003c2ae"), "Primitive", "char", 3 },
                    { new Guid("ef0ab172-1f45-4514-a86b-72f684509a02"), "Primitive", "float", 6 },
                    { new Guid("fa7e86c6-c31c-47f0-9c7d-6bc99d57efa8"), "Primitive", "byte", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_InvokedMethodId",
                table: "Attributes",
                column: "InvokedMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_InvokedMethods_InvokedMethodId",
                table: "Attributes",
                column: "InvokedMethodId",
                principalTable: "InvokedMethods",
                principalColumn: "Id");
        }
    }
}
