using System;
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
    internal class CostoOrganizacionBL
    {
        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<CostoOrganizacionInfo> ObtenerPorPagina(PaginacionInfo pagina, CostoOrganizacionInfo filtro)
        {
            ResultadoInfo<CostoOrganizacionInfo> costoOrganizacionLista;
            try
            {
                Logger.Info();
                var costoOrganizacionDAL = new CostoOrganizacionDAL();
                costoOrganizacionLista = costoOrganizacionDAL.ObtenerPorPagina(pagina, filtro);
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
        internal void Actualizar(CostoOrganizacionInfo info)
        {
            try
            {
                Logger.Info();
                var costoOrganizacionDAL = new CostoOrganizacionDAL();
                costoOrganizacionDAL.Actualizar(info);
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
        internal CostoOrganizacionInfo ObtenerPorID(int infoId)
        {
            CostoOrganizacionInfo info;
            try
            {
                Logger.Info();
                var costoOrganizacionDAL = new CostoOrganizacionDAL();
                info = costoOrganizacionDAL.ObtenerPorID(infoId);
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
        internal void Crear(CostoOrganizacionInfo info)
        {
            try
            {
                Logger.Info();
                var costoOrganizacionDAL = new CostoOrganizacionDAL();
                if (info.CostoOrganizacionID == 0)
                {
                    costoOrganizacionDAL.Crear(info);
                }
                else
                {
                    costoOrganizacionDAL.Actualizar(info);
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
        ///     Obtiene una lista de CostoOrganizacion filtrando por su estatus
        /// </summary>
        /// <param name="estatus"> </param>
        /// <returns></returns>
        internal IList<CostoOrganizacionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var costoOrganizacionDAL = new CostoOrganizacionDAL();
                IList<CostoOrganizacionInfo> lista = costoOrganizacionDAL.ObtenerTodos(estatus);

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
        internal List<EntradaGanadoCostoInfo> ObtenerCostosAutomaticos(EntradaGanadoInfo entradaGanado)
        {
            List<EntradaGanadoCostoInfo> listaEntradaGanadoCostos = new List<EntradaGanadoCostoInfo>();
            try
            {
                Logger.Info();
                var costoOrganizacionDAL = new CostoOrganizacionDAL();
                
                var listaCostos = costoOrganizacionDAL.ObtenerPorOrganizacion(entradaGanado);

                if(listaCostos == null)
                {
                    return listaEntradaGanadoCostos;
                }

                listaCostos.ForEach(costo =>
                    {
                        var entradaGanadoCosto = new EntradaGanadoCostoInfo
                            {
                                Costo = costo.Costo,
                                Importe = costo.Importe
                            };
                        listaEntradaGanadoCostos.Add(entradaGanadoCosto);
                    });
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
            return listaEntradaGanadoCostos;
        }

        /// <summary>
        /// Obtiene un Costo Organizacion por tipo de organizacion y costo
        /// </summary>
        /// <param name="costoOrganizacion"></param>
        /// <returns></returns>
        internal CostoOrganizacionInfo ObtenerPorTipoOrganizacionCosto(CostoOrganizacionInfo costoOrganizacion)
        {
            CostoOrganizacionInfo costoOrganizacionInfo;
            try
            {
                Logger.Info();
                var costoOrganizacionDAL = new CostoOrganizacionDAL();
                costoOrganizacionInfo = costoOrganizacionDAL.ObtenerPorTipoOrganizacionCosto(costoOrganizacion);
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
            return costoOrganizacionInfo;
        }
    }
}
