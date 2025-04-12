using FluentAssertions;
using Simulator.Domain;

namespace Simulator.DataAcces.Test;

[TestClass]
public class MethodRepositoryTest
{
    private readonly SqlContext _context = ProgramTest.BuildTestDbContext();
    private readonly MethodRepository _repositoryTest;
    private Guid _id;
    private string? _methodName;
    private Accessibility _accessibility;

    public MethodRepositoryTest()
    {
        _repositoryTest = new MethodRepository(_context);
    }

    [TestInitialize]
    public void Setup()
    {
        _context.Database.EnsureCreated();
        _id = Guid.NewGuid();
        _methodName = "Method";
        _accessibility = Accessibility.Public;
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
    }

    #region Add
    [TestMethod]
    public void Add_WhenValidMethodProvided_ShouldBeSavedInDatabase()
    {
        Method method = new Method(_id, _methodName, _accessibility, null);

        _repositoryTest.Add(method);

        using var otherContext = ProgramTest.BuildTestDbContext();
        var saved = otherContext.Methods.ToList();

        saved.Count.Should().Be(1);
        saved[0].Id.Should().Be(method.Id);
        saved[0].Name.Should().Be(method.Name);
        saved[0].Accessibility.Should().Be(method.Accessibility);
        saved[0].Stereotype.Should().BeNull();
        saved[0].Methods.Should().NotBeNull();
        saved[0].Methods.Should().BeEmpty();
    }
    #endregion

    [TestMethod]
    public void Find_WhenMethodExists_ShouldReturnIt()
    {
        Method method = new Method(Guid.NewGuid(), "Agility", Accessibility.Protected, null);
        _context.Methods.Add(method);
        _context.SaveChanges();

        var result = _repositoryTest.Find(method.Id);

        result.Should().NotBeNull();
        result.Id.Should().Be(method.Id);
        result.Name.Should().Be("Agility");
        result.Accessibility.Should().Be(method.Accessibility);
        result.Stereotype.Should().Be(method.Stereotype);
        result.Methods.Should().NotBeNull();
        result.Methods.Should().BeEmpty();
    }
}
