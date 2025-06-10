using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class NameSpaceMigrationFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "NamespaceComponents",
                columns: new[] { "Id", "Discriminator", "Name", "NameSpaceId", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("01196b2b-ba9e-4402-8cf4-e904879b188f"), "Primitive", "sByte", null, 2 },
                    { new Guid("01ee0727-ab17-4f67-9e63-ab40d3f46b44"), "Primitive", "byte", null, 1 },
                    { new Guid("2c4378d4-c216-46a3-8565-abb7f5f15475"), "Primitive", "ulong", null, 12 },
                    { new Guid("438e0819-e1e1-4505-aff4-b37c2f5a3fd5"), "Primitive", "float", null, 6 },
                    { new Guid("4c4e414b-be77-4926-8320-03241a82f439"), "Primitive", "double", null, 5 },
                    { new Guid("5fec940b-987f-4121-b703-2462233ebb1d"), "Primitive", "nint", null, 9 },
                    { new Guid("7f6c1e17-0adb-4fd8-bb2d-3ac52cbbe948"), "Primitive", "uint", null, 8 },
                    { new Guid("834f988d-1177-435b-948d-c5eccbccca96"), "Primitive", "ushort", null, 14 },
                    { new Guid("8b9a39e2-35a2-4b2a-8ac1-284b43407f2f"), "Primitive", "char", null, 3 },
                    { new Guid("93a737f9-4b71-4d20-99d5-975f1355e02d"), "Primitive", "short", null, 13 },
                    { new Guid("a65aac5c-82d1-43ff-a3fc-8499340c5837"), "Primitive", "decimal", null, 4 },
                    { new Guid("fcc0348e-5273-4d4a-a163-a3c2794de300"), "Primitive", "long", null, 11 },
                    { new Guid("ff2f094d-bf61-4856-81a9-1fcc920bd9f9"), "Primitive", "nuint", null, 10 }
                });

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InstanceTypeId", "MethodId", "MethodSignatureId", "Name", "ReferenceTypeId" },
                values: new object[,]
                {
                    { new Guid("50d6edd2-fc33-4fc6-a77f-fcb79f20d0e3"), null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), null, "StrIsNullOrEmpty", null },
                    { new Guid("5bbf2561-6dba-43a9-bcd1-f221ea939300"), null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), null, "Obj", null },
                    { new Guid("7fc8c2c8-01f6-4a77-aec2-5cff890c943f"), null, new Guid("77777777-7777-7777-7777-777777777777"), null, "StrToCompare", null },
                    { new Guid("9b6e7231-d5fd-4621-9cc4-a6390ab197c3"), null, new Guid("44444444-4444-4444-4444-444444444444"), null, "StrToParse", null },
                    { new Guid("d96be760-87a7-48b5-b03e-0a986d8f89e2"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, "Obj", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("01196b2b-ba9e-4402-8cf4-e904879b188f"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("01ee0727-ab17-4f67-9e63-ab40d3f46b44"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("2c4378d4-c216-46a3-8565-abb7f5f15475"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("438e0819-e1e1-4505-aff4-b37c2f5a3fd5"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("4c4e414b-be77-4926-8320-03241a82f439"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("5fec940b-987f-4121-b703-2462233ebb1d"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("7f6c1e17-0adb-4fd8-bb2d-3ac52cbbe948"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("834f988d-1177-435b-948d-c5eccbccca96"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("8b9a39e2-35a2-4b2a-8ac1-284b43407f2f"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("93a737f9-4b71-4d20-99d5-975f1355e02d"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("a65aac5c-82d1-43ff-a3fc-8499340c5837"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("fcc0348e-5273-4d4a-a163-a3c2794de300"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("ff2f094d-bf61-4856-81a9-1fcc920bd9f9"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("50d6edd2-fc33-4fc6-a77f-fcb79f20d0e3"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("5bbf2561-6dba-43a9-bcd1-f221ea939300"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("7fc8c2c8-01f6-4a77-aec2-5cff890c943f"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("9b6e7231-d5fd-4621-9cc4-a6390ab197c3"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("d96be760-87a7-48b5-b03e-0a986d8f89e2"));

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
        }
    }
}
