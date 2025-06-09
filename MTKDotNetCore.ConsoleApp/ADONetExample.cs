using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTKDotNetCore.ConsoleApp
{
    public class ADONetExample
    {
        readonly string _connectionString = "Data Source = DESKTOP-QREHFRH; Initial Catalog = DotNetTrainingBatch5; User ID = sa; Password = rider";

        public void Read()
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            Console.WriteLine("Connection Opening");
            sqlConnection.Open();
            Console.WriteLine("Connection Opened");

            SqlCommand comm = new SqlCommand("SELECT * FROM Blog WHERE DeleteFlag != @deleteFlag", sqlConnection);
            //SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(comm);
            comm.Parameters.AddWithValue("@deleteFlag", 1);
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(reader["BlogId"]);
                Console.WriteLine(reader["BlogTitle"]);
                Console.WriteLine(reader["BlogAuthor"]);
                Console.WriteLine(reader["BlogContent"]);
                Console.WriteLine(reader["DeleteFlag"]);
                Console.WriteLine("=====================================");
            }

            //DataTable dt = new DataTable();
            //sqlDataAdapter.Fill(dt);

            Console.WriteLine("Connection Closing");
            sqlConnection.Close();
            Console.WriteLine("Connection Closed");

            //foreach (DataRow row in dt.Rows)
            //{
            //    Console.WriteLine(row["BlogId"]);
            //    Console.WriteLine(row["BlogTitle"]);
            //    Console.WriteLine(row["BlogAuthor"]);
            //    Console.WriteLine(row["BlogContent"]);
            //    Console.WriteLine(row["DeleteFlag"]);
            //}

            Console.ReadKey();
        }

        public void Create()
        {
            Console.Write("Enter Blog Title: ");
            string blogTitle = Console.ReadLine();

            Console.Write("Enter Blog Author: ");
            string blogAuthor = Console.ReadLine();

            Console.Write("Enter Blog Content: ");
            string blogContent = Console.ReadLine();

            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            Console.WriteLine("Connection Opening");
            sqlConnection.Open();
            Console.WriteLine("Connection Opened");

            SqlCommand comm = new SqlCommand("INSERT INTO Blog (BlogTitle, BlogAuthor, BlogContent, DeleteFlag) VALUES (@title, @author, @content, 0)", sqlConnection);
            comm.Parameters.AddWithValue("@title", blogTitle);
            comm.Parameters.AddWithValue("@author", blogAuthor);
            comm.Parameters.AddWithValue("@content", blogContent);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(comm);

            int rowsAffected = comm.ExecuteNonQuery();

            Console.WriteLine("Connection Closing");
            sqlConnection.Close();
            Console.WriteLine("Connection Closed");

            Console.WriteLine(rowsAffected == 1 ? "Saving Success" : "Got Error");
        }

        public void Edit()
        {
            Console.Write("Enter Blog Id to Edit: ");
            string blogId = Console.ReadLine();

            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            Console.WriteLine("Connection Opening");
            sqlConnection.Open();
            Console.WriteLine("Connection Opened");

            SqlCommand comm = new SqlCommand("SELECT * FROM Blog WHERE BlogId = @blogId", sqlConnection);

            comm.Parameters.AddWithValue("@blogId", blogId);

            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                Console.WriteLine(reader["BlogId"]);
                Console.WriteLine(reader["BlogTitle"]);
                Console.WriteLine(reader["BlogAuthor"]);
                Console.WriteLine(reader["BlogContent"]);
                Console.WriteLine(reader["DeleteFlag"]);
                Console.WriteLine("=====================================");

                Console.WriteLine("Connection Closing");
                sqlConnection.Close();
                Console.WriteLine("Connection Closed");
            }
            else
            {
                Console.WriteLine("No blog found with the given ID.");
                sqlConnection.Close();
                return;
            }

            Console.Write("Are you sure to update Y/N");
            string isEditing = Console.ReadLine();

            if (isEditing.ToLowerInvariant() == "y")
            {

                Console.WriteLine("Connection Opening");
                sqlConnection.Open();
                Console.WriteLine("Connection Opened");

                Console.Write("Enter Blog Title: ");
                string blogTitle = Console.ReadLine();

                Console.Write("Enter Blog Author: ");
                string blogAuthor = Console.ReadLine();

                Console.Write("Enter Blog Content: ");
                string blogContent = Console.ReadLine();

                string query = "UPDATE Blog SET BlogTitle = @title, BlogAuthor = @author, BlogContent = @content, DeleteFlag = 0 WHERE BlogId = @blogId";
                SqlCommand updateSqlComm = new SqlCommand(query, sqlConnection);
                updateSqlComm.Parameters.AddWithValue("@title", blogTitle);
                updateSqlComm.Parameters.AddWithValue("@author", blogAuthor);
                updateSqlComm.Parameters.AddWithValue("@content", blogContent);
                updateSqlComm.Parameters.AddWithValue("@blogId", blogId);

                int updateRowEffected = updateSqlComm.ExecuteNonQuery();
                Console.WriteLine(updateRowEffected == 1 ? "Update Success" : "Got Error");
            }
            else
            {
                Console.WriteLine("Editing Cancelled");
                sqlConnection.Close();
                return;
            }
            Console.WriteLine("Connection Closing");
            sqlConnection.Close();
            Console.WriteLine("Connection Closed");
        }

        public void Delete()
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            Console.WriteLine("Connection Opening");
            sqlConnection.Open();
            Console.WriteLine("Connection Opened");

            SqlCommand comm = new SqlCommand("SELECT * FROM Blog", sqlConnection);
            SqlDataReader reader = comm.ExecuteReader();

            int count = 1;

            while (reader.Read())
            {
                Console.WriteLine( count + ". " + reader["BlogTitle"]);
                Console.WriteLine("=====================================");
                count++;
            }

            Console.WriteLine("Connection Closing");
            sqlConnection.Close();
            Console.WriteLine("Connection Closed");


            Console.Write("Enter Blog Id to Delete: ");
            string blogId = Console.ReadLine();

            Console.WriteLine("Connection Opening");
            sqlConnection.Open();
            Console.WriteLine("Connection Opened");

            SqlCommand delComm = new SqlCommand("DELETE FROM Blog WHERE BlogId = @blogId", sqlConnection);
            delComm.Parameters.AddWithValue("@blogId", blogId);
            
            int delRowEffected = delComm.ExecuteNonQuery();

            Console.WriteLine("Connection Closing");
            sqlConnection.Close();
            Console.WriteLine("Connection Closed");

            Console.WriteLine(delRowEffected == 1 ? "Delete Success" : "Got Error");

            Console.ReadKey();
        }
    }
}
