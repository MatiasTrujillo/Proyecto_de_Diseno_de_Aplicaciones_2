using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Simulator.Domain;

namespace Simulator.DataAcces.Test;

[TestClass]
public class ParameterRepositoryTest
{
    private readonly SqlContext _context = ProgramTest.BuildTestDbContext();
    private readonly ParameterRepository _repository;
    private readonly Parameter _parameter;
    private readonly Primitive _primitive;

    public ParameterRepositoryTest()
    {
        _repository = new ParameterRepository(_context);

        _primitive = new Primitive
        {
            Id = Guid.NewGuid(),
            Name = "int"
        };

        _parameter = new Parameter
        {
            Id = Guid.NewGuid(),
            Name = "myParam",
            ParameterType = _primitive
        };
    }

    [TestInitialize]
    public void Setup()
    {
        _context.Database.EnsureCreated();
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
    }

    #region Add
    [TestMethod]
    public void Add_WhenValidParameterProvided_ShouldBeSavedInDatabase()
    {
        _context.Primitives.Add(_primitive);
        _context.SaveChanges();

        _repository.Add(_parameter);

        using var refreshedContext = ProgramTest.BuildTestDbContext();
        var saved = refreshedContext.Parameters.Include(p => p.ParameterType).FirstOrDefault(p => p.Id == _parameter.Id);

        saved.Should().NotBeNull();
        saved!.Name.Should().Be(_parameter.Name);
        saved.ParameterType.Should().NotBeNull();
        saved.ParameterType!.Id.Should().Be(_primitive.Id);
        saved.ParameterType.Name.Should().Be(_primitive.Name);
    }
    #endregion
}
