using FluentAssertions;
using Simulator.Domain;

namespace Simulator.DataAcces.Test;

[TestClass]
public class MethodRepositoryTest
{
    private readonly SqlContext _context = ProgramTest.BuildTestDbContext();
    private readonly MethodRepository _repositoryTest;
    private Guid _oneId;
    private string? _oneMethodName;
    private Accessibility _accessibilityPublic;
    private Method _oneMethod = new Method();
    private Method _secondMethod = new Method();
    private Accessibility _accessibilityPrivate;
    private Guid _secondId;
    private Guid _thirdId;
    private string? _secondMethodName;
    private string? _thirdMethodName;
    private Method _thirdMethod = new Method();

    public MethodRepositoryTest()
    {
        _repositoryTest = new MethodRepository(_context);
    }

    [TestInitialize]
    public void Setup()
    {
        _context.Database.EnsureCreated();
        _oneId = Guid.NewGuid();
        _secondId = Guid.NewGuid();
        _thirdId = Guid.NewGuid();
        _oneMethodName = "Method";
        _secondMethodName = "Second Method";
        _thirdMethodName = "Third Method";
        _accessibilityPublic = Accessibility.Public;
        _oneMethod = new Method(_oneId, _oneMethodName, _accessibilityPublic, null);
        _secondMethod = new Method(_secondId, _secondMethodName, _accessibilityPrivate, KeyWord.Abstract);
        _thirdMethod = new Method(_thirdId, _thirdMethodName, Accessibility.Protected, KeyWord.Sealed);
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
        var result = _repositoryTest.Find(_oneId);

        result.Should().BeNull();
    }
    #endregion

    #region FindAll
    [TestMethod]
    public void FindAll_WhenMultipleMethodsExist_ShouldReturnAll()
    {
        _context.Methods.AddRange(_oneMethod, _secondMethod, _thirdMethod);
        _context.SaveChanges();

        var result = _repositoryTest.FindAll();

        result.Count.Should().Be(3);
        result.Should().Contain(m => m.Name == _oneMethod.Name);
        result.Should().Contain(a => a.Name == _secondMethod.Name);
        result.Should().Contain(a => a.Name == _thirdMethod.Name);
    }
    #endregion

    #region Exist
    [TestMethod]
    public void Exist_WhenMethodExists_ShouldReturnTrue()
    {
        _context.Methods.Add(_oneMethod);
        _context.SaveChanges();

        var exists = _repositoryTest.Exist(_oneId);

        exists.Should().BeTrue();
    }
    #endregion
}
