using ApplicationAPI.DbContexts;
using ApplicationAPI.Models.Dto;
using ApplicationAPI.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace ApplicationAPI.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public EmployeeRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> CreateEmployee(EmployeeDto employeeDto)
        {
            Employee employee = _mapper.Map<EmployeeDto, Employee>(employeeDto);
            employee.Company = _mapper.Map<CompanyDto, Company>(employeeDto.CompanyDto);

            _db.Employees.Add(employee);

            await _db.SaveChangesAsync();

            employeeDto = _mapper.Map<Employee, EmployeeDto>(employee);
            employeeDto.CompanyDto = _mapper.Map<Company, CompanyDto>(employee.Company);

            return employeeDto;

        }

        public async Task<bool> DeleteEmployee(Guid employeeId)
        {
            try
            {
                Employee employee = await _db.Employees.FirstOrDefaultAsync(u => u.Id == employeeId);
                if (employee == null)
                {
                    return false;
                }
                _db.Employees.Remove(employee);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<EmployeeDto> GetEmployeeById(Guid employeeId)
        {
            Employee employee = await _db.Employees.Where(u => u.Id == employeeId).FirstOrDefaultAsync();
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployees()
        {
            List<Employee> employees = await _db.Employees.ToListAsync();
            return _mapper.Map<List<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto> GetEmployeesByCompanyId(Guid companyId)
        {
            Employee employee = await _db.Employees.Where(u => u.Company.Id == companyId).FirstOrDefaultAsync();
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> UpdateEmployee(EmployeeDto employeeDto)
        {
            Employee employee = _mapper.Map<EmployeeDto, Employee>(employeeDto);
            _db.Employees.Update(employee);

            await _db.SaveChangesAsync();

            return _mapper.Map<Employee, EmployeeDto>(employee);
        }
    }
}
