using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyJobEducationRepository : BaseADORepository<CompanyJobEducationPoco>, IDataRepository<CompanyJobEducationPoco>
    {
        public void Add(params CompanyJobEducationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyJobEducationPoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Job_Educations]
           ([Id]
           ,[Job]
           ,[Major]
           ,[Importance])
                        VALUES
                               (@Id,@Job,@Major,@Importance)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Job", item.Job);
                    cmd.Parameters.AddWithValue("@Major", item.Major);
                    cmd.Parameters.AddWithValue("@Importance", item.Importance);
                    

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public override IList<CompanyJobEducationPoco> GetAll(params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [Id]
      ,[Job]
      ,[Major]
      ,[Importance]
      ,[Time_Stamp]
                                    FROM[JOB_PORTAL_DB].[dbo].[Company_Job_Educations]";

                conn.Open();

                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                CompanyJobEducationPoco[] appPocos = new CompanyJobEducationPoco[1000];
                while (rdr.Read())
                {
                    CompanyJobEducationPoco poco = new CompanyJobEducationPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Job = rdr.GetGuid(1);
                    poco.Major = rdr.GetString(2);
                    poco.Importance = rdr.GetInt16(3);
                  
                    poco.TimeStamp = (byte[])rdr[7];

                    appPocos[x] = poco;
                    x++;
                }

                return appPocos.Where(a => a != null).ToList();
            }
        }

        public void Update(params CompanyJobEducationPoco[] items)
        {

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyJobEducationPoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Company_Job_Educations]
                       SET [Job] = @Job
                          ,[Major] = @Major
                          ,[Importance] = @Importance
                          
                       WHERE [Id] = @Id";

                   
                    cmd.Parameters.AddWithValue("@Job", item.Job);
                    cmd.Parameters.AddWithValue("@Major", item.Major);
                    cmd.Parameters.AddWithValue("@Importance", item.Importance);
                    cmd.Parameters.AddWithValue("@Id", item.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
