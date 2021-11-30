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
    public class UserRepository : DBConnection, IRepository<User>
    {
        public UserRepository(string connection) : base(connection)
        {

        }
        public void Create(User user)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = $"Insert into Users(RoleID, Email, Password) " +
                               $"values(" +
                               $"@RoleID, " +
                               $"@Email, " +
                               $"@Password)";

                db.Execute(sqlQuery, user);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = $"Delete from Users where ID = @id";
                db.Execute(sqlQuery, new { id });
            }
        }

        public User Get(int id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlquery = $"Select * from Users u " +
                               $"left join Roles r " +
                               $"on u.RoleID = r.ID " +
                               $"where u.ID = @id";
                return db.Query<User, Role, User>(sqlquery, (user, role) => { user.Role = role; return user; }, new { id }).FirstOrDefault();
            }
        }

        public IEnumerable<User> GetList()
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlquery = $"Select * from Users u " +
                               $"left join Roles r " +
                               $"on u.RoleID = r.ID";
                return db.Query<User, Role, User>(sqlquery, (user, role) => { user.Role = role; return user; }).ToList();
            }
        }

        public void Update(User user)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = $"Update Users set " +
                               $"RoleID = @RoleID, " +
                               $"Email = @Email, " +
                               $"Password = @Password, " +
                               $"where ID = @ID";
                db.Execute(sqlQuery, user);
            }
        }
    }
}
