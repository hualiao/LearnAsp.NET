using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CSharp.SQLHelper
{
    class SqlCommand
    {
        private static string _connection;

        public static void BatchInsert()
        {
            // how to get App Connecting string 
            // Ref:http://www.codeproject.com/Tips/416198/How-to-get-Connection-String-from-App-Config-in-Cs
            _connection = System.Configuration.ConfigurationManager.ConnectionStrings["AliCloud"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "insert into Person(Name,Gender,Address) values(@Name1,@Gender1,@Address1); insert into Person(Name,Gender,Address) values(@Name1,@Gender1,@Address2);";
                SqlParameter[] parameters = { 
                                                new SqlParameter("@Name1","liao"),
                                                new SqlParameter("@Gender1",1),
                                                new SqlParameter("@Address1","厦门"),
                                                new SqlParameter("@Address2","福建")
                                            };
                command.Parameters.AddRange(parameters);
                connection.Open();
                int result = command.ExecuteNonQuery();
            }
        }
    }
}
