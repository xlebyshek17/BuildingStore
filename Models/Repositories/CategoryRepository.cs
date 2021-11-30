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
    public class CategoryRepository : DBConnection, IRepository<Category>
    {
        public CategoryRepository(string connection) : base(connection)
        {

        }
        public void Create(Category category)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = $"Insert into Categories(Name) values(@Name)";

                db.Execute(sqlQuery, category);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = $"Delete from Categories where ID = @id";
                db.Execute(sqlQuery, new { id });
            }
        }

        public Category Get(int id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Category>($"Select * from Categories where ID = @id", new { id }).FirstOrDefault();
            }
        }

        public IEnumerable<Category> GetList()
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Category>($"Select * from Categories").ToList();
            }
        }

        public void Update(Category category)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = $"Update Categories set Name = @Name where ID = @ID";
                db.Execute(sqlQuery, category);
            }
        }
    }
}
