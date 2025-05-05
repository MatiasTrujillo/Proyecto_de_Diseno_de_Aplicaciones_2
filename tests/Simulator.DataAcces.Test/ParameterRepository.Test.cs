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

    #region Find
    [TestMethod]
    public void Find_WhenParameterExists_ShouldReturnItWithType()
    {
        _context.Primitives.Add(_primitive);
        _context.Parameters.Add(_parameter);
        _context.SaveChanges();

        var result = _repository.Find(_parameter.Id);

        result.Should().NotBeNull();
        result!.Id.Should().Be(_parameter.Id);
        result.Name.Should().Be(_parameter.Name);
        result.ParameterType.Should().NotBeNull();
        result.ParameterType!.Name.Should().Be(_primitive.Name);
    }

    [TestMethod]
    public void Find_WhenParameterDoesNotExist_ShouldReturnNull()
    {
        var result = _repository.Find(Guid.NewGuid());

        result.Should().BeNull();
    }
    #endregion

    #region FindAll
    [TestMethod]
    public void FindAll_WhenMultipleParametersExist_ShouldReturnAll()
    {
        var anotherParameter = new Parameter
        {
            Id = Guid.NewGuid(),
            Name = "param2",
            ParameterType = _primitive
        };

        _context.Primitives.Add(_primitive);
        _context.Parameters.Add(_parameter);
        _context.Parameters.Add(anotherParameter);
        _context.SaveChanges();

        var result = _repository.FindAll();

        result.Should().HaveCount(3);
        result.Should().Contain(p => p.Name == _parameter.Name);
        result.Should().Contain(p => p.Name == anotherParameter.Name);
    }

    [TestMethod]
    public void FindAll_WhenNoParametersExist_ShouldReturnAListWithOnePreInstalledParameter()
    {
        var result = _repository.FindAll();

        result.Count.Should().Be(1);
        result.Should().Contain(p => p.Name == "Obj");
    }
    #endregion

    #region Update
    [TestMethod]
    public void Update_WhenParameterExists_ShouldUpdateValuesInDatabase()
    {
        _context.Primitives.Add(_primitive);
        _context.Parameters.Add(_parameter);
        _context.SaveChanges();

        var updatedParameter = new Parameter
        {
            Id = _parameter.Id,
            Name = "updatedParam",
            ParameterType = _primitive
        };

        _repository.Update(updatedParameter);

        using var refreshedContext = ProgramTest.BuildTestDbContext();
        var updatedInDb = refreshedContext.Parameters.Include(p => p.ParameterType).FirstOrDefault(p => p.Id == _parameter.Id);

        updatedInDb.Should().NotBeNull();
        updatedInDb!.Name.Should().Be("updatedParam");
        updatedInDb.ParameterType.Should().NotBeNull();
        updatedInDb.ParameterType!.Id.Should().Be(_primitive.Id);
    }
    #endregion
}
