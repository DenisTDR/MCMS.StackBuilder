using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MCMS.Controllers.Ui;
using MCMS.Display;
using MCMS.Display.Link;
using MCMS.Display.ModelDisplay;
using Microsoft.AspNetCore.Mvc;

namespace MCMS.StackBuilder.Stacks
{
    [Display(Name = "Stacks")]
    public class StacksAdminUiController : GenericModalAdminUiController<StackEntity, StackFormModel, StackViewModel,
        StacksAdminApiController>
    {
        public override IActionResult Create()
        {
            ViewBag.ModalDialogClasses = "modal-3xl";
            return base.Create();
        }

        public override Task<IActionResult> Edit(string id)
        {
            ViewBag.ModalDialogClasses = "modal-3xl";
            return base.Edit(id);
        }

        public override async Task<IndexPageConfig> GetIndexPageConfig()
        {
            var config = await base.GetIndexPageConfig();
            config.TableConfig.ItemActions.Add(
                new MRichLink("", typeof(StacksAdminUiController), nameof(GetGenerateLink)).WithTag("get-gen-link")
                    .AsButton("outline-info").WithModal().WithIconClasses("fas fa-arrow-right")
                    .WithValues(new {id = "ENTITY_ID"}));
            return config;
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetGenerateLink([FromRoute] string id)
        {
            var e = await Repo.GetOneOrThrow(id);
            var vm = Mapper.Map<StackViewModel>(e);
            var model = new DetailsViewModelT<StackViewModel>(vm, DetailsConfigService.GetDetailsFields());
            return View(model);
        }
    }
}