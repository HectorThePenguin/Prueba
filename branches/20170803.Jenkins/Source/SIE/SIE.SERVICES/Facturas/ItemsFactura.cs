namespace SIE.Services.Facturas
{
    public class ItemsFactura
    {
        public ItemsFactura()
        {
            LineNumber = "1";
            LineType = "LINE";
            InventoryItemId = "1";
            UomCode = "Kg";
            TaxRate = "0.00";
            TaxableAmount = "0.00";
        }
        public string LineNumber { get; set; }
        public string LineType { get; set; }
        public string InventoryItemId { get; set; }
        public string ItemEanNumber { get; set; }
        public string Serial { get; set; }
        public string Description { get; set; }
        public string QuantityInvoiced { get; set; }
        public string QuantityCredited { get; set; }
        public string UnitSellingPrice { get; set; }
        public string UomCode { get; set; }
        public string TaxRate { get; set; }
        public string TaxableAmount { get; set; }
        public string PrecioNeto { get; set; }
        public string Descuento { get; set; }
        public string VatTaxId { get; set; }
        public string CantidadBultos { get; set; }
    }
}
