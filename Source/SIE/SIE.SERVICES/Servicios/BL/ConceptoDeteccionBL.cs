using System;
using System.Collections.Generic;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    internal class ConceptoDeteccionBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad ConceptoDeteccion
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(ConceptoDeteccionInfo info)
        {
            try
            {
                Logger.Info();
                var conceptoDeteccionDAL = new ConceptoDeteccionDAL();
                int result = info.ConceptoDeteccionID;
                if (info.ConceptoDeteccionID == 0)
                {
                    result = conceptoDeteccionDAL.Crear(info);
                }
                else
                {
                    conceptoDeteccionDAL.Actualizar(info);
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
        internal ResultadoInfo<ConceptoDeteccionInfo> ObtenerPorPagina(PaginacionInfo pagina, ConceptoDeteccionInfo filtro)
        {
            try
            {
                Logger.Info();
                var conceptoDeteccionDAL = new ConceptoDeteccionDAL();
                ResultadoInfo<ConceptoDeteccionInfo> result = conceptoDeteccionDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de ConceptoDeteccion
        /// </summary>
        /// <returns></returns>
        internal IList<ConceptoDeteccionInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var conceptoDeteccionDAL = new ConceptoDeteccionDAL();
                IList<ConceptoDeteccionInfo> result = conceptoDeteccionDAL.ObtenerTodos();
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
        internal IList<ConceptoDeteccionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var conceptoDeteccionDAL = new ConceptoDeteccionDAL();
                IList<ConceptoDeteccionInfo> result = conceptoDeteccionDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad ConceptoDeteccion por su Id
        /// </summary>
        /// <param name="conceptoDeteccionID">Obtiene una entidad ConceptoDeteccion por su Id</param>
        /// <returns></returns>
        internal ConceptoDeteccionInfo ObtenerPorID(int conceptoDeteccionID)
        {
            try
            {
                Logger.Info();
                var conceptoDeteccionDAL = new ConceptoDeteccionDAL();
                ConceptoDeteccionInfo result = conceptoDeteccionDAL.ObtenerPorID(conceptoDeteccionID);
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
        /// Obtiene una entidad ConceptoDeteccion por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal ConceptoDeteccionInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var conceptoDeteccionDAL = new ConceptoDeteccionDAL();
                ConceptoDeteccionInfo result = conceptoDeteccionDAL.ObtenerPorDescripcion(descripcion);
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

