using Simulator.Domain;
using Type = Simulator.Domain.Type;

namespace Simulator.DTO;

public class MethodDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Accessibility Accessibility { get; set; }
    public KeyWord? Stereotype { get; set; }
    private List<LocalVariable> LocalVariable { get; set; } = [];
    private List<Parameter> Parameters { get; set; } = [];
    private List<Method> Methods { get; set; } = [];
    public Type ReturnType { get; set; } = new Primitive();

    public Method ToEntity()
    {
        return new Method(Name, Accessibility, Stereotype, Methods, Parameters, LocalVariable, ReturnType);
    }
}
