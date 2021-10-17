using System.ComponentModel.DataAnnotations;
using MCMS.Base.Data.FormModels;
using MCMS.Base.SwaggerFormly.Formly.Base;
using MCMS.Base.SwaggerFormly.Formly.Fields;

namespace MCMS.StackBuilder.Stacks.SubModels
{
    public class FileFieldOptions : IFormModel
    {
        [FormlyField(ClassName = "col-3 d-flex-nf", DefaultValue = true)]
        public bool UseFileUploaderInForm { get; set; }

        [FormlyField(ClassName = "col-3 d-flex-nf")]
        [FormlyFieldProp("hideExpression", "!model.useFileUploaderInForm")]
        public string FilePurpose { get; set; }

        [FormlyField(ClassName = "col-3 d-flex-nf")]
        [Display(Description = "Leave blank to use Purpose")]
        [FormlyFieldProp("hideExpression", "!model.useFileUploaderInForm")]
        public string FilePath { get; set; }

        [FormlyField(ClassName = "col-3 d-flex-nf")]
        [Display(Description = "CSV list of extensions")]
        [FormlyFieldProp("hideExpression", "!model.useFileUploaderInForm")]
        public string AllowedExtensions { get; set; }
    }
}