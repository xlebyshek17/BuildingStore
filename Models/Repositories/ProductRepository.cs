using BuildingStore.Models.Entity;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingStore.Models.Repositories
{
    public class ProductRepository : DBConnection, IRepository<Product>
    {
        public ProductRepository(string connection) : base(connection)
        {

        }
        public void Create(Product product)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = $"Insert into Products(CategoryID, Name, Image, Price, Description) " +
                               $"values(" +
                               $"@CategoryID, " +
                               $"@Name, " +
                               $"@Image, " +
                               $"@Price, " +
                               $"@Description)";

                db.Execute(sqlQuery, product);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = $"Delete from Products where ID = @id";
                db.Execute(sqlQuery, new { id });
            }
        }

        public Product Get(int id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlquery = $"Select * from Products p " +
                               $"left join Categories c " +
                               $"on p.CategoryID = c.ID " +
                               $"where p.ID = @id";
                return db.Query<Product, Category, Product>(sqlquery, (product, category) => { product.Category = category; return product; }, new { id }).FirstOrDefault();
            }
        }

        public IEnumerable<Product> GetList()
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlquery = $"Select * from Products p " +
                               $"left join Categories c " +
                               $"on p.CategoryID = c.ID";
                return db.Query<Product, Category, Product>(sqlquery, (product, category) => { product.Category = category; return product; }).ToList();
            }
        }

        public void Update(Product product)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = $"Update Products set " +
                               $"CategoryID = @CategoryID, " +
                               $"Name = @Name, " +
                               $"Image = @Image, " +
                               $"Price = @Price, " +
                               $"Description = @Description " +
                               $"where ID = @ID";
                db.Execute(sqlQuery, product);
            }
        }
    }
}
