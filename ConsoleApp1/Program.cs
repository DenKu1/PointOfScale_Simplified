using PointOfScale.Lib.API.Concrete;
using PointOfScale.Lib.API.DTOs;
using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var products = new List<ProductDTO> 
            {
                new ProductDTO { Code = "A", RetailPrice = 1.25m, VolumePrice = 3m, VolumeQuantity = 3 },
                new ProductDTO { Code = "B", RetailPrice = 4.25m },
                new ProductDTO { Code = "C", RetailPrice = 1m, VolumePrice = 5m, VolumeQuantity = 6 },
                new ProductDTO { Code = "D", RetailPrice = 0.75m}
            };

            PointOfSaleTerminal terminal1 = new PointOfSaleTerminal();

            terminal1.SetPricing(products);
            terminal1.Scan("A");
            terminal1.Scan("B");
            terminal1.Scan("C");
            terminal1.Scan("D");
            terminal1.Scan("A");
            terminal1.Scan("B");
            terminal1.Scan("A");

            Console.WriteLine($"ABCDABA - 13.25???{terminal1.CalculateTotal()}");

            PointOfSaleTerminal terminal2 = new PointOfSaleTerminal();

            terminal2.SetPricing(products);
            terminal2.Scan("C");
            terminal2.Scan("C");
            terminal2.Scan("C");
            terminal2.Scan("C");
            terminal2.Scan("C");
            terminal2.Scan("C");
            terminal2.Scan("C");

            Console.WriteLine($"CCCCCCC - 6???{terminal2.CalculateTotal()}");

            PointOfSaleTerminal terminal3 = new PointOfSaleTerminal();

            terminal3.SetPricing(products);
            terminal3.Scan("A");
            terminal3.Scan("B");
            terminal3.Scan("C");
            terminal3.Scan("D");

            Console.WriteLine($"ABCD - 7.25???{terminal3.CalculateTotal()}");

            Console.ReadLine();
        }
    }
}
