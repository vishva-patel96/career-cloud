using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyProfileRepository : BaseADORepository<CompanyProfilePoco>, IDataRepository<CompanyProfilePoco>
    {
        public void Add(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyProfilePoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Profiles]
           ([Id]
           ,[Registration_Date]
           ,[Company_Website]
           ,[Contact_Phone]
           ,[Contact_Name]
           ,[Company_Logo])
                        VALUES
                               (@Id,@Registration_Date,@Company_Website,@Contact_Phone,@Contact_Name,@Company_Logo)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                    cmd.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                    cmd.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);
                   

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public override IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [Id]
      ,[Registration_Date]
      ,[Company_Website]
      ,[Contact_Phone]
      ,[Contact_Name]
      ,[Company_Logo]
      ,[Time_Stamp]
                                    FROM[JOB_PORTAL_DB].[dbo].[Company_Profiles]";

                conn.Open();

                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                CompanyProfilePoco[] appPocos = new CompanyProfilePoco[5000];
                while (rdr.Read())
                {
                    CompanyProfilePoco poco = new CompanyProfilePoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.RegistrationDate = rdr.GetDateTime(1);
                    poco.CompanyWebsite = (rdr.IsDBNull(2)) ? String.Empty : rdr.GetString(2);
                    poco.ContactPhone = rdr.GetString(3);
                    poco.ContactName = (rdr.IsDBNull(4)) ? String.Empty : rdr.GetString(4);
                    poco.CompanyLogo = (rdr.IsDBNull(5)) ? Array.Empty<byte>() : (byte[])rdr[5];
                    poco.TimeStamp = (byte[])rdr[6];

                    appPocos[x] = poco;
                    x++;
                }

                return appPocos.Where(a => a != null).ToList();
            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyProfilePoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Company_Profiles]
                       SET [Registration_Date] = @Registration_Date
                          ,[Company_Website] = @Company_Website
                          ,[Contact_Phone] = @Contact_Phone
                          ,[Contact_Name] = @Contact_Name
                          ,[Company_Logo] = @Company_Logo
                      
                       WHERE [Id] = @Id";

                    cmd.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                    cmd.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                    cmd.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);
                    cmd.Parameters.AddWithValue("@Id", item.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
