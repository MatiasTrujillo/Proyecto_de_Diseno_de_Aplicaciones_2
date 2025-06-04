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
    public DbSet<NameSpace> NameSpaces { get; set; }
    public DbSet<Interface> Interfaces { get; set; }
    public DbSet<MethodSignature> MethodSignatures { get; set; }
    public DbSet<Session> Sessions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Type>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<Class>()
            .HasBaseType<Type>();

        modelBuilder.Entity<Primitive>()
            .HasBaseType<Type>();

        modelBuilder.Entity<Interface>()
            .HasBaseType<Type>();

        modelBuilder.Entity<Class>()
            .HasMany(c => c.Parents)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "ClassParents",
                j => j.HasOne<Type>().WithMany().HasForeignKey("ParentId"),
                j => j.HasOne<Class>().WithMany().HasForeignKey("ClassId"),
                j =>
                {
                    j.HasKey("ClassId", "ParentId");
                    j.ToTable("ClassParents");
                });

        modelBuilder.Entity<MethodSignature>(entity =>
        {
            entity.HasKey(m => m.Id);

            entity.HasOne(m => m.ReturnType)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(m => m.Parameters)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Attribute>()
            .HasKey(a => a.Id);

        modelBuilder.Entity<InvokedMethod>(entity =>
        {
            entity.ToTable("InvokedMethods");
            entity.HasKey(i => i.Id);
        });

        modelBuilder.Entity<Method>()
            .HasMany(m => m.LocalVariables)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Method>()
            .HasOne(m => m.ReturnType)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Parameter>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<LocalVariable>()
            .HasKey(l => l.Id);

        modelBuilder.Entity<Type>()
            .HasMany(t => t.Methods)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "TypeMethod",
                j => j.HasOne<Method>().WithMany().HasForeignKey("MethodsId"),
                j => j.HasOne<Type>().WithMany().HasForeignKey("TypeId"),
                j =>
                {
                    j.HasKey("TypeId", "MethodsId");
                    j.ToTable("TypeMethod");
                });

        modelBuilder.Entity<Method>()
            .HasMany(m => m.Methods)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "MethodInvokedMethod",
                j => j.HasOne<InvokedMethod>().WithMany().HasForeignKey("InvokedMethodId"),
                j => j.HasOne<Method>().WithMany().HasForeignKey("MethodsId"),
                j =>
                {
                    j.HasKey("MethodsId", "InvokedMethodId");
                    j.ToTable("MethodInvokedMethod");
                });

        modelBuilder.Entity<Attribute>()
            .HasMany(m => m.InvokedMethods)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "AttributeInvokedMethods",
                j => j.HasOne<InvokedMethod>().WithMany().HasForeignKey("InvokedMethodId"),
                j => j.HasOne<Attribute>().WithMany().HasForeignKey("AttributeId"),
                j =>
                {
                    j.HasKey("AttributeId", "InvokedMethodId");
                    j.ToTable("AttributeInvokedMethods");
                });

        modelBuilder.Entity<Interface>()
            .HasMany(t => t.MethodSignatures)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "InterfaceMethodSignature",
                j => j.HasOne<MethodSignature>().WithMany().HasForeignKey("MethodSignatureId"),
                j => j.HasOne<Interface>().WithMany().HasForeignKey("InterfaceId"),
                j =>
                {
                    j.HasKey("InterfaceId", "MethodSignatureId");
                    j.ToTable("InterfaceMethodSignature");
                });

        ConfigSeedData(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private void ConfigSeedData(ModelBuilder modelBuilder)
    {
        var systemNameSpaceId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab");
        var parseMethodId = Guid.Parse("44444444-4444-4444-4444-444444444444");
        var maxValueMethodId = Guid.Parse("55555555-5555-5555-5555-555555555555");
        var minValueMethodId = Guid.Parse("66666666-6666-6666-6666-666666666666");
        var compareToMethodId = Guid.Parse("77777777-7777-7777-7777-777777777777");
        var toLowerCaseMethodId = Guid.Parse("88888888-8888-8888-8888-888888888888");
        var toUpperCaseMethodId = Guid.Parse("99999999-9999-9999-9999-999999999999");
        var isNullOrEmptyMethodId = Guid.Parse("aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa");
        var stringEqualsMethodId = Guid.Parse("bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb");
        var intToStringMethodId = Guid.Parse("cccccccc-3333-3333-3333-cccccccccccc");

        var methodId1 = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        var methodId2 = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
        var methodId3 = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");
        var methodId4 = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee");
        var classId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");
        var boolTypeId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var stringTypeId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var intTypeId = Guid.Parse("33333333-3333-3333-3333-333333333333");

        var session1 = Guid.Parse("b142d2d6-ac0c-4af1-b272-c03e431555f3");
        var session2 = Guid.Parse("675c1f61-9dfa-4ead-bae4-dc432ee2bd32");
        var session3 = Guid.Parse("756ec724-3e51-4e0d-a857-cceee846a5f6");
        var session4 = Guid.Parse("a4a20350-e7e1-4080-b4da-75e875af6b61");
        var session5 = Guid.Parse("2070a73f-97a1-4365-a231-dca969fb52fb");
        var session6 = Guid.Parse("f6e32298-4362-4ef1-9e6a-e2dfc9e58a47");
        var session7 = Guid.Parse("231ecdac-9680-4772-a95a-29abdfd4ad5f");
        var session9 = Guid.Parse("5c8f91eb-5bd1-4706-96ca-cfcec81473b8");
        var session10 = Guid.Parse("66d9652f-3585-4982-9ab7-8d8159197147");

        modelBuilder.Entity<NameSpace>().HasData(new
        {
            Id = systemNameSpaceId,
            Name = "System",
            FatherId = (Guid?)null
        });

        modelBuilder.Entity<Session>().HasData(
            new
            {
                Token = session1,
                IsActive = true,
            },
            new
            {
                Token = session2,
                IsActive = true
            },
            new
            {
                Token = session3,
                IsActive = true
            },
            new
            {
                Token = session4,
                IsActive = true
            },
            new
            {
                Token = session5,
                IsActive = true
            },
            new
            {
                Token = session6,
                IsActive = true
            },
            new
            {
                Token = session7,
                IsActive = true
            },
            new
            {
                Token = session9,
                IsActive = true
            },
            new
            {
                Token = session10,
                IsActive = true
            });

        modelBuilder.Entity<Class>().HasData(new
        {
            Id = classId,
            Name = "Object",
            Stereotype = (KeyWord?)null,
            HierarchyClass = (Guid?)null,
            NameSpaceId = systemNameSpaceId
        });

        modelBuilder.Entity<Primitive>().HasData(
            new
            {
                Id = boolTypeId,
                Name = "bool",
                PrimitiveType = PrimitiveType.Bool
            },
            new
            {
                Id = stringTypeId,
                Name = "string",
                PrimitiveType = PrimitiveType.String
            },
            new
            {
                Id = intTypeId,
                Name = "int",
                PrimitiveType = PrimitiveType.Int
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "byte",
                PrimitiveType = PrimitiveType.Byte
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "sByte",
                PrimitiveType = PrimitiveType.Sbyte
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "char",
                PrimitiveType = PrimitiveType.Char
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "decimal",
                PrimitiveType = PrimitiveType.Decimal
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "double",
                PrimitiveType = PrimitiveType.Double
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "float",
                PrimitiveType = PrimitiveType.Float
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "uint",
                PrimitiveType = PrimitiveType.Uint
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "nint",
                PrimitiveType = PrimitiveType.Nint
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "nuint",
                PrimitiveType = PrimitiveType.Nuint
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "long",
                PrimitiveType = PrimitiveType.Long
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "ulong",
                PrimitiveType = PrimitiveType.Ulong
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "short",
                PrimitiveType = PrimitiveType.Short
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "ushort",
                PrimitiveType = PrimitiveType.Ushort
            });

        modelBuilder.Entity<Method>().HasData(
            new
            {
                Id = methodId1,
                Name = "ToString",
                Accessibility = Accessibility.Public,
                Stereotype = (KeyWord?)null,
                ReturnTypeId = stringTypeId,
                IsOverride = false,
            },
            new
            {
                Id = methodId4,
                Name = "ToString",
                Accessibility = Accessibility.Public,
                Stereotype = (KeyWord?)null,
                ReturnTypeId = stringTypeId,
                IsOverride = false,
            },
            new
            {
                Id = methodId2,
                Name = "Equals",
                Accessibility = Accessibility.Public,
                Stereotype = (KeyWord?)null,
                ReturnTypeId = boolTypeId,
                IsOverride = false,
            },
            new
            {
                Id = methodId3,
                Name = "GetHashCode",
                Accessibility = Accessibility.Public,
                Stereotype = (KeyWord?)null,
                ReturnTypeId = intTypeId,
                IsOverride = false,
            });

        modelBuilder.Entity("TypeMethod").HasData(
            new Dictionary<string, object>
            {
                ["TypeId"] = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                ["MethodsId"] = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")
            },
            new Dictionary<string, object>
            {
                ["TypeId"] = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                ["MethodsId"] = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb")
            },
            new Dictionary<string, object>
            {
                ["TypeId"] = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                ["MethodsId"] = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc")
            });

        modelBuilder.Entity<Parameter>().HasData(
            new
            {
                Id = Guid.NewGuid(),
                Name = "Obj",
                ParameterTypeId = classId,
                MethodId = methodId2
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "Obj",
                ParameterTypeId = classId,
                MethodId = stringEqualsMethodId
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "StrToParse",
                ParameterTypeId = stringTypeId,
                MethodId = parseMethodId
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "StrIsNullOrEmpty",
                ParameterTypeId = stringTypeId,
                MethodId = isNullOrEmptyMethodId
            },
            new
            {
                Id = Guid.NewGuid(),
                Name = "StrToCompare",
                ParameterTypeId = stringTypeId,
                MethodId = compareToMethodId,
            });

        modelBuilder.Entity<Method>().HasData(
            new
            {
                Id = parseMethodId,
                Name = "Parse",
                Accessibility = Accessibility.Public,
                Stereotype = (KeyWord?)null,
                ReturnTypeId = intTypeId,
                IsOverride = false
            },
            new
            {
                Id = maxValueMethodId,
                Name = "MaxValue",
                Accessibility = Accessibility.Public,
                Stereotype = (KeyWord?)null,
                ReturnTypeId = intTypeId,
                IsOverride = false
            },
            new
            {
                Id = minValueMethodId,
                Name = "MinValue",
                Accessibility = Accessibility.Public,
                Stereotype = (KeyWord?)null,
                ReturnTypeId = intTypeId,
                IsOverride = false
            },
            new
            {
                Id = compareToMethodId,
                Name = "CompareTo",
                Accessibility = Accessibility.Public,
                Stereotype = (KeyWord?)null,
                ReturnTypeId = intTypeId,
                IsOverride = false
            },
            new
            {
                Id = intToStringMethodId,
                Name = "ToString",
                Accessibility = Accessibility.Public,
                Stereotype = (KeyWord?)null,
                ReturnTypeId = stringTypeId,
                IsOverride = false
            },
            new
            {
                Id = toLowerCaseMethodId,
                Name = "ToLowerCase",
                Accessibility = Accessibility.Public,
                Stereotype = (KeyWord?)null,
                ReturnTypeId = stringTypeId,
                IsOverride = false
            },
            new
            {
                Id = toUpperCaseMethodId,
                Name = "ToUpperCase",
                Accessibility = Accessibility.Public,
                Stereotype = (KeyWord?)null,
                ReturnTypeId = stringTypeId,
                IsOverride = false
            },
            new
            {
                Id = isNullOrEmptyMethodId,
                Name = "IsNullOrEmpty",
                Accessibility = Accessibility.Public,
                Stereotype = (KeyWord?)null,
                ReturnTypeId = boolTypeId,
                IsOverride = false
            },
            new
            {
                Id = stringEqualsMethodId,
                Name = "Equals",
                Accessibility = Accessibility.Public,
                Stereotype = (KeyWord?)null,
                ReturnTypeId = boolTypeId,
                IsOverride = false
            });
        modelBuilder.Entity("TypeMethod").HasData(
            new Dictionary<string, object> { ["TypeId"] = intTypeId, ["MethodsId"] = parseMethodId },
            new Dictionary<string, object> { ["TypeId"] = intTypeId, ["MethodsId"] = maxValueMethodId },
            new Dictionary<string, object> { ["TypeId"] = intTypeId, ["MethodsId"] = minValueMethodId },
            new Dictionary<string, object> { ["TypeId"] = intTypeId, ["MethodsId"] = compareToMethodId },
            new Dictionary<string, object> { ["TypeId"] = intTypeId, ["MethodsId"] = intToStringMethodId },
            new Dictionary<string, object> { ["TypeId"] = stringTypeId, ["MethodsId"] = toLowerCaseMethodId },
            new Dictionary<string, object> { ["TypeId"] = stringTypeId, ["MethodsId"] = toUpperCaseMethodId },
            new Dictionary<string, object> { ["TypeId"] = stringTypeId, ["MethodsId"] = isNullOrEmptyMethodId },
            new Dictionary<string, object> { ["TypeId"] = stringTypeId, ["MethodsId"] = stringEqualsMethodId },
            new Dictionary<string, object> { ["TypeId"] = stringTypeId, ["MethodsId"] = methodId4 });
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
