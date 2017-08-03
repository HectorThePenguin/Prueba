using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    public class IndicadorObjetivoBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad IndicadorObjetivo
        /// </summary>
        /// <param name="info"></param>
        public int Guardar(IndicadorObjetivoInfo info)
        {
            try
            {
                Logger.Info();
                var indicadorObjetivoDAL = new IndicadorObjetivoDAL();
                int result = info.IndicadorObjetivoID;
                if (info.IndicadorObjetivoID == 0)
                {
                    result = indicadorObjetivoDAL.Crear(info);
                }
                else
                {
                    indicadorObjetivoDAL.Actualizar(info);
                }
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
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<IndicadorObjetivoInfo> ObtenerPorPagina(PaginacionInfo pagina, IndicadorObjetivoInfo filtro)
        {
            try
            {
                Logger.Info();
                var indicadorObjetivoDAL = new IndicadorObjetivoDAL();
                ResultadoInfo<IndicadorObjetivoInfo> result = indicadorObjetivoDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de IndicadorObjetivo
        /// </summary>
        /// <returns></returns>
        public IList<IndicadorObjetivoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var indicadorObjetivoDAL = new IndicadorObjetivoDAL();
                IList<IndicadorObjetivoInfo> result = indicadorObjetivoDAL.ObtenerTodos();
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
        public IList<IndicadorObjetivoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var indicadorObjetivoDAL = new IndicadorObjetivoDAL();
                IList<IndicadorObjetivoInfo> result = indicadorObjetivoDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad IndicadorObjetivo por su Id
        /// </summary>
        /// <param name="indicadorObjetivoID">Obtiene una entidad IndicadorObjetivo por su Id</param>
        /// <returns></returns>
        public IndicadorObjetivoInfo ObtenerPorID(int indicadorObjetivoID)
        {
            try
            {
                Logger.Info();
                var indicadorObjetivoDAL = new IndicadorObjetivoDAL();
                IndicadorObjetivoInfo result = indicadorObjetivoDAL.ObtenerPorID(indicadorObjetivoID);
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
        /// Obtiene una entidad IndicadorObjetivo por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public IndicadorObjetivoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var indicadorObjetivoDAL = new IndicadorObjetivoDAL();
                IndicadorObjetivoInfo result = indicadorObjetivoDAL.ObtenerPorDescripcion(descripcion);
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
        /// Metodo para Obtener el semaforo
        /// </summary>
        public string ObtenerSemaforo(int pedidoID, int productoID, int organizacionID)
        {
            try
            {
                Logger.Info();
                var indicadorObjetivoDAL = new IndicadorObjetivoDAL();
                List<IndicadorObjetivoSemaforoInfo> indicadorObjetivoSemaforo =
                    indicadorObjetivoDAL.ObtenerSemaforo(pedidoID, productoID, organizacionID);
                if (indicadorObjetivoSemaforo != null && indicadorObjetivoSemaforo.Any())
                {
                    ValidarRangoObjetivo(indicadorObjetivoSemaforo);
                }
                var semaforo = indicadorObjetivoSemaforo == null || !indicadorObjetivoSemaforo.Any()
                                   ? null
                                   : ConvertirSemaforoXML(indicadorObjetivoSemaforo);
                return semaforo;
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

        private void ValidarRangoObjetivo(List<IndicadorObjetivoSemaforoInfo> indicadorObjetivoSemaforo)
        {
            List<int> tipoObjetivoCalidad =
                indicadorObjetivoSemaforo.Select(toc => toc.TipoObjetivoCalidadID).Distinct().ToList();
            for (var indexTipoObjetivoCalidad = 0
                   ; indexTipoObjetivoCalidad < tipoObjetivoCalidad.Count
                   ; indexTipoObjetivoCalidad++)
            {
                List<IndicadorObjetivoSemaforoInfo> indicador =
                    indicadorObjetivoSemaforo.Where(
                       ind => ind.TipoObjetivoCalidadID == tipoObjetivoCalidad[indexTipoObjetivoCalidad]).ToList();
                indicador.ForEach(indicadorRango =>
                                      {
                                          switch ((TipoObjetivoCalidad) tipoObjetivoCalidad[indexTipoObjetivoCalidad])
                                          {
                                              case TipoObjetivoCalidad.Minimo:
                                                  indicadorRango.Rango = Convert.ToString(indicadorRango.ObjetivoMinimo);
                                                  break;
                                              case TipoObjetivoCalidad.Maximo:
                                                  indicadorRango.Rango = Convert.ToString(indicadorRango.ObjetivoMaximo);
                                                  break;
                                              case TipoObjetivoCalidad.Rango:
                                                  indicadorRango.Rango = string.Format("{0}-{1}",
                                                                                       indicadorRango.ObjetivoMinimo,
                                                                                       indicadorRango.ObjetivoMaximo);
                                                  break;
                                              case TipoObjetivoCalidad.Optimo:
                                                  indicadorRango.Rango = string.Format("{0}",
                                                                                       indicadorRango.ObjetivoMinimo +
                                                                                       indicadorRango.Tolerancia);
                                                  break;
                                          }
                                      });
            }
        }

        /// <summary>
        /// Obtiene un xml con los indicadores
        /// </summary>
        /// <param name="semaforo"></param>
        /// <returns></returns>
        private string ConvertirSemaforoXML(IEnumerable<IndicadorObjetivoSemaforoInfo> semaforo)
        {
            var xml =
                new XElement("ROOT",
                             from sem in semaforo
                             select new XElement("semaforo",
                                                 new XElement("CodigoColor",
                                                              sem.CodigoColor),
                                                 new XElement("ColorDescripcion", sem.ColorDescripcion),
                                                 new XElement("ColorObjetivoID", sem.ColorObjetivoID),
                                                 new XElement("Indicador", sem.Indicador),
                                                 new XElement("IndicadorID", sem.IndicadorID),
                                                 new XElement("IndicadorObjetivoID", sem.IndicadorObjetivoID),
                                                 new XElement("Medicion",
                                                              sem.Medicion.Replace("'", string.Empty)),
                                                 new XElement("ObjetivoMaximo",
                                                              sem.ObjetivoMaximo),
                                                 new XElement("ObjetivoMinimo", sem.ObjetivoMinimo),
                                                 new XElement("PedidoDetalleID", sem.PedidoDetalleID),
                                                 new XElement("Tendencia", sem.Tendencia),
                                                 new XElement("TipoObjetivoCalidad", sem.TipoObjetivoCalidad),
                                                 new XElement("TipoObjetivoCalidadID", sem.TipoObjetivoCalidadID),
                                                 new XElement("Rango", sem.Rango),
                                                 new XElement("ColorSemaforo", sem.ColorSemaforo),
                                                 new XElement("Resultado", sem.Resultado),
                                                 new XElement("ColorObjetivoIDValor", sem.ColorObjetivoIDValor),
                                                 new XElement("Tolerancia", sem.Tolerancia),
                                                 new XElement("Valido", sem.Valido)
                                 ));
            return xml.ToString();
        }

        /// <summary>
        /// Obtiene los valores del semaforo para los valores ingresados
        /// </summary>
        /// <param name="valorIndicador"></param>
        /// <param name="descripcionIndicador"></param>
        /// <param name="indicadores"></param>
        /// <returns></returns>
        public string ValidarIndicadores(decimal valorIndicador, string descripcionIndicador, string indicadores)
        {
            var xDocument = XDocument.Parse(indicadores);
            var semaforoIndicadores = xDocument.Descendants("semaforo")
                .Select(element => new IndicadorObjetivoSemaforoInfo
                                       {
                                           CodigoColor = element.Element("CodigoColor").Value,
                                           ColorDescripcion = element.Element("ColorDescripcion").Value,
                                           ColorObjetivoID = Convert.ToInt32(element.Element("ColorObjetivoID").Value),
                                           Indicador = element.Element("Indicador").Value,
                                           IndicadorID = Convert.ToInt32(element.Element("IndicadorID").Value),
                                           IndicadorObjetivoID =
                                               Convert.ToInt32(element.Element("IndicadorObjetivoID").Value),
                                           Medicion = element.Element("Medicion").Value,
                                           ObjetivoMaximo = Convert.ToDecimal(element.Element("ObjetivoMaximo").Value),
                                           ObjetivoMinimo = Convert.ToDecimal(element.Element("ObjetivoMinimo").Value),
                                           PedidoDetalleID = Convert.ToInt32(element.Element("PedidoDetalleID").Value),
                                           Rango = element.Element("Rango").Value,
                                           Tendencia = element.Element("Tendencia").Value,
                                           TipoObjetivoCalidad = element.Element("TipoObjetivoCalidad").Value,
                                           TipoObjetivoCalidadID =
                                               Convert.ToInt32(element.Element("TipoObjetivoCalidadID").Value),
                                           Tolerancia = Convert.ToDecimal(element.Element("Tolerancia").Value),
                                           Resultado = element.Element("Resultado").Value,
                                           ColorObjetivoIDValor = Convert.ToInt32(element.Element("ColorObjetivoIDValor").Value),
                                           ColorSemaforo = element.Element("ColorSemaforo").Value,
                                           Valido = Convert.ToInt32(element.Element("Valido").Value)
                                       }).ToList();
            List<int> tipoObjetivoCalidad =
                semaforoIndicadores.Select(toc => toc.TipoObjetivoCalidadID).Distinct().ToList();
            for (var indexTipoObjetivoCalidad = 0
                   ; indexTipoObjetivoCalidad < tipoObjetivoCalidad.Count
                   ; indexTipoObjetivoCalidad++)
            {
                List<IndicadorObjetivoSemaforoInfo> indicador =
                    semaforoIndicadores.Where(
                        ind => ind.Indicador.Equals(descripcionIndicador)).Distinct().ToList();
                indicador.ForEach(indicadorRango =>
                                      {
                                          var clase = string.Empty;
                                          var simbolo = string.Empty;
                                          switch ((TipoObjetivoCalidad) indicadorRango.TipoObjetivoCalidadID)
                                          {
                                              case TipoObjetivoCalidad.Minimo:
                                                  if (valorIndicador >= indicadorRango.ObjetivoMinimo)
                                                  {
                                                      clase = string.Format("{0}{1}",
                                                                            TipoObjetivoCalidad.Minimo.ToString(),
                                                                            "MayorIgual");
                                                      simbolo = ">=";
                                                      indicadorRango.Valido = 1;
                                                  }
                                                  else
                                                  {
                                                      if (valorIndicador < indicadorRango.ObjetivoMinimo)
                                                      {
                                                          clase = string.Format("{0}{1}",
                                                                                TipoObjetivoCalidad.Minimo.ToString(),
                                                                                "Menor");
                                                          simbolo = "<";
                                                          indicadorRango.Valido = 0;
                                                      }
                                                  }
                                                  break;
                                              case TipoObjetivoCalidad.Maximo:
                                                  if (valorIndicador <= indicadorRango.ObjetivoMaximo)
                                                  {
                                                      clase = string.Format("{0}{1}",
                                                                            TipoObjetivoCalidad.Maximo.ToString(),
                                                                            "MenorIgual");
                                                      simbolo = "<=";
                                                      indicadorRango.Valido = 1;
                                                  }
                                                  else
                                                  {
                                                      if (valorIndicador > indicadorRango.ObjetivoMaximo)
                                                      {
                                                          clase = string.Format("{0}{1}",
                                                                                TipoObjetivoCalidad.Maximo.ToString(),
                                                                                "Mayor");
                                                          simbolo = ">";
                                                          indicadorRango.Valido = 0;
                                                      }
                                                  }
                                                  break;
                                              case TipoObjetivoCalidad.Rango:
                                                  if (valorIndicador >= indicadorRango.ObjetivoMinimo
                                                      && valorIndicador <= indicadorRango.ObjetivoMaximo)
                                                  {
                                                      clase = string.Format("{0}{1}",
                                                                            TipoObjetivoCalidad.Rango.ToString(),
                                                                            "Igual");
                                                      simbolo = "=";
                                                      indicadorRango.Valido = 1;
                                                  }
                                                  else
                                                  {
                                                      if (valorIndicador > indicadorRango.ObjetivoMaximo)
                                                      {
                                                          clase = string.Format("{0}{1}",
                                                                                TipoObjetivoCalidad.Rango.ToString(),
                                                                                "Mayor");
                                                          simbolo = ">";
                                                          indicadorRango.Valido = 0;
                                                      }
                                                      else
                                                      {
                                                          if (valorIndicador < indicadorRango.ObjetivoMaximo)
                                                          {
                                                              clase = string.Format("{0}{1}",
                                                                                    TipoObjetivoCalidad.Rango.ToString(),
                                                                                    "Menor");
                                                              simbolo = "<";
                                                              indicadorRango.Valido = 0;
                                                          }
                                                      }
                                                  }
                                                  break;
                                              case TipoObjetivoCalidad.Optimo:
                                                  if (valorIndicador >= indicadorRango.ObjetivoMinimo
                                                      && valorIndicador <= (indicadorRango.ObjetivoMinimo + indicadorRango.Tolerancia))
                                                  {
                                                      clase = string.Format("{0}{1}",
                                                                            TipoObjetivoCalidad.Optimo.ToString(),
                                                                            "Igual");
                                                      simbolo = "=";
                                                      indicadorRango.Valido = 1;
                                                  }
                                                  else
                                                  {
                                                      if (valorIndicador >
                                                          (indicadorRango.ObjetivoMinimo + indicadorRango.Tolerancia))
                                                      {
                                                          clase = string.Format("{0}{1}",
                                                                                TipoObjetivoCalidad.Optimo.ToString(),
                                                                                "Mayor");
                                                          simbolo = ">";
                                                          indicadorRango.Valido = 0;
                                                      }
                                                      else
                                                      {
                                                          if (valorIndicador < indicadorRango.ObjetivoMinimo)
                                                          {
                                                              clase = string.Format("{0}{1}",
                                                                                    TipoObjetivoCalidad.Optimo.ToString(),
                                                                                    "Menor");
                                                              simbolo = "<";
                                                              indicadorRango.Valido = 0;
                                                          }
                                                      }
                                                  }
                                                  break;
                                          }
                                          if (valorIndicador > 0)
                                          {
                                              indicadorRango.ColorObjetivoIDValor =
                                                  semaforoIndicadores.Where(
                                                      color =>
                                                      color.TipoObjetivoCalidadID ==
                                                      indicadorRango.TipoObjetivoCalidadID &&
                                                      simbolo.Equals(color.Tendencia)).Select(id => id.ColorObjetivoID).
                                                      FirstOrDefault();
                                              indicadorRango.ColorSemaforo = clase;
                                          }
                                          else
                                          {
                                              indicadorRango.Valido = 1;
                                              indicadorRango.ColorSemaforo = string.Empty;
                                          }
                                          indicadorRango.Resultado = Convert.ToString(valorIndicador);
                                      });
            }
            string semaforo = ConvertirSemaforoXML(semaforoIndicadores);
            return semaforo;
        }

        /// <summary>
        /// Obtiene una entidad IndicadorObjetivo por su Id
        /// </summary>
        /// <param name="filtros">Obtiene una entidad IndicadorObjetivo por sus filtros</param>
        /// <returns></returns>
        public IndicadorObjetivoInfo ObtenerPorFiltros(IndicadorObjetivoInfo filtros)
        {
            try
            {
                Logger.Info();
                var indicadorObjetivoDAL = new IndicadorObjetivoDAL();
                IndicadorObjetivoInfo result = indicadorObjetivoDAL.ObtenerPorFiltros(filtros);
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
    }
}
