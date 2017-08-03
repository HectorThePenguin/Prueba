namespace SIE.Services.Polizas.Modelos
{
    public class PolizaEncabezadoModel
    {
        /// <summary>
        /// Encabezado de la seccion de la poliza
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Desplazamiento de columnas
        /// </summary>
        public int Desplazamiento { get; set; }
        /// <summary>
        /// Indica la alineacion del campo
        /// </summary>
        public string Alineacion { get; set; }
    }
}
