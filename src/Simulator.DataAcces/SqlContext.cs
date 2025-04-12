using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Simulator.Domain;
using Attribute = Simulator.Domain.Attribute;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attribute>()
            .HasKey(a => a.Id);
        modelBuilder.Entity<Method>()
            .HasKey(m => m.Id);

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
