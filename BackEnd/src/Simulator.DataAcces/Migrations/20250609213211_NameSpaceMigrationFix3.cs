using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class NameSpaceMigrationFix3 : Migration
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
                    { new Guid("06c35d30-dede-482b-99f4-afda37af757e"), "Primitive", null, "nint", null, 9 },
                    { new Guid("0d2fe030-471f-4179-9edf-c162a820cf3c"), "Primitive", null, "ulong", null, 12 },
                    { new Guid("0d387c34-a1bd-41c3-b9b6-ec20e045cf0a"), "Primitive", null, "long", null, 11 },
                    { new Guid("1249d65d-3d75-42e6-bde8-516662a6d4b1"), "Primitive", null, "byte", null, 1 },
                    { new Guid("2cdf1c88-1c5c-4ba0-94d6-02f9ca6fd58d"), "Primitive", null, "ushort", null, 14 },
                    { new Guid("5bc423fb-45c8-4bf5-840a-26c1c515fd0b"), "Primitive", null, "short", null, 13 },
                    { new Guid("89bfdd57-15b1-49bf-803c-fdfba5df6590"), "Primitive", null, "uint", null, 8 },
                    { new Guid("99072a92-0403-4bf6-a5b4-d74eddafa049"), "Primitive", null, "float", null, 6 },
                    { new Guid("b8671c54-67fd-4d7a-97a9-26cb1234e76f"), "Primitive", null, "double", null, 5 },
                    { new Guid("bd1bce09-aaff-42c6-98fb-cf7aa5bb6895"), "Primitive", null, "sByte", null, 2 },
                    { new Guid("cc6c35fb-2d11-4c6f-986e-d5ed1877fde2"), "Primitive", null, "nuint", null, 10 },
                    { new Guid("d9bc6c28-8702-4ccb-9a62-d3e205d2c369"), "Primitive", null, "char", null, 3 },
                    { new Guid("e4c58af5-eb59-441f-8faf-4c00f794b2c3"), "Primitive", null, "decimal", null, 4 }
                });

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InstanceTypeId", "MethodId", "MethodSignatureId", "Name", "ReferenceTypeId" },
                values: new object[,]
                {
                    { new Guid("38ee69ee-b113-4908-a3bd-606bd7ad602a"), null, new Guid("44444444-4444-4444-4444-444444444444"), null, "StrToParse", null },
                    { new Guid("8c006d2b-3fdf-4eea-aa2a-7aaa17cfb054"), null, new Guid("77777777-7777-7777-7777-777777777777"), null, "StrToCompare", null },
                    { new Guid("b354d596-0c7e-4b3c-b6b0-2e478af963fa"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, "Obj", null },
                    { new Guid("cfbaee16-0464-4ed1-8159-871985d5eae0"), null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), null, "Obj", null },
                    { new Guid("ffb1dd36-e4a6-4eba-ab3b-d448f6f7db62"), null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), null, "StrIsNullOrEmpty", null }
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
                keyValue: new Guid("06c35d30-dede-482b-99f4-afda37af757e"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("0d2fe030-471f-4179-9edf-c162a820cf3c"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("0d387c34-a1bd-41c3-b9b6-ec20e045cf0a"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("1249d65d-3d75-42e6-bde8-516662a6d4b1"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("2cdf1c88-1c5c-4ba0-94d6-02f9ca6fd58d"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("5bc423fb-45c8-4bf5-840a-26c1c515fd0b"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("89bfdd57-15b1-49bf-803c-fdfba5df6590"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("99072a92-0403-4bf6-a5b4-d74eddafa049"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("b8671c54-67fd-4d7a-97a9-26cb1234e76f"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("bd1bce09-aaff-42c6-98fb-cf7aa5bb6895"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("cc6c35fb-2d11-4c6f-986e-d5ed1877fde2"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("d9bc6c28-8702-4ccb-9a62-d3e205d2c369"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("e4c58af5-eb59-441f-8faf-4c00f794b2c3"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("38ee69ee-b113-4908-a3bd-606bd7ad602a"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("8c006d2b-3fdf-4eea-aa2a-7aaa17cfb054"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("b354d596-0c7e-4b3c-b6b0-2e478af963fa"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("cfbaee16-0464-4ed1-8159-871985d5eae0"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("ffb1dd36-e4a6-4eba-ab3b-d448f6f7db62"));

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
