using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class ParentsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Type_Type_HierarchyClassId",
                table: "Type");

            migrationBuilder.DropIndex(
                name: "IX_Type_HierarchyClassId",
                table: "Type");

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("77c01e11-60ae-4e89-b17e-f99ae11228e1"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("daca4005-bc53-48ff-abe0-412d5ae8df9e"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("e320583f-1ebb-4483-a234-0fd23a5e2f4b"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("f984e2c8-9ece-4092-b0c2-71747b498b03"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("ffafdef6-b88b-4a61-ab2b-f0bce2260084"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("279a3662-4256-4ed4-807f-d8551526af58"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("2c9c838f-68f0-4262-81ec-5bea323989ad"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("50e1ed0f-3a03-4626-8394-81f94658b845"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("6181877b-c627-4949-9600-08b5ccd81260"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("730fc595-1f4d-4ebe-85d9-9022255f3078"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("967c5970-2c96-4cd4-b8bb-a159a6e0c656"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("978ff7e9-5484-4e7f-85f7-ee1e5d8c15a7"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("a7be503e-036b-42d2-92b2-aeba24c38c55"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("d1b34526-6eba-4644-b41e-311843c32301"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("eadf98eb-da3c-4199-8189-9afed15849ba"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("ee450cc6-2f48-41cf-abd9-95339273cba8"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("f3052a3d-e01a-46cd-a8df-ed2780664ca4"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("fba590b0-ebdf-4972-b415-954af3c09d7b"));

            migrationBuilder.DropColumn(
                name: "HierarchyClassId",
                table: "Type");

            migrationBuilder.CreateTable(
                name: "ClassParents",
                columns: table => new
                {
                    ClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassParents", x => new { x.ClassId, x.ParentId });
                    table.ForeignKey(
                        name: "FK_ClassParents_Type_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Type",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClassParents_Type_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InvokedMethodId", "MethodId", "MethodSignatureId", "Name", "ParameterTypeId" },
                values: new object[,]
                {
                    { new Guid("0075fe47-ae43-420f-80c3-304df0c54cfd"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("a9e54469-95ee-4663-8ec2-6249fb14d617"), null, new Guid("44444444-4444-4444-4444-444444444444"), null, "StrToParse", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("ab0f18b3-3a75-4697-b26f-3322e5eead7b"), null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("b89feea7-8fc8-4642-947f-9a1f2f330ad4"), null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), null, "StrIsNullOrEmpty", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("ea160ff1-5525-462e-9942-9bb649415db5"), null, new Guid("77777777-7777-7777-7777-777777777777"), null, "StrToCompare", new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("04b4cf8c-ca6d-40dc-a7ad-1f5edf674f9d"), "Primitive", "double", 5 },
                    { new Guid("0b672e6e-0171-46d2-a6f9-bde7a1842c5b"), "Primitive", "float", 6 },
                    { new Guid("24421ff2-feef-4b12-bb79-d26b592e763b"), "Primitive", "ushort", 14 },
                    { new Guid("28215233-655c-4b48-973a-ab9dabe5eaf8"), "Primitive", "sByte", 2 },
                    { new Guid("42583468-eac7-4006-b65d-5091d7aa95a3"), "Primitive", "nint", 9 },
                    { new Guid("815e4239-fb3e-4448-8e6c-8610bcf6bef3"), "Primitive", "long", 11 },
                    { new Guid("9407ef58-b980-4403-9d5d-36672febbc7d"), "Primitive", "decimal", 4 },
                    { new Guid("b03b65a4-9cb5-4350-8788-9bac17b3cac9"), "Primitive", "char", 3 },
                    { new Guid("b35c3477-c6f4-4ce3-9625-e14e504f8330"), "Primitive", "uint", 8 },
                    { new Guid("b911ef44-ef4a-408f-9c27-4a114a7bd656"), "Primitive", "nuint", 10 },
                    { new Guid("c73e1d14-0e01-4c1d-bd41-e13f0b0e76af"), "Primitive", "ulong", 12 },
                    { new Guid("dc7e032b-9bd5-443c-a1a2-161c66e25414"), "Primitive", "short", 13 },
                    { new Guid("ea656b1c-0ced-439d-bb3b-d4981b66afeb"), "Primitive", "byte", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassParents_ParentId",
                table: "ClassParents",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassParents");

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("0075fe47-ae43-420f-80c3-304df0c54cfd"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("a9e54469-95ee-4663-8ec2-6249fb14d617"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("ab0f18b3-3a75-4697-b26f-3322e5eead7b"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("b89feea7-8fc8-4642-947f-9a1f2f330ad4"));

            migrationBuilder.DeleteData(
                table: "Parameters",
                keyColumn: "Id",
                keyValue: new Guid("ea160ff1-5525-462e-9942-9bb649415db5"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("04b4cf8c-ca6d-40dc-a7ad-1f5edf674f9d"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("0b672e6e-0171-46d2-a6f9-bde7a1842c5b"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("24421ff2-feef-4b12-bb79-d26b592e763b"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("28215233-655c-4b48-973a-ab9dabe5eaf8"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("42583468-eac7-4006-b65d-5091d7aa95a3"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("815e4239-fb3e-4448-8e6c-8610bcf6bef3"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("9407ef58-b980-4403-9d5d-36672febbc7d"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("b03b65a4-9cb5-4350-8788-9bac17b3cac9"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("b35c3477-c6f4-4ce3-9625-e14e504f8330"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("b911ef44-ef4a-408f-9c27-4a114a7bd656"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("c73e1d14-0e01-4c1d-bd41-e13f0b0e76af"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("dc7e032b-9bd5-443c-a1a2-161c66e25414"));

            migrationBuilder.DeleteData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("ea656b1c-0ced-439d-bb3b-d4981b66afeb"));

            migrationBuilder.AddColumn<Guid>(
                name: "HierarchyClassId",
                table: "Type",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "InvokedMethodId", "MethodId", "MethodSignatureId", "Name", "ParameterTypeId" },
                values: new object[,]
                {
                    { new Guid("77c01e11-60ae-4e89-b17e-f99ae11228e1"), null, new Guid("44444444-4444-4444-4444-444444444444"), null, "StrToParse", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("daca4005-bc53-48ff-abe0-412d5ae8df9e"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("e320583f-1ebb-4483-a234-0fd23a5e2f4b"), null, new Guid("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa"), null, "StrIsNullOrEmpty", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("f984e2c8-9ece-4092-b0c2-71747b498b03"), null, new Guid("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb"), null, "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("ffafdef6-b88b-4a61-ab2b-f0bce2260084"), null, new Guid("77777777-7777-7777-7777-777777777777"), null, "StrToCompare", new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.UpdateData(
                table: "Type",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                column: "HierarchyClassId",
                value: null);

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("279a3662-4256-4ed4-807f-d8551526af58"), "Primitive", "ushort", 14 },
                    { new Guid("2c9c838f-68f0-4262-81ec-5bea323989ad"), "Primitive", "short", 13 },
                    { new Guid("50e1ed0f-3a03-4626-8394-81f94658b845"), "Primitive", "long", 11 },
                    { new Guid("6181877b-c627-4949-9600-08b5ccd81260"), "Primitive", "decimal", 4 },
                    { new Guid("730fc595-1f4d-4ebe-85d9-9022255f3078"), "Primitive", "double", 5 },
                    { new Guid("967c5970-2c96-4cd4-b8bb-a159a6e0c656"), "Primitive", "uint", 8 },
                    { new Guid("978ff7e9-5484-4e7f-85f7-ee1e5d8c15a7"), "Primitive", "byte", 1 },
                    { new Guid("a7be503e-036b-42d2-92b2-aeba24c38c55"), "Primitive", "ulong", 12 },
                    { new Guid("d1b34526-6eba-4644-b41e-311843c32301"), "Primitive", "nuint", 10 },
                    { new Guid("eadf98eb-da3c-4199-8189-9afed15849ba"), "Primitive", "char", 3 },
                    { new Guid("ee450cc6-2f48-41cf-abd9-95339273cba8"), "Primitive", "sByte", 2 },
                    { new Guid("f3052a3d-e01a-46cd-a8df-ed2780664ca4"), "Primitive", "float", 6 },
                    { new Guid("fba590b0-ebdf-4972-b415-954af3c09d7b"), "Primitive", "nint", 9 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Type_HierarchyClassId",
                table: "Type",
                column: "HierarchyClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Type_Type_HierarchyClassId",
                table: "Type",
                column: "HierarchyClassId",
                principalTable: "Type",
                principalColumn: "Id");
        }
    }
}
