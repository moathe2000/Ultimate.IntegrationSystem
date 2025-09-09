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



            CreateMap<EmployeeDocument, EmployeeDocumentDto>()
                // حقول مباشرة
                .ForMember(d => d.EmployeeNo, opt => opt.MapFrom(s => s.EmployeeNumber))
                .ForMember(d => d.DocNo, opt => opt.MapFrom(s => s.DocumentNumber))
                .ForMember(d => d.IssueDate, opt => opt.MapFrom(s => s.IssueDate))
                .ForMember(d => d.ExpiryDate, opt => opt.MapFrom(s => s.ExpiryDate))
                .ForMember(d => d.RenewalDate, opt => opt.MapFrom(s => s.RenewalDate))
                .ForMember(d => d.IssuePlace, opt => opt.MapFrom(s => s.IssuePlace))
                .ForMember(d => d.IsDefault, opt => opt.MapFrom(s => s.IsDefault))
                .ForMember(d => d.IsInactive, opt => opt.MapFrom(s => s.IsInactive))

                // نوع الوثيقة المقروء من رقم النوع
                .ForMember(d => d.DocType, opt => opt.ConvertUsing(new DocTypeNameConverter(), src => src.DocumentTypeNumber))

                // subtype (عربي -> إنجليزي -> Fallback)
                .ForMember(d => d.SubType, opt => opt.MapFrom<SubTypeResolver>())

                // توحيد شكل التاريخ الهجري
                .ForMember(d => d.IssueDateHijri, opt => opt.ConvertUsing(new HijriSlashConverter(), src => src.IssueDateHijri))
                .ForMember(d => d.ExpiryDateHijri, opt => opt.ConvertUsing(new HijriSlashConverter(), src => src.ExpiryDateHijri))
                .ForMember(d => d.RenewalDateHijri, opt => opt.ConvertUsing(new HijriSlashConverter(), src => src.RenewalDateHijri))

                // اشتقاقات العرض
                .ForMember(d => d.IsExpired, opt => opt.MapFrom(s =>
                    s.ExpiryDate.HasValue && s.ExpiryDate.Value.Date < DateTime.Today))

                .ForMember(d => d.DaysToExpire, opt => opt.MapFrom(s =>
                    s.ExpiryDate.HasValue
                        ? (int?)(s.ExpiryDate.Value.Date - DateTime.Today).TotalDays
                        : null
                ));




        }
    }
}
