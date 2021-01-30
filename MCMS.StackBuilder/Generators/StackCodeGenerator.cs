using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;
using MCMS.Controllers.Api;
using MCMS.StackBuilder.Stacks;
using MCMS.StackBuilder.Stacks.SubModels;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Razor.Templating.Core;

namespace MCMS.StackBuilder.Generators
{
    public class StackCodeGenerator
    {
        public async Task<Dictionary<string, string>> GenerateClasses(StackEntity stack)
        {
            var dict = new Dictionary<string, string>();

            var config = stack.Config;
            if (config.CreateEntity)
            {
                dict[stack.GetNameFor(ModelType.Entity)] = await GenerateEntity(stack);
            }

            if (config.CreateFormModel)
            {
                dict[stack.GetNameFor(ModelType.FormModel)] = await GenerateFormModel(stack);
            }

            if (config.CreateViewModel)
            {
                dict[stack.GetNameFor(ModelType.ViewModel)] = await GenerateViewModel(stack);
            }

            if (config.ShouldCreateApiController())
            {
                dict[stack.GetNameFor(ModelType.AdminApiController)] = await GenerateAdminApiController(stack);
            }

            if (config.ShouldCreateUiController())
            {
                dict[stack.GetNameFor(ModelType.UiController)] = await GenerateUiController(stack);
            }

            if (stack.Config.ShouldCreateEntityTypeConfiguration())
            {
                dict[stack.GetNameFor(ModelType.Entity) + "TypeConfig"] = await GenerateTypeConfig(stack);
            }

            foreach (var propertyModel in stack.Properties.Where(p => p.Type == PropertyType.NewEnum))
            {
                dict[propertyModel.EnumName] = await GenerateEnum(stack, propertyModel);
            }

            return dict;
        }


        private async Task<string> GenerateEntity(StackEntity stack)
        {
            return await RenderTemplateAndIndent("EntityTemplate", stack);
        }

        private async Task<string> GenerateFormModel(StackEntity stack)
        {
            return await RenderTemplateAndIndent("FormModelTemplate", stack);
        }

        private async Task<string> GenerateViewModel(StackEntity stack)
        {
            return await RenderTemplateAndIndent("ViewModelTemplate", stack);
        }

        private async Task<string> GenerateUiController(StackEntity stack)
        {
            return await RenderTemplateAndIndent("AdminUiControllerTemplate", stack);
        }

        private async Task<string> GenerateAdminApiController(StackEntity stack)
        {
            return await RenderTemplateAndIndent("AdminApiControllerTemplate", stack);
        }

        private async Task<string> GenerateTypeConfig(StackEntity stack)
        {
            return await RenderTemplateAndIndent("EntityTypeConfigurationTemplate", stack);
        }

        private async Task<string> GenerateEnum(StackEntity stack, PropertyModel property)
        {
            return await RenderTemplateAndIndent("EnumTemplate", new EnumTemplateModel(stack, property));
        }

        private async Task<string> RenderTemplateAndIndent(string templateName, object model)
        {
            var str = await RazorTemplateEngine.RenderAsync(templateName, model);
            str = (await CSharpSyntaxTree.ParseText(str).GetRootAsync()).NormalizeWhitespace().ToFullString();
            return str;
        }
    }
}