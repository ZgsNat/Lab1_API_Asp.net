using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAccess
{
    public class CategoryDAO
    {
        public static List<Category> GetCategories()
        {
            var lstCategories = new List<Category>();
            try
            {
                using (var context = new Lab1Prn231Context())
                {
                    lstCategories = context.Categories.ToList();
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return lstCategories;
        }
        
    }
}
