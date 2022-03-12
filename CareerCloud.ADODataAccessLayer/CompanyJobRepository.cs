using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyJobRepository : BaseADORepository<CompanyJobPoco>, IDataRepository<CompanyJobPoco>
    {
        public void Add(params CompanyJobPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyJobPoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Jobs]
           ([Id]
           ,[Company]
           ,[Profile_Created]
           ,[Is_Inactive]
           ,[Is_Company_Hidden])
                        VALUES
                               (@Id,@Company,@Profile_Created,@Is_Inactive,@Is_Company_Hidden)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Company", item.Company);
                    cmd.Parameters.AddWithValue("@Profile_Created", item.ProfileCreated);
                    cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    cmd.Parameters.AddWithValue("@Is_Company_Hidden", item.IsCompanyHidden);
                  

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public override IList<CompanyJobPoco> GetAll(params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [Id]
      ,[Company]
      ,[Profile_Created]
      ,[Is_Inactive]
      ,[Is_Company_Hidden]
      ,[Time_Stamp]
                                    FROM[JOB_PORTAL_DB].[dbo].[Company_Jobs]";

                conn.Open();

                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                CompanyJobPoco[] appPocos = new CompanyJobPoco[5000];
                while (rdr.Read())
                {
                    CompanyJobPoco poco = new CompanyJobPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Company = rdr.GetGuid(1);
                    poco.ProfileCreated = rdr.GetDateTime(2);
                    poco.IsInactive = rdr.GetBoolean(3);
                    poco.IsCompanyHidden = rdr.GetBoolean(4);
                  
                    poco.TimeStamp = (byte[])rdr[5];

                    appPocos[x] = poco;
                    x++;
                }

                return appPocos.Where(a => a != null).ToList();
            }
        }

        public void Update(params CompanyJobPoco[] items)
        {

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyJobPoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Company_Jobs]
                       SET [Company] = @Company
                          ,[Profile_Created] = @Profile_Created
                          ,[Is_Inactive] = @Is_Inactive
                          ,[Is_Company_Hidden] = @Is_Company_Hidden
                         
                       WHERE [Id] = @Id";

                    
                    cmd.Parameters.AddWithValue("@Company", item.Company);
                    cmd.Parameters.AddWithValue("@Profile_Created", item.ProfileCreated);
                    cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    cmd.Parameters.AddWithValue("@Is_Company_Hidden", item.IsCompanyHidden);

                    cmd.Parameters.AddWithValue("@Id", item.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
