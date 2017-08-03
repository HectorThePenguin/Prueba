using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using SuKarne.Controls.Impresora;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class VentaGanadoDAL : DALBase
    {
        /// <summary>
        /// Obtiene el listado de los datos generales del ticket.
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="organizacionID"> </param>
        /// <returns></returns>
        internal VentaGanadoInfo ObtenerVentaGanadoPorTicket(TicketInfo Ticket)
        {
            try
            {
                Dictionary<string, object> parameters = AuxVentaGanado.ObtenerParametrosObtenerPorFolioTicket(Ticket);
                DataSet ds = Retrieve("SalidaIndividualVenta_ConsultaVentaGanadoPorTicket", parameters);
                VentaGanadoInfo venta = null;
                if (ValidateDataSet(ds))
                {
                    venta = MapVentaGanadoDAL.ObtenerVentaGanadoPorTicket(ds);
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

        internal List<ContenedorVentaGanado> ObtenerVentaGanadoPorTicketPoliza(int folioTicket, int organizacionID)
        {
            try
            {
                Dictionary<string, object> parameters = AuxVentaGanado.ObtenerParametrosPorFolioTicketPoliza(folioTicket, organizacionID);
                DataSet ds = Retrieve("VentaGanado_ObtenerSalidasPoliza", parameters);
                List<ContenedorVentaGanado> venta = null;
                if (ValidateDataSet(ds))
                {
                    venta = MapVentaGanadoDAL.ObtenerVentaGanadoPorTicketPoliza(ds);
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

        /// <summary>
        /// Obtiene los datos de la venta de tipo intensivo
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        internal List<ContenedorVentaGanado> ObtenerVentaGanadoIntensivoPorTicketPoliza(TicketInfo ticket)
        {
            try
            {
                Dictionary<string, object> parameters = AuxVentaGanado.ObtenerParametrosPorFolioTicketPoliza(ticket.FolioTicket, ticket.Organizacion);
                DataSet ds = Retrieve("VentaGanado_ObtenerSalidasPolizaVentaIntensiva", parameters);
                List<ContenedorVentaGanado> venta = null;
                if (ValidateDataSet(ds))
                {
                    venta = MapVentaGanadoDAL.ObtenerVentaGanadoIntensivoPorTicketPoliza(ds);
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
        internal ResultadoInfo<VentaGanadoInfo> ObtenerVentaGanadoPorPagina(PaginacionInfo pagina, VentaGanadoInfo ventaGanado)
        {
            try
            {
                Dictionary<string, object> parameters = AuxVentaGanado.ObtenerParametrosPorPagina(pagina, ventaGanado);
                DataSet ds = Retrieve("VentaGanado_ObtenerPorPagina", parameters);
                ResultadoInfo<VentaGanadoInfo> venta = null;
                if (ValidateDataSet(ds))
                {
                    venta = MapVentaGanadoDAL.ObtenerVentaGanadoPorPagina(ds);
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

        internal VentaGanadoInfo ObtenerPorFolioTicket(VentaGanadoInfo ventaGanado)
        {
            try
            {
                Dictionary<string, object> parameters = AuxVentaGanado.ObtenerParametrosPorFolioTicket(ventaGanado);
                DataSet ds = Retrieve("VentaGanado_ObtenerPorFolioTicket", parameters);
                VentaGanadoInfo venta = null;
                if (ValidateDataSet(ds))
                {
                    venta = MapVentaGanadoDAL.ObtenerVentaGanadoPorFolioTicket(ds);
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

        /// <summary>
        /// Obtiene la venta de ganado.
        /// </summary>
        /// <returns></returns>
        internal List<ContenedorVentaGanado> ObtenerPorFechaConciliacion(DateTime fechaInicial, DateTime fechaFinal, int organizacionID)
        {
            try
            {
                Dictionary<string, object> parameters =
                    AuxVentaGanado.ObtenerParametrosPorFechaConciliacion(fechaInicial, fechaFinal, organizacionID);
                DataSet ds = Retrieve("VentaGanado_ObtenerSalidasPolizaConciliacion", parameters);
                List<ContenedorVentaGanado> venta = null;
                if (ValidateDataSet(ds))
                {
                    venta = MapVentaGanadoDAL.ObtenerVentaGanadoPorFechaConciliacion(ds);
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
    }
}
