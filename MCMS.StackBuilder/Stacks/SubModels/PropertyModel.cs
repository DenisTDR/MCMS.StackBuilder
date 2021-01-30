using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MCMS.Base.Data.FormModels;
using MCMS.Base.SwaggerFormly.Formly;
using MCMS.Base.SwaggerFormly.Formly.Fields;

namespace MCMS.StackBuilder.Stacks.SubModels
{
    public class PropertyModel : IFormModel
    {
        [FormlyField(ClassName = "col-3 d-block")]
        [Required]
        public string Name { get; set; }

        [FormlyField(ClassName = "col-3 d-block", DefaultValue = PropertyType.String)]
        public PropertyType Type { get; set; }

        [FormlyField(ClassName = "col-3 d-flex-nf")]
        [FormlyFieldProp("hideExpression", "model.type !== 'customType'")]
        public string CustomType { get; set; }

        [FormlyField(ClassName = "col-3 d-flex-nf", DefaultValue = false)]
        [FormlyFieldProp("hideExpression", "model.type !== 'customType'")]
        public bool IsList { get; set; }

        [FormlyField(ClassName = "col-3 d-flex-nf", DefaultValue = false)]
        [FormlyFieldProp("hideExpression", "model.type !== 'customType'")]
        public bool IsEntityWithStack { get; set; }

        [FormlyField(ClassName = "col-3 d-flex-nf")]
        [FormlyFieldProp("hideExpression", "model.type !== 'newEnum'")]
        public string EnumName { get; set; }

        [FormlyFieldProp("hideExpression", "field.parent.model.type !== 'newEnum'")]
        [FormlyArray(ClassName = "flex-grow-1 mx-3", FieldGroupClassName = "full-width d-flex",
            Wrappers = new[] {"card"})]
        public List<EnumMemberModel> EnumMembers { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}