using Simulator.Domain;
using Simulator.Domain.Exception;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;
using Attribute = Simulator.Domain.Attribute;

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
        ValidateClassNameIsUnique(classDto.ToEntity());
        ValidateClassIdExists(id);
        var classT = _classRepository.Find(id);
        classT.Name = classDto.Name;
        classT.Stereotype = classDto.Stereotype;

        if(classT.HierarchyClass != null)
        {
            classT.HierarchyClass = classDto.HirarchyClass.ToEntity();
        }
        else
        {
            classT.HierarchyClass = null;
        }

        classT.Attributes = ConvertAttributesToEntity(classDto.Attributes);
        _classRepository.Update(classT);
    }

    public void DeleteClass(Guid id)
    {
        ValidateClassIdExists(id);
        _classRepository.Delete(id);
    }

    private void ValidateClassNameIsUnique(Class aClass)
    {
        var classes = _classRepository.FindAll();

        if(classes.Any(classT => classT.Name == aClass.Name && classT.Id != aClass.Id))
        {
            throw new ClassException("There is already a class with this name");
        }
    }

    private void ValidateClassIdExists(Guid id)
    {
        var classes = _classRepository.FindAll();

        if(!classes.Exists(classT => classT.Id == id))
        {
            throw new ClassException("There is no class with this id");
        }
    }

    private List<Attribute> ConvertAttributesToEntity(List<AttributeDto> attributeDtos)
    {
        List<Attribute> attributes = new List<Attribute>();
        foreach(var attDto in attributeDtos)
        {
            attributes.Add(attDto.ToEntity());
        }

        return attributes;
    }
}
