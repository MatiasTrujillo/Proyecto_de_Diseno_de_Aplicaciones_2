using DTO;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;

namespace Simulator.BussinesLogic;

public class ClassService(IClassRepository classRepository) : IClassService
{
    private readonly IClassRepository _classRepository = classRepository;

    // private readonly IAttributeRepository _attributeRepository = attributeRepository;

    // private readonly IMethodRepository _mothodRepository = methodRepository;

    public void CreateClass(ClassDTO classDto)
    {
        throw new NotImplementedException();
    }

    public void UpdateClass(Guid id, ClassDTO classDto)
    {
        throw new NotImplementedException();
    }

    public void DeleteClass(Guid id)
    {
        throw new NotImplementedException();
    }
}
