using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagerMVC.Models
{
    public class EmployeeProject 
    {
        [Key][Column(Order = 1)]
        public int ProjectID { get; set; }
        [Key][Column(Order = 2)]
        public int EmployeeID { get; set; }

        public Employee Employee { get; set; }
        public Project Project { get; set; }

    }
}
