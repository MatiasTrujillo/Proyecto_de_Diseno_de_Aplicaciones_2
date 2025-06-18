using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class MethodsLocalVariableId4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("39346504-66f2-49bd-a13f-b5f53b2af024"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("3a84315e-6fd8-431a-953a-14fe12a765ba"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("3bfd4bdc-828d-476e-a323-8cb189f2c0cd"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("417ca2af-c78d-467d-85eb-f78d1833610a"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("5dea3690-3624-48aa-837b-e9272e0ed0de"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("a62ffaf2-4844-4156-af25-c68ff2799fa5"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("a7162b2d-9506-4219-9b4b-56e43073b4aa"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("c189aaa4-2fba-4407-87d7-3419e6a47e01"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("c799079c-1c6a-468f-aaff-b2e290fa0849"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("d498fb3b-7efc-4b5a-a975-99b27cfd58b4"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("ec991fcc-feb0-4e01-b8c3-beb603c48f7b"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("f7418126-2e16-4e5a-a3d1-58696bb7d835"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("ffdd78f8-f6aa-43c5-95df-eb5b26a56567"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("4bfa6295-2ad7-4e20-9b2e-d1eec82007f1"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("6a500513-d065-4e3d-9307-0151beaab2fa"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("6da33d2e-f5f6-45e8-a961-969c2f973ac6"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("80d5ed8c-e9b6-4bd6-82da-e2892200ef60"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("9907fdcd-8192-4562-8401-df841964a95e"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { new Guid("39346504-66f2-49bd-a13f-b5f53b2af024"), "Primitive", "byte", null, 1 },
                    { new Guid("3a84315e-6fd8-431a-953a-14fe12a765ba"), "Primitive", "sByte", null, 2 },
                    { new Guid("3bfd4bdc-828d-476e-a323-8cb189f2c0cd"), "Primitive", "char", null, 3 },
                    { new Guid("417ca2af-c78d-467d-85eb-f78d1833610a"), "Primitive", "ulong", null, 12 },
                    { new Guid("5dea3690-3624-48aa-837b-e9272e0ed0de"), "Primitive", "ushort", null, 14 },
                    { new Guid("a62ffaf2-4844-4156-af25-c68ff2799fa5"), "Primitive", "long", null, 11 },
                    { new Guid("a7162b2d-9506-4219-9b4b-56e43073b4aa"), "Primitive", "double", null, 5 },
                    { new Guid("c189aaa4-2fba-4407-87d7-3419e6a47e01"), "Primitive", "float", null, 6 },
                    { new Guid("c799079c-1c6a-468f-aaff-b2e290fa0849"), "Primitive", "nint", null, 9 },
                    { new Guid("d498fb3b-7efc-4b5a-a975-99b27cfd58b4"), "Primitive", "nuint", null, 10 },
                    { new Guid("ec991fcc-feb0-4e01-b8c3-beb603c48f7b"), "Primitive", "decimal", null, 4 },
                    { new Guid("f7418126-2e16-4e5a-a3d1-58696bb7d835"), "Primitive", "uint", null, 8 },
                    { new Guid("ffdd78f8-f6aa-43c5-95df-eb5b26a56567"), "Primitive", "short", null, 13 }
                });

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InstanceTypeId", "InvokedMethodId", "MethodId", "MethodSignatureId", "Name", "ReferenceTypeId" },
                values: new object[,]
                {
                    { new Guid("4bfa6295-2ad7-4e20-9b2e-d1eec82007f1"), null, null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), null, "Obj", null },
                    { new Guid("6a500513-d065-4e3d-9307-0151beaab2fa"), null, null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), null, "StrIsNullOrEmpty", null },
                    { new Guid("6da33d2e-f5f6-45e8-a961-969c2f973ac6"), null, null, new Guid("44444444-4444-4444-4444-444444444444"), null, "StrToParse", null },
                    { new Guid("80d5ed8c-e9b6-4bd6-82da-e2892200ef60"), null, null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, "Obj", null },
                    { new Guid("9907fdcd-8192-4562-8401-df841964a95e"), null, null, new Guid("77777777-7777-7777-7777-777777777777"), null, "StrToCompare", null }
                });
        }
    }
}
