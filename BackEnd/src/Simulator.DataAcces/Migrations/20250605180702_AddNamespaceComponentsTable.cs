using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class AddNamespaceComponentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_NamespaceComponent_ClassId",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_NamespaceComponent_InstanceTypeId",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_NamespaceComponent_ReferenceTypeId",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassParents_NamespaceComponent_ClassId",
                table: "ClassParents");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassParents_NamespaceComponent_ParentId",
                table: "ClassParents");

            migrationBuilder.DropForeignKey(
                name: "FK_InterfaceMethodSignature_NamespaceComponent_InterfaceId",
                table: "InterfaceMethodSignature");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalVariables_NamespaceComponent_InstanceTypeId",
                table: "LocalVariables");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalVariables_NamespaceComponent_ReferenceTypeId",
                table: "LocalVariables");

            migrationBuilder.DropForeignKey(
                name: "FK_Methods_NamespaceComponent_ReturnTypeId",
                table: "Methods");

            migrationBuilder.DropForeignKey(
                name: "FK_MethodSignatures_NamespaceComponent_ReturnTypeId",
                table: "MethodSignatures");

            migrationBuilder.DropForeignKey(
                name: "FK_NamespaceComponent_NamespaceComponent_FatherId",
                table: "NamespaceComponent");

            migrationBuilder.DropForeignKey(
                name: "FK_NamespaceComponent_NamespaceComponent_NameSpaceId",
                table: "NamespaceComponent");

            migrationBuilder.DropForeignKey(
                name: "FK_NamespaceComponent_NamespaceComponent_NameSpaceId1",
                table: "NamespaceComponent");

            migrationBuilder.DropForeignKey(
                name: "FK_NamespaceComponent_NamespaceComponent_NamespaceId",
                table: "NamespaceComponent");

            migrationBuilder.DropForeignKey(
                name: "FK_Parameters_NamespaceComponent_ParameterTypeId",
                table: "Parameters");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeMethod_NamespaceComponent_TypeId",
                table: "TypeMethod");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NamespaceComponent",
                table: "NamespaceComponent");

            migrationBuilder.DropIndex(
                name: "IX_NamespaceComponent_NameSpaceId",
                table: "NamespaceComponent");

            migrationBuilder.DropIndex(
                name: "IX_NamespaceComponent_NameSpaceId1",
                table: "NamespaceComponent");

            migrationBuilder.DeleteData(
                table: "NamespaceComponent",
                keyColumn: "Id",
                keyValue: new Guid("1ca51895-6bff-4e71-9702-39e5d059261d"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponent",
                keyColumn: "Id",
                keyValue: new Guid("2299304b-62ba-4d90-a536-a1d95cfcaf24"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponent",
                keyColumn: "Id",
                keyValue: new Guid("69d6150d-af95-49f1-9128-3aaec961665f"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponent",
                keyColumn: "Id",
                keyValue: new Guid("6eea2a44-da90-4b4e-b497-30522486fc16"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponent",
                keyColumn: "Id",
                keyValue: new Guid("71577f69-e8c4-4166-9ae0-218c50dd2d66"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponent",
                keyColumn: "Id",
                keyValue: new Guid("75f39ab1-eed0-4c16-9672-9d4798a10ce0"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponent",
                keyColumn: "Id",
                keyValue: new Guid("767d82a1-479c-44e0-a0da-8948a8139b0d"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponent",
                keyColumn: "Id",
                keyValue: new Guid("814b9860-30a1-4829-9f35-4deda0a7ecae"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponent",
                keyColumn: "Id",
                keyValue: new Guid("852ed5ab-e77a-4915-8db7-ef58cd8f68b0"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponent",
                keyColumn: "Id",
                keyValue: new Guid("d41b6692-c26e-4874-9023-1d9a02bd3f34"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponent",
                keyColumn: "Id",
                keyValue: new Guid("f19b4b7c-336d-433c-9772-b62ad07b5498"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponent",
                keyColumn: "Id",
                keyValue: new Guid("f7c25d60-80f4-4796-9184-9a0313d9f1d8"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponent",
                keyColumn: "Id",
                keyValue: new Guid("fa8eb4e6-cb9b-4426-976a-a782a24fa55d"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("083e0d51-f540-4967-a1c2-2bad06d4c391"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("0dc17e3c-91ea-4eb3-b25f-c73e5e585dda"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("18c59f83-82a2-4165-9994-1d48f9ac33cb"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("5213252d-8d6d-42ad-80f9-851c9d16e775"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("e9989b6a-df51-4b96-a0cb-ca3c3f29b813"));

            migrationBuilder.DropColumn(
                name: "NameSpaceId",
                table: "NamespaceComponent");

            migrationBuilder.DropColumn(
                name: "NameSpaceId1",
                table: "NamespaceComponent");

            migrationBuilder.RenameTable(
                name: "NamespaceComponent",
                newName: "NamespaceComponents");

            migrationBuilder.RenameColumn(
                name: "NamespaceId",
                table: "NamespaceComponents",
                newName: "NameSpaceId");

            migrationBuilder.RenameColumn(
                name: "Type_Name",
                table: "NamespaceComponents",
                newName: "NameSpace_Name");

            migrationBuilder.RenameIndex(
                name: "IX_NamespaceComponent_NamespaceId",
                table: "NamespaceComponents",
                newName: "IX_NamespaceComponents_NameSpaceId");

            migrationBuilder.RenameIndex(
                name: "IX_NamespaceComponent_FatherId",
                table: "NamespaceComponents",
                newName: "IX_NamespaceComponents_FatherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NamespaceComponents",
                table: "NamespaceComponents",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "Name", "NameSpaceId" },
                values: new object[] { "bool", null });

            migrationBuilder.UpdateData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "Name", "NameSpaceId" },
                values: new object[] { "string", null });

            migrationBuilder.UpdateData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "Name", "NameSpaceId" },
                values: new object[] { "int", null });

            migrationBuilder.UpdateData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab"),
                columns: new[] { "NameSpace_Name", "NameSpaceId" },
                values: new object[] { "System", null });

            migrationBuilder.UpdateData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                columns: new[] { "Name", "NameSpaceId" },
                values: new object[] { "Object", new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab") });

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
                name: "FK_Attributes_NamespaceComponents_ClassId",
                table: "Attributes",
                column: "ClassId",
                principalTable: "NamespaceComponents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_NamespaceComponents_InstanceTypeId",
                table: "Attributes",
                column: "InstanceTypeId",
                principalTable: "NamespaceComponents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_NamespaceComponents_ReferenceTypeId",
                table: "Attributes",
                column: "ReferenceTypeId",
                principalTable: "NamespaceComponents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassParents_NamespaceComponents_ClassId",
                table: "ClassParents",
                column: "ClassId",
                principalTable: "NamespaceComponents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassParents_NamespaceComponents_ParentId",
                table: "ClassParents",
                column: "ParentId",
                principalTable: "NamespaceComponents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InterfaceMethodSignature_NamespaceComponents_InterfaceId",
                table: "InterfaceMethodSignature",
                column: "InterfaceId",
                principalTable: "NamespaceComponents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocalVariables_NamespaceComponents_InstanceTypeId",
                table: "LocalVariables",
                column: "InstanceTypeId",
                principalTable: "NamespaceComponents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalVariables_NamespaceComponents_ReferenceTypeId",
                table: "LocalVariables",
                column: "ReferenceTypeId",
                principalTable: "NamespaceComponents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Methods_NamespaceComponents_ReturnTypeId",
                table: "Methods",
                column: "ReturnTypeId",
                principalTable: "NamespaceComponents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MethodSignatures_NamespaceComponents_ReturnTypeId",
                table: "MethodSignatures",
                column: "ReturnTypeId",
                principalTable: "NamespaceComponents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NamespaceComponents_NamespaceComponents_FatherId",
                table: "NamespaceComponents",
                column: "FatherId",
                principalTable: "NamespaceComponents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NamespaceComponents_NamespaceComponents_NameSpaceId",
                table: "NamespaceComponents",
                column: "NameSpaceId",
                principalTable: "NamespaceComponents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parameters_NamespaceComponents_ParameterTypeId",
                table: "Parameters",
                column: "ParameterTypeId",
                principalTable: "NamespaceComponents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TypeMethod_NamespaceComponents_TypeId",
                table: "TypeMethod",
                column: "TypeId",
                principalTable: "NamespaceComponents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_NamespaceComponents_ClassId",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_NamespaceComponents_InstanceTypeId",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_NamespaceComponents_ReferenceTypeId",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassParents_NamespaceComponents_ClassId",
                table: "ClassParents");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassParents_NamespaceComponents_ParentId",
                table: "ClassParents");

            migrationBuilder.DropForeignKey(
                name: "FK_InterfaceMethodSignature_NamespaceComponents_InterfaceId",
                table: "InterfaceMethodSignature");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalVariables_NamespaceComponents_InstanceTypeId",
                table: "LocalVariables");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalVariables_NamespaceComponents_ReferenceTypeId",
                table: "LocalVariables");

            migrationBuilder.DropForeignKey(
                name: "FK_Methods_NamespaceComponents_ReturnTypeId",
                table: "Methods");

            migrationBuilder.DropForeignKey(
                name: "FK_MethodSignatures_NamespaceComponents_ReturnTypeId",
                table: "MethodSignatures");

            migrationBuilder.DropForeignKey(
                name: "FK_NamespaceComponents_NamespaceComponents_FatherId",
                table: "NamespaceComponents");

            migrationBuilder.DropForeignKey(
                name: "FK_NamespaceComponents_NamespaceComponents_NameSpaceId",
                table: "NamespaceComponents");

            migrationBuilder.DropForeignKey(
                name: "FK_Parameters_NamespaceComponents_ParameterTypeId",
                table: "Parameters");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeMethod_NamespaceComponents_TypeId",
                table: "TypeMethod");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NamespaceComponents",
                table: "NamespaceComponents");

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

            migrationBuilder.RenameTable(
                name: "NamespaceComponents",
                newName: "NamespaceComponent");

            migrationBuilder.RenameColumn(
                name: "NameSpaceId",
                table: "NamespaceComponent",
                newName: "NamespaceId");

            migrationBuilder.RenameColumn(
                name: "NameSpace_Name",
                table: "NamespaceComponent",
                newName: "Type_Name");

            migrationBuilder.RenameIndex(
                name: "IX_NamespaceComponents_NameSpaceId",
                table: "NamespaceComponent",
                newName: "IX_NamespaceComponent_NamespaceId");

            migrationBuilder.RenameIndex(
                name: "IX_NamespaceComponents_FatherId",
                table: "NamespaceComponent",
                newName: "IX_NamespaceComponent_FatherId");

            migrationBuilder.AddColumn<Guid>(
                name: "NameSpaceId",
                table: "NamespaceComponent",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NameSpaceId1",
                table: "NamespaceComponent",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NamespaceComponent",
                table: "NamespaceComponent",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "NamespaceComponent",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "Type_Name", "NameSpaceId" },
                values: new object[] { "bool", null });

            migrationBuilder.UpdateData(
                table: "NamespaceComponent",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "Type_Name", "NameSpaceId" },
                values: new object[] { "string", null });

            migrationBuilder.UpdateData(
                table: "NamespaceComponent",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "Type_Name", "NameSpaceId" },
                values: new object[] { "int", null });

            migrationBuilder.UpdateData(
                table: "NamespaceComponent",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab"),
                columns: new[] { "Name", "NameSpaceId" },
                values: new object[] { "System", null });

            migrationBuilder.UpdateData(
                table: "NamespaceComponent",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                columns: new[] { "Type_Name", "NameSpaceId", "NamespaceId" },
                values: new object[] { "Object", new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab"), null });

            migrationBuilder.InsertData(
                table: "NamespaceComponent",
                columns: new[] { "Id", "Discriminator", "Type_Name", "NameSpaceId", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("1ca51895-6bff-4e71-9702-39e5d059261d"), "Primitive", "nuint", null, 10 },
                    { new Guid("2299304b-62ba-4d90-a536-a1d95cfcaf24"), "Primitive", "nint", null, 9 },
                    { new Guid("69d6150d-af95-49f1-9128-3aaec961665f"), "Primitive", "decimal", null, 4 },
                    { new Guid("6eea2a44-da90-4b4e-b497-30522486fc16"), "Primitive", "ushort", null, 14 },
                    { new Guid("71577f69-e8c4-4166-9ae0-218c50dd2d66"), "Primitive", "byte", null, 1 },
                    { new Guid("75f39ab1-eed0-4c16-9672-9d4798a10ce0"), "Primitive", "ulong", null, 12 },
                    { new Guid("767d82a1-479c-44e0-a0da-8948a8139b0d"), "Primitive", "double", null, 5 },
                    { new Guid("814b9860-30a1-4829-9f35-4deda0a7ecae"), "Primitive", "sByte", null, 2 },
                    { new Guid("852ed5ab-e77a-4915-8db7-ef58cd8f68b0"), "Primitive", "float", null, 6 },
                    { new Guid("d41b6692-c26e-4874-9023-1d9a02bd3f34"), "Primitive", "char", null, 3 },
                    { new Guid("f19b4b7c-336d-433c-9772-b62ad07b5498"), "Primitive", "short", null, 13 },
                    { new Guid("f7c25d60-80f4-4796-9184-9a0313d9f1d8"), "Primitive", "long", null, 11 },
                    { new Guid("fa8eb4e6-cb9b-4426-976a-a782a24fa55d"), "Primitive", "uint", null, 8 }
                });

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InvokedMethodId", "MethodId", "MethodSignatureId", "Name", "ParameterTypeId" },
                values: new object[,]
                {
                    { new Guid("083e0d51-f540-4967-a1c2-2bad06d4c391"), null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), null, "StrIsNullOrEmpty", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("0dc17e3c-91ea-4eb3-b25f-c73e5e585dda"), null, new Guid("77777777-7777-7777-7777-777777777777"), null, "StrToCompare", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("18c59f83-82a2-4165-9994-1d48f9ac33cb"), null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("5213252d-8d6d-42ad-80f9-851c9d16e775"), null, new Guid("44444444-4444-4444-4444-444444444444"), null, "StrToParse", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("e9989b6a-df51-4b96-a0cb-ca3c3f29b813"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_NamespaceComponent_NameSpaceId",
                table: "NamespaceComponent",
                column: "NameSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_NamespaceComponent_NameSpaceId1",
                table: "NamespaceComponent",
                column: "NameSpaceId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_NamespaceComponent_ClassId",
                table: "Attributes",
                column: "ClassId",
                principalTable: "NamespaceComponent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_NamespaceComponent_InstanceTypeId",
                table: "Attributes",
                column: "InstanceTypeId",
                principalTable: "NamespaceComponent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_NamespaceComponent_ReferenceTypeId",
                table: "Attributes",
                column: "ReferenceTypeId",
                principalTable: "NamespaceComponent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassParents_NamespaceComponent_ClassId",
                table: "ClassParents",
                column: "ClassId",
                principalTable: "NamespaceComponent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassParents_NamespaceComponent_ParentId",
                table: "ClassParents",
                column: "ParentId",
                principalTable: "NamespaceComponent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InterfaceMethodSignature_NamespaceComponent_InterfaceId",
                table: "InterfaceMethodSignature",
                column: "InterfaceId",
                principalTable: "NamespaceComponent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocalVariables_NamespaceComponent_InstanceTypeId",
                table: "LocalVariables",
                column: "InstanceTypeId",
                principalTable: "NamespaceComponent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalVariables_NamespaceComponent_ReferenceTypeId",
                table: "LocalVariables",
                column: "ReferenceTypeId",
                principalTable: "NamespaceComponent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Methods_NamespaceComponent_ReturnTypeId",
                table: "Methods",
                column: "ReturnTypeId",
                principalTable: "NamespaceComponent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MethodSignatures_NamespaceComponent_ReturnTypeId",
                table: "MethodSignatures",
                column: "ReturnTypeId",
                principalTable: "NamespaceComponent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NamespaceComponent_NamespaceComponent_FatherId",
                table: "NamespaceComponent",
                column: "FatherId",
                principalTable: "NamespaceComponent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NamespaceComponent_NamespaceComponent_NameSpaceId",
                table: "NamespaceComponent",
                column: "NameSpaceId",
                principalTable: "NamespaceComponent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NamespaceComponent_NamespaceComponent_NameSpaceId1",
                table: "NamespaceComponent",
                column: "NameSpaceId1",
                principalTable: "NamespaceComponent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NamespaceComponent_NamespaceComponent_NamespaceId",
                table: "NamespaceComponent",
                column: "NamespaceId",
                principalTable: "NamespaceComponent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parameters_NamespaceComponent_ParameterTypeId",
                table: "Parameters",
                column: "ParameterTypeId",
                principalTable: "NamespaceComponent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TypeMethod_NamespaceComponent_TypeId",
                table: "TypeMethod",
                column: "TypeId",
                principalTable: "NamespaceComponent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
