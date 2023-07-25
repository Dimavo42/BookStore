using LearningApp.DataAccess.Data;
using LearningApp.DataAccess.Repository.IRepository;
using LearningApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningApp.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplictionDBContex _db;
        public ProductRepository(ApplictionDBContex dBContex) : base(dBContex)
        {
            _db= dBContex;
        }

        public void Update(Product product)
        {
            var objFromDB = _db.Products.FirstOrDefault(x=> x.Id ==product.Id);
            if (objFromDB != null)
            {
                objFromDB.Title = product.Title;
                objFromDB.ISBN = product.ISBN;
                objFromDB.Price = product.Price;
                objFromDB.Author = product.Author;
                objFromDB.ListPrice = product.ListPrice;
                objFromDB.Price50 = product.Price50;
                objFromDB.Price100 = product.Price100;
                objFromDB.CategoryId = product.CategoryId;
                if(objFromDB.ImageUrl != null)
                {
                    objFromDB.ImageUrl = product.ImageUrl;
                }
            }
        }
    }
}
