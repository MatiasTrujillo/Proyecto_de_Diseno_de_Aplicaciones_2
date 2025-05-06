using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class FifthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvokedMethods_Methods_MethodId",
                table: "InvokedMethods");

            migrationBuilder.DropForeignKey(
                name: "FK_Parameters_Methods_MethodId",
                table: "Parameters");

            migrationBuilder.DropIndex(
                name: "IX_InvokedMethods_MethodId",
                table: "InvokedMethods");

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("012c129a-ed43-46c9-876a-8acb6bfa5949"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("11b73714-df0c-4457-b42a-6a94108f6dc9"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("24332cb3-421d-4fcf-b075-0fa93f9d3d71"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("2fa9febb-1a2c-4257-a043-572c64f253af"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("30aea049-9555-4717-b5ef-d2329ad58ede"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("3806e85e-c2c9-4fb6-9633-e2c1858f4986"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("4d865a06-0b53-4264-9bcf-7a911906151a"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("7095a344-8520-4b27-b2f4-09a57abcfd61"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("7864c9ef-85d7-4f8d-b3f8-09487170df05"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("7a6dc2f8-f117-4733-a349-7f5aaf35a148"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("9832e0ee-9ebb-492e-82d2-60f4dd657549"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("ab55ee74-396b-4e4c-a198-3009ebd12323"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("b2c0bf64-010a-4d67-81a4-887a561221fd"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("ca76d4e4-3645-4384-a683-03c1d25cc9cd"));

            migrationBuilder.CreateTable(
                name: "MethodInvokedMethods",
                columns: table => new
                {
                    MethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvokedMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MethodInvokedMethods", x => new { x.MethodId, x.InvokedMethodId });
                    table.ForeignKey(
                        name: "FK_MethodInvokedMethods_InvokedMethods_InvokedMethodId",
                        column: x => x.InvokedMethodId,
                        principalTable: "InvokedMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MethodInvokedMethods_Methods_MethodId",
                        column: x => x.MethodId,
                        principalTable: "Methods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InvokedMethodId", "MethodId", "Name", "ParameterTypeId" },
                values: new object[] { new Guid("d51ec3a4-eb93-4312-8dc5-9c0b1a3fe1b2"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("03edf7c2-c022-4310-957d-b67dcdd53311"), "Primitive", "ulong", 12 },
                    { new Guid("1f9ecbf0-01c8-4206-9a11-60a3d31811aa"), "Primitive", "long", 11 },
                    { new Guid("1fdf1e6e-4287-4ac2-a773-895b0318b44d"), "Primitive", "double", 5 },
                    { new Guid("31011703-063b-4caf-834f-4c0b745bcc55"), "Primitive", "ushort", 14 },
                    { new Guid("40b743f5-3dd4-43cf-afa8-298b116572bd"), "Primitive", "float", 6 },
                    { new Guid("4dc777c5-431f-489d-9eb3-b55f54a6019a"), "Primitive", "short", 13 },
                    { new Guid("55e4c39a-fd6f-4840-acbc-9327c800d57c"), "Primitive", "sByte", 2 },
                    { new Guid("971715ca-7181-4163-a4a8-4115aa746e5d"), "Primitive", "byte", 1 },
                    { new Guid("9d8be925-e45e-42a7-a841-9608038b638b"), "Primitive", "nint", 9 },
                    { new Guid("a8ed71d3-608a-442c-9429-50801ba0f0fc"), "Primitive", "uint", 8 },
                    { new Guid("b4dbb59d-79cc-4e0d-a6b1-c6bbbc63d26c"), "Primitive", "nuint", 10 },
                    { new Guid("d56ac69d-08bb-4dc0-b55c-4ade6578828f"), "Primitive", "decimal", 4 },
                    { new Guid("e65db76b-5414-497b-8a61-495068fb1aa1"), "Primitive", "char", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MethodInvokedMethods_InvokedMethodId",
                table: "MethodInvokedMethods",
                column: "InvokedMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parameters_Methods_MethodId",
                table: "Parameters",
                column: "MethodId",
                principalTable: "Methods",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parameters_Methods_MethodId",
                table: "Parameters");

            migrationBuilder.DropTable(
                name: "MethodInvokedMethods");

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("d51ec3a4-eb93-4312-8dc5-9c0b1a3fe1b2"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("03edf7c2-c022-4310-957d-b67dcdd53311"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("1f9ecbf0-01c8-4206-9a11-60a3d31811aa"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("1fdf1e6e-4287-4ac2-a773-895b0318b44d"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("31011703-063b-4caf-834f-4c0b745bcc55"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("40b743f5-3dd4-43cf-afa8-298b116572bd"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("4dc777c5-431f-489d-9eb3-b55f54a6019a"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("55e4c39a-fd6f-4840-acbc-9327c800d57c"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("971715ca-7181-4163-a4a8-4115aa746e5d"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("9d8be925-e45e-42a7-a841-9608038b638b"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("a8ed71d3-608a-442c-9429-50801ba0f0fc"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("b4dbb59d-79cc-4e0d-a6b1-c6bbbc63d26c"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("d56ac69d-08bb-4dc0-b55c-4ade6578828f"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("e65db76b-5414-497b-8a61-495068fb1aa1"));

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InvokedMethodId", "MethodId", "Name", "ParameterTypeId" },
                values: new object[] { new Guid("012c129a-ed43-46c9-876a-8acb6bfa5949"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("11b73714-df0c-4457-b42a-6a94108f6dc9"), "Primitive", "nuint", 10 },
                    { new Guid("24332cb3-421d-4fcf-b075-0fa93f9d3d71"), "Primitive", "char", 3 },
                    { new Guid("2fa9febb-1a2c-4257-a043-572c64f253af"), "Primitive", "sByte", 2 },
                    { new Guid("30aea049-9555-4717-b5ef-d2329ad58ede"), "Primitive", "long", 11 },
                    { new Guid("3806e85e-c2c9-4fb6-9633-e2c1858f4986"), "Primitive", "ushort", 14 },
                    { new Guid("4d865a06-0b53-4264-9bcf-7a911906151a"), "Primitive", "nint", 9 },
                    { new Guid("7095a344-8520-4b27-b2f4-09a57abcfd61"), "Primitive", "ulong", 12 },
                    { new Guid("7864c9ef-85d7-4f8d-b3f8-09487170df05"), "Primitive", "uint", 8 },
                    { new Guid("7a6dc2f8-f117-4733-a349-7f5aaf35a148"), "Primitive", "byte", 1 },
                    { new Guid("9832e0ee-9ebb-492e-82d2-60f4dd657549"), "Primitive", "double", 5 },
                    { new Guid("ab55ee74-396b-4e4c-a198-3009ebd12323"), "Primitive", "float", 6 },
                    { new Guid("b2c0bf64-010a-4d67-81a4-887a561221fd"), "Primitive", "short", 13 },
                    { new Guid("ca76d4e4-3645-4384-a683-03c1d25cc9cd"), "Primitive", "decimal", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvokedMethods_MethodId",
                table: "InvokedMethods",
                column: "MethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvokedMethods_Methods_MethodId",
                table: "InvokedMethods",
                column: "MethodId",
                principalTable: "Methods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parameters_Methods_MethodId",
                table: "Parameters",
                column: "MethodId",
                principalTable: "Methods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
