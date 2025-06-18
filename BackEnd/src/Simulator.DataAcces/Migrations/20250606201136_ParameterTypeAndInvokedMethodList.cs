using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class ParameterTypeAndInvokedMethodList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_InvokedMethods_InvokedMethodId",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalVariables_InvokedMethods_InvokedMethodId",
                table: "LocalVariables");

            migrationBuilder.DropForeignKey(
                name: "FK_Parameters_InvokedMethods_InvokedMethodId",
                table: "Parameters");

            migrationBuilder.DropIndex(
                name: "IX_Parameters_InvokedMethodId",
                table: "Parameters");

            migrationBuilder.DropIndex(
                name: "IX_LocalVariables_InvokedMethodId",
                table: "LocalVariables");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_InvokedMethodId",
                table: "Attributes");

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

            migrationBuilder.DropColumn(
                name: "InvokedMethodId",
                table: "Parameters");

            migrationBuilder.DropColumn(
                name: "InvokedMethodId",
                table: "LocalVariables");

            migrationBuilder.DropColumn(
                name: "InvokedMethodId",
                table: "Attributes");

            migrationBuilder.CreateTable(
                name: "AttributeInvokedMethods",
                columns: table => new
                {
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvokedMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeInvokedMethods", x => new { x.AttributeId, x.InvokedMethodId });
                    table.ForeignKey(
                        name: "FK_AttributeInvokedMethods_Attributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeInvokedMethods_InvokedMethods_InvokedMethodId",
                        column: x => x.InvokedMethodId,
                        principalTable: "InvokedMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocalVariableInvokedMethods",
                columns: table => new
                {
                    LocalVariableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvokedMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalVariableInvokedMethods", x => new { x.LocalVariableId, x.InvokedMethodId });
                    table.ForeignKey(
                        name: "FK_LocalVariableInvokedMethods_InvokedMethods_InvokedMethodId",
                        column: x => x.InvokedMethodId,
                        principalTable: "InvokedMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocalVariableInvokedMethods_LocalVariables_LocalVariableId",
                        column: x => x.LocalVariableId,
                        principalTable: "LocalVariables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParameterInvokedMethods",
                columns: table => new
                {
                    ParameterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvokedMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterInvokedMethods", x => new { x.ParameterId, x.InvokedMethodId });
                    table.ForeignKey(
                        name: "FK_ParameterInvokedMethods_InvokedMethods_InvokedMethodId",
                        column: x => x.InvokedMethodId,
                        principalTable: "InvokedMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParameterInvokedMethods_Parameters_ParameterId",
                        column: x => x.ParameterId,
                        principalTable: "Parameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "NamespaceComponents",
                columns: new[] { "Id", "Discriminator", "Name", "NameSpaceId", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("15bbb320-f7ba-4eb9-a1c2-fe50e686a8a2"), "Primitive", "nuint", null, 10 },
                    { new Guid("193a23d9-c50d-4ba7-b844-fc5ec3d895f7"), "Primitive", "ulong", null, 12 },
                    { new Guid("1be58ce2-cbe6-4920-8f64-1a2b5b835463"), "Primitive", "char", null, 3 },
                    { new Guid("307bbe68-1fbe-4cf6-8021-901447e2c86e"), "Primitive", "sByte", null, 2 },
                    { new Guid("50c36b2c-c85e-4cc5-890a-7f74e8583106"), "Primitive", "ushort", null, 14 },
                    { new Guid("587053c5-2c34-4a2e-8ae8-e3912d9f4bfa"), "Primitive", "byte", null, 1 },
                    { new Guid("9dcf4da4-63cc-49e0-bf69-eb14ee58db15"), "Primitive", "short", null, 13 },
                    { new Guid("9fa445e3-b1f8-4170-8d9d-ca665b910582"), "Primitive", "float", null, 6 },
                    { new Guid("a9cea71b-22f5-4180-b072-5dad4cde24ff"), "Primitive", "nint", null, 9 },
                    { new Guid("b43be84c-8e22-4620-860a-406ad11d22ea"), "Primitive", "long", null, 11 },
                    { new Guid("c18439af-1687-4f4d-b71d-13a780ac53c6"), "Primitive", "uint", null, 8 },
                    { new Guid("c19d93c2-1ef1-45b8-99e8-8cf966f6e493"), "Primitive", "decimal", null, 4 },
                    { new Guid("d0008295-c786-444c-9b44-c7cd9c00d534"), "Primitive", "double", null, 5 }
                });

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InstanceTypeId", "MethodId", "MethodSignatureId", "Name", "ReferenceTypeId" },
                values: new object[,]
                {
                    { new Guid("1fccd0b8-6fb1-4026-b731-bd16d9dc0e84"), null, new Guid("77777777-7777-7777-7777-777777777777"), null, "StrToCompare", null },
                    { new Guid("7dc6d7cc-1abf-4e6a-b8f0-9b338795709e"), null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), null, "Obj", null },
                    { new Guid("8f99186b-3329-4fac-8fc2-84e55ffefe72"), null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), null, "StrIsNullOrEmpty", null },
                    { new Guid("b7a509dc-88a9-4121-b359-4b4bc3915b2f"), null, new Guid("44444444-4444-4444-4444-444444444444"), null, "StrToParse", null },
                    { new Guid("d7b3538e-3414-4005-a1fb-df9ce5159901"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, "Obj", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeInvokedMethods_InvokedMethodId",
                table: "AttributeInvokedMethods",
                column: "InvokedMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalVariableInvokedMethods_InvokedMethodId",
                table: "LocalVariableInvokedMethods",
                column: "InvokedMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterInvokedMethods_InvokedMethodId",
                table: "ParameterInvokedMethods",
                column: "InvokedMethodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributeInvokedMethods");

            migrationBuilder.DropTable(
                name: "LocalVariableInvokedMethods");

            migrationBuilder.DropTable(
                name: "ParameterInvokedMethods");

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("15bbb320-f7ba-4eb9-a1c2-fe50e686a8a2"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("193a23d9-c50d-4ba7-b844-fc5ec3d895f7"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("1be58ce2-cbe6-4920-8f64-1a2b5b835463"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("307bbe68-1fbe-4cf6-8021-901447e2c86e"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("50c36b2c-c85e-4cc5-890a-7f74e8583106"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("587053c5-2c34-4a2e-8ae8-e3912d9f4bfa"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("9dcf4da4-63cc-49e0-bf69-eb14ee58db15"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("9fa445e3-b1f8-4170-8d9d-ca665b910582"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("a9cea71b-22f5-4180-b072-5dad4cde24ff"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("b43be84c-8e22-4620-860a-406ad11d22ea"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("c18439af-1687-4f4d-b71d-13a780ac53c6"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("c19d93c2-1ef1-45b8-99e8-8cf966f6e493"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("d0008295-c786-444c-9b44-c7cd9c00d534"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("1fccd0b8-6fb1-4026-b731-bd16d9dc0e84"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("7dc6d7cc-1abf-4e6a-b8f0-9b338795709e"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("8f99186b-3329-4fac-8fc2-84e55ffefe72"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("b7a509dc-88a9-4121-b359-4b4bc3915b2f"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("d7b3538e-3414-4005-a1fb-df9ce5159901"));

            migrationBuilder.AddColumn<Guid>(
                name: "InvokedMethodId",
                table: "Parameters",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InvokedMethodId",
                table: "LocalVariables",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InvokedMethodId",
                table: "Attributes",
                type: "uniqueidentifier",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_InvokedMethodId",
                table: "Parameters",
                column: "InvokedMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalVariables_InvokedMethodId",
                table: "LocalVariables",
                column: "InvokedMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_InvokedMethodId",
                table: "Attributes",
                column: "InvokedMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_InvokedMethods_InvokedMethodId",
                table: "Attributes",
                column: "InvokedMethodId",
                principalTable: "InvokedMethods",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalVariables_InvokedMethods_InvokedMethodId",
                table: "LocalVariables",
                column: "InvokedMethodId",
                principalTable: "InvokedMethods",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parameters_InvokedMethods_InvokedMethodId",
                table: "Parameters",
                column: "InvokedMethodId",
                principalTable: "InvokedMethods",
                principalColumn: "Id");
        }
    }
}
