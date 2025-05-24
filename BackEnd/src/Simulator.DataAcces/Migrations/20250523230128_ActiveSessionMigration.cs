using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class ActiveSessionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("3bb33e32-6fdc-457f-b69b-5e7f7d742cf6"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("50978c9b-595b-4b88-ab9a-345c5a78fc31"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("52ba5bb0-5295-4ca9-acb6-6f1492e264a9"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("667469ab-2833-4078-8ec2-7a4c7b3edae7"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("b266cb0b-649b-4947-8404-f89aab66c804"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("1047534f-c50c-4908-a1ac-18f6d8f741ed"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("24807890-eb7b-417a-8ff4-719f1edecb88"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("39e20a2f-a3a4-40ae-ae01-2ba64110ee9a"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("497e165c-089b-427f-a0bb-a171674192ae"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("57fc923b-c168-454f-a24b-55a6c72211c7"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("685ac5cd-906d-4986-83f4-0a01a2fa1914"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("7a29f447-e3c6-4a41-a42b-6224ac941703"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("99b9818b-db40-4192-b34a-58470b49c2a4"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("a62546f8-9cab-4f8b-ace9-243e7a551519"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("b877f862-960c-4c2f-ad68-2465eeaaf945"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("be5e2f3d-6009-431c-b88a-544cf9577708"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("f4253064-9dfa-41e0-b2fc-54c539bc54e6"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("f6b7a81c-8562-4050-8133-18c799b9287d"));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Sessions",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
                table: "Sessions",
                keyColumn: "Token",
                keyValue: new Guid("2070a73f-97a1-4365-a231-dca969fb52fb"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Token",
                keyValue: new Guid("231ecdac-9680-4772-a95a-29abdfd4ad5f"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Token",
                keyValue: new Guid("5c8f91eb-5bd1-4706-96ca-cfcec81473b8"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Token",
                keyValue: new Guid("66d9652f-3585-4982-9ab7-8d8159197147"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Token",
                keyValue: new Guid("675c1f61-9dfa-4ead-bae4-dc432ee2bd32"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Token",
                keyValue: new Guid("756ec724-3e51-4e0d-a857-cceee846a5f6"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Token",
                keyValue: new Guid("a4a20350-e7e1-4080-b4da-75e875af6b61"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Token",
                keyValue: new Guid("b142d2d6-ac0c-4af1-b272-c03e431555f3"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Token",
                keyValue: new Guid("f6e32298-4362-4ef1-9e6a-e2dfc9e58a47"),
                column: "IsActive",
                value: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "IsActive",
                table: "Sessions");

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InvokedMethodId", "MethodId", "MethodSignatureId", "Name", "ParameterTypeId" },
                values: new object[,]
                {
                    { new Guid("3bb33e32-6fdc-457f-b69b-5e7f7d742cf6"), null, new Guid("44444444-4444-4444-4444-444444444444"), null, "StrToParse", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("50978c9b-595b-4b88-ab9a-345c5a78fc31"), null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("52ba5bb0-5295-4ca9-acb6-6f1492e264a9"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("667469ab-2833-4078-8ec2-7a4c7b3edae7"), null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), null, "StrIsNullOrEmpty", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("b266cb0b-649b-4947-8404-f89aab66c804"), null, new Guid("77777777-7777-7777-7777-777777777777"), null, "StrToCompare", new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("1047534f-c50c-4908-a1ac-18f6d8f741ed"), "Primitive", "char", 3 },
                    { new Guid("24807890-eb7b-417a-8ff4-719f1edecb88"), "Primitive", "byte", 1 },
                    { new Guid("39e20a2f-a3a4-40ae-ae01-2ba64110ee9a"), "Primitive", "double", 5 },
                    { new Guid("497e165c-089b-427f-a0bb-a171674192ae"), "Primitive", "float", 6 },
                    { new Guid("57fc923b-c168-454f-a24b-55a6c72211c7"), "Primitive", "ulong", 12 },
                    { new Guid("685ac5cd-906d-4986-83f4-0a01a2fa1914"), "Primitive", "nuint", 10 },
                    { new Guid("7a29f447-e3c6-4a41-a42b-6224ac941703"), "Primitive", "ushort", 14 },
                    { new Guid("99b9818b-db40-4192-b34a-58470b49c2a4"), "Primitive", "uint", 8 },
                    { new Guid("a62546f8-9cab-4f8b-ace9-243e7a551519"), "Primitive", "decimal", 4 },
                    { new Guid("b877f862-960c-4c2f-ad68-2465eeaaf945"), "Primitive", "short", 13 },
                    { new Guid("be5e2f3d-6009-431c-b88a-544cf9577708"), "Primitive", "long", 11 },
                    { new Guid("f4253064-9dfa-41e0-b2fc-54c539bc54e6"), "Primitive", "nint", 9 },
                    { new Guid("f6b7a81c-8562-4050-8133-18c799b9287d"), "Primitive", "sByte", 2 }
                });
        }
    }
}
