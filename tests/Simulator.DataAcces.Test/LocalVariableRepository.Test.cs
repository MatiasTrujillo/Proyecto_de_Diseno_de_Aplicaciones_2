using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Simulator.Domain;

namespace Simulator.DataAcces.Test;

[TestClass]
public class LocalVariableRepositoryTest
{
    private readonly SqlContext _context = ProgramTest.BuildTestDbContext();
    private readonly LocalVariableRepository _repository;
    private readonly LocalVariable _localVariable;
    private readonly Primitive _primitive;

    public LocalVariableRepositoryTest()
    {
        _repository = new LocalVariableRepository(_context);

        _primitive = new Primitive
        {
            Id = Guid.NewGuid(),
            Name = "int"
        };

        _localVariable = new LocalVariable
        {
            Id = Guid.NewGuid(),
            Name = "myParam",
            LocalVariableType = _primitive
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

        _repository.Add(_localVariable);

        using var refreshedContext = ProgramTest.BuildTestDbContext();
        var saved = refreshedContext.LocalVariables.Include(p => p.LocalVariableType).FirstOrDefault(p => p.Id == _localVariable.Id);

        saved.Should().NotBeNull();
        saved!.Name.Should().Be(_localVariable.Name);
        saved.LocalVariableType.Should().NotBeNull();
        saved.LocalVariableType!.Id.Should().Be(_primitive.Id);
        saved.LocalVariableType.Name.Should().Be(_primitive.Name);
    }
    #endregion
}
