using System;
using System.Collections.Generic;
using SIE.Services.Info.Info;
using SIE.Services.Info.Enums;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System.Reflection;
using System.Linq;

namespace SIE.Services.Servicios.BL
{
    internal class CostoEmbarqueDetalleBL
    {
        /// <summary>
        ///     Metodo que actualiza un Costo Embarque Detalle
        /// </summary>
        /// <param name="info"></param>
        internal void Actualizar(CostoEmbarqueDetalleInfo info)
        {
            try
            {
                Logger.Info();
                var costoEmbarqueDetalleDAL = new CostoEmbarqueDetalleDAL();
                costoEmbarqueDetalleDAL.Actualizar(info);
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
        ///     Obtiene un Costo Embarque Detalle por Id
        /// </summary>
        /// <param name="infoId"></param>
        /// <returns></returns>
        internal CostoEmbarqueDetalleInfo ObtenerPorID(int infoId)
        {
            CostoEmbarqueDetalleInfo info;
            try
            {
                Logger.Info();
                var costoEmbarqueDetalleDAL = new CostoEmbarqueDetalleDAL();
                info = costoEmbarqueDetalleDAL.ObtenerPorID(infoId);
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
        ///     Obtiene un Costo Embarque Detalle por Id
        /// </summary>
        /// <param name="embarqueDetalleId"></param>
        /// <returns></returns>
        internal List<CostoEmbarqueDetalleInfo> ObtenerPorEmbarqueDetalleID(int embarqueDetalleId)
        {
            List<CostoEmbarqueDetalleInfo> info;
            try
            {
                Logger.Info();
                var costoEmbarqueDetalleDAL = new CostoEmbarqueDetalleDAL();
                info = costoEmbarqueDetalleDAL.ObtenerPorEmbarqueDetalleID(embarqueDetalleId);
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
        ///     Metodo que crear un Costo Embarque Detalle
        /// </summary>
        /// <param name="info"></param>
        internal void Crear(CostoEmbarqueDetalleInfo info)
        {
            try
            {
                Logger.Info();
                var costoEmbarqueDetalleDAL = new CostoEmbarqueDetalleDAL();
                costoEmbarqueDetalleDAL.Crear(info);
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
        ///     Obtiene la Lista de Costos del Embarque
        /// </summary>
        /// <param name="entradaGanado"></param>
        /// <returns></returns>
        internal List<EntradaGanadoCostoInfo> ObtenerPorEmbarqueIDOrganizacionrigen(EntradaGanadoInfo entradaGanado)
        {
            List<EntradaGanadoCostoInfo> info;
            try
            {
                Logger.Info();
                var costoEmbarqueDetalleDAL = new CostoEmbarqueDetalleDAL();
                info = costoEmbarqueDetalleDAL.ObtenerPorEmbarqueIDOrganizacionOrigen(entradaGanado);
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
        ///     Obtiene la Lista de Costos del Embarque
        /// </summary>
        /// <param name="entradaGanado"></param>
        /// <returns></returns>
        internal List<EntradaGanadoCostoInfo> ObtenerPorEmbarqueID(EntradaGanadoInfo entradaGanado)
        {
            List<EntradaGanadoCostoInfo> info;
            try
            {
                Logger.Info();
                var costoEmbarqueDetalleDAL = new CostoEmbarqueDetalleDAL();
                info = costoEmbarqueDetalleDAL.ObtenerPorEmbarqueID(entradaGanado);
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
        /// Valdiar si existen Compras directas en el ruteo
        /// </summary>
        /// <param name="entradaGanado"></param>
        /// <returns></returns>
        internal List<EntradaGanadoInfo> ObtenerEntradasPorEmbarqueID(EntradaGanadoInfo entradaGanado)
        {
            List<EntradaGanadoInfo> info;
            try
            {
                Logger.Info();
                var costoEmbarqueDetalleDAL = new CostoEmbarqueDetalleDAL();
                info = costoEmbarqueDetalleDAL.ObtenerEntradasPorEmbarqueID(entradaGanado);
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
        /// Obtiene el ultimo costo registrado que coincida con el origen, destino y proveedor
        /// </summary>
        /// <param name="embarqueDetalleInfo"></param>
        /// <returns></returns>
        internal CostoInfo ObtenerUltimoCostoPorProveedorIDOrigenIDDestinoIDCostoID(EmbarqueInfo embarqueInfo)
        {
            CostoInfo info = null;
            List<CostoInfo> lista = null;
            try
            {
                Logger.Info();
                var costoEmbarqueDetalleDAL = new CostoEmbarqueDetalleDAL();

                if (embarqueInfo.TipoEmbarque.TipoEmbarqueID == (int)TipoEmbarque.Ruteo)
                {
                    int numTotalEscalas = embarqueInfo.ListaEscala.Last().Orden;
                    int numEscala = 1;
                    int esIgual = 1;
                    if (embarqueInfo.ListaEscala.Count > 2)
                    {
                        IList<EmbarqueDetalleInfo> detalles = costoEmbarqueDetalleDAL.ObtenerEmbarqueDetallesCostosPorProveedorIDOrigenIDDestinoIDCostoID(embarqueInfo);
                        if (detalles != null)
                        {
                            detalles = (from EmbarqueDetalleInfo item in detalles
                                        orderby item.EmbarqueID descending, item.Orden
                                        select item).ToList();
                            decimal importe = 0;
                            for (int i = 0; i < detalles.Count; i++)
                            {
                                if (detalles[i].ListaCostoEmbarqueDetalle.Count > 0)
                                    importe = detalles[i].ListaCostoEmbarqueDetalle[0].Importe;
                                if (detalles[i].OrganizacionOrigen.OrganizacionID == embarqueInfo.ListaEscala[numEscala].OrganizacionOrigen.OrganizacionID &&
                                    detalles[i].OrganizacionDestino.OrganizacionID == embarqueInfo.ListaEscala[numEscala].OrganizacionDestino.OrganizacionID)
                                    esIgual = 1;
                                else
                                    esIgual = 0;

                                if (numEscala == numTotalEscalas)
                                {
                                    if (esIgual == 1)
                                    {
                                        info = new CostoInfo() { ImporteCosto = importe };
                                        break;
                                    }
                                    numEscala = 1;
                                    esIgual = 1;
                                }
                                numEscala++;
                            }
                        }

                    }
                }
                else
                {
                    lista = costoEmbarqueDetalleDAL.ObtenerCostosPorProveedorIDOrigenIDDestinoIDCostoID(embarqueInfo);
                    if (lista != null)
                    {
                        info = (from CostoInfo item in lista
                                orderby item.FechaCosto descending
                                select item).FirstOrDefault<CostoInfo>();
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
    }
}
