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
    public void Add_WhenValidLocalVariableProvided_ShouldBeSavedInDatabase()
    {
        _context.Primitives.Add(_primitive);
        _context.SaveChanges();

        _repository.Add(_localVariable);

        using var refreshedContext = ProgramTest.BuildTestDbContext();
        var saved = refreshedContext.LocalVariables.Include(l => l.LocalVariableType).FirstOrDefault(l => l.Id == _localVariable.Id);

        saved.Should().NotBeNull();
        saved!.Name.Should().Be(_localVariable.Name);
        saved.LocalVariableType.Should().NotBeNull();
        saved.LocalVariableType!.Id.Should().Be(_primitive.Id);
        saved.LocalVariableType.Name.Should().Be(_primitive.Name);
    }
    #endregion

    #region Find
    [TestMethod]
    public void Find_WhenLocalVariableExists_ShouldReturnItWithType()
    {
        _context.Primitives.Add(_primitive);
        _context.LocalVariables.Add(_localVariable);
        _context.SaveChanges();

        var result = _repository.Find(_localVariable.Id);

        result.Should().NotBeNull();
        result!.Id.Should().Be(_localVariable.Id);
        result.Name.Should().Be(_localVariable.Name);
        result.LocalVariableType.Should().NotBeNull();
        result.LocalVariableType!.Name.Should().Be(_primitive.Name);
    }

    [TestMethod]
    public void Find_WhenLocalVariableDoesNotExist_ShouldReturnNull()
    {
        var result = _repository.Find(Guid.NewGuid());

        result.Should().BeNull();
    }
    #endregion
}
