namespace SIE.Services.Info.Info
{
    public class IndicadorObjetivoSemaforoInfo
    {
        /// <summary>
        /// Clave del indicador
        /// </summary>
        public int IndicadorID { get; set; }
        /// <summary>
        /// Descripcion del indicador
        /// </summary>
        public string Indicador { get; set; }
        /// <summary>
        /// Medicion
        /// </summary>
        public string Medicion { get; set; }
        /// <summary>
        /// Clave del tipo objetivo de calidad
        /// </summary>
        public int TipoObjetivoCalidadID { get; set; }
        /// <summary>
        /// Descripcion del tipo objetivo calidad
        /// </summary>
        public string TipoObjetivoCalidad { get; set; }
        /// <summary>
        /// Clave del indicador objetivo
        /// </summary>
        public int IndicadorObjetivoID { get; set; }
        /// <summary>
        /// Valor minimo
        /// </summary>
        public decimal ObjetivoMinimo { get; set; }
        /// <summary>
        /// Valor maximo
        /// </summary>
        public decimal ObjetivoMaximo { get; set; }
        /// <summary>
        /// Valor de la tolerancia
        /// </summary>
        public decimal Tolerancia { get; set; }
        /// <summary>
        /// Clave del Color objetivo
        /// </summary>
        public int ColorObjetivoID { get; set; }
        /// <summary>
        /// Tendencia
        /// </summary>
        public string Tendencia { get; set; }
        /// <summary>
        /// Clave del color
        /// </summary>
        public string CodigoColor { get; set; }
        /// <summary>
        /// Descripcion del color
        /// </summary>
        public string ColorDescripcion { get; set; }
        /// <summary>
        /// Clave del pedido detalle
        /// </summary>
        public int PedidoDetalleID { get; set; }
        /// <summary>
        /// Rango que sera valido para el indicador
        /// </summary>
        public string Rango { get; set; }
        /// <summary>
        /// Color que sera mostrado en el semaforo
        /// </summary>
        public string ColorSemaforo { get; set; }
        /// <summary>
        /// Resultado para el indicador
        /// </summary>
        public string Resultado { get; set; }
        /// <summary>
        /// Identificador del color asignado al resultado
        /// </summary>
        public int ColorObjetivoIDValor { get; set; }
        /// <summary>
        /// Indica si es valido
        /// </summary>
        public int Valido { get; set; }
    }
}
