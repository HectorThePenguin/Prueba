using System;
using System.Collections.Generic;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System.Reflection;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class TipoProrrateoPL
    {
        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<TipoProrrateoInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoProrrateoInfo filtro)
        {
            ResultadoInfo<TipoProrrateoInfo> costoLista;
            try
            {
                Logger.Info();
                var tipoProrrateoBL = new TipoProrrateoBL();
                costoLista = tipoProrrateoBL.ObtenerPorPagina(pagina, filtro);
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
        public void Actualizar(TipoProrrateoInfo info)
        {
            try
            {
                Logger.Info();
                var tipoProrrateoBL = new TipoProrrateoBL();
                tipoProrrateoBL.Actualizar(info);
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
        public TipoProrrateoInfo ObtenerPorID(int infoId)
        {
            TipoProrrateoInfo info;
            try
            {
                Logger.Info();
                var tipoProrrateoBL = new TipoProrrateoBL();
                info = tipoProrrateoBL.ObtenerPorID(infoId);
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
        public void Crear(TipoProrrateoInfo info)
        {
            try
            {
                Logger.Info();
                var tipoProrrateoBL = new TipoProrrateoBL();
                tipoProrrateoBL.Crear(info);
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
        ///     Obtiene una lista de TipoPoliza filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<TipoProrrateoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var tipoProrrateoBL = new TipoProrrateoBL();
                IList<TipoProrrateoInfo> result = tipoProrrateoBL.ObtenerTodos(estatus);

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
