using Microsoft.EntityFrameworkCore;
using Simulator.Domain;
using Simulator.IDataAcces;
using Type = Simulator.Domain.Type;

namespace Simulator.DataAcces;

public class ParameterRepository() : IParameterRepository
{
    public void Add(Parameter parameter)
    {
        throw new NotImplementedException();
    }

    public Parameter Find(Guid id)
    {
        throw new NotImplementedException();
    }

    public List<Parameter> FindAll()
    {
        throw new NotImplementedException();
    }

    public bool Exist(Guid id)
    {
        throw new NotImplementedException();
    }
}
