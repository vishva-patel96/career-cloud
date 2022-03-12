using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantSkillRepository : BaseADORepository<ApplicantSkillPoco>, IDataRepository<ApplicantSkillPoco>
    {
        public void Add(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantSkillPoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Skills]
           ([Id]
           ,[Applicant]
           ,[Skill]
           ,[Skill_Level]
           ,[Start_Month]
           ,[Start_Year]
           ,[End_Month]
           ,[End_Year])
                        VALUES
                               (@Id,@Applicant,@Skill,@Skill_Level,@Start_Month,@Start_Year,@End_Month,@End_Year)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Skill", item.Skill);
                    cmd.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);

                    cmd.Parameters.AddWithValue("@Start_Month", item.StartMonth);

                    cmd.Parameters.AddWithValue("@Start_Year", item.StartYear);

                    cmd.Parameters.AddWithValue("@End_Month", item.EndMonth);
                    cmd.Parameters.AddWithValue("@End_Year", item.EndYear);
                    

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public override IList<ApplicantSkillPoco> GetAll(params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [Id]
      ,[Applicant]
      ,[Skill]
      ,[Skill_Level]
      ,[Start_Month]
      ,[Start_Year]
      ,[End_Month]
      ,[End_Year]
      ,[Time_Stamp]
                                    FROM[JOB_PORTAL_DB].[dbo].[Applicant_Skills]";

                conn.Open();

                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                ApplicantSkillPoco[] appPocos = new ApplicantSkillPoco[1000];
                while (rdr.Read())
                {
                    ApplicantSkillPoco poco = new ApplicantSkillPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Applicant = rdr.GetGuid(1);
                    poco.Skill = rdr.GetString(2);
                    poco.SkillLevel = rdr.GetString(3);
                    poco.StartMonth = rdr.GetByte(4);
                    poco.StartYear = rdr.GetInt32(5);
                    poco.EndMonth = rdr.GetByte(6);
                    poco.EndYear = rdr.GetInt32(7);
                    poco.TimeStamp = (byte[])rdr[8];

                    appPocos[x] = poco;
                    x++;
                }

                return appPocos.Where(a => a != null).ToList();
            }
        }

        public void Update(params ApplicantSkillPoco[] items)
        {

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantSkillPoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Applicant_Skills]
                       SET [Applicant] = @Applicant
                          ,[Skill] = @Skill
                          ,[Skill_Level] = @Skill_Level
                          ,[Start_Month] = @Start_Month
                          ,[Start_Year] = @Start_Year
                          ,[End_Month] = @End_Month
                          ,[End_Year] = @End_Year
                        
                       WHERE [Id] = @Id";

                    
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Skill", item.Skill);
                    cmd.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);

                    cmd.Parameters.AddWithValue("@Start_Month", item.StartMonth);

                    cmd.Parameters.AddWithValue("@Start_Year", item.StartYear);

                    cmd.Parameters.AddWithValue("@End_Month", item.EndMonth);
                    cmd.Parameters.AddWithValue("@End_Year", item.EndYear);
                    cmd.Parameters.AddWithValue("@Id", item.Id);


                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
