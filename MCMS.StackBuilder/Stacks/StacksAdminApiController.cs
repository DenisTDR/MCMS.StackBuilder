using System.Threading.Tasks;
using MCMS.Base.Helpers;
using MCMS.Controllers.Api;

namespace MCMS.StackBuilder.Stacks
{
    public class StacksAdminApiController : CrudAdminApiController<StackEntity, StackFormModel, StackViewModel>
    {
        protected override Task OnCreating(StackEntity e)
        {
            e.Token = Utils.GenerateRandomHexString(15);
            return base.OnCreating(e);
        }
    }
}