using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class EleventhMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<bool>(
                name: "IsOverride",
                table: "Methods",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Methods",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "IsOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "Methods",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "IsOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "Methods",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "IsOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "Methods",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "IsOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "Methods",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                column: "IsOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "Methods",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"),
                column: "IsOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "Methods",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"),
                column: "IsOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "Methods",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "IsOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "Methods",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"),
                column: "IsOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "Methods",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "IsOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "Methods",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-3333-3333-3333-cccccccccccc"),
                column: "IsOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "Methods",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                column: "IsOverride",
                value: false);

            migrationBuilder.UpdateData(
                table: "Methods",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                column: "IsOverride",
                value: false);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "IsOverride",
                table: "Methods");

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InvokedMethodId", "MethodId", "MethodSignatureId", "Name", "ParameterTypeId" },
                values: new object[,]
                {
                    { new Guid("1a1064c6-2cdb-42c2-bd1e-d1d07f23c79f"), null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), null, "StrIsNullOrEmpty", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("495d869b-1c64-4faf-9327-fce2db796149"), null, new Guid("77777777-7777-7777-7777-777777777777"), null, "StrToCompare", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("6c84ef36-d185-4d4f-9977-46f224573ede"), null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("c4e4d4d8-b17e-47d1-996e-ce928ec9c172"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("dbb49ef0-ab9e-4fed-8b07-01ec13974119"), null, new Guid("44444444-4444-4444-4444-444444444444"), null, "StrToParse", new Guid("22222222-2222-2222-2222-222222222222") }
                });

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
        }
    }
}
