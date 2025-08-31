

namespace Ultimate.IntegrationSystem.Web.Dto.Muqeem
{
    public class IqamaIssueDto
    {
        public string BirthDateG { get; set; }
        public DateTime? IqamaExpiryDateG { get; set; }
        public DateTime? IqamaExpiryDateH { get; set; }
        public string IqamaNumber { get; set; }
        public LocalizedValue Nationality { get; set; }
        public LocalizedValue Occupation { get; set; }
        public string OrganizationMOINumber { get; set; }
        public string OrganizationName { get; set; }
        public LocalizedValue Religion { get; set; }
        public string ResidentName { get; set; }
        public string TranslatedResidentName { get; set; }
    }
}

   
