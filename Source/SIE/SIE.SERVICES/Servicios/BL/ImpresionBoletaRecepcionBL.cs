using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using Color = iTextSharp.text.Color;
using Font = iTextSharp.text.Font;
using Rectangle = iTextSharp.text.Rectangle;

namespace SIE.Services.Servicios.BL 
{
    internal class ImpresionBoletaRecepcionBL
    {
        private Font fuenteDatos;
        private Font fuenteTitulo;
        private Font fuenteTituloSeccion;
        private Font fuenteTituloDatosAnalisis;
        
        internal ImpresionBoletaRecepcionBL()
        {
            InicializarFuentes();
        }

        private void InicializarFuentes()
        {
            fuenteDatos = new Font(Font.HELVETICA, 8, Font.NORMAL, new Color(0, 0, 0));
            fuenteTituloSeccion = new Font(Font.HELVETICA, 9, Font.BOLD, new Color(0, 0, 0));
            fuenteTitulo = new Font(Font.HELVETICA, 10, Font.BOLD, new Color(0, 0, 0));
            fuenteTituloDatosAnalisis = new Font(Font.HELVETICA, 6, Font.BOLD, new Color(0, 0, 0));
        }

        internal void ImprimirBoleta()
        {
            
        }

        /// <summary>
        /// Genera la boleta recepción forraje en documento pdf.
        /// </summary>
        /// <param name="etiquetas"></param>
        /// <param name="entradaProducto"></param>
        internal void ImprimirBoletaForraje(ImpresionBoletaRecepcionInfo etiquetas, EntradaProductoInfo entradaProducto)
        {
            var reporte = new Document(PageSize.A4, 10, 10, 35, 75);
            try
            {
                const string nombreArchivo = "boletaRecepcionForraje.pdf";
                PdfWriter.GetInstance(reporte, new FileStream(nombreArchivo, FileMode.OpenOrCreate));

                reporte.Open();

                float[] medidaCeldas = {0.75f, 0.75f, 0.20f, 0.75f, 0.75f};
                var tablePrincipal = new PdfPTable(5);

                tablePrincipal.SetWidths(medidaCeldas);

                //Diseño del encabezado
                var cell = new PdfPCell(new Phrase(entradaProducto.Organizacion.Descripcion, fuenteTitulo))
                {
                    Colspan = 4,
                    HorizontalAlignment = 1,
                    Border = 0
                };
                tablePrincipal.AddCell(cell);

                cell = new PdfPCell(new Phrase("", fuenteTitulo)) {Colspan = 2, HorizontalAlignment = 1, Border = 0};
                tablePrincipal.AddCell(cell);

                cell = new PdfPCell(new Phrase(entradaProducto.Organizacion.Direccion, fuenteDatos))
                {
                    Colspan = 4,
                    HorizontalAlignment = 1,
                    Border = 0
                };
                tablePrincipal.AddCell(cell);

                //Diseño Folio
                cell = new PdfPCell(ObtenerDatosFolio(etiquetas, entradaProducto));
                cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER |
                              Rectangle.RIGHT_BORDER;
                cell.BorderWidth = 1;
                tablePrincipal.AddCell(cell);
                //Fin Diseño Folio

                //Salto de Linea
                cell = new PdfPCell(new Phrase("", fuenteTitulo)) {Colspan = 5, HorizontalAlignment = 1, Border = 0};
                tablePrincipal.AddCell(cell);

                cell = new PdfPCell(new Phrase("", fuenteTitulo)) {Colspan = 5, HorizontalAlignment = 1, Border = 0};
                tablePrincipal.AddCell(cell);
                //Fin Salto de Linea

                //Titulo Reporte
                cell = new PdfPCell(new Phrase(etiquetas.EtiquetaTitulo, fuenteTitulo))
                {
                    Colspan = 4,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Border = 0
                };
                tablePrincipal.AddCell(cell);
                //Fin Titulo Reporte

                //Diseño Fecha
                cell = new PdfPCell(ObtenerDatosFecha(etiquetas));
                cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER |
                              Rectangle.RIGHT_BORDER;
                cell.BorderWidth = 1;
                tablePrincipal.AddCell(cell);
                //Fin Diseño Fecha

                //Salto de Linea
                cell = new PdfPCell(new Phrase("", fuenteDatos)) {Colspan = 5, Border = 0};
                tablePrincipal.AddCell(cell);

                cell = new PdfPCell(new Phrase("", fuenteDatos)) {Colspan = 5, Border = 0};
                tablePrincipal.AddCell(cell);
                //Fin Salto de Linea}

                //Fin Diseño Encabezado

                //Diseño Datos Generales
                cell = new PdfPCell(ObtenerDatosGenerales(etiquetas, entradaProducto)) {Colspan = 5, Border = 0};
                tablePrincipal.AddCell(cell);

                cell = new PdfPCell(new Phrase("", fuenteDatos)) {Colspan = 5, Border = 0};
                tablePrincipal.AddCell(cell);

                cell = new PdfPCell(new Phrase("", fuenteDatos)) {Colspan = 5, Border = 0};
                tablePrincipal.AddCell(cell);
                //Fin Diseño Datos Generales

                //Diseño Muestras
                cell = new PdfPCell(ObtenerMuestrasForraje(etiquetas, entradaProducto)) {Colspan = 2, Border = 1};
                cell.Padding = 0f;
                tablePrincipal.AddCell(cell);
                //Fin Diseño Muestras

                cell = new PdfPCell(new Phrase("", fuenteDatos)) {Colspan = 1, Border = 0};
                tablePrincipal.AddCell(cell);

                //Diseño Muestras
                float[] medidaCeldasBascula = {1.5f};
                var tablaBascula = new PdfPTable(1);

                tablaBascula.SetWidths(medidaCeldasBascula);

                cell = new PdfPCell(ObtenerDatosBascula(etiquetas, entradaProducto)) {Border = 1};
                cell.Padding = 0f;
                tablaBascula.AddCell(cell);

                cell = new PdfPCell(new Phrase("", fuenteDatos)) {Colspan = 1, Border = 0};
                tablaBascula.AddCell(cell);

                cell = new PdfPCell(new Phrase("", fuenteDatos)) {Colspan = 1, Border = 0};
                tablaBascula.AddCell(cell);

                cell = new PdfPCell(ObtenerDatosObservaciones(etiquetas, entradaProducto)) {Border = 1};
                cell.Padding = 0f;
                tablaBascula.AddCell(cell);

                cell = new PdfPCell(tablaBascula) {Colspan = 2, Border = 1};
                cell.Padding = 0f;
                tablePrincipal.AddCell(cell);
                //Fin Diseño Muestras 

                //Diseño Destino
                cell = new PdfPCell(ObtenerDatosDestino(etiquetas, entradaProducto)) {Colspan = 5, Border = 0};
                tablePrincipal.AddCell(cell);
                //Fin Diseño Destino

                //Diseño Descarga
                cell = new PdfPCell(ObtenerDatosDescarga(etiquetas, entradaProducto)) {Colspan = 5, Border = 0};
                tablePrincipal.AddCell(cell);
                //Fin Diseño Descarga

                //Diseño Responsables
                cell = new PdfPCell(ObtenerDatosResponsables(etiquetas, entradaProducto)) {Colspan = 5, Border = 0};
                tablePrincipal.AddCell(cell);
                //Fin Diseño Responsables

                reporte.Add(tablePrincipal);
                
                MostrarPantallaImpresion(AppDomain.CurrentDomain.BaseDirectory + nombreArchivo);

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw  new ExcepcionDesconocida(MethodBase.GetCurrentMethod(),ex);
            }
            finally
            {
                reporte.Close();
            }
        }

        /// <summary>
        /// Obtiene los datos de la tabla de datos generales
        /// </summary>
        /// <param name="etiquetas"></param>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        private PdfPTable ObtenerDatosGenerales(ImpresionBoletaRecepcionInfo etiquetas,EntradaProductoInfo entradaProducto)
        {
            float[] medidaCeldas = { 0.75f, 0.75f, 0.40f, 0.75f, 0.75f };
            var tabla = new PdfPTable(5);

            tabla.SetWidths(medidaCeldas);

            var etiquetaProducto = "";

            if (entradaProducto.Producto.SubFamilia.SubFamiliaID == (int) SubFamiliasEnum.Forrajes)
            {
                etiquetaProducto = etiquetas.EtiquetaProductoForraje;
            }
            else
            {
                etiquetaProducto = etiquetas.EtiquetaProducto;
            }

            var cell = new PdfPCell(new Phrase(etiquetaProducto, fuenteDatos)) { Border = 1 };
            cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(entradaProducto.Producto.ProductoDescripcion, fuenteDatos)) { Colspan = 4, Border = 1 };
            cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(etiquetas.EtiquetaContrato, fuenteDatos)) { Border = 1 };
            cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(entradaProducto.Contrato.Folio.ToString(), fuenteDatos)) { Colspan = 4, Border = 1 };
            cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(etiquetas.EtiquetaProveedor, fuenteDatos)) { Border = 1 };
            cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabla.AddCell(cell);

            var proveedor = "";
            if (entradaProducto.Contrato.Proveedor != null)
            {
                proveedor = entradaProducto.Contrato.Proveedor.Descripcion;
            }else if (entradaProducto.RegistroVigilancia != null &&
                      entradaProducto.RegistroVigilancia.ProveedorMateriasPrimas != null)
            {
                proveedor = entradaProducto.RegistroVigilancia.ProveedorMateriasPrimas.Descripcion;
            }
            cell = new PdfPCell(new Phrase(proveedor, fuenteDatos)) { Colspan = 4, Border = 1 };
            cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(etiquetas.EtiquetaChofer, fuenteDatos)) { Border = 1 };
            cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabla.AddCell(cell);


            if (entradaProducto.Contrato.TipoFlete.TipoFleteId == (int)TipoFleteEnum.LibreAbordo)
            {
                cell = new PdfPCell(new Phrase(entradaProducto.RegistroVigilancia.Chofer, fuenteDatos)) { Colspan = 4, Border = 1 };
            }
            else
            {
                cell = new PdfPCell(new Phrase(entradaProducto.RegistroVigilancia.ProveedorChofer.Chofer.NombreCompleto, fuenteDatos)) { Colspan = 4, Border = 1 };
            }
            cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabla.AddCell(cell);

            
            cell = new PdfPCell(new Phrase(etiquetas.EtiquetaPlacasCamion, fuenteDatos)) { Border = 1 };
            cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabla.AddCell(cell);
            if (entradaProducto.Contrato.TipoFlete.TipoFleteId == (int)TipoFleteEnum.LibreAbordo)
            {
                cell = new PdfPCell(new Phrase(entradaProducto.RegistroVigilancia.CamionCadena, fuenteDatos)) { Colspan = 4, Border = 1 };
            }
            else
            {
                cell = new PdfPCell(new Phrase(entradaProducto.RegistroVigilancia.Camion.PlacaCamion, fuenteDatos)) { Colspan = 4, Border = 1 };
            }
            cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            tabla.AddCell(cell);

            return tabla;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="etiquetas"></param>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        private PdfPTable ObtenerMuestrasForraje(ImpresionBoletaRecepcionInfo etiquetas,EntradaProductoInfo entradaProducto)
        {
            float[] medidaCeldas = { 0.75f, 0.75f };
            var tabla = new PdfPTable(2);

            tabla.SetWidths(medidaCeldas);
            
            var listaMuestras = (from entradaProductoIndicador in entradaProducto.ProductoDetalle
                                     from entradaProductoMuestras in entradaProductoIndicador.ProductoMuestras
                                     where entradaProductoMuestras.EntradaProductoDetalleId == entradaProductoIndicador.EntradaProductoDetalleId && entradaProductoMuestras.EsOrigen == EsOrigenEnum.Destino
                                     select entradaProductoMuestras.Porcentaje).ToList();

            var muestrasOrigen = (from entradaProductoIndicador in entradaProducto.ProductoDetalle
                                 from entradaProductoMuestras in entradaProductoIndicador.ProductoMuestras
                                 where
                                     entradaProductoMuestras.EntradaProductoDetalleId ==
                                     entradaProductoIndicador.EntradaProductoDetalleId &&
                                     entradaProductoMuestras.EsOrigen == EsOrigenEnum.Origen
                                 select entradaProductoMuestras).ToList();

            decimal muestraOrigen = 0;
            if (muestrasOrigen.Any())
            {
                var muestra = muestrasOrigen.FirstOrDefault();
                if (muestra != null)
                {
                    muestraOrigen = muestra.Porcentaje;
                }
                
            }

            var promedio = (from porcentaje in listaMuestras
                             select porcentaje).Average();

            promedio = Math.Round(promedio, 2);

            if (listaMuestras != null && listaMuestras.Count > 0)
            {
                var cell = new PdfPCell(new Phrase(etiquetas.EtiquetaMuestrasHumedades, fuenteTituloSeccion));
                cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER |
                              Rectangle.RIGHT_BORDER;
                cell.BorderWidth = 1;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Colspan = 2;
                tabla.AddCell(cell);

                cell = new PdfPCell(new Phrase(etiquetas.EtiquetaMuestra1, fuenteTituloSeccion));
                cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER |
                              Rectangle.RIGHT_BORDER;
                cell.BorderWidth = 1;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Colspan = 1;
                tabla.AddCell(cell);

                cell = new PdfPCell(new Phrase(etiquetas.EtiquetaMuestra2, fuenteTituloSeccion));
                cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER |
                              Rectangle.RIGHT_BORDER;
                cell.BorderWidth = 1;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Colspan = 1;
                tabla.AddCell(cell);

                for (var i = 0; i < 15; i ++)
                {
                    var posMuestra2 = i + 15;

                    string valorCeldaMuestra = " ";
                    if(i < listaMuestras.Count )
                    {
                        valorCeldaMuestra = listaMuestras[i].ToString();
                    }

                    cell = new PdfPCell(new Phrase(valorCeldaMuestra, fuenteDatos));
                    cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER |
                                  Rectangle.RIGHT_BORDER;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tabla.AddCell(cell);

                    valorCeldaMuestra = " ";
                    if (posMuestra2 < listaMuestras.Count)
                    {
                        valorCeldaMuestra = listaMuestras[posMuestra2].ToString();
                    }

                    cell = new PdfPCell(new Phrase(valorCeldaMuestra, fuenteDatos));

                    //try
                    //{
                    //    cell = new PdfPCell(new Phrase(listaMuestras[posMuestra2].ToString(), fuenteDatos));
                    //}
                    //catch
                    //{
                    //    cell = new PdfPCell(new Phrase("", fuenteDatos));
                    //}

                    cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER |
                                  Rectangle.RIGHT_BORDER;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tabla.AddCell(cell);
                }

                cell = new PdfPCell(new Phrase(etiquetas.EtiquetaHumedadPromedio, fuenteTituloSeccion));
                cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER |
                              Rectangle.RIGHT_BORDER;
                cell.BorderWidth = 1;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tabla.AddCell(cell);

                cell = new PdfPCell(new Phrase(promedio.ToString(CultureInfo.InvariantCulture), fuenteDatos));
                cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER |
                              Rectangle.RIGHT_BORDER;
                cell.BorderWidth = 1;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tabla.AddCell(cell);

                cell = new PdfPCell(new Phrase(etiquetas.EtiquetaHumedadOrigen, fuenteTituloSeccion));
                cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER |
                              Rectangle.RIGHT_BORDER;
                cell.BorderWidth = 1;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tabla.AddCell(cell);

                cell = new PdfPCell(new Phrase(muestraOrigen.ToString(CultureInfo.InvariantCulture), fuenteDatos));
                cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER |
                              Rectangle.RIGHT_BORDER;
                cell.BorderWidth = 1;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tabla.AddCell(cell);
            }

            return tabla;
        }

        /// <summary>
        /// Obtener los datos de bascula.
        /// </summary>
        /// <param name="etiquetas"></param>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        private PdfPTable ObtenerDatosBascula(ImpresionBoletaRecepcionInfo etiquetas, EntradaProductoInfo entradaProducto)
        {
            float[] medidaCeldas = { 0.75f, 0.75f };
            var tabla = new PdfPTable(2);

            tabla.SetWidths(medidaCeldas);

            var cell = new PdfPCell(new Phrase(etiquetas.EtiquetaDatosBascula, fuenteTituloSeccion));
            cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 2;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(etiquetas.EtiquetaPesoBruto, fuenteDatos));
            cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(String.Format("{0:N0} {1}", entradaProducto.PesoBruto, etiquetas.EtiquetaKgs), fuenteDatos));
            cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(etiquetas.EtiquetaPesoTara, fuenteDatos));
            cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(String.Format("{0:N0} {1}", entradaProducto.PesoTara, etiquetas.EtiquetaKgs), fuenteDatos));
            cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(etiquetas.EtiquetaPesoNeto, fuenteDatos));
            cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            tabla.AddCell(cell);

            var neto = entradaProducto.PesoBruto - entradaProducto.PesoTara;
            cell = new PdfPCell(new Phrase(String.Format("{0:N0} {1}", neto, etiquetas.EtiquetaKgs), fuenteDatos));
            cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(etiquetas.EtiquetaDescuento, fuenteDatos));
            cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            tabla.AddCell(cell);

            var descuento = entradaProducto.ProductoDetalle != null ? ObtenerDescuentoBascula(entradaProducto):0;
            cell = new PdfPCell(new Phrase(String.Format("{0:N0} {1}", descuento, etiquetas.EtiquetaKgs), fuenteDatos));
            cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(etiquetas.EtiquetaPesoNetoAnalizado, fuenteDatos));
            cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            tabla.AddCell(cell);

            var netoAnalizado = neto - descuento;
            cell = new PdfPCell(new Phrase(String.Format("{0:N0} {1}", netoAnalizado, etiquetas.EtiquetaKgs), fuenteDatos));
            cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tabla.AddCell(cell);

            if (entradaProducto.Producto.SubFamilia.SubFamiliaID == (int) SubFamiliasEnum.Forrajes)
            {
                cell = new PdfPCell(new Phrase(etiquetas.EtiquetasPiezas, fuenteDatos));
                cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER |
                              Rectangle.RIGHT_BORDER;
                cell.BorderWidth = 1;
                tabla.AddCell(cell);

                cell = new PdfPCell(new Phrase(entradaProducto.Piezas.ToString(), fuenteDatos));
                cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER |
                              Rectangle.RIGHT_BORDER;
                cell.BorderWidth = 1;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tabla.AddCell(cell);
            }

            return tabla;
        }


        /// <summary>
        /// Devuelve el total del descuento por detalle
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        private decimal ObtenerDescuentoBascula(EntradaProductoInfo entradaProducto)
        {
            
            var listaMuestras = (from entradaProductoIndicador in entradaProducto.ProductoDetalle
                                 from entradaProductoMuestras in entradaProductoIndicador.ProductoMuestras
                                 where entradaProductoMuestras.EntradaProductoDetalleId == entradaProductoIndicador.EntradaProductoDetalleId
                                 select entradaProductoMuestras.Descuento).ToList();

            var descuento = (from descuentos in listaMuestras
                             select descuentos).Sum();

            descuento = ((entradaProducto.PesoBruto - entradaProducto.PesoTara)*descuento/100);

            return descuento;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="etiquetas"></param>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        private PdfPTable ObtenerDatosObservaciones(ImpresionBoletaRecepcionInfo etiquetas,EntradaProductoInfo entradaProducto)
        {
            float[] medidaCeldas = { 1.5f };
            var tabla = new PdfPTable(1);

            tabla.SetWidths(medidaCeldas);

            var cell = new PdfPCell(new Phrase(etiquetas.EtiquetaObservaciones, fuenteTituloSeccion));
            cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            cell.VerticalAlignment = 2;
            cell.HorizontalAlignment = 1;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(entradaProducto.Observaciones, fuenteDatos));
            cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            cell.VerticalAlignment = 2;
            tabla.AddCell(cell);

            return tabla;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="etiquetas"></param>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        private PdfPTable ObtenerDatosDestino(ImpresionBoletaRecepcionInfo etiquetas,EntradaProductoInfo entradaProducto)
        {
            float[] medidaCeldas = {7.5f };

            var valorLoteAlmacen = "";
            var valorLoteProceso = "";
            var valorBodegaTerceros = "";

            if (entradaProducto.TipoContrato != null && entradaProducto.TipoContrato.TipoContratoId == (int)TipoContratoEnum.BodegaTercero)
            {
                valorBodegaTerceros = "X";
            }
            else if (entradaProducto.TipoContrato != null && entradaProducto.TipoContrato.TipoContratoId == (int)TipoContratoEnum.Proceso)
            {
                valorLoteProceso = "X";
            }
            else
            {
                valorLoteAlmacen = "X";
            }

            var tabla = new PdfPTable(1);

            tabla.SetWidths(medidaCeldas);

            var cell = new PdfPCell(new Phrase("", fuenteDatos)) {Border = 0};
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase("", fuenteDatos)) {Border = 0};
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase("", fuenteDatos)) {Border = 0 };
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(etiquetas.EtiquetaDestino, fuenteTituloSeccion));
            cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            cell.VerticalAlignment = 2;
            cell.HorizontalAlignment = 1;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase("", fuenteDatos)) {Border = 0 };
            cell.Border =  Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase("", fuenteDatos)) { Colspan = 10, Border = 0 };
            cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            tabla.AddCell(cell);

            var tablaDetalle = new PdfPTable(11);

            cell = new PdfPCell(new Phrase("", fuenteDatos)) { Border = 0 };
            cell.BorderWidth = 1;
            tablaDetalle.AddCell(cell);

            cell = new PdfPCell(new Phrase(etiquetas.EtiquetaLoteAlmacen, fuenteDatos));
            cell.VerticalAlignment = 2;
            cell.HorizontalAlignment = 2;
            cell.Colspan = 2;
            cell.BorderWidth = 0;
            tablaDetalle.AddCell(cell);

            cell = new PdfPCell(new Phrase(valorLoteAlmacen, fuenteDatos));
            cell.BorderWidth = 1;
            cell.VerticalAlignment = 2;
            cell.HorizontalAlignment = 1;
            tablaDetalle.AddCell(cell);

            cell = new PdfPCell(new Phrase(etiquetas.EtiquetaLoteProceso, fuenteDatos));
            cell.VerticalAlignment = 2;
            cell.HorizontalAlignment = 2;
            cell.Colspan = 2;
            cell.BorderWidth = 0;
            tablaDetalle.AddCell(cell);

            cell = new PdfPCell(new Phrase(valorLoteProceso, fuenteDatos));
            cell.BorderWidth = 1;
            cell.VerticalAlignment = 2;
            cell.HorizontalAlignment = 1;
            tablaDetalle.AddCell(cell);

            
            cell = new PdfPCell(new Phrase(etiquetas.EtiquetaBodegaTerceros, fuenteDatos));
            cell.VerticalAlignment = 2;
            cell.HorizontalAlignment = 2;
            cell.Colspan = 2;
            cell.BorderWidth = 0;
            tablaDetalle.AddCell(cell);

            cell = new PdfPCell(new Phrase(valorBodegaTerceros, fuenteDatos));
            cell.BorderWidth = 1;
            cell.VerticalAlignment = 2;
            cell.HorizontalAlignment = 1;
            tablaDetalle.AddCell(cell);

            cell = new PdfPCell(new Phrase("", fuenteDatos)) { Border = 0 };
            tablaDetalle.AddCell(cell);

            cell = new PdfPCell(tablaDetalle);
            cell.Border = cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            cell.Colspan = 10;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase("", fuenteDatos)) { Border = 0 };
            cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase("", fuenteDatos)) { Border = 0 };
            cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
            cell.BorderWidth = 1;
            tabla.AddCell(cell);

            return tabla;
        }

        /// <summary>
        /// Obtiene los datos de descarga.
        /// </summary>
        /// <param name="etiquetas"></param>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        private PdfPTable ObtenerDatosDescarga(ImpresionBoletaRecepcionInfo etiquetas, EntradaProductoInfo entradaProducto)
        {
            float[] medidaCeldas = { 1.5f, 1.5f, 1.5f, 1.5f };
            var tabla = new PdfPTable(4);

            tabla.SetWidths(medidaCeldas);

            var cell = new PdfPCell(new Phrase("", fuenteDatos));
            cell.Colspan = 4;
            cell.Border = 0;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase("", fuenteDatos));
            cell.Colspan = 4;
            cell.Border = 0;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase("", fuenteDatos));
            cell.Colspan = 4;
            cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER;
            cell.BorderWidth = 1;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase("", fuenteDatos));
            cell.Colspan = 4;
            cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(etiquetas.EtiquetaDescarga, fuenteDatos));
            cell.Border = Rectangle.LEFT_BORDER;
            cell.BorderWidth = 1;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(etiquetas.EtiquetaInicio, fuenteDatos));
            cell.Border = 0;
            cell.HorizontalAlignment = 1;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(etiquetas.EtiquetaFin, fuenteDatos));
            cell.Border = 0;
            cell.HorizontalAlignment = 1;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(etiquetas.EtiquetaTiempoEfectivo, fuenteDatos));
            cell.Border = Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            cell.HorizontalAlignment = 1;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase("", fuenteDatos));
            cell.Border = Rectangle.LEFT_BORDER;
            cell.BorderWidth = 1;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(String.Format("{0} {1}", entradaProducto.FechaInicioDescarga.ToShortDateString(), entradaProducto.FechaInicioDescarga.ToShortTimeString()), fuenteDatos));
            cell.Border = 0;
            cell.HorizontalAlignment = 1;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(String.Format("{0} {1}", entradaProducto.FechaFinDescarga.ToShortDateString(), entradaProducto.FechaFinDescarga.ToShortTimeString()), fuenteDatos));
            cell.Border = 0;
            cell.HorizontalAlignment = 1;
            tabla.AddCell(cell);


            var horas = entradaProducto.FechaFinDescarga - entradaProducto.FechaInicioDescarga;
            cell = new PdfPCell(new Phrase(horas.ToString(), fuenteDatos));
            cell.Border = Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            cell.HorizontalAlignment = 1;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase("", fuenteDatos));
            cell.Colspan = 4;
            cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase("", fuenteDatos));
            cell.Colspan = 4;
            cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
            cell.BorderWidth = 1;
            tabla.AddCell(cell);

            return tabla;
        }

        /// <summary>
        /// Obtiene los datos de los responsables Analista y Operador de Bascula.
        /// </summary>
        /// <param name="etiquetas"></param>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        private PdfPTable ObtenerDatosResponsables(ImpresionBoletaRecepcionInfo etiquetas,EntradaProductoInfo entradaProducto)
        {
            float[] medidaCeldas = { 1.5f, 2.5f, 1.5f,};
            var tabla = new PdfPTable(3);
            var fuenteSubrayada = new Font(Font.HELVETICA, 8, Font.UNDERLINE, new Color(0,0,0));

            tabla.SetWidths(medidaCeldas);

            var cell = new PdfPCell(new Phrase("", fuenteDatos)){Colspan = 3,Border = 0};
            tabla.AddCell(cell);
            
            cell = new PdfPCell(new Phrase("", fuenteDatos)) { Colspan = 3, Border = 0 };
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase("", fuenteDatos)) { Colspan = 3, Border = 0 };
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase("", fuenteDatos));
            cell.Colspan = 3;
            cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER;
            cell.BorderWidth = 1;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase("", fuenteDatos));
            cell.Colspan = 3;
            cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(etiquetas.EtiquetaResponsable, fuenteDatos));
            cell.Border = Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 1;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(etiquetas.EtiquetaFirma, fuenteDatos));
            cell.Border = 0;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(etiquetas.EtiquetaHora, fuenteDatos));
            cell.Border = Rectangle.RIGHT_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 1;
            tabla.AddCell(cell);

            // Renglon en blanco
            cell = new PdfPCell(new Phrase(" ", fuenteDatos)) { Colspan = 5, HorizontalAlignment = 1, Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER, BorderWidth = 1 };
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(etiquetas.EtiquetaAnalista, fuenteDatos));
            cell.Border = Rectangle.LEFT_BORDER;
            cell.BorderWidth = 1;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(entradaProducto.OperadorAnalista.NombreCompleto, fuenteSubrayada));
            cell.Border = 0;
            cell.BorderWidth = 1;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(entradaProducto.Fecha.ToShortTimeString(), fuenteSubrayada));
            cell.Border = Rectangle.RIGHT_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 1;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(etiquetas.EtiquetaBascula, fuenteDatos));
            cell.Border = Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 1;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase(entradaProducto.OperadorBascula.NombreCompleto, fuenteSubrayada));
            cell.Border = 0;
            cell.BorderWidth = 1;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            tabla.AddCell(cell);


            cell = new PdfPCell(new Phrase(DateTime.Now.ToShortTimeString(), fuenteSubrayada));
            cell.Border = Rectangle.RIGHT_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 1;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase("", fuenteDatos));
            cell.Colspan = 4;
            cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell.BorderWidth = 1;
            tabla.AddCell(cell);

            cell = new PdfPCell(new Phrase("", fuenteDatos));
            cell.Colspan = 4;
            cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
            cell.BorderWidth = 1;
            tabla.AddCell(cell);

            return tabla;
        }

        /// <summary>
        /// Obtiene los datos del folio de la entrada de producto
        /// </summary>
        /// <param name="etiquetas"></param>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        private PdfPTable ObtenerDatosFolio(ImpresionBoletaRecepcionInfo etiquetas,EntradaProductoInfo entradaProducto)
        {

            var tablaFolio = new PdfPTable(2);
            var cell = new PdfPCell(new Phrase(etiquetas.EtiquetaFolio, fuenteDatos)) { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER, BorderWidth = 1};
            tablaFolio.AddCell(cell);

            var fuenteFolio = new Font(Font.HELVETICA, 10, Font.BOLD, new Color(255, 0, 0));

            cell = new PdfPCell(new Phrase(entradaProducto.Folio.ToString(CultureInfo.InvariantCulture), fuenteFolio)) { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = 2, BorderWidth = 1};
            tablaFolio.AddCell(cell);

            return tablaFolio;
        }

        /// <summary>
        /// Devuelve los datos de la fecha que se genero la boleta recepcion
        /// </summary>
        /// <param name="etiquetas"></param>
        /// <returns></returns>
        private PdfPTable ObtenerDatosFecha(ImpresionBoletaRecepcionInfo etiquetas)
        {

            var tablaFecha = new PdfPTable(2);
            var cell = new PdfPCell(new Phrase(etiquetas.EtiquetaFecha, fuenteDatos)) { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER, BorderWidth = 1 };
            tablaFecha.AddCell(cell);

            cell = new PdfPCell(new Phrase(DateTime.Now.ToShortDateString(), fuenteTituloSeccion)) { Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = 2, BorderWidth = 1};
            tablaFecha.AddCell(cell);

            return tablaFecha;
        }

        /// <summary>
        /// Envia a la pantalla de impresion
        /// </summary>
        /// <param name="archivo"></param>
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
        /// <summary>
        /// Imprime la boleta para los productos que no son forraje
        /// </summary>
        /// <param name="etiquetas"></param>
        /// <param name="entradaProducto"></param>
        internal void ImprimirBoletaMateriaPrima(ImpresionBoletaRecepcionInfo etiquetas, EntradaProductoInfo entradaProducto)
        {
            var reporte = new Document(PageSize.A4, 10, 10, 35, 75);
            try
            {
                const string nombreArchivo = "BoletaRecepcionBasculaMateriaPrima.pdf";
                PdfWriter.GetInstance(reporte, new FileStream(nombreArchivo, FileMode.OpenOrCreate));

                reporte.Open();

                float[] medidaCeldas = { 0.75f, 0.75f, 0.40f, 0.75f, 0.75f };
                var tablePrincipal = new PdfPTable(5);

                tablePrincipal.SetWidths(medidaCeldas);

                //Encabezado
                var cell = new PdfPCell(Encabezado(etiquetas, entradaProducto)) { Colspan = 5, HorizontalAlignment = 1, Border = 0 };
                tablePrincipal.AddCell(cell);

                // Renglon en blanco
                cell = new PdfPCell(new Phrase(" ", fuenteDatos)) { Colspan = 5, Border = 0 };
                tablePrincipal.AddCell(cell);

                // Datos del Producto
                cell = new PdfPCell(ObtenerDatosGenerales(etiquetas, entradaProducto)) { Colspan = 5, HorizontalAlignment = 1, Border = 0 };
                tablePrincipal.AddCell(cell);

                // Renglon en blanco
                cell = new PdfPCell(new Phrase(" ", fuenteDatos)) { Colspan = 5, Border = 0 };
                tablePrincipal.AddCell(cell);

                // Datos del Destino
                cell = new PdfPCell(ObtenerDatosDestino(etiquetas, entradaProducto)) { Colspan = 5, HorizontalAlignment = 1, Border = 0 };
                tablePrincipal.AddCell(cell);

                // Datos de la Descarga
                cell = new PdfPCell(ObtenerDatosDescarga(etiquetas, entradaProducto)) { Colspan = 5, HorizontalAlignment = 1, Border = 0 };
                tablePrincipal.AddCell(cell);

                // Renglon en blanco
                cell = new PdfPCell(new Phrase(" ", fuenteDatos)) { Colspan = 5, Border = 0 };
                tablePrincipal.AddCell(cell);

                // Datos del Analisis de las Muestras
                if (entradaProducto.ProductoDetalle != null)
                {
                    cell = new PdfPCell(DatosAnalisis(etiquetas, entradaProducto)) { Colspan = 2, Border = 0 };
                    tablePrincipal.AddCell(cell);
                }

                // Espacio entre las tablas de Datos Analisis y Bascula
                cell = new PdfPCell(new Phrase(" ", fuenteDatos)) { Colspan = 1, Border = 0 };
                tablePrincipal.AddCell(cell);

                // Datos de la bascula
                cell = new PdfPCell(ObtenerDatosBascula(etiquetas, entradaProducto)) { Colspan = 2, HorizontalAlignment = 1, Border = 0 };
                tablePrincipal.AddCell(cell);

                // Renglon en blanco
                cell = new PdfPCell(new Phrase(" ", fuenteDatos)) { Colspan = 5, Border = 0 };
                tablePrincipal.AddCell(cell);

                // Datos de los responsables.
                cell = new PdfPCell(ObtenerDatosResponsables(etiquetas, entradaProducto)) { Colspan = 5, HorizontalAlignment = 1, Border = 0 };
                tablePrincipal.AddCell(cell);

                reporte.Add(tablePrincipal);

                reporte.Close();

                MostrarPantallaImpresion(AppDomain.CurrentDomain.BaseDirectory + nombreArchivo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                reporte.Close();
            }
        }
        /// <summary>
        /// Imprime el encabezado del Reporte
        /// </summary>
        /// <param name="etiquetas"></param>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        internal PdfPTable Encabezado(ImpresionBoletaRecepcionInfo etiquetas, EntradaProductoInfo entradaProducto)
        {
            var tablaPrincipal = new PdfPTable(5);
            float[] medidaCeldas = { 0.75f, 0.75f, 0.40f, 0.75f, 0.75f };
            tablaPrincipal.SetWidths(medidaCeldas);

            var cell = new PdfPCell(new Phrase(entradaProducto.Organizacion.Descripcion, fuenteTitulo)) { Colspan = 4, HorizontalAlignment = 1, Border = 0 };
            tablaPrincipal.AddCell(cell);

            cell = new PdfPCell(new Phrase(" ", fuenteTitulo)) { Colspan = 1, HorizontalAlignment = 1, Border = 0 };
            tablaPrincipal.AddCell(cell);

            cell = new PdfPCell(new Phrase(entradaProducto.Organizacion.Direccion, fuenteDatos)) { Colspan = 4, HorizontalAlignment = 1, Border = 0 };
            tablaPrincipal.AddCell(cell);

            cell = new PdfPCell(ObtenerDatosFolio(etiquetas, entradaProducto)) { Colspan = 1, HorizontalAlignment = 1, Border = 0 };
            tablaPrincipal.AddCell(cell);

            cell = new PdfPCell(new Phrase(" ", fuenteTitulo)) { Colspan = 5, HorizontalAlignment = 1, Border = 0 };
            tablaPrincipal.AddCell(cell);

            cell = new PdfPCell(new Phrase(etiquetas.EtiquetaTitulo, fuenteTitulo)) { Colspan = 4, HorizontalAlignment = 1, Border = 0 };
            tablaPrincipal.AddCell(cell);

            cell = new PdfPCell(ObtenerDatosFecha(etiquetas)) { Colspan = 1, HorizontalAlignment = 1, Border = 0 };
            tablaPrincipal.AddCell(cell);

            return tablaPrincipal;
        }
        /// <summary>
        /// Imprime la seccion de los analisis del producto
        /// </summary>
        /// <param name="etiquetas"></param>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        internal PdfPTable DatosAnalisis(ImpresionBoletaRecepcionInfo etiquetas, EntradaProductoInfo entradaProducto)
        {
            decimal descuento = 0, porcentaje = 0;
            var tablaPrincipal = new PdfPTable(4);
            float[] medidaCeldas = { 0.75f, 0.75f, 0.75f, 0.75f };
            tablaPrincipal.SetWidths(medidaCeldas);

            var cell = new PdfPCell(new Phrase(etiquetas.EtiquetasDatosAnalisis, fuenteTituloSeccion)) { Colspan = 4, HorizontalAlignment = 1, Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER, BorderWidth = 1 };
            tablaPrincipal.AddCell(cell);

            cell = new PdfPCell(new Phrase(etiquetas.EtiquetaIndicadores, fuenteTituloDatosAnalisis)) { Colspan = 1, HorizontalAlignment = 1, Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER, BorderWidth = 1 };
            tablaPrincipal.AddCell(cell);

            cell = new PdfPCell(new Phrase(etiquetas.EtiquetaCondiciones, fuenteTituloDatosAnalisis)) { Colspan = 1, HorizontalAlignment = 1, Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER, BorderWidth = 1 };
            tablaPrincipal.AddCell(cell);

            cell = new PdfPCell(new Phrase(etiquetas.EtiquetaOrigen, fuenteTituloDatosAnalisis)) { Colspan = 1, HorizontalAlignment = 1, Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER, BorderWidth = 1 };
            tablaPrincipal.AddCell(cell);

            cell = new PdfPCell(new Phrase(etiquetas.EtiquetaDescuentos, fuenteTituloDatosAnalisis)) { Colspan = 1, HorizontalAlignment = 1, Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER, BorderWidth = 1 };
            tablaPrincipal.AddCell(cell);

            foreach (EntradaProductoDetalleInfo detalleProducto in entradaProducto.ProductoDetalle)
            {
                descuento = 0;
                porcentaje = 0;
                cell = new PdfPCell(new Phrase(detalleProducto.Indicador.Descripcion, fuenteDatos)) { Colspan = 1, HorizontalAlignment = 1, Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER, BorderWidth = 1 };
                tablaPrincipal.AddCell(cell);

                // Descuento Para el Destino
                foreach (var productoMuestra in detalleProducto.ProductoMuestras.Where(t=> t.EsOrigen == EsOrigenEnum.Destino))
                {
                    descuento += productoMuestra.Descuento;
                    porcentaje += productoMuestra.Porcentaje;
                }
                cell = new PdfPCell(new Phrase(porcentaje.ToString(CultureInfo.InvariantCulture), fuenteDatos)) { Colspan = 1, HorizontalAlignment = 1, Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER, BorderWidth = 1 };
                tablaPrincipal.AddCell(cell);
                
                porcentaje = 0;

                // Descuentos para el Origen
                foreach (var productoMuestra in detalleProducto.ProductoMuestras.Where(t=>t.EsOrigen == EsOrigenEnum.Origen))
                {
                    porcentaje += productoMuestra.Porcentaje;
                }
                cell = new PdfPCell(new Phrase(porcentaje.ToString(CultureInfo.InvariantCulture) == "0" ? "" : porcentaje.ToString(CultureInfo.InvariantCulture), fuenteDatos)) { Colspan = 1, HorizontalAlignment = 1, Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER, BorderWidth = 1 };
                tablaPrincipal.AddCell(cell);

                cell = new PdfPCell(new Phrase(descuento.ToString(CultureInfo.InvariantCulture), fuenteDatos)) { Colspan = 1, HorizontalAlignment = 1, Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER, BorderWidth = 1 };
                tablaPrincipal.AddCell(cell);
            }

            return tablaPrincipal;
        }
    }
}
