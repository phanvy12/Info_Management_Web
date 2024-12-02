using Info_Management_Web.DAO;
using Info_Management_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Info_Management_Web.Pages
{
    public class HomeModel : PageModel
    {
        public Account CurAccount { get; set; }
        public IActionResult OnGet()
        {
            var accJson = HttpContext.Session.GetString("USER");
            if (string.IsNullOrEmpty(accJson))
            {
                return Redirect("/Index");
            }
            CurAccount = JsonSerializer.Deserialize<Account>(accJson);
            if(CurAccount == null)
            {
                return Redirect("/Index");
            }
            return Page();
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Clear();
            return Redirect("/Index");
        }
    }
}
