using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using System.Collections.Generic;
//Reporte
using SIE.Services.Properties;
using Color = iTextSharp.text.Color;
using Font = iTextSharp.text.Font;
using Rectangle = iTextSharp.text.Rectangle;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace SIE.Services.Servicios.BL
{
    internal class ProgramacionCorteBL
    {
        private Font fuenteDatos;
        private Font fuenteTitulo;
        private Font fuenteTituloSeccion;

        /// <summary>
        ///     Metodo que Crea una Programacion de Corte
        /// </summary>
        /// <param name="programacionCorte"></param>
        internal int GuardarProgramacionCorte(IList<ProgramacionCorteInfo> programacionCorte)
        {
            try
            {
                int programacionID;
                Logger.Info();
                var programacionCorteDAL = new ProgramacionCorteDAL();
                programacionID = programacionCorteDAL.GuardarProgramacionCorte(programacionCorte);
                return programacionID;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Quita el registro de programacion de corte especificado
        /// </summary>
        /// <param name="programacionCorte"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal int EliminarProgramacionCorte(ProgramacionCorte programacionCorte, int organizacionID)
        {
            try
            {
                int programacionID;
                Logger.Info();
                var programacionCorteDAL = new ProgramacionCorteDAL();
                programacionID = programacionCorteDAL.EliminarProgramacionCorte(programacionCorte, organizacionID);
                return programacionID;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Se actualiza la
        /// </summary>
        /// <param name="programacionCorte"></param>
        public void ActualizarFechaInicioProgramacionCorte(ProgramacionCorte programacionCorte)
        {
            try
            {
                Logger.Info();
                var programacionCorteDAL = new ProgramacionCorteDAL();
                programacionCorteDAL.ActualizarFechaInicioProgramacionCorte(programacionCorte);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Inicializa las fuentes del informa de programacion de corte
        /// </summary>
        private void InicializarFuentes()
        {
            fuenteDatos = new Font(Font.HELVETICA, 8, Font.NORMAL, new Color(0, 0, 0));
            fuenteTituloSeccion = new Font(Font.HELVETICA, 9, Font.BOLD, new Color(0, 0, 0));
            fuenteTitulo = new Font(Font.HELVETICA, 9, Font.BOLD, new Color(0, 0, 0));
        }

        /// <summary>
        /// Imprime la programación de corte especificada
        /// </summary>
        internal bool ImprimirProgramacionCorte(ImpresionProgramacionCorteInfo datos)
        {
            InicializarFuentes();
            string nombreArchivo = "ProgramacionCorte.pdf";
            var retValue = false;
            var reporte = new Document(PageSize.A4.Rotate(), 0, 0, 35, 75);
            
            try
            {

                if (datos != null)
                {
                    //const string nombre = nombreArchivo;
                    if (nombreArchivo.Substring(nombreArchivo.Length - 4).ToUpper() != ".PDF")
                    {
                        nombreArchivo = nombreArchivo + ".PDF";
                    }

                    if (File.Exists(nombreArchivo))
                    {
                        try
                        {
                            using (Stream stream = new FileStream(nombreArchivo, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                            {
                            }
                            try
                            {
                                File.Delete(nombreArchivo);
                            }
                            catch (Exception ex)
                            {
                                Logger.Error(ex);
                                throw new ExcepcionDesconocida(ResourceServices.ProgramacionCorteImpresion_ArchivoEnUso);
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex);
                            throw new ExcepcionDesconocida(ResourceServices.ProgramacionCorteImpresion_ArchivoEnUso);
                        }


                    }

                    PdfWriter.GetInstance(reporte, new FileStream(nombreArchivo, FileMode.OpenOrCreate));
                    reporte.Open();

                    var tablaPrincipal = new PdfPTable(1);
                    float[] medidaPrincipal = { 1.0f };
                    tablaPrincipal.SetWidths(medidaPrincipal);

                    var tablaEncabezado = new PdfPTable(4);
                    float[] medidaCeldasEncabezado = { 0.30f, 0.10f, 0.35f, 0.25f };
                    tablaEncabezado.SetWidths(medidaCeldasEncabezado);

                    var dirLogo = AppDomain.CurrentDomain.BaseDirectory + ResourceServices.ProgramacionCorteImpresion_Logo;
                    var imgSuperior = Image.GetInstance(dirLogo);
                    imgSuperior.ScaleAbsolute(90f, 25f);

                    var cell = new PdfPCell(imgSuperior) { Padding = 2, Border = 0 };
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    tablaEncabezado.AddCell(cell);

                    cell = new PdfPCell(new Phrase(ResourceServices.ProgramacionCorteImpresion_Fecha, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))) { Border = 0 };
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
                    tablaEncabezado.AddCell(cell);

                    cell = new PdfPCell(new Phrase(datos.FechaProgramacion.ToString(ResourceServices.ProgramacionCorteImpresion_FechaFormato), FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.NORMAL))) { Border = 0 };
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER ;
                    tablaEncabezado.AddCell(cell);

                    cell = new PdfPCell(new Phrase(ResourceServices.ProgramacionCorteImpresion_Folio + ResourceServices.ProgramacionCorteImpresion_FormatoReporte, FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD))) { Border = 0 };
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tablaEncabezado.AddCell(cell);

                    cell = new PdfPCell(tablaEncabezado) { Border = 0 };
                    tablaPrincipal.AddCell(cell);

                    //Diseño Datos
                    var tablaDatos = ObtenerTablaProgramacion(datos);
                    cell = new PdfPCell(tablaDatos) { Border = 0 };
                    cell.PaddingTop = 10.0f;
                    tablaPrincipal.AddCell(cell);

                    reporte.Add(tablaPrincipal);

                    reporte.Close();

                    SendToPrinter(AppDomain.CurrentDomain.BaseDirectory + nombreArchivo);

                    retValue = true;
                }
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (DocumentException ex)
            {
                Logger.Error(ex);
                reporte.Close();
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (IOException ex)
            {
                Logger.Error(ex);
                reporte.Close();
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                reporte.Close();
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return retValue;
        }

        /// <summary>
        /// Obtiene la tabla de datos de la programacion de corte
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        private PdfPTable ObtenerTablaProgramacion(ImpresionProgramacionCorteInfo datos)
        {
            var tabla = new PdfPTable(10);
            //Tamaño de las culumnas
            float[] medidaCeldas = { 0.07f, 0.08f, 0.30f, 0.07f, 0.05f, 0.05f, 0.13f, 0.05f, 0.10f, 0.10f };
            tabla.SetWidths(medidaCeldas);

            var cell = new PdfPCell(new Phrase(ResourceServices.ProgramacionCorteImpresion_EncCorral, fuenteTitulo));
            cell.Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(ResourceServices.ProgramacionCorteImpresion_EncPartida, fuenteTitulo));
            cell.Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; cell.BorderWidth = 1;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(ResourceServices.ProgramacionCorteImpresion_EncOrigen, fuenteTitulo));
            cell.Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; cell.BorderWidth = 1;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(ResourceServices.ProgramacionCorteImpresion_EncCabezas, fuenteTitulo));
            cell.Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; cell.BorderWidth = 1;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(ResourceServices.ProgramacionCorteImpresion_EncMachos, fuenteTitulo));
            cell.Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; cell.BorderWidth = 1;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(ResourceServices.ProgramacionCorteImpresion_EncHembras, fuenteTitulo));
            cell.Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; cell.BorderWidth = 1;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(ResourceServices.ProgramacionCorteImpresion_EncFechaEntrada, fuenteTitulo));
            cell.Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; cell.BorderWidth = 1;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(ResourceServices.ProgramacionCorteImpresion_EncDias, fuenteTitulo));
            cell.Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; cell.BorderWidth = 1;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(ResourceServices.ProgramacionCorteImpresion_EncEvaluacion, fuenteTitulo));
            cell.Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; cell.BorderWidth = 1;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(ResourceServices.ProgramacionCorteImpresion_EncGarrapata, fuenteTitulo));
            cell.Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; cell.BorderWidth = 1;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.AddCell(cell);


            //Llenado de datos
            int i = 0;
            int res = 0;
            var color1 = new Color(250, 250, 250);
            var color2 = new Color(237, 237, 237);
            foreach (var prog in datos.ProgramacionCorte)
            {
                res = i%2;
                cell = new PdfPCell(new Phrase(prog.CodigoCorral, fuenteDatos));
                cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                cell.BorderWidth = 1;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.BackgroundColor = res == 0 ? color1 : color2;
                tabla.AddCell(cell);

                cell = new PdfPCell(new Phrase(prog.FolioEntradaID.ToString(), fuenteDatos));
                cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; cell.BorderWidth = 1;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.BackgroundColor = res == 0 ? color1 : color2;
                tabla.AddCell(cell);

                cell = new PdfPCell(new Phrase(prog.OrganizacionNombre, fuenteDatos));
                cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; cell.BorderWidth = 1;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.BackgroundColor = res == 0 ? color1 : color2;
                tabla.AddCell(cell);

                cell = new PdfPCell(new Phrase(prog.CabezasRecibidas.ToString(), fuenteDatos));
                cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; cell.BorderWidth = 1;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.BackgroundColor = res == 0 ? color1 : color2;
                tabla.AddCell(cell);

                cell = new PdfPCell(new Phrase(prog.Machos.ToString(), fuenteDatos));
                cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; cell.BorderWidth = 1;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.BackgroundColor = res == 0 ? color1 : color2;
                tabla.AddCell(cell);

                cell = new PdfPCell(new Phrase(prog.Hembras.ToString(), fuenteDatos));
                cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; cell.BorderWidth = 1;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.BackgroundColor = res == 0 ? color1 : color2;
                tabla.AddCell(cell);

                cell = new PdfPCell(new Phrase(prog.FechaEntrada, fuenteDatos));
                cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; cell.BorderWidth = 1;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.BackgroundColor = res == 0 ? color1 : color2;
                tabla.AddCell(cell);

                cell = new PdfPCell(new Phrase(prog.Dias.ToString(), fuenteDatos));
                cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; cell.BorderWidth = 1;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.BackgroundColor = res == 0 ? color1 : color2;
                tabla.AddCell(cell);

                cell = new PdfPCell(new Phrase(prog.Evaluacion, fuenteDatos));
                cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; cell.BorderWidth = 1;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.BackgroundColor = res == 0 ? color1 : color2;
                tabla.AddCell(cell);

                cell = new PdfPCell(new Phrase(prog.LeyendaNivelGarrapata, fuenteDatos));
                cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER; cell.BorderWidth = 1;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.BackgroundColor = res == 0 ? color1 : color2;
                tabla.AddCell(cell);

                i++;
            }

            res = i % 2;
            //Resumen
            cell = new PdfPCell(new Phrase(String.Empty, fuenteTitulo));
            cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER; cell.BorderWidth = 1;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = res == 0 ? color1 : color2;
            tabla.AddCell(cell);
            cell = new PdfPCell(new Phrase(String.Empty, fuenteTitulo));
            cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER; cell.BorderWidth = 1;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = res == 0 ? color1 : color2;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(ResourceServices.ProgramacionCorteImpresion_EncTotalTitulo, fuenteTitulo));
            cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER; cell.BorderWidth = 1;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BackgroundColor = res == 0 ? color1 : color2;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(datos.TotalRecibidas.ToString(), fuenteTitulo));
            cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER; cell.BorderWidth = 1;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.BackgroundColor = res == 0 ? color1 : color2;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(datos.TotalMachos.ToString(), fuenteTitulo));
            cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER; cell.BorderWidth = 1;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.BackgroundColor = res == 0 ? color1 : color2;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(datos.TotalHembras.ToString(), fuenteTitulo));
            cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER; cell.BorderWidth = 1;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.BackgroundColor = res == 0 ? color1 : color2;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(String.Empty, fuenteTitulo));
            cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER; cell.BorderWidth = 1;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = res == 0 ? color1 : color2;
            tabla.AddCell(cell);
            cell = new PdfPCell(new Phrase(String.Empty, fuenteTitulo));
            cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER; cell.BorderWidth = 1;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = res == 0 ? color1 : color2;
            tabla.AddCell(cell);
            cell = new PdfPCell(new Phrase(String.Empty, fuenteTitulo));
            cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER; cell.BorderWidth = 1;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = res == 0 ? color1 : color2;
            tabla.AddCell(cell);
            cell = new PdfPCell(new Phrase(String.Empty, fuenteTitulo));
            cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER; cell.BorderWidth = 1;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = res == 0 ? color1 : color2;
            tabla.AddCell(cell);

            return tabla;
        }
         
        /// <summary>
        /// Enviar la pantalla de impresion
        /// </summary>
        /// <param name="archivo"></param>
        private void SendToPrinter(string archivo)
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
