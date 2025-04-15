using DTO;
using Simulator.Domain;
using Simulator.Domain.Exception;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;

namespace Simulator.BussinesLogic;

public class ClassService(IClassRepository classRepository) : IClassService
{
    private readonly IClassRepository _classRepository = classRepository;

    // private readonly IAttributeRepository _attributeRepository = attributeRepository;

    // private readonly IMethodRepository _methodRepository = methodRepository;

    public void CreateClass(ClassDTO classDto)
    {
        var aClass = classDto.ToEntity();
        ValidateClassNameIsUnique(aClass);
        _classRepository.Add(aClass);
    }

    public void UpdateClass(Guid id, ClassDTO classDto)
    {
        throw new NotImplementedException();
    }

    public void DeleteClass(Guid id)
    {
        throw new NotImplementedException();
    }

    private void ValidateClassNameIsUnique(Class aClass)
    {
        var classes = _classRepository.FindAll();

        if(classes.Any(classT => classT.Name == aClass.Name))
        {
            throw new ClassException("There is already a class with this name");
        }
    }
}
