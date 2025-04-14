using DTO;
using Moq;
using Simulator.Domain;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;

namespace Simulator.BussinesLogic.Test;

[TestClass]
public class MethodServiceTest
{
    private Mock<IMethodRepository> _mockMethodRepository = null!;
    private MethodService _methodService = null!;

    [TestInitialize]
    public void Initialize()
    {
        _mockMethodRepository = new Mock<IMethodRepository>(MockBehavior.Strict);
        _mockMethodRepository
            .Setup(repo => repo.Add(It.IsAny<Method>()));
        _methodService = new MethodService(_mockMethodRepository.Object);
    }

    [TestMethod]
    public void CreateMethod_ShouldCallRepositoryAdd_WithValidMethod()
    {
        MethodDTO methodDto = new MethodDTO()
        {
            Id = Guid.NewGuid(),
            Name = "First Method Dto",
            Accessibility = Accessibility.Private,
            Stereotype = null
        };

        _methodService.CreateMethod(methodDto);

        _mockMethodRepository.Verify(repo => repo.Add(
            It.Is<Method>(m =>
                m.Id == methodDto.Id &&
                m.Name == methodDto.Name &&
                m.Accessibility == methodDto.Accessibility &&
                m.Stereotype == null)), Times.Once);
    }
}
