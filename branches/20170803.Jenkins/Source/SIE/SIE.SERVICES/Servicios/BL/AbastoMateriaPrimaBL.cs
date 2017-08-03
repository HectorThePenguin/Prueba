using System;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Servicios.BL
{
    internal class AbastoMateriaPrimaBL
    {
        /// <summary>
        /// Guarda un nuevo pesaje de materia prima
        /// </summary>
        /// <param name="pesajeMateriaPrima"></param>
        /// <param name="programacionMateriaPrima"></param>
        /// <param name="pedido"></param>
        internal void GuardarAbastoDeMateriaPrima(PesajeMateriaPrimaInfo pesajeMateriaPrima, ProgramacionMateriaPrimaInfo programacionMateriaPrima, int pedido)
        {
            try
            {
                Logger.Info();
                using (var pesajeMateriaPrimaBl = new PesajeMateriaPrimaBL())
                {
                    var programacionMateriaPrimaBl = new ProgramacionMateriaPrimaBL();
                    var pedidoBl = new PedidosBL();

                    using (var transaction = new TransactionScope())
                    {
                        pesajeMateriaPrimaBl.Crear(pesajeMateriaPrima);
                        programacionMateriaPrimaBl.ActualizarCantidadEntregada(programacionMateriaPrima);
                        programacionMateriaPrimaBl.ActualizarJustificacion(programacionMateriaPrima);

                        pedidoBl.ActualizarEstatusPedido(new PedidoInfo
                        {
                            PedidoID = pedido,
                            EstatusPedido = new EstatusInfo
                            {
                                EstatusId = (int)Estatus.PedidoParcial
                            },
                            UsuarioModificacion = new UsuarioInfo { UsuarioModificacionID = pesajeMateriaPrima.UsuarioModificacionID }
                        });

                        transaction.Complete();
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
        }
        /// <summary>
        /// Actualiza el pesaje de materia prima
        /// </summary>
        /// <param name="pesajeMateriaPrima"></param>
        /// <param name="programacionMateriaPrima"></param>
        internal void ActualizarAbastoDeMateriaPrima(PesajeMateriaPrimaInfo pesajeMateriaPrima, ProgramacionMateriaPrimaInfo programacionMateriaPrima)
        {
            try
            {
                Logger.Info();
                var pesajeMateriaPrimaBl = new PesajeMateriaPrimaBL();
                var programacionMateriaPrimaBl = new ProgramacionMateriaPrimaBL();

                using (var transaction = new TransactionScope())
                {
                    pesajeMateriaPrima.Activo = true;
                    var pesaje = pesajeMateriaPrimaBl.ObtenerPorId(pesajeMateriaPrima);
                    pesaje.UsuarioModificacionID = pesajeMateriaPrima.UsuarioModificacionID;
                    pesaje.Piezas += pesajeMateriaPrima.Piezas;

                    pesajeMateriaPrimaBl.ActualizarPesajePorId(pesaje);

                    programacionMateriaPrimaBl.ActualizarJustificacion(programacionMateriaPrima);

                    transaction.Complete();
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
    }
}
