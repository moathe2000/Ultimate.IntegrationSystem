namespace Ultimate.IntegrationSystem.Api.Dto.Muqeem.Responses
{
    public class RawIssueIqamaResponse
    {
        public string birthDateG { get; set; }
        public string iqamaExpiryDateG { get; set; }
        public string iqamaExpiryDateH { get; set; }
        public string iqamaNumber { get; set; }
        public RawNested nationality { get; set; }
        public RawNested occupation { get; set; }
        public RawNested religion { get; set; }
        public string organizationMOINumber { get; set; }
        public string organizationName { get; set; }
        public string residentName { get; set; }
        public string translatedResidentName { get; set; }
    }

    public class RawNested
    {
        public string ar { get; set; }
        public string en { get; set; }
        public string code { get; set; }
    }

}
