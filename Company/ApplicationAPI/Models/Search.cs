namespace ApplicationAPI.Models
{
    public class Search
    {
        public string Keyword { get; set; }
        public DateTime EmployeeDateOfBirthFrom { get; set; }
        public DateTime EmployeeDateOfBirthTo { get; set; }
        public string EmployeeJobTitles { get; set; }
    }
}
