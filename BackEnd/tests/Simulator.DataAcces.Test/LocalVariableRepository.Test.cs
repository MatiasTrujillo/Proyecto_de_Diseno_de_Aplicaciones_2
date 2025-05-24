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

    #region FindAll
    [TestMethod]
    public void FindAll_WhenLocalVariablesExist_ShouldReturnAllWithTypes()
    {
        var secondPrimitive = new Primitive
        {
            Id = Guid.NewGuid(),
            Name = "string"
        };

        var secondLocalVariable = new LocalVariable
        {
            Id = Guid.NewGuid(),
            Name = "myOtherParam",
            LocalVariableType = secondPrimitive
        };

        _context.Primitives.AddRange(_primitive, secondPrimitive);
        _context.LocalVariables.AddRange(_localVariable, secondLocalVariable);
        _context.SaveChanges();

        var results = _repository.FindAll();

        results.Should().NotBeNull();
        results.Should().HaveCount(2);

        results.Should().ContainSingle(v => v.Id == _localVariable.Id &&
                                            v.LocalVariableType != null &&
                                            v.LocalVariableType.Name == _primitive.Name);

        results.Should().ContainSingle(v => v.Id == secondLocalVariable.Id &&
                                            v.LocalVariableType != null &&
                                            v.LocalVariableType.Name == secondPrimitive.Name);
    }
    #endregion

    #region Update
    [TestMethod]
    public void Update_WhenLocalVariableExists_ShouldUpdateValuesInDatabase()
    {
        _context.Primitives.Add(_primitive);
        _context.LocalVariables.Add(_localVariable);
        _context.SaveChanges();

        var updatedVariable = new LocalVariable
        {
            Id = _localVariable.Id,
            Name = "updatedParam",
            LocalVariableType = _primitive
        };

        _repository.Update(updatedVariable);

        using var refreshedContext = ProgramTest.BuildTestDbContext();
        var updatedInDb = refreshedContext.LocalVariables.Include(l => l.LocalVariableType).FirstOrDefault(l => l.Id == _localVariable.Id);

        updatedInDb.Should().NotBeNull();
        updatedInDb!.Name.Should().Be("updatedParam");
        updatedInDb.LocalVariableType.Should().NotBeNull();
        updatedInDb.LocalVariableType!.Id.Should().Be(_primitive.Id);
    }
    #endregion
}
