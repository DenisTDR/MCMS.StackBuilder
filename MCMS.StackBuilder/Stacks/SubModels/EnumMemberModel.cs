using System.ComponentModel.DataAnnotations;
using MCMS.Base.Data.FormModels;
using MCMS.Base.SwaggerFormly.Formly.Fields;

namespace MCMS.StackBuilder.Stacks.SubModels
{
    public class EnumMemberModel : IFormModel
    {
        [FormlyField(ClassName = "col-3 d-block-nf")]
        [Required]
        public string Name { get; set; }

        [FormlyField(ClassName = "col-3 d-block-nf")]
        public string Value { get; set; }

        [FormlyField(ClassName = "col-3 d-block-nf")]
        public string DisplayName { get; set; }
    }
}