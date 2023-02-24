using Microsoft.AspNetCore.Components.Forms;

namespace Exl.ResumeBuilder.Web
{
    public class BootstrapValidationClassProvider : FieldCssClassProvider
    {
        public override string GetFieldCssClass(EditContext editContext,
            in FieldIdentifier fieldIdentifier)
        {
            var isValid = !editContext.GetValidationMessages(fieldIdentifier).Any();

            return isValid ? "valid-feedback" : "invalid-feedback";
        }
    }
}
