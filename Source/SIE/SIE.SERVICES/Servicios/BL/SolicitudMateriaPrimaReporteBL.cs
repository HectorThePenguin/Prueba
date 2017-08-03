using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Properties;

namespace SIE.Services.Servicios.BL
{
    internal class SolicitudMateriaPrimaReporteBL
    {
        public bool ImprimirPedidoMateriaPrima(PedidoInfo pedido,string nombreArchivo)
        {
            var reporte = new Document(PageSize.A4, 10, 10, 35, 75);
            try
            {
                var pedidoBl = new PedidosBL();
                pedido = pedidoBl.ObtenerPedidoPorFolio(pedido);

                if (pedido != null)
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
                                throw new ExcepcionDesconocida(ResourceServices.ConfigurarFormula_ArchivoEnUso);
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex);
                            throw new ExcepcionDesconocida(ResourceServices.ConfigurarFormula_ArchivoEnUso);
                        }
                        
                        
                    }

                    PdfWriter.GetInstance(reporte, new FileStream(nombreArchivo, FileMode.OpenOrCreate));
                    reporte.Open();

                    var fuenteDatos = new Font { Size = 4, Color = Color.BLACK };


                    var table = new PdfPTable(20);

                    var dirLogo = AppDomain.CurrentDomain.BaseDirectory + "Imagenes\\skLogo.png";
                    var imgSuperior = Image.GetInstance(dirLogo);
                    imgSuperior.ScaleAbsolute(90f, 25f);

                    var cell = new PdfPCell(imgSuperior) { Padding = 2, Border = 0 };
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("SuKarne Agroindustrial SA de CV", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD))) { Border = 0 };
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.Colspan = 20;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Formato de Pase a Proceso", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Border = 0 };
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.Colspan = 20;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Fecha de entrega", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 10 };
                    cell.BackgroundColor = Color.GRAY;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Fecha para aplicación", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 10 };
                    cell.BackgroundColor = Color.GRAY;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(pedido.FechaPedido.ToLongDateString().ToString(CultureInfo.InvariantCulture), FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 10 };
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 10 };
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    table = ObtenerTablaGrano(table, fuenteDatos, pedido.DetallePedido.Where(pedidoDetalle => pedidoDetalle.Producto.SubFamilia.SubFamiliaID == (int)SubFamiliasEnum.Granos).ToList());

                    cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Border = 0 };
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.Colspan = 20;
                    table.AddCell(cell);

                    table = ObtenerTablaPremezclas(table, fuenteDatos, new List<PedidoDetalleInfo>());

                    table = ObtenerTablaSoya(table, fuenteDatos, new List<PedidoDetalleInfo>());

                    table = ObtenerTablaDDG(table, fuenteDatos, new List<PedidoDetalleInfo>());

                    table = ObtenerTablaSoyPlus(table, fuenteDatos, new List<PedidoDetalleInfo>());

                    table = ObtenerTablaSebo(table, fuenteDatos, new List<PedidoDetalleInfo>());

                    table = ObtenerTablaMelaza(table, fuenteDatos, new List<PedidoDetalleInfo>());

                    table = ObtenerTablaForraje(table, fuenteDatos, pedido.DetallePedido.Where(pedidoDetalle => pedidoDetalle.Producto.SubFamilia.SubFamiliaID == (int)SubFamiliasEnum.Forrajes).ToList());

                    reporte.Add(table);

                    reporte.Close();

                    SendToPrinter(AppDomain.CurrentDomain.BaseDirectory + nombreArchivo);

                    return true;
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
            return false;
        }

        /// <summary>
        /// Obtener tabla de forraje
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fuenteDatos"></param>
        /// <param name="pedidoDetalleForraje"></param>
        /// <returns></returns>
        private PdfPTable ObtenerTablaForraje(PdfPTable table, Font fuenteDatos, List<PedidoDetalleInfo> pedidoDetalleForraje)
        {
            var cell = new PdfPCell(new Phrase("FORRAJE", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 20 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Número", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Tipo", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Ticket", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Boleta Origen", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Kilos", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Lote", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Lote PP", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Firma MP", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Firma Pta. Alim.", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Observaciones", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 4 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            for (int i = 1; i <= 15; i++)
            {
                if (pedidoDetalleForraje.Count >= i)
                {
                    table = AgregarRenglonForraje(i, table, fuenteDatos,pedidoDetalleForraje);
                }
                else
                {
                    table = AgregarRenglonVacioForraje(i, table, fuenteDatos);
                }
            }

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 7 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(pedidoDetalleForraje.Sum(registro=>registro.CantidadSolicitada).ToString(CultureInfo.InvariantCulture), FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 4 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Valido por Materias Primas", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 10 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Valido por Planta de Alimentos", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 6 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 4 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("__________________________", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 10 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("_____________________________", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 6 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 4 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Jefe Materias Primas", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 10 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Jefe de planta de alimentos", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 6 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 4 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            return table;
        }

        /// <summary>
        /// Agrega un renglon de forraje
        /// </summary>
        /// <param name="i"></param>
        /// <param name="table"></param>
        /// <param name="fuenteDatos"></param>
        /// <param name="pedidoDetalleForraje"></param>
        /// <returns></returns>
        private PdfPTable AgregarRenglonForraje(int i, PdfPTable table, Font fuenteDatos, List<PedidoDetalleInfo> pedidoDetalleForraje )
        {
            var cell = new PdfPCell(new Phrase(i.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(pedidoDetalleForraje[i-1].Producto.ProductoDescripcion, FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(pedidoDetalleForraje[i - 1].CantidadSolicitada.ToString(CultureInfo.InvariantCulture), FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(pedidoDetalleForraje[i - 1].InventarioLoteDestino.Lote.ToString(CultureInfo.InvariantCulture), FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 4 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            return table;
        }

        /// <summary>
        /// Agrega un renglon vacio en la tabla forraje
        /// </summary>
        /// <param name="i"></param>
        /// <param name="table"></param>
        /// <param name="fuenteDatos"></param>
        /// <returns></returns>
        private PdfPTable AgregarRenglonVacioForraje(int i, PdfPTable table, Font fuenteDatos)
        {
            var cell = new PdfPCell(new Phrase(i.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 4 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            return table;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fuenteDatos"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private PdfPTable ObtenerTablaMelaza(PdfPTable table, Font fuenteDatos, List<PedidoDetalleInfo> list)
        {
            var cell = new PdfPCell(new Phrase("MELAZA", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 20 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Número", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Hora", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 4 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Agua", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Melaza", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Lote", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Lote PP", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Firma MP", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Firma Pta. Alim.", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Observaciones", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 4 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            for (int i = 1; i <= 2; i++)
            {
                table = AgregarRenglonVacioMelaza(i, table, fuenteDatos);
            }

            cell = new PdfPCell(new Phrase("Total Melaza", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 7 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("0", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 4 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            return table;
        }

        /// <summary>
        /// Agrega un renglon a la tabla de melaza
        /// </summary>
        /// <param name="i"></param>
        /// <param name="table"></param>
        /// <param name="fuenteDatos"></param>
        /// <returns></returns>
        private PdfPTable AgregarRenglonVacioMelaza(int i, PdfPTable table, Font fuenteDatos)
        {
            var cell = new PdfPCell(new Phrase(i.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 4 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 4 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            return table;
        }

        /// <summary>
        /// Genera la tabla Soy Plus
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fuenteDatos"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private PdfPTable ObtenerTablaSebo(PdfPTable table, Font fuenteDatos, List<PedidoDetalleInfo> list)
        {
            var cell = new PdfPCell(new Phrase("SEBO", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 20 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Número", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Inicio", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Fin", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Kilos", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Lote", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Lote PP", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Firma MP", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 3 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Firma Pta. Alim.", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 3 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Observaciones", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 4 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            for (int i = 1; i <= 2; i++)
            {
                table = AgregarRenglonVacioSEBO(i, table, fuenteDatos);
            }

            cell = new PdfPCell(new Phrase("Total SEBO", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 7 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("0", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 3 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 3 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 4 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            return table;
        }

        /// <summary>
        /// Agrega un renglon vacio a la tabla SEBO
        /// </summary>
        /// <param name="i"></param>
        /// <param name="table"></param>
        /// <param name="fuenteDatos"></param>
        /// <returns></returns>
        private PdfPTable AgregarRenglonVacioSEBO(int i, PdfPTable table, Font fuenteDatos)
        {
            var cell = new PdfPCell(new Phrase(i.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 3 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 3 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 4 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            return table;
        }

        /// <summary>
        /// Genera la tabla Soy Plus
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fuenteDatos"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private PdfPTable ObtenerTablaSoyPlus(PdfPTable table, Font fuenteDatos, List<PedidoDetalleInfo> list)
        {
            var cell = new PdfPCell(new Phrase("SOY PLUS", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 20 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Número", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Ticket", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 4 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Boleta Origen", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Kilos", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Lote", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Lote PP", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Firma MP", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Firma Pta. Alim.", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Observaciones", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 4 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            for (int i = 1; i <= 2; i++)
            {
                table = AgregarRenglonVacioSoyPlus(i, table, fuenteDatos);
            }

            cell = new PdfPCell(new Phrase("SOY PLUS", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 7 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("0", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 4 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            return table;
        }

        /// <summary>
        /// Agrega un renglon vacio a la tabla SoyPlus
        /// </summary>
        /// <param name="i"></param>
        /// <param name="table"></param>
        /// <param name="fuenteDatos"></param>
        /// <returns></returns>
        private PdfPTable AgregarRenglonVacioSoyPlus(int i, PdfPTable table, Font fuenteDatos)
        {
            var cell = new PdfPCell(new Phrase(i.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 4 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 4 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            return table;
        }

        /// <summary>
        /// Genera la tabla DDG
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fuenteDatos"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private PdfPTable ObtenerTablaDDG(PdfPTable table, Font fuenteDatos, List<PedidoDetalleInfo> list)
        {
            var cell = new PdfPCell(new Phrase("DDG", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 20 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Número", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Ticket", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Boleta Origen", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Kilos", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Lote", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Lote PP", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Firma MP", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 3 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Firma Pta. Alim.", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 3 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Observaciones", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 4 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            for (int i = 1; i <= 2; i++)
            {
                table = AgregarRenglonVacioDDG(i, table, fuenteDatos);
            }

            cell = new PdfPCell(new Phrase("DDG", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 7 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 3 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 3 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 4 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            return table;
        }

        /// <summary>
        /// Agrega un renglon vacio
        /// </summary>
        /// <param name="i"></param>
        /// <param name="table"></param>
        /// <param name="fuenteDatos"></param>
        private PdfPTable AgregarRenglonVacioDDG(int i, PdfPTable table, Font fuenteDatos)
        {
            var cell = new PdfPCell(new Phrase(i.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 3 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 3 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 4 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            return table;
        }

        /// <summary>
        /// Obtiene la tabla de soya
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fuenteDatos"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private PdfPTable ObtenerTablaSoya(PdfPTable table, Font fuenteDatos, List<PedidoDetalleInfo> list)
        {
            var cell = new PdfPCell(new Phrase("PASTA DE SOYA", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 20 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Número", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Ticket", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Boleta Origen", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Kilos", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Lote", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Lote PP", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Firma MP", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 3 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Firma Pta. Alim.", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 3 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Observaciones", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 4 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            for (int i = 1; i <= 15; i++)
            {
                table = AgregarRenglonVacioSoya(i, table, fuenteDatos);
            }

            cell = new PdfPCell(new Phrase("PASTA DE SOYA", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 5 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("0", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 3 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 3 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 4 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            return table;
        }

        /// <summary>
        /// Agrega un renglon vacio
        /// </summary>
        /// <param name="i"></param>
        /// <param name="table"></param>
        /// <param name="fuenteDatos"></param>
        private PdfPTable AgregarRenglonVacioSoya(int i, PdfPTable table, Font fuenteDatos)
        {
            var cell = new PdfPCell(new Phrase(i.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 3 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 3 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 4 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            return table;
        }

        /// <summary>
        /// Obtiene la tabla de premezclas
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fuenteDatos"></param>
        /// <param name="pedidoDetallePremezclas"></param>
        /// <returns></returns>
        private PdfPTable ObtenerTablaPremezclas(PdfPTable table, Font fuenteDatos, List<PedidoDetalleInfo> pedidoDetallePremezclas)
        {
            var cell = new PdfPCell(new Phrase("PREMEZCLAS", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 20 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Nombre", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 3 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Boleta Origen", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 3 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Kilos", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Lote", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Lote PP", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Firma MP", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Firma Pta. Alim.", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Observaciones", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 4 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            for (int i = 1; i <= 15; i++)
            {
                table = AgregarRenglonVacioPremezclas(i, table, fuenteDatos);
            }

            return table;
        }

        /// <summary>
        /// Retorna un renglon en la tabla de premezclas
        /// </summary>
        /// <param name="contador"></param>
        /// <param name="table"></param>
        /// <param name="fuenteDatos"></param>
        /// <returns></returns>
        private PdfPTable AgregarRenglonVacioPremezclas(int contador, PdfPTable table, Font fuenteDatos)
        {
            var cell = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 3 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 3 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 4 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            return table;
        }

        /// <summary>
        /// Genera la tabla de grano
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fuenteDatos"></param>
        /// <param name="pedidoDetalleGranos"></param>
        /// <returns></returns>
        private PdfPTable ObtenerTablaGrano(PdfPTable table, Font fuenteDatos, List<PedidoDetalleInfo> pedidoDetalleGranos)
        {
            var cell = new PdfPCell(new Phrase("GRANO", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 20 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Número", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Ticket", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Boleta", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Peso Neto", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Origen", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Humedad", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Lote PP", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Firma MP", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Firma Pta. Alim.", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Observaciones", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 4 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.GRAY;
            table.AddCell(cell);

            if (pedidoDetalleGranos.Count > 0)
            {
                int contador = 1;
                foreach (var pedidoDetalleGrano in pedidoDetalleGranos)
                {
                    cell = new PdfPCell(new Phrase(contador.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 1 };
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(pedidoDetalleGrano.CantidadSolicitada.ToString(CultureInfo.InvariantCulture), FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 1 };
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(pedidoDetalleGrano.InventarioLoteDestino.Lote.ToString(CultureInfo.InvariantCulture), FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 4 };
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    contador++;
                }
                if (contador < 5)
                {
                    for (int i = contador; i <= 5; i++)
                    {
                        table = AgregarRenglonVacioGranos(i, table, fuenteDatos);
                    }
                }
            }
            else
            {
                for (int i = 0; i <= 5; i++)
                {
                    table = AgregarRenglonVacioGranos(i + 1, table, fuenteDatos);
                }
            }

            table = AgregarTotalesTablaGranos(table, fuenteDatos, pedidoDetalleGranos);

            return table;
        }

        /// <summary>
        /// Agrega los totales a la tabla de granos
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fuenteDatos"></param>
        /// <param name="pedidoDetalleGranos"></param>
        /// <returns></returns>
        private PdfPTable AgregarTotalesTablaGranos(PdfPTable table, Font fuenteDatos, List<PedidoDetalleInfo> pedidoDetalleGranos)
        {
            var cell = new PdfPCell(new Phrase("Total", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 5 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            if (pedidoDetalleGranos.Count > 0)
            {
                cell =
                    new PdfPCell(new Phrase(
                        pedidoDetalleGranos.Sum(registro => registro.CantidadSolicitada).ToString(CultureInfo.InvariantCulture),
                        FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.BOLD))) { Colspan = 2 };
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);
            }
            else
            {
                cell = new PdfPCell(new Phrase("0", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);
            }

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 4 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            return table;
        }

        /// <summary>
        /// Agrega un renglon vacio a la tabla de granos
        /// </summary>
        /// <param name="contador"></param>
        /// <param name="table"></param>
        /// <param name="fuenteDatos"></param>
        /// <returns></returns>
        private PdfPTable AgregarRenglonVacioGranos(int contador, PdfPTable table, Font fuenteDatos)
        {
            var cell = new PdfPCell(new Phrase(contador.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 1 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 2 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.HELVETICA, fuenteDatos.Size, Font.NORMAL))) { Colspan = 4 };
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            return table;
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
