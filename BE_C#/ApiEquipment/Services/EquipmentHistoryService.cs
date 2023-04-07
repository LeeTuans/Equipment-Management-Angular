using ApiEquipment.Dto;
using ApiEquipment.Entities;
using ApiEquipment.Helpers;
using ApiEquipment.Interfaces;
using ApiEquipment.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiEquipment.Services
{
    public class EquipmentHistoryService : IEquipmentHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EquipmentHistoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
 

        public async Task<IEnumerable<DtoOutEquipmentHistory>> GetAll()
        {
            var equipmentHistories = await _unitOfWork.GetRepo<IEquipmentHistoryRepo>().GetAll();
            
            List<DtoOutEquipmentHistory> dtoOutEquipmentHistories = new List<DtoOutEquipmentHistory>();
            foreach (EquipmentHistory e in equipmentHistories)
            {
                var dtoOutEquipmentHistory = new DtoOutEquipmentHistory();
                dtoOutEquipmentHistory.SetValueFromEntity(e);
                dtoOutEquipmentHistories.Add(dtoOutEquipmentHistory);
            }
            return dtoOutEquipmentHistories;
           
        }


        public async Task<DtoOutEquipmentHistory?> GetById(int id)
        {
            
            var equipmentHistory = await _unitOfWork.GetRepo<IEquipmentHistoryRepo>().GetById(id);

            if (equipmentHistory == null)
            {
                return null;
                
            }
            var dtoOutEquipmentHistory = new DtoOutEquipmentHistory();
            dtoOutEquipmentHistory.SetValueFromEntity(equipmentHistory);
            return dtoOutEquipmentHistory;
        }
        public async Task<IEnumerable<DtoOutEquipmentHistory>> GetByEmployeeId(int id)
        {

            var equipmentHistories = await _unitOfWork.GetRepo<IEquipmentHistoryRepo>().GetByEmployeeId(id);
            List<DtoOutEquipmentHistory> dtoOutEquipmentHistories = new List<DtoOutEquipmentHistory>();
            foreach (EquipmentHistory e in equipmentHistories)
            {
                var dtoOutEquipmentHistory = new DtoOutEquipmentHistory();
                dtoOutEquipmentHistory.SetValueFromEntity(e);
                dtoOutEquipmentHistories.Add(dtoOutEquipmentHistory);
            }
            return dtoOutEquipmentHistories;
        }
        public async Task<IEnumerable<DtoOutEquipmentHistory>> GetEquipmentAssignedByEmployeeId(int id)
        {

            var equipmentHistories = await _unitOfWork.GetRepo<IEquipmentHistoryRepo>().GetEquipmentAssignedByEmployeeId(id);
            List<DtoOutEquipmentHistory> dtoOutEquipmentHistories = new List<DtoOutEquipmentHistory>();
            foreach (EquipmentHistory e in equipmentHistories)
            {
                var dtoOutEquipmentHistory = new DtoOutEquipmentHistory();
                dtoOutEquipmentHistory.SetValueFromEntity(e);
                dtoOutEquipmentHistories.Add(dtoOutEquipmentHistory);
            }
            return dtoOutEquipmentHistories;
        }
        
        public async Task<bool?> Update(int id, DtoEquipmentHistory dtoEquipmentHistory)
        {

            

            var equipmentHistory = await _unitOfWork.GetRepo<IEquipmentHistoryRepo>().GetById(id);
            if (equipmentHistory == null) return null;

            equipmentHistory.SetValueFromDto(dtoEquipmentHistory);

            _unitOfWork.GetRepo<IEquipmentHistoryRepo>().Update(equipmentHistory);
            var result = _unitOfWork.Save();

            if (result > 0)
                return true;
            else
                return false;

        }
  
        public async Task<DtoOutEquipmentHistory?> Add(DtoEquipmentHistory dtoEquipmentHistory)
        {

            Equipment? equipment = await _unitOfWork.GetRepo<IEquipmentRepo>().GetById(dtoEquipmentHistory.EquipmentId);

            if (equipment != null)
            {
                if (equipment.IsAvailable != true)
                {
                    return null;
                }
            }
            var equipmentHistoriesEmployee = await _unitOfWork.GetRepo<IEquipmentHistoryRepo>().GetByEmployeeId(dtoEquipmentHistory.EmployeeId);
            var check = equipmentHistoriesEmployee.FirstOrDefault(eh => eh.EmployeeId == dtoEquipmentHistory.EmployeeId && eh.EquipmentId == dtoEquipmentHistory.EquipmentId && eh.IsAproved == false);
            if (check != null)
            {
                return null;
            }
            var equipmentHistory = new EquipmentHistory();
            equipmentHistory.SetValueFromDto(dtoEquipmentHistory);
            await _unitOfWork.GetRepo<IEquipmentHistoryRepo>().Add(equipmentHistory);

            var result = _unitOfWork.Save();
            if (result <= 0) return null;
            var newEquipmentHistory = await _unitOfWork.GetRepo<IEquipmentHistoryRepo>().GetById(equipmentHistory.EquipmentHistoryId);

            if (newEquipmentHistory != null)
            {
                var dtoOutEquipmentHistory = new DtoOutEquipmentHistory();
                dtoOutEquipmentHistory.SetValueFromEntity(newEquipmentHistory);
                return dtoOutEquipmentHistory;
            }
            return null;
        }


        public async Task<bool?> Delete(int id)
        {

         
            var equipmentHistory = await _unitOfWork.GetRepo<IEquipmentHistoryRepo>().GetById(id);
            if (equipmentHistory == null) return null;
           
            _unitOfWork.GetRepo<IEquipmentHistoryRepo>().Delete(equipmentHistory);
            var result = _unitOfWork.Save();


            if (result <= 0) return false;
            return true;
        }
        public async Task<bool?> Approve(int id)
        {

            var equipmentHistory = await _unitOfWork.GetRepo<IEquipmentHistoryRepo>().GetById(id);
            if (equipmentHistory == null) return null;
            Equipment? equipment = await _unitOfWork.GetRepo<IEquipmentRepo>().GetById(equipmentHistory.EquipmentId);

            if(equipment != null)
            {
                if (equipment.IsAvailable == true)
                {
                    equipmentHistory.IsAproved = true;
                    _unitOfWork.GetRepo<IEquipmentHistoryRepo>().Update(equipmentHistory);
                    equipment.IsAvailable = false;
                    _unitOfWork.GetRepo<IEquipmentRepo>().Update(equipment);
                }
            }
            else
            {
                return false;
            }
            var result = _unitOfWork.Save();
            if (result <= 0) return false;
            return true;     
        }
        public async Task<bool?> CheckReturn(int id)
        {
            var equipmentHistory = await _unitOfWork.GetRepo<IEquipmentHistoryRepo>().GetById(id);
            if (equipmentHistory == null) return null;
            Equipment? equipment = await _unitOfWork.GetRepo<IEquipmentRepo>().GetById(equipmentHistory.EquipmentId);
            if (equipmentHistory.IsAproved != true)
            {
                return false;
            }
            if (equipmentHistory.ReturnedDate != null)
            {
                return false;
            }
            equipmentHistory.ReturnedDate = DateTime.Now;
            if (equipment != null)
            {
                equipment.IsAvailable = true;
                _unitOfWork.GetRepo<IEquipmentRepo>().Update(equipment);

            }
            _unitOfWork.GetRepo<IEquipmentHistoryRepo>().Update(equipmentHistory);
            var result = _unitOfWork.Save();
            if (result <= 0) return false;
            return true;
        }

        
    }
}
