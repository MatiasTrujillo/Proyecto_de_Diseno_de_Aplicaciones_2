using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class ThirteenMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parameters_MethodSignatures_MethodSignatureId",
                table: "Parameters");

            migrationBuilder.DropForeignKey(
                name: "FK_Type_NameSpaces_Class_NameSpaceId",
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
                    { new Guid("3d073d3c-8fc1-4d4b-9147-38bc38619d28"), null, new Guid("44444444-4444-4444-4444-444444444444"), null, "StrToParse", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("685976a3-20e7-41bb-9143-7284bc656978"), null, new Guid("77777777-7777-7777-7777-777777777777"), null, "StrToCompare", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("7c6f7473-10ad-4771-8650-eda8530d8884"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("ee60bb01-e119-4809-9961-1c6ddbb66498"), null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("f969e93c-4560-48b6-9621-c0e25bf2c0c9"), null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), null, "StrIsNullOrEmpty", new Guid("22222222-2222-2222-2222-222222222222") }
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

            migrationBuilder.AddForeignKey(
                name: "FK_Parameters_MethodSignatures_MethodSignatureId",
                table: "Parameters",
                column: "MethodSignatureId",
                principalTable: "MethodSignatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parameters_MethodSignatures_MethodSignatureId",
                table: "Parameters");

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
                name: "FK_Parameters_MethodSignatures_MethodSignatureId",
                table: "Parameters",
                column: "MethodSignatureId",
                principalTable: "MethodSignatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Type_NameSpaces_Class_NameSpaceId",
                table: "Type",
                column: "Class_NameSpaceId",
                principalTable: "NameSpaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
