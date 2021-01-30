using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [TableColumn] public string Name { get; set; }
        public string PluralName { get; set; }

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