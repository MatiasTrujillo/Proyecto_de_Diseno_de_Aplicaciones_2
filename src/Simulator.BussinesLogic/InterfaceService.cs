using System;
using System.Collections.Generic;
using Simulator.BussinesLogic.Exception;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;
using Type = Simulator.Domain.Type;

namespace Simulator.BussinesLogic;

public class InterfaceService(
    IInterfaceRepository interfaceRepository,
    IClassRepository classRepository) : IInterfaceService
{
    private readonly IInterfaceRepository _interfaceRepository = interfaceRepository;
    private readonly IClassRepository _classRepository = classRepository;

    public Guid CreateInterface(InterfaceDTO interfaceDto)
    {
        var anInterface = interfaceDto.ToEntity();
        var types = _classRepository.FindAllTypes();
        VerifyName(interfaceDto);
        VerifyInterfaceNameAlreadyExists(interfaceDto, types);
        _interfaceRepository.Add(anInterface);
        return anInterface.Id;
    }

    private static void VerifyName(InterfaceDTO interfaceDto)
    {
        var name = interfaceDto.Name;
        VerifyNameIsNotEmpty(name);
        VerifyNameStartsWithUpperCase(name);
    }

    private static void VerifyNameIsNotEmpty(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            throw new InterfaceException("The interface name cannot be empty.");
        }
    }

    private static void VerifyNameStartsWithUpperCase(string name)
    {
        if(!char.IsUpper(name[0]))
        {
            throw new InterfaceException("The interface name must start with an uppercase letter.");
        }
    }

    private static void VerifyInterfaceNameAlreadyExists(InterfaceDTO interfaceDto, List<Type> types)
    {
        foreach(var type in types)
        {
            if(type.Name == interfaceDto.Name)
            {
                throw new InvalidInterfaceException("There is already a type with this name");
            }
        }
    }

    public void DeleteInterface(Guid id)
    {
        throw new NotImplementedException();
    }
}
