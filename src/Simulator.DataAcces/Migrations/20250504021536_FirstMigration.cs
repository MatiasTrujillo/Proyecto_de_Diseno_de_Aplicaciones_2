using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Type",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Stereotype = table.Column<int>(type: "int", nullable: true),
                    HierarchyClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PrimitiveType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Type_Type_HierarchyClassId",
                        column: x => x.HierarchyClassId,
                        principalTable: "Type",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Attributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Accessibility = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attributes_Type_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Type",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attributes_Type_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Type",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Methods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Accessibility = table.Column<int>(type: "int", nullable: false),
                    Stereotype = table.Column<int>(type: "int", nullable: true),
                    ReturnTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Methods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Methods_Type_ReturnTypeId",
                        column: x => x.ReturnTypeId,
                        principalTable: "Type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClassMethod",
                columns: table => new
                {
                    ClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MethodsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassMethod", x => new { x.ClassId, x.MethodsId });
                    table.ForeignKey(
                        name: "FK_ClassMethod_Methods_MethodsId",
                        column: x => x.MethodsId,
                        principalTable: "Methods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassMethod_Type_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvokedMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferenceName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvokedMethods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvokedMethods_Methods_MethodId",
                        column: x => x.MethodId,
                        principalTable: "Methods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocalVariables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalVariableTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalVariables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocalVariables_Methods_MethodId",
                        column: x => x.MethodId,
                        principalTable: "Methods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocalVariables_Type_LocalVariableTypeId",
                        column: x => x.LocalVariableTypeId,
                        principalTable: "Type",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Parameters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParameterTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parameters_Methods_MethodId",
                        column: x => x.MethodId,
                        principalTable: "Methods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Parameters_Type_ParameterTypeId",
                        column: x => x.ParameterTypeId,
                        principalTable: "Type",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("01820ae6-18b0-45bb-b357-afe004cd4dc4"), "Primitive", "nuint", 10 },
                    { new Guid("04137227-fecc-4e94-9977-34a424bb86f6"), "Primitive", "float", 6 },
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Primitive", "bool", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Primitive", "string", 15 },
                    { new Guid("3266c213-9bfa-499e-9112-7914a1b18fa0"), "Primitive", "decimal", 4 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Primitive", "int", 7 },
                    { new Guid("3a594898-b4e3-45ea-bf52-ad81e047bdec"), "Primitive", "long", 11 },
                    { new Guid("3ac3234b-ee84-4ad5-873f-33c1ed165c59"), "Primitive", "byte", 1 },
                    { new Guid("433bd90b-f076-4736-8366-08949cc4c3d6"), "Primitive", "short", 13 },
                    { new Guid("59075d40-d193-46cb-94a4-4e1cbc2dff54"), "Primitive", "sByte", 2 },
                    { new Guid("a570de13-013e-48a1-a20c-b048a08aaf79"), "Primitive", "uint", 8 },
                    { new Guid("a76f684d-ceba-4fb4-8f0c-35f6fafbfb30"), "Primitive", "double", 5 },
                    { new Guid("ceac46b7-1983-4d9f-ba11-ac5267c859a3"), "Primitive", "ulong", 12 },
                    { new Guid("d13a84fd-b311-4557-a9d0-c7ce386b0706"), "Primitive", "char", 3 }
                });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "HierarchyClassId", "Name", "Stereotype" },
                values: new object[] { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), "Class", null, "Object", null });

            migrationBuilder.InsertData(
                table: "Type",
                columns: new[] { "Id", "Discriminator", "Name", "PrimitiveType" },
                values: new object[,]
                {
                    { new Guid("e71c4e14-8924-47b1-8f2f-4172146d52ca"), "Primitive", "nint", 9 },
                    { new Guid("f362917b-0eab-4781-b058-41fb7eaec2aa"), "Primitive", "ushort", 14 }
                });

            migrationBuilder.InsertData(
                table: "Methods",
                columns: new[] { "Id", "Accessibility", "Name", "ReturnTypeId", "Stereotype" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 0, "ToString", new Guid("22222222-2222-2222-2222-222222222222"), null },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 0, "Equals", new Guid("11111111-1111-1111-1111-111111111111"), null },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), 0, "GetHashCode", new Guid("33333333-3333-3333-3333-333333333333"), null }
                });

            migrationBuilder.InsertData(
                table: "ClassMethod",
                columns: new[] { "ClassId", "MethodsId" },
                values: new object[,]
                {
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb") },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc") }
                });

            migrationBuilder.InsertData(
                table: "Parameters",
                columns: new[] { "Id", "MethodId", "Name", "ParameterTypeId" },
                values: new object[] { new Guid("a7ca113e-f6d9-480f-b008-5f4ae1913c07"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") });

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_ClassId",
                table: "Attributes",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_TypeId",
                table: "Attributes",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassMethod_MethodsId",
                table: "ClassMethod",
                column: "MethodsId");

            migrationBuilder.CreateIndex(
                name: "IX_InvokedMethods_MethodId",
                table: "InvokedMethods",
                column: "MethodId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalVariables_LocalVariableTypeId",
                table: "LocalVariables",
                column: "LocalVariableTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalVariables_MethodId",
                table: "LocalVariables",
                column: "MethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Methods_ReturnTypeId",
                table: "Methods",
                column: "ReturnTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_MethodId",
                table: "Parameters",
                column: "MethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_ParameterTypeId",
                table: "Parameters",
                column: "ParameterTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Type_HierarchyClassId",
                table: "Type",
                column: "HierarchyClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attributes");

            migrationBuilder.DropTable(
                name: "ClassMethod");

            migrationBuilder.DropTable(
                name: "InvokedMethods");

            migrationBuilder.DropTable(
                name: "LocalVariables");

            migrationBuilder.DropTable(
                name: "Parameters");

            migrationBuilder.DropTable(
                name: "Methods");

            migrationBuilder.DropTable(
                name: "Type");
        }
    }
}
