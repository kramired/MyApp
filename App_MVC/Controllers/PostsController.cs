using App_MVC.Models;
using App_MVC.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Data;
using System.Data.SqlClient;

namespace App_MVC.Controllers
{
    public class PostsController : Controller
    {
        private DbConnection dbConnection;

        public PostsController(IConfiguration configuration)
        {
            dbConnection = DbConnection.Instance(configuration);
        }
        public IActionResult Index()
        {
            List<Posts> PostList = new List<Posts>();
            try
            {
                //String connectionString = "Data Source=DESKTOP-JF7JG7B;Initial Catalog=StackOverflow2010;Integrated Security=true;";

                using (SqlConnection sqlConnection = dbConnection.GetConnection())
                {
                    sqlConnection.Open();
                    String query = "Select top 10 * from Posts";
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        Posts post = new Posts();
                        post.Id = Convert.ToInt32(dataTable.Rows[i]["Id"]);
                        post.Title = Convert.ToString(dataTable.Rows[i]["Title"]);
                        post.OwnerUserId = Convert.ToInt32(dataTable.Rows[i]["OwnerUserId"]);
                        post.Score = Convert.ToInt32(dataTable.Rows[i]["Score"]);
                        PostList.Add(post);

                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception has occured" + ex);
            }
            return View(PostList);
            //return View();
        }

        public IActionResult Search(string query, int page = 1)
        {
            const int pageSize = 10;

            var searchResults = SearchPosts(query, page, pageSize);

            return PartialView("_Search", searchResults);
        }

        private List<Posts> SearchPosts(string query, int page, int pageSize)
        {
            List<Posts> searchResults = new List<Posts>();

            using (SqlConnection connection = dbConnection.GetConnection())
            {
                string sqlQuery = $"SELECT TOP (@PageSize) * FROM Posts WHERE Title LIKE @Query OR Body LIKE @Query ORDER BY Score DESC OFFSET (@Page - 1) * @PageSize ROWS";

                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@Query", $"%{query}%");
                command.Parameters.AddWithValue("@Page", page);
                command.Parameters.AddWithValue("@PageSize", pageSize);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Posts post = new Posts
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Title = Convert.ToString(reader["Title"]),
                            Body = Convert.ToString(reader["Body"]),
                            VoteCount = Convert.ToInt32(reader["VoteCount"]),
                            AnswerCount = Convert.ToInt32(reader["AnswerCount"]),
                            OwnerUserId = Convert.ToInt32(reader["OwnerUserId"]) // Assuming UserId is present in the Posts table
                            // Add other properties as needed
                        };

                        searchResults.Add(post);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Handle any exceptions
                }
            }

            return searchResults;
        }
    }
}
