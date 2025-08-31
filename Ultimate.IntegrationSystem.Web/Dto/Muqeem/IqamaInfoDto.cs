namespace Ultimate.IntegrationSystem.Web.Dto.Muqeem
{
    public class IqamaInfoDto
    {
        public string? BirthDateG { get; set; }
        public DateTime? IqamaExpiryDateG { get; set; }
        public string? IqamaExpiryDateH { get; set; }
        public string? IqamaNumber { get; set; }

        public LocalizedValue? Nationality { get; set; }
        public LocalizedValue? Occupation { get; set; }
        public LocalizedValue? Religion { get; set; }

        public string? OrganizationMOINumber { get; set; }
        public string? OrganizationName { get; set; }

        public string? ResidentName { get; set; }
        public string? TranslatedResidentName { get; set; }
    }

    /// <summary>
    /// كلاس مساعد للحقول اللي فيها (ar/en/code)
    /// </summary>
    public class LocalizedValue
    {
        public string? Ar { get; set; }
        public string? En { get; set; }
        public string? Code { get; set; }
    }

}
