using AutoMapper;
using Ultimate.IntegrationSystem.Api.Dto.Muqeem.Responses;
using Ultimate.IntegrationSystem.Api.Models;

namespace Ultimate.IntegrationSystem.Api.AutoMapper
{
    public sealed class DocTypeNameConverter : IValueConverter<int, string>
    {
        public string Convert(int sourceMember, ResolutionContext context)
            => sourceMember switch
            {
                4 => "إقامة",
                1 => "جواز سفر",
                2 => "تأشيرة",
                3 => "هوية",
                0 => "غير محدد",
                _ => $"نوع-{sourceMember}"
            };
    }

    // يطبّق استبدال بسيط لتوحيد صيغة التاريخ الهجري (- إلى /)
    public sealed class HijriSlashConverter : IValueConverter<string, string>
    {
        public string Convert(string sourceMember, ResolutionContext context)
            => string.IsNullOrWhiteSpace(sourceMember) ? sourceMember : sourceMember.Replace('-', '/');
    }

    // يختار العربي ثم الإنجليزي للـ SubType وإلا يضع fallback
    public sealed class SubTypeResolver : IValueResolver<EmployeeDocument, EmployeeDocumentDto, string>
    {
        public string Resolve(EmployeeDocument src, EmployeeDocumentDto dest, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrWhiteSpace(src.SubCodeNameArabic)) return src.SubCodeNameArabic;
            if (!string.IsNullOrWhiteSpace(src.SubCodeNameEnglish)) return src.SubCodeNameEnglish;
            return $"Code {src.CodeNumber} / Sub {src.SubCodeNumber}";
        }
    }
}
