using Attribute = Simulator.Domain.Attribute;
namespace Simulator.IDataAcces;

public interface IAttributeRepository
{
    void Add(Attribute attribute);
    void Update(Attribute attribute);
    Attribute Find(Guid id);
    List<Attribute> FindAll();
    bool Exist(Guid id);
    void Delete(Guid id);
}
