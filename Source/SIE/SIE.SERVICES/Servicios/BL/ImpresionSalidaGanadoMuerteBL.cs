using iTextSharp.text;
using iTextSharp.text.pdf;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Color = iTextSharp.text.Color;
using Font = iTextSharp.text.Font;
using Rectangle = iTextSharp.text.Rectangle;

namespace SIE.Services.Servicios.BL
{
    internal class ImpresionSalidaGanadoMuerteBL
    {
        private Font fuenteDatos;
        private Font fuenteDatosNegrita;
        private Font fuenteTitulo;
        private Font fuenteTituloSeccion;

        private int paginaActual;
        private int paginaFinal;

        const string nombreArchivo = "OrdenSalidaMuerteGanado.pdf";

        /// <summary>
        /// Inicializa las Fuentes utilizadas para el reporte
        /// </summary>
        private void InicializaFuentes()
        {
            fuenteDatos = new Font(Font.HELVETICA, 9, Font.NORMAL, new Color(0, 0, 0));
            fuenteDatosNegrita = new Font(Font.HELVETICA, 9, Font.BOLD, new Color(0, 0, 0));
            fuenteTituloSeccion = new Font(Font.HELVETICA, 12, Font.BOLD, new Color(0, 0, 0));
            fuenteTitulo = new Font(Font.HELVETICA, 14, Font.BOLD, new Color(0, 0, 0));
        }


        /// <summary>
        /// Crea el reporte de salida de ganado por muerte y lo muestra en pantalla
        /// </summary>
        /// <param name="etiquetas"></param>
        /// <param name="listaMuertes"></param>
        internal void CrearReporte(ImpresionSalidaGanadoMuertoInfo etiquetas, IList<SalidaGanadoMuertoInfo> listaMuertes)
        {
            InicializaFuentes();
            paginaActual = 1;
            paginaFinal = 1;

            var reporte = new Document(PageSize.A4, 10, 10, 75, 65);
            try
            {
                CalcularPaginaFinal(listaMuertes.Count);

                PdfWriter.GetInstance(reporte, new FileStream(nombreArchivo, FileMode.Create));

                reporte.Open();

                float[] medidaCeldas = { 20 };
                var tablePrincipal = new PdfPTable(1);

                tablePrincipal.SetWidths(medidaCeldas);

                //Encabezado
                tablePrincipal.AddCell(new PdfPCell(ObtenerEncabezado(etiquetas)) {  
                    Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER, 
                    BorderWidth = 1, 
                    BackgroundColor = new Color(192,192,192)
                });
                tablePrincipal.AddCell(new PdfPCell(ObtenerEspacios(2)) { Border = 0});
                //Folio
                tablePrincipal.AddCell(new PdfPCell(ObtenerFolio(etiquetas)){
                    Border = 0,
                    HorizontalAlignment = Cell.ALIGN_RIGHT
                });
                tablePrincipal.AddCell(new PdfPCell(ObtenerEspacios(2)) { Border = 0 });
                //Fecha y Campos en Blanco
                tablePrincipal.AddCell(new PdfPCell(ObtenerCamposFecha(etiquetas)) {
                    Border = 0
                });
                tablePrincipal.AddCell(new PdfPCell(ObtenerEspacios(2)) { Border = 0 });
                //Muertes
                var listaTablasMuertes = ObtenerMuertes(etiquetas, listaMuertes);
                foreach (var tabla in listaTablasMuertes)
                {
                    tablePrincipal.AddCell(new PdfPCell(tabla) { Border = 0 });
                }


                /*if (paginaFinal > 1)
                {
                    //Paginado
                    tablePrincipal.AddCell(new PdfPCell(ObtenerNumeroPagina())
                    {
                        Border = 0,
                        HorizontalAlignment = Cell.ALIGN_RIGHT
                    });
                }*/

                tablePrincipal.AddCell(new PdfPCell(ObtenerEspacios(4)) { Border = 0 });
                //Firmas
                tablePrincipal.AddCell(new PdfPCell(ObtenerFirmas(etiquetas)){ Border = 0 });

                reporte.Add(tablePrincipal);

                reporte.Close();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                reporte.Close();
            }
        }

        /// <summary>
        /// Obtiene las tablas por pagina de una lista de muertes
        /// </summary>
        /// <param name="etiquetas"></param>
        /// <param name="listaMuertes"></param>
        /// <returns></returns>
        private List<PdfPTable> ObtenerMuertes(ImpresionSalidaGanadoMuertoInfo etiquetas, IList<SalidaGanadoMuertoInfo> listaMuertes)
        {
            float[] medidas = { 0.5f, 1.2f, 1.2f, 0.7f, 1.3f, 0.5f, 1.3f };
            var listaTablas = new List<PdfPTable>();
            var tblEspacios = new PdfPTable(7);
            int fila = 1;
            tblEspacios.SetWidths(medidas);
            //Encabezados
            ObtenerMuertesEncabezado(etiquetas, tblEspacios);
            foreach (SalidaGanadoMuertoInfo muerteInfo in listaMuertes)
            {
                var cell = new PdfPCell(new Phrase(muerteInfo.CodigoCorral, fuenteDatos)) { BorderWidth = 0.5f };
                tblEspacios.AddCell(cell);
                cell = new PdfPCell(new Phrase(muerteInfo.Arete, fuenteDatos)) { BorderWidth = 0.5f, HorizontalAlignment = Cell.ALIGN_RIGHT };
                tblEspacios.AddCell(cell);
                cell = new PdfPCell(new Phrase(muerteInfo.AreteTestigo, fuenteDatos)) { BorderWidth = 0.5f, HorizontalAlignment = Cell.ALIGN_RIGHT };
                tblEspacios.AddCell(cell);
                cell = new PdfPCell(new Phrase(muerteInfo.Sexo, fuenteDatos)) { BorderWidth = 0.5f };
                tblEspacios.AddCell(cell);
                cell = new PdfPCell(new Phrase(muerteInfo.TipoGanado, fuenteDatos)) { BorderWidth = 0.5f };
                tblEspacios.AddCell(cell);
                cell = new PdfPCell(new Phrase(muerteInfo.Peso.ToString("N0"), fuenteDatos)) { BorderWidth = 0.5f, HorizontalAlignment = Cell.ALIGN_RIGHT };
                tblEspacios.AddCell(cell);
                cell = new PdfPCell(new Phrase(muerteInfo.Causa, fuenteDatos)) { BorderWidth = 0.5f };
                tblEspacios.AddCell(cell);
                fila++;
                if ((listaTablas.Count == 0 && fila > 38) || (fila > 40))
                {
                    listaTablas.Add(tblEspacios);
                    
                    /*if (paginaFinal > 1)
                    {
                        //Paginado
                        cell = new PdfPCell(ObtenerNumeroPagina())
                        {
                            Border = 0,
                            HorizontalAlignment = Cell.ALIGN_RIGHT
                        };
                        var tablaPagina = new PdfPTable(1);
                        tablaPagina.AddCell(cell);
                        listaTablas.Add(tablaPagina);
                    }*/

                    //Encabezado
                    cell = new PdfPCell(ObtenerEncabezado(etiquetas))
                    {
                        Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER,
                        BorderWidth = 2,
                        BackgroundColor = new Color(192, 192, 192)
                    };
                    var tblEncabezado = new PdfPTable(1);
                    tblEncabezado.AddCell(cell);
                    listaTablas.Add(tblEncabezado);
                  
                    listaTablas.Add(ObtenerEspacios(2));
                    //Folio
                    listaTablas.Add(ObtenerFolio(etiquetas));
                  
                    listaTablas.Add(ObtenerEspacios(2));

                    tblEspacios = new PdfPTable(7);
                    fila = 1;
                    tblEspacios.SetWidths(medidas);
                    ObtenerMuertesEncabezado(etiquetas, tblEspacios);
                }
            }
            if (tblEspacios.Rows.Count > 1)
            {
                listaTablas.Add(tblEspacios);
            }
            return listaTablas;
        }

        private void CalcularPaginaFinal(int totalMuertes)
        {
            var primeraPagina = true;
            var fila = 1;
            for (int i = 1; i < totalMuertes; i++)
			{
                if ((primeraPagina && fila > 38) || (fila > 40))
                {
                    primeraPagina = false;
                    fila = 1;
                    paginaFinal++;
                }
                fila++;
			}   
        }

        /// <summary>
        /// Obtiene el encabezado para la tabla de muertes
        /// </summary>
        /// <param name="etiquetas"></param>
        /// <param name="tabla"></param>
        private void ObtenerMuertesEncabezado(ImpresionSalidaGanadoMuertoInfo etiquetas, PdfPTable tabla)
        {
            var cell = new PdfPCell(new Phrase(etiquetas.clmCorral, fuenteDatosNegrita)) { BorderWidth = 0.5f, HorizontalAlignment = Cell.ALIGN_CENTER, BackgroundColor = new Color(192, 192, 192) };
            tabla.AddCell(cell);
            cell = new PdfPCell(new Phrase(etiquetas.clmArete, fuenteDatosNegrita)) { BorderWidth = 0.5f, HorizontalAlignment = Cell.ALIGN_CENTER, BackgroundColor = new Color(192, 192, 192) };
            tabla.AddCell(cell);
            cell = new PdfPCell(new Phrase(etiquetas.clmAreteTestigo, fuenteDatosNegrita)) { BorderWidth = 0.5f, HorizontalAlignment = Cell.ALIGN_CENTER, BackgroundColor = new Color(192, 192, 192) };
            tabla.AddCell(cell);
            cell = new PdfPCell(new Phrase(etiquetas.clmSexo, fuenteDatosNegrita)) { BorderWidth = 0.5f, HorizontalAlignment = Cell.ALIGN_CENTER, BackgroundColor = new Color(192, 192, 192) };
            tabla.AddCell(cell);
            cell = new PdfPCell(new Phrase(etiquetas.clmTipoGanado, fuenteDatosNegrita)) { BorderWidth = 0.5f, HorizontalAlignment = Cell.ALIGN_CENTER, BackgroundColor = new Color(192, 192, 192) };
            tabla.AddCell(cell);
            cell = new PdfPCell(new Phrase(etiquetas.clmPeso, fuenteDatosNegrita)) { BorderWidth = 0.5f, HorizontalAlignment = Cell.ALIGN_CENTER, BackgroundColor = new Color(192, 192, 192) };
            tabla.AddCell(cell);
            cell = new PdfPCell(new Phrase(etiquetas.clmCausa, fuenteDatosNegrita)) { BorderWidth = 0.5f, HorizontalAlignment = Cell.ALIGN_CENTER, BackgroundColor = new Color(192, 192, 192) };
            tabla.AddCell(cell);
        }

        /// <summary>
        /// Obtiene el encabezado del reporte
        /// </summary>
        /// <param name="etiquetas"></param>
        /// <returns></returns>
        private PdfPTable ObtenerEncabezado(ImpresionSalidaGanadoMuertoInfo etiquetas)
        {
            var tblEncabezado = new PdfPTable(2);
            float[] medidas = { 1.5f, 6f }; 
            tblEncabezado.SetWidths(medidas);

            var logo = Image.GetInstance(AppDomain.CurrentDomain.BaseDirectory + "Imagenes\\skLogo.png");
            logo.ScaleAbsolute(90f, 25f);
            var cell = new PdfPCell(logo, true)
            {
                BorderWidth = 0,
                PaddingLeft = 5,
                VerticalAlignment = Cell.ALIGN_MIDDLE,
                HorizontalAlignment = Cell.ALIGN_CENTER
            };
            tblEncabezado.AddCell(new PdfPCell(cell) { BorderWidth = 0 });

            PdfPTable titulos = new PdfPTable(1);

            cell = new PdfPCell(new Phrase("", fuenteTitulo)) { BorderWidth = 0, FixedHeight = 10 };
            titulos.AddCell(cell);

            //Titulo
            cell = new PdfPCell(new Phrase(etiquetas.Titulo, fuenteTitulo)) { 
                BorderWidth = 0, 
                HorizontalAlignment = Cell.ALIGN_CENTER };
            titulos.AddCell(cell);

            cell = new PdfPCell(new Phrase("", fuenteTitulo)) { BorderWidth = 0, FixedHeight = 15 };
            titulos.AddCell(cell);

             //SubTitulo
            cell = new PdfPCell(new Phrase(etiquetas.SubTitulo, fuenteTituloSeccion)) {
                BorderWidth = 0,
                HorizontalAlignment = Cell.ALIGN_CENTER
            };
            titulos.AddCell(cell);

            cell = new PdfPCell(new Phrase("", fuenteTitulo)) { BorderWidth = 0, FixedHeight = 15 };
            titulos.AddCell(cell);

            tblEncabezado.AddCell(new PdfPCell(titulos) { BorderWidth = 0 });

            return tblEncabezado;
        }

        /// <summary>
        /// Obtiene el folio para el reporte
        /// </summary>
        /// <param name="etiquetas"></param>
        /// <returns></returns>
        private PdfPTable ObtenerFolio(ImpresionSalidaGanadoMuertoInfo etiquetas)
        {
            var tblFolio = new PdfPTable(2);
            float[] medidas = { 9f, 1f };
            tblFolio.SetWidths(medidas);

            //Etiqueta
            var cell = new PdfPCell(new Phrase(etiquetas.lblFolio, fuenteTituloSeccion)) { BorderWidth = 0, HorizontalAlignment = Cell.ALIGN_RIGHT};
            tblFolio.AddCell(cell);

            //Valor
            cell = new PdfPCell(new Phrase(etiquetas.Folio, fuenteTituloSeccion)) { BorderWidth = 0, HorizontalAlignment = Cell.ALIGN_RIGHT, VerticalAlignment = Cell.ALIGN_MIDDLE };
            tblFolio.AddCell(cell);

            return tblFolio;
        }

        private PdfPTable ObtenerNumeroPagina()
        {
            var tblPaginado = new PdfPTable(2);
            float[] medidas = { 9f, 1f };
            tblPaginado.SetWidths(medidas);

            
            var cell = new PdfPCell(new Phrase("", fuenteDatosNegrita)) { BorderWidth = 0, HorizontalAlignment = Cell.ALIGN_RIGHT };
            tblPaginado.AddCell(cell);

            //Pagina
            cell = new PdfPCell(new Phrase(String.Format("PAG. {0}-{1}", paginaActual, paginaFinal), fuenteDatosNegrita)) { BorderWidth = 0, HorizontalAlignment = Cell.ALIGN_RIGHT };
            tblPaginado.AddCell(cell);
            paginaActual++;

            return tblPaginado;
        }

        /// <summary>
        /// Genera espacios para el reporte
        /// </summary>
        /// <param name="espacios"></param>
        /// <returns></returns>
        private PdfPTable ObtenerEspacios(int espacios)
        {
            var tblEspacios = new PdfPTable(1);
            float[] medidas = {1f};
            tblEspacios.SetWidths(medidas);
            for (int i = 1; i < espacios; i++)
            {
                var cell = new PdfPCell(new Phrase(" ", fuenteTitulo)) { BorderWidth = 0};
                tblEspacios.AddCell(cell);
            }
            return tblEspacios;
        }

        /// <summary>
        /// Obtiene los campos en blanco con la fecha del reporte
        /// </summary>
        /// <param name="etiquetas"></param>
        /// <returns></returns>
        private PdfPTable ObtenerCamposFecha(ImpresionSalidaGanadoMuertoInfo etiquetas)
        {
            var tblEspacios = new PdfPTable(4);
            float[] medidas = { 0.6f,4f,0.6f,2f};
            tblEspacios.SetWidths(medidas);
            //Unidad
            var cell = new PdfPCell(new Phrase(etiquetas.lblUnidad, fuenteDatosNegrita)) { BorderWidth = 0, VerticalAlignment = Cell.ALIGN_BOTTOM };
            tblEspacios.AddCell(cell);
            cell = new PdfPCell(new Phrase("", fuenteDatosNegrita)) { Border= Rectangle.BOTTOM_BORDER , BorderWidth = 1 };
            tblEspacios.AddCell(cell);
            //Fecha
            cell = new PdfPCell(new Phrase(" " + etiquetas.lblFecha, fuenteDatosNegrita)) { BorderWidth = 0, VerticalAlignment = Cell.ALIGN_BOTTOM };
            tblEspacios.AddCell(cell);
            cell = new PdfPCell(new Phrase(etiquetas.Fecha, fuenteDatos)) { Border = Rectangle.BOTTOM_BORDER, BorderWidth = 1, HorizontalAlignment = Cell.ALIGN_CENTER };
            tblEspacios.AddCell(cell);
            //Chofer
            cell = new PdfPCell(new Phrase(etiquetas.lblChofer, fuenteDatosNegrita)) { BorderWidth = 0, VerticalAlignment = Cell.ALIGN_BOTTOM };
            tblEspacios.AddCell(cell);
            cell = new PdfPCell(new Phrase("", fuenteDatosNegrita)) { Border = Rectangle.BOTTOM_BORDER, BorderWidth = 1 };
            tblEspacios.AddCell(cell);
            //Placas
            cell = new PdfPCell(new Phrase(" "+etiquetas.lblPlacas, fuenteDatosNegrita)) { BorderWidth = 0, VerticalAlignment = Cell.ALIGN_BOTTOM };
            tblEspacios.AddCell(cell);
            cell = new PdfPCell(new Phrase("", fuenteDatosNegrita)) { Border = Rectangle.BOTTOM_BORDER, BorderWidth = 1 };
            tblEspacios.AddCell(cell);
            
            return tblEspacios;
        }

        /// <summary>
        /// Obtiene los Campos para la firma del reporte
        /// </summary>
        /// <param name="etiquetas"></param>
        /// <returns></returns>
        private PdfPTable ObtenerFirmas(ImpresionSalidaGanadoMuertoInfo etiquetas)
        {
            var tblEspacios = new PdfPTable(7);
            float[] medidas = { 2.1f, 0.2f, 2.1f, 0.2f, 2.1f, 0.2f, 2.1f };
            tblEspacios.SetWidths(medidas);
            //Unidad
            var cell = new PdfPCell(new Phrase(etiquetas.lblGerenteEngorda, fuenteDatosNegrita)) { Border = Rectangle.TOP_BORDER, BorderWidth = 1, HorizontalAlignment = Cell.ALIGN_CENTER };
            tblEspacios.AddCell(cell);
            cell = new PdfPCell(new Phrase("", fuenteDatosNegrita)) { Border =  0 };
            tblEspacios.AddCell(cell);
            //Fecha
            cell = new PdfPCell(new Phrase(etiquetas.lblGerenteAdministrativo, fuenteDatosNegrita)) { Border = Rectangle.TOP_BORDER, BorderWidth = 1, HorizontalAlignment = Cell.ALIGN_CENTER };
            tblEspacios.AddCell(cell);
            cell = new PdfPCell(new Phrase("", fuenteDatosNegrita)) { Border = 0 };
            tblEspacios.AddCell(cell);
            //Fecha
            cell = new PdfPCell(new Phrase(etiquetas.lblGerenteGeneral, fuenteDatosNegrita)) { Border = Rectangle.TOP_BORDER, BorderWidth = 1, HorizontalAlignment = Cell.ALIGN_CENTER };
            tblEspacios.AddCell(cell);
            cell = new PdfPCell(new Phrase("", fuenteDatosNegrita)) { Border = 0 };
            tblEspacios.AddCell(cell);
            //Fecha
            cell = new PdfPCell(new Phrase(etiquetas.lblProteccionPatrimonial, fuenteDatosNegrita)) { Border = Rectangle.TOP_BORDER, BorderWidth = 1, HorizontalAlignment = Cell.ALIGN_CENTER };
            tblEspacios.AddCell(cell);
            return tblEspacios;
        }

        /// <summary>
        /// Muestra en Pantalla el reporte generado
        /// </summary>
        /// <param name="archivo"></param>
        internal bool MostrarPantallaImpresion()
        {
            var result = false;
            try
            {
                Process.Start(AppDomain.CurrentDomain.BaseDirectory + nombreArchivo);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return result;
        }
    }
}
