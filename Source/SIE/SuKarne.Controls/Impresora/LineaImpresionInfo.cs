namespace SuKarne.Controls.Impresora
{
    public class LineaImpresionInfo
    {
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public LineaImpresionInfo()
        {
            Opciones = new OpcionesLinea();
        }

        /// <summary>
        /// Constructor con texto de linea
        /// </summary>
        /// <param name="texto"></param>
        public LineaImpresionInfo(string texto)
        {
            Texto = texto;
            Opciones = new OpcionesLinea();
        }

        /// <summary>
        /// Constructor con texto y opciones de linea
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="opciones"></param>
        public LineaImpresionInfo(string texto, OpcionesLinea opciones)
        {
            Texto = texto;
            Opciones = opciones;
        }


        /// <summary>
        /// Texto de la linea
        /// </summary>
        public string Texto { get; set; }

        /// <summary>
        /// opciones de la linea
        /// </summary>
        public OpcionesLinea Opciones { get; set; }
    }
}