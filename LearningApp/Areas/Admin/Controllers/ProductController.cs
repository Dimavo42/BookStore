using Learing.Utility;
using LearningApp.DataAccess.Repository;
using LearningApp.Model;
using LearningApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace LearningApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork UnitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = UnitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> products = _unitOfWork.Products.GetAll(includeProps: "Category").ToList();
            return View(products);
        }

        public IActionResult Upsert(int? id)
        {
            //IEnumerable<SelectListItem> CategoryList = /*_unitOfWork*/.Category.GetAll().
            //    Select(u => new SelectListItem
            //    {
            //        Text = u.Name,
            //        Value = u.Id.ToString(),
            //    });
            //ViewBag.CategoryList = CategoryList;
            //ViewData[nameof(CategoryList)] = CategoryList;
            ProductVM productsVM = new ProductVM
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(x =>
                new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                }),
                Product = new Product()
            };
            if (id == null || id == 0)
            {
                //Create
                return View(productsVM);
            }
            else
            {
                //Update
                productsVM.Product = _unitOfWork.Products.GetFirstOrDefault(x => x.Id == id);
                return View(productsVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM prouductViewModel, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string proudectPath = Path.Combine(wwwRootPath, @"images\product");
                    if (!string.IsNullOrEmpty(prouductViewModel.Product.ImageUrl))
                    {
                        //Delete The old Img
                        var oldImagePath =
                            Path.Combine(wwwRootPath, prouductViewModel.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (FileStream fileStream = new FileStream(Path.Combine(proudectPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    prouductViewModel.Product.ImageUrl = @"/images/product/" + fileName;
                }
                if (prouductViewModel.Product.Id == 0)
                {
                    _unitOfWork.Products.Add(prouductViewModel.Product);
                }
                else
                {
                    _unitOfWork.Products.Update(prouductViewModel.Product);
                }
                _unitOfWork.Save();
                TempData["success"] = "Book added succesed";
                return RedirectToAction("Index");
            }
            else
            {
                prouductViewModel.CategoryList = _unitOfWork.Category.GetAll().Select(x =>
                new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                });
                return View(prouductViewModel);
            }
        }


        //public iactionresult delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return notfound();
        //    }
        //    product? product = _unitofwork.products.getfirstordefault(x=>x.id == id);
        //    if (product == null)
        //    { 
        //        return notfound();
        //    }
        //    return view(product);
        //}
        //[httppost, actionname("delete")]
        //public iactionresult deletepost(int? id)
        //{
        //    product? product = _unitofwork.products.getfirstordefault (x=> x.id == id);
        //    if (product == null)
        //    {
        //        return notfound();
        //    }
        //    _unitofwork.products.delete(product);
        //    _unitofwork.save();
        //    tempdata["success"] = "book deleted seccused";
        //    return redirecttoaction("index");
        //}

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProudects = _unitOfWork.Products.GetAll(includeProps: "Category").ToList();
            return Json(new { data = objProudects });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productFromDB = _unitOfWork.Products.GetFirstOrDefault(x => x.Id == id);
            if (productFromDB == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productFromDB.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Products.Delete(productFromDB);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete succesed" });
        }

        #endregion
    }
}
