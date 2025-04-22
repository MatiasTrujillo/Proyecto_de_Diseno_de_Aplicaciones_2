using FluentAssertions;
using Simulator.Domain;
using Attribute = Simulator.Domain.Attribute;
using Type = Simulator.Domain.Type;

namespace Simulator.DataAcces.Test;

[TestClass]
public class AttributeRepositoryTest
{
    private readonly SqlContext _context = ProgramTest.BuildTestDbContext();
    private readonly AttributeRepository _repository;
    private readonly Type _primitive = new Primitive(PrimitiveType.Int);

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
        var attribute = new Attribute(Guid.NewGuid(), "Strength", Accessibility.Public, _primitive);

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
        var attribute = new Attribute(Guid.NewGuid(), "Agility", Accessibility.Protected, _primitive);
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
        var attr1 = new Attribute(Guid.NewGuid(), "Speed", Accessibility.Private, _primitive);
        var attr2 = new Attribute(Guid.NewGuid(), "Intelligence", Accessibility.Public, _primitive);

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
        var attribute = new Attribute(Guid.NewGuid(), "Charisma", Accessibility.Public, _primitive);
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

    #region Update
    [TestMethod]
    public void Update_WhenAttributeExists_ShouldModifyIt()
    {
        var attribute = new Attribute(Guid.NewGuid(), "Wisdom", Accessibility.Protected, _primitive);
        _context.Attributes.Add(attribute);
        _context.SaveChanges();

        var updated = new Attribute(attribute.Id, "Knowledge", Accessibility.Private, _primitive);

        _repository.Update(updated);

        using var refreshedContext = ProgramTest.BuildTestDbContext();
        var saved = refreshedContext.Attributes.Find(attribute.Id);

        saved.Should().NotBeNull();
        saved.Name.Should().Be("Knowledge");
        saved.Accessibility.Should().Be(Accessibility.Private);
    }
    #endregion

    #region Delete
    [TestMethod]
    public void Delete_WhenAttributeExists_ShouldRemoveItFromDatabase()
    {
        var attribute = new Attribute(Guid.NewGuid(), "Dexterity", Accessibility.Public, _primitive);
        _context.Attributes.Add(attribute);
        _context.SaveChanges();

        _repository.Delete(attribute.Id);

        using var refreshedContext = ProgramTest.BuildTestDbContext();
        var deleted = refreshedContext.Attributes.Find(attribute.Id);
        deleted.Should().BeNull();
    }
    #endregion
}
