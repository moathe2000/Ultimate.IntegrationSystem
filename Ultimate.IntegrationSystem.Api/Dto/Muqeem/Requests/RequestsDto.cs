using Newtonsoft.Json;

namespace Ultimate.IntegrationSystem.Api.Dto.Muqeem.Requests
{
    // ✅ إصدار تأشيرة خروج وعودة
    public sealed class ERVisaIssuanceRequestDto
    {
        [JsonProperty("iqamaNumber")]
        public string IqamaNumber { get; set; }

        [JsonProperty("visaDuration")]
        public int VisaDuration { get; set; } // بالأيام

        [JsonProperty("visaType")]
        public int VisaType { get; set; }     // 1 = مفردة، 2 = متعددة

        [JsonProperty("returnBefore")]
        public string ReturnBefore { get; set; } // yyyy-MM-dd
    }

    // ✅ إلغاء تأشيرة خروج وعودة
    public sealed class ERVisaCancellationRequestDto
    {
        [JsonProperty("erVisaNumber")]
        public string ErVisaNumber { get; set; }

        [JsonProperty("iqamaNumber")]
        public string IqamaNumber { get; set; }
    }

    // ✅ إعادة طباعة تأشيرة خروج وعودة
    public sealed class ERVisaReprintRequestDto
    {
        [JsonProperty("iqamaNumber")]
        public string IqamaNumber { get; set; }

        [JsonProperty("visaNumber")]
        public string VisaNumber { get; set; }
    }

    // ✅ إصدار تأشيرة خروج نهائي
    public sealed class FEVisaIssuanceRequestDto
    {
        [JsonProperty("iqamaNumber")]
        public string IqamaNumber { get; set; }
    }

    // ✅ إلغاء تأشيرة خروج نهائي
    public sealed class FEVisaCancellationRequestDto
    {
        [JsonProperty("feVisaNumber")]
        public string FeVisaNumber { get; set; }

        [JsonProperty("iqamaNumber")]
        public string IqamaNumber { get; set; }
    }

    // ✅ تجديد إقامة
    public sealed class RenewIqamaRequestDto
    {
        [JsonProperty("iqamaDuration")]
        public string IqamaDuration { get; set; } // "12" أو "24"

        [JsonProperty("iqamaNumber")]
        public string IqamaNumber { get; set; }
    }

    // ✅ تمديد صلاحية الجواز
    public sealed class UIExtendPassportValidityRequestDto
    {
        [JsonProperty("iqamaNumber")]
        public string IqamaNumber { get; set; }

        [JsonProperty("newPassportExpiryDate")]
        public string NewPassportExpiryDate { get; set; } // yyyy-MM-dd

        [JsonProperty("passportNumber")]
        public string PassportNumber { get; set; }
    }

    // ✅ تجديد بيانات الجواز
    public sealed class UIRenewPassportRequestDto
    {
        [JsonProperty("iqamaNumber")]
        public string IqamaNumber { get; set; }

        [JsonProperty("newPassportExpiryDate")]
        public string NewPassportExpiryDate { get; set; }

        [JsonProperty("newPassportIssueDate")]
        public string NewPassportIssueDate { get; set; }

        [JsonProperty("newPassportIssueLocation")]
        public string NewPassportIssueLocation { get; set; }

        [JsonProperty("newPassportNumber")]
        public string NewPassportNumber { get; set; }

        [JsonProperty("passportNumber")]
        public string PassportNumber { get; set; }
    }

    // ✅ إصدار إقامة جديدة
    public sealed class IssueIqamaRequestDto
    {
        public string BorderNumber { get; set; }
        public string IqamaDuration { get; set; }
        public string LkBirthCountry { get; set; }
        public string MaritalStatus { get; set; }
        public string PassportIssueCity { get; set; }
        public string TrFamilyName { get; set; }
        public string TrFatherName { get; set; }
        public string TrFirstName { get; set; }
        public string TrGrandFatherName { get; set; }
    }

    // ✅ التحقق من الموافقة على تغيير مهنة
    public sealed class ChangeOccupationApprovalRequestDto
    {
        public string IqamaNumber { get; set; }
    }

    // ✅ تنفيذ تغيير المهنة
    public sealed class ChangeOccupationRequestDto
    {
        public string IqamaNumber { get; set; }
    }

    // ✅ تقرير الخدمات التفاعلية
    public sealed class InteractiveServicesReportRequestDto
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string OperatorId { get; set; }
        public string User { get; set; }
    }

    // ✅ إعادة طباعة خروج وعودة
    public sealed class ReprintERVisaRequestDto
    {
        public string IqamaNumber { get; set; }
        public string VisaNumber { get; set; }
    }

    // ✅ تمديد تأشيرة خروج وعودة
    public sealed class ERVisaExtendRequestDto
    {
        public string IqamaNumber { get; set; }
        public string VisaNumber { get; set; }
        public int VisaDuration { get; set; }
        public string ReturnBefore { get; set; } // yyyy-MM-dd
    }

    // ✅ تمديد زيارة
    public sealed class ExtendVisitVisaRequestDto
    {
        public string BorderNumber { get; set; }
    }

    // ✅ نقل كفالة
    public sealed class TransferIqamaRequestDto
    {
        public string IqamaNumber { get; set; }
        public string NewSponsorId { get; set; }
    }

    // ✅ طباعة تقرير مقيم
    public sealed class MuqeemReportRequestDto
    {
        public string IqamaNumber { get; set; }
        public string Language { get; set; } // ar/en
        public bool Print { get; set; }      // true=print, false=reprint
    }

    // ✅ طباعة تقرير زائر
    public sealed class VisitorReportRequestDto
    {
        public string BorderNumber { get; set; }
        public string Language { get; set; } // ar/en
        public bool Print { get; set; }
    }

    // ✅ إصدار خروج نهائي فترة تجربة
    public sealed class IssueFEDuringTheProbationaryPeriodRequestDto
    {
        public string BorderNumber { get; set; }
        public string LkBirthCountry { get; set; }
        public string MaritalStatus { get; set; }
        public string PassportIssueCity { get; set; }
        public string TrFamilyName { get; set; }
        public string TrFatherName { get; set; }
        public string TrFirstName { get; set; }
        public string TrGrandFatherName { get; set; }
    }
}


