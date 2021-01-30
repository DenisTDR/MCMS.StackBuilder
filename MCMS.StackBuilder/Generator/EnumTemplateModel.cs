using MCMS.StackBuilder.Stacks;
using MCMS.StackBuilder.Stacks.SubModels;

namespace MCMS.StackBuilder.Generator
{
    public class EnumTemplateModel
    {
        public StackEntity Stack { get; set; }
        public PropertyModel PropertyModel { get; set; }

        public EnumTemplateModel(StackEntity stack, PropertyModel propertyModel)
        {
            Stack = stack;
            PropertyModel = propertyModel;
        }

        public EnumTemplateModel()
        {
            
        }
    }
}