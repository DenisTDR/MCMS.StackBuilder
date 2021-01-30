using System;
using MCMS.StackBuilder.Stacks.SubModels;

namespace MCMS.StackBuilder.Stacks
{
    public static class StackEntityExtensions
    {
        public static string GetNameFor(this StackEntity entity, ModelType modelType)
        {
            return modelType switch
            {
                ModelType.Entity => entity.Name + "Entity",
                ModelType.ViewModel => entity.Name + "ViewModel",
                ModelType.FormModel => entity.Name + "FormModel",
                ModelType.AdminApiController => entity.PluralName + "AdminApiController",
                ModelType.UiController => entity.PluralName + "UiController",
                _ => throw new ArgumentOutOfRangeException(nameof(modelType), modelType, null)
            };
        }
    }
}