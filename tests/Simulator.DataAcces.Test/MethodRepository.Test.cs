using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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
    private Parameter _parameter = new Parameter();
    private LocalVariable _localVariable = new LocalVariable();

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
        _oneMethodName = "First Method";
        _secondMethodName = "Second Method";
        _thirdMethodName = "Third Method";
        _accessibilityPublic = Accessibility.Public;
        _accessibilityPrivate = Accessibility.Private;
        _secondMethod = new Method(_secondId, _secondMethodName, _accessibilityPrivate, KeyWord.Abstract);
        _thirdMethod = new Method(_thirdId, _thirdMethodName, Accessibility.Protected, KeyWord.Sealed);
        _parameter = new Parameter(Guid.NewGuid(), "Parameter");
        _localVariable = new LocalVariable(Guid.NewGuid(), "LocalVariable");
        _oneMethod = new Method(_oneId, _oneMethodName, _accessibilityPublic, null, [_secondMethod],
            [_parameter], [_localVariable]);
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
        var saved = otherContext.Methods
            .Include(method => method.Parameters)
            .Include(method => method.LocalVariables)
            .Include(method => method.Methods)
            .Single(m => m.Id == _oneMethod.Id);

        saved.Id.Should().Be(_oneMethod.Id);
        saved.Name.Should().Be(_oneMethod.Name);
        saved.Accessibility.Should().Be(_oneMethod.Accessibility);
        saved.Stereotype.Should().BeNull();
        saved.Methods.Count.Should().Be(1);
        saved.Methods[0].Id.Should().Be(_secondMethod.Id);
        saved.Parameters[0].Id.Should().Be(_parameter.Id);
        saved.LocalVariables[0].Id.Should().Be(_localVariable.Id);
    }
    #endregion

    #region Find
    [TestMethod]
    public void Find_WhenMethodExists_ShouldReturnIt()
    {
        _context.Methods.Add(_oneMethod);
        _context.SaveChanges();

        var result = _repositoryTest.Find(_oneMethod.Id);
        var methodsList = result.Methods.ToList();
        var parametersList = result.Parameters.ToList();
        var localVariablesList = result.LocalVariables.ToList();

        result.Should().NotBeNull();
        result.Id.Should().Be(_oneMethod.Id);
        result.Name.Should().Be(_oneMethod.Name);
        result.Accessibility.Should().Be(_oneMethod.Accessibility);
        result.Stereotype.Should().Be(_oneMethod.Stereotype);
        methodsList.Should().NotBeEmpty();
        parametersList.Should().NotBeEmpty();
        localVariablesList.Should().NotBeEmpty();
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

    [TestMethod]
    public void Exist_WhenMethodDoesNotExist_ShouldReturnFalse()
    {
        var exists = _repositoryTest.Exist(Guid.NewGuid());

        exists.Should().BeFalse();
    }
    #endregion

    #region Delete
    [TestMethod]
    public void Delete_WhenAttributeExists_ShouldRemoveItFromDatabase()
    {
        _context.Methods.Add(_oneMethod);
        _context.SaveChanges();

        _repositoryTest.Delete(_oneMethod.Id);

        using var refreshedContext = ProgramTest.BuildTestDbContext();
        var deleted = refreshedContext.Methods.Find(_oneMethod.Id);
        deleted.Should().BeNull();
    }
    #endregion

    #region Update
    [TestMethod]
    public void Update_WhenMethodExists_ShouldAddAMethodToList()
    {
        _context.Methods.AddRange(_secondMethod, _thirdMethod);
        _context.SaveChanges();

        _secondMethod.Methods.Add(_thirdMethod);
        _repositoryTest.Update(_secondMethod);

        using var refreshedContext = ProgramTest.BuildTestDbContext();
        Method? saved = refreshedContext.Methods.Include(m => m.Methods)
            .FirstOrDefault(m => m.Id == _secondMethod.Id);

        saved.Should().NotBeNull();
        saved.Methods.Count.Should().Be(1);
        saved.Methods[0].Id.Should().Be(_thirdMethod.Id);
    }

    [TestMethod]
    public void Update_WhenMethodExists_ShouldAddAParameterToList()
    {
        _context.Methods.Add(_secondMethod);
        _context.Parameters.Add(_parameter);
        _context.SaveChanges();

        _secondMethod.Parameters.Add(_parameter);
        _repositoryTest.Update(_secondMethod);

        using var refreshedContext = ProgramTest.BuildTestDbContext();
        Method? saved = refreshedContext.Methods.Include(m => m.Parameters).Include(method => method.Methods)
            .FirstOrDefault(m => m.Id == _secondMethod.Id);

        saved.Should().NotBeNull();
        saved.Parameters.Count.Should().Be(1);
        saved.Parameters[0].Id.Should().Be(_parameter.Id);
    }
    #endregion
}
