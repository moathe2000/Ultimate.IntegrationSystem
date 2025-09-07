namespace Ultimate.IntegrationSystem.Web.Dto.Muqeem
{
    public  class MasterMuqeemRequestDto
    {
        // --- ER Visa ---
        public string IqamaNumber { get; set; }
        public int? VisaDuration { get; set; } // بالأيام
        public int? VisaType { get; set; }     // 1=مفردة, 2=متعددة
        public string ReturnBefore { get; set; } // yyyy-MM-dd
        public string ErVisaNumber { get; set; }
        public string VisaNumber { get; set; }

        // --- FE Visa ---
        public string FeVisaNumber { get; set; }

        // --- Iqama / Passport ---
        public string IqamaDuration { get; set; } // "12" أو "24"
        public string EmployeeNumber { get; set; }
        public string BorderNumber { get; set; }
        public string NewPassportExpiryDate { get; set; } // yyyy-MM-dd
        public string PassportNumber { get; set; }
        public string NewPassportIssueDate { get; set; }
        public string NewPassportIssueLocation { get; set; }
        public string NewPassportNumber { get; set; }

        // --- Issue New Iqama ---
        public string LkBirthCountry { get; set; }
        public string MaritalStatus { get; set; }
        public string PassportIssueCity { get; set; }
        public string TrFamilyName { get; set; }
        public string TrFatherName { get; set; }
        public string TrFirstName { get; set; }
        public string TrGrandFatherName { get; set; }

        // --- Change Occupation ---
        // IqamaNumber موجود مسبقاً

        // --- Interactive Services Report ---
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string OperatorId { get; set; }
        public string User { get; set; }

        // --- Transfer Iqama ---
        public string NewSponsorId { get; set; }

        // --- Reports ---
        public string Language { get; set; } // ar/en
        public bool? Print { get; set; }     // true=print, false=reprint
    }
}
