using System;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Interfaces;
using System.Collections.Generic;

namespace SIE.Services.Servicios.BL
{
    public class ZonaBL
    {
        /// <summary>
        ///     Obtiene un lista paginada de zonas
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ZonaInfo> ObtenerPorPagina(PaginacionInfo pagina, ZonaInfo filtro)
        {
            ResultadoInfo<ZonaInfo> result;
            try
            {
                Logger.Info();
                var zonaDAL = new ZonaDAL();
                result = zonaDAL.ObtenerPorPagina(pagina, filtro);
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
            return result;
        }

        /// <summary>
        ///     Obtiene un zona por Id
        /// </summary>
        /// <param name="zonaID"></param>
        /// <returns></returns>
        public ZonaInfo ObtenerPorID(int zonaID)
        {
            ZonaInfo zonaInfo;
            try
            {
                Logger.Info();
                var zonaDAL = new ZonaDAL();
                zonaInfo = zonaDAL.ObtenerPorID(zonaID);
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
            return zonaInfo;
        }

        /// <summary>
        ///     Obtiene un zona por Id
        /// </summary>
        /// <param name="zonaInfo"></param>
        /// <returns></returns>
        public ZonaInfo ObtenerPorID(ZonaInfo zonaInfo)
        {
            ZonaInfo zonInfo;
            try
            {
                Logger.Info();
                var zonaDAL = new ZonaDAL();
                zonInfo = zonaDAL.ObtenerPorID(zonaInfo);
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
            return zonInfo;
        }

        /// <summary>
        /// Obtiene una entidad zona por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad Zona por su Id</param>
        /// <returns></returns>
        public ZonaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var zonaDAL = new ZonaDAL();
                ZonaInfo result = zonaDAL.ObtenerPorDescripcion(descripcion);
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
        ///     Metodo que guarda un zona
        /// </summary>
        /// <param name="info"></param>
        public void Guardar(ZonaInfo info)
        {
            try
            {
                Logger.Info();
                var zonaDAL = new ZonaDAL();
                if (info.ZonaID != 0)
                {
                    zonaDAL.Actualizar(info);
                }
                else
                {
                    zonaDAL.Crear(info);
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
    }
}
