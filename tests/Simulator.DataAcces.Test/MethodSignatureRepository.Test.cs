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

    public MethodSignatureRepository_Test()
    {
        _repository = new MethodSignatureRepository(_context);
        type = new Primitive();
        aSignature = new MethodSignature("ASignature", type, []);
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
        _context.Primitives.Add(type);
        _context.SaveChanges();
        _repository.Add(aSignature);

        using var otherContext = ProgramTest.BuildTestDbContext();
        var saved = otherContext.Set<MethodSignature>().ToList();

        saved.Count.Should().Be(1);
        saved[0].Id.Should().Be(aSignature.Id);
        saved[0].Name.Should().Be(aSignature.Name);
    }
    #endregion

    [TestMethod]
    public void Find_WhenMethodSignatureExists_ShouldReturnIt()
    {
        _context.Primitives.Add(type);
        _context.SaveChanges();
        _repository.Add(aSignature);

        var result = _repository.Find(aSignature.Id);

        result.Should().NotBeNull();
        result!.Id.Should().Be(aSignature.Id);
        result.Name.Should().Be(aSignature.Name);
        result.ReturnType.Should().NotBeNull();
    }
}
