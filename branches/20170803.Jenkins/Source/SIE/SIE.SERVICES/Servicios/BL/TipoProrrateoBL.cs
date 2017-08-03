using System;
using System.Collections.Generic;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    internal class TipoProrrateoBL
    {
        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<TipoProrrateoInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoProrrateoInfo filtro)
        {
            ResultadoInfo<TipoProrrateoInfo> costoLista;
            try
            {
                Logger.Info();
                var tipoProrrateoDAL = new TipoProrrateoDAL();
                costoLista = tipoProrrateoDAL.ObtenerPorPagina(pagina, filtro);
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
            return costoLista;
        }

        /// <summary>
        ///     Metodo que actualiza un Tipo Prorrateo
        /// </summary>
        /// <param name="info"></param>
        internal void Actualizar(TipoProrrateoInfo info)
        {
            try
            {
                Logger.Info();
                var tipoProrrateoDAL = new TipoProrrateoDAL();
                tipoProrrateoDAL.Actualizar(info);
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
        internal TipoProrrateoInfo ObtenerPorID(int infoId)
        {
            TipoProrrateoInfo info;
            try
            {
                Logger.Info();
                var tipoProrrateoDAL = new TipoProrrateoDAL();
                info = tipoProrrateoDAL.ObtenerPorID(infoId);
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
        internal void Crear(TipoProrrateoInfo info)
        {
            try
            {
                Logger.Info();
                var tipoProrrateoDAL = new TipoProrrateoDAL();
                tipoProrrateoDAL.Crear(info);
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
        ///     Obtiene una lista de TipoProrrateo filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal IList<TipoProrrateoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoProrrateoDAL = new TipoProrrateoDAL();
                IList<TipoProrrateoInfo> result = tipoProrrateoDAL.ObtenerTodos(estatus);

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
