using ApplicationAPI.Models.Dto;
using ApplicationAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/company/")]
    public class CompanyAPIController : ControllerBase
    {
        protected ResponseDto _response;
        private ICompanyRepository _companyRepository;
        private IEmployeeRepository _employeeRepository;

        public CompanyAPIController(ICompanyRepository companyRepository, IEmployeeRepository employeeRepository)
        {
            _companyRepository = companyRepository;
            this._response = new ResponseDto();
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<CompanyDto> companyDtos = await _companyRepository.GetCompanies();
                _response.Result = companyDtos;
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>(){ ex.ToString() };
            }

            return _response;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<object> Get(Guid id)
        {
            try
            {
                CompanyDto companyDto = await _companyRepository.GetCompanyById(id);
                _response.Result = companyDto;
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpPost]
        [Route("create")]
        public async Task<object> Post([FromBody] CompanyDto companyDto)
        {
            try
            {
                CompanyDto model = await _companyRepository.CreateCompany(companyDto);
                _response.Result = model;

                return model.Id;
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message.ToString() };                

                return BadRequest(_response.ErrorMessages);
            }

        }
        
        [HttpPut]
        [Route("update/{id}")]
        public async Task<object> Put(Guid id,[FromBody] CompanyDto companyDto)
        {
            try
            {
                CompanyDto model = await _companyRepository.UpdateCompany(id, companyDto);
                _response.Result = model;

                return _response;
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message.ToString() };

                return BadRequest(_response.ErrorMessages);
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<object> Delete(Guid id)
        {
            try
            {
                bool isSuccess = await _companyRepository.DeleteCompany(id);
                _response.Result = isSuccess;

                return _response;
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message.ToString() };

                return BadRequest(_response.ErrorMessages);
            }

            
        }

        [HttpPost]
        [Route("search")]
        public async Task<object> Post([FromBody] SearchDto searchDto)
        {
            try
            {
                var models = await _companyRepository.GetCompaniesByFilter(searchDto);
                _response.Result = models;
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }
    }
}
