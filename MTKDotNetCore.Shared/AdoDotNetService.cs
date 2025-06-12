using System.Data;
using System.Data.SqlClient;

namespace MTKDotNetCore.Shared
{
    public class AdoDotNetService
    {
        private readonly string _connectionString;

        public class SqlParameterModel
        {
            public string Name { get; set; }

            public object Value { get; set; }
            
            public SqlParameterModel(string name, object value)
            {
                Name = name;
                Value = value;
            }
        }

        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable Query(string query, params SqlParameterModel[] sqlParameters) 
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(query, connection);

            if (sqlParameters is not null)
            {
                foreach (var param in sqlParameters)
                {
                    command.Parameters.AddWithValue(param.Name, param.Value);
                }
            }

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            connection.Open();

            adapter.Fill(dt);

            connection.Close();

            return dt;
        }

        public int QueryExecute(string query, params SqlParameterModel[] sqlParameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            
            if (sqlParameters is not null)
            {
                foreach (var param in sqlParameters)
                {
                    command.Parameters.AddWithValue(param.Name, param.Value);
                }
            }

            int result = 0;

            connection.Open();
            result = command.ExecuteNonQuery();
            connection.Close();

            return result;
        }
    }
}
