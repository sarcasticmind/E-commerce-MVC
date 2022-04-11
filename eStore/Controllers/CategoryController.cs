using eStore.Models;
using eStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers
{
    public class CategoryController : Controller
    {
        IRepositoryIntid<Category> cagRepo;
        public CategoryController(eStoreContext _context,IRepositoryIntid<Category> _cagRepo)
        {
            cagRepo = _cagRepo;
        }
        // GET: CategoryController
        public ActionResult Index()
        {
           var categories =  cagRepo.GetAll();
            return View(categories);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            var categ = cagRepo.GetByID(id);
            return View(categ);
        }

        [Authorize(Roles = "admin")]
        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category categ)
        {
            if (ModelState.IsValid)
            {
                cagRepo.Insert(categ);
                return RedirectToAction("Index");
            }
            return View("Create", categ);
        }

        [Authorize(Roles = "admin")]
        #region edit
        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category categ)
        {
            if (ModelState.IsValid)
            {
                cagRepo.Update(id, categ);
                return RedirectToAction("Index");
            }
            return View("Edit", categ);
        }
        #endregion

        [Authorize(Roles = "admin")]
        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var categ = cagRepo.GetByID(id);
            if (categ != null)
            {
                cagRepo.Delete(id);
            }
            return RedirectToAction("index");
        }

        [Authorize(Roles = "admin")]
        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
