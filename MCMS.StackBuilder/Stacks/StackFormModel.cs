using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MCMS.Base.Data.FormModels;
using MCMS.Base.SwaggerFormly.Formly.Fields;
using MCMS.StackBuilder.Stacks.SubModels;

namespace MCMS.StackBuilder.Stacks
{
    public class StackFormModel : IFormModel
    {
        [FormlyField(ClassName = "col-6 d-flex")]
        public string Name { get; set; }

        [FormlyField(ClassName = "col-6 d-flex")]
        public string PluralName { get; set; }

        [FormlyFieldGroup(FieldGroupClassName = "row")]
        public StackConfigModel Config { get; set; }

        [FormlyArray(FieldGroupClassName = "row", Wrappers = new[] {"card"})]
        [MinLength(1)]
        public List<PropertyModel> Properties { get; set; }
    }
}