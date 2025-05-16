using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class TenthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("0fe48caf-d45c-40f4-8be2-b48fd661d97f"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("436569d4-23b8-436a-9550-dbb6ff0744d0"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("548d565c-6988-498b-9220-93a61ca2be79"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("79d298b8-40fa-4a34-bdbb-7906ad6f869c"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("af0e1944-8cd2-4d05-9196-c8270202f310"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("03b39ee5-305f-46d3-8007-ef3979d99738"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("07a5ec8e-348a-4879-b630-fbac5a4cf6da"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("220663c0-aef4-48d8-bb40-e860e43e68e5"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("25cffcc3-37b2-4047-82b9-33903fc96906"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("3f6b5b13-19e2-49bf-961e-43014db463f5"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("5e9f1d37-3676-4cb3-836a-86165f770377"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("6ccac1a8-a281-474a-859a-ca3f3af237b0"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("72c76c45-f8d6-41ab-b9c6-424c92ece934"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("73459a3e-cc60-4313-8add-1344b0371325"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("8970636b-75c9-4cfb-9390-c3eba6f788da"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("9f6332b7-f46e-42cf-b381-e4738594b9aa"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("c0d019e0-72ea-4286-a633-2615bfcf6b78"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("f8a9f44a-cba5-4e78-8e49-cb8b5c5e1a66"));

            migrationBuilder.AddColumn<Guid>(
                name: "MethodSignatureId",
                table: "Parameters",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MethodSignatures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReturnTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MethodSignatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MethodSignatures_Type_ReturnTypeId",
                        column: x => x.ReturnTypeId,
                        principalTable: "Type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InterfaceMethodSignature",
                columns: table => new
                {
                    InterfaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MethodSignatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterfaceMethodSignature", x => new { x.InterfaceId, x.MethodSignatureId });
                    table.ForeignKey(
                        name: "FK_InterfaceMethodSignature_MethodSignatures_MethodSignatureId",
                        column: x => x.MethodSignatureId,
                        principalTable: "MethodSignatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterfaceMethodSignature_Type_InterfaceId",
                        column: x => x.InterfaceId,
                        principalTable: "Type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InvokedMethodId", "MethodId", "MethodSignatureId", "Name", "ParameterTypeId" },
                values: new object[,]
                {
                    { new Guid("10430884-bf30-4b4a-9fff-4238f247df9d"), null, new Guid("77777777-7777-7777-7777-777777777777"), null, "StrToCompare", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("4b23204e-065f-45e1-8afc-e8f68aef92f5"), null, new Guid("44444444-4444-4444-4444-444444444444"), null, "StrToParse", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("69919187-6035-4193-ab78-139223689a5a"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("a47167eb-1d9b-41a9-9aea-f72b48f4a382"), null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("e5bb48a0-d125-4e1d-9a39-416442c8b1ab"), null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), null, "StrIsNullOrEmpty", new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("03dda656-31a1-49b9-9a36-30348d5fb47e"), "Primitive", "ushort", 14 },
                    { new Guid("27a9f176-6fc0-4af9-8de1-9b5532271d1a"), "Primitive", "decimal", 4 },
                    { new Guid("3bf42bb7-b895-48fb-972c-605526825f0f"), "Primitive", "ulong", 12 },
                    { new Guid("55fc1163-0353-49c6-854e-c47a624ee9e5"), "Primitive", "short", 13 },
                    { new Guid("7d1c83ab-4ca0-4b43-bc8f-d7e541a6abad"), "Primitive", "float", 6 },
                    { new Guid("ab69ce2a-0a2c-413e-ba74-4cb7218a0997"), "Primitive", "byte", 1 },
                    { new Guid("ac911356-838f-44c1-956e-694dbc89fa53"), "Primitive", "long", 11 },
                    { new Guid("b4378e0d-4dad-4945-9456-0009773844d6"), "Primitive", "nint", 9 },
                    { new Guid("c36d55ff-9339-4467-a969-0f764b3c728a"), "Primitive", "uint", 8 },
                    { new Guid("e2d221c8-58c2-4931-b858-8cddf103d6d1"), "Primitive", "char", 3 },
                    { new Guid("e8166d79-717c-4a48-9429-76ad9bbbbe90"), "Primitive", "sByte", 2 },
                    { new Guid("f17fbea8-2753-4454-80d7-6a3df72c730e"), "Primitive", "double", 5 },
                    { new Guid("ff0a2cbe-73e0-47ea-b8f5-81ebe4661721"), "Primitive", "nuint", 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_MethodSignatureId",
                table: "Parameters",
                column: "MethodSignatureId");

            migrationBuilder.CreateIndex(
                name: "IX_InterfaceMethodSignature_MethodSignatureId",
                table: "InterfaceMethodSignature",
                column: "MethodSignatureId");

            migrationBuilder.CreateIndex(
                name: "IX_MethodSignatures_ReturnTypeId",
                table: "MethodSignatures",
                column: "ReturnTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parameters_MethodSignatures_MethodSignatureId",
                table: "Parameters",
                column: "MethodSignatureId",
                principalTable: "MethodSignatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parameters_MethodSignatures_MethodSignatureId",
                table: "Parameters");

            migrationBuilder.DropTable(
                name: "InterfaceMethodSignature");

            migrationBuilder.DropTable(
                name: "MethodSignatures");

            migrationBuilder.DropIndex(
                name: "IX_Parameters_MethodSignatureId",
                table: "Parameters");

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("10430884-bf30-4b4a-9fff-4238f247df9d"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("4b23204e-065f-45e1-8afc-e8f68aef92f5"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("69919187-6035-4193-ab78-139223689a5a"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("a47167eb-1d9b-41a9-9aea-f72b48f4a382"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("e5bb48a0-d125-4e1d-9a39-416442c8b1ab"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("03dda656-31a1-49b9-9a36-30348d5fb47e"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("27a9f176-6fc0-4af9-8de1-9b5532271d1a"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("3bf42bb7-b895-48fb-972c-605526825f0f"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("55fc1163-0353-49c6-854e-c47a624ee9e5"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("7d1c83ab-4ca0-4b43-bc8f-d7e541a6abad"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("ab69ce2a-0a2c-413e-ba74-4cb7218a0997"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("ac911356-838f-44c1-956e-694dbc89fa53"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("b4378e0d-4dad-4945-9456-0009773844d6"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("c36d55ff-9339-4467-a969-0f764b3c728a"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("e2d221c8-58c2-4931-b858-8cddf103d6d1"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("e8166d79-717c-4a48-9429-76ad9bbbbe90"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("f17fbea8-2753-4454-80d7-6a3df72c730e"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("ff0a2cbe-73e0-47ea-b8f5-81ebe4661721"));

            migrationBuilder.DropColumn(
                name: "MethodSignatureId",
                table: "Parameters");

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InvokedMethodId", "MethodId", "Name", "ParameterTypeId" },
                values: new object[,]
                {
                    { new Guid("0fe48caf-d45c-40f4-8be2-b48fd661d97f"), null, new Guid("77777777-7777-7777-7777-777777777777"), "StrToCompare", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("436569d4-23b8-436a-9550-dbb6ff0744d0"), null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), "StrIsNullOrEmpty", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("548d565c-6988-498b-9220-93a61ca2be79"), null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("79d298b8-40fa-4a34-bdbb-7906ad6f869c"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("af0e1944-8cd2-4d05-9196-c8270202f310"), null, new Guid("44444444-4444-4444-4444-444444444444"), "StrToParse", new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("03b39ee5-305f-46d3-8007-ef3979d99738"), "Primitive", "byte", 1 },
                    { new Guid("07a5ec8e-348a-4879-b630-fbac5a4cf6da"), "Primitive", "double", 5 },
                    { new Guid("220663c0-aef4-48d8-bb40-e860e43e68e5"), "Primitive", "long", 11 },
                    { new Guid("25cffcc3-37b2-4047-82b9-33903fc96906"), "Primitive", "ushort", 14 },
                    { new Guid("3f6b5b13-19e2-49bf-961e-43014db463f5"), "Primitive", "short", 13 },
                    { new Guid("5e9f1d37-3676-4cb3-836a-86165f770377"), "Primitive", "nint", 9 },
                    { new Guid("6ccac1a8-a281-474a-859a-ca3f3af237b0"), "Primitive", "float", 6 },
                    { new Guid("72c76c45-f8d6-41ab-b9c6-424c92ece934"), "Primitive", "sByte", 2 },
                    { new Guid("73459a3e-cc60-4313-8add-1344b0371325"), "Primitive", "decimal", 4 },
                    { new Guid("8970636b-75c9-4cfb-9390-c3eba6f788da"), "Primitive", "uint", 8 },
                    { new Guid("9f6332b7-f46e-42cf-b381-e4738594b9aa"), "Primitive", "ulong", 12 },
                    { new Guid("c0d019e0-72ea-4286-a633-2615bfcf6b78"), "Primitive", "nuint", 10 },
                    { new Guid("f8a9f44a-cba5-4e78-8e49-cb8b5c5e1a66"), "Primitive", "char", 3 }
                });
        }
    }
}
