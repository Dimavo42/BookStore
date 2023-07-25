using LearningApp.DataAccess.Data;
using LearningApp.DataAccess.Repository.IRepository;
using LearningApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LearningApp.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplictionDBContex _db;
        public CategoryRepository(ApplictionDBContex db):base(db) 
        {
            _db=db;
        }

        public void Update(Category category)
        {
            _db.Categories.Update(category);
        }
    }
}
