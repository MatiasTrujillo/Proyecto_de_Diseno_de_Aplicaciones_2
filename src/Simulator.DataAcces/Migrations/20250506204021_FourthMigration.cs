using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class FourthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parameters_Methods_MethodId",
                table: "Parameters");

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
                values: new object[] { new Guid("81f6a054-2f9d-4e9d-9ad4-89c39281ab4d"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("1015ca9a-46d5-440f-9c5c-78c3671e4dc1"), "Primitive", "byte", 1 },
                    { new Guid("3ef43e9a-cb79-4b4a-af41-15cd6bc4c008"), "Primitive", "float", 6 },
                    { new Guid("47fc0a91-8d59-43ad-997b-25c9dda9291c"), "Primitive", "sByte", 2 },
                    { new Guid("4dc09f4b-2c79-4322-aec7-b8e5685908fe"), "Primitive", "uint", 8 },
                    { new Guid("5c4e72b8-f5c4-4ec2-9d64-946ade48dd3d"), "Primitive", "char", 3 },
                    { new Guid("6d5e646f-2719-4fa5-9af0-d029f23760b6"), "Primitive", "nint", 9 },
                    { new Guid("7dba885d-0202-47e6-bb0f-8cc3bfd9a34d"), "Primitive", "double", 5 },
                    { new Guid("84d923df-5e42-4476-aacf-96eb2cfec694"), "Primitive", "short", 13 },
                    { new Guid("a5aa6983-8395-4125-b11f-642d2dbb0087"), "Primitive", "decimal", 4 },
                    { new Guid("b13b4584-31b8-4a09-89cd-24f5d030fe25"), "Primitive", "nuint", 10 },
                    { new Guid("b20ec442-257a-4b71-92f5-3fd122b33085"), "Primitive", "ulong", 12 },
                    { new Guid("d78eeca6-90ea-4bc4-94b0-c0e2109beac7"), "Primitive", "ushort", 14 },
                    { new Guid("da843033-4c82-4a55-a96e-73ce8bc9bdc4"), "Primitive", "long", 11 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Parameters_Methods_MethodId",
                table: "Parameters",
                column: "MethodId",
                principalTable: "Methods",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parameters_Methods_MethodId",
                table: "Parameters");

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("81f6a054-2f9d-4e9d-9ad4-89c39281ab4d"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("1015ca9a-46d5-440f-9c5c-78c3671e4dc1"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("3ef43e9a-cb79-4b4a-af41-15cd6bc4c008"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("47fc0a91-8d59-43ad-997b-25c9dda9291c"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("4dc09f4b-2c79-4322-aec7-b8e5685908fe"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("5c4e72b8-f5c4-4ec2-9d64-946ade48dd3d"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("6d5e646f-2719-4fa5-9af0-d029f23760b6"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("7dba885d-0202-47e6-bb0f-8cc3bfd9a34d"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("84d923df-5e42-4476-aacf-96eb2cfec694"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("a5aa6983-8395-4125-b11f-642d2dbb0087"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("b13b4584-31b8-4a09-89cd-24f5d030fe25"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("b20ec442-257a-4b71-92f5-3fd122b33085"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("d78eeca6-90ea-4bc4-94b0-c0e2109beac7"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("da843033-4c82-4a55-a96e-73ce8bc9bdc4"));

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

            migrationBuilder.AddForeignKey(
                name: "FK_Parameters_Methods_MethodId",
                table: "Parameters",
                column: "MethodId",
                principalTable: "Methods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
