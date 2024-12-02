using Info_Management_Web.DAO;
using Info_Management_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Info_Management_Web.Pages.Management.Accounts
{
    public class IndexModel : PageModel
    {
        private AccountDAO accountDAO = new AccountDAO();
        public List<Account> Accounts { get; set; }

        public string? Role { get; set; }

        public string? Username { get; set; }

        public string? Fullname { get; set; }

        public string? Password { get; set; }

        public Account EditAccount { get; set; }

        public int PageSize { get; set; } = 10;

        public int TotalPages { get; set; } = 1;

        public int PageIndex { get; set; } = 1;

        public int NextPage { get; set; } = 1;

        public int PreviousPage { get; set; } = 1;

        public async Task<IActionResult> OnGet(int? accountId = null, string? search = null, int? pageNumber = 1)
        {
            var accounts = accountDAO.GetAllAccounts();
            if (accounts == null)
            {
                accounts = new List<Account>();
            } else
            {
                if(search != null)
                {
                    accounts = accounts.Where(a => a.Fullname.ToLower().Contains(search.ToLower()) || a.Username.ToLower().Contains(search.ToLower())).ToList();
                }
                if (pageNumber != null)
                {
                    PageIndex = pageNumber.Value;
                    int totalAmount = accounts.Count();
                    TotalPages = (int)Math.Ceiling((double)totalAmount / (double)PageSize);
                    accounts = accounts.Skip((pageNumber.Value - 1) * PageSize)
                                               .Take(PageSize)
                                               .ToList();
                    int nextPage = pageNumber.Value + 1;
                    int previousPage = pageNumber.Value - 1;
                    if (nextPage > TotalPages)
                    {
                        nextPage = TotalPages;
                    }
                    if(previousPage <= 0)
                    {
                        previousPage = 1;
                    }
                    NextPage = nextPage;
                    PreviousPage = previousPage;
                }
            }
            Accounts = accounts;
            if(accountId != null)
            {
                EditAccount = accountDAO.GetAccountById(accountId.Value);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostCreateNewAccount(string fullName, string username, string password, string role)
        {
            await OnGet();
            if(string.IsNullOrEmpty(fullName))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập họ tên!";
                return Page();
            }
            if(string.IsNullOrEmpty(username))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập tên đăng nhập!";
                return Page();
            }
            if(string.IsNullOrEmpty(password))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập mật khẩu!";
                return Page();
            }
            if(string.IsNullOrEmpty(role))
            {
                TempData["ErrorMessage"] = "Vui lòng chọn quyền!";
                return Page();
            }
            UserRole userRole = UserRole.Student;
            if(role == UserRole.Admin.ToString())
            {
                userRole = UserRole.Admin;
            } else if(role == UserRole.Teacher.ToString())
            {
                userRole = UserRole.Teacher;
            }
            var existUsername = accountDAO.GetAccountByUsername(username);
            if(existUsername != null)
            {
                TempData["ErrorMessage"] = "Tên đăng nhập đã tồn tại!";
                return Page();
            }
            try
            {
                accountDAO.AddAccount(new Account()
                {
                    Fullname = fullName,
                    Username = username,
                    Password = password,
                    Role = (int)userRole,
                });
                TempData["SuccessMessage"] = "Tạo tài khoản thành công!";
                return Redirect("/Management/Accounts/Index");
            } catch(Exception ex)
            {

            }
            TempData["ErrorMessage"] = "Lỗi kết nối cơ sở dữ liệu!";
            return Page();
        }

        public async Task<IActionResult> OnPostEditAccount(int id, string fullName, string username, string password, string role)
        {
            await OnGet();
            if (id < 0)
            {
                TempData["ErrorMessage"] = "Vui lòng chọn tài khoản!";
                return Page();
            }
            if (string.IsNullOrEmpty(fullName))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập họ tên!";
                return Page();
            }
            if (string.IsNullOrEmpty(username))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập tên đăng nhập!";
                return Page();
            }
            if (string.IsNullOrEmpty(password))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập mật khẩu!";
                return Page();
            }
            if (string.IsNullOrEmpty(role))
            {
                TempData["ErrorMessage"] = "Vui lòng chọn quyền!";
                return Page();
            }
            UserRole userRole = UserRole.Student;
            if (role == UserRole.Admin.ToString())
            {
                userRole = UserRole.Admin;
            }
            else if (role == UserRole.Teacher.ToString())
            {
                userRole = UserRole.Teacher;
            }
            var existAccount = accountDAO.GetAccountById(id);
            if(existAccount == null)
            {
                TempData["ErrorMessage"] = "Không tồn tại tài khoản này!";
                return Page();
            }
            var existUsername = accountDAO.GetAccountByUsername(username);
            if (existUsername != null)
            {
                if(existUsername.Id != id)
                {
                    TempData["ErrorMessage"] = "Tên đăng nhập đã tồn tại!";
                    return Page();
                }
            }
            try
            {
                if(password != existAccount.Password)
                {
                    existAccount.Password = password;
                }
                if (fullName != existAccount.Fullname)
                {
                    existAccount.Fullname = fullName;
                }
                if ((int)userRole != existAccount.Role)
                {
                    existAccount.Role = (int)userRole;
                }
                accountDAO.UpdateAccount(existAccount);
                TempData["SuccessMessage"] = "Cập nhật tài khoản thành công!";
                return Redirect("/Management/Accounts/Index");
            }
            catch (Exception ex)
            {

            }
            TempData["ErrorMessage"] = "Lỗi kết nối cơ sở dữ liệu!";
            return Page();
        }

        public async Task<IActionResult> OnGetDelete(int id)
        {
            var existAccount = accountDAO.GetAccountById(id);
            if(existAccount == null)
            {
                TempData["ErrorMessage"] = "Không tồn tại tài khoản này!";
                return Page();
            }
            accountDAO.DeleteAccount(id);
            TempData["SuccessMessage"] = "Không tồn tại tài khoản này!";
            return Redirect("/Management/Accounts/Index");
        }
    }
}
