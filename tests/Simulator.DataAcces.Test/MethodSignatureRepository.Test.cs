using FluentAssertions;
using Simulator.Domain;

namespace Simulator.DataAcces.Test;

[TestClass]
public class MethodSignatureRepository_Test
{
    private readonly SqlContext _context = ProgramTest.BuildTestDbContext();
    private readonly MethodSignatureRepository _repository;

    public MethodSignatureRepository_Test()
    {
        _repository = new MethodSignatureRepository();
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

    [TestMethod]
    public void Add_WhenGivenValidMethodSignatureProvided_ShouldAddItToRepository()
    {
        var type = new Primitive();
        var aSignature = new MethodSignature("ASignature", type, []);
        _repository.Add(aSignature);

        using var otherContext = ProgramTest.BuildTestDbContext();
        var saved = otherContext.Set<MethodSignature>().ToList();

        saved.Count.Should().Be(1);
        saved[0].Id.Should().Be(aSignature.Id);
        saved[0].Name.Should().Be(aSignature.Name);
    }
}
