using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ConceptoDeteccionPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad ConceptoDeteccion
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(ConceptoDeteccionInfo info)
        {
            try
            {
                Logger.Info();
                var conceptoDeteccionBL = new ConceptoDeteccionBL();
                int result = conceptoDeteccionBL.Guardar(info);
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
        public ResultadoInfo<ConceptoDeteccionInfo> ObtenerPorPagina(PaginacionInfo pagina, ConceptoDeteccionInfo filtro)
        {
            try
            {
                Logger.Info();
                var conceptoDeteccionBL = new ConceptoDeteccionBL();
                ResultadoInfo<ConceptoDeteccionInfo> result = conceptoDeteccionBL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una lista
        /// </summary>
        /// <returns></returns>
        public IList<ConceptoDeteccionInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var conceptoDeteccionBL = new ConceptoDeteccionBL();
                IList<ConceptoDeteccionInfo> result = conceptoDeteccionBL.ObtenerTodos();
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
        ///  Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<ConceptoDeteccionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var conceptoDeteccionBL = new ConceptoDeteccionBL();
                IList<ConceptoDeteccionInfo> result = conceptoDeteccionBL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad por su Id
        /// </summary>
        /// <param name="conceptoDeteccionID"></param>
        /// <returns></returns>
        public ConceptoDeteccionInfo ObtenerPorID(int conceptoDeteccionID)
        {
            try
            {
                Logger.Info();
                var conceptoDeteccionBL = new ConceptoDeteccionBL();
                ConceptoDeteccionInfo result = conceptoDeteccionBL.ObtenerPorID(conceptoDeteccionID);
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
        /// Obtiene una entidad por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public ConceptoDeteccionInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var conceptoDeteccionBL = new ConceptoDeteccionBL();
                ConceptoDeteccionInfo result = conceptoDeteccionBL.ObtenerPorDescripcion(descripcion);
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

