using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class NameSpaceMigrationFix4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "NameSpace_Name",
                table: "NamespaceComponents");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "NamespaceComponents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab"),
                column: "Name",
                value: "System");

            migrationBuilder.InsertData(
                table: "NamespaceComponents",
                columns: new[] { "Id", "Discriminator", "FatherId", "Name", "NameSpaceId", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("2d06bda1-0835-4106-81aa-4ed8f891b6f0"), "Primitive", null, "double", null, 5 },
                    { new Guid("40a2dec0-9cb7-4be4-97a1-c492a25a62c6"), "Primitive", null, "long", null, 11 },
                    { new Guid("4c19d95b-94f7-4327-a3a8-6b2f81ba84a4"), "Primitive", null, "nuint", null, 10 },
                    { new Guid("81ea290d-77e4-4317-b3c9-c02e61facad8"), "Primitive", null, "byte", null, 1 },
                    { new Guid("840025fa-2f01-4a3e-abb9-8ddfad689868"), "Primitive", null, "sByte", null, 2 },
                    { new Guid("9888e6be-ec95-4a3e-9e4d-de209af372fb"), "Primitive", null, "char", null, 3 },
                    { new Guid("a5de54fc-787e-465f-8b97-774eab3be85c"), "Primitive", null, "short", null, 13 },
                    { new Guid("ae45f5f1-b17f-4bf0-b2fb-77118022b337"), "Primitive", null, "ushort", null, 14 },
                    { new Guid("af759eee-0853-4591-8d70-0daa710edace"), "Primitive", null, "nint", null, 9 },
                    { new Guid("c350797a-3a3d-434f-baa2-a5d91f8505b1"), "Primitive", null, "ulong", null, 12 },
                    { new Guid("dbb85fdb-ee6e-4054-a7c4-e3a6527e4fd5"), "Primitive", null, "float", null, 6 },
                    { new Guid("e32a3d91-5331-4f31-9ba5-bb88e5ea19cc"), "Primitive", null, "uint", null, 8 },
                    { new Guid("f8e718da-df25-4a5b-8205-f2a7733d28a0"), "Primitive", null, "decimal", null, 4 }
                });

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InstanceTypeId", "MethodId", "MethodSignatureId", "Name", "ReferenceTypeId" },
                values: new object[,]
                {
                    { new Guid("1bad35ba-7569-42d4-890f-f38f8a61c02e"), null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), null, "Obj", null },
                    { new Guid("9758b434-697d-4616-a2ef-240f2a17fe0c"), null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), null, "StrIsNullOrEmpty", null },
                    { new Guid("aa208b53-5bac-4cbf-9288-f10d83b8b8a8"), null, new Guid("44444444-4444-4444-4444-444444444444"), null, "StrToParse", null },
                    { new Guid("d12e6e08-36bc-43a6-b23c-fe130d2207a0"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, "Obj", null },
                    { new Guid("fbd9d846-31b4-46a8-899c-86448d46c35c"), null, new Guid("77777777-7777-7777-7777-777777777777"), null, "StrToCompare", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("2d06bda1-0835-4106-81aa-4ed8f891b6f0"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("40a2dec0-9cb7-4be4-97a1-c492a25a62c6"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("4c19d95b-94f7-4327-a3a8-6b2f81ba84a4"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("81ea290d-77e4-4317-b3c9-c02e61facad8"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("840025fa-2f01-4a3e-abb9-8ddfad689868"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("9888e6be-ec95-4a3e-9e4d-de209af372fb"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("a5de54fc-787e-465f-8b97-774eab3be85c"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("ae45f5f1-b17f-4bf0-b2fb-77118022b337"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("af759eee-0853-4591-8d70-0daa710edace"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("c350797a-3a3d-434f-baa2-a5d91f8505b1"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("dbb85fdb-ee6e-4054-a7c4-e3a6527e4fd5"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("e32a3d91-5331-4f31-9ba5-bb88e5ea19cc"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("f8e718da-df25-4a5b-8205-f2a7733d28a0"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("1bad35ba-7569-42d4-890f-f38f8a61c02e"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("9758b434-697d-4616-a2ef-240f2a17fe0c"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("aa208b53-5bac-4cbf-9288-f10d83b8b8a8"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("d12e6e08-36bc-43a6-b23c-fe130d2207a0"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("fbd9d846-31b4-46a8-899c-86448d46c35c"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "NamespaceComponents",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "NameSpace_Name",
                table: "NamespaceComponents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab"),
                column: "NameSpace_Name",
                value: "System");

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
        }
    }
}
