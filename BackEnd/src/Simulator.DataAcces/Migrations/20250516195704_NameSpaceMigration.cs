using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class NameSpaceMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("26608c4b-a761-466e-8005-806e9c202b20"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("4e63dae4-88c5-4dec-b78f-87326c4fcad0"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("6d274937-8191-418d-a192-4b591e95f0ce"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("77a1cbdc-156b-4590-a6e5-17871c85210a"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("871ce097-c9a9-479a-884c-4d5a49c219be"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("1242c908-143a-4561-878b-c0a388a71a32"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("160fbbb3-de99-40cf-93cf-dce902a3450f"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("1edd413c-e9aa-4c95-ba54-ef5b61bc76ff"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("22ee83a8-9128-4c10-b2ee-53d49f8b198e"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("24c9469d-b7ab-40f3-8498-0ccffc3c905d"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("27721149-2238-4b82-8d35-16a98e99e2b6"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("43dc7689-0497-4c6f-a9e1-662eca686721"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("9922523d-bc98-4f70-8123-dc7c7c800c49"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("b39e7657-2b97-40dd-a41f-af9d44ce7074"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("cd2da086-6e7f-4427-a524-10390a0554ea"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("d05d3351-6440-470f-9ba3-64f8ed390892"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("e2126779-aeee-4857-8787-e7def842060b"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("ec748eb6-b4d4-4ce5-875f-de0a9dab699d"));

            migrationBuilder.AddColumn<Guid>(
                name: "NameSpaceId",
                table: "Type",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NameSpaces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                columns: new[] { "Id", "InvokedMethodId", "MethodId", "Name", "ParameterTypeId" },
                values: new object[,]
                {
                    { new Guid("1a1064c6-2cdb-42c2-bd1e-d1d07f23c79f"), null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), "StrIsNullOrEmpty", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("495d869b-1c64-4faf-9327-fce2db796149"), null, new Guid("77777777-7777-7777-7777-777777777777"), "StrToCompare", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("6c84ef36-d185-4d4f-9977-46f224573ede"), null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("c4e4d4d8-b17e-47d1-996e-ce928ec9c172"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("dbb49ef0-ab9e-4fed-8b07-01ec13974119"), null, new Guid("44444444-4444-4444-4444-444444444444"), "StrToParse", new Guid("22222222-2222-2222-2222-222222222222") }
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
                    { new Guid("0adb1525-fcd4-4e86-9abe-6004364d2f17"), "Primitive", "nint", 9 },
                    { new Guid("1110bd16-ee93-4cd4-8480-9a2b16086547"), "Primitive", "char", 3 },
                    { new Guid("2fca8914-b4ee-43cc-a981-e2cd4f5e77d7"), "Primitive", "ushort", 14 },
                    { new Guid("40c05930-de07-4141-b1c4-5b984168830f"), "Primitive", "short", 13 },
                    { new Guid("538ed9ea-b6b7-4841-a203-7406ecced0a9"), "Primitive", "uint", 8 },
                    { new Guid("55ed0daf-26e4-463d-8133-9525a2602dc4"), "Primitive", "nuint", 10 },
                    { new Guid("5bc87b35-20bf-4a5c-b006-838c1fa1331f"), "Primitive", "float", 6 },
                    { new Guid("6f15a0d8-02eb-4373-8c92-438c915af85e"), "Primitive", "ulong", 12 },
                    { new Guid("745f42e2-2ef1-463f-8426-c8e4c274d457"), "Primitive", "decimal", 4 },
                    { new Guid("87551ed3-e823-4d57-87a4-56a2e87596df"), "Primitive", "byte", 1 },
                    { new Guid("8a730f9b-be6c-41fc-bd2d-02f1eadeee64"), "Primitive", "double", 5 },
                    { new Guid("a39c5048-243c-48fc-bde8-89e77c24751d"), "Primitive", "sByte", 2 },
                    { new Guid("f01a1f88-170e-4b8a-a3f0-d0afb49a2396"), "Primitive", "long", 11 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Type_NameSpaceId",
                table: "Type",
                column: "NameSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_NameSpaces_FatherId",
                table: "NameSpaces",
                column: "FatherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Type_NameSpaces_NameSpaceId",
                table: "Type",
                column: "NameSpaceId",
                principalTable: "NameSpaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Type_NameSpaces_NameSpaceId",
                table: "Type");

            migrationBuilder.DropTable(
                name: "NameSpaces");

            migrationBuilder.DropIndex(
                name: "IX_Type_NameSpaceId",
                table: "Type");

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("1a1064c6-2cdb-42c2-bd1e-d1d07f23c79f"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("495d869b-1c64-4faf-9327-fce2db796149"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("6c84ef36-d185-4d4f-9977-46f224573ede"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("c4e4d4d8-b17e-47d1-996e-ce928ec9c172"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("dbb49ef0-ab9e-4fed-8b07-01ec13974119"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("0adb1525-fcd4-4e86-9abe-6004364d2f17"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("1110bd16-ee93-4cd4-8480-9a2b16086547"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("2fca8914-b4ee-43cc-a981-e2cd4f5e77d7"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("40c05930-de07-4141-b1c4-5b984168830f"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("538ed9ea-b6b7-4841-a203-7406ecced0a9"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("55ed0daf-26e4-463d-8133-9525a2602dc4"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("5bc87b35-20bf-4a5c-b006-838c1fa1331f"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("6f15a0d8-02eb-4373-8c92-438c915af85e"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("745f42e2-2ef1-463f-8426-c8e4c274d457"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("87551ed3-e823-4d57-87a4-56a2e87596df"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("8a730f9b-be6c-41fc-bd2d-02f1eadeee64"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("a39c5048-243c-48fc-bde8-89e77c24751d"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("f01a1f88-170e-4b8a-a3f0-d0afb49a2396"));

            migrationBuilder.DropColumn(
                name: "NameSpaceId",
                table: "Type");

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InvokedMethodId", "MethodId", "Name", "ParameterTypeId" },
                values: new object[,]
                {
                    { new Guid("26608c4b-a761-466e-8005-806e9c202b20"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("4e63dae4-88c5-4dec-b78f-87326c4fcad0"), null, new Guid("77777777-7777-7777-7777-777777777777"), "StrToCompare", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("6d274937-8191-418d-a192-4b591e95f0ce"), null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("77a1cbdc-156b-4590-a6e5-17871c85210a"), null, new Guid("44444444-4444-4444-4444-444444444444"), "StrToParse", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("871ce097-c9a9-479a-884c-4d5a49c219be"), null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), "StrIsNullOrEmpty", new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("1242c908-143a-4561-878b-c0a388a71a32"), "Primitive", "sByte", 2 },
                    { new Guid("160fbbb3-de99-40cf-93cf-dce902a3450f"), "Primitive", "float", 6 },
                    { new Guid("1edd413c-e9aa-4c95-ba54-ef5b61bc76ff"), "Primitive", "long", 11 },
                    { new Guid("22ee83a8-9128-4c10-b2ee-53d49f8b198e"), "Primitive", "nint", 9 },
                    { new Guid("24c9469d-b7ab-40f3-8498-0ccffc3c905d"), "Primitive", "decimal", 4 },
                    { new Guid("27721149-2238-4b82-8d35-16a98e99e2b6"), "Primitive", "ulong", 12 },
                    { new Guid("43dc7689-0497-4c6f-a9e1-662eca686721"), "Primitive", "char", 3 },
                    { new Guid("9922523d-bc98-4f70-8123-dc7c7c800c49"), "Primitive", "short", 13 },
                    { new Guid("b39e7657-2b97-40dd-a41f-af9d44ce7074"), "Primitive", "uint", 8 },
                    { new Guid("cd2da086-6e7f-4427-a524-10390a0554ea"), "Primitive", "nuint", 10 },
                    { new Guid("d05d3351-6440-470f-9ba3-64f8ed390892"), "Primitive", "byte", 1 },
                    { new Guid("e2126779-aeee-4857-8787-e7def842060b"), "Primitive", "double", 5 },
                    { new Guid("ec748eb6-b4d4-4ce5-875f-de0a9dab699d"), "Primitive", "ushort", 14 }
                });
        }
    }
}
