using ApiEquipment.Entities;

namespace ApiEquipment.Dto
{
    public class DtoOutEmployee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
        public string? AvatarUrl { get; set; }
        public bool IsBan { get; set; }
        public virtual List<DtoOutRole>? Roles { get; set; }
        public void SetValueFromEntity(Employee employee)
        {
            EmployeeId = employee.EmployeeId;
            Name = employee.Name;
            Email = employee.Email;
            Birthdate = employee.Birthdate;
            AvatarUrl = employee.AvatarUrl;
            IsBan = employee.IsBan;
            Roles = new List<DtoOutRole>();

            if (employee.EmployeeRoles != null)
            {
                foreach (var er in employee.EmployeeRoles)
                {
                    if (er.Role != null)
                    {
                        var dtoRole = new DtoOutRole();
                        dtoRole.SetValueFromEntity(er.Role);
                        Roles.Add(dtoRole);
                    }
                }
            }
        }
    }
}
