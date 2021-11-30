using BuildingStore.Models.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingStore.Models.Repositories
{
    public class OrderItemRepository : DBConnection, IRepository<OrderItem>
    {
        public OrderItemRepository(string connection) : base(connection)
        {

        }

        public void Create(OrderItem orderItem)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = $"Insert into OrderItems(UserID, ProductID, Qty, Status) " +
                               $"values(" +
                               $"@UserID, " +
                               $"@ProductID, " +
                               $"@Qty, " +
                               $"@Status)";

                db.Execute(sqlQuery, orderItem);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = $"Delete from OrderItems where ID = @id";
                db.Execute(sqlQuery, new { id });
            }
        }

        public OrderItem Get(int id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlquery = $"Select * from OrderItems o " +
                               $"left join Products p " +
                               $"on o.ProductID = p.ID " +
                               $"left join Users u " +
                               $"on o.UserID = u.ID " +
                               $"where o.ID = @id";
                return db.Query<OrderItem, Product, User, OrderItem>(sqlquery, (orderItem, product, user) => { orderItem.Product = product; orderItem.User = user;  return orderItem; }, new { id }).FirstOrDefault();
            }
        }

        public IEnumerable<OrderItem> GetList()
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlquery = $"Select * from OrderItems o " +
                               $"left join Products p " +
                               $"on o.ProductID = p.ID " +
                               $"left join Users u " +
                               $"on o.UserID = o.ID";
                return db.Query<OrderItem, Product, User, OrderItem>(sqlquery, (orderItem, product, user) => { orderItem.Product = product; orderItem.User = user;  return orderItem; }).ToList();
            }
        }

        public void Update(OrderItem orderItem)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = $"Update OrderItems set " +
                               $"UserID = @UserID, " +
                               $"ProductID = @ProductID, " +
                               $"Qty = @Qty, " +
                               $"Status = @Status " +
                               $"where ID = @ID";
                db.Execute(sqlQuery, orderItem);
            }
        }
    }
}
