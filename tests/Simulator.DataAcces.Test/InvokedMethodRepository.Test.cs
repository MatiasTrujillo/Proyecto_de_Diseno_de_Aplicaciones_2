using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Simulator.Domain;

namespace Simulator.DataAcces.Test;

[TestClass]
public class InvokedMethodRepository_Test
{
 private readonly SqlContext _context = ProgramTest.BuildTestDbContext();
    private readonly InvokedMethodRepository _repository;
    private readonly InvokedMethod _invokedMethod;

    public InvokedMethodRepository_Test()
    {
        _repository = new InvokedMethodRepository(_context);

        _invokedMethod = new InvokedMethod
        {
            Id = Guid.NewGuid(),
            Method = new Method
            {
                Id = Guid.NewGuid(),
                Name = "TestMethod",
                ReturnType = new Primitive(PrimitiveType.Int),
                Parameters = new List<Parameter>()
            },
            ReferenceName = "TestReference",
        };
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
    public void Add_WhenValidInvokedMethodProvided_ShouldBeSavedInDatabase()
    {
        var method = new Method
        {
            Id = Guid.NewGuid(),
            Name = "TestMethod",
            ReturnType = new Primitive(PrimitiveType.Int),
            Parameters = new List<Parameter>()
        };

        _context.Methods.Add(method);
        _context.SaveChanges();

        var invokedMethod = new InvokedMethod
        {
            Id = Guid.NewGuid(),
            MethodId = method.Id,
            Method = method,
            ReferenceName = "TestReference"
        };

        _repository.Add(invokedMethod);

        using var refreshedContext = ProgramTest.BuildTestDbContext();
        var saved = refreshedContext.InvokedMethods
            .Include(i => i.Method)
            .FirstOrDefault(i => i.Id == invokedMethod.Id);

        saved.Should().NotBeNull();
        saved!.ReferenceName.Should().Be("TestReference");
        saved.Method.Should().NotBeNull();
        saved.Method.Name.Should().Be("TestMethod");
    }
    #endregion

    #region Find
    [TestMethod]
    public void Find_WhenInvokedMethodExists_ShouldReturnItWithMethod()
    {
        var method = new Method
        {
            Id = Guid.NewGuid(),
            Name = "ExistingMethod",
            ReturnType = new Primitive(PrimitiveType.Int),
            Parameters = new List<Parameter>()
        };

        var invokedMethod = new InvokedMethod
        {
            Id = Guid.NewGuid(),
            MethodId = method.Id,
            Method = method,
            ReferenceName = "RefMethod"
        };

        _context.Methods.Add(method);
        _context.InvokedMethods.Add(invokedMethod);
        _context.SaveChanges();

        var result = _repository.Find(invokedMethod.Id);

        result.Should().NotBeNull();
        result!.Id.Should().Be(invokedMethod.Id);
        result.ReferenceName.Should().Be("RefMethod");
        result.Method.Should().NotBeNull();
        result.Method.Id.Should().Be(method.Id);
        result.Method.Name.Should().Be("ExistingMethod");
    }

    [TestMethod]
    public void Find_WhenInvokedMethodDoesNotExist_ShouldReturnNull()
    {
        var result = _repository.Find(Guid.NewGuid());

        result.Should().BeNull();
    }

    #endregion
}
