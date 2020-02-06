using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using backend.Models;
namespace backend
{
    public class UserManager
    {
        private const string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=UserDB;Integrated Security=True";
        private SqlConnection connection;

        public UserManager()
        {
            connection = new SqlConnection(connectionString);
        }

        public List<UserModel> Get()
        {
            List<UserModel> users = new List<UserModel>();

            try
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "select * from Users";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int i = 0;
                            var item = new UserModel
                            {
                                Id = reader.GetInt32(i++),
                                FirstName = reader.GetString(i++),
                                LastName = reader.GetString(i++)
                            };

                            users.Add(item);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                connection.Close();
            }

            return users;
        }


        public int Add(Model model)
        {
            int newItemId;
            try
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = string.Format("insert into Users (FirstName, LastName) values ('{0}', '{1}')",model.FirstName, model.LastName);

                    cmd.ExecuteNonQuery();
       

                    cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                    newItemId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                connection.Close();
            }

            return newItemId;
        }
    }

}
