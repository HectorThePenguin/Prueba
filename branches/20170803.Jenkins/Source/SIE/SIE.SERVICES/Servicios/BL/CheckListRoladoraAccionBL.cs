using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.ORM;

namespace SIE.Services.Servicios.BL
{
    public class CheckListRoladoraAccionBL : IDisposable
    {
        CheckListRoladoraAccionDAL checkListRoladoraAccionDAL;

        public CheckListRoladoraAccionBL()
        {
            checkListRoladoraAccionDAL = new CheckListRoladoraAccionDAL();
        }

        public void Dispose()
        {
            checkListRoladoraAccionDAL.Disposed += (s, e) =>
            {
                checkListRoladoraAccionDAL = null;
            };
            checkListRoladoraAccionDAL.Dispose();
        }
        
        /// <summary>
        /// Obtiene una lista paginada de CheckListRoladoraAccion
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CheckListRoladoraAccionInfo> ObtenerPorPagina(PaginacionInfo pagina, CheckListRoladoraAccionInfo filtro)
        {
            try
            {
                Logger.Info();
                return checkListRoladoraAccionDAL.ObtenerPorPagina(pagina, filtro);
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de CheckListRoladoraAccion
        /// </summary>
        /// <returns></returns>
        public IList<CheckListRoladoraAccionInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return checkListRoladoraAccionDAL.ObtenerTodos().ToList();
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de CheckListRoladoraAccion filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IList<CheckListRoladoraAccionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return checkListRoladoraAccionDAL.ObtenerTodos().Where(e=> e.Activo == estatus).ToList();
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad de CheckListRoladoraAccion por su Id
        /// </summary>
        /// <param name="checkListRoladoraAccionId">Obtiene una entidad CheckListRoladoraAccion por su Id</param>
        /// <returns></returns>
        public CheckListRoladoraAccionInfo ObtenerPorID(int checkListRoladoraAccionId)
        {
            try
            {
                Logger.Info();
                return checkListRoladoraAccionDAL.ObtenerTodos().Where(e=> e.CheckListRoladoraAccionID == checkListRoladoraAccionId).FirstOrDefault();
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad de CheckListRoladoraAccion por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad CheckListRoladoraAccion por su descripcion</param>
        /// <returns></returns>
        public CheckListRoladoraAccionInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                return checkListRoladoraAccionDAL.ObtenerTodos().Where(e=> e.Descripcion.ToLower() == descripcion.ToLower()).FirstOrDefault();
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para Guardar/Modificar una entidad CheckListRoladoraAccion
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(CheckListRoladoraAccionInfo info)
        {
            try
            {
                Logger.Info();
                return checkListRoladoraAccionDAL.Guardar(info);
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public dynamic ObtenerParametros()
        {
            try
            {
                Logger.Info();
                var checkListRoladoraAccionDal = new Integracion.DAL.Implementacion.CheckListRoladoraAccionDAL();
                IList<CheckListRoladoraAccionInfo> parametros = checkListRoladoraAccionDal.ObtenerParametros();
                dynamic parametrosPorTipo = ObtenerParametrosPorTipo(parametros);
                return parametrosPorTipo;
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

        private dynamic ObtenerParametrosPorTipo(IList<CheckListRoladoraAccionInfo> parametros)
        {
            List<int> tipoPreguntaIds =
                parametros.Select(tipos => tipos.CheckListRoladoraRango.Pregunta.TipoPregunta.TipoPreguntaID).Distinct()
                    .ToList();
            var resultado = new List<dynamic>();
            for (int indexTipoPregunta = 0; indexTipoPregunta < tipoPreguntaIds.Count; indexTipoPregunta++)
            {
                List<PreguntaInfo> preguntas =
                    parametros.Where(
                        tipo =>
                        tipo.CheckListRoladoraRango.Pregunta.TipoPregunta.TipoPreguntaID ==
                        tipoPreguntaIds[indexTipoPregunta]).Select(preg => new PreguntaInfo
                                                                               {
                                                                                   Descripcion =
                                                                                       preg.CheckListRoladoraRango.
                                                                                       Pregunta.Descripcion,
                                                                                   PreguntaID =
                                                                                       preg.CheckListRoladoraRango.
                                                                                       Pregunta.PreguntaID
                                                                               }).ToList();
                preguntas.ForEach(preg =>
                                      {
                                          List<CheckListRoladoraAccionInfo> acciones = parametros.Where(
                                              tipo =>
                                              tipo.CheckListRoladoraRango.Pregunta.PreguntaID == preg.PreguntaID).
                                              Select(
                                                  accion => new CheckListRoladoraAccionInfo
                                                                {
                                                                    Descripcion = accion.Descripcion,
                                                                    CheckListRoladoraAccionID =
                                                                        accion.CheckListRoladoraAccionID,
                                                                }).ToList();
                                          List<CheckListRoladoraRangoInfo> rangos = parametros.Where(
                                              tipo =>
                                              tipo.CheckListRoladoraRango.Pregunta.PreguntaID == preg.PreguntaID).
                                              Select(
                                                  rango => new CheckListRoladoraRangoInfo
                                                               {
                                                                   Descripcion =
                                                                       rango.CheckListRoladoraRango.Descripcion,
                                                                   CheckListRoladoraRangoID =
                                                                       rango.CheckListRoladoraRango.
                                                                       CheckListRoladoraRangoID
                                                               }).ToList();
                                          var tipoPregunta =
                                              parametros.Where(
                                                  tipo =>
                                                  tipo.CheckListRoladoraRango.Pregunta.TipoPregunta.TipoPreguntaID ==
                                                  tipoPreguntaIds[indexTipoPregunta]).
                                                  Select(
                                                      tipoPreg =>
                                                      tipoPreg.CheckListRoladoraRango.Pregunta.TipoPregunta.Descripcion)
                                                  .FirstOrDefault();
                                          resultado.Add(
                                              new
                                                  {
                                                      Titulo = tipoPregunta,
                                                      Xml = ConvertirSemaforoXML(parametros),
                                                      Pregunta =
                                                  new
                                                      {
                                                          preg.Descripcion,
                                                          preg.PreguntaID,                                                          
                                                          Preguntas = preguntas,
                                                          Rangos = rangos,
                                                          Acciones = acciones
                                                      }
                                                  });
                                      });
            }
            dynamic titulos = resultado.Select(tit => tit.Titulo).Distinct().ToList();
            dynamic resultadoParametros = new List<dynamic>();
            for (int i = 0; i < titulos.Count; i++)
            {
                dynamic datos = resultado.Where(tit => tit.Titulo.Equals(titulos[i])).ToList();
                var param = new List<dynamic>();
                for (int j = 0; j < datos.Count; j++)
                {
                    param.Add(datos[j].Pregunta);
                }
                resultadoParametros.Add(new {Titulo = titulos[i], Parametros = param, datos[0].Xml});
            }

            return resultadoParametros;
        }

        /// <summary>
        /// Obtiene un xml con los indicadores
        /// </summary>
        /// <param name="semaforo"></param>
        /// <returns></returns>
        private string ConvertirSemaforoXML(IEnumerable<CheckListRoladoraAccionInfo> semaforo)
        {
            var xml =
                new XElement("ROOT",
                             from sem in semaforo
                             select new XElement("semaforo",
                                                 new XElement("CodigoColor",
                                                              sem.CheckListRoladoraRango.CodigoColor),
                                                 new XElement("DescripcionAccion", sem.Descripcion),
                                                 new XElement("TipoPreguntaID",
                                                              sem.CheckListRoladoraRango.Pregunta.TipoPregunta.
                                                                  TipoPreguntaID),
                                                 new XElement("TipoPregunta",
                                                              sem.CheckListRoladoraRango.Pregunta.TipoPregunta.
                                                                  Descripcion),
                                                 new XElement("PreguntaID",
                                                              sem.CheckListRoladoraRango.Pregunta.PreguntaID),
                                                 new XElement("Pregunta",
                                                              sem.CheckListRoladoraRango.Pregunta.Descripcion),
                                                 new XElement("CheckListRoladoraRangoID",
                                                              sem.CheckListRoladoraRango.CheckListRoladoraRangoID),
                                                 new XElement("DescripcionRango",
                                                              sem.CheckListRoladoraRango.Descripcion),
                                                 new XElement("CheckListRoladoraAccionID",
                                                              sem.CheckListRoladoraAccionID),
                                                 new XElement("Indice",
                                                              sem.Indice)
                                 ));
            return xml.ToString();
        }

        public string GenerarEstilosParametros()
        {
            try
            {
                Logger.Info();
                var checkListRoladoraAccionDal = new Integracion.DAL.Implementacion.CheckListRoladoraAccionDAL();
                IList<CheckListRoladoraAccionInfo> parametros = checkListRoladoraAccionDal.ObtenerParametros();
                string xmlParametro = string.Empty;
                if (parametros != null && parametros.Any())
                {
                    xmlParametro = GenerarHojaEstilosParametros(parametros);
                }
                return xmlParametro;
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

        private string GenerarHojaEstilosParametros(IList<CheckListRoladoraAccionInfo> parametros)
        {
            var SB = new StringBuilder();
            for (int indexParametro = 0; indexParametro < parametros.Count; indexParametro++)
            {
                SB.Append(".").Append(
                    parametros[indexParametro].CheckListRoladoraRango.Pregunta.TipoPregunta.Descripcion.Replace(
                        "(", string.Empty).Replace(")", string.Empty).Replace(" ", string.Empty));
                long indice = parametros[indexParametro].Indice;
                SB.Append(indice);
                SB.Append("{").AppendLine();
                SB.Append("background: none repeat scroll 0 0 ").Append(
                    parametros[indexParametro].CheckListRoladoraRango.CodigoColor).
                    Append(" !important;").AppendLine();
                SB.Append("border-radius: 50%  !important;").AppendLine();
                SB.Append("width: 20px  !important;").AppendLine();
                SB.Append("height: 20px  !important; ").AppendLine();
                SB.Append("margin-top: 7px !important; ").AppendLine();
                SB.Append("margin-left: 5px !important; ").AppendLine().Append("}");

                SB.AppendLine();
            }

            SB.Append(".deshabilitado {").AppendLine();
            SB.Append("background: none repeat scroll 0 0 #B4B4B4 !important;").AppendLine();
            SB.Append("border-radius: 50%  !important;").AppendLine();
            SB.Append("width: 20px  !important;").AppendLine();
            SB.Append("height: 20px  !important; ").AppendLine();
            SB.Append("margin-top: 7px !important; ").AppendLine();
            SB.Append("margin-left: 5px !important; ").AppendLine().Append("}");
            SB.AppendLine();

            return SB.ToString();
        }
    }
}
