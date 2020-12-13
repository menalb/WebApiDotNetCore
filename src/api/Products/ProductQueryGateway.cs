using System.Collections.Generic;
using System.Linq;

namespace ProductsApi.Product
{
    public interface ProductQuery
    {
        IEnumerable<Product> GetAll();
        Product Get(int id);
        Product GetByEan(string ean);
    }
    public class ProductQueryDb : ProductQuery
    {
        public IEnumerable<Product> GetAll() => products;

        public Product Get(int id) => products.FirstOrDefault(produc => produc.Id == id);

        public Product GetByEan(string ean) => products.FirstOrDefault(product => product.EAN == ean);

        private static readonly IEnumerable<Product> products = new List<Product>{
            new Product{Id=1,Name="Pasta", EAN="123456789"},
            new Product{Id=2,Name="Tuna", EAN="987654321"},
            new Product{Id=3,Name="Butter", EAN="63728291"}
        };
    }
}