using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class FleteMermaPermitidaBL
    {
        /// <summary>
        /// Obtiene configuracion de flete merma permitida
        /// </summary>
        /// <param name="fleteMermaPermitidaInfo"></param>
        /// <returns></returns>
        public FleteMermaPermitidaInfo ObtenerConfiguracion(FleteMermaPermitidaInfo fleteMermaPermitidaInfo)
        {
            FleteMermaPermitidaInfo info;
            try
            {
                Logger.Info();
                var fleteMermaPermitidaDal = new FleteMermaPermitidaDAL();
                info = fleteMermaPermitidaDal.ObtenerConfiguracion(fleteMermaPermitidaInfo);
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
            return info;
        }
		
		/// <summary>
        /// Metodo para Guardar/Modificar una entidad FleteMermaPermitida
        /// </summary>
        /// <param name="info"></param>
        public int Guardar(FleteMermaPermitidaInfo info)
        {
            try
            {
                Logger.Info();
                var fleteMermaPermitidaDAL = new FleteMermaPermitidaDAL();
                int result = info.FleteMermaPermitidaID;
                if (info.FleteMermaPermitidaID == 0)
                {
                    result = fleteMermaPermitidaDAL.Crear(info);
                }
                else
                {
                    fleteMermaPermitidaDAL.Actualizar(info);
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
        public ResultadoInfo<FleteMermaPermitidaInfo> ObtenerPorPagina(PaginacionInfo pagina, FleteMermaPermitidaInfo filtro)
        {
            try
            {
                Logger.Info();
                var fleteMermaPermitidaDAL = new FleteMermaPermitidaDAL();
                ResultadoInfo<FleteMermaPermitidaInfo> result = fleteMermaPermitidaDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de FleteMermaPermitida
        /// </summary>
        /// <returns></returns>
        public IList<FleteMermaPermitidaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var fleteMermaPermitidaDAL = new FleteMermaPermitidaDAL();
                IList<FleteMermaPermitidaInfo> result = fleteMermaPermitidaDAL.ObtenerTodos();
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
        public IList<FleteMermaPermitidaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var fleteMermaPermitidaDAL = new FleteMermaPermitidaDAL();
                IList<FleteMermaPermitidaInfo> result = fleteMermaPermitidaDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad FleteMermaPermitida por su Id
        /// </summary>
        /// <param name="fleteMermaPermitidaID">Obtiene una entidad FleteMermaPermitida por su Id</param>
        /// <returns></returns>
        public FleteMermaPermitidaInfo ObtenerPorID(int fleteMermaPermitidaID)
        {
            try
            {
                Logger.Info();
                var fleteMermaPermitidaDAL = new FleteMermaPermitidaDAL();
                FleteMermaPermitidaInfo result = fleteMermaPermitidaDAL.ObtenerPorID(fleteMermaPermitidaID);
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
        /// Obtiene una entidad FleteMermaPermitida por su descripción
        /// </summary>
        /// <param name="fleteMermaPermitida"></param>
        /// <returns></returns>
        public FleteMermaPermitidaInfo ObtenerPorDescripcion(FleteMermaPermitidaInfo fleteMermaPermitida)
        {
            try
            {
                Logger.Info();
                var fleteMermaPermitidaDAL = new FleteMermaPermitidaDAL();
                FleteMermaPermitidaInfo result = fleteMermaPermitidaDAL.ObtenerPorDescripcion(fleteMermaPermitida);
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
