using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data;

namespace DatabaseDropper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Gathering Connection String");
            string sqlConnectionString = "Data Source=184.168.194.75;Initial Catalog=iGrad;Integrated Security=False;User ID=iGradAdmin;Password=Ezj11c#6;";

            Console.WriteLine("Obtaining SQL Commands");
            // Read all the sql text
            string sql = "";

            using (FileStream strm = File.OpenRead("drop.sql"))
            {
                StreamReader reader = new StreamReader(strm);
                sql = reader.ReadToEnd();
            }

            Console.WriteLine("Executing Connection and Query");
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                try
                {

                    Regex regex = new Regex("^GO", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    string[] lines = regex.Split(sql);

                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.Connection = connection;
                        cmd.Transaction = transaction;

                        foreach (string line in lines)
                        {
                            if (line.Length > 0)
                            {
                                cmd.CommandText = line;
                                cmd.CommandType = CommandType.Text;

                                try
                                {
                                    cmd.ExecuteNonQuery();
                                }
                                catch (SqlException)
                                {
                                    Console.WriteLine("Chances are Database does not Exist, or Sql Script is not up to date");
                                    transaction.Rollback();
                                    throw;
                                }
                            }
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: ");
                    Console.WriteLine(ex.ToString());
                }
                connection.Close();
            }
                

            Console.WriteLine("---------------------------------");
            Console.WriteLine("-            FINISHED           -");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Press any key to stop...");
            Console.ReadKey();
        }
    }
}
