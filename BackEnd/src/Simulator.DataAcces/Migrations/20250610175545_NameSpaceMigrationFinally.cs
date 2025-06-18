using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class NameSpaceMigrationFinally : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "NamespaceComponents",
                columns: new[] { "Id", "Discriminator", "FatherId", "Name", "NameSpaceId", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("18cc1336-9de9-40d4-b495-e1ab6e31c326"), "Primitive", null, "ushort", null, 14 },
                    { new Guid("2c2f5ed2-5750-4361-9f90-ad56202f7091"), "Primitive", null, "decimal", null, 4 },
                    { new Guid("3e469430-390e-409d-8985-435a350d73aa"), "Primitive", null, "short", null, 13 },
                    { new Guid("4350fe20-97b7-47ab-ae71-43e8d762cdc7"), "Primitive", null, "float", null, 6 },
                    { new Guid("4ed61a16-7254-4cf0-9c9d-44bcde35e1a6"), "Primitive", null, "nint", null, 9 },
                    { new Guid("56718bb1-6ebd-4c80-9965-59d476c88133"), "Primitive", null, "uint", null, 8 },
                    { new Guid("5b939986-61ca-4a22-90ba-de6700397b7d"), "Primitive", null, "ulong", null, 12 },
                    { new Guid("7586c9d9-ee70-4d98-847c-fe061d5f1563"), "Primitive", null, "sByte", null, 2 },
                    { new Guid("8b05efb8-4404-4a32-910b-781f2329abb4"), "Primitive", null, "nuint", null, 10 },
                    { new Guid("d230193d-49e7-4fed-8cf6-9f18d03d8d3b"), "Primitive", null, "byte", null, 1 },
                    { new Guid("da4ae3ae-a766-4f94-86ba-c565766df053"), "Primitive", null, "long", null, 11 },
                    { new Guid("e226c6be-499e-4859-8c00-354d297968a6"), "Primitive", null, "char", null, 3 },
                    { new Guid("e46aae45-0156-4a2f-8523-4088a3d1830d"), "Primitive", null, "double", null, 5 }
                });

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InstanceTypeId", "MethodId", "MethodSignatureId", "Name", "ReferenceTypeId" },
                values: new object[,]
                {
                    { new Guid("09dede5c-9b3a-43c3-a23e-66a1cbe68dfc"), null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), null, "Obj", null },
                    { new Guid("18ea72c6-9609-48a9-8035-9a3fa414c007"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, "Obj", null },
                    { new Guid("3c7cf80f-33a8-4664-a30d-922dacfdcd01"), null, new Guid("77777777-7777-7777-7777-777777777777"), null, "StrToCompare", null },
                    { new Guid("6cf8f20a-141a-400a-8ee0-963f5703a080"), null, new Guid("44444444-4444-4444-4444-444444444444"), null, "StrToParse", null },
                    { new Guid("a3ced9f6-1790-47b4-9214-4807d80f6c5b"), null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), null, "StrIsNullOrEmpty", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("18cc1336-9de9-40d4-b495-e1ab6e31c326"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("2c2f5ed2-5750-4361-9f90-ad56202f7091"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("3e469430-390e-409d-8985-435a350d73aa"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("4350fe20-97b7-47ab-ae71-43e8d762cdc7"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("4ed61a16-7254-4cf0-9c9d-44bcde35e1a6"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("56718bb1-6ebd-4c80-9965-59d476c88133"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("5b939986-61ca-4a22-90ba-de6700397b7d"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("7586c9d9-ee70-4d98-847c-fe061d5f1563"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("8b05efb8-4404-4a32-910b-781f2329abb4"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("d230193d-49e7-4fed-8cf6-9f18d03d8d3b"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("da4ae3ae-a766-4f94-86ba-c565766df053"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("e226c6be-499e-4859-8c00-354d297968a6"));

            migrationBuilder.DeleteData(
                table: "NamespaceComponents",
                keyColumn: "Id",
                keyValue: new Guid("e46aae45-0156-4a2f-8523-4088a3d1830d"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("09dede5c-9b3a-43c3-a23e-66a1cbe68dfc"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("18ea72c6-9609-48a9-8035-9a3fa414c007"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("3c7cf80f-33a8-4664-a30d-922dacfdcd01"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("6cf8f20a-141a-400a-8ee0-963f5703a080"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("a3ced9f6-1790-47b4-9214-4807d80f6c5b"));

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
    }
}
