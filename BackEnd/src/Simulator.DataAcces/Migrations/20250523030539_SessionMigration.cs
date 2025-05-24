using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class SessionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("3d073d3c-8fc1-4d4b-9147-38bc38619d28"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("685976a3-20e7-41bb-9143-7284bc656978"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("7c6f7473-10ad-4771-8650-eda8530d8884"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("ee60bb01-e119-4809-9961-1c6ddbb66498"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("f969e93c-4560-48b6-9621-c0e25bf2c0c9"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("0a8e3855-efac-4f58-8a97-324bc2ae16a8"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("137f90d9-cf8d-4d36-bb24-8a107c70bbc3"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("3b5e8bf2-8dc4-48c9-b3bf-458023df6aaa"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("74d92164-3f4c-4aae-901d-b0fa36f9460d"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("81bcfb9a-9f6c-4e2c-8025-c62689333b0c"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("83940b90-6481-4ea0-a7f1-ab37c9125446"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("8927458e-feed-443c-89c3-45642074ec24"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("9371293e-f09d-4d57-8527-b2c250f1feec"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("9f4a7b1f-1a3e-45f1-b342-f8d6252f2307"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("b51a43b9-a0d4-4055-8866-e8e78234f507"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("bf1cf04b-34c7-441e-8f44-44b3c2189df2"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("d369863a-7fad-4cef-bbfb-30722b91c3bb"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("dffab210-8d74-47f4-8917-e908b710a320"));

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Token = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Token);
                });

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
                table: "Sessions",
                column: "Token",
                values: new object[]
                {
                    new Guid("2070a73f-97a1-4365-a231-dca969fb52fb"),
                    new Guid("231ecdac-9680-4772-a95a-29abdfd4ad5f"),
                    new Guid("5c8f91eb-5bd1-4706-96ca-cfcec81473b8"),
                    new Guid("66d9652f-3585-4982-9ab7-8d8159197147"),
                    new Guid("675c1f61-9dfa-4ead-bae4-dc432ee2bd32"),
                    new Guid("756ec724-3e51-4e0d-a857-cceee846a5f6"),
                    new Guid("a4a20350-e7e1-4080-b4da-75e875af6b61"),
                    new Guid("b142d2d6-ac0c-4af1-b272-c03e431555f3"),
                    new Guid("f6e32298-4362-4ef1-9e6a-e2dfc9e58a47")
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sessions");

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

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InvokedMethodId", "MethodId", "MethodSignatureId", "Name", "ParameterTypeId" },
                values: new object[,]
                {
                    { new Guid("3d073d3c-8fc1-4d4b-9147-38bc38619d28"), null, new Guid("44444444-4444-4444-4444-444444444444"), null, "StrToParse", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("685976a3-20e7-41bb-9143-7284bc656978"), null, new Guid("77777777-7777-7777-7777-777777777777"), null, "StrToCompare", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("7c6f7473-10ad-4771-8650-eda8530d8884"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("ee60bb01-e119-4809-9961-1c6ddbb66498"), null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("f969e93c-4560-48b6-9621-c0e25bf2c0c9"), null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), null, "StrIsNullOrEmpty", new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("0a8e3855-efac-4f58-8a97-324bc2ae16a8"), "Primitive", "decimal", 4 },
                    { new Guid("137f90d9-cf8d-4d36-bb24-8a107c70bbc3"), "Primitive", "sByte", 2 },
                    { new Guid("3b5e8bf2-8dc4-48c9-b3bf-458023df6aaa"), "Primitive", "long", 11 },
                    { new Guid("74d92164-3f4c-4aae-901d-b0fa36f9460d"), "Primitive", "uint", 8 },
                    { new Guid("81bcfb9a-9f6c-4e2c-8025-c62689333b0c"), "Primitive", "byte", 1 },
                    { new Guid("83940b90-6481-4ea0-a7f1-ab37c9125446"), "Primitive", "double", 5 },
                    { new Guid("8927458e-feed-443c-89c3-45642074ec24"), "Primitive", "float", 6 },
                    { new Guid("9371293e-f09d-4d57-8527-b2c250f1feec"), "Primitive", "short", 13 },
                    { new Guid("9f4a7b1f-1a3e-45f1-b342-f8d6252f2307"), "Primitive", "ulong", 12 },
                    { new Guid("b51a43b9-a0d4-4055-8866-e8e78234f507"), "Primitive", "char", 3 },
                    { new Guid("bf1cf04b-34c7-441e-8f44-44b3c2189df2"), "Primitive", "nint", 9 },
                    { new Guid("d369863a-7fad-4cef-bbfb-30722b91c3bb"), "Primitive", "nuint", 10 },
                    { new Guid("dffab210-8d74-47f4-8917-e908b710a320"), "Primitive", "ushort", 14 }
                });
        }
    }
}
