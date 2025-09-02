namespace Ultimate.IntegrationSystem.Web.Models
{
    public class IqamaModel
    {
        public string BorderNumber { get; set; } = "";
        public int IqamaDuration { get; set; } = 12;
        public string BirthCountryCode { get; set; } = "";
        public string MaritalStatus { get; set; } = "";
        public string PassportIssueCity { get; set; } = "";

        // تجديد
        public string IqamaNumber { get; set; } = "";

        // EN Names (إصدار)
        public string TrFirstName { get; set; } = "";
        public string TrFatherName { get; set; } = "";
        public string TrGrandFatherName { get; set; } = "";
        public string TrFamilyName { get; set; } = "";
    }
}
