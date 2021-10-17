using MCMS.Base.Extensions;

namespace MCMS.StackBuilder.Stacks.SubModels
{
    public static class PropertyModelExtensions
    {
        public static string GetTypeStr(this PropertyModel propertyModel, ModelType modelType)
        {
            switch (propertyModel.Type)
            {
                case PropertyType.CustomType:
                    var str = propertyModel.GetCustomTypeName(modelType);

                    if (propertyModel.IsList)
                    {
                        str = "List<" + str + ">";
                    }

                    return str;

                case PropertyType.McmsFile:
                    return modelType != ModelType.Entity ? "FileViewModel" : "FileEntity";
                case PropertyType.NewEnum:
                    return propertyModel.EnumName;
                default:
                    return propertyModel.Type.GetCustomValue().ToString();
            }
        }

        public static string GetCustomTypeName(this PropertyModel propertyModel, ModelType modelType)
        {
            if (!propertyModel.IsEntityWithStack)
            {
                return propertyModel.CustomType;
            }

            switch (modelType)
            {
                case ModelType.Entity:
                    return propertyModel.CustomType + "Entity";
                case ModelType.AdminApiController:
                    return propertyModel.CustomType + "sAdminApiController";
                default:
                    return propertyModel.CustomType + "ViewModel";
            }
        }
    }
}