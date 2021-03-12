using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MCMS.Base.Data.FormModels;
using MCMS.Base.SwaggerFormly.Formly.Base;
using MCMS.Base.SwaggerFormly.Formly.Fields;

namespace MCMS.StackBuilder.Stacks.SubModels
{
    public class PropertyModel : IFormModel
    {
        [FormlyField(ClassName = "col-2 d-block")]
        [Required]
        public string Name { get; set; }

        [FormlyField(ClassName = "col-3 d-block", DefaultValue = PropertyType.String)]
        public PropertyType Type { get; set; }

        [FormlyField(ClassName = "col-3 d-block")]
        [Display(Description = "Leave blank to use Name")]
        public string DisplayName { get; set; }

        [FormlyField(ClassName = "col-2 d-flex-nf", DefaultValue = false)]
        public bool Required { get; set; }

        [FormlyField(ClassName = "col-2 d-flex-nf", DefaultValue = false)]
        [FormlyFieldProp("hideExpression", "model.type !== 'string'")]
        [Display(Description = "Use CkEditor")]
        public bool IsRichText { get; set; }

        [FormlyField(ClassName = "col-3 d-flex-nf")]
        [FormlyFieldProp("hideExpression", "model.type !== 'customType'")]
        public string CustomType { get; set; }

        [FormlyField(ClassName = "col-3 d-flex-nf", DefaultValue = true)]
        [FormlyFieldProp("hideExpression", "model.type !== 'customType'")]
        [Display(Description = "Append model type to type name.")]
        public bool IsEntityWithStack { get; set; }

        [FormlyField(ClassName = "col-3 d-flex-nf", DefaultValue = false)]
        [FormlyFieldProp("hideExpression", "model.type !== 'customType'")]
        [FormlyFieldProp("templateOptions.description",
            "'Use \\'List<' + (model.customType ? model.customType.split('.').slice(-1).pop() : 'T') + '>\\' as property type'",
            "expressionProperties")]
        [FormlyFieldProp("templateOptions.disabled", "!model.isEntityWithStack", "expressionProperties")]
        public bool IsList { get; set; }

        [FormlyField(ClassName = "col-3 d-flex-nf", DefaultValue = true)]
        [FormlyFieldProp("hideExpression", "model.type !== 'customType'")]
        [FormlyFieldProp("templateOptions.disabled", "!model.isEntityWithStack", "expressionProperties")]
        [DisplayName("Join db table")]
        public bool JoinDbTable { get; set; }

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

        public bool IsValidList()
        {
            return Type == PropertyType.CustomType && IsList;
        }

        public bool UseCkEditor()
        {
            return Type == PropertyType.String && IsRichText;
        }
    }
}