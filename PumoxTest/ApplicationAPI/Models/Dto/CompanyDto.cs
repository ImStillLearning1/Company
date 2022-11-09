using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApplicationAPI.Models.Dto
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int EstabilishmentYear { get; set; }
        public ICollection<EmployeeDto> Employees { get; set; }
    }
}
