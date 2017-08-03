using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Excepciones;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;

namespace SIE.WinForm.Auxiliar
{
    public class ExportarPDF<T>
        where T : class
    {
        public IList<T> Datos { get; set; }
        public string[] Encabezados { get; set; }
        public string TituloReporte { get; set; }
        public string NombreReporte { get; set; }
        public string SubTitulo { get; set; }
        public string NombreArchivo { get; set; }
        public bool MostrarLogo { get; set; }
        private string RutaFinal { get; set; }



        /// <summary>
        /// Genera el reporte en PDF, y regresa la ruta del archivo generado
        /// </summary>
        public string GenerarPDF()
        {
            try
            {
                if (Datos != null && Datos.Count > 0)
                {
                    RutaFinal = string.Empty;
                    var xml = new XDocument();
                    GenerarEstructuraPrincipal(xml);
                    GenerarCabeceros(xml);
                    GenerarEncabezados(xml);
                    GenerarDatosReporte(xml);
                    GenerarArchivoPDF(xml);
                }
            }
            catch (ExcepcionServicio)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return RutaFinal;
        }

        /// <summary>
        /// Genera la estructura principal del XML
        /// </summary>
        private void GenerarEstructuraPrincipal(XDocument xml)
        {
            var itext = new XElement("itext");

            var tablaCabeceros = new XElement("table", new XAttribute("Valor", "TablaCabeceros"),
                                        new XAttribute("columns", "2"),
                                        new XAttribute("width", "100%"),
                                        new XAttribute("align", "Center"),
                                        new XAttribute("cellpadding", "1"),
                                        new XAttribute("widths", "15;80"),
                                        new XAttribute("borderwidth", "0.8"),
                                        new XAttribute("red", "0"),
                                        new XAttribute("green", "0"),
                                        new XAttribute("blue", "0"));
            itext.Add(tablaCabeceros);

            var type = typeof(T);
            var properties = type.GetProperties();
            var porcentajeColumnas = Math.Round(((decimal)100 / properties.Length), 2);

            string[] arrayColumnas = Enumerable.Repeat(porcentajeColumnas.ToString(CultureInfo.CurrentCulture), properties.Length).ToArray();
            string porcentajes = string.Join(";", arrayColumnas);

            //var porcentajes = new StringBuilder();
            //    for (int index = 0; index < properties.Length; index++)
            //    {
            //        if(porcentajes.Length == 0)
            //        {
            //            porcentajes.Append(porcentajeColumnas).Append(";");
            //        }
            //        else
            //        {
            //            porcentajes.Append(";").Append(porcentajeColumnas);
            //        }
            //    }

            var tablaValores = new XElement("table", new XAttribute("Valor", "TablaValores"),
                                       new XAttribute("columns", properties.Length),
                                       new XAttribute("width", "100%"),
                                       new XAttribute("align", "Center"),
                                       new XAttribute("cellpadding", "2"),
                                       new XAttribute("widths", porcentajes),
                                       new XAttribute("borderwidth", "0.8"),
                                       new XAttribute("red", "0"),
                                       new XAttribute("green", "0"),
                                       new XAttribute("blue", "0"));

            itext.Add(tablaValores);

            xml.Add(itext);
        }

        /// <summary>
        /// Genera el archivo PDF
        /// </summary>
        private void GenerarArchivoPDF(XDocument xml)
        {
            const string version = "1.0";
            const string codificacion = "ISO-8859-1";
            var xmlString = String.Format("{0}\r{1}", "<?xml version='" + version + "' encoding='" + codificacion + "' ?>", xml);

            var output = new MemoryStream();
            var document = new Document();
            //document.SetPageSize(PageSize.A4.Rotate());
            //document.Open();


            PdfWriter.GetInstance(document, output);
            var xmlHandler = new ITextHandler(document);

            var documentoXML = new XmlDocument();
            documentoXML.LoadXml(xmlString);

            xmlHandler.Parse(documentoXML);

            if (string.IsNullOrWhiteSpace(NombreArchivo))
            {
                NombreArchivo = "Reporte";
            }

            var file = new SaveFileDialog { FileName = NombreArchivo, Filter = @"Archivos PDF|*.pdf", Title = @"Guardar Archivo PDF" };
            var result = file.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (file.FileName != "")
                {
                    if (File.Exists(file.FileName))
                    {
                        try
                        {
                            var archivoPDF = new FileStream(file.FileName, FileMode.Open);
                            archivoPDF.Close();
                        }
                        catch (IOException)
                        {
                            throw new ExcepcionServicio(Properties.Resources.ExportarExcel_ArchivoAbierto);
                        }
                    }
                    File.WriteAllBytes(file.FileName, output.ToArray());
                    RutaFinal = file.FileName;
                }
            }

        }

        /// <summary>
        /// Genera los cabeceros del reporte
        /// </summary>
        private void GenerarCabeceros(XDocument xml)
        {
            var tablaCabeceros = (from tabla in xml.Elements().Elements()
                                  let xAttribute = tabla.Attribute("Valor")
                                  where
                                      xAttribute != null && (xAttribute != null &&
                                                                           xAttribute.Value.Equals("TablaCabeceros"))
                                  select tabla);
            var nodoTabla = tablaCabeceros.FirstOrDefault();
            if (nodoTabla == null)
            {
                return;
            }

            #region Empresa
            var rowEmpresa = new XElement("row");

            var cellEmpresa = new XElement("cell", new XAttribute("bgred", "240"),
                                    new XAttribute("bggreen", "240"),
                                    new XAttribute("bgblue", "240"),
                                    new XAttribute("borderwidth", "0"),
                                    new XAttribute("horizontalalign", "Center"),
                                    new XAttribute("leading", "18.0"));

            rowEmpresa.Add(cellEmpresa);

            var cellDescripcionEmpresa = new XElement("cell", new XAttribute("bgred", "240"),
                                                  new XAttribute("bggreen", "240"),
                                                  new XAttribute("bgblue", "240"),
                                                  new XAttribute("borderwidth", "0"),
                                                  new XAttribute("horizontalalign", "Center"),
                                                  new XAttribute("leading", "16.0"));

            var valorDescripcionEmpresa = new XElement("chunk", new XAttribute("style", "bold"),
                                                       new XAttribute("font", "Arial"),
                                                       new XAttribute("size", "18"));

            if (string.IsNullOrWhiteSpace(TituloReporte))
            {
                valorDescripcionEmpresa.Value = string.Empty;
            }
            else
            {
                var espaciosEmpresa = new string(Enumerable.Range(1, 10).Select(i => ' ').ToArray());
                valorDescripcionEmpresa.Value = string.Format("{0}{1}", TituloReporte, espaciosEmpresa);
            }

            cellDescripcionEmpresa.Add(valorDescripcionEmpresa);
            rowEmpresa.Add(cellDescripcionEmpresa);
            nodoTabla.Add(rowEmpresa);
            #endregion Empresa

            #region Titulo
            var rowTitulo = new XElement("row");

            var cellTitulo = new XElement("cell", new XAttribute("bgred", "240"),
                                    new XAttribute("bggreen", "240"),
                                    new XAttribute("bgblue", "240"),
                                    new XAttribute("borderwidth", "0"),
                                    new XAttribute("horizontalalign", "Left"),
                                    new XAttribute("leading", "18.0"));

            if (MostrarLogo)
            {
                var imagen = new XElement("image", new XAttribute("url", "Imagenes\\skLogo.png"),
                                          new XAttribute("plainwidth", "60"),
                                          new XAttribute("plainheight", "20"));

                cellTitulo.Add(imagen);
            }

            rowTitulo.Add(cellTitulo);

            var cellDescripcionTitulo = new XElement("cell", new XAttribute("bgred", "240"),
                                                  new XAttribute("bggreen", "240"),
                                                  new XAttribute("bgblue", "240"),
                                                  new XAttribute("borderwidth", "0"),
                                                  new XAttribute("horizontalalign", "Center"),
                                                  new XAttribute("leading", "16.0"));

            var valorDescripcionTitulo = new XElement("chunk", new XAttribute("style", "bold"),
                                                      new XAttribute("font", "Arial"),
                                                      new XAttribute("size", "14"));

            if (string.IsNullOrWhiteSpace(NombreReporte))
            {
                valorDescripcionTitulo.Value = string.Empty;
            }
            else
            {
                var espaciosTitulo = new string(Enumerable.Range(1, 15).Select(i => ' ').ToArray());
                valorDescripcionTitulo.Value = string.Format("{0}{1}", NombreReporte, espaciosTitulo);
            }
            cellDescripcionTitulo.Add(valorDescripcionTitulo);
            rowTitulo.Add(cellDescripcionTitulo);
            nodoTabla.Add(rowTitulo);
            #endregion Titulo

            #region Subtitulo
            var rowSubtitulo = new XElement("row");

            var cellSubtitulo = new XElement("cell", new XAttribute("bgred", "240"),
                                    new XAttribute("bggreen", "240"),
                                    new XAttribute("bgblue", "240"),
                                    new XAttribute("borderwidth", "0"),
                                    new XAttribute("horizontalalign", "Center"),
                                    new XAttribute("leading", "18.0"));

            rowSubtitulo.Add(cellSubtitulo);

            var cellDescripcionSubtitulo = new XElement("cell", new XAttribute("bgred", "240"),
                                                  new XAttribute("bggreen", "240"),
                                                  new XAttribute("bgblue", "240"),
                                                  new XAttribute("borderwidth", "0"),
                                                  new XAttribute("horizontalalign", "Center"),
                                                  new XAttribute("leading", "16.0"));

            var valorDescripcionSubtitulo = new XElement("chunk", new XAttribute("style", "bold"),
                                                         new XAttribute("font", "Arial"),
                                                         new XAttribute("size", "12"));


            if (string.IsNullOrWhiteSpace(SubTitulo))
            {
                valorDescripcionSubtitulo.Value = string.Empty;
            }
            else
            {
                var espaciosSubtitulo = new string(Enumerable.Range(1, 20).Select(i => ' ').ToArray());
                valorDescripcionSubtitulo.Value = string.Format("{0}{1}", SubTitulo, espaciosSubtitulo);
            }

            cellDescripcionSubtitulo.Add(valorDescripcionSubtitulo);
            rowSubtitulo.Add(cellDescripcionSubtitulo);
            nodoTabla.Add(rowSubtitulo);
            #endregion Subtitulo
        }

        /// <summary>
        /// Genera los encabezados de las columnas
        /// </summary>
        private void GenerarEncabezados(XDocument xml)
        {
            var tablaValores = (from tabla in xml.Elements().Elements()
                                let xAttribute = tabla.Attribute("Valor")
                                where
                                    xAttribute != null && (xAttribute != null &&
                                                                         xAttribute.Value.Equals("TablaValores"))
                                select tabla);
            var nodoTabla = tablaValores.FirstOrDefault();
            if (nodoTabla == null)
            {
                return;
            }
            foreach (var encabezado in Encabezados)
            {
                var row = new XElement("row");
                var cellDescripcion = new XElement("cell", new XAttribute("bgred", "255"),
                                                   new XAttribute("bggreen", "255"),
                                                   new XAttribute("bgblue", "255"),
                                                   new XAttribute("borderwidth", "0.5"),
                                                   new XAttribute("horizontalalign", "Center"),
                                                   new XAttribute("leading", "10.0"));

                var valorDescripcion = new XElement("chunk", new XAttribute("style", "bold"),
                                                    new XAttribute("font", "Arial"),
                                                    new XAttribute("size", "9"))
                    {
                        Value = encabezado
                    };
                cellDescripcion.Add(valorDescripcion);
                row.Add(cellDescripcion);
                nodoTabla.Add(row);
            }
        }

        /// <summary>
        /// Genera la información del reporte
        /// </summary>
        private void GenerarDatosReporte(XDocument xml)
        {
            var type = typeof(T);
            var properties = type.GetProperties();

            var tablaValores = (from tabla in xml.Elements().Elements()
                                let xAttribute = tabla.Attribute("Valor")
                                where
                                    xAttribute != null && (xAttribute != null &&
                                                                         xAttribute.Value.Equals("TablaValores"))
                                select tabla);
            var nodoTabla = tablaValores.FirstOrDefault();
            if (nodoTabla == null)
            {
                return;
            }
            foreach (var item in Datos)
            {
                var row = new XElement("row");
                foreach (var property in properties)
                {
                    string valor;
                    TypeCode typeCode = Type.GetTypeCode(property.PropertyType);
                    bool esNumero = false;
                    switch (typeCode)
                    {
                        case TypeCode.DateTime:
                            var fechas = (DateTime)property.GetValue(item, null);
                            valor = fechas != DateTime.MinValue ? fechas.ToString("dd/MM/yyyy") : string.Empty;
                            break;
                        case TypeCode.Decimal:
                            var valorDecimal = (decimal)property.GetValue(item, null);
                            valor = valorDecimal.ToString("N", CultureInfo.CurrentCulture);
                            esNumero = true;
                            break;
                        case TypeCode.Int16:
                        case TypeCode.Int64:
                        case TypeCode.Int32:
                        case TypeCode.Single:
                        case TypeCode.Double:
                            valor = property.GetValue(item, null) != null
                                        ? property.GetValue(item, null).ToString()
                                        : string.Empty;
                            esNumero = true;
                            break;
                        default:
                            valor = property.GetValue(item, null) != null
                                        ? property.GetValue(item, null).ToString()
                                        : string.Empty;
                            break;
                    }


                    var cellDescripcion = new XElement("cell",
                                                       new XAttribute("borderwidth", "0"),
                                                       new XAttribute("horizontalalign", esNumero ? "right" : "Center"),
                                                       new XAttribute("leading", "10.0"));

                    var valorDescripcion = new XElement("chunk", new XAttribute("style", "bold"),
                                                        new XAttribute("font", "Arial"),
                                                        new XAttribute("size", "8"))
                        {
                            Value = valor
                        };
                    cellDescripcion.Add(valorDescripcion);

                    row.Add(cellDescripcion);
                }
                nodoTabla.Add(row);
            }


        }
    }
}
