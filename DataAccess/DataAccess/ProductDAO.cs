using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAccess
{
    public class ProductDAO
    {
        public static List<Product> GetProducts()
        {
            var list = new List<Product>();
            try
            {
                using(var context = new Lab1Prn231Context())
                {
                    list = context.Products.Include(c => c.Category).ToList();
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
        public static Product FindProductById (int Id)
        {
            Product product = new Product();
            try
            {
                using (var context = new Lab1Prn231Context())
                {
                    product = context.Products.Include(c => c.Category).FirstOrDefault(p => p.ProductId == Id);
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }
        public static void SaveProduct(Product p)
        {
            try
            {
                using (var context = new Lab1Prn231Context())
                {
                    context.Products.Add(p);
                    context.SaveChanges();
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdateProduc(Product p)
        {
            try
            {
                using (var context = new Lab1Prn231Context())
                {
                    context.Entry<Product>(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void DeleteProduc(Product p)
        {
            try
            {
                using (var context = new Lab1Prn231Context())
                {
                   var product = context.Products.FirstOrDefault(x => x.ProductId == p.ProductId);
                    context.Products.Remove(product);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
