using FluentAssertions;
using Simulator.Domain;

namespace Simulator.DataAcces.Test;

[TestClass]
public class ClassRepositoryTest
{
    private readonly SqlContext _context = ProgramTest.BuildTestDbContext();

    private readonly ClassRepository _repository;
    private readonly Class aClass;
    private readonly Guid _id;
    private readonly Class anotherClass;

    public ClassRepositoryTest()
    {
        _id = Guid.NewGuid();
        _repository = new ClassRepository(_context);
        aClass = new Class(KeyWord.Abstract, null, [], [], "Class1") { Id = _id };
        anotherClass = new Class(null, null, [], [], "Class1");
        aClass.Name = "Class";
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
        var saved = otherContext.Set<Class>().ToList();

        saved.Count.Should().Be(2);
        saved[1].Id.Should().Be(aClass.Id);
        saved[1].Name.Should().Be(aClass.Name);
        saved[1].Stereotype.Should().Be(aClass.Stereotype);
    }
    #endregion

    #region Find

    [TestMethod]
    public void Find_WhenNothing_ShouldHaveObjectClass()
    {
        var result = _repository.Find(Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"));

        result.Name.Should().Be("Object");
    }

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

    [TestMethod]
    public void Find_WhenClassDoesNotExist_ShouldReturnNull()
    {
        var result = _repository.Find(Guid.NewGuid());

        result.Should().BeNull();
    }
    #endregion

    #region FindAll
    [TestMethod]
    public void FindAll_WhenMultipleClassesExist_ShouldReturnAll()
    {
        var aClass = new Simulator.Domain.Class { Name = "Class" };
        var anotherClass = new Simulator.Domain.Class { Name = "Class1" };

        _context.Classes.AddRange(aClass, anotherClass);
        _context.SaveChanges();

        var result = _repository.FindAll();

        result.Count.Should().Be(3);

        result.Should().Contain(a => a.Name == "Class");
        result.Should().Contain(a => a.Name == "Class1");
    }
    #endregion

    #region FindAllTypes
    [TestMethod]
    public void FindAllTypes_WhenTypesExist_ShouldReturnAllTypes()
    {
        var aClassType = new Simulator.Domain.Class { Name = "MyTypeClass" };
        var anotherClassType = new Simulator.Domain.Primitive { Name = "MyTypeClass1" };
        _context.Classes.Add(aClassType);
        _context.Primitives.Add(anotherClassType);
        _context.SaveChanges();

        var result = _repository.FindAllTypes();

        result.Should().NotBeNull();
        result.Count.Should().BeGreaterThanOrEqualTo(2);
        result.Should().Contain(t => t.Name == "MyTypeClass1");
        result.Should().Contain(t => t.Name == "MyTypeClass");
    }
    #endregion

    #region Exist
    [TestMethod]
    public void Exist_WhenClassExists_ShouldReturnTrue()
    {
        _context.Classes.Add(aClass);
        _context.SaveChanges();

        var exists = _repository.Exist(aClass.Id);

        exists.Should().BeTrue();
    }

    [TestMethod]
    public void Exist_WhenClassDoesNotExist_ShouldReturnFalse()
    {
        var exists = _repository.Exist(Guid.NewGuid());

        exists.Should().BeFalse();
    }
    #endregion

    #region Update
    [TestMethod]
    public void Update_WhenClassExists_ShouldModifyIt()
    {
        _context.Classes.Add(aClass);
        _context.SaveChanges();

        var updated = new Class(null, null, [], [], "Class1");
        updated.Id = aClass.Id;

        _repository.Update(updated);

        using var refreshedContext = ProgramTest.BuildTestDbContext();
        var saved = refreshedContext.Set<Class>().Find(aClass.Id);

        saved.Should().NotBeNull();
        saved.Name.Should().Be("Class1");
        saved.Stereotype.Should().Be(null);
    }
    #endregion

    #region Delete
    [TestMethod]
    public void Delete_WhenClassExists_ShouldRemoveItFromDatabase()
    {
        _context.Classes.Add(aClass);
        _context.SaveChanges();

        _repository.Delete(aClass.Id);

        using var refreshedContext = ProgramTest.BuildTestDbContext();
        var deleted = refreshedContext.Classes.Find(aClass.Id);
        deleted.Should().BeNull();
    }
    #endregion
}
