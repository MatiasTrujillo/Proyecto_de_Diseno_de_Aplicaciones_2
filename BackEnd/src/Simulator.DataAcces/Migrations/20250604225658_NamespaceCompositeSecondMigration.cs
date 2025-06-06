using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class NamespaceCompositeSecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_Type_ClassId",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_Type_InstanceTypeId",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_Type_ReferenceTypeId",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassParents_Type_ClassId",
                table: "ClassParents");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassParents_Type_ParentId",
                table: "ClassParents");

            migrationBuilder.DropForeignKey(
                name: "FK_InterfaceMethodSignature_Type_InterfaceId",
                table: "InterfaceMethodSignature");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalVariables_Type_InstanceTypeId",
                table: "LocalVariables");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalVariables_Type_ReferenceTypeId",
                table: "LocalVariables");

            migrationBuilder.DropForeignKey(
                name: "FK_Methods_Type_ReturnTypeId",
                table: "Methods");

            migrationBuilder.DropForeignKey(
                name: "FK_MethodSignatures_Type_ReturnTypeId",
                table: "MethodSignatures");

            migrationBuilder.DropForeignKey(
                name: "FK_Parameters_Type_ParameterTypeId",
                table: "Parameters");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeMethod_Type_TypeId",
                table: "TypeMethod");

            migrationBuilder.DropTable(
                name: "NameSpaces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Type",
                table: "Type");

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

            migrationBuilder.RenameTable(
                name: "Type",
                newName: "NamespaceComponent");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "NamespaceComponent",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "NamespaceComponent",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

            migrationBuilder.AddColumn<Guid>(
                name: "FatherId",
                table: "NamespaceComponent",
                type: "uniqueidentifier",
                nullable: true);

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

            migrationBuilder.AddColumn<Guid>(
                name: "NamespaceId",
                table: "NamespaceComponent",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type_Name",
                table: "NamespaceComponent",
                type: "nvarchar(max)",
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
                    { new Guid("852ed5ab-e77a-4915-8db7-ef58cd8f68b0"), "Primitive", "float", null, 6 }
                });

            migrationBuilder.InsertData(
                table: "NamespaceComponent",
                columns: new[] { "Id", "Discriminator", "FatherId", "Name", "NameSpaceId" },
                values: new object[] { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab"), "NameSpace", null, "System", null });

            migrationBuilder.InsertData(
                table: "NamespaceComponent",
                columns: new[] { "Id", "Discriminator", "Type_Name", "NameSpaceId", "PrimitiveType" },
                values: new object[,]
                {
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
                name: "IX_NamespaceComponent_FatherId",
                table: "NamespaceComponent",
                column: "FatherId");

            migrationBuilder.CreateIndex(
                name: "IX_NamespaceComponent_NamespaceId",
                table: "NamespaceComponent",
                column: "NamespaceId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "IX_NamespaceComponent_FatherId",
                table: "NamespaceComponent");

            migrationBuilder.DropIndex(
                name: "IX_NamespaceComponent_NamespaceId",
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
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab"));

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
                name: "FatherId",
                table: "NamespaceComponent");

            migrationBuilder.DropColumn(
                name: "NameSpaceId",
                table: "NamespaceComponent");

            migrationBuilder.DropColumn(
                name: "NameSpaceId1",
                table: "NamespaceComponent");

            migrationBuilder.DropColumn(
                name: "NamespaceId",
                table: "NamespaceComponent");

            migrationBuilder.DropColumn(
                name: "Type_Name",
                table: "NamespaceComponent");

            migrationBuilder.RenameTable(
                name: "NamespaceComponent",
                newName: "Type");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Type",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "Type",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(21)",
                oldMaxLength: 21);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Type",
                table: "Type",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "NameSpaces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FatherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NameSpaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NameSpaces_NameSpaces_FatherId",
                        column: x => x.FatherId,
                        principalTable: "NameSpaces",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "NameSpaces",
                columns: new[] { "Id", "FatherId", "Name" },
                values: new object[] { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab"), null, "System" });

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

            migrationBuilder.UpdateData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "Name",
                value: "bool");

            migrationBuilder.UpdateData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "Name",
                value: "string");

            migrationBuilder.UpdateData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "Name",
                value: "int");

            migrationBuilder.UpdateData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                column: "Name",
                value: "Object");

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

            migrationBuilder.CreateIndex(
                name: "IX_NameSpaces_FatherId",
                table: "NameSpaces",
                column: "FatherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_Type_ClassId",
                table: "Attributes",
                column: "ClassId",
                principalTable: "Type",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_Type_InstanceTypeId",
                table: "Attributes",
                column: "InstanceTypeId",
                principalTable: "Type",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_Type_ReferenceTypeId",
                table: "Attributes",
                column: "ReferenceTypeId",
                principalTable: "Type",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassParents_Type_ClassId",
                table: "ClassParents",
                column: "ClassId",
                principalTable: "Type",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassParents_Type_ParentId",
                table: "ClassParents",
                column: "ParentId",
                principalTable: "Type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InterfaceMethodSignature_Type_InterfaceId",
                table: "InterfaceMethodSignature",
                column: "InterfaceId",
                principalTable: "Type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocalVariables_Type_InstanceTypeId",
                table: "LocalVariables",
                column: "InstanceTypeId",
                principalTable: "Type",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalVariables_Type_ReferenceTypeId",
                table: "LocalVariables",
                column: "ReferenceTypeId",
                principalTable: "Type",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Methods_Type_ReturnTypeId",
                table: "Methods",
                column: "ReturnTypeId",
                principalTable: "Type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MethodSignatures_Type_ReturnTypeId",
                table: "MethodSignatures",
                column: "ReturnTypeId",
                principalTable: "Type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parameters_Type_ParameterTypeId",
                table: "Parameters",
                column: "ParameterTypeId",
                principalTable: "Type",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TypeMethod_Type_TypeId",
                table: "TypeMethod",
                column: "TypeId",
                principalTable: "Type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
