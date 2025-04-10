using Moq;
using Simulator.BussinesLogic.DTO;
using Simulator.Domain;
using Simulator.IDataAcces;
using Attribute = Simulator.Domain.Attribute;

namespace Simulator.BussinesLogic.Test;

[TestClass]
public class AttributeServiceTest
{
    private Mock<IAttributeRepository> _mockAttributeRepository = null!;
    private AttributeService _attributeService = null!;

    [TestInitialize]
    public void Initialize()
    {
        _mockAttributeRepository = new Mock<IAttributeRepository>();
        _attributeService = new AttributeService(_mockAttributeRepository.Object);
    }

    [TestMethod]
    public void CreateAttribute_ShouldCallRepositoryAdd_WithCorrectEntity()
    {
        var dto = new AttributeDTO
        {
            Id = Guid.NewGuid(),
            Name = "Speed",
            Accessibility = Accessibility.Public
        };

        _attributeService.CreateAttribute(dto);

        _mockAttributeRepository.Verify(repo => repo.Add(
            It.Is<Attribute>(a =>
                a.Id == dto.Id &&
                a.Name == dto.Name &&
                a.Accessibility == dto.Accessibility)), Times.Once);
    }
}
