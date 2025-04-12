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
    private Accessibility _accessibilityPublic;
    private Method _oneMethod = new Method();
    private Method _secondMethod = new Method();
    private Accessibility _accessibilityPrivate;

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
        _accessibilityPublic = Accessibility.Public;
        _oneMethod = new Method(_id, _methodName, _accessibilityPublic, null);
        _secondMethod = new Method(_id, _methodName, _accessibilityPrivate, null);
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
        _repositoryTest.Add(_oneMethod);

        using var otherContext = ProgramTest.BuildTestDbContext();
        var saved = otherContext.Methods.ToList();

        saved.Count.Should().Be(1);
        saved[0].Id.Should().Be(_oneMethod.Id);
        saved[0].Name.Should().Be(_oneMethod.Name);
        saved[0].Accessibility.Should().Be(_oneMethod.Accessibility);
        saved[0].Stereotype.Should().BeNull();
        saved[0].Methods.Should().NotBeNull();
        saved[0].Methods.Should().BeEmpty();
    }
    #endregion

    #region Find
    [TestMethod]
    public void Find_WhenMethodExists_ShouldReturnIt()
    {
        _context.Methods.Add(_secondMethod);
        _context.SaveChanges();

        var result = _repositoryTest.Find(_secondMethod.Id);

        result.Should().NotBeNull();
        result.Id.Should().Be(_secondMethod.Id);
        result.Name.Should().Be(_secondMethod.Name);
        result.Accessibility.Should().Be(_secondMethod.Accessibility);
        result.Stereotype.Should().Be(_secondMethod.Stereotype);
        result.Methods.Should().NotBeNull();
        result.Methods.Should().BeEmpty();
    }

    [TestMethod]
    public void Find_WhenMethodDoesNotExist_ShouldReturnNull()
    {
        var result = _repositoryTest.Find(_id);

        result.Should().BeNull();
    }
    #endregion
}
