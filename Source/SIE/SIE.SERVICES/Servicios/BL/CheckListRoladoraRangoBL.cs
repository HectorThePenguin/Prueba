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
    public class CheckListRoladoraRangoBL : IDisposable
    {
        CheckListRoladoraRangoDAL checkListRoladoraRangoDAL;

        public CheckListRoladoraRangoBL()
        {
            checkListRoladoraRangoDAL = new CheckListRoladoraRangoDAL();
        }

        public void Dispose()
        {
            checkListRoladoraRangoDAL.Disposed += (s, e) =>
            {
                checkListRoladoraRangoDAL = null;
            };
            checkListRoladoraRangoDAL.Dispose();
        }
        
        /// <summary>
        /// Obtiene una lista paginada de CheckListRoladoraRango
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CheckListRoladoraRangoInfo> ObtenerPorPagina(PaginacionInfo pagina, CheckListRoladoraRangoInfo filtro)
        {
            try
            {
                Logger.Info();
                return checkListRoladoraRangoDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una lista de CheckListRoladoraRango
        /// </summary>
        /// <returns></returns>
        public IList<CheckListRoladoraRangoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return checkListRoladoraRangoDAL.ObtenerTodos().ToList();
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
        /// Obtiene una lista de CheckListRoladoraRango filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IList<CheckListRoladoraRangoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return checkListRoladoraRangoDAL.ObtenerTodos().Where(e=> e.Activo == estatus).ToList();
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
        /// Obtiene una entidad de CheckListRoladoraRango por su Id
        /// </summary>
        /// <param name="checkListRoladoraRangoId">Obtiene una entidad CheckListRoladoraRango por su Id</param>
        /// <returns></returns>
        public CheckListRoladoraRangoInfo ObtenerPorID(int checkListRoladoraRangoId)
        {
            try
            {
                Logger.Info();
                return checkListRoladoraRangoDAL.ObtenerTodos().Where(e=> e.CheckListRoladoraRangoID == checkListRoladoraRangoId).FirstOrDefault();
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
        /// Obtiene una entidad de CheckListRoladoraRango por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad CheckListRoladoraRango por su descripcion</param>
        /// <returns></returns>
        public CheckListRoladoraRangoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                return checkListRoladoraRangoDAL.ObtenerTodos().Where(e=> e.Descripcion.ToLower() == descripcion.ToLower()).FirstOrDefault();
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
        /// Metodo para Guardar/Modificar una entidad CheckListRoladoraRango
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(CheckListRoladoraRangoInfo info)
        {
            try
            {
                Logger.Info();
                return checkListRoladoraRangoDAL.Guardar(info);
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

        public dynamic ObtenerClaseRango(int preguntaId, int rangoId, string parametros)
        {
            var xDocument = XDocument.Parse(parametros);
            var semaforoRangos = xDocument.Descendants("semaforo")
                .Select(element => new
                                       {
                                           CodigoColor = element.Element("CodigoColor").Value,
                                           DescripcionAccion = element.Element("DescripcionAccion").Value,
                                           TipoPreguntaID = Convert.ToInt32(element.Element("TipoPreguntaID").Value),
                                           TipoPregunta = element.Element("TipoPregunta").Value,
                                           PreguntaID = Convert.ToInt32(element.Element("PreguntaID").Value),
                                           Pregunta = element.Element("Pregunta").Value,
                                           CheckListRoladoraRangoID =
                                       Convert.ToInt32(element.Element("CheckListRoladoraRangoID").Value),
                                           DescripcionRango = element.Element("DescripcionRango").Value,
                                           CheckListRoladoraAccionID =
                                       Convert.ToInt32(element.Element("CheckListRoladoraAccionID").Value),
                                           Indice = Convert.ToInt32(element.Element("Indice").Value)
                                       }).FirstOrDefault(
                                           cond =>
                                           cond.PreguntaID == preguntaId && cond.CheckListRoladoraRangoID == rangoId);
            var clase = new StringBuilder();
            int habilitar = 0;
            if (semaforoRangos != null)
            {
                clase.Append(
                    semaforoRangos.TipoPregunta.Replace("(", string.Empty).Replace(")", string.Empty).Replace(" ",
                                                                                                              string.
                                                                                                                  Empty));
                clase.Append(semaforoRangos.Indice);

                habilitar = semaforoRangos.CheckListRoladoraAccionID;
            }
            return new { Clase = clase.ToString(), Habilitar = habilitar };
        }
    }
}
