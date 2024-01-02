using AzureWebApp.Models;
using System.Data.SqlClient;

namespace AzureWebApp.Services
{
    public interface IProductService
    {
        public SqlConnection GetConnection();
        public List<Product> GetProducts();
    }
}
