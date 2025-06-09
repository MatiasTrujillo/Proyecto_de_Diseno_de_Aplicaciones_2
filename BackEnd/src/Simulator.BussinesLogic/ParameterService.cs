using System;
using System.Linq;
using Simulator.BussinesLogic.Exception;
using Simulator.Domain;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;
using Type = Simulator.Domain.Type;

namespace Simulator.BussinesLogic;

public class ParameterService(IParameterRepository parameterRepository, IClassRepository classRepository, IInvokedMethodRepository invokedMethodRepository) : IParameterService
{
    private readonly IParameterRepository _parameterRepository = parameterRepository;
    private readonly IClassRepository _classRepository = classRepository;
    private readonly IInvokedMethodRepository _invokedMethodRepository = invokedMethodRepository;

    public Guid CreateParameter(ParameterDTO parameter)
    {
        ValidateParameterIsNotNull(parameter);
        var parameterT = parameter.ToEntity();
        var type = parameterT.ReferenceType.Name;
        var returnType = _classRepository.FindAllTypes().FirstOrDefault(c => c.Name == type);
        parameterT.ReferenceType = returnType;
        parameterT.InstanceType = returnType;
        _parameterRepository.Add(parameterT);
        return parameterT.Id;
    }

    public void SetInvokeMethodToParameter(Guid id, Guid idInvokeGuid)
    {
        var parameter = _parameterRepository.Find(id);
        var invokedMethod = _invokedMethodRepository.Find(idInvokeGuid);
        ValidateParameterExist(parameter);
        ValidateInvokedMethodExist(invokedMethod);
        ValidateTypeIsPrimitive(parameter.ReferenceType);
        parameter.InvokedMethods.Add(invokedMethod);
        _parameterRepository.Update(parameter);
    }

    private void ValidateTypeIsPrimitive(Type? attributeTType)
    {
        if(attributeTType?.GetType() != typeof(Class) && attributeTType?.GetType() != typeof(Primitive))
        {
            throw new InvalidAttributeException("In v2 of this program only Class and Primitive types are accepted.");
        }
    }

    private void ValidateInvokedMethodExist(InvokedMethod invokedMethod)
    {
        if(invokedMethod == null)
        {
            throw new InvokedMethodNotFoundException("Invoked Method not found.");
        }
    }

    private void ValidateParameterExist(Parameter parameter)
    {
        if(parameter == null)
        {
            throw new ParameterNotFoundException("Parameter not found.");
        }
    }

    private void ValidateParameterIsNotNull(ParameterDTO parameter)
    {
        if(parameter == null)
        {
            throw new ParameterNotFoundException("Parameter cannot be null.");
        }
    }
}
