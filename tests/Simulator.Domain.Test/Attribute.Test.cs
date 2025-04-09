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

    [TestMethod]
    public void Create_WhenUsingDefaultConstructor_ShouldInitializeWithDefaultValues()
    {
        var attribute = new Attribute();
        Assert.AreNotEqual(attribute.Id, Guid.Empty);
        Assert.AreEqual(attribute.Name, string.Empty);
        Assert.AreEqual(attribute.Accessibility, Accessibility.Public);
    }

    [TestMethod]
    public void SetProperties_WhenPropertiesAreSet_ShouldUpdateValues()
    {
        var attribute = new Attribute();
        attribute.Name = "Updated Name";
        attribute.Accessibility = Accessibility.Private;

        Assert.AreEqual(attribute.Name, "Updated Name");
        Assert.AreEqual(attribute.Accessibility, Accessibility.Private);
    }
}
