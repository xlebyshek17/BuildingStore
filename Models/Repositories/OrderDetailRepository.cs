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
    public class OrderDetailRepository : DBConnection, IRepository<OrderDetail>
    {
        public OrderDetailRepository(string connection) : base(connection)
        {

        }

        public void Create(OrderDetail orderDetail)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = $"Insert into OrderDetails(OrderID, OrderItemID, TotalPrice, Date, BuyingType, Comments) " +
                               $"values(" +
                               $"@OrderID, " +
                               $"@OrderItemID, " +
                               $"@TotalPrice, " +
                               $"@Date, " +
                               $"@BuyingType, " +
                               $"@Comments)";

                db.Execute(sqlQuery, orderDetail);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = $"Delete from OrderDetails where ID = @id";
                db.Execute(sqlQuery, new { id });
            }
        }

        public OrderDetail Get(int id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlquery = $"Select * from OrderDetails od " +
                               $"left join OrderItems o " +
                               $"on od.OrderItemID = o.ID " +
                               $"left join Products pr " +
                               $"on o.ProductID = pr.ID " +
                               $"left join Orders p " +
                               $"on od.OrderID = p.ID " +
                               $"left join Users u " +
                               $"on u.ID = p.UserID and o.UserID = u.ID " +
                               $"where od.OrderID = @id";
                return db.Query<OrderDetail, OrderItem, Product, Order, User, OrderDetail>(sqlquery, 
                    (orderDetail, orderItem, product, order, user) => { orderDetail.Order = order; 
                        orderDetail.OrderItem = orderItem;  orderDetail.OrderItem.Product = product;
                        orderDetail.OrderItem.User = user; orderDetail.Order.User = user; 
                        return orderDetail; }, new { id }).FirstOrDefault();
            }
        }

        public IEnumerable<OrderDetail> GetList()
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlquery = $"Select * from OrderDetails od " +
                               $"left join OrderItems o " +
                               $"on od.OrderItemID = o.ID " +
                               $"left join Products pr " +
                               $"on o.ProductID = pr.ID " +
                               $"left join Orders p " +
                               $"on od.OrderID = p.ID " +
                               $"left join Users u " +
                               $"on u.ID = p.UserID and o.UserID = u.ID";
                return db.Query<OrderDetail, OrderItem, Product, Order, User, OrderDetail>(sqlquery, 
                    (orderDetail, orderItem, product, order, user) => { orderDetail.Order = order; 
                        orderDetail.OrderItem = orderItem; orderDetail.OrderItem.Product = product; 
                        orderDetail.Order.User = user; orderDetail.OrderItem.User = user; return orderDetail; }).ToList();
            }
        }

        public void Update(OrderDetail orderDetail)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = $"Update OrderDetails set " +
                               $"OrderID = @OrderID, " +
                               $"OrderItemID = @OrderItemID, " +
                               $"Name = @Name, " +
                               $"TotalPrice = @TotalPrice, " +
                               $"Date = @Date, " +
                               $"BuyingType = @BuyingType, " +
                               $"Comments = @Comments " +
                               $"where ID = @ID";
                db.Execute(sqlQuery, orderDetail);
            }
        }
    }
}
