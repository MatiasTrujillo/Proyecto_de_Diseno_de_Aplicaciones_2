using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class InstanceTypeMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parameters_NamespaceComponents_ParameterTypeId",
                table: "Parameters");

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("122c882a-b7e4-4c7c-82d9-80974afcc358"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("18f5cae7-599d-473c-952c-856e268f693a"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("2d38a6c1-430a-4346-a6af-2efbb0d7392a"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("35552f14-fb7a-4968-85bf-319e754bd390"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("3be807b5-f54b-49e0-8cc0-ad2f49055629"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("66a801ba-55b0-4f76-8fa9-9391f914f392"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("6a670f69-5da1-408c-9ed6-03cc57b2610c"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("709de1df-fc15-46e8-9257-4d5aa3f07744"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("92bdfdf2-1832-41a4-aa95-e9c39a9c3215"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("bae5b082-d38f-4cba-acbb-fa01f139570f"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("c8ee3304-6223-4d81-8bf8-2960e9db7e77"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("d62a102b-fcdd-4e73-9ae5-aba7b3ef454b"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("e08cfe27-74a9-4d27-b662-36f7d3f1d1de"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("11a76645-b7e2-4f3e-beee-064135a188ca"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("381e6a90-5397-4488-a1e9-9cc2863c2649"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("72f54d20-efd4-44dd-ba70-809b4eb7fd67"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("b2f13077-64ab-4613-9196-4c03356fe867"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("feb4268c-72b4-4673-b557-2384a162034b"));

            migrationBuilder.RenameColumn(
                name: "ParameterTypeId",
                table: "Parameters",
                newName: "ReferenceTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Parameters_ParameterTypeId",
                table: "Parameters",
                newName: "IX_Parameters_ReferenceTypeId");

            migrationBuilder.AddColumn<Guid>(
                name: "InstanceTypeId",
                table: "Parameters",
                type: "uniqueidentifier",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_InstanceTypeId",
                table: "Parameters",
                column: "InstanceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parameters_NamespaceComponents_InstanceTypeId",
                table: "Parameters",
                column: "InstanceTypeId",
                principalTable: "NamespaceComponents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parameters_NamespaceComponents_ReferenceTypeId",
                table: "Parameters",
                column: "ReferenceTypeId",
                principalTable: "NamespaceComponents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parameters_NamespaceComponents_InstanceTypeId",
                table: "Parameters");

            migrationBuilder.DropForeignKey(
                name: "FK_Parameters_NamespaceComponents_ReferenceTypeId",
                table: "Parameters");

            migrationBuilder.DropIndex(
                name: "IX_Parameters_InstanceTypeId",
                table: "Parameters");

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

            migrationBuilder.DropColumn(
                name: "InstanceTypeId",
                table: "Parameters");

            migrationBuilder.RenameColumn(
                name: "ReferenceTypeId",
                table: "Parameters",
                newName: "ParameterTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Parameters_ReferenceTypeId",
                table: "Parameters",
                newName: "IX_Parameters_ParameterTypeId");

            migrationBuilder.InsertData(
                table: "NamespaceComponents",
                columns: new[] { "Id", "Discriminator", "Name", "NameSpaceId", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("122c882a-b7e4-4c7c-82d9-80974afcc358"), "Primitive", "nint", null, 9 },
                    { new Guid("18f5cae7-599d-473c-952c-856e268f693a"), "Primitive", "char", null, 3 },
                    { new Guid("2d38a6c1-430a-4346-a6af-2efbb0d7392a"), "Primitive", "sByte", null, 2 },
                    { new Guid("35552f14-fb7a-4968-85bf-319e754bd390"), "Primitive", "decimal", null, 4 },
                    { new Guid("3be807b5-f54b-49e0-8cc0-ad2f49055629"), "Primitive", "ulong", null, 12 },
                    { new Guid("66a801ba-55b0-4f76-8fa9-9391f914f392"), "Primitive", "long", null, 11 },
                    { new Guid("6a670f69-5da1-408c-9ed6-03cc57b2610c"), "Primitive", "byte", null, 1 },
                    { new Guid("709de1df-fc15-46e8-9257-4d5aa3f07744"), "Primitive", "ushort", null, 14 },
                    { new Guid("92bdfdf2-1832-41a4-aa95-e9c39a9c3215"), "Primitive", "nuint", null, 10 },
                    { new Guid("bae5b082-d38f-4cba-acbb-fa01f139570f"), "Primitive", "short", null, 13 },
                    { new Guid("c8ee3304-6223-4d81-8bf8-2960e9db7e77"), "Primitive", "double", null, 5 },
                    { new Guid("d62a102b-fcdd-4e73-9ae5-aba7b3ef454b"), "Primitive", "uint", null, 8 },
                    { new Guid("e08cfe27-74a9-4d27-b662-36f7d3f1d1de"), "Primitive", "float", null, 6 }
                });

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InvokedMethodId", "MethodId", "MethodSignatureId", "Name", "ParameterTypeId" },
                values: new object[,]
                {
                    { new Guid("11a76645-b7e2-4f3e-beee-064135a188ca"), null, new Guid("44444444-4444-4444-4444-444444444444"), null, "StrToParse", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("381e6a90-5397-4488-a1e9-9cc2863c2649"), null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), null, "StrIsNullOrEmpty", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("72f54d20-efd4-44dd-ba70-809b4eb7fd67"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("b2f13077-64ab-4613-9196-4c03356fe867"), null, new Guid("77777777-7777-7777-7777-777777777777"), null, "StrToCompare", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("feb4268c-72b4-4673-b557-2384a162034b"), null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Parameters_NamespaceComponents_ParameterTypeId",
                table: "Parameters",
                column: "ParameterTypeId",
                principalTable: "NamespaceComponents",
                principalColumn: "Id");
        }
    }
}
