using System.Collections.Generic;
using System.Linq;

//[assembly: InternalsVisibleTo("PointOfScale.Tests")]
namespace PointOfScale.Lib.Entities
{
    class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Product product)
        {
            CartLine line = lineCollection
                .Where(p => p.Product.Code == product.Code)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = 1
                });
            }
            else
            {
                line.Quantity += 1;
            }
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(line => line.CalculatePrice());
        }

    }
}
