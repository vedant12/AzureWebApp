using AzureWebApp.Models;
using System.Data.SqlClient;

namespace AzureWebApp.Services
{
    public class ProductService : IProductService
    {
        public IConfiguration _configuration { get; }
        public ProductService(IConfiguration configuration) 
        {
            _configuration = configuration;
        }


        public SqlConnection GetConnection()
        {
            var _builder = new SqlConnectionStringBuilder();
            return new SqlConnection(_configuration.GetConnectionString("SQLConnection"));
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
