using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ApplicationAPI.Models.Enum;

namespace ApplicationAPI.Models
{
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [EnumDataType(typeof(JobTitles))]
        public JobTitles JobTitle { get; set; }

        public Company Company { get; set; }
    }
}
