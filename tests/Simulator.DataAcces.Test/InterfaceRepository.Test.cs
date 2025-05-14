using FluentAssertions;
using Simulator.Domain;

namespace Simulator.DataAcces.Test;

[TestClass]
public class InterfaceRepository_Test
{
    private static readonly SqlContext _context = ProgramTest.BuildTestDbContext();
    private readonly InterfaceRepository _repository = new InterfaceRepository(_context);
    private Interface anInterface;

    public InterfaceRepository_Test()
    {
        anInterface = new Interface("Interface");
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

    [TestMethod]
    public void FindAll_WhenMultipleInterfacesExist_ShouldReturnAll()
    {
        Interface anotherInterface = new Interface("AnotherInterface");

        _context.Interfaces.AddRange(anInterface, anotherInterface);

        var result = _repository.FindAll();

        result.Count.Should().Be(2);

        result.Should().Contain(i => i.Name == "Interface");
        result.Should().Contain(i => i.Name == "AnotherInterface");
    }
}
