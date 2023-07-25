using LearningApp.DataAccess.Data;
using LearningApp.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningApp.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplictionDBContex _db;
        public ICategoryRepository Category { get; private set; }

        public IProductRepository Products { get; private set; }

        public UnitOfWork(ApplictionDBContex db)
        {
            _db= db;
            Category = new CategoryRepository(_db);
            Products = new ProductRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
