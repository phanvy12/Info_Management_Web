using Info_Management_Web.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Info_Management_Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly AccountDAO _accountDAO = new AccountDAO();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
        public IActionResult OnPost(string username, string password) {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                TempData["Error"] = "Sai tài khoản hoặc mật khẩu!";
                return Page();
            }
            var acc = _accountDAO.login(username, password);
            if(acc == null)
            {
                TempData["Error"] = "Sai tài khoản hoặc mật khẩu!";
                return Page();
            }
            HttpContext.Session.SetString("USER", JsonSerializer.Serialize(acc));
            return RedirectToPage("/Home");
        }
    }
}
