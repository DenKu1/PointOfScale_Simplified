using PointOfScale.Lib.API.Abstract;
using PointOfScale.Lib.API.DTOs;
using PointOfScale.Lib.API.Exceptions;
using PointOfScale.Lib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PointOfScale.Lib.API.Concrete
{
    public class PointOfSaleTerminal : IPointOfSaleTerminal
    {
        private ProductRange productRange = new ProductRange();
        private Cart cart = new Cart();

        public void Scan(string productCode)
        {
            if (productCode is null)
            {
                throw new ArgumentNullException(nameof(productCode));
            }

            var product = productRange.GetByCode(productCode);

            if (product == null)
            {
                throw new PointOfScaleException($"Unexisting product code: {productCode}");
            }

            cart.AddItem(product);
        }

        public void SetPricing(IEnumerable<ProductDTO> productDTOs)
        {
            if (productDTOs is null)
            {
                throw new ArgumentNullException(nameof(productDTOs));
            }

            if (productDTOs.Select(p => p.Code).Distinct().Count() != productDTOs.Count())
            {
                throw new PointOfScaleException("Product codes are not unique");
            }

            var products = productDTOs.Select(dto => new Product
            {
                Code = dto.Code,
                RetailPrice = dto.RetailPrice,
                VolumePrice = dto.VolumePrice,
                VolumeQuantity = dto.VolumeQuantity
            });

            productRange.LoadProducts(products);
        }

        public decimal CalculateTotal()
        {
            return cart.ComputeTotalValue();
        }
    }
}
