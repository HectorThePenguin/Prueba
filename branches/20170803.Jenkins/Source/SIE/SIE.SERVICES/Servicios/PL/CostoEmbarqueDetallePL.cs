using System;
using System.Collections.Generic;
using SIE.Services.Info.Info;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using System.Reflection;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class CostoEmbarqueDetallePL
    {
        /// <summary>
        ///     Metodo que actualiza un Costo Embarque Detalle
        /// </summary>
        /// <param name="info"></param>
        public void Actualizar(CostoEmbarqueDetalleInfo info)
        {
            try
            {
                Logger.Info();
                var costoEmbarqueDetalleBL = new CostoEmbarqueDetalleBL();
                costoEmbarqueDetalleBL.Actualizar(info);
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
        ///     Obtiene un CostoEmbarque por Id
        /// </summary>
        /// <param name="infoId"></param>
        /// <returns></returns>
        public CostoEmbarqueDetalleInfo ObtenerPorID(int infoId)
        {
            CostoEmbarqueDetalleInfo info;
            try
            {
                Logger.Info();
                var costoEmbarqueDetalleBL = new CostoEmbarqueDetalleBL();
                info = costoEmbarqueDetalleBL.ObtenerPorID(infoId);
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
        ///     Obtiene un CostoEmbarque por Id
        /// </summary>
        /// <param name="embarqueDetalleId"></param>
        /// <returns></returns>
        public List<CostoEmbarqueDetalleInfo> ObtenerPorEmbarqueDetalleID(int embarqueDetalleId)
        {
            List<CostoEmbarqueDetalleInfo> info;
            try
            {
                Logger.Info();
                var costoEmbarqueDetalleBL = new CostoEmbarqueDetalleBL();
                info = costoEmbarqueDetalleBL.ObtenerPorEmbarqueDetalleID(embarqueDetalleId);
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
        ///     Metodo que crear un Costo Embarque
        /// </summary>
        /// <param name="info"></param>
        public void Crear(CostoEmbarqueDetalleInfo info)
        {
            try
            {
                Logger.Info();
                var costoEmbarqueDetalleBL = new CostoEmbarqueDetalleBL();
                costoEmbarqueDetalleBL.Crear(info);
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
        public List<EntradaGanadoCostoInfo> ObtenerPorEmbarqueIDOrganizacionOrigen(EntradaGanadoInfo entradaGanado)
        {
            List<EntradaGanadoCostoInfo> info;
            try
            {
                Logger.Info();
                var costoEmbarqueDetalleBL = new CostoEmbarqueDetalleBL();
                info = costoEmbarqueDetalleBL.ObtenerPorEmbarqueIDOrganizacionrigen(entradaGanado);
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
        public List<EntradaGanadoCostoInfo> ObtenerPorEmbarqueID(EntradaGanadoInfo entradaGanado)
        {
            List<EntradaGanadoCostoInfo> info;
            try
            {
                Logger.Info();
                var costoEmbarqueDetalleBL = new CostoEmbarqueDetalleBL();
                info = costoEmbarqueDetalleBL.ObtenerPorEmbarqueID(entradaGanado);
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
        public List<EntradaGanadoInfo> ObtenerEntradasPorEmbarqueID(EntradaGanadoInfo entradaGanado)
        {
            List<EntradaGanadoInfo> info;
            try
            {
                Logger.Info();
                var costoEmbarqueDetalleBL = new CostoEmbarqueDetalleBL();
                info = costoEmbarqueDetalleBL.ObtenerEntradasPorEmbarqueID(entradaGanado);
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
        public CostoInfo ObtenerUltimoPorCostoIDProveedorIDOrganizacionOrigenOrganizacionDestino(EmbarqueInfo embarqueInfo)
        {
            CostoInfo info;
            try
            {
                Logger.Info();
                var costoEmbarqueDetalleBL = new CostoEmbarqueDetalleBL();
                info = costoEmbarqueDetalleBL.ObtenerUltimoCostoPorProveedorIDOrigenIDDestinoIDCostoID(embarqueInfo);
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
