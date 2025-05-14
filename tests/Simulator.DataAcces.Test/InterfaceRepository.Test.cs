using FluentAssertions;
using Simulator.Domain;

namespace Simulator.DataAcces.Test;

[TestClass]
public class InterfaceRepository_Test
{
    private static readonly SqlContext _context = ProgramTest.BuildTestDbContext();
    private readonly InterfaceRepository _repository = new InterfaceRepository(_context);

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
    public void Add_WhenValidInterfaceProvided_ShouldBeSavedInDatabase()
    {
        Interface anInterface = new Interface("Interface");
        _repository.Add(anInterface);

        using var otherContext = ProgramTest.BuildTestDbContext();
        var saved = otherContext.Set<Interface>().ToList();

        saved.Count.Should().Be(1);
        saved[0].Id.Should().Be(anInterface.Id);
        saved[0].Name.Should().Be(anInterface.Name);
    }
}
