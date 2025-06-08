using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class MethodsLocalVariableId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("35ea830d-acc8-45e3-8584-beb3c0c6d3b6"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("63e6eed7-9722-4c63-8554-9d6d247cad0e"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("7523e310-a61a-48dc-9f98-3e3785350a84"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("a0c5d303-9eca-4ef0-9497-6699124200bf"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("d9636bf9-82c8-4d09-92e8-e4179f1988aa"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("24da5399-1132-47d6-b303-bd30e03e6a38"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("28fd5f3e-9175-4f2f-9f51-b2aaa0a256b5"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("3ec4e730-348e-48d9-99aa-06312a4b221c"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("3edc4e39-a9a7-4100-9a66-55d9b205251e"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("4b8b0598-fc5a-4d51-b621-631f705f5b72"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("502e3386-c33d-4ca6-a6da-da0baa91a74a"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("65d8efcd-fccc-4ecf-a13b-9c3bfdc425a5"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("699584e0-3c97-4f14-aca5-49df295f7662"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("8967e2f5-e049-4046-8190-8aee49a6ece1"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("aac6a47f-33d2-4847-8aaf-ba175692cfda"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("bfea30de-69c3-4675-b5c1-5752715a7f27"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("e0ccf6e4-7d36-4c32-b74f-cfa9a3b1781f"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("eb3f43bd-7f57-40a2-8f27-de26a02f6819"));

            migrationBuilder.InsertData(
                table: "LocalVariables",
                columns: new[] { "Id", "InvokedMethodId", "LocalVariableTypeId", "MethodId", "Name" },
                values: new object[] { new Guid("c7946ae1-78e3-4d7d-b3e2-4d2363b20c38"), null, new Guid("33333333-3333-3333-3333-333333333333"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "localVar1" });

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InvokedMethodId", "MethodId", "MethodSignatureId", "Name", "ParameterTypeId" },
                values: new object[,]
                {
                    { new Guid("0d12579c-ebbe-4e7e-adfd-d8221b91ced5"), null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), null, "StrIsNullOrEmpty", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("1aa27ec9-0dbc-4631-96d2-7484e0d89a2b"), null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("22aa2066-ed2b-461f-9ea8-66a61117ef4b"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("3d094f4c-9653-4f88-962d-93ea30584641"), null, new Guid("44444444-4444-4444-4444-444444444444"), null, "StrToParse", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("5fc3d61e-ff49-41e2-b52e-399d84103260"), null, new Guid("77777777-7777-7777-7777-777777777777"), null, "StrToCompare", new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("1021b867-73aa-43ef-98ac-867750cb3fd4"), "Primitive", "float", 6 },
                    { new Guid("124c2af8-5805-4b1d-a981-252f8c89c4d3"), "Primitive", "double", 5 },
                    { new Guid("277096c6-0d3e-44ea-85b5-c9153adfea70"), "Primitive", "byte", 1 },
                    { new Guid("5338af26-f9ee-4d97-bd53-10c7f19b5f5b"), "Primitive", "ulong", 12 },
                    { new Guid("6f289b84-3e1c-4406-a346-fedac090c85f"), "Primitive", "nint", 9 },
                    { new Guid("a4e01781-886b-4a6e-ba62-ee859395569f"), "Primitive", "nuint", 10 },
                    { new Guid("b43c47a9-a973-4359-9ca5-c31bea94e6da"), "Primitive", "short", 13 },
                    { new Guid("b757ee82-a54f-4583-92d2-e4c2340671a4"), "Primitive", "long", 11 },
                    { new Guid("c78a67a0-ed03-4b47-be5e-c95e08c93e16"), "Primitive", "ushort", 14 },
                    { new Guid("c85be602-dfce-4da5-a0ad-4e026c833db0"), "Primitive", "sByte", 2 },
                    { new Guid("d6e7b7d7-5fb2-4954-8309-407fcde6c58d"), "Primitive", "char", 3 },
                    { new Guid("d9e27037-072a-4d94-b725-382167cd0cbe"), "Primitive", "decimal", 4 },
                    { new Guid("fec86091-ac29-4be3-8a80-8b4846d8b8b4"), "Primitive", "uint", 8 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LocalVariables",
                keyColumn: "Id",
                keyValue: new Guid("c7946ae1-78e3-4d7d-b3e2-4d2363b20c38"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("0d12579c-ebbe-4e7e-adfd-d8221b91ced5"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("1aa27ec9-0dbc-4631-96d2-7484e0d89a2b"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("22aa2066-ed2b-461f-9ea8-66a61117ef4b"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("3d094f4c-9653-4f88-962d-93ea30584641"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("5fc3d61e-ff49-41e2-b52e-399d84103260"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("1021b867-73aa-43ef-98ac-867750cb3fd4"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("124c2af8-5805-4b1d-a981-252f8c89c4d3"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("277096c6-0d3e-44ea-85b5-c9153adfea70"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("5338af26-f9ee-4d97-bd53-10c7f19b5f5b"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("6f289b84-3e1c-4406-a346-fedac090c85f"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("a4e01781-886b-4a6e-ba62-ee859395569f"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("b43c47a9-a973-4359-9ca5-c31bea94e6da"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("b757ee82-a54f-4583-92d2-e4c2340671a4"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("c78a67a0-ed03-4b47-be5e-c95e08c93e16"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("c85be602-dfce-4da5-a0ad-4e026c833db0"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("d6e7b7d7-5fb2-4954-8309-407fcde6c58d"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("d9e27037-072a-4d94-b725-382167cd0cbe"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("fec86091-ac29-4be3-8a80-8b4846d8b8b4"));

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InvokedMethodId", "MethodId", "MethodSignatureId", "Name", "ParameterTypeId" },
                values: new object[,]
                {
                    { new Guid("35ea830d-acc8-45e3-8584-beb3c0c6d3b6"), null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("63e6eed7-9722-4c63-8554-9d6d247cad0e"), null, new Guid("77777777-7777-7777-7777-777777777777"), null, "StrToCompare", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("7523e310-a61a-48dc-9f98-3e3785350a84"), null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), null, "StrIsNullOrEmpty", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("a0c5d303-9eca-4ef0-9497-6699124200bf"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("d9636bf9-82c8-4d09-92e8-e4179f1988aa"), null, new Guid("44444444-4444-4444-4444-444444444444"), null, "StrToParse", new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("24da5399-1132-47d6-b303-bd30e03e6a38"), "Primitive", "long", 11 },
                    { new Guid("28fd5f3e-9175-4f2f-9f51-b2aaa0a256b5"), "Primitive", "decimal", 4 },
                    { new Guid("3ec4e730-348e-48d9-99aa-06312a4b221c"), "Primitive", "ushort", 14 },
                    { new Guid("3edc4e39-a9a7-4100-9a66-55d9b205251e"), "Primitive", "uint", 8 },
                    { new Guid("4b8b0598-fc5a-4d51-b621-631f705f5b72"), "Primitive", "sByte", 2 },
                    { new Guid("502e3386-c33d-4ca6-a6da-da0baa91a74a"), "Primitive", "byte", 1 },
                    { new Guid("65d8efcd-fccc-4ecf-a13b-9c3bfdc425a5"), "Primitive", "nint", 9 },
                    { new Guid("699584e0-3c97-4f14-aca5-49df295f7662"), "Primitive", "nuint", 10 },
                    { new Guid("8967e2f5-e049-4046-8190-8aee49a6ece1"), "Primitive", "short", 13 },
                    { new Guid("aac6a47f-33d2-4847-8aaf-ba175692cfda"), "Primitive", "float", 6 },
                    { new Guid("bfea30de-69c3-4675-b5c1-5752715a7f27"), "Primitive", "char", 3 },
                    { new Guid("e0ccf6e4-7d36-4c32-b74f-cfa9a3b1781f"), "Primitive", "double", 5 },
                    { new Guid("eb3f43bd-7f57-40a2-8f27-de26a02f6819"), "Primitive", "ulong", 12 }
                });
        }
    }
}
