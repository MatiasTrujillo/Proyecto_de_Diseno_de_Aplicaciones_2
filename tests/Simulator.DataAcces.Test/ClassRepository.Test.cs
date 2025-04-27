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
        aClass = new Class("Class1", KeyWord.Abstract, null, [], []);
        aClass.Id = _id;
        anotherClass = new Class("Class", null, null, [], []);
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
        _context.Classes.AddRange(aClass, anotherClass);
        _context.SaveChanges();

        var result = _repository.FindAll();

        result.Count.Should().Be(2);
        result.Should().Contain(a => a.Name == "Class");
        result.Should().Contain(a => a.Name == "Class1");
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

        var updated = new Class("Class1", null, null, [], []);
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
