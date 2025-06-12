using MTKDotNetCore.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MTKDotNetCore.Shared.AdoDotNetService;

namespace MTKDotNetCore.ConsoleApp
{
    public class ADONetExampleWithService
    {

        private readonly string _connectionString = "Data Source = DESKTOP-QREHFRH; Initial Catalog = DotNetTrainingBatch5; User ID = sa; Password = rider";

        private readonly AdoDotNetService _adoDotNetService;

        public ADONetExampleWithService()
        {
              _adoDotNetService = new AdoDotNetService(_connectionString);
        }

        public void Read()
        {
            string query = "SELECT * FROM Blog WHERE DeleteFlag != @deleteFlag";
            DataTable dt = _adoDotNetService.Query(query, new SqlParameterModel("@deleteFlag", 1));
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine(row["BlogId"]);
                Console.WriteLine(row["BlogTitle"]);
                Console.WriteLine(row["BlogAuthor"]);
                Console.WriteLine(row["BlogContent"]);
                Console.WriteLine(row["DeleteFlag"]);
                Console.WriteLine("=====================================");
            }
        }

        public void Show()
        {
            Console.Write("Ener Blog Id: ");
            int blogId = Convert.ToInt32(Console.ReadLine());
            string query = "SELECT * FROM Blog WHERE BlogId = @blogId";
            DataTable dt = _adoDotNetService.Query(query, new SqlParameterModel("@blogId", blogId));
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine(row["BlogId"]);
                Console.WriteLine(row["BlogTitle"]);
                Console.WriteLine(row["BlogAuthor"]);
                Console.WriteLine(row["BlogContent"]);
                Console.WriteLine(row["DeleteFlag"]);
                Console.WriteLine("=====================================");
            }
        }

        public void Store()
        {
            Console.Write("Enter Blog Title: ");
            string blogTitle = Console.ReadLine();
            Console.Write("Enter Blog Author: ");
            string blogAuthor = Console.ReadLine();
            Console.Write("Enter Blog Content: ");
            string blogContent = Console.ReadLine();
            string query = "INSERT INTO Blog (BlogTitle, BlogAuthor, BlogContent, DeleteFlag) VALUES (@blogTitle, @blogAuthor, @blogContent, @deleteFlag)";
            int result = _adoDotNetService.QueryExecute(query,
                new SqlParameterModel("@blogTitle", blogTitle),
                new SqlParameterModel("@blogAuthor", blogAuthor),
                new SqlParameterModel("@deleteFlag", 0),
                new SqlParameterModel("@blogContent", blogContent));
            if (result > 0)
            {
                Console.WriteLine("Blog added successfully.");

                DataTable dt = _adoDotNetService.Query("SELECT top 1 * FROM Blog Order By BlogId DESC");
                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine("BlogId: " + row["BlogId"]);
                    Console.WriteLine("BlogTitle: " + row["BlogTitle"]);
                    Console.WriteLine("BlogAuthor: " + row["BlogAuthor"]);
                    Console.WriteLine("BlogContent: " + row["BlogContent"]);
                    Console.WriteLine("DeleteFlag: " + row["DeleteFlag"]);
                    Console.WriteLine("=====================================");
                }
            }
            else
            {
                Console.WriteLine("Failed to add blog.");
            }
        }

        public void Edit()
        {
            Console.Write("Enter Blog Id to Edit: ");
            int blogId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter New Blog Title: ");
            string blogTitle = Console.ReadLine();
            Console.Write("Enter New Blog Author: ");
            string blogAuthor = Console.ReadLine();
            Console.Write("Enter New Blog Content: ");
            string blogContent = Console.ReadLine();
            string query = "UPDATE Blog SET BlogTitle = @blogTitle, BlogAuthor = @blogAuthor, BlogContent = @blogContent, DeleteFlag = @deleteFlag WHERE BlogId = @blogId";
            int result = _adoDotNetService.QueryExecute(query,
                new SqlParameterModel("@blogId", blogId),
                new SqlParameterModel("@blogTitle", blogTitle),
                new SqlParameterModel("@blogAuthor", blogAuthor),
                new SqlParameterModel("@deleteFlag", 0),
                new SqlParameterModel("@blogContent", blogContent));
            if (result > 0)
            {
                Console.WriteLine("Blog updated successfully.");
                Show();
            }
            else
            {
                Console.WriteLine("Failed to update blog.");
            }
        }
    }
}
