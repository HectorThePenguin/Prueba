using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Base.Infos;
using SIE.Base.Interfaces;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System.Reflection;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class CostoPL : IPaginador<CostoInfo>
    {
        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CostoInfo> ObtenerPorPagina(PaginacionInfo pagina, CostoInfo filtro)
        {
            ResultadoInfo<CostoInfo> costoLista;
            try
            {
                Logger.Info();
                var costoBL = new CostoBL();
                costoLista = costoBL.ObtenerPorPagina(pagina, filtro);
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CostoInfo> ObtenerPorPaginaSinId(PaginacionInfo pagina, CostoInfo filtro)
        {
            ResultadoInfo<CostoInfo> costoLista;
            try
            {
                Logger.Info();
                var costoBL = new CostoBL();
                costoLista = costoBL.ObtenerPorPaginaSinId(pagina, filtro);
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
        public void Actualizar(CostoInfo info)
        {
            try
            {
                Logger.Info();
                var costoBL = new CostoBL();
                costoBL.Actualizar(info);
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
        public CostoInfo ObtenerPorID(int infoId)
        {
            CostoInfo info;
            try
            {
                Logger.Info();
                var costoBL = new CostoBL();
                info = costoBL.ObtenerPorID(infoId);
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
        ///     Obtiene un CostoInfo por Id
        /// </summary>
        /// <param name="costoInfo"></param>
        /// <returns></returns>
        public CostoInfo ObtenerPorID(CostoInfo costoInfo)
        {
            CostoInfo info;
            try
            {
                Logger.Info();
                var costoBL = new CostoBL();
                info = costoBL.ObtenerPorID(costoInfo.CostoID);
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
        public void Crear(CostoInfo info)
        {
            try
            {
                Logger.Info();
                var costoBL = new CostoBL();
                costoBL.Crear(info);
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
        ///  Obtiene una lista de Costo filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns> </returns>
        public IList<CostoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var costoBL = new CostoBL();
                IList<CostoInfo> lista = costoBL.ObtenerTodos(estatus);

                return lista;
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

        public ResultadoInfo<CostoInfo> ObtenerPorId(int id)
        {
            var resultado = new ResultadoInfo<CostoInfo>();
            var costos = new List<CostoInfo>();
            var costo = ObtenerPorID(id);
            costos.Add(costo);
            resultado.Lista = costos;

            return resultado;
        }

        public ResultadoInfo<CostoInfo> ObtenerPorDescripcion(PaginacionInfo pagina, string descripcion)
        {
            var costo = new CostoInfo { Descripcion = descripcion, Activo = EstatusEnum.Activo };
            ResultadoInfo<CostoInfo> resultado = ObtenerPorPagina(pagina, costo);

            return resultado;
        }

        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CostoInfo> ObtenerPorPaginaTipoCosto(PaginacionInfo pagina, CostoInfo filtro)
        {
            ResultadoInfo<CostoInfo> costoLista;
            try
            {
                Logger.Info();
                var costoBL = new CostoBL();
                costoLista = costoBL.ObtenerPorPaginaTipoCosto(pagina, filtro);
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

        public ResultadoInfo<CostoInfo> ObtenerPorDescripcionTipoCosto(PaginacionInfo pagina, string descripcion)
        {
            ResultadoInfo<CostoInfo> resultado;
            try
            {
                var costo = new CostoInfo { Descripcion = descripcion, Activo = EstatusEnum.Activo };
                resultado = ObtenerPorPaginaTipoCosto(pagina, costo);
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
            return resultado;
        }

        /// <summary>
        ///     Obtiene un CostoInfo por Id
        /// </summary>
        /// <param name="costoInfo"></param>
        /// <returns></returns>
        public CostoInfo ObtenerPorClaveContableTipoCosto(CostoInfo costoInfo)
        {
            CostoInfo info;
            try
            {
                Logger.Info();
                var costoBL = new CostoBL();
                info = costoBL.ObtenerPorClaveContableTipoCosto(costoInfo);
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
        ///     Obtiene un CostoInfo por Id
        /// </summary>
        /// <param name="costoInfo"></param>
        /// <returns></returns>
        public CostoInfo ObtenerPorClaveContable(CostoInfo costoInfo)
        {
            CostoInfo info;
            try
            {
                Logger.Info();
                var costoBL = new CostoBL();
                info = costoBL.ObtenerPorClaveContable(costoInfo);
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CostoInfo> ObtenerPorPaginaClaveContable(PaginacionInfo pagina, CostoInfo filtro)
        {
            ResultadoInfo<CostoInfo> costoLista;
            try
            {
                Logger.Info();
                var costoBL = new CostoBL();
                costoLista = costoBL.ObtenerPorPaginaClaveContable(pagina, filtro);
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
        /// Obtiene una entidad por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public CostoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var costoBL = new CostoBL();
                CostoInfo result = costoBL.ObtenerPorDescripcion(descripcion);
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

        public CostoInfo ObtenerPorIDFiltroTipoCosto(int infoId)
        {
            CostoInfo info;
            try
            {
                Logger.Info();
                var costoBL = new CostoBL();
                info = costoBL.ObtenerPorIDFiltroTipoCosto(infoId);
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
        ///     Obtiene un CostoInfo por Id
        /// </summary>
        /// <param name="costoInfo"></param>
        /// <returns></returns>
        public CostoInfo ObtenerPorIDFiltroTipoCosto(CostoInfo costoInfo)
        {
            CostoInfo info;
            try
            {
                Logger.Info();
                var costoBL = new CostoBL();
                info = costoBL.ObtenerPorIDFiltroTipoCosto(costoInfo.CostoID);
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

        public ResultadoInfo<CostoInfo> ObtenerPorIDPaginaTipoCosto(PaginacionInfo pagina, CostoInfo filtro)
        {
            ResultadoInfo<CostoInfo> costoLista;
            try
            {
                Logger.Info();
                var costoBL = new CostoBL();
                costoLista = costoBL.ObtenerPorPaginaIDTipoCosto(pagina, filtro);
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

        public List<FleteDetalleInfo> ObtenerPorFleteID(int idFlete)
        {
            List<FleteDetalleInfo> info;
            try
            {
                Logger.Info();
                var costoBL = new CostoBL();
                info = costoBL.ObtenerPorFleteID(idFlete);
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
        /// Obtiene un costo por id
        /// </summary>
        /// <param name="costoInfo"></param>
        /// <returns></returns>
        public CostoInfo ObtenerCostoPorID(CostoInfo costoInfo)
        {
            CostoInfo info;
            try
            {
                Logger.Info();
                var costoBL = new CostoBL();
                info = costoBL.ObtenerCostoPorID(costoInfo);
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
        /// Obtiene la lista de costos filtrada por uno o mas tipos de gasto
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CostoInfo> ObtenerPorPaginaCostoPorTiposGasto(PaginacionInfo pagina, CostoInfo filtro)
        {
            ResultadoInfo<CostoInfo> costoLista;
            try
            {
                Logger.Info();
                var costoBL = new CostoBL();
                costoLista = costoBL.ObtenerPorPaginaCostoPorTiposGasto(pagina, filtro);
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
        /// Obtiene los costos para el tipo de costo gasto
        /// </summary>
        /// <returns></returns>
        public IList<CostoInfo> ObtenerPorTipoGasto()
        {
            IList<CostoInfo> info;
            try
            {
                Logger.Info();
                var costoBL = new CostoBL();
                info = costoBL.ObtenerTodos(EstatusEnum.Activo);

                info = info.Where(p => p.TipoCosto.TipoCostoID == TipoCostoEnum.Gasto.GetHashCode()).ToList();

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
        /// Obtiene el centro de costo sap asociado a un tipo de costo
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public CentroCostoInfo ObtenerCentroCostoSAPPorCosto(CostoInfo filtro)
        {
            CentroCostoInfo retValue = null;
            try
            {
                Logger.Info();
                var costoBL = new CostoBL();
                retValue = costoBL.ObtenerCentroCostoSAPPorCosto(filtro);

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

            return retValue;
        }
    }
}
