using System.ComponentModel.DataAnnotations;

namespace EmployeesWebNetCore.Models
{
    public class Employee
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required.")]
        public string Surname { get; set; }
    }
}