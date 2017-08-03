using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Facturas;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class SalidaIndividualDAL:DALBase
    {
        internal int Guardar(string arete, int organizacion, string codigoCorral, int corraletaID, int usuarioCreacion, int tipoMovimiento)
        {
            try
            {
                int result = 0;
                Logger.Info();
                Dictionary<string, object> parameters = AuxSalidaIndividualDAL.ObtenerParametrosGrabar(arete, organizacion, codigoCorral, corraletaID, usuarioCreacion, tipoMovimiento);
                result = Create("SalidaIndividual_Guardar", parameters);
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                return -1;
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                return -1;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return -1;
            }
            
        }
        
        /// <summary>
        /// Método para obtener el ticket
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        internal int ObtenerTicket(TicketInfo ticket)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSalidaIndividualDAL.ObtenerParametrosObtenerTicket(ticket);
                int result = Create("SalidaIndividualVenta_GenerarFolio", parameters);
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
        /// Metodo que le da salida por venta al ganado
        /// </summary>
        /// <param name="salidaIndividual"></param>
        /// <returns></returns>
        internal int GuardarSalidaIndividualGanadoVenta(SalidaIndividualInfo salidaIndividual)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSalidaIndividualDAL.ObtenerParametrosGuardarSalidaIndividualGanado(salidaIndividual);
                int result = Create("SalidaIndividualVenta_GuardarGanadoVendido", parameters);
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
        /// Obtiene los datos de la factura para generarla
        /// </summary>
        /// <param name="folioTicket"></param>
        /// <param name="organizacionID"> </param>
        /// <returns></returns>
        internal FacturaInfo ObtenerDatosFacturaVentaDeGanado(int folioTicket, int organizacionID)
        {
            FacturaInfo facturaInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxSalidaIndividualDAL.ObtenerParametrosObtenerDatosFacturaVentaDeGanado(folioTicket, organizacionID);
                DataSet ds = Retrieve("SalidaIndividual_ObtenerDatosFactura", parameters);
                if (ValidateDataSet(ds))
                {
                    facturaInfo = MapSalidaIndividualDAL.ObtenerDatosFacturaSalidaIndividual(ds);
                }
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

            return facturaInfo;
        }

        /// <summary>
        /// Obtiene los datos de la factura para generarla
        /// </summary>
        /// <param name="folioTicket"></param>
        /// <param name="organizacionId"> </param>
        /// <returns></returns>
        internal FacturaInfo ObtenerDatosFacturaVentaDeGanadoIntensivo(int folioTicket, int organizacionId)
        {
            FacturaInfo facturaInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxSalidaIndividualDAL.ObtenerParametrosObtenerDatosFacturaVentaDeGanado(folioTicket, organizacionId);
                DataSet ds = Retrieve("SalidaIndividualIntensivo_ObtenerDatosFactura", parameters);
                if (ValidateDataSet(ds))
                {
                    facturaInfo = MapSalidaIndividualDAL.ObtenerDatosFacturaSalidaIndividualIntensivo(ds);
                }
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

            return facturaInfo;
        }

        /// <summary>
        /// Guarda los historicos de los animales que salieron por venta
        /// </summary>
        internal void GuardarCostosHistoricos(SalidaIndividualInfo salidaIndividual)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSalidaIndividualDAL.ObtenerParametrosGuardarHistoricos(salidaIndividual);
                Create("SalidaIndividualVenta_GuardarCostosHistoricos", parameters);
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
        /// Guarda los historicos de los animales que salieron por venta
        /// </summary>
        internal void GuardarConsumoHistoricos(SalidaIndividualInfo salidaIndividual)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSalidaIndividualDAL.ObtenerParametrosGuardarHistoricos(salidaIndividual);
                Create("SalidaIndividualVenta_GuardarConsumoHistoricos", parameters);
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
        /// Guarda los historicos de los animales que salieron por venta
        /// </summary>
        internal void GuardarAnimalHistoricos(SalidaIndividualInfo salidaIndividual)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSalidaIndividualDAL.ObtenerParametrosGuardarHistoricos(salidaIndividual);
                Create("SalidaIndividualVenta_GuardarAnimalHistoricos", parameters);
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
