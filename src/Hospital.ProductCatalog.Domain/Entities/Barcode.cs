namespace Hospital.ProductCatalog.Domain.Entities
{
    public class Barcode
    {
        // Abstracted barcode into its own class. Allthough It is in fact just a string.
        // Barcodes might have special validation rules (checksum) and I think that overall
        // it is better than using primitives every where. 

        public string Code { get; set; }
        public int ProductCode { get; set; }

        public override string ToString()
        {
            return Code;
        }
    }
}
