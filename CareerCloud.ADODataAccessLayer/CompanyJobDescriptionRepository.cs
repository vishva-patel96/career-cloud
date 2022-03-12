using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyJobDescriptionRepository : BaseADORepository<CompanyJobDescriptionPoco>, IDataRepository<CompanyJobDescriptionPoco>
    {
        public void Add(params CompanyJobDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyJobDescriptionPoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Jobs_Descriptions]
           ([Id]
           ,[Job]
           ,[Job_Name]
           ,[Job_Descriptions])
                        VALUES
                               (@Id,@Job,@Job_Name,@Job_Descriptions)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Job", item.Job);
                    cmd.Parameters.AddWithValue("@Job_Name", item.JobName);
                    cmd.Parameters.AddWithValue("@Job_Descriptions", item.JobDescriptions);
                   
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public override IList<CompanyJobDescriptionPoco> GetAll(params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [Id]
      ,[Job]
      ,[Job_Name]
      ,[Job_Descriptions]
      ,[Time_Stamp]
                                    FROM[JOB_PORTAL_DB].[dbo].[Company_Jobs_Descriptions]";

                conn.Open();

                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                CompanyJobDescriptionPoco[] appPocos = new CompanyJobDescriptionPoco[1000];
                while (rdr.Read())
                {
                    CompanyJobDescriptionPoco poco = new CompanyJobDescriptionPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Job = rdr.GetGuid(1);
                    poco.JobName = rdr.GetString(2);
                    poco.JobDescriptions = rdr.GetString(3);
                   
                    poco.TimeStamp = (byte[])rdr[7];

                    appPocos[x] = poco;
                    x++;
                }

                return appPocos.Where(a => a != null).ToList();
            }
        }

        public void Update(params CompanyJobDescriptionPoco[] items)
        {

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyJobDescriptionPoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Company_Jobs_Descriptions]
                       SET [Job] = @Job
                          ,[Job_Name] = @Job_Name
                          ,[Job_Descriptions] = @Job_Descriptions
                         
                       WHERE [Id] = @Id";

                   
                    cmd.Parameters.AddWithValue("@Job", item.Job);
                    cmd.Parameters.AddWithValue("@Job_Name", item.JobName);
                    cmd.Parameters.AddWithValue("@Job_Descriptions", item.JobDescriptions);
                    cmd.Parameters.AddWithValue("@Id", item.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
