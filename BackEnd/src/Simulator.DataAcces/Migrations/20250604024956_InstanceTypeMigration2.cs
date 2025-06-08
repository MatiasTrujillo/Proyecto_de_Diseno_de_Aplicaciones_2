using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class InstanceTypeMigration2 : Migration
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
