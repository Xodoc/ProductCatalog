using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Client.HttpDataClients.Interfaces;
using ProductCatalog.Client.Models;

namespace ProductCatalog.Client.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpUserDataClient _userDataClient;

        public UserController(IHttpUserDataClient userDataClient)
        {
            _userDataClient = userDataClient;
        }

        public async Task<ActionResult> Index()
        {
            var jwt = Request.Cookies["jwt"];

            if (jwt is null)
                return RedirectToAction("Index", "Home");

            var users = await _userDataClient.GetUsersAsync(jwt);

            if (users is null)
                return RedirectToAction("Index", "Product");

            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateUserRequest request)
        {
            var jwt = Request.Cookies["jwt"];

            if (jwt is null)
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                var userId = await _userDataClient.CreateUserAsync(request, jwt);

                if (!string.IsNullOrEmpty(userId))
                    return RedirectToAction("Index");
            }

            return View(request);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            var jwt = Request.Cookies["jwt"];

            if (jwt is null)
                return RedirectToAction("Index", "Home");

            var users = await _userDataClient.GetUsersAsync(jwt); // лень делать еще одину точку
            var user = users.FirstOrDefault(x => x.Id == id);

            if (user is null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, string newPassword)
        {
            var jwt = Request.Cookies["jwt"];

            if (jwt is null)
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                var result = await _userDataClient.ChangeUserPasswordAsync(id, newPassword, jwt);

                if (result is true)
                    return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<ActionResult> Delete(string id)
        {
            var jwt = Request.Cookies["jwt"];

            if (jwt is null)
                return RedirectToAction("Index", "Home");

            var users = await _userDataClient.GetUsersAsync(jwt); // лень делать еще одину точку
            var user = users.FirstOrDefault(x => x.Id == id);

            if (user == null)
                return NotFound();


            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            var jwt = Request.Cookies["jwt"];

            if (jwt is null)
                return RedirectToAction("Index", "Home");

            await _userDataClient.DeleteUserByIdAsync(id, jwt);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Block(string id)
        {
            var jwt = Request.Cookies["jwt"];

            if (jwt is null)
                return RedirectToAction("Index", "Home");

            var users = await _userDataClient.GetUsersAsync(jwt); // лень делать еще одину точку
            var user = users.FirstOrDefault(x => x.Id == id);

            if (user == null)
                return NotFound();


            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BlockConfirmed(string id)
        {
            var jwt = Request.Cookies["jwt"];

            if (jwt is null)
                return RedirectToAction("Index", "Home");

            await _userDataClient.BlockUserByIdAsync(id, jwt);

            return RedirectToAction("Index");
        }
    }
}
