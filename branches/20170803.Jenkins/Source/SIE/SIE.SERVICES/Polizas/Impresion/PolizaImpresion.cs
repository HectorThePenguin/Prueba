using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using SIE.Services.Info.Atributos;
using SIE.Services.Info.Enums;
using SIE.Services.Polizas.Modelos;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;

namespace SIE.Services.Polizas.Impresion
{
    internal class PolizaImpresion<T> where T : PolizaModel
    {
        private XDocument xDocument;
        private T datosPoliza;
        private TipoPoliza tipoPoliza;        
        private XElement itext;

        internal PolizaImpresion(T impresionPoliza, TipoPoliza tipoPoliza)
        {
            xDocument = new XDocument();
            datosPoliza = impresionPoliza;
            this.tipoPoliza = tipoPoliza;
            itext = new XElement("itext");
            xDocument.Add(itext);
        }

        #region METODOS INTERNOS

        internal void GeneraCabecero(string[] anchos, string nombreTabla)
        {
            GenerarTablaCabeceros(xDocument, anchos, nombreTabla);
        }

        internal void GenerarDetalles(string nombreTabla)
        {
            GenerarTablaDetalles(xDocument, nombreTabla);
        }

        internal void GeneraCostos(string nombreTabla)
        {
            GenerarTablaCostos(xDocument, nombreTabla);
        }

        internal void GenerarLineaEnBlanco()
        {
            GenerarEspacioPDF(xDocument);
        }

        internal void GenerarLineaEnBlanco(string nombreTabla, int desplazamiento)
        {
            GenerarEspacioPDF(xDocument, nombreTabla, desplazamiento);
        }

        internal MemoryStream GenerarArchivo()
        {
            return GenerarArchivoPDF(xDocument);
        }

        internal void GenerarRegistroContable(string nombreTabla)
        {
            GenerarTablaRegistroContable(xDocument, nombreTabla);
        }

        #endregion METODOS INTERNOS

        #region METODOS PRIVADOS        

        /// <summary>
        /// Genera el archivo PDF
        /// </summary>
        private MemoryStream GenerarArchivoPDF(XDocument xml)
        {
            const string version = "1.0";
            const string codificacion = "ISO-8859-1";
            var xmlString = String.Format("{0}\r{1}",
                                          "<?xml version='" + version + "' encoding='" + codificacion + "' ?>", xml);

            var output = new MemoryStream();
            var document = new Document();

            PdfWriter.GetInstance(document, output);
            var xmlHandler = new ITextHandler(document);

            var documentoXML = new XmlDocument();
            documentoXML.LoadXml(xmlString);

            xmlHandler.Parse(documentoXML);

            return output;
        }        

        /// <summary>
        /// Genera la estructura de la tabla de los Cabeceros
        /// </summary>
        private void GenerarTablaCabeceros(XDocument xml, string[] anchos, string nombreTabla)
        {
            var existeEncabezado = false;
            XElement tablaCabeceros = ObtenerTabla(xml, nombreTabla);
            if (tablaCabeceros == null)
            {
                tablaCabeceros = new XElement("table",
                                              new XAttribute("Valor", nombreTabla),
                                              new XAttribute("columns", anchos.Length),
                                              new XAttribute("width", "100%"),
                                              new XAttribute("align", "Center"),
                                              new XAttribute("cellpadding", "1"),
                                              new XAttribute("widths", string.Join(";", anchos)),
                                              new XAttribute("borderwidth", "0"));
            }
            else
            {
                existeEncabezado = true;
            }
            PolizaEncabezadoModel encabezado;
            var row = new XElement("row");
            for (var indexEncabezado = 0; indexEncabezado < datosPoliza.Encabezados.Count; indexEncabezado++)
            {
                encabezado = datosPoliza.Encabezados[indexEncabezado];
                var cellDescripcion = new XElement("cell", new XAttribute("borderwidth", "0"),
                                                   encabezado.Desplazamiento == 0
                                                       ? null
                                                       : new XAttribute("colspan", encabezado.Desplazamiento),
                                                   new XAttribute("horizontalalign",
                                                                  string.IsNullOrWhiteSpace(encabezado.Alineacion)
                                                                      ? "left"
                                                                      : encabezado.Alineacion),
                                                   new XAttribute("leading", "8"));

                var valorDescripcion = new XElement("chunk", new XAttribute("font", "Arial"),
                                                    new XAttribute("size", "8"))
                                           {
                                               Value = encabezado.Descripcion
                                           };
                cellDescripcion.Add(valorDescripcion);
                row.Add(cellDescripcion);
            }
            tablaCabeceros.Add(row);
            if (!existeEncabezado)
            {
                xml.Root.Add(tablaCabeceros);
            }
        }

        /// <summary>
        /// Genera la estructura de la tabla del Peso del Ganado
        /// </summary>
        private void GenerarTablaDetalles(XDocument xml, string nombreTabla)
        {
            XElement tablaPesoGanado = ObtenerTabla(xml, nombreTabla);
            var propiedades = (from det in datosPoliza.Detalle
                               let prop = det.GetType().GetProperties()
                               from p in prop
                               let attr = p.GetCustomAttributes(typeof (AtributoDetallePoliza), false)
                               from at in attr
                               where ((AtributoDetallePoliza) at).TipoPoliza == tipoPoliza
                               orderby ((AtributoDetallePoliza) at).Orden
                               select new
                                          {
                                              ((AtributoDetallePoliza) at).Alineacion,
                                              ((AtributoDetallePoliza) at).Desplazamiento,
                                              ((AtributoDetallePoliza)at).AplicarDesplazamiento,
                                              ((AtributoDetallePoliza) at).Orden,
                                              Propiedad = p.Name,
                                          }).Distinct().ToList();
            PolizaDetalleModel detalle;
            for (var indexDetalle = 0; indexDetalle < datosPoliza.Detalle.Count; indexDetalle++)
            {
                var row = new XElement("row");
                detalle = datosPoliza.Detalle[indexDetalle];

                XElement cellDescripcion;
                XElement valorDescripcion;
                for (int indexPropiedades = 0; indexPropiedades < propiedades.Count; indexPropiedades++)
                {
                    cellDescripcion = new XElement("cell", new XAttribute("borderwidth", "0"),
                                                       propiedades[indexPropiedades].Desplazamiento == 0
                                                       || !propiedades[indexPropiedades].AplicarDesplazamiento
                                                           ? null
                                                           : new XAttribute("colspan",
                                                                            propiedades[indexPropiedades].Desplazamiento),
                                                       new XAttribute("horizontalalign",
                                                                      propiedades[indexPropiedades].Alineacion),
                                                       new XAttribute("leading", "8"));
                    object valor =
                        detalle.GetType().GetProperty(propiedades[indexPropiedades].Propiedad).GetValue(detalle, null);
                    valorDescripcion = new XElement("chunk", new XAttribute("font", "Arial"),
                                                        new XAttribute("size", "8"))
                    {
                        Value = valor == null ? string.Empty : valor.ToString()
                    };
                    cellDescripcion.Add(valorDescripcion);
                    row.Add(cellDescripcion);

                    if (propiedades[indexPropiedades].Desplazamiento > 0)
                    {
                        GenerarDesplazamiento(row, propiedades[indexPropiedades].Desplazamiento
                                              , propiedades[indexPropiedades].Alineacion);
                    }
                }
                tablaPesoGanado.Add(row);
            }
        }

        private void GenerarTablaCostos(XDocument xml, string nombreTabla)
        {
            XElement tablaPesoGanado = ObtenerTabla(xml, nombreTabla);
            var propiedades = (from det in datosPoliza.Costos
                               let prop = det.GetType().GetProperties()
                               from p in prop
                               let attr = p.GetCustomAttributes(typeof(AtributoCostos), false)
                               from at in attr
                               where ((AtributoCostos)at).TipoPoliza == tipoPoliza
                               orderby ((AtributoCostos)at).Orden
                               select new
                               {
                                   ((AtributoCostos)at).Alineacion,
                                   ((AtributoCostos)at).Desplazamiento,
                                   ((AtributoCostos)at).Orden,
                                   Propiedad = p.Name,
                               }).Distinct().ToList();
            PolizaCostoModel costo;
            for (var indexDetalle = 0; indexDetalle < datosPoliza.Costos.Count; indexDetalle++)
            {
                var row = new XElement("row");
                costo = datosPoliza.Costos[indexDetalle];

                XElement cellDescripcion;
                XElement valorDescripcion;
                for (int indexPropiedades = 0; indexPropiedades < propiedades.Count; indexPropiedades++)
                {
                    cellDescripcion = new XElement("cell", new XAttribute("borderwidth", "0"),
                                                       new XAttribute("horizontalalign",
                                                                      propiedades[indexPropiedades].Alineacion),
                                                       new XAttribute("leading", "8"));
                    object valor =
                        costo.GetType().GetProperty(propiedades[indexPropiedades].Propiedad).GetValue(costo, null);
                    valorDescripcion = new XElement("chunk", new XAttribute("font", "Arial"),
                                                        new XAttribute("size", "8"))
                    {
                        Value = valor == null ? string.Empty : valor.ToString()
                    };
                    cellDescripcion.Add(valorDescripcion);
                    row.Add(cellDescripcion);

                    if (propiedades[indexPropiedades].Desplazamiento > 0)
                    {
                        GenerarDesplazamiento(row, propiedades[indexPropiedades].Desplazamiento
                                              , propiedades[indexPropiedades].Alineacion);
                    }
                }
                tablaPesoGanado.Add(row);
            }
        }

        private void GenerarTablaRegistroContable(XDocument xml, string nombreTabla)
        {
            XElement tablaPesoGanado = ObtenerTabla(xml, nombreTabla);
            var propiedades = (from det in datosPoliza.RegistroContable
                               let prop = det.GetType().GetProperties()
                               from p in prop
                               let attr = p.GetCustomAttributes(typeof(AtributoRegistroContable), false)
                               from at in attr
                               where ((AtributoRegistroContable)at).TipoPoliza == tipoPoliza
                               orderby ((AtributoRegistroContable)at).Orden
                               select new
                               {
                                   ((AtributoRegistroContable)at).Alineacion,
                                   ((AtributoRegistroContable)at).Orden,
                                   Propiedad = p.Name,
                               }).Distinct().ToList();
            PolizaRegistroContableModel registroContable;
            for (var indexDetalle = 0; indexDetalle < datosPoliza.RegistroContable.Count; indexDetalle++)
            {
                var row = new XElement("row");
                registroContable = datosPoliza.RegistroContable[indexDetalle];

                XElement cellDescripcion;
                XElement valorDescripcion;
                for (int indexPropiedades = 0; indexPropiedades < propiedades.Count; indexPropiedades++)
                {
                    cellDescripcion = new XElement("cell", new XAttribute("borderwidth", "0"),
                                                       new XAttribute("horizontalalign",
                                                                      propiedades[indexPropiedades].Alineacion),
                                                       new XAttribute("leading", "8"));
                    object valor =
                        registroContable.GetType().GetProperty(propiedades[indexPropiedades].Propiedad).GetValue(
                            registroContable, null);
                    valorDescripcion = new XElement("chunk", new XAttribute("font", "Arial"),
                                                        new XAttribute("size", "8"))
                    {
                        Value = valor == null ? string.Empty : valor.ToString()
                    };
                    cellDescripcion.Add(valorDescripcion);
                    row.Add(cellDescripcion);
                }
                tablaPesoGanado.Add(row);
            }
        }

        private XElement ObtenerTabla(XDocument xml, string nombreTabla)
        {
            XElement tablaPesoGanado = (from tabla in xml.Elements().Elements()
                                        let xAttribute = tabla.Attribute("Valor")
                                        where
                                            xAttribute != null && (xAttribute != null &&
                                                                   xAttribute.Value.Equals(nombreTabla))
                                        select tabla).FirstOrDefault();
            return tablaPesoGanado;
        }
        private void GenerarDesplazamiento(XElement row, int desplazamiento, string alineacion)
        {
            for (var indexDesplazamiento = 1; indexDesplazamiento < desplazamiento; indexDesplazamiento++)
            {
                var cellDescripcion = new XElement("cell", new XAttribute("borderwidth", "0"),
                                               new XAttribute("horizontalalign", alineacion),
                                               new XAttribute("leading", "8"));
                var valorDescripcion = new XElement("chunk", new XAttribute("font", "Arial"),
                                                new XAttribute("size", "8"))
                                       {
                                           Value = string.Empty
                                       };
                cellDescripcion.Add(valorDescripcion);
                row.Add(cellDescripcion);
            }
        }

        /// <summary>
        /// Genera un espacio
        /// </summary>
        private void GenerarEspacioPDF(XDocument xml)
        {
            var parrafo = new XElement("paragraph", new XAttribute("leading", "10.0"),
                                       new XAttribute("align", "Center"));
            var linea = new XElement("newline");
            parrafo.Add(linea);
            xml.Root.Add(parrafo);
        }

        /// <summary>
        /// Genera un espacio
        /// </summary>
        private void GenerarEspacioPDF(XDocument xml, string nombreTabla, int desplazamiento)
        {
            var tabla = ObtenerTabla(xml, nombreTabla);
            var row = new XElement("row");
            var parrafo = new XElement("paragraph", new XAttribute("leading", "10.0"),
                                       new XAttribute("align", "Center"));
            var linea = new XElement("newline");
            var cellDescripcion = new XElement("cell", new XAttribute("borderwidth", "0"),
                                               new XAttribute("horizontalalign", "Left"),
                                               new XAttribute("colSpan", desplazamiento),
                                               new XAttribute("leading", "8"));
            parrafo.Add(linea);
            cellDescripcion.Add(parrafo);
            row.Add(cellDescripcion);
            tabla.Add(row);
        }

        #endregion METODOS PRIVADOS
    }
}
