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

    #region FindAll
    [TestMethod]
    public void FindAll_WhenMultipleAttributesExist_ShouldReturnAll()
    {
        var attr1 = new Attribute(Guid.NewGuid(), "Speed", Accessibility.Private);
        var attr2 = new Attribute(Guid.NewGuid(), "Intelligence", Accessibility.Public);

        _context.Attributes.AddRange(attr1, attr2);
        _context.SaveChanges();

        var result = _repository.FindAll();

        result.Count.Should().Be(2);
        result.Should().Contain(a => a.Name == "Speed");
        result.Should().Contain(a => a.Name == "Intelligence");
    }
    #endregion

    #region Exist
    [TestMethod]
    public void Exist_WhenAttributeExists_ShouldReturnTrue()
    {
        var attribute = new Attribute(Guid.NewGuid(), "Charisma", Accessibility.Public);
        _context.Attributes.Add(attribute);
        _context.SaveChanges();

        var exists = _repository.Exist(attribute.Id);

        exists.Should().BeTrue();
    }

    [TestMethod]
    public void Exist_WhenAttributeDoesNotExist_ShouldReturnFalse()
    {
        var exists = _repository.Exist(Guid.NewGuid());

        exists.Should().BeFalse();
    }
    #endregion

}
