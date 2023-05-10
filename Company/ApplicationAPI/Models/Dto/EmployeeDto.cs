using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using ApplicationAPI.Models.Enum;

namespace ApplicationAPI.Models.Dto
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        [EnumDataType(typeof(JobTitles))]
        public JobTitles JobTitle { get; set; }
        public CompanyDto CompanyDto { get; set; }
    }
}
