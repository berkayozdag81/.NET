using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    //NuGet
   public class EfProductDal : EfEntityRepositoryBase<Product,NorthwindContext>,IProductDal
    {
        public List<ProductDeatilDto> GetProductDetails()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var reseult = from p in context.Products
                    join c in context.Categories
                        on p.CategoryId equals c.CategoryId
                    select new ProductDeatilDto
                    {
                        ProductId = p.ProductId,ProductName =p.ProductName,
                        CategoryName = c.CategoryName,UnitsInStock = p.UnitsInStock

                    };
                return reseult.ToList();

            }
        }
    }
}
