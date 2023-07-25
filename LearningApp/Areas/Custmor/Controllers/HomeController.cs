using LearningApp.DataAccess.Repository;
using LearningApp.Model;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LearningApp.Areas.Custmor.Controllers
{
    [Area("Custmor")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Products.GetAll(includeProps: "Category");
            return View(productList);
        }

        public IActionResult Details(int? productId)
        {
            Product product = _unitOfWork.Products.GetFirstOrDefault(x => x.Id == productId, includeProps: "Category");
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}