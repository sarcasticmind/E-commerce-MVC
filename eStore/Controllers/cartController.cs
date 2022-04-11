using eStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace eStore.Models
{
   // [Authorize(Roles ="customer")]
    public class cartController : Controller
    {
            IRepositoryIntid<ProductOrder> cartRepo;
            IRepositoryIntid<Product> prodRepo;
            Iuser<ProductOrder> ucart;
            eStoreContext db = new eStoreContext();

            public cartController(IRepositoryIntid<ProductOrder> _cartRepo, Iuser<ProductOrder> _ucart,
                IRepositoryIntid<Product> _prodRepo)
            {
                cartRepo = _cartRepo;
                ucart = _ucart;
            prodRepo = _prodRepo;
            }
            public IActionResult Index()
            {
            string UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var cart = ucart.All(UserId);
                return View(cart);
            }
        public IActionResult checkedOut()
        {
            string UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var cart = ucart.All(UserId);
            foreach(var item in cart)
            {
                item.isCheckedout = true;
            }
            db.UpdateRange(cart);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult CustomerCart(string id)
            {
                var cart = ucart.All(id);
                return View(cart);
            }
        public IActionResult saveOrder(int ProductId, int quantity)
        {

            string UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            float ProductPrice = db.Products.Where(p => p.id == ProductId).FirstOrDefault().Price;
            ProductOrder OrderModel = new ProductOrder(); //db.productOrders.FirstOrDefault(p=>p.Customer_id == UserId);
            OrderModel.Customer_id = UserId;
            OrderModel.Date = System.DateTime.Now;
            OrderModel.Price =  ProductPrice;
            OrderModel.isCheckedout = false;
            OrderModel.Product_id = ProductId;
            OrderModel.Quantity = quantity;
            db.productOrders.Add(OrderModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Add(int id)
        {
            Product Pro = prodRepo.GetByID(id);
            ViewBag.pro = Pro;
            return View(new ProductOrder());
        }

        public IActionResult Delete(int id)
        {
            var order = cartRepo.GetByID(id);
            if (order != null)
            {
                cartRepo.Delete(id);
            }
            return RedirectToAction("index");
        }
        
        public IActionResult Bill()
        {
            string UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var products = ucart.All(UserId);
            return View(products);
        }
    }
}
