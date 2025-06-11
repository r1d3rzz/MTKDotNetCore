using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MTKDotNetCore.ConsoleApp.Model;
using MTKDotNetCore.WebAPI.Models;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace MTKDotNetCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=DESKTOP-QREHFRH;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=rider;TrustServerCertificate=True";

        [HttpGet]
        public IActionResult Index()
        {
            List<BlogViewModel> blogs = new List<BlogViewModel>();
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand comm = new SqlCommand("SELECT * FROM Blog WHERE DeleteFlag != @deleteFlag", connection);
            comm.Parameters.AddWithValue("@deleteFlag", 1);
            connection.Open();

            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                blogs.Add(new BlogViewModel
                {
                    BlogId = (int)reader["BlogId"],
                    BlogTitle = reader["BlogTitle"] as string,
                    BlogAuthor = reader["BlogAuthor"] as string,
                    BlogContent = reader["BlogContent"] as string,
                    DeleteFlag = (bool)reader["DeleteFlag"]
                });
            }

            connection.Close();
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public IActionResult Show(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand comm = new SqlCommand("SELECT * FROM Blog WHERE BlogId = @id AND DeleteFlag != @deleteFlag", connection);
            comm.Parameters.AddWithValue("@id", id);
            comm.Parameters.AddWithValue("@deleteFlag", 1);
            connection.Open();

            BlogViewModel blog = new BlogViewModel();

            SqlDataAdapter dataAdapter = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            connection.Close();

            if (dt.Rows.Count == 0)
            {
                return NotFound("Blog not found or has been deleted.");
            }
            else
            {
                foreach (DataRow reader in dt.Rows)
                {
                    blog.BlogId = (int)reader["BlogId"];
                    blog.BlogTitle = reader["BlogTitle"] as string;
                    blog.BlogAuthor = reader["BlogAuthor"] as string;
                    blog.BlogContent = reader["BlogContent"] as string;
                    blog.DeleteFlag = (bool)reader["DeleteFlag"];
                }
            }

            return Ok(blog);
        }

        [HttpPost]
        public IActionResult Create(Blog blog)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            SqlCommand comm = new SqlCommand("INSERT INTO Blog (BlogTitle, BlogAuthor, BlogContent, DeleteFlag) VALUES (@BlogTitle, @BlogAuthor, @BlogContent, 0);", sqlConnection);
            comm.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle ?? (object)DBNull.Value);
            comm.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor ?? (object)DBNull.Value);
            comm.Parameters.AddWithValue("@BlogContent", blog.BlogContent ?? (object)DBNull.Value);
            sqlConnection.Open();

            int rowsAffected = comm.ExecuteNonQuery();

            sqlConnection.Close();

            if (rowsAffected > 0)
            {
                return Ok("Blog created successfully.");
            }
            else
            {
                return BadRequest("Failed to create blog.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogViewModel blog)
        {

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand comm = new SqlCommand("Update Blog SET BlogTitle = @BlogTitle, BlogAuthor = @BlogAuthor, BlogContent = @BlogContent, DeleteFlag = @DeleteFlag WHERE BlogId = @id", conn);
            comm.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle ?? (object)DBNull.Value);
            comm.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor ?? (object)DBNull.Value);
            comm.Parameters.AddWithValue("@BlogContent", blog.BlogContent ?? (object)DBNull.Value);
            comm.Parameters.AddWithValue("@DeleteFlag", blog.DeleteFlag);
            comm.Parameters.AddWithValue("@id", id);
            conn.Open();

            int rowEffect = comm.ExecuteNonQuery();

            conn.Close();

            if (rowEffect == 0)
            {
                return NotFound("Blog not found or has been deleted.");
            }
            else {
                return Ok("Blog updated successfully.");
            }
        }

        [HttpPatch("{id}")]
        public IActionResult PatchUpdate(int id, BlogViewModel blog)
        {
            string condition = string.Empty;    

            SqlConnection connection = new SqlConnection(_connectionString);

            if (blog.BlogTitle != null)
            {
                condition += "BlogTitle = @BlogTitle, ";
            }

            if (blog.BlogAuthor != null)
            {
                condition += "BlogAuthor = @BlogAuthor, ";
            }

            if (blog.BlogContent != null)
            {
                condition += "BlogContent = @BlogContent, ";
            }

            if (blog.DeleteFlag != false)
            {
                condition += "DeleteFlag = @DeleteFlag, ";
            }   

            if (string.IsNullOrEmpty(condition))
            {
                return BadRequest("No fields to update.");
            }

            condition = condition.TrimEnd(',', ' ');

            SqlCommand comm = new SqlCommand($"UPDATE Blog SET {condition} WHERE BlogId = @id", connection);

            comm.Parameters.AddWithValue("@id", id);
            if (blog.BlogTitle != null)
            {
                comm.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            }
            if (blog.BlogAuthor != null)
            {
                comm.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            }
            if (blog.BlogContent != null)
            {
                comm.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            }
            if (blog.DeleteFlag != false)
            {
                comm.Parameters.AddWithValue("@DeleteFlag", blog.DeleteFlag);
            }

            connection.Open();

            int rowEffect = comm.ExecuteNonQuery();

            connection.Close();

            if (rowEffect == 0)
            {
                return NotFound("Blog not found or has been deleted.");
            }
            else
            {
                return Ok("Blog updated successfully.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand comm = new SqlCommand("UPDATE Blog SET DeleteFlag = 1 WHERE BlogId = @id", connection);
            comm.Parameters.AddWithValue("@id", id);
            connection.Open();
            int rowEffect = comm.ExecuteNonQuery();
            connection.Close();

            if (rowEffect == 0)
            {
                return NotFound();
            }

            return Ok("Blog Delete Success");
        }
    }
}
