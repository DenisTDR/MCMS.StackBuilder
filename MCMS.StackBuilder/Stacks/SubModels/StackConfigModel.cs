using System.Linq;
using MCMS.Base.Data.FormModels;
using MCMS.Base.SwaggerFormly.Formly.Fields;

namespace MCMS.StackBuilder.Stacks.SubModels
{
    public class StackConfigModel : IFormModel
    {
        [FormlyField(ClassName = "col-12 d-flex", DefaultValue = false)]
        public bool CreateEntityTypeConfigurationFile { get; set; }

        [FormlyField(ClassName = "col-4 d-flex", DefaultValue = true)]
        public bool CreateEntity { get; set; }

        [FormlyField(ClassName = "col-4 d-flex", DefaultValue = true)]
        public bool CreateViewModel { get; set; }

        [FormlyField(ClassName = "col-4 d-flex", DefaultValue = true)]
        public bool CreateFormModel { get; set; }

        [FormlyField(ClassName = "col-6 d-flex", DefaultValue = true)]
        public bool CreateUiController { get; set; }

        [FormlyField(ClassName = "col-6 d-flex", DefaultValue = true)]
        public bool CreateApiController { get; set; }

        public override string ToString()
        {
            return string.Join(", ",
                new[]
                {
                    CreateEntity ? "Entity" : null,
                    CreateViewModel ? "ViewModel" : null,
                    CreateFormModel ? "FormModel" : null,
                    CreateEntityTypeConfigurationFile ? "TypeConfig" : null
                }.Where(s => s != null));
        }

        public bool ShouldCreateEntityTypeConfiguration()
        {
            return CreateEntity && CreateEntityTypeConfigurationFile;
        }

        public bool ShouldCreateApiController()
        {
            return CreateEntity && CreateViewModel && CreateFormModel && CreateApiController;
        }

        public bool ShouldCreateUiController()
        {
            return ShouldCreateApiController() && CreateUiController;
        }
    }
}