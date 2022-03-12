using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class SecurityLoginsLogRepository : BaseADORepository<SecurityLoginsLogPoco>, IDataRepository<SecurityLoginsLogPoco>
    {
        public void Add(params SecurityLoginsLogPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SecurityLoginsLogPoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Security_Logins_Log]
           ([Id]
           ,[Login]
           ,[Source_IP]
           ,[Logon_Date]
           ,[Is_Succesful])
                        VALUES
                               (@Id,@Login,@Source_IP,@Logon_Date,@Is_Succesful)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Source_IP", item.SourceIP);
                    cmd.Parameters.AddWithValue("@Logon_Date", item.LogonDate);
                    cmd.Parameters.AddWithValue("@Is_Succesful", item.IsSuccesful);
                   

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public override IList<SecurityLoginsLogPoco> GetAll(params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [Id]
      ,[Login]
      ,[Source_IP]
      ,[Logon_Date]
      ,[Is_Succesful]
                                    FROM[JOB_PORTAL_DB].[dbo].[Security_Logins_Log]";

                conn.Open();

                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                SecurityLoginsLogPoco[] appPocos = new SecurityLoginsLogPoco[1000];
                while (rdr.Read())
                {
                    SecurityLoginsLogPoco poco = new SecurityLoginsLogPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Login = rdr.GetGuid(1);
                    poco.SourceIP = rdr.GetString(2);
                    poco.LogonDate = rdr.GetDateTime(3);
                    poco.IsSuccesful = rdr.GetBoolean(4);
                   

                    appPocos[x] = poco;
                    x++;
                }

                return appPocos.Where(a => a != null).ToList();
            }
        }

        public void Update(params SecurityLoginsLogPoco[] items)
        {

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SecurityLoginsLogPoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Security_Logins_Log]
                       SET [Login ] = @Login 
                          ,[Source_IP] = @Source_IP
                          ,[Logon_Date] = @Logon_Date
                          ,[Is_Succesful] = @Is_Succesful
                         
                       WHERE [Id] = @Id";

                 
                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Source_IP", item.SourceIP);
                    cmd.Parameters.AddWithValue("@Logon_Date", item.LogonDate);
                    cmd.Parameters.AddWithValue("@Is_Succesful", item.IsSuccesful);
                    cmd.Parameters.AddWithValue("@Id", item.Id);


                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
