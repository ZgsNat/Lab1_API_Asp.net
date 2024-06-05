using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using System.Net.Http.Headers;
using NuGet.Protocol.Plugins;
using System.Text.Json;
using System.Text;

namespace ProductManagementWebClient.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient _httpClient = null;
        private string productApiUrl = "";
        public ProductsController()
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            productApiUrl = "http://localhost:5230/api/Products";
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage responseMessage = await _httpClient.GetAsync(productApiUrl);
            string strData = await responseMessage.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Product> list = JsonSerializer.Deserialize<List<Product>>(strData, option);
            return View(list);
        }
        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage responseMessage = await _httpClient.GetAsync("http://localhost:5230/api/Products/id?id=" + id);
            string strData = await responseMessage.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            Product p = JsonSerializer.Deserialize<Product>(strData, option);
            return View(p);
        }
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage responseMessage = await _httpClient.GetAsync("http://localhost:5230/api/Products/id?id=" + id);
            string strData = await responseMessage.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            Product p = JsonSerializer.Deserialize<Product>(strData, option);
            //
            HttpResponseMessage responseMessage1 = await _httpClient.GetAsync("http://localhost:5230/api/Categories");
            string strData1 = await responseMessage1.Content.ReadAsStringAsync();
            var option1 = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Category> list1 = JsonSerializer.Deserialize<List<Category>>(strData1, option1);
            int categoryId = p.CategoryId;

            // Create a SelectListItem list with a single item
            List<SelectListItem> categoryList = new List<SelectListItem>();
            foreach (var category in list1)
            {
                // Create a SelectListItem for each category
                SelectListItem item = new SelectListItem
                {
                    Value = category.CategoryId.ToString(),
                    Text = category.CategoryName,
                    Selected = category.CategoryId == categoryId // Set Selected property
                };

                // Add the SelectListItem to the list
                categoryList.Add(item);
            }

            // Assign the list to ViewBag.CategoryId
            ViewBag.CategoryId = categoryList;
            return View(p);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product editedProduct)
        {
            if (ModelState.IsValid)
            {
                // Serialize the editedProduct object to JSON
                var jsonContent = JsonSerializer.Serialize(editedProduct);

                // Create a StringContent instance with the JSON data
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Send a PUT request to the API endpoint with the updated product data
                HttpResponseMessage responseMessage = await _httpClient.PutAsync("http://localhost:5230/api/Products/id?id=" + editedProduct.ProductId, httpContent);

                if (responseMessage.IsSuccessStatusCode)
                {
                    // Product updated successfully
                    return RedirectToAction("Index"); // Redirect to the product list page or any other page
                }
                else
                {
                    // Handle the case where updating the product fails
                    ModelState.AddModelError("", "Failed to update the product. Please try again.");
                    return View(editedProduct);
                }
            }
            else
            {
                // Model validation failed, return the view with validation errors
                return View(editedProduct);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            // Send a DELETE request to the API endpoint to delete the product with the specified ID
            HttpResponseMessage responseMessage = await _httpClient.DeleteAsync("http://localhost:5230/api/Products/id?id=" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                // Product deleted successfully
                return RedirectToAction("Index"); // Redirect to the product list page or any other page
            }
            else
            {
                // Handle the case where deleting the product fails
                ModelState.AddModelError("", "Failed to delete the product. Please try again.");
                return RedirectToAction("Index"); // You can customize the error handling as needed
            }
        }
        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Get the product details from the API
            HttpResponseMessage responseMessage = await _httpClient.GetAsync("http://localhost:5230/api/Products/id?id=" + id);

            if (!responseMessage.IsSuccessStatusCode)
            {
                return NotFound();
            }

            string strData = await responseMessage.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            Product p = JsonSerializer.Deserialize<Product>(strData, option);

            if (p == null)
            {
                return NotFound();
            }

            return View(p);
        }
        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            HttpResponseMessage responseMessage1 = await _httpClient.GetAsync("http://localhost:5230/api/Categories");
            string strData1 = await responseMessage1.Content.ReadAsStringAsync();
            var option1 = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Category> list1 = JsonSerializer.Deserialize<List<Category>>(strData1, option1);

            // Create a SelectListItem list with a single item
            List<SelectListItem> categoryList = new List<SelectListItem>();
            foreach (var category in list1)
            {
                // Create a SelectListItem for each category
                SelectListItem item = new SelectListItem
                {
                    Value = category.CategoryId.ToString(),
                    Text = category.CategoryName
                };

                // Add the SelectListItem to the list
                categoryList.Add(item);
            }

            // Assign the list to ViewBag.CategoryId
            ViewBag.CategoryId = categoryList;
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product newProduct)
        {
            if (ModelState.IsValid)
            {
                // Serialize the newProduct object to JSON
                var jsonContent = JsonSerializer.Serialize(newProduct);

                // Create a StringContent instance with the JSON data
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Send a POST request to the API endpoint to create the new product
                HttpResponseMessage responseMessage = await _httpClient.PostAsync("http://localhost:5230/api/Products", httpContent);

                if (responseMessage.IsSuccessStatusCode)
                {
                    // Product created successfully
                    return RedirectToAction("Index"); // Redirect to the product list page or any other page
                }
                else
                {
                    // Handle the case where creating the product fails
                    ModelState.AddModelError("", "Failed to create the product. Please try again.");
                    return View(newProduct);
                }
            }
            else
            {
                // Model validation failed, return the view with validation errors
                return View(newProduct);
            }
        }



    }
}
