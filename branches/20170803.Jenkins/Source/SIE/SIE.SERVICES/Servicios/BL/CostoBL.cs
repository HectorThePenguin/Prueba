using System;
using System.Linq;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System.Reflection;
using System.Collections.Generic;

namespace SIE.Services.Servicios.BL
{
    internal class CostoBL
    {
        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<CostoInfo> ObtenerPorPagina(PaginacionInfo pagina, CostoInfo filtro)
        {
            ResultadoInfo<CostoInfo> costoLista;
            try
            {
                Logger.Info();
                var costoDAL = new CostoDAL();
                costoLista = costoDAL.ObtenerPorPagina(pagina, filtro);
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
        internal ResultadoInfo<CostoInfo> ObtenerPorPaginaSinId(PaginacionInfo pagina, CostoInfo filtro)
        {
            ResultadoInfo<CostoInfo> costoLista;
            try
            {
                Logger.Info();
                var costoDAL = new CostoDAL();
                costoLista = costoDAL.ObtenerPorPaginaSinId(pagina, filtro);
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
        internal void Actualizar(CostoInfo info)
        {
            try
            {
                Logger.Info();
                var costoDAL = new CostoDAL();
                costoDAL.Actualizar(info);
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
        internal CostoInfo ObtenerPorID(int infoId)
        {
            CostoInfo info;
            try
            {
                Logger.Info();
                var costoDAL = new CostoDAL();
                info = costoDAL.ObtenerPorID(infoId);
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
        internal void Crear(CostoInfo info)
        {
            try
            {
                Logger.Info();
                var costoDAL = new CostoDAL();
                if (info.CostoID == 0)
                {
                    costoDAL.Crear(info);
                }
                else
                {
                    costoDAL.Actualizar(info);
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

        /// <summary>
        ///     Obtiene una lista de costo filtrando por su estatus
        /// </summary>
        /// <param name="estatus"> </param>
        /// <returns></returns>
        internal IList<CostoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var costoDAL = new CostoDAL();
                IList<CostoInfo> lista = costoDAL.ObtenerTodos(estatus);

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

        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<CostoInfo> ObtenerPorPaginaTipoCosto(PaginacionInfo pagina, CostoInfo filtro)
        {
            ResultadoInfo<CostoInfo> costoLista;
            try
            {
                Logger.Info();
                var costoDAL = new CostoDAL();
                costoLista = costoDAL.ObtenerPorPaginaTipoCosto(pagina, filtro);
                if (costoLista == null)
                {
                    return null;
                }
                foreach (var elemento in costoLista.Lista)
                {
                    elemento.ListaTipoCostos = filtro.ListaTipoCostos;
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
            return costoLista;
        }

        /// <summary>
        ///     Obtiene un TipoCostoInfo por Id
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal CostoInfo ObtenerPorClaveContableTipoCosto(CostoInfo filtro)
        {
            CostoInfo info;
            try
            {
                Logger.Info();
                var costoDAL = new CostoDAL();
                info = costoDAL.ObtenerPorClaveContableTipoCosto(filtro);
                if (info == null)
                {
                    return null;
                }
                info.ListaTipoCostos = filtro.ListaTipoCostos;
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
        internal ResultadoInfo<CostoInfo> ObtenerPorPaginaClaveContable(PaginacionInfo pagina, CostoInfo filtro)
        {
            ResultadoInfo<CostoInfo> costoLista;
            try
            {
                Logger.Info();
                var costoDAL = new CostoDAL();
                costoLista = costoDAL.ObtenerPorPaginaClaveContable(pagina, filtro);
              
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
        ///     Obtiene un TipoCostoInfo por Id
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal CostoInfo ObtenerPorClaveContable(CostoInfo filtro)
        {
            CostoInfo info;
            try
            {
                Logger.Info();
                var costoDAL = new CostoDAL();
                info = costoDAL.ObtenerPorClaveContable(filtro);
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
        /// Obtiene una entidad Costo por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal CostoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var costoDAL = new CostoDAL();
                CostoInfo result = costoDAL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene por id
        /// </summary>
        /// <param name="infoId"></param>
        /// <returns></returns>
        public CostoInfo ObtenerPorIDFiltroTipoCosto(int infoId)
        {
            CostoInfo info;
            try
            {
                Logger.Info();
                var costoDAL = new CostoDAL();
                info = costoDAL.ObtenerPorID(infoId);
                if (info != null)
                {
                    if (info.TipoCosto.TipoCostoID != (int)Costo.Fletes)
                    {
                        info = null;
                    }
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
            return info;
        }

        public ResultadoInfo<CostoInfo> ObtenerPorPaginaIDTipoCosto(PaginacionInfo pagina, CostoInfo filtro)
        {
            ResultadoInfo<CostoInfo> costoLista;
            try
            {
                Logger.Info();
                var costoDAL = new CostoDAL();
                costoLista = costoDAL.ObtenerPorPaginaIDTipoCosto(pagina, filtro);
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
        /// Obtiene el costo por el flete
        /// </summary>
        /// <param name="idFlete"></param>
        /// <returns></returns>
        public List<FleteDetalleInfo> ObtenerPorFleteID(int idFlete)
        {
            List<FleteDetalleInfo> info;
            try
            {
                Logger.Info();
                var costoDAL = new CostoDAL();
                info = costoDAL.ObtenerPorFleteID(idFlete);

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
        /// Obtiene el costo por id
        /// </summary>
        /// <param name="costo"></param>
        /// <returns></returns>
        internal CostoInfo ObtenerCostoPorID(CostoInfo costo)
        {
            CostoInfo info;
            try
            {
                Logger.Info();
                var costoDAL = new CostoDAL();
                info = costoDAL.ObtenerCostoPorID(costo);
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
        /// Obtiene la lista de costos filtrada por una lista de tipos de gasto
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
                var costoDAL = new CostoDAL();
                var lista = costoDAL.ObtenerTodos(EstatusEnum.Activo);

                costoLista = new ResultadoInfo<CostoInfo>();
                lista = lista.Where(id => id.CostoID != 1).ToList();
                var filteredItems = (from costo in lista from tipo in filtro.ListaTipoCostos 
                                     where costo.TipoCosto.TipoCostoID == tipo.TipoCostoID select costo).ToList();

                filteredItems = filteredItems.Where(
                    p => p.Descripcion.ToUpper().Contains(filtro.Descripcion)).ToList();

                costoLista.Lista = filteredItems;
                costoLista.TotalRegistros = filteredItems.Count;
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
        /// Obtiene el centro de costo para un costoid dado
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal CentroCostoInfo ObtenerCentroCostoSAPPorCosto(CostoInfo filtro)
        {
            CentroCostoInfo info;
            try
            {
                Logger.Info();
                var costoDAL = new CostoDAL();
                info = costoDAL.ObtenerCentroCostoSAPPorCosto(filtro);
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
    }
}
