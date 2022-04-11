using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using eStore.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using eStore.Services;
using Microsoft.AspNetCore.Hosting;
using eStore.ViewModels;
using System.Threading.Tasks;
using System.IO;

namespace eStore.Controllers
{
    public class ProductController : Controller
    {
        IRepositoryIntid<Product> prodRepo;
        IRepositoryIntid<Category> categoryRepo;
        IProductinCateg prodcat;
        private readonly IWebHostEnvironment webhost;
        public ProductController(IRepositoryIntid<Product> _prodRepo,
            IRepositoryIntid<Category> _categoryRepo, IProductinCateg _prodcat,
            IWebHostEnvironment _webhost)
        {
            prodRepo = _prodRepo;
            categoryRepo = _categoryRepo;
            prodcat = _prodcat;
            webhost = _webhost;
        }



        // GET: ProductController
        public ActionResult Index()
        {
            var products = prodRepo.GetAll();
            List<Category> prods = categoryRepo.GetAll();
            ViewData["prods"] = prods;
            return View(products);
        }
        // GET: ProductController/Details/5
        public IActionResult Details(int id)
        {
            var product = prodRepo.GetByID(id);
            return View(product);
        }
        
        [Authorize(Roles = "admin")]
        #region Create
        // GET: ProductsController/Create
        public ActionResult Create()
        {
            List<Category> prods = categoryRepo.GetAll();
            ViewData["prods"] = prods;
            return View(new ProductInfo());
        }
        
        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductInfo model)
        {
            var savimg1 = Path.Combine(webhost.WebRootPath, "Images", model.img1.FileName);
            string imgext1 = Path.GetExtension(model.img1.FileName);
            var savimg2 = Path.Combine(webhost.WebRootPath, "Images", model.img2.FileName);
            string imgext2 = Path.GetExtension(model.img2.FileName);
            var savimg3 = Path.Combine(webhost.WebRootPath, "Images", model.img3.FileName);
            string imgext3 = Path.GetExtension(model.img3.FileName);
            if (imgext1 == ".png" || imgext1 == ".jpg")
            {
                using (var uploadimg1 = new FileStream(savimg1, FileMode.Create))
                {
                    await model.img1.CopyToAsync(uploadimg1);
                }
                using (var uploadimg2 = new FileStream(savimg2, FileMode.Create))
                {
                    await model.img2.CopyToAsync(uploadimg2);
                }
                using (var uploadimg3 = new FileStream(savimg3, FileMode.Create))
                {
                    await model.img3.CopyToAsync(uploadimg3);
                }
                Product product = new Product
                {
                    Image1 = model.img1.FileName,
                    Image2 = model.img2.FileName,
                    Image3 = model.img3.FileName,
                    Price = model.price,
                    Cat_id = model.Categoryid,
                    Name = model.name,
                };

                prodRepo.Insert(product);
            }
            return RedirectToAction("index");
        }

        #endregion

        [Authorize(Roles = "admin")]
        #region Edit
        // GET: ProductsController/Edit/5
        public ActionResult Edit(int id)
        {
            Product product = prodRepo.GetByID(id);
            List<Category> cags = categoryRepo.GetAll();
            ViewData["cags"] = cags;
            return View(product);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            if (ModelState.IsValid)
            {
                prodRepo.Update(id, product);
                return RedirectToAction("Index");
            }
            return View("Edit", product);
        }

        // GET: ProductsController/Delete/5

        #endregion

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            var product = prodRepo.GetByID(id);
            if (product != null)
            {
                prodRepo.Delete(id);
            }
            return RedirectToAction("index");
        }


        [Authorize]
        public IActionResult prodInfo(int id)
        {
            Product product = prodRepo.GetByID(id);
            ProductInfo pro = new ProductInfo();

            pro.name = product.Name;
            pro.price = product.Price;
            pro.Categoryid = product.Cat_id;
            return View(pro);
        }

        public IActionResult prods(int id)
        {
            var prodct = prodRepo.GetByID(id);
            return PartialView("_prds", prodct);
        }

        public IActionResult productInCateg(int cat_id)
        {
          var  products = prodcat.GetProducts(cat_id);
            return PartialView("_products", products);
        }
    }
}
