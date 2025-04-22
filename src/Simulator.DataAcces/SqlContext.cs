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
    public DbSet<Class> Classes { get; set; } // Corrected to DbSet<Class>
    public DbSet<Primitive> Primitives { get; set; }
    public DbSet<Parameter> Parameters { get; set; }
    public DbSet<LocalVariable> LocalVariables { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Type hierarchy
        modelBuilder.Entity<Type>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<Class>()
            .HasBaseType<Type>();

        modelBuilder.Entity<Primitive>()
            .HasBaseType<Type>();

        // Configure Attribute
        modelBuilder.Entity<Attribute>()
            .HasKey(a => a.Id);

        modelBuilder.Entity<Method>()
            .HasMany(m => m.Methods)
            .WithMany()
            .UsingEntity(j => j.ToTable("MethodRelations"));

        modelBuilder.Entity<Method>()
            .HasMany(m => m.Parameters)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Method>()
            .HasMany(m => m.LocalVariables)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Method>()
            .HasOne(m => m.ReturnType)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        // Configure Parameter
        modelBuilder.Entity<Parameter>()
            .HasKey(p => p.Id);

        // Configure LocalVariable
        modelBuilder.Entity<LocalVariable>()
            .HasKey(l => l.Id);

        base.OnModelCreating(modelBuilder);
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
