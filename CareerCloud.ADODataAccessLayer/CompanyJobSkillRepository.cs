using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyJobSkillRepository : BaseADORepository<CompanyJobSkillPoco>, IDataRepository<CompanyJobSkillPoco>
    {
        public void Add(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyJobSkillPoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Job_Skills] 
           ([Id]
           ,[Job]
           ,[Skill]
           ,[Skill_Level]
           ,[Importance])
                        VALUES
                               (@Id,@Job,@Skill,@Skill_Level,@Importance)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Job", item.Job);
                    cmd.Parameters.AddWithValue("@Skill", item.Skill);
                    cmd.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                    cmd.Parameters.AddWithValue("@Importance", item.Importance);
                   

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public override IList<CompanyJobSkillPoco> GetAll(params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [Id]
      ,[Job]
      ,[Skill]
      ,[Skill_Level]
      ,[Importance]
      ,[Time_Stamp]
                                    FROM[JOB_PORTAL_DB].[dbo].[Company_Job_Skills]";

                conn.Open();

                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                CompanyJobSkillPoco[] appPocos = new CompanyJobSkillPoco[1000];
                while (rdr.Read())
                {
                    CompanyJobSkillPoco poco = new CompanyJobSkillPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Job = rdr.GetGuid(1);
                    poco.Skill = rdr.GetString(2);
                    poco.SkillLevel = rdr.GetString(3);
                    poco.Importance = rdr.GetInt32(4);
                  
                    poco.TimeStamp = (byte[])rdr[5];

                    appPocos[x] = poco;
                    x++;
                }

                return appPocos.Where(a => a != null).ToList();
            }
        }

        public void Update(params CompanyJobSkillPoco[] items)
        {

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyJobSkillPoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Company_Job_Skills]
                       SET [Job] = @Job
                          ,[Skill] = @Skill
                          ,[SkillLevel] = @SkillLevel
                          ,[Importance] = @Importance
                         
                       WHERE [Id] = @Id";

                  
                    cmd.Parameters.AddWithValue("@Job", item.Job);
                    cmd.Parameters.AddWithValue("@Skill", item.Skill);
                    cmd.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
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
