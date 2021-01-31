using System.ComponentModel;
using System.Linq;
using MCMS.Base.Data.FormModels;
using MCMS.Base.SwaggerFormly.Formly;
using MCMS.Base.SwaggerFormly.Formly.Fields;

namespace MCMS.StackBuilder.Stacks.SubModels
{
    public class StackConfigModel : IFormModel
    {
        [FormlyField(ClassName = "col-4 d-flex", DefaultValue = true)]
        [DisplayName("Create Directory")]
        public bool CreateDirectoryWithPluralName { get; set; }

        [FormlyField(ClassName = "col-8 d-flex", DefaultValue = false)]
        [FormlyFieldProp("templateOptions.disabled", "!model.createEntity", "expressionProperties")]
        [DisplayName("Create TypeConfiguration")]
        public bool CreateEntityTypeConfigurationFile { get; set; }

        [FormlyField(ClassName = "col-4 d-flex", DefaultValue = true)]
        [DisplayName("Entity")]
        public bool CreateEntity { get; set; }

        [FormlyField(ClassName = "col-4 d-flex", DefaultValue = true)]
        [DisplayName("ViewModel")]
        public bool CreateViewModel { get; set; }

        [FormlyField(ClassName = "col-4 d-flex", DefaultValue = true)]
        [DisplayName("FormModel")]
        public bool CreateFormModel { get; set; }


        [FormlyField(ClassName = "col-4 d-flex", DefaultValue = true)]
        [FormlyFieldProp("templateOptions.disabled",
            "!model.createEntity || !model.createViewModel || !model.createFormModel", "expressionProperties")]
        [DisplayName("AdminApiController")]
        public bool CreateApiController { get; set; }

        [FormlyField(ClassName = "col-8 d-flex", DefaultValue = true)]
        [FormlyFieldProp("templateOptions.disabled",
            "!model.createApiController || !model.createEntity || !model.createViewModel || !model.createFormModel",
            "expressionProperties")]
        [DisplayName("UiController")]
        public bool CreateUiController { get; set; }

        public override string ToString()
        {
            return string.Join(", ",
                new[]
                {
                    CreateDirectoryWithPluralName ? "Directory" : null,
                    CreateEntity ? "Entity" : null,
                    CreateViewModel ? "ViewModel" : null,
                    CreateFormModel ? "FormModel" : null,
                    ShouldCreateEntityTypeConfiguration() ? "TypeConfig" : null,
                    ShouldCreateApiController() ? "AdminApiController" : null,
                    ShouldCreateUiController() ? "UiController" : null,
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