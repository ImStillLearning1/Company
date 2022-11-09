using ApplicationAPI.Models;
using ApplicationAPI.Models.Dto;
using AutoMapper;

namespace ApplicationAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CompanyDto, Company>();
                config.CreateMap<Company, CompanyDto>();
                config.CreateMap<EmployeeDto, Employee>();
                config.CreateMap<Employee, EmployeeDto>();
            });

            return mappingConfig;
        }
    }
}
