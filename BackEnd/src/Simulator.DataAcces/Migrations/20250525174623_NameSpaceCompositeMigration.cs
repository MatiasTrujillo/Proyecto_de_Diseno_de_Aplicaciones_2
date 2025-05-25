using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class NameSpaceCompositeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Type_NameSpaces_NameSpaceId",
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
                name: "NameSpaceId",
                table: "Type");

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InvokedMethodId", "MethodId", "MethodSignatureId", "Name", "ParameterTypeId" },
                values: new object[,]
                {
                    { new Guid("613d354b-fbf9-4c01-bbf9-28e3974f9783"), null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("732c991a-1bd8-43d2-b29f-1e71bf64840d"), null, new Guid("77777777-7777-7777-7777-777777777777"), null, "StrToCompare", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("d921a234-1dd7-48ee-83c2-4a2d17715294"), null, new Guid("44444444-4444-4444-4444-444444444444"), null, "StrToParse", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("e630a35c-a28b-45ac-af1a-8e1fd57278bd"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("ea1d64db-6288-49e5-a72f-0822bf93be66"), null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), null, "StrIsNullOrEmpty", new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("05e12e7f-ca1a-4f75-b239-c2ece8a11369"), "Primitive", "float", 6 },
                    { new Guid("1176ad05-b059-4e0e-bc64-2da6327c7607"), "Primitive", "sByte", 2 },
                    { new Guid("2d2120c5-b899-452a-b24b-1f2292ea6ff9"), "Primitive", "ulong", 12 },
                    { new Guid("36109cd6-a856-4081-9e83-3208b6164f9b"), "Primitive", "double", 5 },
                    { new Guid("5021a8b1-cc64-4219-b2c0-c1d6fae05cfa"), "Primitive", "uint", 8 },
                    { new Guid("52f44093-dd42-42fa-a245-a82bb0840d84"), "Primitive", "nint", 9 },
                    { new Guid("5f2aa93a-8478-4600-bd06-c77e137c5feb"), "Primitive", "ushort", 14 },
                    { new Guid("91792a74-b057-4064-9434-302377bca8ad"), "Primitive", "short", 13 },
                    { new Guid("95096bc2-8c4d-49ba-9810-de472ab3217c"), "Primitive", "byte", 1 },
                    { new Guid("d01956ad-06b1-4031-9797-7d95510cf5a8"), "Primitive", "long", 11 },
                    { new Guid("e5626f4f-6563-4262-8345-07a642833677"), "Primitive", "char", 3 },
                    { new Guid("ed15c2c9-9220-4d6e-ab6f-add5852a9805"), "Primitive", "decimal", 4 },
                    { new Guid("ed9fe44b-fe4c-4018-b80b-fa0946c88de7"), "Primitive", "nuint", 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("613d354b-fbf9-4c01-bbf9-28e3974f9783"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("732c991a-1bd8-43d2-b29f-1e71bf64840d"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("d921a234-1dd7-48ee-83c2-4a2d17715294"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("e630a35c-a28b-45ac-af1a-8e1fd57278bd"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("ea1d64db-6288-49e5-a72f-0822bf93be66"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("05e12e7f-ca1a-4f75-b239-c2ece8a11369"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("1176ad05-b059-4e0e-bc64-2da6327c7607"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("2d2120c5-b899-452a-b24b-1f2292ea6ff9"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("36109cd6-a856-4081-9e83-3208b6164f9b"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("5021a8b1-cc64-4219-b2c0-c1d6fae05cfa"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("52f44093-dd42-42fa-a245-a82bb0840d84"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("5f2aa93a-8478-4600-bd06-c77e137c5feb"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("91792a74-b057-4064-9434-302377bca8ad"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("95096bc2-8c4d-49ba-9810-de472ab3217c"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("d01956ad-06b1-4031-9797-7d95510cf5a8"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("e5626f4f-6563-4262-8345-07a642833677"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("ed15c2c9-9220-4d6e-ab6f-add5852a9805"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("ed9fe44b-fe4c-4018-b80b-fa0946c88de7"));

            migrationBuilder.AddColumn<Guid>(
                name: "NameSpaceId",
                table: "Type",
                type: "uniqueidentifier",
                nullable: true);

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
                column: "NameSpaceId",
                value: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab"));

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
                name: "IX_Type_NameSpaceId",
                table: "Type",
                column: "NameSpaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Type_NameSpaces_NameSpaceId",
                table: "Type",
                column: "NameSpaceId",
                principalTable: "NameSpaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
