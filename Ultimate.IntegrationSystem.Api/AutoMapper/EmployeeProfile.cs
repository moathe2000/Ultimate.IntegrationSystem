using AutoMapper;
using Ultimate.IntegrationSystem.Api.Dto;
using Ultimate.IntegrationSystem.Api.Dto.Muqeem.Responses;
using Ultimate.IntegrationSystem.Api.Models;

namespace Ultimate.IntegrationSystem.Api.AutoMapper
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeModel, EmployeeDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EmployeeNumber))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.EmployeeName))
                .ForMember(dest => dest.FullNameEn, opt => opt.MapFrom(src => src.EmployeeNameForeign))
                .ForMember(dest => dest.JobId, opt => opt.MapFrom(src => src.JobNo))
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.JobName))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.HierarchyNumber))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.HierarchyName))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.TelNo))
                .ForMember(dest => dest.Mobile, opt => opt.MapFrom(src => src.MobileNo))
                .ForMember(dest => dest.PoBox, opt => opt.MapFrom(src => src.PoBoxNo))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Website))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Inactive == "0")) // 0 = Active
                .ForMember(dest => dest.BorderId, opt => opt.MapFrom(src => src.BorderNumber))
                .ForMember(dest => dest.BirthCountryId, opt => opt.MapFrom(src => src.BirthCountry))
                .ForMember(dest => dest.BirthCountry, opt => opt.MapFrom(src => src.BirthCountryName))
                .ForMember(dest => dest.MaritalStatusId, opt => opt.MapFrom(src => src.MaritalStatus))
                .ForMember(dest => dest.MaritalStatus, opt => opt.MapFrom(src => src.MaritalStatusName))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstNameLocal))
                .ForMember(dest => dest.FirstNameEn, opt => opt.MapFrom(src => src.FirstNameForeign))
                .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.SecondNameLocal))
                .ForMember(dest => dest.MiddleNameEn, opt => opt.MapFrom(src => src.SecondNameForeign))
                .ForMember(dest => dest.ThirdName, opt => opt.MapFrom(src => src.ThirdNameLocal))
                .ForMember(dest => dest.ThirdNameEn, opt => opt.MapFrom(src => src.ThirdNameForeign))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastNameLocal))
                .ForMember(dest => dest.LastNameEn, opt => opt.MapFrom(src => src.LastNameForeign))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.CityName));


            CreateMap<RawIssueIqamaResponse, IssueIqamaResponseDto>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.birthDateG) ? (DateTime?)null : DateTime.Parse(src.birthDateG)))
                .ForMember(dest => dest.ExpiryDateGregorian, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.iqamaExpiryDateG) ? (DateTime?)null : DateTime.Parse(src.iqamaExpiryDateG)))
                .ForMember(dest => dest.ExpiryDateHijri, opt => opt.MapFrom(src => src.iqamaExpiryDateH))
                .ForMember(dest => dest.IqamaNumber, opt => opt.MapFrom(src => src.iqamaNumber))

                .ForMember(dest => dest.ResidentArabicName, opt => opt.MapFrom(src => src.residentName))
                .ForMember(dest => dest.ResidentEnglishName, opt => opt.MapFrom(src => src.translatedResidentName))

                .ForMember(dest => dest.NationalityArabic, opt => opt.MapFrom(src => src.nationality.ar))
                .ForMember(dest => dest.NationalityEnglish, opt => opt.MapFrom(src => src.nationality.en))
                .ForMember(dest => dest.NationalityCode, opt => opt.MapFrom(src => src.nationality.code))

                .ForMember(dest => dest.OccupationArabic, opt => opt.MapFrom(src => src.occupation.ar))
                .ForMember(dest => dest.OccupationEnglish, opt => opt.MapFrom(src => src.occupation.en))
                .ForMember(dest => dest.OccupationCode, opt => opt.MapFrom(src => src.occupation.code))

                .ForMember(dest => dest.ReligionArabic, opt => opt.MapFrom(src => src.religion.ar))
                .ForMember(dest => dest.ReligionEnglish, opt => opt.MapFrom(src => src.religion.en))
                .ForMember(dest => dest.ReligionCode, opt => opt.MapFrom(src => src.religion.code))

                .ForMember(dest => dest.OrganizationMOINumber, opt => opt.MapFrom(src => src.organizationMOINumber))
                .ForMember(dest => dest.OrganizationName, opt => opt.MapFrom(src => src.organizationName));
        }

    }
}
