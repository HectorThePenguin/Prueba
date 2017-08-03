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
    public class PaisBL
    {

        /// <summary>
        ///     Obtiene un lista paginada de paises
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<PaisInfo> ObtenerPorPagina(PaginacionInfo pagina, PaisInfo filtro)
        {
            ResultadoInfo<PaisInfo> result;
            try
            {
                Logger.Info();
                var paisDAL = new PaisDAL();
                result = paisDAL.ObtenerPorPagina(pagina, filtro);
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
        ///     Obtiene un país por Id
        /// </summary>
        /// <param name="paisID"></param>
        /// <returns></returns>
        public PaisInfo ObtenerPorID(int paisID)
        {
            PaisInfo paisInfo;
            try
            {
                Logger.Info();
                var paisDAL = new PaisDAL();
                paisInfo = paisDAL.ObtenerPorID(paisID);
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
            return paisInfo;
        }

        /// <summary>
        ///     Obtiene un pais por Id
        /// </summary>
        /// <param name="paisInfo"></param>
        /// <returns></returns>
        public PaisInfo ObtenerPorID(PaisInfo paisInfo)
        {
            PaisInfo pasInfo;
            try
            {
                Logger.Info();
                var paisDAL = new PaisDAL();
                pasInfo = paisDAL.ObtenerPorID(paisInfo);
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
            return pasInfo;
        }

        /// <summary>
        /// Obtiene una entidad Pais por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad Pais por su Id</param>
        /// <returns></returns>
        public PaisInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var paisDAL = new PaisDAL();
                PaisInfo result = paisDAL.ObtenerPorDescripcion(descripcion);
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
