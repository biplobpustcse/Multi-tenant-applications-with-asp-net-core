using Microsoft.AspNetCore.Mvc;
using MultiTenantApp.DbContexts;
using MultiTenantApp.Models;
using MultiTenantApp.Services;
using System.Diagnostics;
using System.Security.Claims;

namespace MultiTenantApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAppUserService appUserService;
        private readonly ITenantService tenantService;

        public HomeController(IAppUserService appUserService,
            ITenantService tenantService)
        {
            this.appUserService = appUserService;
            this.tenantService = tenantService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Signin(string emailid="")
        {
            ViewBag.Email = emailid;
            return View();
        }

        [HttpPost]
        public IActionResult Signin(Signin model)
        {
            // checking model state
            if (ModelState.IsValid)
            {
                // checking email at first time
                if (model.Password is null)
                {
                    // retrieve tenant information by user email
                    var result = this.appUserService.GetTenantByEmail(model.Email);
                    // if valid email then redirect for password
                    if (result is not null) return Redirect(result + "?emailid=" + model.Email);
                    else // if email is invalid then clear Email-ViewBag to stay same page and get again email
                    {
                        ViewBag.Email = string.Empty;
                        ViewBag.Error = "Provide valid email";
                    }
                }
                else // this block for password verification, when user provide password to signin
                {
                    var result = this.appUserService.Signin(model);
                    if (result is null) // if password is wrong then again provide valid password
                    {
                        ViewBag.Email = model.Email;
                        ViewBag.Error = "Provide valid password";
                    }
                    else return Redirect(result); // if password is valid then portal will open for user access
                }
            }
            else ViewBag.Email = ""; // if email is invalid then clear Email-ViewBag to stay same page and get again email
            return View();
        }

        
        public IActionResult Logout()
        {
            return Redirect("http://localhost:5057");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}