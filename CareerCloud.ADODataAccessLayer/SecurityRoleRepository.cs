using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class SecurityRoleRepository : BaseADORepository<SecurityRolePoco>, IDataRepository<SecurityRolePoco>
    {
        public void Add(params SecurityRolePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SecurityRolePoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Security_Roles]
           ([Id]
           ,[Role]
           ,[Is_Inactive])
                        VALUES
                               (@Id,@Role,@Is_Inactive)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Role", item.Role);
                    cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public override IList<SecurityRolePoco> GetAll(params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [Id]
      ,[Role]
      ,[Is_Inactive]
                                    FROM[JOB_PORTAL_DB].[dbo].[Security_Roles]";

                conn.Open();

                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                SecurityRolePoco[] appPocos = new SecurityRolePoco[1000];
                while (rdr.Read())
                {
                    SecurityRolePoco poco = new SecurityRolePoco();
                    poco.Id = rdr.GetGuid(0);
                    
                    poco.Role = rdr.GetString(1);
                    poco.IsInactive = rdr.GetBoolean(2);
                    

                    appPocos[x] = poco;
                    x++;
                }

                return appPocos.Where(a => a != null).ToList();
            }
        }

        public void Update(params SecurityRolePoco[] items)
        {

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SecurityRolePoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Security_Roles]
                       SET [Role] = @Role
                          ,[Is_Inactive] = @Is_Inactive
                         
                       WHERE [Id] = @Id";

                   
                    cmd.Parameters.AddWithValue("@Role", item.Role);
                    cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    cmd.Parameters.AddWithValue("@Id", item.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
