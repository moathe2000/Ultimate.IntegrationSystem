namespace Ultimate.IntegrationSystem.Api.Dto
{
    public class EmpDocPara
    {
        public int? P_EMP_NO { get; set; }          // رقم الموظف
        public int? P_CODE_NO { get; set; }         // الكود
        public int? P_DCMNT_TYP_NO { get; set; }    // نوع الوثيقة
        public int? P_DOC_OWNR_TYP { get; set; }    // 1=الموظف، 2=المرافق
        public string? P_DOC_NO { get; set; }          // رقم الوثيقة (اختياري)
        public int? P_SUB_CODE_NO { get; set; }     // الرقم التسلسلي الفرعي (اختياري)
        public int? P_ONLY_ACTIVE { get; set; } = 1; // 1=نشط فقط (افتراضي)، 0=الكل

        // فلاتر جدول HRS_RNWL_EMP_DOC_DTL
        public int? P_RNWL_DOC_TYP { get; set; } = 810; // نوع مستند التجديد (افتراضي 810)
        public int? P_RNWL_DOC_NO { get; set; }        // رقم مستند التجديد
        public int? P_RNWL_DOC_SRL { get; set; }       // التسلسل
    }

}
