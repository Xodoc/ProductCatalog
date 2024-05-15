using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Client.HttpDataClients.Interfaces;
using ProductCatalog.Client.Models;

namespace ProductCatalog.Client.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IHttpCategoryDataClient _categoryDataClient;

        public CategoryController(IHttpCategoryDataClient categoryDataClient)
        {
            _categoryDataClient = categoryDataClient;
        }

        public async Task<ActionResult> Index()
        {
            var jwt = Request.Cookies["jwt"];

            if (jwt is null)
                return RedirectToAction("Index", "Home");

            var categories = await _categoryDataClient.GetCategoriesAsync(jwt);

            if (categories is null)
                return View(new List<CategoryModel>());

            return View(categories);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateCategoryRequest request)
        {
            var jwt = Request.Cookies["jwt"];

            if (jwt is null)
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                var categoryId = await _categoryDataClient.CreateCategoryAsync(request.Name, jwt);

                if (categoryId == 1)
                    return RedirectToAction("Index");
            }

            return View(request);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var jwt = Request.Cookies["jwt"];

            if (jwt is null)
                return RedirectToAction("Index", "Home");

            var categories = await _categoryDataClient.GetCategoriesAsync(jwt);
            var category = categories.FirstOrDefault(x => x.Id == id);

            if (category is null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpdateCategoryRequest request)
        {
            var jwt = Request.Cookies["jwt"];

            if (jwt is null)
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                var result = await _categoryDataClient.UpdateCategoryAsync(request, jwt);

                if (result is true)
                    return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<ActionResult> Delete(int id)
        {
            var jwt = Request.Cookies["jwt"];

            if (jwt is null)
                return RedirectToAction("Index", "Home");

            var categories = await _categoryDataClient.GetCategoriesAsync(jwt);
            var category = categories.FirstOrDefault(x => x.Id == id);

            if (category is null)
                return NotFound();


            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var jwt = Request.Cookies["jwt"];

            if (jwt is null)
                return RedirectToAction("Index", "Home");

            await _categoryDataClient.DeleteCategoryByIdAsync(id, jwt);

            return RedirectToAction("Index");
        }
    }
}
