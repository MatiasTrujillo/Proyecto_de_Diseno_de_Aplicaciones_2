using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class NinthMigration : Migration
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

            migrationBuilder.AddColumn<int>(
                name: "Stereotype",
                table: "Attributes",
                type: "int",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Stereotype",
                table: "Attributes");

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
