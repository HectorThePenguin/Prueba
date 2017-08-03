using System.Drawing;

namespace SuKarne.Controls.Impresora
{
    public class OpcionesLinea
    {
        /// <summary>
        /// Constructor por defecto 
        /// </summary>
        public OpcionesLinea()
        {
            ColorFuente = new SolidBrush(Color.Black);
            Fuente = new Font("Lucida Console", 9, FontStyle.Regular);
            MargenIzquierdo = 0;
        }

        /// <summary>
        /// Color de la funte
        /// </summary>
        public SolidBrush ColorFuente { get; set; }

        /// <summary>
        /// Fuente de la liena
        /// </summary>
        public Font Fuente { get; set; }

        /// <summary>
        /// Margen izquierdo de la linea 
        /// </summary>
        public int MargenIzquierdo { get; set; }       

    }
}