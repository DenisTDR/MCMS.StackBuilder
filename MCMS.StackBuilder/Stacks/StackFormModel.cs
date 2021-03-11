using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MCMS.Base.Data.FormModels;
using MCMS.Base.SwaggerFormly.Formly;
using MCMS.Base.SwaggerFormly.Formly.Fields;
using MCMS.StackBuilder.Stacks.SubModels;

namespace MCMS.StackBuilder.Stacks
{
    public class StackFormModel : IFormModel
    {
        [FormlyField(ClassName = "col-4 d-flex")]
        [RegularExpression("^[a-zA-Z_][a-zA-Z0-9_]{2,}$")]
        [Required]
        public string Name { get; set; }

        [FormlyField(ClassName = "col-4 d-flex")]
        [FormlyAutoFill("model.name + 's'")]
        [Required]
        public string PluralName { get; set; }

        [FormlyField(ClassName = "col-4 d-flex")]
        public string RootNamespace { get; set; }

        [FormlyFieldGroup(FieldGroupClassName = "row")]
        [Required]
        public StackConfigModel Config { get; set; }

        [FormlyArray(FieldGroupClassName = "row", Wrappers = new[] {"card"})]
        [MinLength(1)]
        [Required]
        public List<PropertyModel> Properties { get; set; }
    }
}