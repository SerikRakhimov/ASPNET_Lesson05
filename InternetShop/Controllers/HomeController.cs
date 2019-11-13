using InternetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternetShop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Products()
        {

            using (var context = new Context())
            {
                ViewBag.Products = context.Products.ToList();
            }

            return View();
        }

        public ActionResult Product(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            using (var context = new Context())
            {
                var product = context.Products.FirstOrDefault(p => p.Id == id);
                if (product == null)
                {
                    return HttpNotFound();
                }

                ViewBag.Product = product;
            }

            return View();
        }

        public ActionResult AddProduct()
        {
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            if (product == null)
            {
                return HttpNotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Message = "";
                return View(product);
            }

            using (var context = new Context())
            {
                var resultSeek = context.Products.Any(x => x.Name == product.Name);
                if (resultSeek)
                {
                    ViewBag.Message = "Наименование \"" + product.Name + "\" уже есть в базе данных. Повторите ввод!";
                    return View(product);
                }

                context.Products.Add(product);
                context.SaveChanges();
            }

            return RedirectToAction("Products");
        }

        public ActionResult EditProduct()
        {
            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult EditProduct(int? id)
        {
            Product product = null;

            if (id == null)
            {
                return HttpNotFound();
            }

            using (var context = new Context())
            {
                product = context.Products.FirstOrDefault(p => p.Id == id);
                if (product == null)
                {
                    return HttpNotFound();
                }
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            Product prod;
            if (product == null)
            {
                return HttpNotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(product);
            }

            using (var context = new Context())
            {

                prod = context.Products.Find(product.Id);
                if (prod != null)
                {
                    prod.Name = product.Name;
                    prod.Price = product.Price;
                    context.SaveChanges();
                }
            }

            return RedirectToAction("Products");
        }

        public ActionResult DeleteProduct()
        {
            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult DeleteProduct(int? id)
        {
            Product product = null;

            if (id == null)
            {
                return HttpNotFound();
            }

            using (var context = new Context())
            {
                product = context.Products.FirstOrDefault(p => p.Id == id);
                if (product == null)
                {
                    return HttpNotFound();
                }
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult DeleteProduct(Product product)
        {
            Product prod;
            if (product == null)
            {
                return HttpNotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(product);
            }

            using (var context = new Context())
            {

                prod = context.Products.Find(product.Id);
                if (prod != null)
                {
                    context.Products.Remove(prod);
                    context.SaveChanges();
                }
            }

            return RedirectToAction("Products");
        }

        [HttpGet]
        public ActionResult LoginUser()
        {
            ViewBag.Login = "";
            ViewBag.Password = "";
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        public ActionResult LoginUser(string login, string password)
        {
            bool resultSeek;

            ViewBag.Login = login;
            ViewBag.Password = password;

            if (login == "")
            {
                ViewBag.Message = "Введите логин!";
                return View();
            }
            if (password == "")
            {
                ViewBag.Message = "Введите пароль!";
                return View();
            }
            using (var context = new Context())
            {
                resultSeek = context.Users.Any(x => x.Login == login);
                if (!resultSeek)
                {
                    ViewBag.Message = "Логин \"" + login + "\" не найден в базе данных. Повторите ввод!";
                    return View();
                }
                resultSeek = context.Users.Any(x => x.Login == login && x.Password == password);
                if (!resultSeek)
                {
                    ViewBag.Message = "Пароль введен неправильно. Повторите ввод!";
                    return View();
                }
            }

            Session["userMain"] = login;

            return RedirectToAction("Products");

        }

        public ActionResult LogoutUser()
        {

            Session["userMain"] = "";

            return RedirectToAction("Products");

        }

        public ActionResult RegistrationUser()
        {
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        public ActionResult RegistrationUser(User user)
        {
            if (user == null)
            {
                return HttpNotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Message = "";
                return View(user);
            }

            using (var context = new Context())
            {
                var resultSeek = context.Users.Any(x => x.Login == user.Login);
                if (resultSeek)
                {
                    ViewBag.Message = "Логин \"" + user.Login + "\" уже есть в базе данных. Повторите ввод!";
                    return View(user);
                }
                context.Users.Add(user);
                context.SaveChanges();

            }

            Session["userMain"] = user.Login;

            return RedirectToAction("Products");

        }

    }
}