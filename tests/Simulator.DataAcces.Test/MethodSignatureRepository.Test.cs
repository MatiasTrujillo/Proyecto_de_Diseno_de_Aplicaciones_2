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
        _context.Primitives.Add(type);
        _context.SaveChanges();
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
            InvokedMethod = null
        };

        secondSignature = new MethodSignature
        {
            Id = Guid.NewGuid(),
            Name = "myOtherSignature",
            ReturnType = secondPrimitive,
            Parameters = [aParameter]
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

    [TestMethod]
    public void FindAll_WhenMethodSignaturesExist_ShouldReturnAll()
    {
        _context.Primitives.Add(secondPrimitive);
        _context.Parameters.Add(aParameter);
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
}
