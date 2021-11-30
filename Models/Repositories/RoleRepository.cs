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
    public class RoleRepository : DBConnection, IRepository<Role>
    {
        public RoleRepository(string connection) : base(connection)
        {

        }

        public void Create(Role role)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = $"Insert into Roles(Name) values(@Name)";

                db.Execute(sqlQuery, role);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = $"Delete from Roles where ID = @id";
                db.Execute(sqlQuery, new { id });
            }
        }

        public Role Get(int id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Role>($"Select * from Roles where ID = @id", new { id }).FirstOrDefault();
            }
        }

        public IEnumerable<Role> GetList()
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Role>($"Select * from Roles").ToList();
            }
        }

        public void Update(Role role)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var sqlQuery = $"Update Roles set Name = @Name where ID = @ID";
                db.Execute(sqlQuery, role);
            }
        }
    }
}
