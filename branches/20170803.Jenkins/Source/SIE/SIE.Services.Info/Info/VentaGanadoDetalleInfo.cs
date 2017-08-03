namespace SIE.Services.Info.Info
{
    public class VentaGanadoDetalleInfo
    {
        public int VentaGanadoDetalleID { get; set; }
        public int VentaGanadoID { get; set; }
        public string Arete { get; set; }
        public string AreteMetalico { get; set; }
        public string FotoVenta { get; set; }
        public int CausaPrecioID { get; set; }
        public bool Activo { get; set; }
        /// <summary>
        /// INformaicon del animal
        /// </summary>
        public AnimalInfo Animal { get; set; }
    }
}
