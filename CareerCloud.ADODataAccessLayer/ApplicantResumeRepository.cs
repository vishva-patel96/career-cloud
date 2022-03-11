using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantResumeRepository : BaseADORepository<ApplicantResumePoco>, IDataRepository<ApplicantResumePoco>
    {
        public void Add(params ApplicantResumePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantResumePoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Resumes]
           ([Id]
           ,[Applicant]
           ,[Resume]
           ,[Last_Updated])
                        VALUES
                               (@Id,@Applicant,@Resume,@Last_Updated)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Resume", item.Resume);
                    cmd.Parameters.AddWithValue("@Last_Updated", item.LastUpdated);
                    

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public override IList<ApplicantResumePoco> GetAll(params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [Id]
      ,[Applicant]
      ,[Resume]
      ,[Last_Updated]
                                  
                                    FROM[JOB_PORTAL_DB].[dbo].[Applicant_Resumes]";

                conn.Open();

                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                ApplicantResumePoco[] appPocos = new ApplicantResumePoco[1000];
                while (rdr.Read())
                {
                    ApplicantResumePoco poco = new ApplicantResumePoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Applicant = rdr.GetGuid(1);
                    poco.Resume = rdr.GetString(2);

                    poco.LastUpdated = rdr.GetDateTime(3);




                    appPocos[x] = poco;
                    x++;
                }

                return appPocos.Where(a => a != null).ToList();
            }
        }

        public void Update(params ApplicantResumePoco[] items)
        {

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantResumePoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Applicant_Educations]
                       SET [Applicant] = @Applicant
                          ,[Resume] = @Resume
                          ,[Last_Updated] = @Last_Updated
                          
                       WHERE [Id] = @Id";

                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Resume", item.Resume);
                    cmd.Parameters.AddWithValue("@Last_Updated", item.LastUpdated);
                   
                    cmd.Parameters.AddWithValue("@Id", item.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
