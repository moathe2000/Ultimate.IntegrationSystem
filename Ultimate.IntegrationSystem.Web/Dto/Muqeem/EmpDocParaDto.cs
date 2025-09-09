namespace Ultimate.IntegrationSystem.Web.Dto.Muqeem
{
    public class EmpDocParaDto
    {
        public int? P_EMP_NO { get; set; }
        public int? P_CODE_NO { get; set; }
        public int? P_DCMNT_TYP_NO { get; set; }
        public string? P_DOC_NO { get; set; }
        public int? P_SUB_CODE_NO { get; set; }
        public int? P_ONLY_ACTIVE { get; set; } = 1; // 1=نشط فقط
        public int? P_RNWL_DOC_TYP { get; set; } = 810;
        public string? P_RNWL_DOC_NO { get; set; }
        public int? P_RNWL_DOC_SRL { get; set; }
    }
}
