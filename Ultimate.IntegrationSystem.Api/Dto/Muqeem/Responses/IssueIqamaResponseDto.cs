namespace Ultimate.IntegrationSystem.Api.Dto.Muqeem.Responses
{
    public class IssueIqamaResponseDto
    {
        public string IqamaNumber { get; set; } = "";
        public DateTime? BirthDate { get; set; }
        public DateTime? ExpiryDateGregorian { get; set; }
        public string ExpiryDateHijri { get; set; } = "";

        public string ResidentArabicName { get; set; } = "";
        public string ResidentEnglishName { get; set; } = "";

        public string NationalityArabic { get; set; } = "";
        public string NationalityEnglish { get; set; } = "";
        public string NationalityCode { get; set; } = "";

        public string OccupationArabic { get; set; } = "";
        public string OccupationEnglish { get; set; } = "";
        public string OccupationCode { get; set; } = "";

        public string ReligionArabic { get; set; } = "";
        public string ReligionEnglish { get; set; } = "";
        public string ReligionCode { get; set; } = "";

        public string OrganizationMOINumber { get; set; } = "";
        public string OrganizationName { get; set; } = "";
    }

}
