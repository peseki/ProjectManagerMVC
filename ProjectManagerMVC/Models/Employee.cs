using MessagePack;

namespace ProjectManagerMVC.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; } //id проекта
        public string FirstName { get; set; } = string.Empty; //Имя
        public string LastName{ get; set; } = string.Empty; //Фамилия
        public string MiddleName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty; //email
        public virtual ICollection<EmployeeProject> EmployeeProject { get; set; } //связующая таблица
    }
}
