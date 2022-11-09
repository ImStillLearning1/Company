using ApplicationAPI.Models.Dto;

namespace ApplicationAPI.Repository
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<CompanyDto>> GetCompanies();
        Task<CompanyDto> GetCompanyById(Guid companyId);
        Task<CompanyDto> CreateCompany(CompanyDto companyDto);
        Task<CompanyDto> UpdateCompany(Guid id, CompanyDto companyDto);
        Task<bool> DeleteCompany(Guid companyId);
        Task<List<CompanyDto>> GetCompaniesByFilter(SearchDto searchDto);
    }
}
