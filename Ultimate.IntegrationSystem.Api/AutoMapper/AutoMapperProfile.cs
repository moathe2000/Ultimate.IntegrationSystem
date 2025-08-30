using AutoMapper;
using Ultimate.IntegrationSystem.Api.Dto;
using Ultimate.IntegrationSystem.Api.Models;

namespace Ultimate.IntegrationSystem.Api.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EmployeeModel, EmployeeDto>().ReverseMap();
        }
    }
}
