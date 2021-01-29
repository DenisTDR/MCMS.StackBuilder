using System.Collections.Generic;
using MCMS.Admin.Users;
using MCMS.Base.Builder;
using MCMS.Common.Translations.Translations;
using MCMS.Data;
using MCMS.Display.Link;
using MCMS.Display.Menu;
using MCMS.StackBuilder.Data;
using Microsoft.Extensions.DependencyInjection;

namespace MCMS.StackBuilder
{
    public class StackBuilderSpecifications : MSpecifications
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            services.AddScoped<BaseDbContext, ApplicationDbContext>();
            services.AddOptions<SiteConfig>().Configure(c =>
            {
                c.SiteName = "Stack Builder - MCMS";
                c.SiteCopyright = "Copyright &copy; TDR 2021";
            });
            services.Configure<MenuConfig>(ConfigureMenu);
        }

        private void ConfigureMenu(MenuConfig config)
        {
            config.Items.Add(new MenuSection
            {
                Name = "Admin",
                IsCollapsed = true,
                Items = new List<IMenuItem>
                {
                    new MenuLink("Users", typeof(AdminUsersController)).WithIconClasses("fas fa-users"),
                    new MenuLink("Texts / Translations", typeof(TranslationsController))
                        .WithIconClasses("fas fa-globe"),
                    // new MenuLink("Fișiere", typeof(FilesController)).WithIconClasses("fas fa-copy"),
                    new MenuLink("Swagger", "~/api/docs").WithIconClasses("fas fa-file-contract").WithTarget("_blank"),
                }
            }.RequiresRoles("Admin"));

            config.Items.Add(new MenuSection
            {
                Name = "Armour",
                IsCollapsed = false,
                Items = new List<IMenuItem>
                {
                    // new MenuLink("Profile", typeof(ProfilesUiController)).WithIconClasses("fas fa-users"),
                }
            }.RequiresRoles("Moderator"));
        }
    }
}