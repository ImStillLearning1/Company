using ApplicationAPI.DbContexts;
using ApplicationAPI.Models;
using ApplicationAPI.Models.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ApplicationAPI.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public CompanyRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CompanyDto> CreateCompany(CompanyDto companyDto)
        {
            
            Company company = _mapper.Map<CompanyDto, Company>(companyDto);
            bool isCompanyContainsNullValues = CheckIfObjectHasNullValues(company);
            if (isCompanyContainsNullValues == true)
            {
                throw new Exception("Wszystkie pola oprócz listy muszą być wypełnione");
            }
            _db.Companies.Add(company);

            await _db.SaveChangesAsync();
            return _mapper.Map<Company, CompanyDto>(company);
        }

        public async Task<bool> DeleteCompany(Guid companyId)
        {
            Company company = await _db.Companies.FirstOrDefaultAsync(u => u.Id == companyId);
            if (company == null)
                throw new Exception("Nie ma takiego ID w bazie");

            _db.Employees.RemoveRange(_db.Employees.Where(u => u.Company.Id == companyId));
            _db.Companies.Remove(company);

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CompanyDto>> GetCompanies()
        {
            List<Company> companiesList = await _db.Companies.Include(x => x.Employees).ToListAsync();
            return _mapper.Map<List<CompanyDto>>(companiesList);
        }

        public async Task<CompanyDto> GetCompanyById(Guid companyId)
        {
            Company company = await _db.Companies.Include(x => x.Employees).Where(x => x.Id == companyId).FirstOrDefaultAsync();
            return _mapper.Map<CompanyDto>(company);
        }

        public async Task<CompanyDto> UpdateCompany(Guid id, CompanyDto companyDto)
        {
            var companyFromDb = await _db.Companies.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            if (companyFromDb == null)
                throw new Exception("Nie ma takiego ID w bazie");

            else
            {
                companyFromDb = _mapper.Map<CompanyDto, Company>(companyDto);
                companyFromDb.Id = id;

                bool isCompanyContainsNullValues = CheckIfObjectHasNullValues(companyFromDb);
                if (isCompanyContainsNullValues == true)
                {
                    throw new Exception("Wszystkie pola oprócz listy muszą być wypełnione");
                }
                
                _db.Employees.RemoveRange(_db.Employees.Where(u => u.Company.Id == id));
                await _db.SaveChangesAsync();
            }

            _db.Companies.Update(companyFromDb);
            await _db.SaveChangesAsync();

            return _mapper.Map<Company, CompanyDto>(companyFromDb);
        }

        public async Task<List<CompanyDto>> GetCompaniesByFilter(SearchDto searchDto)
        {
            List<Company> companiesList = await _db.Companies.Include(b => b.Employees)
                .Where(x => x.Employees
                    .Where(u => u.DateOfBirth > searchDto.EmployeeDateOfBirthFrom || searchDto.EmployeeDateOfBirthFrom == null)
                    .Where(u => u.DateOfBirth < searchDto.EmployeeDateOfBirthTo || searchDto.EmployeeDateOfBirthTo == null)
                    .Where(u => x.Name.Contains(searchDto.Keyword) || u.FirstName.Contains(searchDto.Keyword) || u.LastName.Contains(searchDto.Keyword) || searchDto.Keyword == null)
                    .Where(u => u.JobTitle == searchDto.EmployeeJobTitles || searchDto.EmployeeJobTitles == null)
                    .Count() > 0)
                .ToListAsync();

            return _mapper.Map<List<CompanyDto>>(companiesList);

        }

        private bool CheckIfObjectHasNullValues(object model)
        {
            var properties = model.GetType().GetProperties();
            foreach(PropertyInfo property in properties)
            {
                if (property.Name == "Employees")
                    continue;

                if (property.GetValue(model) == null)
                    return true;
            }
            return false;
        }
    }
}
