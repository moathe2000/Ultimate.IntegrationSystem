using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace Ultimate.IntegrationSystem.Web.Pages.Integrations.muqeem.Dialoge
{
    public partial class IssueIqamaDialog : ComponentBase
    {
        [CascadingParameter] public IMudDialogInstance MudDialog { get; set; } = default!;
        protected MudForm? _form;

        public class SimpleEmployee
        {
            public int Id { get; set; }
            public string FullName { get; set; } = "";
            public string EmployeeNumber { get; set; } = "";
            public string? IqamaNumber { get; set; }
        }
        [Parameter] public SimpleEmployee? Employee { get; set; }

        public class IssueIqamaModel
        {
            [Required, RegularExpression("^(3|4|5)[0-9]{9}$")]
            public string BorderNumber { get; set; } = "";

            [Required, RegularExpression("^(12|24)$")]
            public string IqamaDuration { get; set; } = "12";

            [Required] public string LkBirthCountry { get; set; } = "";
            [Required] public string MaritalStatus { get; set; } = "";
            [Required] public string PassportIssueCity { get; set; } = "";

            [Required] public string TrFirstName { get; set; } = "";
            [Required] public string TrFatherName { get; set; } = "";
            [Required] public string TrGrandFatherName { get; set; } = "";
            [Required] public string TrFamilyName { get; set; } = "";
        }
        [Parameter] public IssueIqamaModel Model { get; set; } = new();

        protected override void OnParametersSet()
        {
            // تعبئة أولية تساعد المستخدم (يمكنك حذفها إذا لا تناسبك)
            if (Employee != null && string.IsNullOrWhiteSpace(Model.BorderNumber) && !string.IsNullOrWhiteSpace(Employee.IqamaNumber))
                Model.BorderNumber = ""; // لا يوجد تحويل حد — اتركها فارغة أو اجلبها من مصدر آخر
        }

        private string GetInitials(string? name)
        {
            if (string.IsNullOrWhiteSpace(name)) return "؟";
            var p = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return p.Length == 1 ? p[0][0].ToString() : $"{p[0][0]}{p[^1][0]}";
        }

        protected async Task Save()
        {
            if (_form is null) return;
            await _form.Validate();
            if (_form.IsValid)
                MudDialog.Close(DialogResult.Ok(Model));
        }
        protected void Cancel() => MudDialog.Cancel();
    }
}
