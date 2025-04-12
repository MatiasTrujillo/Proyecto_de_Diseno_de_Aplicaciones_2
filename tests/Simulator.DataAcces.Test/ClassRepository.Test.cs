using FluentAssertions;
using Simulator.Domain;
using Attribute = Simulator.Domain.Attribute;

namespace Simulator.DataAcces.Test;

[TestClass]
public class ClassRepositoryTest
{
    private readonly SqlContext _context = ProgramTest.BuildTestDbContext();

    private readonly ClassRepository _repository;
    private Class aClass;

    public ClassRepositoryTest()
    {
        _repository = new ClassRepository(_context);
        aClass = new Class(Guid.NewGuid(), "jose", KeyWord.Abstract, null, new List<Attribute>());
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
    public void Add_WhenValidClassProvided_ShouldBeSavedInDatabase()
    {
        _repository.Add(aClass);

        using var otherContext = ProgramTest.BuildTestDbContext();
        var saved = otherContext.Classes.ToList();

        saved.Count.Should().Be(1);
        saved[0].Id.Should().Be(aClass.Id);
        saved[0].Name.Should().Be(aClass.Name);
        saved[0].Stereotype.Should().Be(aClass.Stereotype);
    }
    #endregion

    #region Find
    [TestMethod]
    public void Find_WhenClassExists_ShouldReturnIt()
    {
        _context.Classes.Add(aClass);
        _context.SaveChanges();

        var result = _repository.Find(aClass.Id);

        result.Should().NotBeNull();
        result.Id.Should().Be(aClass.Id);
        result.Name.Should().Be(aClass.Name);
    }
    #endregion
}
