using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class TwelveMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Type_NameSpaces_NameSpaceId",
                table: "Type");

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

            migrationBuilder.AddColumn<Guid>(
                name: "Class_NameSpaceId",
                table: "Type",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InvokedMethodId", "MethodId", "MethodSignatureId", "Name", "ParameterTypeId" },
                values: new object[,]
                {
                    { new Guid("03eabe81-6188-4cd3-8ef9-d08c8725e3f5"), null, new Guid("44444444-4444-4444-4444-444444444444"), null, "StrToParse", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("391d2d6d-e06c-4865-8adb-812ff101d24b"), null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("420d1638-b6dd-484e-8981-8192617bcda8"), null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), null, "StrIsNullOrEmpty", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("a374ca9d-7adf-450c-a133-d53c45c311f3"), null, new Guid("77777777-7777-7777-7777-777777777777"), null, "StrToCompare", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("d6116c27-a89f-42cb-8fa7-6d35dc612d4f"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") }
                });

            migrationBuilder.UpdateData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                column: "Class_NameSpaceId",
                value: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab"));

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("0c6a1c04-51ae-402c-9728-9c4d23a93d98"), "Primitive", "nuint", 10 },
                    { new Guid("1e17f498-cbf3-4016-8d37-4d32fb876d35"), "Primitive", "decimal", 4 },
                    { new Guid("3c2f51ac-2a36-4b43-a317-4722c75c5909"), "Primitive", "nint", 9 },
                    { new Guid("4b387ffa-712d-4b8d-a007-892c105f4f54"), "Primitive", "byte", 1 },
                    { new Guid("4cfa4c0d-5ad3-4f63-99b6-7042f12c1bce"), "Primitive", "float", 6 },
                    { new Guid("6eb540bc-9931-487c-9ab0-722fec5b9002"), "Primitive", "ushort", 14 },
                    { new Guid("9c2e6793-6bde-4523-9ef7-205d047f3fa8"), "Primitive", "char", 3 },
                    { new Guid("b1a81729-f5bd-4009-88f5-bf82397d6f72"), "Primitive", "long", 11 },
                    { new Guid("b623dd07-0551-4ed6-aeaf-1f7c9cffd2e8"), "Primitive", "short", 13 },
                    { new Guid("cb11bc17-2241-408d-8a43-a233b83ced6b"), "Primitive", "sByte", 2 },
                    { new Guid("d3f85a2c-5044-4dd8-9a57-e159fdf93a80"), "Primitive", "double", 5 },
                    { new Guid("eb626aa9-52d5-4761-98eb-806dfc2c5956"), "Primitive", "uint", 8 },
                    { new Guid("eb65fba8-b1b0-4b9a-9283-d2d9663270fe"), "Primitive", "ulong", 12 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Type_Class_NameSpaceId",
                table: "Type",
                column: "Class_NameSpaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Type_NameSpaces_Class_NameSpaceId",
                table: "Type",
                column: "Class_NameSpaceId",
                principalTable: "NameSpaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Type_NameSpaces_NameSpaceId",
                table: "Type",
                column: "NameSpaceId",
                principalTable: "NameSpaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Type_NameSpaces_Class_NameSpaceId",
                table: "Type");

            migrationBuilder.DropForeignKey(
                name: "FK_Type_NameSpaces_NameSpaceId",
                table: "Type");

            migrationBuilder.DropIndex(
                name: "IX_Type_Class_NameSpaceId",
                table: "Type");

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("03eabe81-6188-4cd3-8ef9-d08c8725e3f5"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("391d2d6d-e06c-4865-8adb-812ff101d24b"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("420d1638-b6dd-484e-8981-8192617bcda8"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("a374ca9d-7adf-450c-a133-d53c45c311f3"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("d6116c27-a89f-42cb-8fa7-6d35dc612d4f"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("0c6a1c04-51ae-402c-9728-9c4d23a93d98"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("1e17f498-cbf3-4016-8d37-4d32fb876d35"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("3c2f51ac-2a36-4b43-a317-4722c75c5909"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("4b387ffa-712d-4b8d-a007-892c105f4f54"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("4cfa4c0d-5ad3-4f63-99b6-7042f12c1bce"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("6eb540bc-9931-487c-9ab0-722fec5b9002"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("9c2e6793-6bde-4523-9ef7-205d047f3fa8"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("b1a81729-f5bd-4009-88f5-bf82397d6f72"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("b623dd07-0551-4ed6-aeaf-1f7c9cffd2e8"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("cb11bc17-2241-408d-8a43-a233b83ced6b"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("d3f85a2c-5044-4dd8-9a57-e159fdf93a80"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("eb626aa9-52d5-4761-98eb-806dfc2c5956"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("eb65fba8-b1b0-4b9a-9283-d2d9663270fe"));

            migrationBuilder.DropColumn(
                name: "Class_NameSpaceId",
                table: "Type");

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
                name: "FK_Type_NameSpaces_NameSpaceId",
                table: "Type",
                column: "NameSpaceId",
                principalTable: "NameSpaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
