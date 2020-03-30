using Hospital.ProductCatalog.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Hospital.ProductCatalog.BusinessLogic.Products.Queries
{
    public class ProductDTO
    {
        public int Code { get; set; }

        public string Description { get; set; }

        public List<string> Barcodes { get; set; }

        public Category Category { get; set; }

        public int Quantity { get; set; }

        public string UnitOfMeasurement { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
