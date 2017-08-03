using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Servicios.BL
{
    internal class BasculaMateriaPrimaBL
    {
        /// <summary>
        /// Metodo que guarda el primer pesaje (peso tara)
        /// </summary>
        /// <returns></returns>
        public PesajeMateriaPrimaInfo GuardarPrimerPesaje(PesajeMateriaPrimaInfo pesajeMateriaPrimaInfo)
        {
            PesajeMateriaPrimaInfo result;
            try
            {
                using (var transaction = new TransactionScope())
                {
                    var pesajeMateriaPrimaBl = new PesajeMateriaPrimaBL();
                    result = pesajeMateriaPrimaBl.Crear(pesajeMateriaPrimaInfo);
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
            return result;
        }

        /// <summary>
        /// Guarda el segundo pesaje
        /// </summary>
        /// <param name="pesajeMateriaPrimaInfo"></param>
        /// <param name="pesoNeto"></param>
        /// <param name="pedidoInfo"></param>
        /// <returns></returns>
        public void GuardarSegundoPesaje(PesajeMateriaPrimaInfo pesajeMateriaPrimaInfo, decimal pesoNeto, PedidoInfo pedidoInfo)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    var pesajeMateriaPrimaBl = new PesajeMateriaPrimaBL();
                    var pedidoBl = new PedidosBL();
                    //Se actualiza el registro, no se inserta

                    var pesoBruto = pesajeMateriaPrimaInfo.PesoBruto;
                    var usuario = pesajeMateriaPrimaInfo.UsuarioModificacionID;
                    pesajeMateriaPrimaInfo = pesajeMateriaPrimaBl.ObtenerPorId(pesajeMateriaPrimaInfo);
                    pesajeMateriaPrimaInfo.PesoBruto = pesoBruto;
                    pesajeMateriaPrimaInfo.UsuarioModificacionID = usuario;
                    pesajeMateriaPrimaInfo.EstatusID = Estatus.PedidoPendiente.GetHashCode();
                    pesajeMateriaPrimaBl.ActualizarPesajePorId(pesajeMateriaPrimaInfo);

                    //Actualizar programacion materia prima
                    var programacionMateriaPrimaBl = new ProgramacionMateriaPrimaBL();
                    var programacionMateriaPrimaInfo = new ProgramacionMateriaPrimaInfo()
                    {
                        ProgramacionMateriaPrimaId = pesajeMateriaPrimaInfo.ProgramacionMateriaPrimaID,
                        CantidadEntregada = pesoNeto,
                        UsuarioModificacion = new UsuarioInfo(){UsuarioID = pesajeMateriaPrimaInfo.UsuarioModificacionID}
                    };
                    programacionMateriaPrimaBl.ActualizarCantidadEntregada(programacionMateriaPrimaInfo);

                    //Actualizar estatus del folio
                    pedidoInfo.EstatusPedido.EstatusId = Estatus.PedidoParcial.GetHashCode();
                    if (pedidoInfo.UsuarioModificacion == null)
                    {
                        pedidoInfo.UsuarioModificacion = new UsuarioInfo();
                    }
                    pedidoInfo.UsuarioModificacion.UsuarioModificacionID = pesajeMateriaPrimaInfo.UsuarioModificacionID;
                    pedidoBl.ActualizarEstatusPedido(pedidoInfo);

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
