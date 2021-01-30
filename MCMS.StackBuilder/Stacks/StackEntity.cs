using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MCMS.Base.Data.Entities;
using MCMS.Base.Helpers;
using MCMS.StackBuilder.Stacks.SubModels;

namespace MCMS.StackBuilder.Stacks
{
    [Table("Stacks")]
    public class StackEntity : Entity
    {
        public string Token { get; set; }
        public string Name { get; set; }
        public string PluralName { get; set; }

        public string PropertiesSerialized
        {
            get => SerializablePropertyUtils.SerializeOrEmpty(Properties);
            set => Properties = SerializablePropertyUtils.DeserializeOrDefault<List<PropertyModel>>(value);
        }

        [NotMapped] public List<PropertyModel> Properties { get; set; }

        public string ConfigSerialized
        {
            get => SerializablePropertyUtils.SerializeOrEmpty(Config);
            set => Config = SerializablePropertyUtils.DeserializeOrDefault<StackConfigModel>(value);
        }

        [NotMapped] public StackConfigModel Config { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public string GetNamespace()
        {
            return "MCMS.StackGenerated";
        }

        public string GetDirectoryName()
        {
            if (Config.CreateDirectoryWithPluralName)
            {
                return PluralName;
            }

            return null;
        }
    }
}