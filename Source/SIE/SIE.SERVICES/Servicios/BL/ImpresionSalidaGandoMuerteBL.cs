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
    internal class ImpresionSalidaGandoMuerteBL
    {
        private Font fuenteDatos;
        private Font fuenteDatosNegrita;
        private Font fuenteTitulo;
        private Font fuenteTituloSeccion;

        private void InicializaFuentes()
        {
            fuenteDatos = new Font(Font.HELVETICA, 9, Font.NORMAL, new Color(0, 0, 0));
            fuenteDatosNegrita = new Font(Font.HELVETICA, 9, Font.BOLD, new Color(0, 0, 0));
            fuenteTituloSeccion = new Font(Font.HELVETICA, 12, Font.BOLD, new Color(0, 0, 0));
            fuenteTitulo = new Font(Font.HELVETICA, 14, Font.BOLD, new Color(0, 0, 0));
        }

        internal void CrearReporte(ImpresionSalidaGanadoMuertoInfo etiquetas, IList<SalidaGanadoMuertoInfo> listaMuertes)
        {
            InicializaFuentes();
            var reporte = new Document(PageSize.A4, 10, 10, 75, 75);
            try
            {
                const string nombreArchivo = "OrdenSalidaMuerteGanado.pdf";
                PdfWriter.GetInstance(reporte, new FileStream(nombreArchivo, FileMode.OpenOrCreate));

                reporte.Open();

                float[] medidaCeldas = { 20 };
                var tablePrincipal = new PdfPTable(1);

                tablePrincipal.SetWidths(medidaCeldas);

                //Encabezado
                tablePrincipal.AddCell(new PdfPCell(ObtenerEncabezado(etiquetas)) {  
                    Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER, 
                    BorderWidth = 2, 
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
                foreach (var tabla in ObtenerMuertes(etiquetas, listaMuertes))
                {
                    tablePrincipal.AddCell(new PdfPCell(tabla) { Border = 0 });
                }
                tablePrincipal.AddCell(new PdfPCell(ObtenerEspacios(4)) { Border = 0 });
                //Firmas
                tablePrincipal.AddCell(new PdfPCell(ObtenerFirmas(etiquetas)){ Border = 0 });

                reporte.Add(tablePrincipal);

                reporte.Close();
                MostrarPantallaImpresion(AppDomain.CurrentDomain.BaseDirectory + nombreArchivo); 
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
                var cell = new PdfPCell(new Phrase(muerteInfo.CodigoCorral, fuenteDatos)) { BorderWidth = 1 };
                tblEspacios.AddCell(cell);
                cell = new PdfPCell(new Phrase(muerteInfo.Arete, fuenteDatos)) { BorderWidth = 1, HorizontalAlignment = Cell.ALIGN_RIGHT };
                tblEspacios.AddCell(cell);
                cell = new PdfPCell(new Phrase(muerteInfo.AreteTestigo, fuenteDatos)) { BorderWidth = 1, HorizontalAlignment = Cell.ALIGN_RIGHT };
                tblEspacios.AddCell(cell);
                cell = new PdfPCell(new Phrase(muerteInfo.Sexo, fuenteDatos)) { BorderWidth = 1};
                tblEspacios.AddCell(cell);
                cell = new PdfPCell(new Phrase(muerteInfo.TipoGanado, fuenteDatos)) { BorderWidth = 1 };
                tblEspacios.AddCell(cell);
                cell = new PdfPCell(new Phrase(muerteInfo.Peso.ToString("N0"), fuenteDatos)) { BorderWidth = 1, HorizontalAlignment = Cell.ALIGN_RIGHT };
                tblEspacios.AddCell(cell);
                cell = new PdfPCell(new Phrase(muerteInfo.Causa, fuenteDatos)) { BorderWidth = 1 };
                tblEspacios.AddCell(cell);
                fila++;
                if ((listaTablas.Count == 0 && fila > 38) ||
                   (fila > 50))
                {
                    listaTablas.Add(tblEspacios);
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

        private void ObtenerMuertesEncabezado(ImpresionSalidaGanadoMuertoInfo etiquetas, PdfPTable tabla)
        {
            var cell = new PdfPCell(new Phrase(etiquetas.clmCorral, fuenteDatosNegrita)) { BorderWidth = 1, HorizontalAlignment = Cell.ALIGN_CENTER, BackgroundColor = new Color(192, 192, 192) };
            tabla.AddCell(cell);
            cell = new PdfPCell(new Phrase(etiquetas.clmArete, fuenteDatosNegrita)) { BorderWidth = 1, HorizontalAlignment = Cell.ALIGN_CENTER, BackgroundColor = new Color(192, 192, 192) };
            tabla.AddCell(cell);
            cell = new PdfPCell(new Phrase(etiquetas.clmAreteTestigo, fuenteDatosNegrita)) { BorderWidth = 1, HorizontalAlignment = Cell.ALIGN_CENTER, BackgroundColor = new Color(192, 192, 192) };
            tabla.AddCell(cell);
            cell = new PdfPCell(new Phrase(etiquetas.clmSexo, fuenteDatosNegrita)) { BorderWidth = 1, HorizontalAlignment = Cell.ALIGN_CENTER, BackgroundColor = new Color(192, 192, 192) };
            tabla.AddCell(cell);
            cell = new PdfPCell(new Phrase(etiquetas.clmTipoGanado, fuenteDatosNegrita)) { BorderWidth = 1, HorizontalAlignment = Cell.ALIGN_CENTER, BackgroundColor = new Color(192, 192, 192) };
            tabla.AddCell(cell);
            cell = new PdfPCell(new Phrase(etiquetas.clmPeso, fuenteDatosNegrita)) { BorderWidth = 1, HorizontalAlignment = Cell.ALIGN_CENTER, BackgroundColor = new Color(192, 192, 192) };
            tabla.AddCell(cell);
            cell = new PdfPCell(new Phrase(etiquetas.clmCausa, fuenteDatosNegrita)) { BorderWidth = 1, HorizontalAlignment = Cell.ALIGN_CENTER, BackgroundColor = new Color(192, 192, 192) };
            tabla.AddCell(cell);
        }

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

        private PdfPTable ObtenerFolio(ImpresionSalidaGanadoMuertoInfo etiquetas)
        {
            var tblFolio = new PdfPTable(2);
            float[] medidas = { 8.5f, 1f };
            tblFolio.SetWidths(medidas);

            //Etiqueta
            var cell = new PdfPCell(new Phrase(etiquetas.lblFolio, fuenteTituloSeccion)) { BorderWidth = 0, HorizontalAlignment = Cell.ALIGN_RIGHT};
            tblFolio.AddCell(cell);

            //Valor
            cell = new PdfPCell(new Phrase(etiquetas.Folio, fuenteTituloSeccion)) { BorderWidth = 0, HorizontalAlignment = Cell.ALIGN_RIGHT, VerticalAlignment = Cell.ALIGN_MIDDLE };
            tblFolio.AddCell(cell);

            return tblFolio;
        }

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

        private void MostrarPantallaImpresion(string archivo)
        {
            try
            {
                Process.Start(archivo);
            }
            catch (Exception ex)
            {

                Logger.Error(ex);
            }

        }
    }
}
