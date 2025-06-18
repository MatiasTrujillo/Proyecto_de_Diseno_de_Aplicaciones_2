using Simulator.Domain;

namespace Simulator.DTO;

public class MethodSignatureDTO
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public TypeDTO? ReturnType { get; set; } = null!;
    public ParameterDTO[] Parameters { get; set; } = [];

    public MethodSignature ToEntity()
    {
        var returnType = ReturnType?.ToEntity() ?? null;
        var parameters = Parameters.Select(p => p.ToEntity()).ToList();
        parameters = ConserveId(parameters);
        return new MethodSignature(Name, returnType, parameters);
    }

    private List<Parameter> ConserveId(List<Parameter> parameters)
    {
        for(var i = 0; i < parameters.Count; i++)
        {
            parameters[i].Id = Guid.Parse(Parameters[i].Id);
        }

        return parameters;
    }
}
