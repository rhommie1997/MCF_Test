using MCF_TestWeb.Services.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using MCF_TestWeb.Models;
using MCF_TestWeb.ViewModels;

namespace MCF_TestWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService loginService;

        public LoginController(ILoginService _ls)
        {
            loginService = _ls;
        }

        public IActionResult Index()
        {
            ClaimsPrincipal cUser = HttpContext.User;

            if (cUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserLoginViewModel user)
        {
            ResponseDto? result = await loginService.GetResult(user);



            if (result != null)
            {
                if (result.IsSuccess)
                {
                    List<Claim> claims = new List<Claim>();

                    if (result.Result != null)
                    {
                        UserViewModel uvm = JsonConvert.DeserializeObject<UserViewModel>(Convert.ToString(result.Result));
                        claims.Add(new Claim("user_name", uvm?.user_name ?? ""));
                        claims.Add(new Claim("password", uvm.password ?? ""));
                        claims.Add(new Claim("isActive", uvm.isActive.ToString() ?? ""));
                        claims.Add(new Claim("user_id", uvm.user_id.ToString() ?? ""));
                        claims.Add(new Claim("token", uvm.Token ?? ""));
                    }

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                    //List<Claim> myClaims = (List<Claim>)claimsIdentity.Claims;

                    //Console.WriteLine("Ini " + claimsIdentity.Claims);
                    //Console.WriteLine("Ini " + myClaims[1].Value);

                    AuthenticationProperties proper = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = false
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), proper);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["ValidateMessage"] = result.Message;
                    return View();
                }


            }
            else
            {
                ViewData["ValidateMessage"] = "Login Failed !!";
                return View();
            }
        }
    }
}
