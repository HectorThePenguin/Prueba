using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Xml;
using System.Xml.Linq;
using SIE.Base.Infos;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;
using SIE.Services.Servicios.PL;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;

namespace SIE.Services.Servicios.BL
{
    internal class CheckListCorralBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad CheckListCorral
        /// </summary>
        /// <param name="info"></param>
        /// <param name="xml"></param>
        internal int Guardar(CheckListCorralInfo info, XDocument xml)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    Logger.Info();
                    var checkListCorralDAL = new CheckListCorralDAL();
                    int result = info.CheckListCorralID;
                    GuardarProyeccion(info, checkListCorralDAL);
                    ArmarXML(info, xml);


                    if (info.CheckListCorralID == 0)
                    {
                        result = checkListCorralDAL.Crear(info);
                    }
                    else
                    {
                        checkListCorralDAL.Actualizar(info);
                    }
                    
                    var loteBL = new LoteBL();
                    var filtroCierreCorral = new FiltroCierreCorral
                        {
                            FechaSacrificio = info.FechaSacrificio,
                            FechaCierre = info.Fecha,
                            LoteID = info.LoteID,
                            UsuarioModificacionID = info.UsuarioCreacionID
                        };
                    loteBL.ActualizaFechaCerrado(filtroCierreCorral);
                    transaction.Complete();
                    return result;
                }
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

        private void GuardarProyeccion(CheckListCorralInfo checkListCorral, CheckListCorralDAL checkListCorralDAL)
        {
            var loteProyeccion = checkListCorralDAL.GenerarProyeccion(checkListCorral);
            var loteProyeccionBL = new LoteProyeccionPL();
            var listaReimplantesGuardar = new List<LoteReimplanteInfo>();
            if(loteProyeccion == null)
            {
                return;
            }
            loteProyeccion.UsuarioCreacionID = checkListCorral.UsuarioCreacionID;
            loteProyeccion.RequiereRevision = checkListCorral.RequiereRevision;
            var loteProyeccionID = loteProyeccionBL.Guardar(loteProyeccion);

            var primerReimplante = new LoteReimplanteInfo
                {
                    LoteProyeccionID = loteProyeccionID,
                    NumeroReimplante = loteProyeccion.Proyeccion.PrimerReimplante,
                    FechaProyectada = loteProyeccion.Proyeccion.FechaProyectada1,
                    PesoProyectado = loteProyeccion.Proyeccion.PesoProyectado1,
                    UsuarioCreacionID = checkListCorral.UsuarioCreacionID
                };
            if(primerReimplante.NumeroReimplante != 0 && primerReimplante.FechaProyectada != DateTime.MinValue && primerReimplante.PesoProyectado != 0)
            {
                listaReimplantesGuardar.Add(primerReimplante);
            }

            var segundoReimplante = new LoteReimplanteInfo
            {
                LoteProyeccionID = loteProyeccionID,
                NumeroReimplante = loteProyeccion.Proyeccion.SegundoReimplante,
                FechaProyectada = loteProyeccion.Proyeccion.FechaProyectada2,
                PesoProyectado = loteProyeccion.Proyeccion.PesoProyectado2,
                UsuarioCreacionID = checkListCorral.UsuarioCreacionID
            };
            if (segundoReimplante.NumeroReimplante != 0 && segundoReimplante.FechaProyectada != DateTime.MinValue && segundoReimplante.PesoProyectado != 0)
            {
                listaReimplantesGuardar.Add(segundoReimplante);
            }

            var loteReimplanteBL = new LoteReimplanteBL();
            if(listaReimplantesGuardar.Count > 0)
            {
                loteReimplanteBL.GuardarListaReimplantes(listaReimplantesGuardar);
            }

            checkListCorral.FechaSacrificio = loteProyeccion.FechaSacrificio;
            checkListCorral.Fecha1Reimplante = loteProyeccion.Proyeccion.FechaProyectada1;
            checkListCorral.Fecha2Reimplante = loteProyeccion.Proyeccion.FechaProyectada2;
            checkListCorral.DiasEngorda = loteProyeccion.DiasEngorda;

        }

        /// <summary>
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<CheckListCorralInfo> ObtenerPorPagina(PaginacionInfo pagina, CheckListCorralInfo filtro)
        {
            try
            {
                Logger.Info();
                var checkListCorralDAL = new CheckListCorralDAL();
                ResultadoInfo<CheckListCorralInfo> result = checkListCorralDAL.ObtenerPorPagina(pagina, filtro);
                return result;
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
        /// Obtiene un lista de CheckListCorral
        /// </summary>
        /// <returns></returns>
        internal IList<CheckListCorralInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var checkListCorralDAL = new CheckListCorralDAL();
                IList<CheckListCorralInfo> result = checkListCorralDAL.ObtenerTodos();
                return result;
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
        /// Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal IList<CheckListCorralInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var checkListCorralDAL = new CheckListCorralDAL();
                IList<CheckListCorralInfo> result = checkListCorralDAL.ObtenerTodos(estatus);
                return result;
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
        /// Obtiene una entidad CheckListCorral por su Id
        /// </summary>
        /// <param name="checkListCorralID">Obtiene una entidad CheckListCorral por su Id</param>
        /// <returns></returns>
        internal CheckListCorralInfo ObtenerPorID(int checkListCorralID)
        {
            try
            {
                Logger.Info();
                var checkListCorralDAL = new CheckListCorralDAL();
                CheckListCorralInfo result = checkListCorralDAL.ObtenerPorID(checkListCorralID);
                return result;
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
        /// Obtiene una entidad CheckListCorral por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad CheckListCorral por su Id</param>
        /// <returns></returns>
        internal CheckListCorralInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var checkListCorralDAL = new CheckListCorralDAL();
                CheckListCorralInfo result = checkListCorralDAL.ObtenerPorDescripcion(descripcion);
                return result;
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
        /// Obtiene un registro de CheckListCorral
        /// </summary>
        /// <param name="loteID">identificador del Lote</param>
        /// <param name="organizacionID">identificador de la Organizacion</param>
        /// <returns></returns>
        internal CheckListCorralInfo ObtenerPorLote(int organizacionID, int loteID)
        {
            try
            {
                Logger.Info();
                var checkListCorralDAL = new CheckListCorralDAL();
                CheckListCorralInfo result = checkListCorralDAL.ObtenerPorLote(organizacionID, loteID);
                return result;
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
        /// Genera el XML que se guarda en la tabla 
        /// </summary>
        /// <param name="checkListCorral">contenedor donde se encuentra la información del Check List</param>
        /// <param name="xml">Layout del XML</param>
        /// <returns></returns>
        internal static void ArmarXML(CheckListCorralInfo checkListCorral, XDocument xml)
        {
            ArmarNodoTablaCheckList(xml, checkListCorral);
            ArmarNodoTablaGanado(xml, checkListCorral.ListaConceptos);
            ArmarNodoTablaCorral(xml, checkListCorral.ListaAcciones);

            var version = "1.0";
            var codificacion = "ISO-8859-1";
            var xmlString = String.Format("{0}\r{1}", "<?xml version='" + version + "' encoding='" + codificacion + "' ?>", xml);

            var output = new MemoryStream();
            var document = new Document();
            PdfWriter.GetInstance(document, output);
            var xmlHandler = new ITextHandler(document);

            var documentoXML = new XmlDocument();
            documentoXML.LoadXml(xmlString);

            xmlHandler.Parse(documentoXML);
            checkListCorral.PDF = output.ToArray();
        }

        /// <summary>
        /// Genera la Primera parte del XML "TablaCheckList"
        /// </summary>
        /// <param name="checkListCorral">contenedor donde se encuentra la información del Check List</param>
        /// <param name="xml">Layout del XML</param>
        /// <returns></returns>
        internal static void ArmarNodoTablaCheckList(XDocument xml, CheckListCorralInfo checkListCorral)
        {
            var type = checkListCorral.GetType();
            var properties = type.GetProperties();

            var elementos = xml.Descendants().Where(x => x.Attribute("Valor") != null).ToList();

            foreach (var property in properties)
            {
                var nodo = elementos.FirstOrDefault(no =>
                {
                    var xAttribute = no.Attribute("Valor");
                    return xAttribute != null && xAttribute.Value == property.Name;
                });
                if (nodo == null)
                {
                    continue;
                }
                if (property.PropertyType == typeof(DateTime))
                {
                    var fechas = (DateTime)property.GetValue(checkListCorral, null);
                    nodo.Value = fechas != DateTime.MinValue ? fechas.ToString("dd/MM/yyyy") : string.Empty;
                }
                else
                {
                    nodo.Value = property.GetValue(checkListCorral, null) != null
                                     ? property.GetValue(checkListCorral, null).ToString().Trim()
                                     : string.Empty;
                }
                if (property.Name == "DiasEngorda")
                {
                    nodo.Value = string.Format("{0} {1}", nodo.Value, "Días");
                }
                if (property.Name == "Ocupacion")
                {
                    nodo.Value = string.Format("{0}{1}", nodo.Value, "%");
                }
                if (property.Name == "PesoCorte")
                {
                    var pesoCorte = (int)property.GetValue(checkListCorral, null);
                    nodo.Value = pesoCorte.ToString("N0", CultureInfo.CurrentCulture);
                }
            }
        }

        /// <summary>
        /// Genera la Segunda parte del XML "TablaGanado"
        /// </summary>
        /// <param name="listaConceptos">Contiene todos los conceptos seleccionados para el Ganado</param>
        /// <param name="xml">Layout del XML</param>
        /// <returns></returns>
        internal static void ArmarNodoTablaGanado(XDocument xml, List<ConceptoInfo> listaConceptos)
        {
            var tablaGanado = (from tabla in xml.Elements().Elements()
                               let xAttribute = tabla.Attribute("Valor")
                               where
                                   xAttribute != null && (xAttribute != null &&
                                                                        xAttribute.Value.Equals("TablaGanado"))
                               select tabla);
            var nodoTabla = tablaGanado.FirstOrDefault();
            if (nodoTabla == null)
            {
                return;
            }
            foreach (var concepto in listaConceptos)
            {
                var row = new XElement("row");
                var cellConcepto = new XElement("cell", new XAttribute("red", "236"), new XAttribute("green", "236"), new XAttribute("blue", "236"),
                    new XAttribute("borderwidth", "0.5"), new XAttribute("left", "true"), new XAttribute("right", "true"), new XAttribute("top", "true"),
                    new XAttribute("bottom", "true"), new XAttribute("horizontalalign", "Default"), new XAttribute("leading", "12.0"));
                var valorConcepto = new XElement("chunk", new XAttribute("font", "Arial"), new XAttribute("size", "8.0"))
                {
                    Value = concepto.ConceptoDescripcion ?? string.Empty
                };
                cellConcepto.Add(valorConcepto);
                row.Add(cellConcepto);

                var cellBuenoMalo = new XElement("cell", new XAttribute("red", "236"), new XAttribute("green", "236"), new XAttribute("blue", "236"),
                    new XAttribute("borderwidth", "0.5"), new XAttribute("left", "true"), new XAttribute("right", "true"), new XAttribute("top", "true"),
                    new XAttribute("bottom", "true"), new XAttribute("horizontalalign", "Default"), new XAttribute("leading", "12.0"));
                var valorBuenoMalo = new XElement("chunk", new XAttribute("font", "Arial"), new XAttribute("size", "8.0"))
                {
                    Value = concepto.Bueno ? "Bueno" : "Malo"
                };
                if (concepto.AplicaSiNo)
                {
                    valorBuenoMalo.Value = concepto.Bueno ? "SI" : "NO";
                }
                cellBuenoMalo.Add(valorBuenoMalo);
                row.Add(cellBuenoMalo);

                var cellObservacion = new XElement("cell", new XAttribute("red", "236"), new XAttribute("green", "236"), new XAttribute("blue", "236"),
                    new XAttribute("borderwidth", "0.5"), new XAttribute("left", "true"), new XAttribute("right", "true"), new XAttribute("top", "true"),
                    new XAttribute("bottom", "true"), new XAttribute("horizontalalign", "Default"), new XAttribute("leading", "12.0"));
                var valorObservacion = new XElement("chunk", new XAttribute("font", "Arial"), new XAttribute("size", "8.0"))
                {
                    Value = string.IsNullOrWhiteSpace(concepto.Observaciones) ? string.Empty : concepto.Observaciones
                };
                cellObservacion.Add(valorObservacion);
                row.Add(cellObservacion);

                nodoTabla.Add(row);
            }

        }

        /// <summary>
        /// Genera la Tercera parte del XML "TablaCorral"
        /// </summary>
        /// <param name="listaAcciones">Contiene todos los conceptos seleccionados para el Ganado</param>
        /// <param name="xml">Layout del XML</param>
        /// <returns></returns>
        internal static void ArmarNodoTablaCorral(XDocument xml, List<AccionInfo> listaAcciones)
        {
            var tablaCorral = (from tabla in xml.Elements().Elements()
                               let xAttribute = tabla.Attribute("Valor")
                               where
                                   xAttribute != null && (xAttribute != null &&
                                                                        xAttribute.Value.Equals("TablaCorral"))
                               select tabla);

            var nodoTabla = tablaCorral.FirstOrDefault();
            if (nodoTabla == null)
            {
                return;
            }
            foreach (var accion in listaAcciones)
            {
                var row = new XElement("row");
                var cellDescripcion = new XElement("cell", new XAttribute("red", "236"), new XAttribute("green", "236"), new XAttribute("blue", "236"),
                    new XAttribute("borderwidth", "0.5"), new XAttribute("left", "true"), new XAttribute("right", "true"), new XAttribute("top", "true"),
                    new XAttribute("bottom", "true"), new XAttribute("horizontalalign", "Default"), new XAttribute("leading", "12.0"));
                var valorDescripcion = new XElement("chunk", new XAttribute("font", "Arial"), new XAttribute("size", "8.0"))
                {
                    Value = accion.Descripcion ?? string.Empty
                };
                cellDescripcion.Add(valorDescripcion);
                row.Add(cellDescripcion);

                var cellObservacion = new XElement("cell", new XAttribute("red", "236"), new XAttribute("green", "236"), new XAttribute("blue", "236"),
                    new XAttribute("borderwidth", "0.5"), new XAttribute("left", "true"), new XAttribute("right", "true"), new XAttribute("top", "true"),
                    new XAttribute("bottom", "true"), new XAttribute("horizontalalign", "Default"), new XAttribute("leading", "12.0"));
                var valorObservacion = new XElement("chunk", new XAttribute("font", "Arial"), new XAttribute("size", "8.0"))
                {
                    Value = string.IsNullOrWhiteSpace(accion.Observacion) || accion.Observacion.Equals(ConstantesBL.Seleccione) ? string.Empty : accion.Observacion
                };
                cellObservacion.Add(valorObservacion);
                row.Add(cellObservacion);

                var cellAccionesTomadas = new XElement("cell", new XAttribute("red", "236"), new XAttribute("green", "236"), new XAttribute("blue", "236"),
                   new XAttribute("borderwidth", "0.5"), new XAttribute("left", "true"), new XAttribute("right", "true"), new XAttribute("top", "true"),
                   new XAttribute("bottom", "true"), new XAttribute("horizontalalign", "Default"), new XAttribute("leading", "12.0"));
                var valorAccionesTomadas = new XElement("chunk", new XAttribute("font", "Arial"), new XAttribute("size", "8.0"))
                {
                    Value = accion.AccionesTomadas ?? string.Empty
                };
                cellAccionesTomadas.Add(valorAccionesTomadas);
                row.Add(cellAccionesTomadas);

                nodoTabla.Add(row);
            }
        }
    }
}

