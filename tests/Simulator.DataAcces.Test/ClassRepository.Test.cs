using FluentAssertions;
using Simulator.Domain;
using Attribute = Simulator.Domain.Attribute;

namespace Simulator.DataAcces.Test;

[TestClass]
public class ClassRepositoryTest
{
    private readonly SqlContext _context = ProgramTest.BuildTestDbContext();

    private readonly ClassRepository _repository;

    public ClassRepositoryTest()
    {
        _repository = new ClassRepository(_context);
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
}
