using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Client.HttpDataClients.Interfaces;
using ProductCatalog.Client.Models;
using System.Diagnostics;

namespace ProductCatalog.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpAuthDataClient _httpAuthDataClient;

        public HomeController(IHttpAuthDataClient httpAuthDataClient)
        {
            _httpAuthDataClient = httpAuthDataClient;
        }

        public IActionResult Index()
        {
            var jwt = Request.Cookies["jwt"];

            if (!string.IsNullOrEmpty(jwt))
                return RedirectToAction("Products");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthRequest request)
        {
            try
            {
                var jwt = await _httpAuthDataClient.LoginAsync(request);

                Response.Cookies.Append("jwt", jwt, new() { Expires = DateTime.Now.AddHours(24) });

                return RedirectToAction("Users", "Home");
            }
            catch
            {
                TempData["msg"] = "<script>alert('Something went wrong');</script>";

                return RedirectToAction("Index");
            }
        }

        public IActionResult Users()
        {
            var jwt = Request.Cookies["jwt"];

            if (jwt is null)
            {
                TempData["msg"] = "<script>alert('You are not authorize');</script>";

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "User");
        }

        public IActionResult Products()
        {
            var jwt = Request.Cookies["jwt"];

            if (jwt is null)
            {
                TempData["msg"] = "<script>alert('You are not authorize');</script>";

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Product");
        }

        public IActionResult Categories()
        {
            var jwt = Request.Cookies["jwt"];

            if (jwt is null)
            {
                TempData["msg"] = "<script>alert('You are not authorize');</script>";

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Category");
        }

        public IActionResult Logout()
        {
            var jwt = Request.Cookies["jwt"];

            if (jwt is null)
            {
                TempData["msg"] = "<script>alert('🤡');</script>";

                return RedirectToAction("Index", "Home");
            }

            Response.Cookies.Delete("jwt");

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
