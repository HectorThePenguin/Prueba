namespace SIE.Services.Info.Modelos
{
    public class CierreDiaInventarioPAMensajesModel
    {
        public string Producto { get; set; }
        public decimal MermaSuperavit { get; set; }
        public decimal Permitido { get; set; }
        public bool EsAutorizacion { get; set; }
    }
}
