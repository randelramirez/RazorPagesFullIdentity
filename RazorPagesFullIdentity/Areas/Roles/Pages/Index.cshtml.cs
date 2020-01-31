using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesFullIdentity.Areas.Roles.Pages
{
    public class IndexModel : PageModel
    {
        public string ReturnUrl { get; set; }
        public RoleManager<IdentityRole> roleManager { get; set; }

        [BindProperty]
        public RoleIndexViewModel viewModel { get; set; }

        public IndexModel(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
            this.viewModel = new RoleIndexViewModel();
        }



        public IActionResult OnGetAsync(string returnUrl = null)
        {
            viewModel.Roles = this.roleManager.Roles.ToList();
            ReturnUrl = returnUrl;
            return Page();
        }
    }

    public class RoleIndexViewModel
    {
        public IList<IdentityRole> Roles { get; set; }
    }
}
