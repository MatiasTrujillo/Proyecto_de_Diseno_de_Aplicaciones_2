using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class MethodsAttributeFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Type_Type_HierarchyClassId",
                table: "Type");

            migrationBuilder.DropIndex(
                name: "IX_Type_HierarchyClassId",
                table: "Type");

            migrationBuilder.DropIndex(
                name: "IX_Type_NameSpaceId",
                table: "Type");

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("77c01e11-60ae-4e89-b17e-f99ae11228e1"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("daca4005-bc53-48ff-abe0-412d5ae8df9e"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("e320583f-1ebb-4483-a234-0fd23a5e2f4b"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("f984e2c8-9ece-4092-b0c2-71747b498b03"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("ffafdef6-b88b-4a61-ab2b-f0bce2260084"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("279a3662-4256-4ed4-807f-d8551526af58"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("2c9c838f-68f0-4262-81ec-5bea323989ad"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("50e1ed0f-3a03-4626-8394-81f94658b845"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("6181877b-c627-4949-9600-08b5ccd81260"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("730fc595-1f4d-4ebe-85d9-9022255f3078"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("967c5970-2c96-4cd4-b8bb-a159a6e0c656"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("978ff7e9-5484-4e7f-85f7-ee1e5d8c15a7"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("a7be503e-036b-42d2-92b2-aeba24c38c55"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("d1b34526-6eba-4644-b41e-311843c32301"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("eadf98eb-da3c-4199-8189-9afed15849ba"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("ee450cc6-2f48-41cf-abd9-95339273cba8"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("f3052a3d-e01a-46cd-a8df-ed2780664ca4"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("fba590b0-ebdf-4972-b415-954af3c09d7b"));

            migrationBuilder.DropColumn(
                name: "HierarchyClassId",
                table: "Type");

            migrationBuilder.DropColumn(
                name: "NameSpaceId",
                table: "Type");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MethodSignatures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InvokedMethodId", "MethodId", "MethodSignatureId", "Name", "ParameterTypeId" },
                values: new object[,]
                {
                    { new Guid("35ea830d-acc8-45e3-8584-beb3c0c6d3b6"), null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("63e6eed7-9722-4c63-8554-9d6d247cad0e"), null, new Guid("77777777-7777-7777-7777-777777777777"), null, "StrToCompare", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("7523e310-a61a-48dc-9f98-3e3785350a84"), null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), null, "StrIsNullOrEmpty", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("a0c5d303-9eca-4ef0-9497-6699124200bf"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("d9636bf9-82c8-4d09-92e8-e4179f1988aa"), null, new Guid("44444444-4444-4444-4444-444444444444"), null, "StrToParse", new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("24da5399-1132-47d6-b303-bd30e03e6a38"), "Primitive", "long", 11 },
                    { new Guid("28fd5f3e-9175-4f2f-9f51-b2aaa0a256b5"), "Primitive", "decimal", 4 },
                    { new Guid("3ec4e730-348e-48d9-99aa-06312a4b221c"), "Primitive", "ushort", 14 },
                    { new Guid("3edc4e39-a9a7-4100-9a66-55d9b205251e"), "Primitive", "uint", 8 },
                    { new Guid("4b8b0598-fc5a-4d51-b621-631f705f5b72"), "Primitive", "sByte", 2 },
                    { new Guid("502e3386-c33d-4ca6-a6da-da0baa91a74a"), "Primitive", "byte", 1 },
                    { new Guid("65d8efcd-fccc-4ecf-a13b-9c3bfdc425a5"), "Primitive", "nint", 9 },
                    { new Guid("699584e0-3c97-4f14-aca5-49df295f7662"), "Primitive", "nuint", 10 },
                    { new Guid("8967e2f5-e049-4046-8190-8aee49a6ece1"), "Primitive", "short", 13 },
                    { new Guid("aac6a47f-33d2-4847-8aaf-ba175692cfda"), "Primitive", "float", 6 },
                    { new Guid("bfea30de-69c3-4675-b5c1-5752715a7f27"), "Primitive", "char", 3 },
                    { new Guid("e0ccf6e4-7d36-4c32-b74f-cfa9a3b1781f"), "Primitive", "double", 5 },
                    { new Guid("eb3f43bd-7f57-40a2-8f27-de26a02f6819"), "Primitive", "ulong", 12 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("35ea830d-acc8-45e3-8584-beb3c0c6d3b6"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("63e6eed7-9722-4c63-8554-9d6d247cad0e"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("7523e310-a61a-48dc-9f98-3e3785350a84"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("a0c5d303-9eca-4ef0-9497-6699124200bf"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("d9636bf9-82c8-4d09-92e8-e4179f1988aa"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("24da5399-1132-47d6-b303-bd30e03e6a38"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("28fd5f3e-9175-4f2f-9f51-b2aaa0a256b5"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("3ec4e730-348e-48d9-99aa-06312a4b221c"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("3edc4e39-a9a7-4100-9a66-55d9b205251e"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("4b8b0598-fc5a-4d51-b621-631f705f5b72"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("502e3386-c33d-4ca6-a6da-da0baa91a74a"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("65d8efcd-fccc-4ecf-a13b-9c3bfdc425a5"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("699584e0-3c97-4f14-aca5-49df295f7662"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("8967e2f5-e049-4046-8190-8aee49a6ece1"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("aac6a47f-33d2-4847-8aaf-ba175692cfda"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("bfea30de-69c3-4675-b5c1-5752715a7f27"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("e0ccf6e4-7d36-4c32-b74f-cfa9a3b1781f"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("eb3f43bd-7f57-40a2-8f27-de26a02f6819"));

            migrationBuilder.AddColumn<Guid>(
                name: "HierarchyClassId",
                table: "Type",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NameSpaceId",
                table: "Type",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MethodSignatures",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InvokedMethodId", "MethodId", "MethodSignatureId", "Name", "ParameterTypeId" },
                values: new object[,]
                {
                    { new Guid("77c01e11-60ae-4e89-b17e-f99ae11228e1"), null, new Guid("44444444-4444-4444-4444-444444444444"), null, "StrToParse", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("daca4005-bc53-48ff-abe0-412d5ae8df9e"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("e320583f-1ebb-4483-a234-0fd23a5e2f4b"), null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), null, "StrIsNullOrEmpty", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("f984e2c8-9ece-4092-b0c2-71747b498b03"), null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("ffafdef6-b88b-4a61-ab2b-f0bce2260084"), null, new Guid("77777777-7777-7777-7777-777777777777"), null, "StrToCompare", new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.UpdateData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                column: "HierarchyClassId",
                value: null);

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("279a3662-4256-4ed4-807f-d8551526af58"), "Primitive", "ushort", 14 },
                    { new Guid("2c9c838f-68f0-4262-81ec-5bea323989ad"), "Primitive", "short", 13 },
                    { new Guid("50e1ed0f-3a03-4626-8394-81f94658b845"), "Primitive", "long", 11 },
                    { new Guid("6181877b-c627-4949-9600-08b5ccd81260"), "Primitive", "decimal", 4 },
                    { new Guid("730fc595-1f4d-4ebe-85d9-9022255f3078"), "Primitive", "double", 5 },
                    { new Guid("967c5970-2c96-4cd4-b8bb-a159a6e0c656"), "Primitive", "uint", 8 },
                    { new Guid("978ff7e9-5484-4e7f-85f7-ee1e5d8c15a7"), "Primitive", "byte", 1 },
                    { new Guid("a7be503e-036b-42d2-92b2-aeba24c38c55"), "Primitive", "ulong", 12 },
                    { new Guid("d1b34526-6eba-4644-b41e-311843c32301"), "Primitive", "nuint", 10 },
                    { new Guid("eadf98eb-da3c-4199-8189-9afed15849ba"), "Primitive", "char", 3 },
                    { new Guid("ee450cc6-2f48-41cf-abd9-95339273cba8"), "Primitive", "sByte", 2 },
                    { new Guid("f3052a3d-e01a-46cd-a8df-ed2780664ca4"), "Primitive", "float", 6 },
                    { new Guid("fba590b0-ebdf-4972-b415-954af3c09d7b"), "Primitive", "nint", 9 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Type_HierarchyClassId",
                table: "Type",
                column: "HierarchyClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Type_NameSpaceId",
                table: "Type",
                column: "NameSpaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Type_Type_HierarchyClassId",
                table: "Type",
                column: "HierarchyClassId",
                principalTable: "Type",
                principalColumn: "Id");
        }
    }
}
