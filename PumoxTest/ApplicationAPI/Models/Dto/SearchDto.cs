using ApplicationAPI.Models.Enum;

namespace ApplicationAPI.Models.Dto
{
    public class SearchDto
    {
        public string Keyword { get; set; }
        public DateTime? EmployeeDateOfBirthFrom { get; set; }
        public DateTime? EmployeeDateOfBirthTo { get; set; }
        public JobTitles? EmployeeJobTitles { get; set; }
    }
}
