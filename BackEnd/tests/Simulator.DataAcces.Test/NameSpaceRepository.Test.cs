using FluentAssertions;
using Simulator.Domain;

namespace Simulator.DataAcces.Test;

[TestClass]
public class NameSpaceRepository_Test
{
    private readonly SqlContext _context = ProgramTest.BuildTestDbContext();
    private readonly NameSpaceRepository _repository;
    private readonly NameSpace aNameSpace;
    private readonly NameSpace anotherNameSpace;

    public NameSpaceRepository_Test()
    {
        _repository = new NameSpaceRepository(_context);

        aNameSpace = new NameSpace
        {
            Id = Guid.NewGuid(),
            Name = "FirstNS"
        };

        anotherNameSpace = new NameSpace
        {
            Id = Guid.NewGuid(),
            Name = "SecondNS"
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
    public void Add_WhenGivenValidNamespace_ShouldBeSaved()
    {
        _repository.Add(aNameSpace);

        using var freshContext = ProgramTest.BuildTestDbContext();
        var saved = freshContext.NameSpaces.ToList();

        saved.Should().HaveCount(2);
        saved[1].Id.Should().Be(aNameSpace.Id);
        saved[1].Name.Should().Be(aNameSpace.Name);
    }
    #endregion

    #region Find
    [TestMethod]
    public void Find_WhenNamespaceExists_ShouldReturnIt()
    {
        _context.NameSpaces.Add(aNameSpace);
        _context.SaveChanges();

        var result = _repository.Find(aNameSpace.Id);

        result.Should().NotBeNull();
        result!.Id.Should().Be(aNameSpace.Id);
        result.Name.Should().Be(aNameSpace.Name);
    }

    [TestMethod]
    public void Find_WhenNamespaceDoesNotExist_ShouldReturnNull()
    {
        var result = _repository.Find(Guid.NewGuid());

        result.Should().BeNull();
    }
    #endregion

    #region FindAll
    [TestMethod]
    public void FindAll_WhenNamespacesExist_ShouldReturnAll()
    {
        using var context = ProgramTest.BuildTestDbContext();
        var repository = new NameSpaceRepository(context);

        var ns1 = new NameSpace { Id = Guid.NewGuid(), Name = "FirstNS" };
        var ns2 = new NameSpace { Id = Guid.NewGuid(), Name = "SecondNS" };

        context.NameSpaces.AddRange(ns1, ns2);
        context.SaveChanges();

        var results = repository.FindAll();

        results.Should().NotBeNull();
        results.Should().HaveCount(3);
        results.Should().ContainSingle(n => n.Id == ns1.Id && n.Name == ns1.Name);
        results.Should().ContainSingle(n => n.Id == ns2.Id && n.Name == ns2.Name);
    }
    #endregion

    #region Update
    [TestMethod]
    public void Update_WhenNamespaceExists_ShouldUpdateItsValues()
    {
        _context.NameSpaces.Add(aNameSpace);
        _context.SaveChanges();

        var updatedNamespace = new NameSpace
        {
            Id = aNameSpace.Id,
            Name = "UpdatedNS"
        };

        _repository.Update(updatedNamespace);

        using var freshContext = ProgramTest.BuildTestDbContext();
        var result = freshContext.NameSpaces.FirstOrDefault(n => n.Id == aNameSpace.Id);

        result.Should().NotBeNull();
        result!.Name.Should().Be("UpdatedNS");
    }
    #endregion

    #region Delete
    [TestMethod]
    public void Delete_WhenNamespaceExists_ShouldRemoveIt()
    {
        _context.NameSpaces.Add(aNameSpace);
        _context.SaveChanges();

        _repository.Delete(aNameSpace);
        _context.SaveChanges();

        using var freshContext = ProgramTest.BuildTestDbContext();
        var result = freshContext.NameSpaces.FirstOrDefault(n => n.Id == aNameSpace.Id);
        result.Should().BeNull();
    }
    #endregion

    [TestMethod]
    public void FindAllNamespaceComponents_WhenComponentsExist_ShouldReturnAll()
    {
        var ns1 = new NameSpace { Id = Guid.NewGuid(), Name = "NS1" };
        var ns2 = new NameSpace { Id = Guid.NewGuid(), Name = "NS2" };
        _context.NamespaceComponents.Add(ns1);
        _context.NamespaceComponents.Add(ns2);
        _context.SaveChanges();

        var result = _repository.FindAllNamespaceComponents();

        result.Should().NotBeNull();
        result.Should().HaveCount(20);
        result.Should().ContainSingle(n => n.Id == ns1.Id);
        result.Should().ContainSingle(n => n.Id == ns2.Id);
    }
}
