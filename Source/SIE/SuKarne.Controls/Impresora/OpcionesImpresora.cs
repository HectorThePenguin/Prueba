namespace SuKarne.Controls.Impresora
{
    public class OpcionesImpresora
    {
        /// <summary>
        /// Constructor por default
        /// </summary>
        public OpcionesImpresora()
        {
            MaximoLinea = 35;
        }

        /// <summary>
        /// Opciones de impresora con nombre de impresora
        /// </summary>
        /// <param name="impresora"></param>
        public OpcionesImpresora(string impresora)
        {
            MaximoLinea = 35;
            Impresora = impresora;
        }

        /// <summary>
        /// Opciones de impresora con nombre y maximo de linea 
        /// </summary>
        /// <param name="impresora"></param>
        /// <param name="maximoLinea"></param>
        public OpcionesImpresora(string impresora, int maximoLinea)
        {
            MaximoLinea = maximoLinea;
            Impresora = impresora;
        }

        /// <summary>
        /// Nombre de la impresora en la que se imprimirá 
        /// </summary>
        public string Impresora { get; set; }

        /// <summary>
        /// Numero maximo de caracteres por linea
        /// </summary>
        public int MaximoLinea { get; set; }

        /// <summary>
        /// Indica el numero maximo de lineas por pagina
        /// </summary>
        public int LineasPagina { get; set; }
        
    }
}