namespace Simulator.Domain.Test;

[TestClass]
public sealed class AttributeTest
{
    [TestMethod]
    public void Create_WhenAttributeIsCreated_ShouldCreateAttribute()
    {
        var attribute = new Attribute(Guid.NewGuid(), "Name", Accessibility.Public);
        Assert.AreEqual(attribute.Name, "Name");
        Assert.AreEqual(attribute.Accessibility, Accessibility.Public);
        Assert.AreNotEqual(attribute.Id, Guid.Empty);
    }
}
