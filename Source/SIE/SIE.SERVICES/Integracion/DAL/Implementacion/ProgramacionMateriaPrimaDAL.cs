using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using BLToolkit.Data.Sql;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using DataException = BLToolkit.Data.DataException;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ProgramacionMateriaPrimaDAL : DALBase
    {
        /// <summary>
        /// Almacena toda la programación generada para los diferentes productos de pedidos.
        /// </summary>
        /// <param name="listaProgramacion"></param>
        /// <returns></returns>
        public int GuardarProgramacionMateriaPrima(List<ProgramacionMateriaPrimaInfo> listaProgramacion)
        {
            int resultado;

            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxProgramacionMateriaPrimaDAL.ObtenerParametrosGuardarProgramacionMateriaPrima(listaProgramacion);

                resultado = Create("ProgramacionMateriaPrima_Crear", parametros);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resultado;
        }

        /// <summary>
        /// Obtiene la programacion de materia prima del pedido detalle
        /// </summary>
        /// <param name="pedidoDetalle"></param>
        /// <returns></returns>
        internal List<ProgramacionMateriaPrimaInfo> ObtenerProgramacionMateriaPrima(PedidoDetalleInfo pedidoDetalle)
        {
            List<ProgramacionMateriaPrimaInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxProgramacionMateriaPrimaDAL.ObtenerParametrosObtenerProgramacionMateriaPrima(pedidoDetalle);
                DataSet ds = Retrieve("ProgramacionMateriaPrima_ObtenerPorPedidoDetalle", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapProgramacionMateriaPrimaDAL.ObtenerProgramacionMateriaPrima(ds);
                }

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
        /// Actualiza la cantidad entregada
        /// </summary>
        internal void ActualizarCantidadEntregada(ProgramacionMateriaPrimaInfo programacionMateriaPrimaInfo)
        {
            try
            {
                Dictionary<string, object> parameters = AuxProgramacionMateriaPrimaDAL.ObtenerParametrosActualizarCantidadEntregada(programacionMateriaPrimaInfo);
                Update("ProgramacionMateriaPrima_ActualizarCantidadEntregada", parameters);
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
        /// Actualiza una justificacion
        /// </summary>
        /// <param name="programacionMateriaPrimaInfo"></param>
        internal void ActualizarJustificacion(ProgramacionMateriaPrimaInfo programacionMateriaPrimaInfo)
        {
            try
            {
                Dictionary<string, object> parameters = AuxProgramacionMateriaPrimaDAL.ObtenerParametrosActualizarJustificacion(programacionMateriaPrimaInfo);
                Update("ProgramacionMateriaPrima_ActualizarJustificacion", parameters);
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
        /// Obtiene una programacion de materia prima por pesaje
        /// </summary>
        /// <param name="pesajeMateriaPrima"></param>
        /// <returns></returns>
        internal ProgramacionMateriaPrimaInfo ObtenerPorPesajeMateriaPrima(PesajeMateriaPrimaInfo pesajeMateriaPrima)
        {
            ProgramacionMateriaPrimaInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxProgramacionMateriaPrimaDAL.ObtenerPorPesajeMateriaPrima(pesajeMateriaPrima);
                DataSet ds = Retrieve("ProgramacionMateriaPrima_ObtenerPorProgramacionMateriaPrimaID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapProgramacionMateriaPrimaDAL.ObtenerPorPesajeMateriaPrima(ds);
                }

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
        /// Actualiza el almacen movimiento
        /// </summary>
        /// <param name="programacionMateriaPrimaID"> </param>
        /// <param name="almacenMovimientoID"></param>
        internal void ActualizarAlmacenMovimiento(int programacionMateriaPrimaID, long almacenMovimientoID)
        {
            try
            {
                Dictionary<string, object> parameters =
                    AuxProgramacionMateriaPrimaDAL.ObtenerParametrosActualizarAlmacenMovimiento(
                        programacionMateriaPrimaID, almacenMovimientoID);
                Update("ProgramacionMateriaPrima_ActualizarAlmacenMovimiento", parameters);
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
        /// Obtiene una programacion de materia prima por Clave y Ticket
        /// </summary>
        /// <param name="programacionMateriaPrimaId"></param>
        /// <param name="ticket"></param>
        /// <returns></returns>
        internal ProgramacionMateriaPrimaInfo ObtenerPorProgramacionMateriaPrimaTicket(int programacionMateriaPrimaId, int ticket)
        {
            ProgramacionMateriaPrimaInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxProgramacionMateriaPrimaDAL.ObtenerPorProgramacionMateriaPrimaTicket(programacionMateriaPrimaId,
                                                                                            ticket);
                DataSet ds = Retrieve("ProgramacionMateriaPrima_ObtenerPorProgramacionMateriaPrimaTicket", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapProgramacionMateriaPrimaDAL.ObtenerPorProgramacionMateriaPrimaTicket(ds);
                }

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

        internal bool Cancelar(ProgramacionMateriaPrimaInfo programacion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxProgramacionMateriaPrimaDAL.ObtenerPorCancelar(programacion);
                Update("ProgramacionMateriaPrima_Cancelar", parameters);

                return true;
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
