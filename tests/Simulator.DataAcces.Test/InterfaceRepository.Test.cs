using FluentAssertions;
using Simulator.Domain;

namespace Simulator.DataAcces.Test;

[TestClass]
public class InterfaceRepository_Test
{
    private readonly SqlContext _context = ProgramTest.BuildTestDbContext();
    private readonly InterfaceRepository _repository;
    private readonly Interface anInterface;
    private readonly Interface anotherInterface;
    private readonly Primitive aPrimitive;
    private readonly Parameter aParameter;
    private readonly MethodSignature aMethodSignature;

    public InterfaceRepository_Test()
    {
        _repository = new InterfaceRepository(_context);
        anInterface = new Interface("Interface");
        anotherInterface = new Interface("AnotherInterface");
        aPrimitive = new Primitive
        {
            Id = Guid.NewGuid(),
            Name = "int"
        };
        aParameter = new Parameter
        {
            Id = Guid.NewGuid(),
            Name = "myParam",
            ParameterType = aPrimitive
        };
        aMethodSignature = new MethodSignature("AMethodSignature", aPrimitive, [aParameter]);
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
    public void Add_WhenValidInterfaceProvided_ShouldBeSavedInDatabase()
    {
        _repository.Add(anInterface);

        using var otherContext = ProgramTest.BuildTestDbContext();
        var saved = otherContext.Set<Interface>().ToList();

        saved.Count.Should().Be(1);
        saved[0].Id.Should().Be(anInterface.Id);
        saved[0].Name.Should().Be(anInterface.Name);
    }
    #endregion

    #region Find
    [TestMethod]
    public void Find_WhenInterfaceExists_ShouldReturnIt()
    {
        _context.Interfaces.Add(anInterface);

        var result = _repository.Find(anInterface.Id);

        result.Should().NotBeNull();
        result.Id.Should().Be(anInterface.Id);
        result.Name.Should().Be(anInterface.Name);
    }
    #endregion

    #region FindAll
    [TestMethod]
    public void FindAll_WhenMultipleInterfacesExist_ShouldReturnAll()
    {
        _repository.Add(anInterface);
        _repository.Add(anotherInterface);

        var result = _repository.FindAll();

        result.Count.Should().Be(2);

        result.Should().Contain(i => i.Name == anInterface.Name);
        result.Should().Contain(i => i.Name == anotherInterface.Name);
    }
    #endregion

    #region Exist
    [TestMethod]
    public void Exist_WhenInterfaceExists_ShouldReturnTrue()
    {
        _repository.Add(anInterface);

        var exists = _repository.Exist(anInterface.Id);

        exists.Should().BeTrue();
    }

    [TestMethod]
    public void Exist_WhenInterfaceDoesNotExist_ShouldReturnFalse()
    {
        var exists = _repository.Exist(Guid.NewGuid());

        exists.Should().BeFalse();
    }
    #endregion

    #region Update
    [TestMethod]
    public void Update_WhenInterfaceExists_ShouldModifyIt()
    {
        _repository.Add(anInterface);

        var updatedInterface = new Interface("OtherName", []);
        updatedInterface.Id = anInterface.Id;

        _repository.Update(updatedInterface);

        using var refreshedContext = ProgramTest.BuildTestDbContext();
        var saved = refreshedContext.Set<Interface>().Find(anInterface.Id);

        saved.Should().NotBeNull();
        saved.Name.Should().NotBe("OtherName");
    }

    [TestMethod]
    public void Update_WhenInterfaceExistsAndMethodSignature_ShouldAddItToTheInterface()
    {
        _repository.Add(anInterface);

        using var refreshedContext = ProgramTest.BuildTestDbContext();
        refreshedContext.Primitives.Add(aPrimitive);
        refreshedContext.SaveChanges();
        refreshedContext.Parameters.Add(aParameter);
        refreshedContext.MethodSignatures.Add(aMethodSignature);
        refreshedContext.SaveChanges();

        var updatedInterface = new Interface("OtherName", [aMethodSignature])
        {
            Id = anInterface.Id
        };

        var repo = new InterfaceRepository(refreshedContext);
        repo.Update(updatedInterface);
        refreshedContext.SaveChanges();

        var saved = refreshedContext.Set<Interface>().Find(anInterface.Id);

        saved.Should().NotBeNull();
        saved.Id.Should().Be(anInterface.Id);
        saved.MethodSignatures.Count.Should().Be(1);
        saved.MethodSignatures[0].Id.Should().Be(aMethodSignature.Id);
    }
    #endregion

    #region Delete
    [TestMethod]
    public void Delete_WhenInterfaceExists_ShouldRemoveItFromDatabase()
    {
        _context.Interfaces.Add(anInterface);
        _context.SaveChanges();

        _repository.Delete(anInterface.Id);

        using var refreshedContext = ProgramTest.BuildTestDbContext();
        var deleted = refreshedContext.Interfaces.Find(anInterface.Id);
        deleted.Should().BeNull();
    }
    #endregion
}
