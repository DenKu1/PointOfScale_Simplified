using System.Collections.Generic;
using System.Linq;

namespace PointOfScale.Lib.Entities
{
    class ProductRange
    {
        private List<Product> products;

        public void LoadProducts(IEnumerable<Product> products)
        {
            this.products = products.ToList();
        }

        public Product GetByCode(string productCode) 
        {
            return products.FirstOrDefault(p => p.Code == productCode);
        }

    }
}
