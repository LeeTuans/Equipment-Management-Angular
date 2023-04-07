using ApiEquipment.Dto;
using ApiEquipment.Entities;
using ApiEquipment.Helpers;
using ApiEquipment.Interfaces;
using ApiEquipment.Services.Interfaces;

namespace ApiEquipment.Services
{
    public class EquipmentTypeService : IEquipmentTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EquipmentTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<DtoOutEquipmentType>> GetAll()
        {
            var equipmentTypes = await _unitOfWork.GetRepo<IEquipmentTypeRepo>().GetAll();
            
            List<DtoOutEquipmentType> DtoOutEquipmentTypes = new List<DtoOutEquipmentType>();
            foreach (var r in equipmentTypes)
            {
                var DtoOutEquipmentType = new DtoOutEquipmentType();
                DtoOutEquipmentType.SetValueFromEntity(r);
                DtoOutEquipmentTypes.Add(DtoOutEquipmentType);
            }
            return DtoOutEquipmentTypes;
            
        }

        public async Task<DtoOutEquipmentType?> GetById(int id)
        {
 
            var equipmentType = await _unitOfWork.GetRepo<IEquipmentTypeRepo>().GetById(id);

            if (equipmentType == null)
            {
                return null;
            }
            var DtoOutEquipmentType = new DtoOutEquipmentType();
            DtoOutEquipmentType.SetValueFromEntity(equipmentType);
            return DtoOutEquipmentType;
        }

        public async Task<bool?> Update(int id, DtoEquipmentType dtoEquipmentType)
        {
           

            var emp = await _unitOfWork.GetRepo<IEquipmentTypeRepo>().GetById(id);
            if (emp == null) return null;
            emp.SetValueFromDto(dtoEquipmentType);

            _unitOfWork.GetRepo<IEquipmentTypeRepo>().Update(emp);

            var result = _unitOfWork.Save();

            if (result > 0)
                return true;
            else
                return false;
        }
        public async Task<DtoOutEquipmentType?> Add(DtoEquipmentType dtoEquipmentType)
        {
            var addEquipmentType = new EquipmentType();
            addEquipmentType.SetValueFromDto(dtoEquipmentType);
            await _unitOfWork.GetRepo<IEquipmentTypeRepo>().Add(addEquipmentType);
            var result = _unitOfWork.Save();

            if (result <= 0) return null;

            var newEquipmentType = await _unitOfWork.GetRepo<IEquipmentTypeRepo>().GetById(addEquipmentType.EquipmentTypeId);

            if (newEquipmentType != null)
            {
                var DtoOutEquipmentType = new DtoOutEquipmentType();
                DtoOutEquipmentType.SetValueFromEntity(newEquipmentType);
                return DtoOutEquipmentType;
            }
            return null;
        }
        
        public async Task<bool?> Delete(int id)
        {
            
            var equipmentType = await _unitOfWork.GetRepo<IEquipmentTypeRepo>().GetById(id);
            if (equipmentType == null)
            {
                return null;
            }
            _unitOfWork.GetRepo<IEquipmentTypeRepo>().Delete(equipmentType);
            var result = _unitOfWork.Save();
            if (result <= 0) return false;
            return true;
        }

        
    }
    
}
