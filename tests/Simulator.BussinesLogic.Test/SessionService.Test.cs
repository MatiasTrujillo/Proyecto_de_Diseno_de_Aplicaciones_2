using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Simulator.Domain;
using Simulator.DTO;
using Simulator.IDataAcces;

namespace Simulator.BussinesLogic.Test;

[TestClass]
public class SessionServiceTests
{
    private Mock<ISessionRepository> _mockSessionRepository = null!;
    private SessionService _sessionService = null!;
    private SessionDTO _sessionDto = null!;
    private Session _sessionEntity = null!;

    [TestInitialize]
    public void Initialize()
    {
        _mockSessionRepository = new Mock<ISessionRepository>(MockBehavior.Strict);
        _sessionService = new SessionService(_mockSessionRepository.Object);

        _sessionDto = new SessionDTO
        {
            Token = Guid.NewGuid()
        };

        _sessionEntity = new Session
        {
            Token = _sessionDto.Token,
            IsActive = false
        };
    }

    [TestMethod]
    public void SignIn_ShouldAddNewSession_AndReturnToken()
    {
        _mockSessionRepository.Setup(repo => repo.Find(_sessionDto.Token)).Returns((Session?)null);
        _mockSessionRepository.Setup(repo => repo.Add(It.IsAny<Session>()));

        var resultToken = _sessionService.SignIn(_sessionDto);

        Assert.AreEqual(_sessionDto.Token, resultToken);
        _mockSessionRepository.Verify(repo => repo.Add(It.Is<Session>(s => s.Token == _sessionDto.Token)), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void SignIn_ShouldThrow_WhenSessionAlreadyExists()
    {
        _mockSessionRepository.Setup(repo => repo.Find(_sessionDto.Token)).Returns(_sessionEntity);

        _sessionService.SignIn(_sessionDto);
    }

    [TestMethod]
    public void LogIn_ShouldActivateSession()
    {
        _mockSessionRepository.Setup(repo => repo.Find(_sessionEntity.Token)).Returns(_sessionEntity);
        _mockSessionRepository.Setup(repo => repo.Update(It.IsAny<Session>()));

        _sessionService.LogIn(_sessionEntity.Token);

        Assert.IsTrue(_sessionEntity.IsActive);
        _mockSessionRepository.Verify(repo => repo.Update(_sessionEntity), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(UnauthorizedAccessException))]
    public void LogIn_ShouldThrow_WhenSessionNotFound()
    {
        _mockSessionRepository.Setup(repo => repo.Find(_sessionEntity.Token)).Returns((Session?)null);

        _sessionService.LogIn(_sessionEntity.Token);
    }

    [TestMethod]
    public void LogOut_ShouldDeactivateSession()
    {
        _sessionEntity.IsActive = true;
        _mockSessionRepository.Setup(repo => repo.Find(_sessionEntity.Token)).Returns(_sessionEntity);
        _mockSessionRepository.Setup(repo => repo.Update(It.IsAny<Session>()));

        _sessionService.LogOut(_sessionEntity.Token);

        Assert.IsFalse(_sessionEntity.IsActive);
        _mockSessionRepository.Verify(repo => repo.Update(_sessionEntity), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(UnauthorizedAccessException))]
    public void LogOut_ShouldThrow_WhenSessionNotFound()
    {
        _mockSessionRepository.Setup(repo => repo.Find(_sessionEntity.Token)).Returns((Session?)null);

        _sessionService.LogOut(_sessionEntity.Token);
    }
}
