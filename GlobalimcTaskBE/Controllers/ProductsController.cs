using GlobalimcTaskBE.Data;
using GlobalimcTaskBE.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace GlobalimcTaskBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]

    public class ProductsController : ControllerBase
    {
        private readonly GlobalImcDatabaseContext _context;

        public ProductsController(GlobalImcDatabaseContext context)
        {
            _context = context;
        }


        [HttpGet("Details/{id}")]
        // GET: Product/Details/5  
        public Product Details(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var product =  _context.Product.FirstOrDefault(m => m.Id == id.Value);
            if (product == null)
            {
                return null;
            }

            return product;
        }


        // POST: Product/Create  
        [HttpPost("Create")]
        public int Create([Bind("VendorId,Title,Description,Price,Image,DietaryFlag,ViewsCount")] Product product)
        {
            //Product product = new Product();
            if (ModelState.IsValid)
            {
                _context.Add(product);
                 _context.SaveChanges();
                return product.Id;
            }
            return -1;
        }


        // POST: Product/Edit/5  
        [HttpPost("Edit/{id}")]
        public bool Edit(int id, [Bind("VendorId,Title,Description,Price,Image,DietaryFlag,ViewsCount")] Product product)
        {

            product.Id = id;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    _context.SaveChanges();
                }
                catch (Exception )
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        // POST: Product/Delete/5  
        [HttpPost("Delete/{id}")]
        public bool Delete(int id)
        {
            var product = Details(id);
            if (product != null)
            {
                try
                {
                    _context.Remove(product);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        // GET: Product/Search/Name
        [HttpGet("Search")]
        public Product[] Search(string SearchText="")
        {
            return string.IsNullOrEmpty(SearchText)? _context.Product.ToArray(): _context.Product.Where(x => x.Title.ToLower().Contains(SearchText.ToLower()) ||
            x.Description.ToLower().Contains(SearchText.ToLower())).ToArray();

        }

        [HttpPost("Upload"), DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
