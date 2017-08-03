namespace SIE.Services.Info.Info
{
    public class TipoRutaInfo
    {
        /// <summary>
        ///     Identificador tipo de ruta
        /// </summary>
        public int TipoRutaID { get; set; }

        /// <summary>
        ///     Descipción del tipo de embarque
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        ///     Indica si el registro se encuentra Activo
        /// </summary>
        public Enums.EstatusEnum Activo { get; set; }
    }
}
