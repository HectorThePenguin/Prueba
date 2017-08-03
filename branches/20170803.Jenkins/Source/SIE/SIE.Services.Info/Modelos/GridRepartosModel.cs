namespace SIE.Services.Info.Modelos
{
    public class GridRepartosModel
    {
        public int RepartoAlimentoID { get; set; }
        public int RepartoAlimentoDetalleID { get; set; }
        public string NumeroTolva { get; set; }
        public int Servicio { get; set; }
        public int Reparto { get; set; }
        public int RacionFormula { get; set; }
        public int KilosEmbarcados { get; set; }
        public int KilosRepartidos { get; set; }
        public int Sobrante { get; set; }
        public string CorralInicio { get; set; }
        public string CorralFinal { get; set; }
        public string HoraInicioReparto { get; set; }
        public string HoraFinalReparto { get; set; }
        public string TiempoTotalViaje { get; set; }
        public int UsuarioCreacionID { get; set; }
        public string Observaciones { get; set; }
        public int PesoFinal { get; set; }
    }
}
