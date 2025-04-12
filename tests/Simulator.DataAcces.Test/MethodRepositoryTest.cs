using FluentAssertions;
using Simulator.Domain;

namespace Simulator.DataAcces.Test;

[TestClass]
public class MethodRepositoryTest
{
    private readonly SqlContext _context = ProgramTest.BuildTestDbContext();
    private readonly MethodRepository _repositoryTest;

    public MethodRepositoryTest()
    {
        _repositoryTest = new MethodRepository(_context);
    }

    [TestMethod]
    public void Add_WhenValidMethodProvided_ShouldBeSavedInDatabase()
    {
        Method method = new Method(Guid.NewGuid(), "Method", Accessibility.Public, null);

        _repositoryTest.Add(method);

        using var otherContext = ProgramTest.BuildTestDbContext();
        var saved = otherContext.Methods.ToList();

        saved.Count.Should().Be(1);
        saved[0].Id.Should().Be(method.Id);
        saved[0].Name.Should().Be(method.Name);
        saved[0].Accessibility.Should().Be(method.Accessibility);
        saved[0].Stereotype.Should().BeNull();
        saved[0].Methods.Should().NotBeNull();
        saved[0].Methods.Should().BeEmpty();
        saved[0].Parameters.Should().NotBeNull();
        saved[0].Parameters.Should().BeEmpty();
        saved[0].LocalVariables.Should().NotBeNull();
        saved[0].LocalVariables.Should().BeEmpty();
    }
}
