using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("a7ca113e-f6d9-480f-b008-5f4ae1913c07"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("01820ae6-18b0-45bb-b357-afe004cd4dc4"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("04137227-fecc-4e94-9977-34a424bb86f6"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("3266c213-9bfa-499e-9112-7914a1b18fa0"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("3a594898-b4e3-45ea-bf52-ad81e047bdec"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("3ac3234b-ee84-4ad5-873f-33c1ed165c59"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("433bd90b-f076-4736-8366-08949cc4c3d6"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("59075d40-d193-46cb-94a4-4e1cbc2dff54"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("a570de13-013e-48a1-a20c-b048a08aaf79"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("a76f684d-ceba-4fb4-8f0c-35f6fafbfb30"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("ceac46b7-1983-4d9f-ba11-ac5267c859a3"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("d13a84fd-b311-4557-a9d0-c7ce386b0706"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("e71c4e14-8924-47b1-8f2f-4172146d52ca"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("f362917b-0eab-4781-b058-41fb7eaec2aa"));

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
                table: "Parameters",
                columns: new[] { "Id", "InvokedMethodId", "MethodId", "Name", "ParameterTypeId" },
                values: new object[] { new Guid("f3823f23-801b-41ec-94b9-c47c9c55931a"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("09d7b371-34fd-4af4-b14b-e286b06ba0c0"), "Primitive", "decimal", 4 },
                    { new Guid("1b09d8f3-7186-496c-82d3-ee6cbec3903b"), "Primitive", "ushort", 14 },
                    { new Guid("24ec5780-ea54-4a0b-9d27-987ba8885dc8"), "Primitive", "ulong", 12 },
                    { new Guid("33545d42-4251-4063-971e-544c8da9b665"), "Primitive", "char", 3 },
                    { new Guid("66b744e8-0738-4fcf-b208-de685d342a6e"), "Primitive", "nuint", 10 },
                    { new Guid("7b5f9c73-739a-45f9-8f9b-0a442704607e"), "Primitive", "short", 13 },
                    { new Guid("95a689ff-a22c-4a8f-be57-d2bf36e09543"), "Primitive", "byte", 1 },
                    { new Guid("a37591c2-25c6-4f38-a8c6-3047eee41a17"), "Primitive", "uint", 8 },
                    { new Guid("b91a124f-123b-4972-bfdd-06339f658a80"), "Primitive", "float", 6 },
                    { new Guid("bee6c5fd-de63-4862-987a-33d2789309a5"), "Primitive", "long", 11 },
                    { new Guid("dead961b-1977-4587-9a4d-c14a0fabff52"), "Primitive", "double", 5 },
                    { new Guid("e61e9302-6c22-48a8-a0b0-90ae04795f0d"), "Primitive", "sByte", 2 },
                    { new Guid("f6f50d9f-fd26-40c6-a450-e436aaf07501"), "Primitive", "nint", 9 }
                });

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
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_LocalVariables_InvokedMethods_InvokedMethodId",
                table: "LocalVariables",
                column: "InvokedMethodId",
                principalTable: "InvokedMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Parameters_InvokedMethods_InvokedMethodId",
                table: "Parameters",
                column: "InvokedMethodId",
                principalTable: "InvokedMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("f3823f23-801b-41ec-94b9-c47c9c55931a"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("09d7b371-34fd-4af4-b14b-e286b06ba0c0"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("1b09d8f3-7186-496c-82d3-ee6cbec3903b"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("24ec5780-ea54-4a0b-9d27-987ba8885dc8"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("33545d42-4251-4063-971e-544c8da9b665"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("66b744e8-0738-4fcf-b208-de685d342a6e"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("7b5f9c73-739a-45f9-8f9b-0a442704607e"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("95a689ff-a22c-4a8f-be57-d2bf36e09543"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("a37591c2-25c6-4f38-a8c6-3047eee41a17"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("b91a124f-123b-4972-bfdd-06339f658a80"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("bee6c5fd-de63-4862-987a-33d2789309a5"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("dead961b-1977-4587-9a4d-c14a0fabff52"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("e61e9302-6c22-48a8-a0b0-90ae04795f0d"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("f6f50d9f-fd26-40c6-a450-e436aaf07501"));

            migrationBuilder.DropColumn(
                name: "InvokedMethodId",
                table: "Parameters");

            migrationBuilder.DropColumn(
                name: "InvokedMethodId",
                table: "LocalVariables");

            migrationBuilder.DropColumn(
                name: "InvokedMethodId",
                table: "Attributes");

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "MethodId", "Name", "ParameterTypeId" },
                values: new object[] { new Guid("a7ca113e-f6d9-480f-b008-5f4ae1913c07"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("01820ae6-18b0-45bb-b357-afe004cd4dc4"), "Primitive", "nuint", 10 },
                    { new Guid("04137227-fecc-4e94-9977-34a424bb86f6"), "Primitive", "float", 6 },
                    { new Guid("3266c213-9bfa-499e-9112-7914a1b18fa0"), "Primitive", "decimal", 4 },
                    { new Guid("3a594898-b4e3-45ea-bf52-ad81e047bdec"), "Primitive", "long", 11 },
                    { new Guid("3ac3234b-ee84-4ad5-873f-33c1ed165c59"), "Primitive", "byte", 1 },
                    { new Guid("433bd90b-f076-4736-8366-08949cc4c3d6"), "Primitive", "short", 13 },
                    { new Guid("59075d40-d193-46cb-94a4-4e1cbc2dff54"), "Primitive", "sByte", 2 },
                    { new Guid("a570de13-013e-48a1-a20c-b048a08aaf79"), "Primitive", "uint", 8 },
                    { new Guid("a76f684d-ceba-4fb4-8f0c-35f6fafbfb30"), "Primitive", "double", 5 },
                    { new Guid("ceac46b7-1983-4d9f-ba11-ac5267c859a3"), "Primitive", "ulong", 12 },
                    { new Guid("d13a84fd-b311-4557-a9d0-c7ce386b0706"), "Primitive", "char", 3 },
                    { new Guid("e71c4e14-8924-47b1-8f2f-4172146d52ca"), "Primitive", "nint", 9 },
                    { new Guid("f362917b-0eab-4781-b058-41fb7eaec2aa"), "Primitive", "ushort", 14 }
                });
        }
    }
}
