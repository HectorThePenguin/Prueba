using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Transactions;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    public class ProduccionDiariaBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad ProduccionDiaria
        /// </summary>
        /// <param name="info"></param>
        /// <param name="usuario"></param>
        public int Guardar(ProduccionDiariaInfo info, UsuarioInfo usuario)
        {
            try
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    Logger.Info();
                    
                    info.UsuarioCreacionID = usuario.UsuarioID;
                    if (info.ListaProduccionDiariaDetalle != null)
                    {
                        info.ListaProduccionDiariaDetalle.ForEach(prod => prod.UsuarioCreacionID = usuario.UsuarioID);
                    }
                    if (info.ListaTiempoMuerto != null)
                    {
                        info.ListaTiempoMuerto.ForEach(prod => prod.UsuarioCreacionID = usuario.UsuarioID);
                    }

                    var produccionDiariaDAL = new ProduccionDiariaDAL();

                    int result = info.ProduccionDiariaID;
                    if (info.ProduccionDiariaID == 0)
                    {
                        result = produccionDiariaDAL.Crear(info);
                    }
                    else
                    {
                        produccionDiariaDAL.Actualizar(info);
                    }
                    if (info.ListaProduccionDiariaDetalle != null)
                    {
                        GuardarProduccionDiariaDetalle(info, result);
                    }
                    if (info.ListaTiempoMuerto != null)
                    {
                        GuardarTiempoMuerto(info, result);
                    }
                    transaction.Complete();
                    return result;
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

        private void GuardarProduccionDiariaDetalle(ProduccionDiariaInfo info, int produccionDiariaID)
        {
            var produccionDiariaDetalleDAL = new ProduccionDiariaDetalleDAL();
            produccionDiariaDetalleDAL.GuardarProduccionDiariaDetalle(info, produccionDiariaID);

            //var almacenInventarioLoteDAL = new AlmacenInventarioLoteDAL();
            //almacenInventarioLoteDAL.DescontarAlmacenInventarioLoteProduccionDiaria(info);

            //var almacenInventarioDal = new AlmacenInventarioDAL();
            //almacenInventarioDal.DescontarAlmacenInventarioProduccionDiaria(info);

            //var pesajeMateriaPrimaBl = new PesajeMateriaPrimaBL();
            //var programacionMateriaPrimaBl = new ProgramacionMateriaPrimaBL();
            //var almacenMovimientoBl = new AlmacenMovimientoBL();
            //var almacenMovimientoDetalleBl = new AlmacenMovimientoDetalleBL();
            //var almacenInventarioLoteBl = new AlmacenInventarioLoteBL();
            //foreach (var produccionDiariaDetalleInfo in info.ListaProduccionDiariaDetalle)
            //{
            //    var pesajeMateriaPrima = pesajeMateriaPrimaBl.ObtenerPorID(produccionDiariaDetalleInfo.PesajeMateriaPrimaID);
            //    if (pesajeMateriaPrima != null)
            //    {
            //        var programacion = programacionMateriaPrimaBl.ObtenerPorPesajeMateriaPrima(pesajeMateriaPrima);
            //        var almacenMovimiento = new AlmacenMovimientoInfo();
            //        almacenMovimiento.AlmacenID = programacion.Almacen.AlmacenID;
            //        almacenMovimiento.Almacen = programacion.Almacen;
            //        almacenMovimiento.Status = (int) Estatus.AplicadoInv;
            //        almacenMovimiento.TipoMovimientoID = (int) TipoMovimiento.SalidaProduccion;
            //        almacenMovimiento.UsuarioCreacionID = produccionDiariaDetalleInfo.UsuarioCreacionID;

            //        almacenMovimiento.AlmacenMovimientoID = almacenMovimientoBl.Crear(almacenMovimiento);

            //        var almacenMovimientoDetalleInfo = new AlmacenMovimientoDetalle();
            //        almacenMovimientoDetalleInfo.AlmacenMovimientoID = almacenMovimiento.AlmacenMovimientoID;
            //        almacenMovimientoDetalleInfo.AlmacenInventarioLoteId =
            //            programacion.InventarioLoteOrigen.AlmacenInventarioLoteId;
            //        almacenMovimientoDetalleInfo.Piezas = 0;
            //        almacenMovimientoDetalleInfo.ProductoID = produccionDiariaDetalleInfo.ProductoID;
            //        almacenMovimientoDetalleInfo.UsuarioCreacionID = produccionDiariaDetalleInfo.UsuarioCreacionID;

            //        var almacenInventarioLoteInfo =
            //            almacenInventarioLoteBl.ObtenerAlmacenInventarioLotePorId(
            //                programacion.InventarioLoteOrigen.AlmacenInventarioLoteId);
            //        almacenMovimientoDetalleInfo.Cantidad = produccionDiariaDetalleInfo.KilosNeto;
            //        almacenMovimientoDetalleInfo.Precio = almacenInventarioLoteInfo.PrecioPromedio;
            //        almacenMovimientoDetalleInfo.Importe = produccionDiariaDetalleInfo.KilosNeto * almacenInventarioLoteInfo.PrecioPromedio;

            //        almacenMovimientoDetalleBl.Crear(almacenMovimientoDetalleInfo);
            //    }
            //}
        }

        private void GuardarTiempoMuerto(ProduccionDiariaInfo info, int produccionDiariaID)
        {
            var tiempoMuertoDAL = new TiempoMuertoDAL();
            tiempoMuertoDAL.GuardarTiempoMuerto(info, produccionDiariaID);
        }

        /// <summary>
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ProduccionDiariaInfo> ObtenerPorPagina(PaginacionInfo pagina, ProduccionDiariaInfo filtro)
        {
            try
            {
                Logger.Info();
                var produccionDiariaDAL = new ProduccionDiariaDAL();
                ResultadoInfo<ProduccionDiariaInfo> result = produccionDiariaDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de ProduccionDiaria
        /// </summary>
        /// <returns></returns>
        public IList<ProduccionDiariaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var produccionDiariaDAL = new ProduccionDiariaDAL();
                IList<ProduccionDiariaInfo> result = produccionDiariaDAL.ObtenerTodos();
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
        /// Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<ProduccionDiariaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var produccionDiariaDAL = new ProduccionDiariaDAL();
                IList<ProduccionDiariaInfo> result = produccionDiariaDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad ProduccionDiaria por su Id
        /// </summary>
        /// <param name="produccionDiariaID">Obtiene una entidad ProduccionDiaria por su Id</param>
        /// <returns></returns>
        public ProduccionDiariaInfo ObtenerPorID(int produccionDiariaID)
        {
            try
            {
                Logger.Info();
                var produccionDiariaDAL = new ProduccionDiariaDAL();
                ProduccionDiariaInfo result = produccionDiariaDAL.ObtenerPorID(produccionDiariaID);
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
        /// Obtiene una entidad ProduccionDiaria por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public ProduccionDiariaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var produccionDiariaDAL = new ProduccionDiariaDAL();
                ProduccionDiariaInfo result = produccionDiariaDAL.ObtenerPorDescripcion(descripcion);
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
    }
}

