using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class NameSpaceMigrationFix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "FatherId",
                value: null);

            migrationBuilder.UpdateData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "FatherId",
                value: null);

            migrationBuilder.UpdateData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "FatherId",
                value: null);

            migrationBuilder.UpdateData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                column: "FatherId",
                value: null);

            migrationBuilder.InsertData(
                table: "NamespaceComponents",
                columns: new[] { "Id", "Discriminator", "FatherId", "Name", "NameSpaceId", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("154bc4ab-a39d-41d8-9fa4-79728a06f800"), "Primitive", null, "short", null, 13 },
                    { new Guid("34b4bfe1-5fc4-4fb0-a9ea-418526065ff2"), "Primitive", null, "decimal", null, 4 },
                    { new Guid("3950342c-3ba7-4f00-a2c4-53d2d87c75f5"), "Primitive", null, "char", null, 3 },
                    { new Guid("4f78bfb5-5867-464f-bb86-c89478a5c86f"), "Primitive", null, "double", null, 5 },
                    { new Guid("63af80de-e125-49e0-a02e-2efcff87b388"), "Primitive", null, "float", null, 6 },
                    { new Guid("6c1b9329-0e7c-43aa-b621-1e8748a3accb"), "Primitive", null, "long", null, 11 },
                    { new Guid("7de22a88-4ceb-471a-b341-afc9af1eb698"), "Primitive", null, "nuint", null, 10 },
                    { new Guid("7e659cf0-3644-4891-a709-bf31b5a65326"), "Primitive", null, "sByte", null, 2 },
                    { new Guid("841f3c1d-7365-4fc2-98c2-1fbfaf1b9161"), "Primitive", null, "ushort", null, 14 },
                    { new Guid("9db30a93-5a56-43ba-91c5-0e4db02550a0"), "Primitive", null, "uint", null, 8 },
                    { new Guid("a320df93-177b-414d-9202-e52810a978e8"), "Primitive", null, "ulong", null, 12 },
                    { new Guid("aebbefe9-dc19-4a03-9cd1-51bc408c905c"), "Primitive", null, "nint", null, 9 },
                    { new Guid("f65f5a32-bd85-4c95-9bc5-a94d26ab70f8"), "Primitive", null, "byte", null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InstanceTypeId", "MethodId", "MethodSignatureId", "Name", "ReferenceTypeId" },
                values: new object[,]
                {
                    { new Guid("1f743eba-9fcc-4d00-94dd-7b1a26691824"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, "Obj", null },
                    { new Guid("2d27ba04-142a-4345-a47d-7546ee3d576a"), null, new Guid("77777777-7777-7777-7777-777777777777"), null, "StrToCompare", null },
                    { new Guid("5eee9d73-0305-4f90-86c3-b53a2ded63d2"), null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), null, "StrIsNullOrEmpty", null },
                    { new Guid("91729c0d-2b9b-42e5-9b7e-684e3291e92c"), null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), null, "Obj", null },
                    { new Guid("ef548653-ee86-473b-bc31-967f405bb398"), null, new Guid("44444444-4444-4444-4444-444444444444"), null, "StrToParse", null }
                });

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Token",
                keyValue: new Guid("2070a73f-97a1-4365-a231-dca969fb52fb"),
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Token",
                keyValue: new Guid("231ecdac-9680-4772-a95a-29abdfd4ad5f"),
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Token",
                keyValue: new Guid("5c8f91eb-5bd1-4706-96ca-cfcec81473b8"),
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Token",
                keyValue: new Guid("66d9652f-3585-4982-9ab7-8d8159197147"),
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Token",
                keyValue: new Guid("675c1f61-9dfa-4ead-bae4-dc432ee2bd32"),
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Token",
                keyValue: new Guid("756ec724-3e51-4e0d-a857-cceee846a5f6"),
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Token",
                keyValue: new Guid("a4a20350-e7e1-4080-b4da-75e875af6b61"),
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Token",
                keyValue: new Guid("b142d2d6-ac0c-4af1-b272-c03e431555f3"),
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Token",
                keyValue: new Guid("f6e32298-4362-4ef1-9e6a-e2dfc9e58a47"),
                column: "IsActive",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("154bc4ab-a39d-41d8-9fa4-79728a06f800"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("34b4bfe1-5fc4-4fb0-a9ea-418526065ff2"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("3950342c-3ba7-4f00-a2c4-53d2d87c75f5"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("4f78bfb5-5867-464f-bb86-c89478a5c86f"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("63af80de-e125-49e0-a02e-2efcff87b388"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("6c1b9329-0e7c-43aa-b621-1e8748a3accb"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("7de22a88-4ceb-471a-b341-afc9af1eb698"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("7e659cf0-3644-4891-a709-bf31b5a65326"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("841f3c1d-7365-4fc2-98c2-1fbfaf1b9161"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("9db30a93-5a56-43ba-91c5-0e4db02550a0"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("a320df93-177b-414d-9202-e52810a978e8"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("aebbefe9-dc19-4a03-9cd1-51bc408c905c"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("f65f5a32-bd85-4c95-9bc5-a94d26ab70f8"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("1f743eba-9fcc-4d00-94dd-7b1a26691824"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("2d27ba04-142a-4345-a47d-7546ee3d576a"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("5eee9d73-0305-4f90-86c3-b53a2ded63d2"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("91729c0d-2b9b-42e5-9b7e-684e3291e92c"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("ef548653-ee86-473b-bc31-967f405bb398"));

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

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Token",
                keyValue: new Guid("2070a73f-97a1-4365-a231-dca969fb52fb"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Token",
                keyValue: new Guid("231ecdac-9680-4772-a95a-29abdfd4ad5f"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Token",
                keyValue: new Guid("5c8f91eb-5bd1-4706-96ca-cfcec81473b8"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Token",
                keyValue: new Guid("66d9652f-3585-4982-9ab7-8d8159197147"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Token",
                keyValue: new Guid("675c1f61-9dfa-4ead-bae4-dc432ee2bd32"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Token",
                keyValue: new Guid("756ec724-3e51-4e0d-a857-cceee846a5f6"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Token",
                keyValue: new Guid("a4a20350-e7e1-4080-b4da-75e875af6b61"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Token",
                keyValue: new Guid("b142d2d6-ac0c-4af1-b272-c03e431555f3"),
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Token",
                keyValue: new Guid("f6e32298-4362-4ef1-9e6a-e2dfc9e58a47"),
                column: "IsActive",
                value: true);
        }
    }
}
