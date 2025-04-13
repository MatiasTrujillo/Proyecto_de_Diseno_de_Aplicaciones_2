using Moq;
using Simulator.BussinesLogic.DTO;
using Simulator.Domain;
using Simulator.Domain.Exception;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;
using Attribute = Simulator.Domain.Attribute;

namespace Simulator.BussinesLogic.Test;

[TestClass]
public class AttributeServiceTest
{
    private Mock<IAttributeRepository> _mockAttributeRepository = null!;
    private Mock<IClassRepository> _mockClassRepository = null!;
    private AttributeService _attributeService = null!;
    private Guid _classId;

    [TestInitialize]
    public void Initialize()
    {
        _mockAttributeRepository = new Mock<IAttributeRepository>();
        _mockClassRepository = new Mock<IClassRepository>();
        _attributeService = new AttributeService(_mockAttributeRepository.Object, _mockClassRepository.Object);
        _classId = Guid.NewGuid();
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

        var classEntity = new Class
        {
            Id = _classId,
            Name = "TestClass",
            Attributes = new List<Attribute>()
        };

        _mockClassRepository
            .Setup(repo => repo.Find(_classId))
            .Returns(classEntity);

        _attributeService.CreateAttributeForClass(_classId, dto);

        _mockAttributeRepository.Verify(repo => repo.Add(
            It.Is<Attribute>(a =>
                a.Id == dto.Id &&
                a.Name == dto.Name &&
                a.Accessibility == dto.Accessibility)), Times.Once);

        Assert.AreEqual(1, classEntity.Attributes.Count);
        Assert.AreEqual(dto.Name, classEntity.Attributes[0].Name);
    }

    [TestMethod]
    public void CreateAttribute_ShouldThrowException_WhenAttributeNameAlreadyExistsInSameClass()
    {
        var dto = new AttributeDTO
        {
            Id = Guid.NewGuid(),
            Name = "Strength",
            Accessibility = Accessibility.Public
        };

        var classEntity = new Class
        {
            Id = _classId,
            Name = "Warrior",
            Attributes = new List<Attribute>
            {
                new Attribute(Guid.NewGuid(), "Strength", Accessibility.Private)
            }
        };

        _mockClassRepository
            .Setup(repo => repo.Find(_classId))
            .Returns(classEntity);

        var exception = Assert.ThrowsException<AttributeException>(() =>
        {
            _attributeService.CreateAttributeForClass(_classId, dto);
        });

        Assert.AreEqual("An attribute with the name 'Strength' already exists in the class", exception.Message);
    }
}
