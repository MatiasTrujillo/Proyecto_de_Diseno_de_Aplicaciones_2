using FluentAssertions;
using Simulator.Domain;

namespace Simulator.DataAcces.Test;

[TestClass]
public class MethodSignatureRepository_Test
{
    private readonly SqlContext _context = ProgramTest.BuildTestDbContext();
    private readonly MethodSignatureRepository _repository;
    private readonly MethodSignature aSignature;
    private readonly Primitive type;
    private readonly Primitive secondPrimitive;
    private readonly Parameter aParameter;
    private readonly MethodSignature secondSignature;

    public MethodSignatureRepository_Test()
    {
        _repository = new MethodSignatureRepository(_context);
        type = new Primitive();
        aSignature = new MethodSignature("ASignature", type, []);
        secondPrimitive = new Primitive
        {
            Id = Guid.NewGuid(),
            Name = "string"
        };

        aParameter = new Parameter
        {
            Id = Guid.NewGuid(),
            Name = "aParameter",
            ParameterType = secondPrimitive,
            InvokedMethods = null
        };

        secondSignature = new MethodSignature
        {
            Id = Guid.NewGuid(),
            Name = "myOtherSignature",
            ReturnType = secondPrimitive,
            Parameters = [aParameter]
        };
        _context.Primitives.Add(type);
        _context.Primitives.Add(secondPrimitive);
        _context.Parameters.Add(aParameter);
        _context.SaveChanges();
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
    public void Add_WhenGivenValidMethodSignatureProvided_ShouldAddItToRepository()
    {
        _repository.Add(aSignature);

        using var otherContext = ProgramTest.BuildTestDbContext();
        var saved = otherContext.Set<MethodSignature>().ToList();

        saved.Count.Should().Be(1);
        saved[0].Id.Should().Be(aSignature.Id);
        saved[0].Name.Should().Be(aSignature.Name);
    }
    #endregion

    #region Find
    [TestMethod]
    public void Find_WhenMethodSignatureExists_ShouldReturnIt()
    {
        _repository.Add(aSignature);

        var result = _repository.Find(aSignature.Id);

        result.Should().NotBeNull();
        result!.Id.Should().Be(aSignature.Id);
        result.Name.Should().Be(aSignature.Name);
        result.ReturnType.Should().NotBeNull();
    }

    [TestMethod]
    public void Find_WhenLocalVariableDoesNotExist_ShouldReturnNull()
    {
        var result = _repository.Find(Guid.NewGuid());

        result.Should().BeNull();
    }
    #endregion

    #region FindAll
    [TestMethod]
    public void FindAll_WhenMethodSignaturesExist_ShouldReturnAll()
    {
        _context.MethodSignatures.AddRange(aSignature, secondSignature);
        _context.SaveChanges();

        var results = _repository.FindAll();

        results.Should().NotBeNull();
        results.Should().HaveCount(2);

        results.Should().ContainSingle(s => s.Id == aSignature.Id &&
                                            s.Name == aSignature.Name);
        results.Should().ContainSingle(s => s.Id == secondSignature.Id &&
                                            s.Name == secondSignature.Name);
    }
    #endregion

    #region Update
    [TestMethod]
    public void Update_WhenMethodSignatureExists_ShouldOnlyAddAParameter()
    {
        var updatedSignature = new MethodSignature
        {
            Id = aSignature.Id,
            Name = "changedName",
            ReturnType = type,
            Parameters = [aParameter]
        };

        _repository.Add(aSignature);
        _repository.Update(updatedSignature);

        using var refreshedContext = ProgramTest.BuildTestDbContext();
        var updatedInDb = refreshedContext.MethodSignatures.FirstOrDefault(s => s.Id == aSignature.Id);

        updatedInDb.Should().NotBeNull();
        updatedInDb!.Name.Should().Be(aSignature.Name);
        updatedInDb!.Parameters.Should().HaveCount(1);
        updatedInDb.Parameters[0].Id.Should().Be(aParameter.Id);
    }

    [TestMethod]
    public void Update_WhenMethodSignatureDoesNotExist_ShouldNotThrowOrChangeAnything()
    {
        var nonExistentSignature = new MethodSignature("NonExistent", null, []);

        Action act = () => new MethodSignatureRepository(_context).Update(nonExistentSignature);

        act.Should().NotThrow();
        _context.MethodSignatures.Any(ms => ms.Id == nonExistentSignature.Id).Should().BeFalse();
    }
    #endregion
}
