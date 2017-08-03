using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Servicios.BL;
using SIE.Base.Exepciones;

namespace SIE.Services.Servicios.PL
{
    public class TipoCostoPL
    {
         /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<TipoCostoInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoCostoInfo filtro)
         {
             ResultadoInfo<TipoCostoInfo> result;
             try
             {
                 Logger.Info();
                 var tipoCostoBL = new TipoCostoBL();
                 result = tipoCostoBL.ObtenerPorPagina(pagina, filtro);
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
        ///     Metodo que actualiza un Tipo Costo
        /// </summary>
        /// <param name="info"></param>
        public void Actualizar(TipoCostoInfo info)
        {
            try
            {
                Logger.Info();
               var tipoCostoBL = new TipoCostoBL();
                tipoCostoBL.Actualizar(info);
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
        ///     Obtiene un TipoCostoInfo por Id
        /// </summary>
        /// <param name="infoId"></param>
        /// <returns></returns>
        public TipoCostoInfo ObtenerPorID(int infoId)
        {
            TipoCostoInfo info;
            try
            {
                Logger.Info();
                var tipoCostoBL = new TipoCostoBL();
                info = tipoCostoBL.ObtenerPorID(infoId);
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
        ///     Metodo que crear un Tipo Costo
        /// </summary>
        /// <param name="info"></param>
        public void Guardar(TipoCostoInfo info)
        {
            try
            {
                Logger.Info();
                var tipoCostoBL = new TipoCostoBL();
                tipoCostoBL.Guardar(info);
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
        ///     Obtiene una lista de Retencion filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<TipoCostoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoCostoBL = new TipoCostoBL();
                IList<TipoCostoInfo> result = tipoCostoBL.ObtenerTodos(estatus);

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
        public TipoCostoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var tipoCostoBL = new TipoCostoBL();
                TipoCostoInfo result = tipoCostoBL.ObtenerPorDescripcion(descripcion);
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
