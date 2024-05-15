using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Client.HttpDataClients.Interfaces;
using ProductCatalog.Client.Models;

namespace ProductCatalog.Client.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpProductDataClient _productDataClient;
        private readonly IHttpNbrbDataClient _nbrbDataClient;

        public ProductController(IHttpProductDataClient productDataClient, IHttpNbrbDataClient nbrbDataClient)
        {
            _productDataClient = productDataClient;
            _nbrbDataClient = nbrbDataClient;
        }

        public async Task<ActionResult> Index()
        {
            var jwt = Request.Cookies["jwt"];

            if (jwt is null)
                return RedirectToAction("Index", "Home");

            var products = await _productDataClient.GetProductsAsync(jwt);

            if (products is null)
                return View(new List<ProductModel>());

            return View(products);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateProductRequest request)
        {
            var jwt = Request.Cookies["jwt"];

            if (jwt is null)
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                var productId = await _productDataClient.CreateProductAsync(request, jwt);

                if (productId == 1)
                    return RedirectToAction("Index");
            }

            return View(request);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var jwt = Request.Cookies["jwt"];

            if (jwt is null)
                return RedirectToAction("Index", "Home");

            var product = await _productDataClient.GetProductByIdAsync(id, jwt);

            if (product is null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditConfirmed(UpdateProductRequest request)
        {
            var jwt = Request.Cookies["jwt"];

            if (jwt is null)
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                var result = await _productDataClient.UpdateProductAsync(request, jwt);

                if (result is true)
                    return RedirectToAction("Index", "Product");
            }

            return View();
        }

        public async Task<ActionResult> Delete(int id)
        {
            var jwt = Request.Cookies["jwt"];

            if (jwt is null)
                return RedirectToAction("Index", "Home");

            var products = await _productDataClient.GetProductsAsync(jwt);
            var product = products.FirstOrDefault(x => x.Id == id);

            if (product is null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var jwt = Request.Cookies["jwt"];

            if (jwt is null)
                return RedirectToAction("Index", "Home");

            await _productDataClient.DeleteProductByIdAsync(id, jwt);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetExchangeRate()
        {
            try
            {
                var exchangeRate = await _nbrbDataClient.GetExchangeRateAsync();

                return Ok(new { exchangeRate });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving exchange rate: {ex.Message}");
            }
        }
    }
}
