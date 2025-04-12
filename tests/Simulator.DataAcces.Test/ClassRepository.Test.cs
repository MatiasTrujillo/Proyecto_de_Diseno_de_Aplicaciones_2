using FluentAssertions;
using Simulator.Domain;
using Attribute = Simulator.Domain.Attribute;

namespace Simulator.DataAcces.Test;

[TestClass]
public class ClassRepositoryTest
{
    private readonly SqlContext _context = ProgramTest.BuildTestDbContext();

    private readonly ClassRepository _repository;

    public ClassRepositoryTest()
    {
        _repository = new ClassRepository(_context);
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

    [TestMethod]
    public void Add_WhenValidClassProvided_ShouldBeSavedInDatabase()
    {
        var aClass = new Class(Guid.NewGuid(), "juan", null, null, new List<Attribute>());

        _repository.Add(aClass);

        using var otherContext = ProgramTest.BuildTestDbContext();
        var saved = otherContext.Classes.ToList();

        saved.Count.Should().Be(1);
        saved[0].Name.Should().Be(aClass.Name);
        saved[0].Stereotype.Should().Be(aClass.Stereotype);
    }
}
