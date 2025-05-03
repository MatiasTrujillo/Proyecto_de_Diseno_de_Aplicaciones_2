using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Simulator.Domain;
using Attribute = Simulator.Domain.Attribute;
using Type = Simulator.Domain.Type;

namespace Simulator.DataAcces;

public sealed class SqlContext : DbContext
{
    public SqlContext(DbContextOptions<SqlContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Attribute> Attributes { get; set; }
    public DbSet<Method> Methods { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Primitive> Primitives { get; set; }
    public DbSet<Parameter> Parameters { get; set; }
    public DbSet<LocalVariable> LocalVariables { get; set; }
    public DbSet<InvokedMethod> InvokedMethods { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Type>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<Class>()
            .HasBaseType<Type>();

        modelBuilder.Entity<Primitive>()
            .HasBaseType<Type>();

        modelBuilder.Entity<Attribute>()
            .HasKey(a => a.Id);

        modelBuilder.Entity<Method>()
            .HasMany(m => m.Methods)
            .WithMany()
            .UsingEntity(j => j.ToTable("MethodRelations"));

        modelBuilder.Entity<Method>()
            .HasMany(m => m.Parameters)
            .WithOne()
            .HasForeignKey("MethodId")
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Method>()
            .HasMany(m => m.LocalVariables)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Method>()
            .HasOne(m => m.ReturnType)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<InvokedMethod>()
            .HasKey(i => i.Id);

        modelBuilder.Entity<Parameter>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<LocalVariable>()
            .HasKey(l => l.Id);

        modelBuilder.Entity<Class>()
            .HasMany(c => c.Methods)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "ClassMethod",
                j => j.HasOne<Method>().WithMany().HasForeignKey("MethodsId"),
                j => j.HasOne<Class>().WithMany().HasForeignKey("ClassId"),
                j =>
                {
                    j.HasKey("ClassId", "MethodsId");
                    j.ToTable("ClassMethod");
                });

        ConfigSeedData(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private void ConfigSeedData(ModelBuilder modelBuilder)
    {
        var methodId1 = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        var methodId2 = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
        var methodId3 = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");
        var classId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");
        var boolTypeId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var stringTypeId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var intTypeId = Guid.Parse("33333333-3333-3333-3333-333333333333");

        modelBuilder.Entity<Class>().HasData(
            new
            {
                Id = classId,
                Name = "Object",
                Stereotype = (KeyWord?)null,
                HierarchyClass = (Guid?)null
            });

        modelBuilder.Entity<Primitive>().HasData(
            new
            {
                Id = boolTypeId,
                Name = "Bool",
                PrimitiveType = PrimitiveType.Bool
            },
            new
            {
                Id = stringTypeId,
                Name = "String",
                PrimitiveType = PrimitiveType.String
            },
            new
            {
                Id = intTypeId,
                Name = "Int",
                PrimitiveType = PrimitiveType.Int
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "Byte",
                PrimitiveType = PrimitiveType.Byte
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "SByte",
                PrimitiveType = PrimitiveType.Sbyte
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "Char",
                PrimitiveType = PrimitiveType.Char
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "Decimal",
                PrimitiveType = PrimitiveType.Decimal
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "Double",
                PrimitiveType = PrimitiveType.Double
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "Float",
                PrimitiveType = PrimitiveType.Float
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "Uint",
                PrimitiveType = PrimitiveType.Uint
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "Nint",
                PrimitiveType = PrimitiveType.Nint
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "Nuint",
                PrimitiveType = PrimitiveType.Nuint
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "Long",
                PrimitiveType = PrimitiveType.Long
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "Ulong",
                PrimitiveType = PrimitiveType.Ulong
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "Short",
                PrimitiveType = PrimitiveType.Short
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "Ushort",
                PrimitiveType = PrimitiveType.Ushort
            }
        );

        modelBuilder.Entity<Method>().HasData(
            new
            {
                Id = methodId1,
                Name = "ToString",
                Accessibility = Accessibility.Public,
                Stereotype = (KeyWord?)null,
                ReturnTypeId = stringTypeId,
            },
            new
            {
                Id = methodId2,
                Name = "Equals",
                Accessibility = Accessibility.Public,
                Stereotype = (KeyWord?)null,
                ReturnTypeId = boolTypeId,
            },
            new
            {
                Id = methodId3,
                Name = "GetHashCode",
                Accessibility = Accessibility.Public,
                Stereotype = (KeyWord?)null,
                ReturnTypeId = intTypeId,
            }
        );

        modelBuilder.Entity("ClassMethod").HasData(
            new Dictionary<string, object>
            {
                ["ClassId"] = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                ["MethodsId"] = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")
            },
            new Dictionary<string, object>
            {
                ["ClassId"] = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                ["MethodsId"] = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb")
            },
            new Dictionary<string, object>
            {
                ["ClassId"] = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                ["MethodsId"] = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc")
            }
        );

        modelBuilder.Entity<Parameter>().HasData(
            new
            {
                Id = Guid.NewGuid(),
                Name = "Obj",
                ParameterTypeId = classId,
                MethodId = methodId2,
            });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLazyLoadingProxies();
    }
}

public class SqlContextFactory : IDesignTimeDbContextFactory<SqlContext>
{
    public SqlContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SqlContext>();
        optionsBuilder.UseSqlServer("Server=127.0.0.1,1433;Database=dataBase;User Id=sa;Password=Passw1rd;TrustServerCertificate=true;");
        return new SqlContext(optionsBuilder.Options);
    }
}
