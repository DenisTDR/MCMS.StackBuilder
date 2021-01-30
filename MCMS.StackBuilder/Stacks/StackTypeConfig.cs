using MCMS.Base.Data.TypeConfig;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MCMS.StackBuilder.Stacks
{
    public class StackTypeConfig : EntityTypeConfiguration<StackEntity>
    {
        public override void Configure(EntityTypeBuilder<StackEntity> builder)
        {
            base.Configure(builder);
            builder.HasIndex(s => s.Token).IsUnique();
        }
    }
}