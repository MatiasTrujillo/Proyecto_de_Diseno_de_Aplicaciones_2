using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simulator.DataAcces;
using Simulator.DataAcces.Test;
using Simulator.Domain;
using System;
using System.Linq;

namespace Simulator.DataAcces.Test;

[TestClass]
public class SessionRepositoryTest
{
    private readonly SqlContext _context = ProgramTest.BuildTestDbContext();
    private readonly SessionRepository _repository;

    public SessionRepositoryTest()
    {
        _repository = new SessionRepository(_context);
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
    public void Add_WhenValidSessionProvided_ShouldBeSavedInDatabase()
    {
        var session = new Session();

        _repository.Add(session);

        using var refreshedContext = ProgramTest.BuildTestDbContext();
        var saved = refreshedContext.Sessions.FirstOrDefault(s => s.Token == session.Token);

        saved.Should().NotBeNull();
        saved!.Token.Should().Be(session.Token);
    }
    #endregion

    #region Find
    [TestMethod]
    public void Find_WhenSessionExists_ShouldReturnIt()
    {
        var session = new Session();

        _context.Sessions.Add(session);
        _context.SaveChanges();

        var result = _repository.Find(session.Token);

        result.Should().NotBeNull();
        result!.Token.Should().Be(session.Token);
    }

    [TestMethod]
    public void Find_WhenSessionDoesNotExist_ShouldReturnNull()
    {
        var result = _repository.Find(Guid.NewGuid());

        result.Should().BeNull();
    }
    #endregion

    #region FindAll
    [TestMethod]
    public void FindAll_WhenMultipleSessionsExist_ShouldReturnAll()
    {
        var session1 = new Session();
        var session2 = new Session();

        _context.Sessions.AddRange(session1, session2);
        _context.SaveChanges();

        var result = _repository.FindAll();

        result.Should().HaveCount(11);
        result.Should().Contain(s => s.Token == session1.Token);
        result.Should().Contain(s => s.Token == session2.Token);
    }
    #endregion

    #region Delete
    [TestMethod]
    public void Delete_WhenSessionExists_ShouldRemoveItFromDatabase()
    {
        var session = new Session();

        _context.Sessions.Add(session);
        _context.SaveChanges();

        _repository.Delete(session.Token);

        using var refreshedContext = ProgramTest.BuildTestDbContext();
        var deleted = refreshedContext.Sessions.FirstOrDefault(s => s.Token == session.Token);

        deleted.Should().BeNull();
    }

    [TestMethod]
    public void Delete_WhenSessionDoesNotExist_ShouldDoNothing()
    {
        Action act = () => _repository.Delete(Guid.NewGuid());

        act.Should().NotThrow();
    }
    #endregion

    #region Update

    [TestMethod]
    public void Update_WhenSessionExists_ShouldUpdateValues()
    {
        var originalSession = new Session
        {
            Token = Guid.NewGuid(),
            IsActive = false
        };

        _context.Sessions.Add(originalSession);
        _context.SaveChanges();

        var updatedSession = new Session
        {
            Token = originalSession.Token,
            IsActive = true,
        };

        _repository.Update(updatedSession);

        using var refreshedContext = ProgramTest.BuildTestDbContext();
        var sessionFromDb = refreshedContext.Sessions.FirstOrDefault(s => s.Token == originalSession.Token);

        sessionFromDb.Should().NotBeNull();
        sessionFromDb!.IsActive.Should().Be(true);
    }

    [TestMethod]
    public void Update_WhenSessionDoesNotExist_ShouldDoNothing()
    {
        var nonExistentSession = new Session
        {
            Token = Guid.NewGuid(),
            IsActive = true
        };

        Action act = () => _repository.Update(nonExistentSession);

        act.Should().NotThrow();

        var sessionFromDb = _context.Sessions.Find(nonExistentSession.Token);
        sessionFromDb.Should().BeNull();
    }

    #endregion
}
