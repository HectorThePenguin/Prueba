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
    public class CostoPromedioPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad CostoPromedio
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(CostoPromedioInfo info)
        {
            try
            {
                Logger.Info();
                var costoPromedioBL = new CostoPromedioBL();
                int result = costoPromedioBL.Guardar(info);
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
        public ResultadoInfo<CostoPromedioInfo> ObtenerPorPagina(PaginacionInfo pagina, CostoPromedioInfo filtro)
        {
            try
            {
                Logger.Info();
                var costoPromedioBL = new CostoPromedioBL();
                ResultadoInfo<CostoPromedioInfo> result = costoPromedioBL.ObtenerPorPagina(pagina, filtro);
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
        public IList<CostoPromedioInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var costoPromedioBL = new CostoPromedioBL();
                IList<CostoPromedioInfo> result = costoPromedioBL.ObtenerTodos();
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
        public IList<CostoPromedioInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var costoPromedioBL = new CostoPromedioBL();
                IList<CostoPromedioInfo> result = costoPromedioBL.ObtenerTodos(estatus);
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
        /// <param name="costoPromedioID"></param>
        /// <returns></returns>
        public CostoPromedioInfo ObtenerPorID(int costoPromedioID)
        {
            try
            {
                Logger.Info();
                var costoPromedioBL = new CostoPromedioBL();
                CostoPromedioInfo result = costoPromedioBL.ObtenerPorID(costoPromedioID);
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
        /// Obtiene una entidad por su organización y costo
        /// </summary>
        /// <param name="organizacionId">Organización </param>
        /// <param name="costoId">Costo </param>
        /// <returns></returns>
        public CostoPromedioInfo ObtenerPorOrganizacionCosto(int organizacionId, int costoId)
        {
            try
            {
                Logger.Info();
                var costoPromedioBL = new CostoPromedioBL();
                CostoPromedioInfo result = costoPromedioBL.ObtenerPorOrganizacionCosto(organizacionId, costoId);
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

