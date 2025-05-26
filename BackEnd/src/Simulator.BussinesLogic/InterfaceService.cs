using System;
using System.Collections.Generic;
using System.Linq;
using Simulator.BussinesLogic.Exception;
using Simulator.Domain;
using Simulator.DTO;
using Simulator.IBussinesLogic;
using Simulator.IDataAcces;
using Type = Simulator.Domain.Type;

namespace Simulator.BussinesLogic;

public class InterfaceService(
    IInterfaceRepository interfaceRepository,
    IClassRepository classRepository, IMethodSignatureRepository methodSignatureRepository) : IInterfaceService
{
    private readonly IInterfaceRepository _interfaceRepository = interfaceRepository;
    private readonly IClassRepository _classRepository = classRepository;
    private readonly IMethodSignatureRepository _methodSignatureRepository = methodSignatureRepository;

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
        VerifyNameHasSpaces(name);
    }

    private static void VerifyNameHasSpaces(string name)
    {
        if(name.Contains(' '))
        {
            throw new InterfaceException("The interface name must not contain spaces.");
        }
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
        var interfaces = _interfaceRepository.FindAll();
        VerifyExistsInterface(id, interfaces);
        VerifyInterfaceInUse(id);
        _interfaceRepository.Delete(id);
    }

    public void UpdateInterface(Guid id, UpdateInterfaceDTO uInterfaceDto)
    {
        if(!_interfaceRepository.Exist(id))
        {
            throw new InterfaceNotFoundException("Interface not found");
        }

        var signatureToUpdate = _methodSignatureRepository.Find(uInterfaceDto.IdMethodSignature);
        if(signatureToUpdate == null)
        {
            throw new MethodSignatureNotFoundException("Method signature id not found.");
        }

        var interfaceToUpdate = _interfaceRepository.Find(id);
        var currentSignatures = interfaceToUpdate.MethodSignatures?.ToList() ?? [];

        if(!currentSignatures.Any(ms => ms.Id == signatureToUpdate.Id))
        {
            var updatedSignatures = new List<MethodSignature>(currentSignatures)
            {
                signatureToUpdate
            };

            interfaceToUpdate.MethodSignatures = updatedSignatures;
            _interfaceRepository.Update(interfaceToUpdate);
        }
    }

    private void VerifyInterfaceInUse(Guid id)
    {
        var classes = _classRepository.FindAll();
        if(classes.Any(c => c.Parents != null && c.Parents.Find(i => i.Id == id) != null))
        {
            throw new InterfaceInUseException("Interface is associated with a class.");
        }
    }

    private static void VerifyExistsInterface(Guid id, List<Interface> interfaces)
    {
        if(!interfaces.Exists(interfaceT => interfaceT.Id == id))
        {
            throw new InterfaceNotFoundException("There is no interface with this id.");
        }
    }
}
