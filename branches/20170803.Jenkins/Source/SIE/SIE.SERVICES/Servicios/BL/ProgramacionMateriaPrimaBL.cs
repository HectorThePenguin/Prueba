using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class ProgramacionMateriaPrimaBL
    {

        /// <summary>
        /// Almacena la programacion de la cantidad de un producto 
        /// </summary>
        /// <param name="pedido"></param>
        /// <param name="listaProgramacion"></param>
        /// <returns></returns>
        internal bool GuardarProgramacionMateriaPrima(PedidoInfo pedido, List<ProgramacionMateriaPrimaInfo> listaProgramacion )
        {
            bool resultado = true;

            try
            {
                using (var transaction = new TransactionScope())
                {
                    var programacionMateriaDal = new ProgramacionMateriaPrimaDAL();

                    if (listaProgramacion != null && listaProgramacion.Count > 0)
                    {
                        programacionMateriaDal.GuardarProgramacionMateriaPrima(listaProgramacion.Where(pr=> pr.ProgramacionMateriaPrimaId ==0).ToList());

                        var pedidoBl = new PedidosBL();
                        pedidoBl.ActualizarEstatusPedido(pedido);
                        transaction.Complete();
                    }

                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod() , ex);
            }

            return resultado;
        }

        /// <summary>
        /// Obtiene el listado de lotes que tiene programados para el surtido
        /// </summary>
        /// <param name="pedidoDetalle"></param>
        /// <returns></returns>
        internal List<ProgramacionMateriaPrimaInfo> ObtenerProgramacionMateriaPrima(PedidoDetalleInfo pedidoDetalle)
        {
            try
            {
                Logger.Info();
                var programacionMateriaPrimaDal = new ProgramacionMateriaPrimaDAL();
                var listaProgramacion = programacionMateriaPrimaDal.ObtenerProgramacionMateriaPrima(pedidoDetalle);

                if (listaProgramacion != null)
                {
                    var almacenInventarioLoteBl = new AlmacenInventarioLoteBL();
                    var almacenBl = new AlmacenBL();
                    var pesaje = new PesajeMateriaPrimaBL();

                    foreach (ProgramacionMateriaPrimaInfo programacionMateria in listaProgramacion)
                    {
                        programacionMateria.InventarioLoteOrigen =
                            almacenInventarioLoteBl.ObtenerAlmacenInventarioLotePorId(
                                programacionMateria.InventarioLoteOrigen.AlmacenInventarioLoteId);

                        programacionMateria.Almacen = almacenBl.ObtenerPorID(programacionMateria.Almacen.AlmacenID);
                        programacionMateria.PesajeMateriaPrima =
                            pesaje.ObtenerPesajesPorProgramacionMateriaPrimaId(
                                programacionMateria.ProgramacionMateriaPrimaId);
                    }
                }

                return listaProgramacion;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo que actualiza la cantidad entregada
        /// </summary>
        /// <param name="programacionMateriaPrimaInfo"></param>
        internal void ActualizarCantidadEntregada(ProgramacionMateriaPrimaInfo programacionMateriaPrimaInfo)
        {
            try
            {
                Logger.Info();
                var programacionMateriaPrimaDal = new ProgramacionMateriaPrimaDAL();
                programacionMateriaPrimaDal.ActualizarCantidadEntregada(programacionMateriaPrimaInfo);
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
        /// Metodo que actualiza la Justificacion
        /// </summary>
        /// <param name="programacionMateriaPrimaInfo"></param>
        internal void ActualizarJustificacion(ProgramacionMateriaPrimaInfo programacionMateriaPrimaInfo)
        {
            try
            {
                Logger.Info();
                var programacionMateriaPrimaDal = new ProgramacionMateriaPrimaDAL();
                programacionMateriaPrimaDal.ActualizarJustificacion(programacionMateriaPrimaInfo);
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
        /// Obtiene una programacion de materia prima
        /// </summary>
        /// <param name="pesajeMateriaPrima"></param>
        /// <returns></returns>
        internal ProgramacionMateriaPrimaInfo ObtenerPorPesajeMateriaPrima(PesajeMateriaPrimaInfo pesajeMateriaPrima)
        {
            try
            {
                Logger.Info();
                var programacionMateriaPrimaDal = new ProgramacionMateriaPrimaDAL();
                var programacion = programacionMateriaPrimaDal.ObtenerPorPesajeMateriaPrima(pesajeMateriaPrima);
                return programacion;
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
        /// <param name="programacionMateriaPrimaID"></param>
        /// <param name="almacenMovimientoID"></param>
        internal void ActualizarAlmacenMovimiento(int programacionMateriaPrimaID, long almacenMovimientoID)
        {
            try
            {
                Logger.Info();
                var programacionMateriaPrimaDal = new ProgramacionMateriaPrimaDAL();
                programacionMateriaPrimaDal.ActualizarAlmacenMovimiento(programacionMateriaPrimaID, almacenMovimientoID);
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
        /// Obtiene una programacion de materia prima por Clave y Ticket
        /// </summary>
        /// <param name="programacionMateriaPrimaId"></param>
        /// <param name="ticket"></param>
        /// <returns></returns>
        internal ProgramacionMateriaPrimaInfo ObtenerPorProgramacionMateriaPrimaTicket(int programacionMateriaPrimaId, int ticket)
        {
            try
            {
                Logger.Info();
                var programacionMateriaPrimaDal = new ProgramacionMateriaPrimaDAL();
                ProgramacionMateriaPrimaInfo programacion =
                    programacionMateriaPrimaDal.ObtenerPorProgramacionMateriaPrimaTicket(programacionMateriaPrimaId,
                                                                                         ticket);
                return programacion;
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
                var programacionMateriaPrimaDal = new ProgramacionMateriaPrimaDAL();
                programacionMateriaPrimaDal.Cancelar(programacion);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
