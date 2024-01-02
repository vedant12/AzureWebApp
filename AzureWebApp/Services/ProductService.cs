using AzureWebApp.Models;
using System.Data.SqlClient;

namespace AzureWebApp.Services
{
    public class ProductService : IProductService
    {
        private static string db_Source = "vedantmalaikar.database.windows.net";
        private static string db_user = "vedantmalaikar";
        private static string db_password = "Constantine@123";
        private static string db_database = "vedantmalaikarappdb";

        public SqlConnection GetConnection()
        {
            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = db_Source;
            _builder.UserID = db_user;
            _builder.Password = db_password;
            _builder.InitialCatalog = db_database;
            return new SqlConnection(_builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            var _products = new List<Product>();

            SqlConnection con = GetConnection();
            
            string statement = "Select ProductID, ProductName, Quantity from Products";
            con.Open();
            SqlCommand command = new SqlCommand(statement, con);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Product product = new Product();
                product.ProductID = reader.GetInt32(0);
                product.ProductName = reader.GetString(1);
                product.Quantity = reader.GetInt32(2);

                _products.Add(product);
            }

            con.Close();

            return _products;
        }
    }
}
