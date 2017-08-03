namespace SIE.Services.Info.Info
{
    public class CausaSalidaInfo : BitacoraInfo
    {
        private string descripcion;
        /// <summary>
        /// Identificador de la tabla
        /// </summary>
        public int CausaSalidaID { get; set; }

        /// <summary>
        /// Descripción de la Causa Salida
        /// </summary>
        public string Descripcion
        {
            get { return descripcion != null ? descripcion.Trim() : descripcion; }
            set
            {
                if (value != descripcion)
                {
                    descripcion = value != null ? value.Trim() : null;
                }
            }
        }

        /// <summary>
        /// Tipo Movimiento de la Causa Salida
        /// </summary>
        public TipoMovimientoInfo TipoMovimiento { get; set; }
    }
}
