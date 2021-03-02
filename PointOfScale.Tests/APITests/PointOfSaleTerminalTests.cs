using PointOfScale.Lib.API.Concrete;
using PointOfScale.Lib.API.DTOs;
using PointOfScale.Lib.API.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PointOfScale.Tests.APITests
{
    public class PointOfSaleTerminalTests
    {
        PointOfSaleTerminal terminal;

        List<ProductDTO> products;

        public PointOfSaleTerminalTests()
        {
            products = new List<ProductDTO>
            {
                new ProductDTO { Code = "A", RetailPrice = 1.25m, VolumePrice = 3m, VolumeQuantity = 3 },
                new ProductDTO { Code = "B", RetailPrice = 4.25m },
                new ProductDTO { Code = "C", RetailPrice = 1m, VolumePrice = 5m, VolumeQuantity = 6 },
                new ProductDTO { Code = "D", RetailPrice = 0.75m}
            };

            terminal = new PointOfSaleTerminal();

            terminal.SetPricing(products);
        }

        [Fact]
        public void Scan_NullProductCodeShouldRaiseException()
        {
            string productCode = null;

            Action act = () => terminal.Scan(productCode);

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(act);
            Assert.Equal(nameof(productCode), exception.ParamName);
        }

        [Fact]
        public void Scan_UnexistingProductCodeShouldRaiseException()
        {
            string productCode = "E";

            Action act = () => terminal.Scan(productCode);

            PointOfScaleException exception = Assert.Throws<PointOfScaleException>(act);
            Assert.Equal($"Unexisting product code: {productCode}", exception.Message);
        }

        [Fact]
        public void SetPricing_NullProductCodeShouldRaiseException()
        {
            List<ProductDTO> productDTOs = null;

            Action act = () => terminal.SetPricing(productDTOs);

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(act);
            Assert.Equal(nameof(productDTOs), exception.ParamName);
        }

        [Fact]
        public void SetPricing_NotUniqueProducCodesShouldRaiseException()
        {
            ProductDTO dublicateProduct = new ProductDTO { Code = "A" };

            products.Add(dublicateProduct);

            Action act = () => terminal.SetPricing(products);

            PointOfScaleException exception = Assert.Throws<PointOfScaleException>(act);
            Assert.Equal("Product codes are not unique", exception.Message);
        }

        [Theory]
        [InlineData(13.25, new string[] { "A", "B", "C", "D", "A", "B", "A" })]
        [InlineData(6.0, new string[] { "C", "C", "C", "C", "C", "C", "C" })]
        [InlineData(7.25, new string[] { "A", "B", "C", "D" })]
        public void CalculateTotal_ProductsCodesShouldHaveExpectedPrice(decimal expectedPrice, string[] productCodes)
        {
            productCodes.ToList().ForEach(productCode => terminal.Scan(productCode));

            var result = terminal.CalculateTotal();

            Assert.Equal(expectedPrice, result);
        }
    }
}
