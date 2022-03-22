using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Configuration;

namespace CareerCloud.ADODataAccessLayer
{
    public class SystemCountryCodeRepository
    {
        string _connStr;
        public SystemCountryCodeRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }

        public void Add(params SystemCountryCodePoco[] items)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    foreach (SystemCountryCodePoco item in items)
                    {
                        cmd.CommandText = @"INSERT INTO [dbo].[System_Country_Codes]
                                                       ([Code]
                                                       ,[Name])
                                                       
                                                 VALUES
                                                       (@Code
                                                       ,@Name)";

                        cmd.Parameters.AddWithValue("@Code", item.Code);
                        cmd.Parameters.AddWithValue("@Name", item.Name);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public IList<SystemCountryCodePoco> GetAll(params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            SystemCountryCodePoco[] addPocos = new SystemCountryCodePoco[1000];
            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT [Code]
                                              ,[Name]
                                          FROM [dbo].[System_Country_Codes]";

                    conn.Open();

                    int x = 0;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            SystemCountryCodePoco poco = new SystemCountryCodePoco();
                            poco.Code = dr.GetString(0);
                            poco.Name = dr.GetString(1);

                            addPocos[x] = poco;
                            x++;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return addPocos.Where(a => a != null).ToList();
        }

        public void Update(params SystemCountryCodePoco[] items)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    foreach (SystemCountryCodePoco item in items)
                    {
                        cmd.CommandText = @"UPDATE [dbo].[System_Country_Codes]
                                               SET [Name] = @Name                                                  
                                             WHERE [Code] = @Code";

                        cmd.Parameters.AddWithValue("@Code", item.Code);
                        cmd.Parameters.AddWithValue("@Name", item.Name);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Remove(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn
                };
                foreach (SystemCountryCodePoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[System_Country_Codes]
                                              WHERE [Code] = @Code";

                    cmd.Parameters.AddWithValue("@Code", item.Code);

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public SystemCountryCodePoco GetSingle(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemCountryCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }
    }
}