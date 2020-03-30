using System;
using System.Collections.Generic;

namespace Hospital.ProductCatalog.Domain.Entities
{
    public class Product
    {
        public int Code { get; set; }

        public string Description { get; set; }

        public List<Barcode> Barcodes { get; set; }

        public Category Category { get; set; }

        public int Quantity { get; set; }

        public UnitOfMeasurement UnitOfMeasurement { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
