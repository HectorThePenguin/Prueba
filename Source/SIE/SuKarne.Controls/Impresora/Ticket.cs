using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;

namespace SuKarne.Controls.Impresora
{
    public class Ticket
    {
        private int contadorLineas;
        private int lineaActual;
        private Font fuenteLineaActual;
        private Graphics gfx;
        private IList<LineaImpresionInfo> lineas;

        /// <summary>
        /// Crea una instancia sin parametros
        /// </summary>
        public Ticket()
        {
            OpcionesImpresora = new OpcionesImpresora();
        }

        /// <summary>
        /// Crea una instanca de la clase con las opciones de impresora 
        /// </summary>
        /// <param name="opciones"></param>
        public Ticket(OpcionesImpresora opciones)
        {
            OpcionesImpresora = opciones;
        }

        public OpcionesImpresora OpcionesImpresora { get; set; }

        /// <summary>
        /// Imprime el listado de lineas 
        /// </summary>
        /// <param name="listalineas"></param>
        public void Imrpimir(IList<LineaImpresionInfo> listalineas)
        {
            lineas = listalineas;
            var pr = new PrintDocument
                {
                    PrinterSettings = {PrinterName = OpcionesImpresora.Impresora}
                };
            pr.PrintPage += (ImpresionPagina);
            if (pr.PrinterSettings.IsValid)
            {
                pr.Print();    
            }            
        }

        /// <summary>
        /// Imprime la pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImpresionPagina(object sender, PrintPageEventArgs e)
        {
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            gfx = e.Graphics;                        
            ImprimeLineas(e);
        }

        /// <summary>
        /// metodo que recorre todas las lineas para su impresion
        /// </summary>
        private void ImprimeLineas(PrintPageEventArgs e)
        {
            int contadorlineaPagina  = 0;
            contadorLineas = 0;
            bool hojaUnica = OpcionesImpresora.LineasPagina == 0;

            StringBuilder sb;
            var format = new StringFormat();
            LineaImpresionInfo linea;
            while (((contadorlineaPagina < OpcionesImpresora.LineasPagina && !hojaUnica) && lineaActual < lineas.Count) || (lineaActual < lineas.Count && hojaUnica))
            {
                linea = lineas[lineaActual];
                fuenteLineaActual = linea.Opciones.Fuente;
                if (linea.Texto.Length > OpcionesImpresora.MaximoLinea)
                {                    
                    int caracterActual = 0;
                    for (int longitud = linea.Texto.Length;
                         longitud > OpcionesImpresora.MaximoLinea;
                         longitud -= OpcionesImpresora.MaximoLinea)
                    {
                        sb = new StringBuilder();
                        sb.Append(linea.Texto.Substring(caracterActual, OpcionesImpresora.MaximoLinea));
                        gfx.DrawString(sb.ToString(), linea.Opciones.Fuente, linea.Opciones.ColorFuente,
                                       linea.Opciones.MargenIzquierdo, Posicion(), format);
                        contadorLineas++;
                        contadorlineaPagina++;
                        caracterActual += OpcionesImpresora.MaximoLinea;
                    }
                    sb = new StringBuilder();
                    sb.Append(linea.Texto);
                    gfx.DrawString(sb.ToString().Substring(caracterActual, sb.ToString().Length - caracterActual), linea.Opciones.Fuente,
                                   linea.Opciones.ColorFuente,
                                   linea.Opciones.MargenIzquierdo, Posicion(), format);
                    contadorLineas++;
                    contadorlineaPagina++;
                }
                else
                {
                    sb = new StringBuilder();
                    sb.Append(linea.Texto);
                    gfx.DrawString(sb.ToString(), linea.Opciones.Fuente, linea.Opciones.ColorFuente,
                                   linea.Opciones.MargenIzquierdo, Posicion(),
                                   format);
                    contadorLineas++;
                    contadorlineaPagina++;
                }
                lineaActual++;                
            }
            e.HasMorePages = lineaActual < lineas.Count;
        }

        /// <summary>
        /// Metodo que obtiene la posicion de la linea en donde va a imprimir
        /// </summary>
        /// <returns></returns>
        private float Posicion()
        {
            const int margenSuperior = 5;
            return margenSuperior + (contadorLineas*fuenteLineaActual.GetHeight(gfx));
        }
    }
}