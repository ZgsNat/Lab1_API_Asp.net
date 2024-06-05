using BusinessObjects.Models;
using DataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class CategoryRepository
    {
        public CategoryRepository() { }
        public List<Category> GetCategories() => CategoryDAO.GetCategories();
    }
}
