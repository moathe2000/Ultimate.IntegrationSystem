namespace Ultimate.IntegrationSystem.Web.Dto.Muqeem
{
    public class RequestDto
    {
    }

    
        //// ===== Iqama =====
        //public class IssueIqamaRequestDto
        //{
        //    public int EmployeeNo { get; set; }
        //    public string? IqamaNumber { get; set; }
        //    public string? Occupation { get; set; }
        //    public DateTime? IssueDate { get; set; }
        //    public DateTime? ExpiryDate { get; set; }
        //}

        //public class RenewIqamaRequestDto
        //{
        //    public int EmployeeNo { get; set; }
        //    public string? IqamaNumber { get; set; }
        //    public DateTime? OldExpiryDate { get; set; }
        //    public DateTime? NewExpiryDate { get; set; }
        //}

        public class TransferIqamaRequestDto
        {
            public int EmployeeNo { get; set; }
            public string? IqamaNumber { get; set; }
            public int FromOrganizationId { get; set; }
            public int ToOrganizationId { get; set; }
        }

        // ===== Exit / Re-Entry =====
        public class FEVisaIssuanceRequestDto
        {
            public int EmployeeNo { get; set; }
            public string? IqamaNumber { get; set; }
            public DateTime? ReturnBefore { get; set; }
        }

        public class ERVisaCancellationRequestDto
        {
            public int EmployeeNo { get; set; }
            public string? VisaNumber { get; set; }
            public string? Reason { get; set; }
        }

        public class ReprintERVisaRequestDto
        {
            public int EmployeeNo { get; set; }
            public string? VisaNumber { get; set; }
        }

        public class ERVisaExtendRequestDto
        {
            public int EmployeeNo { get; set; }
            public string? VisaNumber { get; set; }
            public DateTime? NewExpiryDate { get; set; }
        }

        // ===== Final Exit =====
        public class FEVisaCancellationRequestDto
        {
            public int EmployeeNo { get; set; }
            public string? IqamaNumber { get; set; }
            public string? Reason { get; set; }
        }

        public class IssueFEDuringTheProbationaryPeriodRequestDto
        {
            public int EmployeeNo { get; set; }
            public string? IqamaNumber { get; set; }
            public DateTime? ProbationEndDate { get; set; }
        }

        // ===== Update Passport Info =====
        public class UIExtendPassportValidityRequestDto
        {
            public int EmployeeNo { get; set; }
            public string? PassportNumber { get; set; }
            public DateTime? OldExpiryDate { get; set; }
            public DateTime? NewExpiryDate { get; set; }
        }

        public class UIRenewPassportRequestDto
        {
            public int EmployeeNo { get; set; }
            public string? PassportNumber { get; set; }
            public DateTime? NewExpiryDate { get; set; }
        }

        // ===== Visit Visa =====
        public class ExtendVisitVisaRequestDto
        {
            public int VisitorId { get; set; }
            public string? PassportNumber { get; set; }
            public DateTime? NewExpiryDate { get; set; }
        }

        // ===== Reports =====
        public class InteractiveServicesReportRequestDto
        {
            public int OrganizationId { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
        }

        public class MuqeemReportRequestDto
        {
            public int OrganizationId { get; set; }
            public string? ReportType { get; set; }
            public DateTime ReportDate { get; set; }
        }

        public class VisitorReportRequestDto
        {
            public int OrganizationId { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
        }

        // ===== Occupation =====
        public class ChangeOccupationApprovalRequestDto
        {
            public int EmployeeNo { get; set; }
            public string? CurrentOccupation { get; set; }
            public string? NewOccupation { get; set; }
        }

        public class ChangeOccupationRequestDto
        {
            public int EmployeeNo { get; set; }
            public string? NewOccupation { get; set; }
            public DateTime EffectiveDate { get; set; }
        }
    

}
