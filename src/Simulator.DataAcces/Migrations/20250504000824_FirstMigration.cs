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
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Primitive", "Bool", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Primitive", "String", 15 },
                    { new Guid("23014a03-91f5-4165-b686-d4601cb9dff5"), "Primitive", "Short", 13 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Primitive", "Int", 7 },
                    { new Guid("48e1136d-5685-4bba-9fc7-5807cb9871ac"), "Primitive", "Double", 5 },
                    { new Guid("51241764-a152-4133-a5b6-d2f587258aa7"), "Primitive", "Uint", 8 },
                    { new Guid("5473a65b-5264-490d-8656-8b05ff1a5519"), "Primitive", "Long", 11 },
                    { new Guid("554b096b-1176-4a80-9da2-2044162de1c2"), "Primitive", "Byte", 1 },
                    { new Guid("583d5a15-a61f-4864-8634-6f08b9351c73"), "Primitive", "Ulong", 12 },
                    { new Guid("7fd5d1fe-ba4a-418d-b999-b54a37871ea6"), "Primitive", "Nuint", 10 },
                    { new Guid("ab2e837c-0ea0-42fb-90dc-0ea726c66174"), "Primitive", "Char", 3 },
                    { new Guid("b8fab0a5-07f5-4b46-860b-ea3c8c45a933"), "Primitive", "Float", 6 },
                    { new Guid("c00165d4-9aae-4d50-84d2-b528b44955c4"), "Primitive", "SByte", 2 },
                    { new Guid("d1a6b88e-2d51-4643-84ee-44b6b124db77"), "Primitive", "Nint", 9 }
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
                    { new Guid("df9d9707-0f04-482c-af92-f3f0b2d2c80c"), "Primitive", "Ushort", 14 },
                    { new Guid("f158d763-7a20-486f-832e-df94ddf1fcf3"), "Primitive", "Decimal", 4 }
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
                values: new object[] { new Guid("28777538-f4b9-4ae9-ab57-a7ae9339969f"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Obj", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") });

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
