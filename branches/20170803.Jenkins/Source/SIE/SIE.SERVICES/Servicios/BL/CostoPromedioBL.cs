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
    internal class CostoPromedioBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad CostoPromedio
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(CostoPromedioInfo info)
        {
            try
            {
                Logger.Info();
                var costoPromedioDAL = new CostoPromedioDAL();
                int result = info.CostoPromedioID;
                if (info.CostoPromedioID == 0)
                {
                    result = costoPromedioDAL.Crear(info);
                }
                else
                {
                    costoPromedioDAL.Actualizar(info);
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
        internal ResultadoInfo<CostoPromedioInfo> ObtenerPorPagina(PaginacionInfo pagina, CostoPromedioInfo filtro)
        {
            try
            {
                Logger.Info();
                var costoPromedioDAL = new CostoPromedioDAL();
                ResultadoInfo<CostoPromedioInfo> result = costoPromedioDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de CostoPromedio
        /// </summary>
        /// <returns></returns>
        internal IList<CostoPromedioInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var costoPromedioDAL = new CostoPromedioDAL();
                IList<CostoPromedioInfo> result = costoPromedioDAL.ObtenerTodos();
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
        internal IList<CostoPromedioInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var costoPromedioDAL = new CostoPromedioDAL();
                IList<CostoPromedioInfo> result = costoPromedioDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad CostoPromedio por su Id
        /// </summary>
        /// <param name="costoPromedioID">Obtiene una entidad CostoPromedio por su Id</param>
        /// <returns></returns>
        internal CostoPromedioInfo ObtenerPorID(int costoPromedioID)
        {
            try
            {
                Logger.Info();
                var costoPromedioDAL = new CostoPromedioDAL();
                CostoPromedioInfo result = costoPromedioDAL.ObtenerPorID(costoPromedioID);
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
        internal CostoPromedioInfo ObtenerPorOrganizacionCosto(int organizacionId, int costoId)
        {
            try
            {
                Logger.Info();
                var costoPromedioDAL = new CostoPromedioDAL();
                CostoPromedioInfo result = costoPromedioDAL.ObtenerPorOrganizacionCosto(organizacionId, costoId);
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

