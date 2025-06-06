using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class MethodsLocalVariableIdSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LocalVariables",
                keyColumn: "Id",
                keyValue: new Guid("8ed66907-8061-49c0-92a6-a747c611c887"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("0a30cce3-9338-429d-8da0-0b2afa92526b"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("21cf5ae5-ddde-46ae-b865-b29b496c51ad"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("32be8210-bcee-4649-b376-c7386340fe67"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("51abf6c6-e6b4-4b39-b696-b26eed0f7bec"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("6722e53e-ea6c-4dda-9ccf-ed24ad1fcee6"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("699bd17c-adb9-421b-a95d-e579607fa18d"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("6b442f52-444f-4574-a7c5-5b1fc44d49af"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("6ee2c602-b989-4ad7-bb48-31b16e585c50"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("89d40aa6-9c58-4c00-ba3c-a6841d31f3e8"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("a7687be7-f036-4083-8ece-532b506ec9dc"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("c4fd3b60-b7e1-4359-bfb4-dcd52bda819d"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("dbcc1253-ea02-42f3-b555-81ea0bd44596"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("e3e79080-8f5e-4f20-ac64-75a77e91e915"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("452b69c4-9a56-49e9-8c12-1a3f942662df"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("7d9798a5-e2aa-4866-8886-911750604eb2"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("92ba75ca-b547-4825-9a85-477c6b0a33e4"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("d97b4b6a-431c-49e6-b187-5a2a11126b9f"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("f61ec37a-cedb-4bfb-8199-58b59ed0774c"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("4c695f2b-5dc3-474e-80aa-d4485130392b"));

            migrationBuilder.InsertData(
                table: "NamespaceComponents",
                columns: new[] { "Id", "Discriminator", "Name", "NameSpaceId", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("127c8a5f-0370-417a-9220-7901cf7ad6d7"), "Primitive", "nuint", null, 10 },
                    { new Guid("1aee60db-8f6f-4ca5-bf62-9736253422f5"), "Primitive", "long", null, 11 },
                    { new Guid("1c7e1dec-28e6-4cd7-a7b7-15c4232082a2"), "Primitive", "ulong", null, 12 },
                    { new Guid("37dda331-1923-4479-98d5-325a8db587d3"), "Primitive", "float", null, 6 },
                    { new Guid("3aeee932-365d-45c8-aca1-7f195eb04419"), "Primitive", "ushort", null, 14 },
                    { new Guid("3cc21c08-495e-4a5d-b96b-6cd8acddf139"), "Primitive", "char", null, 3 },
                    { new Guid("47036309-271d-4b59-9a74-dde0e40eef9d"), "Primitive", "decimal", null, 4 },
                    { new Guid("648d8659-3f6e-4b7d-bdb3-b62bc5b3fb87"), "Primitive", "short", null, 13 },
                    { new Guid("8b6700a4-4186-4c53-8a46-7c3268b1e7ea"), "Primitive", "sByte", null, 2 },
                    { new Guid("d1d82d13-8e92-4fab-93ae-458863876cfe"), "Primitive", "int", null, 7 },
                    { new Guid("e58a1145-2aa6-4d12-97f1-61352242f2a5"), "Primitive", "nint", null, 9 },
                    { new Guid("f8a2ab40-f5ce-4247-8231-ff52605a0db4"), "Primitive", "double", null, 5 },
                    { new Guid("fa0829a3-b682-4157-8466-156c696af6f3"), "Primitive", "uint", null, 8 },
                    { new Guid("fc5c4b1e-e4e5-46ab-8dc4-f5b28dc5b684"), "Primitive", "byte", null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InstanceTypeId", "InvokedMethodId", "MethodId", "MethodSignatureId", "Name", "ReferenceTypeId" },
                values: new object[,]
                {
                    { new Guid("3f24c220-6c8a-4657-8e33-29f4229aff78"), null, null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), null, "StrIsNullOrEmpty", null },
                    { new Guid("43d59fa6-d0ce-4424-ba20-5824b656289a"), null, null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, "Obj", null },
                    { new Guid("4e49fb8f-221d-4f5a-a0ba-35ea63d7f578"), null, null, new Guid("77777777-7777-7777-7777-777777777777"), null, "StrToCompare", null },
                    { new Guid("66f74701-c12d-4ded-8840-1c960538c6c8"), null, null, new Guid("44444444-4444-4444-4444-444444444444"), null, "StrToParse", null },
                    { new Guid("dd6a0fbd-444d-4724-94e7-5184d6933c65"), null, null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), null, "Obj", null }
                });

            migrationBuilder.InsertData(
                table: "LocalVariables",
                columns: new[] { "Id", "InstanceTypeId", "InvokedMethodId", "MethodId", "Name", "ReferenceTypeId" },
                values: new object[] { new Guid("d74b63f7-6bcc-4ed9-af00-c998c0d1e14a"), new Guid("d1d82d13-8e92-4fab-93ae-458863876cfe"), null, null, "counter", new Guid("d1d82d13-8e92-4fab-93ae-458863876cfe") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LocalVariables",
                keyColumn: "Id",
                keyValue: new Guid("d74b63f7-6bcc-4ed9-af00-c998c0d1e14a"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("127c8a5f-0370-417a-9220-7901cf7ad6d7"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("1aee60db-8f6f-4ca5-bf62-9736253422f5"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("1c7e1dec-28e6-4cd7-a7b7-15c4232082a2"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("37dda331-1923-4479-98d5-325a8db587d3"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("3aeee932-365d-45c8-aca1-7f195eb04419"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("3cc21c08-495e-4a5d-b96b-6cd8acddf139"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("47036309-271d-4b59-9a74-dde0e40eef9d"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("648d8659-3f6e-4b7d-bdb3-b62bc5b3fb87"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("8b6700a4-4186-4c53-8a46-7c3268b1e7ea"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("e58a1145-2aa6-4d12-97f1-61352242f2a5"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("f8a2ab40-f5ce-4247-8231-ff52605a0db4"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("fa0829a3-b682-4157-8466-156c696af6f3"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("fc5c4b1e-e4e5-46ab-8dc4-f5b28dc5b684"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("3f24c220-6c8a-4657-8e33-29f4229aff78"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("43d59fa6-d0ce-4424-ba20-5824b656289a"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("4e49fb8f-221d-4f5a-a0ba-35ea63d7f578"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("66f74701-c12d-4ded-8840-1c960538c6c8"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("dd6a0fbd-444d-4724-94e7-5184d6933c65"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("d1d82d13-8e92-4fab-93ae-458863876cfe"));

            migrationBuilder.InsertData(
                table: "NamespaceComponents",
                columns: new[] { "Id", "Discriminator", "Name", "NameSpaceId", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("0a30cce3-9338-429d-8da0-0b2afa92526b"), "Primitive", "long", null, 11 },
                    { new Guid("21cf5ae5-ddde-46ae-b865-b29b496c51ad"), "Primitive", "char", null, 3 },
                    { new Guid("32be8210-bcee-4649-b376-c7386340fe67"), "Primitive", "short", null, 13 },
                    { new Guid("4c695f2b-5dc3-474e-80aa-d4485130392b"), "Primitive", "int", null, 7 },
                    { new Guid("51abf6c6-e6b4-4b39-b696-b26eed0f7bec"), "Primitive", "decimal", null, 4 },
                    { new Guid("6722e53e-ea6c-4dda-9ccf-ed24ad1fcee6"), "Primitive", "ushort", null, 14 },
                    { new Guid("699bd17c-adb9-421b-a95d-e579607fa18d"), "Primitive", "ulong", null, 12 },
                    { new Guid("6b442f52-444f-4574-a7c5-5b1fc44d49af"), "Primitive", "double", null, 5 },
                    { new Guid("6ee2c602-b989-4ad7-bb48-31b16e585c50"), "Primitive", "sByte", null, 2 },
                    { new Guid("89d40aa6-9c58-4c00-ba3c-a6841d31f3e8"), "Primitive", "nuint", null, 10 },
                    { new Guid("a7687be7-f036-4083-8ece-532b506ec9dc"), "Primitive", "uint", null, 8 },
                    { new Guid("c4fd3b60-b7e1-4359-bfb4-dcd52bda819d"), "Primitive", "float", null, 6 },
                    { new Guid("dbcc1253-ea02-42f3-b555-81ea0bd44596"), "Primitive", "nint", null, 9 },
                    { new Guid("e3e79080-8f5e-4f20-ac64-75a77e91e915"), "Primitive", "byte", null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InstanceTypeId", "InvokedMethodId", "MethodId", "MethodSignatureId", "Name", "ReferenceTypeId" },
                values: new object[,]
                {
                    { new Guid("452b69c4-9a56-49e9-8c12-1a3f942662df"), null, null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, "Obj", null },
                    { new Guid("7d9798a5-e2aa-4866-8886-911750604eb2"), null, null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), null, "Obj", null },
                    { new Guid("92ba75ca-b547-4825-9a85-477c6b0a33e4"), null, null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), null, "StrIsNullOrEmpty", null },
                    { new Guid("d97b4b6a-431c-49e6-b187-5a2a11126b9f"), null, null, new Guid("77777777-7777-7777-7777-777777777777"), null, "StrToCompare", null },
                    { new Guid("f61ec37a-cedb-4bfb-8199-58b59ed0774c"), null, null, new Guid("44444444-4444-4444-4444-444444444444"), null, "StrToParse", null }
                });

            migrationBuilder.InsertData(
                table: "LocalVariables",
                columns: new[] { "Id", "InstanceTypeId", "InvokedMethodId", "MethodId", "Name", "ReferenceTypeId" },
                values: new object[] { new Guid("8ed66907-8061-49c0-92a6-a747c611c887"), new Guid("4c695f2b-5dc3-474e-80aa-d4485130392b"), null, null, "counter", new Guid("4c695f2b-5dc3-474e-80aa-d4485130392b") });
        }
    }
}
