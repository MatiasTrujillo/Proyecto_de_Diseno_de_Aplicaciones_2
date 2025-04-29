using Simulator.Domain;

namespace Simulator.IDataAcces;

public interface IParameterRepository
{
    void Add(Parameter method);
    Parameter Find(Guid id);
    List<Parameter> FindAll();
    bool Exist(Guid id);
}
