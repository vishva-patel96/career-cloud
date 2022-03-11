﻿using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantProfileRepository : BaseADORepository<ApplicantProfilePoco>, IDataRepository<ApplicantProfilePoco>
    {
        public void Add(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantProfilePoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Profiles]
           ([Id]
           ,[Login]
           ,[Current_Salary]
           ,[Current_Rate]
           ,[Currency]
           ,[Country_Code]
           ,[State_Province_Code]
           ,[Street_Address]
           ,[City_Town]
           ,[Zip_Postal_Code])
     VALUES
           (@Id,
           ,@Login, 
           ,@Current_Salary
           ,@Current_Rate
           ,@Currency
           ,@Country_Code
            ,@State_Province_Code
           ,@Street_Address
           ,@City_Town
           ,@Zip_Postal_Code)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Current_Salary", item.CurrentSalary);
                    cmd.Parameters.AddWithValue("@CCurrent_Rate", item.CurrentRate);
                    cmd.Parameters.AddWithValue("@Currency", item.Currency);
                    cmd.Parameters.AddWithValue("@Country_Code", item.Country);
                    cmd.Parameters.AddWithValue("@State_Province_Code", item.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", item.Street);

                    cmd.Parameters.AddWithValue("@City_Town", item.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);




                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public override IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [Id]
      ,[Login]
      ,[Current_Salary]
      ,[Current_Rate]
      ,[Currency]
      ,[Country_Code]
      ,[State_Province_Code]
      ,[Street_Address]
      ,[City_Town]
      ,[Zip_Postal_Code]
      ,[Time_Stamp]
  FROM[JOB_PORTAL_DB]. [dbo].[Applicant_Profiles]
";

                conn.Open();

                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                ApplicantProfilePoco[] appPocos = new ApplicantProfilePoco[1000];
                while (rdr.Read())
                {
                    ApplicantProfilePoco poco = new ApplicantProfilePoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Login = rdr.GetGuid(1);
                    poco.CurrentSalary = rdr.GetDecimal(2);
                    poco.CurrentRate = rdr.GetDecimal(3);
                    poco.Currency = rdr.GetString(4);
                    poco.Country = rdr.GetString(5);
                    poco.Province = rdr.GetString(6);
                    poco.Street = rdr.GetString(7);
                    poco.City = rdr.GetString(8);
                    poco.PostalCode = rdr.GetString(9);
                    poco.TimeStamp = (byte[])rdr[7];

                    appPocos[x] = poco;
                    x++;
                }

                return appPocos.Where(a => a != null).ToList();
            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantProfilePoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Applicant_Profiles]
                       SET [Login] = @Login
                          ,[Current_Salary] = @Current_Salary
                          ,[Current_Rate] = @Current_Rate
                          ,[Currency] = @Currency 
                          ,[Country_Code] = @Country_Code
                          ,[State_Province_Code] = @State_Province_Code
                          ,[Street_Address] = @Street_Address

                          ,[City_Town] = @City_Town

                          ,[Zip_Postal_Code] = @Zip_Postal_Code

                       WHERE [Id] = @Id";

                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Current_Salary", item.CurrentSalary);
                    cmd.Parameters.AddWithValue("@Current_Rate", item.CurrentRate);
                    cmd.Parameters.AddWithValue("@Currency", item.Currency);
                    cmd.Parameters.AddWithValue("@Country_Code", item.Country);
                    cmd.Parameters.AddWithValue("@State_Province_Code", item.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", item.Street);

                    cmd.Parameters.AddWithValue("@City_Town", item.City);

                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);

                    cmd.Parameters.AddWithValue("@Id", item.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
