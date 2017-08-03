using System;
using System.Collections.Generic;
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
    public class CostoOrganizacionPL : IPaginador<CostoOrganizacionInfo>
    {
        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CostoOrganizacionInfo> ObtenerPorPagina(PaginacionInfo pagina, CostoOrganizacionInfo filtro)
        {
            ResultadoInfo<CostoOrganizacionInfo> costoOrganizacionLista;
            try
            {
                Logger.Info();
                var costoOrganizacionBL = new CostoOrganizacionBL();
                costoOrganizacionLista = costoOrganizacionBL.ObtenerPorPagina(pagina, filtro);
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
            return costoOrganizacionLista;
        }

        /// <summary>
        ///     Metodo que actualiza un Tipo Prorrateo
        /// </summary>
        /// <param name="info"></param>
        public void Actualizar(CostoOrganizacionInfo info)
        {
            try
            {
                Logger.Info();
                var costoOrganizacionBL = new CostoOrganizacionBL();
                costoOrganizacionBL.Actualizar(info);
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
        ///     Obtiene un TipoCostoOrganizacionInfo por Id
        /// </summary>
        /// <param name="infoId"></param>
        /// <returns></returns>
        public CostoOrganizacionInfo ObtenerPorID(int infoId)
        {
            CostoOrganizacionInfo info;
            try
            {
                Logger.Info();
                var costoOrganizacionBL = new CostoOrganizacionBL();
                info = costoOrganizacionBL.ObtenerPorID(infoId);
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
        ///     Obtiene un CostoOrganizacionInfo por Id
        /// </summary>
        /// <param name="CostoOrganizacionInfo"></param>
        /// <returns></returns>
        public CostoOrganizacionInfo ObtenerPorID(CostoOrganizacionInfo CostoOrganizacionInfo)
        {
            CostoOrganizacionInfo info;
            try
            {
                Logger.Info();
                var costoOrganizacionBL = new CostoOrganizacionBL();
                info = costoOrganizacionBL.ObtenerPorID(CostoOrganizacionInfo.CostoOrganizacionID);
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
        ///     Metodo que crear un Tipo CostoOrganizacion
        /// </summary>
        /// <param name="info"></param>
        public void Crear(CostoOrganizacionInfo info)
        {
            try
            {
                Logger.Info();
                var costoOrganizacionBL = new CostoOrganizacionBL();
                costoOrganizacionBL.Crear(info);
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
        ///  Obtiene una lista de CostoOrganizacion filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns> </returns>
        public IList<CostoOrganizacionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var costoOrganizacionBL = new CostoOrganizacionBL();
                IList<CostoOrganizacionInfo> lista = costoOrganizacionBL.ObtenerTodos(estatus);

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
        /// Obtiene la Lista de los Costos Automaticos, para la funcionalidad de Costeo Entrada Ganado
        /// </summary>
        /// <param name="entradaGanado"></param>
        /// <returns></returns>
        public List<EntradaGanadoCostoInfo> ObtenerCostosAutomaticos(EntradaGanadoInfo entradaGanado)
        {
            try
            {
                Logger.Info();
                var costoOrganizacionBL = new CostoOrganizacionBL();
                List<EntradaGanadoCostoInfo> lista = costoOrganizacionBL.ObtenerCostosAutomaticos(entradaGanado);

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
        ///  Obtiene un CostoOrganizacion filtrando por Tipo Organizacion y Costo
        /// </summary>
        /// <returns> </returns>
        public CostoOrganizacionInfo ObtenerPorTipoOrganizacionCosto(CostoOrganizacionInfo costoOrganizacion)
        {
            try
            {
                Logger.Info();
                var costoOrganizacionBL = new CostoOrganizacionBL();
                CostoOrganizacionInfo costoOrganizacionInfo =
                    costoOrganizacionBL.ObtenerPorTipoOrganizacionCosto(costoOrganizacion);
                return costoOrganizacionInfo;
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
