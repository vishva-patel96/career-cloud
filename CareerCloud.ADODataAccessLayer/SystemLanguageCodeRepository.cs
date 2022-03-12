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
    public class SystemLanguageCodeRepository
    {
        string _connStr;
        public SystemLanguageCodeRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }

        public void Add(params SystemLanguageCodePoco[] items)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    foreach (SystemLanguageCodePoco item in items)
                    {
                        cmd.CommandText = @"INSERT INTO [dbo].[System_Language_Codes]
                                                       ([LanguageID]
                                                       ,[Name]
                                                       ,[Native_Name])
                                                 VALUES
                                                       (@LanguageID
                                                       ,@Name
                                                       ,@Native_Name)";

                        cmd.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                        cmd.Parameters.AddWithValue("@Name", item.Name);
                        cmd.Parameters.AddWithValue("@Native_Name", item.NativeName);

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

        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            SystemLanguageCodePoco[] addPocos = new SystemLanguageCodePoco[1000];
            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT [LanguageID]
                                              ,[Name]
                                              ,[Native_Name]
                                          FROM [dbo].[System_Language_Codes]";

                    conn.Open();

                    int x = 0;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            SystemLanguageCodePoco poco = new SystemLanguageCodePoco();
                            poco.LanguageID = dr.GetString(0);
                            poco.Name = dr.GetString(1);
                            poco.NativeName = dr.GetString(2);

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

        public void Update(params SystemLanguageCodePoco[] items)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    foreach (SystemLanguageCodePoco item in items)
                    {
                        cmd.CommandText = @"UPDATE [dbo].[System_Language_Codes]
                                               SET [Name] = @Name
                                                  ,[Native_Name] = @Native_Name
                                             WHERE [LanguageID] = @LanguageID";

                        cmd.Parameters.AddWithValue("@Name", item.Name);
                        cmd.Parameters.AddWithValue("@Native_Name", item.NativeName);
                        cmd.Parameters.AddWithValue("@LanguageID", item.LanguageID);

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

        public void Remove(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn
                };
                foreach (SystemLanguageCodePoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[System_Language_Codes]
                                              WHERE [LanguageID] = @LanguageID";

                    cmd.Parameters.AddWithValue("@LanguageID", item.LanguageID);

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemLanguageCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }
    }
}