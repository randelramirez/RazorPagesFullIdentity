using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RazorPagesFullIdentity.Areas.Identity.Data;
using RazorPagesFullIdentity.Data;

[assembly: HostingStartup(typeof(RazorPagesFullIdentity.Areas.Identity.IdentityHostingStartup))]
namespace RazorPagesFullIdentity.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<UserManagementContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("UserManagementContextConnection")));

                //services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //    .AddEntityFrameworkStores<UserManagementContext>();

                // We need  services.AddIdentity<IdentityUser, IdentityRole>() if we're going to use the Authorize(Role="RoleName") functionality
                services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<UserManagementContext>()
                    .AddDefaultTokenProviders();

                // use for sending emails ex. forgot password link
                services.AddSingleton<IEmailSender, EmailSender>();

            });
        }
    }

    public class EmailSender : IEmailSender
    {
        public async System.Threading.Tasks.Task SendEmailAsync(string email, string subject, string message)
        {
            await System.IO.File.WriteAllTextAsync("resetLink.txt", message);
        }
    }
}