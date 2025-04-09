namespace Simulator.Domain;

public enum Accessibility
{
    /// <summary>
    /// Indicates that the attribute is publicly accessible.
    /// </summary>
    Public,

    /// <summary>
    /// Indicates that the attribute is privately accessible.
    /// </summary>
    Private,

    /// <summary>
    /// Indicates that the attribute is accessible only within its class or by derived class instances
    /// </summary>
    Protected,
}
