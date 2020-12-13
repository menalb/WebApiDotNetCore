using System.Collections.Generic;
using System.Linq;
using Bogus;

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
        private readonly IEnumerable<Product> _productsDB;
        public ProductQueryDb(IEnumerable<Product> products) => _productsDB = products;

        public IEnumerable<Product> GetAll() => _productsDB;

        public Product Get(int id) => _productsDB.FirstOrDefault(produc => produc.Id == id);

        public Product GetByEan(string ean) => _productsDB.FirstOrDefault(product => product.EAN == ean);
    }
}