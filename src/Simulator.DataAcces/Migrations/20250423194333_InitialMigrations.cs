using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simulator.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Type",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stereotype = table.Column<int>(type: "int", nullable: true),
                    HierarchyClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Methods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Accessibility = table.Column<int>(type: "int", nullable: false),
                    Stereotype = table.Column<int>(type: "int", nullable: true),
                    ReturnTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Methods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Methods_Type_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Type",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Methods_Type_ReturnTypeId",
                        column: x => x.ReturnTypeId,
                        principalTable: "Type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocalVariables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalVariableTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MethodRelations",
                columns: table => new
                {
                    MethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MethodsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MethodRelations", x => new { x.MethodId, x.MethodsId });
                    table.ForeignKey(
                        name: "FK_MethodRelations_Methods_MethodId",
                        column: x => x.MethodId,
                        principalTable: "Methods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MethodRelations_Methods_MethodsId",
                        column: x => x.MethodsId,
                        principalTable: "Methods",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Parameters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParameterTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_ClassId",
                table: "Attributes",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_TypeId",
                table: "Attributes",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalVariables_LocalVariableTypeId",
                table: "LocalVariables",
                column: "LocalVariableTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalVariables_MethodId",
                table: "LocalVariables",
                column: "MethodId");

            migrationBuilder.CreateIndex(
                name: "IX_MethodRelations_MethodsId",
                table: "MethodRelations",
                column: "MethodsId");

            migrationBuilder.CreateIndex(
                name: "IX_Methods_ClassId",
                table: "Methods",
                column: "ClassId");

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
                name: "LocalVariables");

            migrationBuilder.DropTable(
                name: "MethodRelations");

            migrationBuilder.DropTable(
                name: "Parameters");

            migrationBuilder.DropTable(
                name: "Methods");

            migrationBuilder.DropTable(
                name: "Type");
        }
    }
}
