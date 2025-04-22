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
    private readonly Class _class = new Class();
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
        _class.Id = Guid.NewGuid();
        _oneId = Guid.NewGuid();
        _secondId = Guid.NewGuid();
        _thirdId = Guid.NewGuid();
        _oneMethodName = "First Method";
        _secondMethodName = "Second Method";
        _thirdMethodName = "Third Method";
        _accessibilityPublic = Accessibility.Public;
        _accessibilityPrivate = Accessibility.Private;
        _secondMethod = new Method(_secondMethodName, _accessibilityPrivate, KeyWord.Abstract,
             null, null, null, _class);
        _thirdMethod = new Method(_thirdMethodName, Accessibility.Protected, KeyWord.Sealed, null, null, null,
            _class);
        _parameter = new Parameter(Guid.NewGuid(), "Parameter", _class);
        _localVariable = new LocalVariable(Guid.NewGuid(), "LocalVariable", null);
        _oneMethod = new Method(_oneMethodName, _accessibilityPublic, null, [_secondMethod],
            [_parameter], [_localVariable], null);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
    }

    [TestMethod]
    public void Add_WhenCalledWithValidMethod_ShouldPersistMethodWithRelations()
    {
        var returnType = new Simulator.Domain.Class { Id = Guid.NewGuid(), Name = "ReturnClass" };
        var parameter = new Parameter { Id = Guid.NewGuid(), Name = "param1" };
        var localVariable = new LocalVariable { Id = Guid.NewGuid(), Name = "localVar1" };
        var calledMethod = new Method { Id = Guid.NewGuid(), Name = "CalledMethod" };

        _context.Classes.Add(returnType);
        _context.Parameters.Add(parameter);
        _context.LocalVariables.Add(localVariable);
        _context.Methods.Add(calledMethod);
        _context.SaveChanges();

        var method = new Method
        {
            Id = Guid.NewGuid(),
            Name = "MainMethod",
            ReturnType = returnType,
            Parameters = [parameter],
            LocalVariables = [localVariable],
            Methods = [calledMethod]
        };

        _repositoryTest.Add(method);

        using var refreshedContext = ProgramTest.BuildTestDbContext();
        var saved = refreshedContext.Methods
            .Include(m => m.ReturnType)
            .Include(m => m.Parameters)
            .Include(m => m.LocalVariables)
            .Include(m => m.Methods)
            .FirstOrDefault(m => m.Id == method.Id);

        saved.Should().NotBeNull();
        saved!.ReturnType.Should().NotBeNull();
        saved.ReturnType.Id.Should().Be(returnType.Id);

        saved.Parameters.Should().HaveCount(1);
        saved.Parameters[0].Id.Should().Be(parameter.Id);

        saved.LocalVariables.Should().HaveCount(1);
        saved.LocalVariables[0].Id.Should().Be(localVariable.Id);

        saved.Methods.Should().HaveCount(1);
        saved.Methods[0].Id.Should().Be(calledMethod.Id);
    }

    [TestMethod]
    public void Find_WhenMethodExists_ShouldReturnMethodWithRelations()
    {
        var returnType = new Class { Id = Guid.NewGuid(), Name = "ReturnClass" };
        var parameter = new Parameter { Id = Guid.NewGuid(), Name = "Parameter1", ParameterType = returnType };
        var localVariable = new LocalVariable { Id = Guid.NewGuid(), Name = "LocalVar1", LocalVariableType = returnType };
        var relatedMethod = new Method { Id = Guid.NewGuid(), Name = "RelatedMethod" };

        _context.Classes.Add(returnType);
        _context.Parameters.Add(parameter);
        _context.LocalVariables.Add(localVariable);
        _context.Methods.Add(relatedMethod);
        _context.SaveChanges();

        var mainMethod = new Method
        {
            Id = Guid.NewGuid(),
            Name = "MainMethod",
            ReturnType = returnType,
            Parameters = [parameter],
            LocalVariables = [localVariable],
            Methods = [relatedMethod]
        };

        _context.Methods.Add(mainMethod);
        _context.SaveChanges();

        var result = _repositoryTest.Find(mainMethod.Id);

        result.Should().NotBeNull();
        result!.Id.Should().Be(mainMethod.Id);
        result.Name.Should().Be("MainMethod");

        result.ReturnType.Should().NotBeNull();
        result.ReturnType.Id.Should().Be(returnType.Id);

        result.Parameters.Should().HaveCount(1);
        result.Parameters[0].Id.Should().Be(parameter.Id);

        result.LocalVariables.Should().HaveCount(1);
        result.LocalVariables[0].Id.Should().Be(localVariable.Id);

        result.Methods.Should().HaveCount(1);
        result.Methods[0].Id.Should().Be(relatedMethod.Id);
    }

    [TestMethod]
    public void Update_WhenMethodExists_ShouldUpdateParameterList()
    {
        var methodId = Guid.NewGuid();
        var parameterId = Guid.NewGuid();

        var originalMethod = new Method
        {
            Id = methodId,
            Name = "TestMethod",
            Parameters = [],
            ReturnType = new Primitive()
        };

        var parameter = new Parameter
        {
            Id = parameterId,
            Name = "Param1",
            ParameterType = new Primitive()
        };
        _context.Methods.Add(originalMethod);
        _context.Parameters.Add(parameter);
        _context.SaveChanges();

        originalMethod.Parameters.Add(parameter);
        _repositoryTest.Update(originalMethod);

        using var refreshedContext = ProgramTest.BuildTestDbContext();
        var saved = refreshedContext.Methods
            .Include(m => m.Parameters)
            .FirstOrDefault(m => m.Id == methodId);

        saved.Should().NotBeNull();
        saved!.Parameters.Should().ContainSingle(p => p.Id == parameterId);
    }

    [TestMethod]
    public void FindAll_WhenMethodsExist_ShouldReturnMethodsWithRelations()
    {
        var methodId1 = Guid.NewGuid();
        var methodId2 = Guid.NewGuid();

        var method1 = new Method
        {
            Id = methodId1,
            Name = "Method 1",
            Parameters = [new Parameter { Id = Guid.NewGuid(), Name = "param1", ParameterType = new Primitive() }],
            LocalVariables =
                [new LocalVariable { Id = Guid.NewGuid(), Name = "localVar1", LocalVariableType = new Primitive() }],
            ReturnType = new Primitive()
        };

        var method2 = new Method
        {
            Id = methodId2,
            Name = "Method 2",
            Parameters = [new Parameter { Id = Guid.NewGuid(), Name = "param2", ParameterType = new Primitive() }],
            LocalVariables =
                [new LocalVariable { Id = Guid.NewGuid(), Name = "localVar2", LocalVariableType = new Primitive() }],
            ReturnType = new Primitive()
        };

        _context.Methods.Add(method1);
        _context.Methods.Add(method2);
        _context.SaveChanges();

        var methods = _repositoryTest.FindAll();

        methods.Should().NotBeNull();
        methods.Should().HaveCount(2);
        methods.Should().Contain(m => m.Id == methodId1);
        methods.Should().Contain(m => m.Id == methodId2);

        methods[0].Parameters.Should().NotBeEmpty();
        methods[0].LocalVariables.Should().NotBeEmpty();
        methods[0].ReturnType.Should().NotBeNull();
    }

    [TestMethod]
    public void Exist_WhenMethodExists_ShouldReturnTrue()
    {
        var id = Guid.NewGuid();
        var method = new Method { Id = id, Name = "TestMethod", ReturnType = new Primitive() };
        _context.Methods.Add(method);
        _context.SaveChanges();

        var result = _repositoryTest.Exist(id);

        result.Should().BeTrue();
    }

    [TestMethod]
    public void Exist_WhenMethodDoesNotExist_ShouldReturnFalse()
    {
        var nonExistingId = Guid.NewGuid();

        var result = _repositoryTest.Exist(nonExistingId);

        result.Should().BeFalse();
    }

    [TestMethod]
    public void Delete_WhenMethodExists_ShouldRemoveFromDatabase()
    {
        var id = Guid.NewGuid();
        var method = new Method { Id = id, Name = "DeleteMe", ReturnType = new Primitive() };
        _context.Methods.Add(method);
        _context.SaveChanges();

        _repositoryTest.Delete(id);

        var result = _context.Methods.Find(id);
        result.Should().BeNull();
    }
}
