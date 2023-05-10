using ApplicationAPI.Models.Dto;

namespace ApplicationAPI.Repository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeDto>> GetEmployees();
        Task<EmployeeDto> GetEmployeeById(Guid employeeId);
        Task<EmployeeDto> GetEmployeesByCompanyId(Guid companyId);
        Task<EmployeeDto> CreateEmployee(EmployeeDto employeeDto);
        Task<EmployeeDto> UpdateEmployee(EmployeeDto employeeDto);
        Task<bool> DeleteEmployee(Guid companyId);
    }
}
