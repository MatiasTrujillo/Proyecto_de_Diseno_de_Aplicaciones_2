using DTO;
using Moq;
using Simulator.Domain;
using Simulator.IDataAcces;

namespace Simulator.BussinesLogic.Test;

[TestClass]
public class MethodServiceTest
{
    private Mock<IMethodRepository> _mockMethodRepository = null!;
    private MethodService _methodService = null!;
    private MethodDTO methodDto = new MethodDTO();

    [TestInitialize]
    public void Initialize()
    {
        methodDto = new MethodDTO()
        {
            Id = Guid.NewGuid(),
            Name = "First Method Dto",
            Accessibility = Accessibility.Private,
            Stereotype = null
        };
        _mockMethodRepository = new Mock<IMethodRepository>(MockBehavior.Strict);
        _mockMethodRepository
            .Setup(repo => repo.Add(It.IsAny<Method>()));
        _methodService = new MethodService(_mockMethodRepository.Object);
        _mockMethodRepository
            .Setup(repo => repo.Delete(It.IsAny<Guid>()));
    }

    #region CreateMethod
    [TestMethod]
    public void CreateMethod_ShouldCallRepositoryAdd_WithValidMethod()
    {
        _methodService.CreateMethod(methodDto);

        _mockMethodRepository.Verify(repo => repo.Add(
            It.Is<Method>(m =>
                m.Id == methodDto.Id &&
                m.Name.Equals(methodDto.Name) &&
                m.Accessibility == methodDto.Accessibility &&
                m.Stereotype == null)), Times.Once);
    }
    #endregion

    #region DeleteMethod
    [TestMethod]
    public void DeleteMethod_ShouldCallRepositoryDelete_WithValidMethod()
    {
        _methodService.CreateMethod(methodDto);
        _methodService.DeleteMethod(methodDto.Id);

        _mockMethodRepository.Verify(repo => repo.Delete(methodDto.Id), Times.Once);
    }
    #endregion
}
