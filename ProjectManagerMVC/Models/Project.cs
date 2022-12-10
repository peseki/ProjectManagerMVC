using MessagePack;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace ProjectManagerMVC.Models
{
    public class Project
    {
        public int ProjectID { get; set; } //id проекта
        public string ProjectName { get; set; } // название проекта
        public string ClientCompany { get; set; } = string.Empty; //компания заказчик
        public string ContractorCompany { get; set; } = string.Empty; //компания исполнитель
        //public virtual Employee LeaderID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Priority { get; set; }

        public virtual ICollection<EmployeeProject> EmployeeProject { get; set; } // связующая таблица

    }
}
