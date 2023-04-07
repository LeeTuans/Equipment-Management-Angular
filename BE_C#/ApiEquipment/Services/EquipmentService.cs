using ApiEquipment.Dto;
using ApiEquipment.Entities;
using ApiEquipment.Helpers;
using ApiEquipment.Interfaces;
using ApiEquipment.Services.Interfaces;

namespace ApiEquipment.Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EquipmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
       
        public async Task<IEnumerable<DtoOutEquipment>> GetAll()
        {
            var equipments = await _unitOfWork.GetRepo<IEquipmentRepo>().GetAll();
            
            List<DtoOutEquipment> dtoOutEquipments = new List<DtoOutEquipment>();
            foreach (Equipment r in equipments)
            {
                var dtoOutEquipment = new DtoOutEquipment();
                dtoOutEquipment.SetValueFromEntity(r);
                dtoOutEquipments.Add(dtoOutEquipment);
            }
            return dtoOutEquipments;
            
        }

        public async Task<DtoOutEquipment?> GetById(int id)
        {
 
            var equipment = await _unitOfWork.GetRepo<IEquipmentRepo>().GetById(id);

            if (equipment == null)
            {
                return null;
            }
            var dtoOutEquipment = new DtoOutEquipment();
            dtoOutEquipment.SetValueFromEntity(equipment);
            return dtoOutEquipment;
            
        }

        public async Task<bool?> Update(int id, DtoEquipment dtoEquipment)
        {
           

            var emp = await _unitOfWork.GetRepo<IEquipmentRepo>().GetById(id);
            if (emp == null) return null;
            emp.SetValueFromDto(dtoEquipment);

            _unitOfWork.GetRepo<IEquipmentRepo>().Update(emp);

            var result = _unitOfWork.Save();

            if (result > 0)
                return true;
            else
                return false;
        }
        public async Task<DtoOutEquipment?> Add(DtoEquipment dtoEquipment)
        {
            var addEquipment = new Equipment();
            addEquipment.SetValueFromDto(dtoEquipment);
            await _unitOfWork.GetRepo<IEquipmentRepo>().Add(addEquipment);
            var result = _unitOfWork.Save();

            if (result <= 0) return null;

            var newEquipment = await _unitOfWork.GetRepo<IEquipmentRepo>().GetById(addEquipment.EquipmentId);

            if (newEquipment != null)
            {
                var dtoOutEquipment = new DtoOutEquipment();
                dtoOutEquipment.SetValueFromEntity(newEquipment);
                return dtoOutEquipment;
            }
            return null;

            
        }

        public async Task<bool?> Delete(int id)
        {
            var equipment = await _unitOfWork.GetRepo<IEquipmentRepo>().GetById(id);
            if (equipment == null)
            {
                return null;
            }
            if(equipment.EquipmentHistories.Where(eh => eh.IsAproved == true && eh.ReturnedDate == null).FirstOrDefault() != null)
                return false;
            _unitOfWork.GetRepo<IEquipmentRepo>().Delete(equipment);
            var result = _unitOfWork.Save();
            if (result <= 0) return false;
            return true;
        }

    }
}
