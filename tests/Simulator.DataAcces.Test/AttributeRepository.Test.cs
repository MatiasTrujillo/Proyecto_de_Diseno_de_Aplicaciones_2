using FluentAssertions;
using Simulator.Domain;
using Attribute = Simulator.Domain.Attribute;

namespace Simulator.DataAcces.Test;

[TestClass]
public class AttributeRepositoryTest
{
    private readonly SqlContext _context = ProgramTest.BuildTestDbContext();
    private readonly AttributeRepository _repository;

    public AttributeRepositoryTest()
    {
        _repository = new AttributeRepository(_context);
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
    public void Add_WhenValidAttributeProvided_ShouldBeSavedInDatabase()
    {
        var attribute = new Attribute(Guid.NewGuid(), "Strength", Accessibility.Public);

        _repository.Add(attribute);

        using var otherContext = ProgramTest.BuildTestDbContext();
        var saved = otherContext.Attributes.ToList();

        saved.Count.Should().Be(1);
        saved[0].Id.Should().Be(attribute.Id);
        saved[0].Name.Should().Be(attribute.Name);
        saved[0].Accessibility.Should().Be(attribute.Accessibility);
    }
    #endregion
    #region Find
    [TestMethod]
    public void Find_WhenAttributeExists_ShouldReturnIt()
    {
        var attribute = new Attribute(Guid.NewGuid(), "Agility", Accessibility.Protected);
        _context.Attributes.Add(attribute);
        _context.SaveChanges();

        var result = _repository.Find(attribute.Id);

        result.Should().NotBeNull();
        result.Id.Should().Be(attribute.Id);
        result.Name.Should().Be("Agility");
    }

    [TestMethod]
    public void Find_WhenAttributeDoesNotExist_ShouldReturnNull()
    {
        var result = _repository.Find(Guid.NewGuid());

        result.Should().BeNull();
    }

    #endregion
}
