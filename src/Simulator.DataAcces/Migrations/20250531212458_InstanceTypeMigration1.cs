using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class InstanceTypeMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_Type_TypeId",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalVariables_Type_LocalVariableTypeId",
                table: "LocalVariables");

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("00f27e70-eb5a-48c7-ade2-10c2af668716"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("07540df5-9d3a-4413-b7d0-69b3ba68bc76"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("6043305c-6417-41bf-8cb4-f604c1bfd95f"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("83ee25f7-5274-46f2-b47c-5562eb28685a"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("c8a6f00c-1b28-49a5-862d-5d408e149bbb"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("06e7b0a8-c957-475c-9823-369c2d241a6f"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("0fe264fa-d041-4db0-9ef5-1af076882a05"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("2ddacc94-1021-4537-8b29-709972011a4d"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("3b37ca77-5d43-460c-be9c-c4715833ef68"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("5129e946-e293-480e-a146-4e1df2d4434f"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("6673caf7-b4d8-461a-9ca7-90558e26cc4a"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("83a20374-cbc7-4117-ae2e-d2c64d171cba"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("8f918d27-58e1-40c3-8058-524b5b9e33be"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("bd3725e1-fc47-4518-ad44-9dc2f013dcee"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("c57cbaa6-662c-420a-aecb-d9146225f18e"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("ce68ef8b-210d-4742-86f3-ef4b4367ec6b"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("ddcd4696-ea0d-46d5-a4f6-86d3a3a8fa19"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("e66abe9b-9584-4691-bc4a-4c15aeceffcd"));

            migrationBuilder.RenameColumn(
                name: "LocalVariableTypeId",
                table: "LocalVariables",
                newName: "ReferenceTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_LocalVariables_LocalVariableTypeId",
                table: "LocalVariables",
                newName: "IX_LocalVariables_ReferenceTypeId");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Attributes",
                newName: "ReferenceTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Attributes_TypeId",
                table: "Attributes",
                newName: "IX_Attributes_ReferenceTypeId");

            migrationBuilder.AddColumn<Guid>(
                name: "InstanceTypeId",
                table: "LocalVariables",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InstanceTypeId",
                table: "Attributes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InvokedMethodId", "MethodId", "MethodSignatureId", "Name", "ParameterTypeId" },
                values: new object[,]
                {
                    { new Guid("1a361c4c-9a3e-4658-86fb-1d681a7278b0"), null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), null, "StrIsNullOrEmpty", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("2b5a3431-0fa4-44e9-b7a0-4c7c09300ebf"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("528df842-e4b1-4d0a-827f-63d7730883c6"), null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("aee72c8b-874b-4d40-acfe-0ad27d685c7d"), null, new Guid("77777777-7777-7777-7777-777777777777"), null, "StrToCompare", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("bba0f373-462f-40e8-9013-2227e8ea43c5"), null, new Guid("44444444-4444-4444-4444-444444444444"), null, "StrToParse", new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("3dfbddfa-0851-4bc0-bbe2-d3943eae3fcf"), "Primitive", "decimal", 4 },
                    { new Guid("56e7c1cf-ef33-4022-9f4c-27f43864ca95"), "Primitive", "nint", 9 },
                    { new Guid("57d1ffe4-4fa0-48fd-be6b-1dba1fc0b1d9"), "Primitive", "sByte", 2 },
                    { new Guid("62a7933e-ccb0-41d6-9db5-3ac43dc77291"), "Primitive", "short", 13 },
                    { new Guid("67ecb8ec-9a29-4b19-8207-bcc9da5ec34b"), "Primitive", "long", 11 },
                    { new Guid("75d012e1-d529-4e74-9d75-b742ab625dbb"), "Primitive", "char", 3 },
                    { new Guid("8fcf1f90-eb5f-4701-94e0-7c973a7c6e84"), "Primitive", "ushort", 14 },
                    { new Guid("adf1e746-6d5a-45a6-ba9a-5ce1db5b4aae"), "Primitive", "float", 6 },
                    { new Guid("b18d2ec2-8edc-4895-9796-23eb47333ef2"), "Primitive", "double", 5 },
                    { new Guid("e0bfd572-111f-40a8-b8ff-5c312d60dc7d"), "Primitive", "nuint", 10 },
                    { new Guid("eddbc75a-ccc8-4bcf-860b-dc7caf3aab91"), "Primitive", "ulong", 12 },
                    { new Guid("f1ac8949-6fbb-4cdf-b073-2de02dc3d9d5"), "Primitive", "byte", 1 },
                    { new Guid("f9f962a2-a7de-43b2-85b5-e63a16a7999e"), "Primitive", "uint", 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocalVariables_InstanceTypeId",
                table: "LocalVariables",
                column: "InstanceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_InstanceTypeId",
                table: "Attributes",
                column: "InstanceTypeId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_Type_InstanceTypeId",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_Type_ReferenceTypeId",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalVariables_Type_InstanceTypeId",
                table: "LocalVariables");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalVariables_Type_ReferenceTypeId",
                table: "LocalVariables");

            migrationBuilder.DropIndex(
                name: "IX_LocalVariables_InstanceTypeId",
                table: "LocalVariables");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_InstanceTypeId",
                table: "Attributes");

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("1a361c4c-9a3e-4658-86fb-1d681a7278b0"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("2b5a3431-0fa4-44e9-b7a0-4c7c09300ebf"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("528df842-e4b1-4d0a-827f-63d7730883c6"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("aee72c8b-874b-4d40-acfe-0ad27d685c7d"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("bba0f373-462f-40e8-9013-2227e8ea43c5"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("3dfbddfa-0851-4bc0-bbe2-d3943eae3fcf"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("56e7c1cf-ef33-4022-9f4c-27f43864ca95"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("57d1ffe4-4fa0-48fd-be6b-1dba1fc0b1d9"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("62a7933e-ccb0-41d6-9db5-3ac43dc77291"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("67ecb8ec-9a29-4b19-8207-bcc9da5ec34b"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("75d012e1-d529-4e74-9d75-b742ab625dbb"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("8fcf1f90-eb5f-4701-94e0-7c973a7c6e84"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("adf1e746-6d5a-45a6-ba9a-5ce1db5b4aae"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("b18d2ec2-8edc-4895-9796-23eb47333ef2"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("e0bfd572-111f-40a8-b8ff-5c312d60dc7d"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("eddbc75a-ccc8-4bcf-860b-dc7caf3aab91"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("f1ac8949-6fbb-4cdf-b073-2de02dc3d9d5"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("f9f962a2-a7de-43b2-85b5-e63a16a7999e"));

            migrationBuilder.DropColumn(
                name: "InstanceTypeId",
                table: "LocalVariables");

            migrationBuilder.DropColumn(
                name: "InstanceTypeId",
                table: "Attributes");

            migrationBuilder.RenameColumn(
                name: "ReferenceTypeId",
                table: "LocalVariables",
                newName: "LocalVariableTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_LocalVariables_ReferenceTypeId",
                table: "LocalVariables",
                newName: "IX_LocalVariables_LocalVariableTypeId");

            migrationBuilder.RenameColumn(
                name: "ReferenceTypeId",
                table: "Attributes",
                newName: "TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Attributes_ReferenceTypeId",
                table: "Attributes",
                newName: "IX_Attributes_TypeId");

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InvokedMethodId", "MethodId", "MethodSignatureId", "Name", "ParameterTypeId" },
                values: new object[,]
                {
                    { new Guid("00f27e70-eb5a-48c7-ade2-10c2af668716"), null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), null, "StrIsNullOrEmpty", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("07540df5-9d3a-4413-b7d0-69b3ba68bc76"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("6043305c-6417-41bf-8cb4-f604c1bfd95f"), null, new Guid("44444444-4444-4444-4444-444444444444"), null, "StrToParse", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("83ee25f7-5274-46f2-b47c-5562eb28685a"), null, new Guid("77777777-7777-7777-7777-777777777777"), null, "StrToCompare", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("c8a6f00c-1b28-49a5-862d-5d408e149bbb"), null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") }
                });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("06e7b0a8-c957-475c-9823-369c2d241a6f"), "Primitive", "float", 6 },
                    { new Guid("0fe264fa-d041-4db0-9ef5-1af076882a05"), "Primitive", "byte", 1 },
                    { new Guid("2ddacc94-1021-4537-8b29-709972011a4d"), "Primitive", "nint", 9 },
                    { new Guid("3b37ca77-5d43-460c-be9c-c4715833ef68"), "Primitive", "char", 3 },
                    { new Guid("5129e946-e293-480e-a146-4e1df2d4434f"), "Primitive", "ulong", 12 },
                    { new Guid("6673caf7-b4d8-461a-9ca7-90558e26cc4a"), "Primitive", "double", 5 },
                    { new Guid("83a20374-cbc7-4117-ae2e-d2c64d171cba"), "Primitive", "nuint", 10 },
                    { new Guid("8f918d27-58e1-40c3-8058-524b5b9e33be"), "Primitive", "short", 13 },
                    { new Guid("bd3725e1-fc47-4518-ad44-9dc2f013dcee"), "Primitive", "decimal", 4 },
                    { new Guid("c57cbaa6-662c-420a-aecb-d9146225f18e"), "Primitive", "long", 11 },
                    { new Guid("ce68ef8b-210d-4742-86f3-ef4b4367ec6b"), "Primitive", "uint", 8 },
                    { new Guid("ddcd4696-ea0d-46d5-a4f6-86d3a3a8fa19"), "Primitive", "ushort", 14 },
                    { new Guid("e66abe9b-9584-4691-bc4a-4c15aeceffcd"), "Primitive", "sByte", 2 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_Type_TypeId",
                table: "Attributes",
                column: "TypeId",
                principalTable: "Type",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalVariables_Type_LocalVariableTypeId",
                table: "LocalVariables",
                column: "LocalVariableTypeId",
                principalTable: "Type",
                principalColumn: "Id");
        }
    }
}
