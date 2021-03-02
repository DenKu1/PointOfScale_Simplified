using PointOfScale.Lib.API.DTOs;
using System.Collections.Generic;

namespace PointOfScale.Lib.API.Abstract
{
    interface IPointOfSaleTerminal
    {
        decimal CalculateTotal();
        void Scan(string productCode);
        void SetPricing(IEnumerable<ProductDTO> productDTOs);
    }
}
