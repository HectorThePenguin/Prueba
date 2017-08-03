using System;
using System.Collections.Generic;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    public class InterfaceSalidaTraspasoCostoBL
    {
        /// <summary>
        /// Genera los costos del ganado transferido
        /// </summary>
        /// <param name="costosGanadoTransferido"></param>
        internal void Guardar(List<InterfaceSalidaTraspasoCostoInfo> costosGanadoTransferido)
        {
            try
            {
                Logger.Info();
                var interfaceSalidaTraspasoCostoDAL = new InterfaceSalidaTraspasoCostoDAL();
                interfaceSalidaTraspasoCostoDAL.Crear(costosGanadoTransferido);
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
        /// Obtiene los costos de los animales enviados
        /// a sacrificio
        /// </summary>
        /// <param name="interfaceSalidaTraspasoDetalle"></param>
        /// <returns></returns>
        internal List<InterfaceSalidaTraspasoCostoInfo> ObtenerCostosInterfacePorDetalle(List<InterfaceSalidaTraspasoDetalleInfo> interfaceSalidaTraspasoDetalle)
        {
            try
            {
                Logger.Info();
                var interfaceSalidaTraspasoCostoDAL = new InterfaceSalidaTraspasoCostoDAL();
                List<InterfaceSalidaTraspasoCostoInfo> result =
                    interfaceSalidaTraspasoCostoDAL.ObtenerCostosInterfacePorDetalle(interfaceSalidaTraspasoDetalle);
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
        /// Actualiza bandera de facturado
        /// </summary>
        /// <param name="animalesAFacturar"></param>
        internal void ActualizarFacturado(List<long> animalesAFacturar)
        {
            try
            {
                Logger.Info();
                var interfaceSalidaTraspasoCostoDAL = new InterfaceSalidaTraspasoCostoDAL();
                interfaceSalidaTraspasoCostoDAL.ActualizarFacturado(animalesAFacturar);
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
