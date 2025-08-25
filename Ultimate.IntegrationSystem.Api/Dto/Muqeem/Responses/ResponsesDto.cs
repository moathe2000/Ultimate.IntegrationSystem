namespace Ultimate.IntegrationSystem.Api.Dto.Muqeem.Responses
{
    // ✅ رد إصدار/إلغاء/طباعة خروج وعودة
    public class ERVisaResponseDto
    {
        public string IqamaNumber { get; set; }
        public string ResidentName { get; set; }
        public string TranslatedResidentName { get; set; }
        public string VisaNumber { get; set; }
        public string VisaType { get; set; }
        public int VisaDuration { get; set; }
        public string ErVisaPdf { get; set; }
    }

    // ✅ رد تجديد إقامة
    public class IqamaRenewalDto
    {
        public string IqamaNumber { get; set; }
        public string NewIqamaExpiryDateGre { get; set; }
        public string NewIqamaExpiryDateHij { get; set; }
        public string ResidentName { get; set; }
        public string TranslatedResidentName { get; set; }
        public string VersionNumber { get; set; }
    }

    // ✅ رد خروج نهائي
    public class FinalExitDto
    {
        public string IqamaNumber { get; set; }
        public string ResidentName { get; set; }
        public string VisaNumber { get; set; }
        public string VisaStatus { get; set; }
        public string ExitBefore { get; set; }
        public string IssuanceDate { get; set; }
        public string VisaType { get; set; }
    }

    // ✅ رد إصدار إقامة
   
    public class IqamaIssueResponseDto
    {
        public string BirthDateG { get; set; }
        public string IqamaExpiryDateG { get; set; }
        public string IqamaExpiryDateH { get; set; }
        public string IqamaNumber { get; set; }
        public LocalizedValue Nationality { get; set; }
        public LocalizedValue Occupation { get; set; }
        public string OrganizationMOINumber { get; set; }
        public string OrganizationName { get; set; }
        public LocalizedValue Religion { get; set; }
        public string ResidentName { get; set; }
        public string TranslatedResidentName { get; set; }
    }


    // ✅ رد نقل كفالة
    public class TransferIqamaDto
    {
        public string IqamaNumber { get; set; }
        public string ResidentName { get; set; }
        public string PassportNumber { get; set; }
        public string IqamaExpiryDateG { get; set; }
        public string IqamaExpiryDateH { get; set; }
        public string Nationality { get; set; }
        public string Occupation { get; set; }
        public string Religion { get; set; }
        public string Gender { get; set; }
    }

    // ✅ رد التحقق من الموافقة على تغيير المهنة
    public class OccupationApprovalDto
    {
        public string NewOccupationCode { get; set; }
        public string NewOccupationTitle { get; set; }
        public string OldOccupation { get; set; }
        public string SponsorID { get; set; }
        public string SponsorName { get; set; }
    }

    // ✅ رد تغيير المهنة
    public class OccupationChangedDto
    {
        public string IqamaNumber { get; set; }
        public string IqamaExpiryDateG { get; set; }
        public string IqamaExpiryDateH { get; set; }
        public string IqamaVersionNumber { get; set; }
        public string PassportNumber { get; set; }
        public string PassportExpiryDateG { get; set; }
        public string PassportExpiryDateH { get; set; }
        public string OldOccupation { get; set; }
        public string NewOccupationCode { get; set; }
        public string NewOccupationTitle { get; set; }
        public string SponsorID { get; set; }
        public string SponsorName { get; set; }
        public string Nationality { get; set; }
    }

    // ✅ رد تمديد تأشيرة زيارة
    public class ExtendVisitVisaDto
    {
        public string VisaNumber { get; set; }
        public string PassportNumber { get; set; }
        public string VisitorName { get; set; }
        public string VisaExpiryDateG { get; set; }
        public string VisaExpiryDateH { get; set; }
    }

    // ✅ رد تقرير الخدمات التفاعلية
    public class InteractiveServicesReportDto
    {
        public string Company { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string ErrorMessage { get; set; }
        public string IqamaNumber { get; set; }
        public string RequestNumber { get; set; }
        public string Type { get; set; }
        public string User { get; set; }
    }

    // ✅ DTOs مساعدة
    public record LookupItem(string Code, string NameAr, string NameEn);
    public record OperationDone(bool Done);

    public class FilePayload
    {
        public byte[] Bytes { get; }
        public string ContentType { get; }
        public string FileName { get; }

        public FilePayload(byte[] bytes, string contentType, string fileName)
        {
            Bytes = bytes;
            ContentType = contentType;
            FileName = fileName;
        }
    }
}