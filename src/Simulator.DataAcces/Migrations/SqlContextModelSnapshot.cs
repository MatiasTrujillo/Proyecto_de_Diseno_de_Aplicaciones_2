using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Simulator.DataAcces;

#nullable disable

namespace Simulator.DataAcces.Migrations
{
    [DbContext(typeof(SqlContext))]
    partial class SqlContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MethodMethod", b =>
                {
                    b.Property<Guid>("MethodId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MethodsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MethodId", "MethodsId");

                    b.HasIndex("MethodsId");

                    b.ToTable("MethodRelations", (string)null);
                });

            modelBuilder.Entity("Simulator.Domain.Attribute", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Accessibility")
                        .HasColumnType("int");

                    b.Property<Guid?>("ClassId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("TypeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.HasIndex("TypeId");

                    b.ToTable("Attributes");
                });

            modelBuilder.Entity("Simulator.Domain.LocalVariable", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("LocalVariableTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("MethodId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LocalVariableTypeId");

                    b.HasIndex("MethodId");

                    b.ToTable("LocalVariables");
                });

            modelBuilder.Entity("Simulator.Domain.Method", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Accessibility")
                        .HasColumnType("int");

                    b.Property<Guid?>("ClassId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ReturnTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Stereotype")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.HasIndex("ReturnTypeId");

                    b.ToTable("Methods");

                    b.HasData(
                        new
                        {
                            Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                            Accessibility = 0,
                            Name = "ToString",
                            ReturnTypeId = new Guid("22222222-2222-2222-2222-222222222222")
                        },
                        new
                        {
                            Id = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                            Accessibility = 0,
                            Name = "Equals",
                            ReturnTypeId = new Guid("11111111-1111-1111-1111-111111111111")
                        },
                        new
                        {
                            Id = new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                            Accessibility = 0,
                            Name = "GetHashCode",
                            ReturnTypeId = new Guid("33333333-3333-3333-3333-333333333333")
                        });
                });

            modelBuilder.Entity("Simulator.Domain.Parameter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("MethodId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ParameterTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MethodId");

                    b.HasIndex("ParameterTypeId");

                    b.ToTable("Parameters");
                });

            modelBuilder.Entity("Simulator.Domain.Type", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Type");

                    b.HasDiscriminator().HasValue("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Simulator.Domain.Class", b =>
                {
                    b.HasBaseType("Simulator.Domain.Type");

                    b.Property<Guid?>("HierarchyClassId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Stereotype")
                        .HasColumnType("int");

                    b.HasIndex("HierarchyClassId");

                    b.HasDiscriminator().HasValue("Class");

                    b.HasData(
                        new
                        {
                            Id = new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                            Name = "Object"
                        });
                });

            modelBuilder.Entity("Simulator.Domain.Primitive", b =>
                {
                    b.HasBaseType("Simulator.Domain.Type");

                    b.Property<int>("PrimitiveType")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Primitive");

                    b.HasData(
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111111"),
                            Name = "Bool",
                            PrimitiveType = 0
                        },
                        new
                        {
                            Id = new Guid("22222222-2222-2222-2222-222222222222"),
                            Name = "String",
                            PrimitiveType = 15
                        },
                        new
                        {
                            Id = new Guid("33333333-3333-3333-3333-333333333333"),
                            Name = "Int",
                            PrimitiveType = 7
                        });
                });

            modelBuilder.Entity("MethodMethod", b =>
                {
                    b.HasOne("Simulator.Domain.Method", null)
                        .WithMany()
                        .HasForeignKey("MethodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Simulator.Domain.Method", null)
                        .WithMany()
                        .HasForeignKey("MethodsId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Simulator.Domain.Attribute", b =>
                {
                    b.HasOne("Simulator.Domain.Class", null)
                        .WithMany("Attributes")
                        .HasForeignKey("ClassId");

                    b.HasOne("Simulator.Domain.Type", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Simulator.Domain.LocalVariable", b =>
                {
                    b.HasOne("Simulator.Domain.Type", "LocalVariableType")
                        .WithMany()
                        .HasForeignKey("LocalVariableTypeId");

                    b.HasOne("Simulator.Domain.Method", null)
                        .WithMany("LocalVariables")
                        .HasForeignKey("MethodId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("LocalVariableType");
                });

            modelBuilder.Entity("Simulator.Domain.Method", b =>
                {
                    b.HasOne("Simulator.Domain.Class", null)
                        .WithMany("Methods")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Simulator.Domain.Type", "ReturnType")
                        .WithMany()
                        .HasForeignKey("ReturnTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ReturnType");
                });

            modelBuilder.Entity("Simulator.Domain.Parameter", b =>
                {
                    b.HasOne("Simulator.Domain.Method", null)
                        .WithMany("Parameters")
                        .HasForeignKey("MethodId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Simulator.Domain.Type", "ParameterType")
                        .WithMany()
                        .HasForeignKey("ParameterTypeId");

                    b.Navigation("ParameterType");
                });

            modelBuilder.Entity("Simulator.Domain.Class", b =>
                {
                    b.HasOne("Simulator.Domain.Type", "HierarchyClass")
                        .WithMany()
                        .HasForeignKey("HierarchyClassId");

                    b.Navigation("HierarchyClass");
                });

            modelBuilder.Entity("Simulator.Domain.Method", b =>
                {
                    b.Navigation("LocalVariables");

                    b.Navigation("Parameters");
                });

            modelBuilder.Entity("Simulator.Domain.Class", b =>
                {
                    b.Navigation("Attributes");

                    b.Navigation("Methods");
                });
        }
    }
}
