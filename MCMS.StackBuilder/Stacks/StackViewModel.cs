using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MCMS.Base.Attributes.JsonConverters;
using MCMS.Base.Data.ViewModels;
using MCMS.Base.Display.ModelDisplay;
using MCMS.Base.Display.ModelDisplay.Attributes;
using MCMS.StackBuilder.Stacks.SubModels;
using Newtonsoft.Json;

namespace MCMS.StackBuilder.Stacks
{
    [Display(Name = "Stack")]
    public class StackViewModel : ViewModel
    {
        [TableColumn] public string Token { get; set; }
        public string Name { get; set; }

        [TableColumn(DbColumn = "Name", DbFuncFormat = "MDbFunctions.Concat(x.RootNamespace, '.', {0})")]
        [DisplayName("Name")]
        public string FullName => string.Join(".",
            new[] {RootNamespace, Config.CreateDirectoryWithPluralName ? PluralName : null, Name}
                .Where(s => !string.IsNullOrEmpty(s)));

        public string PluralName { get; set; }
        public string RootNamespace { get; set; }

        [TableColumn(Searchable = ServerClient.Client, Orderable = ServerClient.Client)]
        [JsonConverter(typeof(ToStringJsonConverter))]
        public StackConfigModel Config { get; set; }

        [TableColumn(Searchable = ServerClient.Client, Orderable = ServerClient.Client)]
        [JsonConverter(typeof(ToStringJsonConverter))]
        [DisplayFormat]
        public List<PropertyModel> Properties { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}