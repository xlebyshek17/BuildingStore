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
    public class OrderRepository : DBConnection, IRepository<Order>
    {
        public OrderRepository(string connection) : base(connection)
        {

        }
        public void Create(Order order)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = $"Insert into Orders(UserID, Name, Surname, Phone, Address) " +
                               $"values(" +
                               $"@UserID, " +
                               $"@Name, " +
                               $"@Surname, " +
                               $"@Phone, " +
                               $"@Address)";

                db.Execute(sqlQuery, order);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = $"Delete from Orders where ID = @id";
                db.Execute(sqlQuery, new { id });
            }
        }

        public Order Get(int id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlquery = $"Select * from Orders o " +
                               $"left join Users u " +
                               $"on o.UserID = u.ID " +
                               $"where o.ID = @id";
                return db.Query<Order, User, Order>(sqlquery, (order, user) => { order.User = user; return order; }, new { id }).FirstOrDefault();
            }
        }

        public IEnumerable<Order> GetList()
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlquery = $"Select * from Orders o " +
                               $"left join Users u " +
                               $"on o.UserID = u.ID";
                return db.Query<Order, User, Order>(sqlquery, (order, user) => { order.User = user; return order; }).ToList();
            }
        }

        public void Update(Order order)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = $"Update Orders set " +
                               $"UserID = @UserID, " +
                               $"Name = @Name, " +
                               $"Surname = @Surname, " +
                               $"Phone = @Phone, " +
                               $"Address = @Address" +
                               $"where ID = @ID";
                db.Execute(sqlQuery, order);
            }
        }
    }
}
