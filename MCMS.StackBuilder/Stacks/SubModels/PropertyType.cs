using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MCMS.StackBuilder.Stacks.SubModels
{
    public enum PropertyType
    {
        [EnumMember(Value = "string")] [Display(Name = "string")]
        String,

        [EnumMember(Value = "int")] [Display(Name = "int")]
        Int,

        [EnumMember(Value = "float")] [Display(Name = "float")]
        Float,

        [EnumMember(Value = "double")] [Display(Name = "double")]
        Double,

        [EnumMember(Value = "decimal")] [Display(Name = "decimal")]
        Decimal,

        [EnumMember(Value = "bool")] [Display(Name = "bool")]
        Bool,

        [EnumMember(Value = "newEnum")] [Display(Name = "New Enum")]
        NewEnum,

        [EnumMember(Value = "customType")] [Display(Name = "Custom Existing Type")]
        CustomType,
    }
}