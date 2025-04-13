using Simulator.BussinesLogic.DTO;

namespace Simulator.IBussinesLogic;

public interface IClassService
{
   void CreateClass(ClassDTO classDto);
   void UpdateClass(Guid id, ClassDTO classDto);
}
