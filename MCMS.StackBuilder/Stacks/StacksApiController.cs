using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Tar;
using MCMS.Base.Attributes;
using MCMS.Base.Controllers.Api;
using MCMS.Base.Data;
using MCMS.Base.Exceptions;
using MCMS.Base.Extensions;
using MCMS.StackBuilder.Generator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace MCMS.StackBuilder.Stacks
{
    public class StacksApiController : BaseApiController
    {
        private IRepository<StackEntity> Repo => ServiceProvider.GetRepo<StackEntity>();

        private StackCodeGenerator StackCodeGenerator =>
            ServiceProvider.GetRequiredService<StackCodeGenerator>();

        [HttpGet]
        [ApiRoute("{token}/json")]
        public async Task<object> GenerateJson([FromRoute] [Required] string token)
        {
            var stack = await GetStackByToken(token);
            var genResult = await StackCodeGenerator.GenerateClasses(stack);
            return Ok(genResult);
        }

        [HttpGet]
        [ApiRoute("{token}/text")]
        [Produces("text/plain")]
        public async Task<object> GeneratePlainText([FromRoute] [Required] string token)
        {
            var stack = await GetStackByToken(token);
            var genResult = await StackCodeGenerator.GenerateClasses(stack);

            var str = string.Join("\n\n\n",
                genResult.Select(kvp => "// " + kvp.Key + ".cs\n" + kvp.Value));
            return str;
        }

        [HttpGet]
        [ApiRoute("{token}/tgz")]
        public async Task<IActionResult> GetGzippedArchive([FromRoute] [Required] string token)
        {
            var stack = await GetStackByToken(token);
            var genResult = await StackCodeGenerator.GenerateClasses(stack);

            var ms = new MemoryStream();

            ArchiveHelper.WriteGzippedTarStream(ms, genResult, stack.GetDirectoryName());

            ms.Position = 0;
            return File(ms, "application/x-gtar", stack.Name + "-stack.tgz");
        }

        [HttpGet]
        [ApiRoute("{token}/tar")]
        public async Task<IActionResult> GetTarArchive([FromRoute] [Required] string token)
        {
            var stack = await GetStackByToken(token);
            var genResult = await StackCodeGenerator.GenerateClasses(stack);

            var ms = new MemoryStream();

            ArchiveHelper.WriteTarStream(ms, genResult, stack.GetDirectoryName());

            ms.Position = 0;
            return File(ms, "application/x-tar", stack.Name + "-stack.tar");
        }

        private async Task<StackEntity> GetStackByToken(string token)
        {
            var stack = await Repo.GetOne(st => st.Token == token);
            if (stack == null)
            {
                throw new KnownException("Invalid token", 404);
            }

            return stack;
        }
    }
}