using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class VentaGanadoDetalleDAL : DALBase
    {
        internal List<VentaGanadoDetalleInfo> ObtenerVentaGanadoPorTicket(int ventaGanadoID)
        {
            try
            {
                Dictionary<string, object> parameters = AuxVentaGanadoDetalle.ObtenerParametrosObtenerPorVentaGanadoID(ventaGanadoID);
                DataSet ds = Retrieve("SalidaIndividualVenta_ConsultaDetalleVentaGanadoID", parameters);
                List<VentaGanadoDetalleInfo> venta = null;
                if (ValidateDataSet(ds))
                {
                    venta = MapVentaGanadoDetalleDAL.ObtenerVentaGanadoDetallePorID(ds);
                }
                return venta;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal int GuardarDetalle(GrupoVentaGanadoInfo venta)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters;
                if (venta.TipoVenta == Info.Enums.TipoVentaEnum.Propio)
                {
                    parameters = AuxVentaGanadoDetalle.ObtenerParametrosGrabarDetallePropio(venta);
                }
                else
                {
                    parameters = AuxVentaGanadoDetalle.ObtenerParametrosGrabarDetalleExterno(venta);
                }
                int result = Create("SalidaIndividualVenta_GuardarDetalleGanado", parameters);
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para ver si existe el animal como vendido
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        public VentaGanadoDetalleInfo ExisteAnimal(AnimalInfo animal)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxVentaGanadoDetalle.ObtenerParametrosAnimalID(animal);
                DataSet ds = Retrieve("SalidaIndividualVenta_ObtenerVentaDetallePorAnimalID", parameters);
                VentaGanadoDetalleInfo ventaDetalle = null;
                if (ValidateDataSet(ds))
                {
                    ventaDetalle = MapVentaGanadoDetalleDAL.ObtenerVentaGanadoDetalle(ds);
                }
                return ventaDetalle;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
