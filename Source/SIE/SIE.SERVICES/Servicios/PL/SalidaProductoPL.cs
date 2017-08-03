using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Base.Exepciones;
using SIE.Services.Servicios.BL;
using System.IO;

namespace SIE.Services.Servicios.PL
{
    public class SalidaProductoPL
    {
        private SalidaProductoInfo ObtenerPorSalidaProductoId(SalidaProductoInfo salidaProducto)
        {
            try
            {
                var salidaProductoBl = new SalidaProductoBL();
                salidaProducto = salidaProductoBl.ObtenerPorSalidaProductoId(salidaProducto);
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
            return salidaProducto;
        }

        /// <summary>
        /// Obtiene una salida de producto por foliosalida
        /// </summary>
        /// <param name="salidaProducto"></param>
        /// <returns></returns>
        public SalidaProductoInfo ObtenerPorFolioSalida(SalidaProductoInfo salidaProducto)
        {
            try
            {
                var salidaProductoBl = new SalidaProductoBL();
                salidaProducto = salidaProductoBl.ObtenerPorFolioSalida(salidaProducto);
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
            return salidaProducto;
        }
        /// <summary>
        ///     Obtiene un lista paginada de los folios de entrada ganado
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<SalidaProductoInfo> ObtenerFoliosPorPaginaParaSalidaProducto(PaginacionInfo pagina, SalidaProductoInfo filtro)
        {
            ResultadoInfo<SalidaProductoInfo> costoLista;
            try
            {
                Logger.Info();
                var entradaProductoBL = new SalidaProductoBL();
                costoLista = entradaProductoBL.ObtenerFoliosPorPaginaParaSalidaProducto(pagina, filtro);
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
            return costoLista;
        }

        /// <summary>
        ///     Obtiene un lista paginada de los folios de entrada ganado
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<SalidaProductoInfo> ObtenerFoliosPorPaginaParaCancelacion(PaginacionInfo pagina, SalidaProductoInfo filtro)
        {
            ResultadoInfo<SalidaProductoInfo> costoLista;
            try
            {
                Logger.Info();
                var entradaProductoBL = new SalidaProductoBL();
                costoLista = entradaProductoBL.ObtenerFoliosPorPaginaParaCancelacion(pagina, filtro);
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
            return costoLista;
        }
        

        /// <summary>
        /// Guarda el primer pesaje de la salida
        /// </summary>
        /// <param name="salida"></param>
        /// <returns></returns>
        public SalidaProductoInfo GuardarPrimerPesajeSalida(SalidaProductoInfo salida)
        {
            SalidaProductoInfo resultado;
            try
            {
                Logger.Info();
                var entradaProductoBL = new SalidaProductoBL();
                resultado = entradaProductoBL.GuardarPrimerPesajeSalida(salida);
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
            return resultado;
        }
        /// <summary>
        /// Guarda el segundo pesaje de la salida de producto
        /// </summary>
        /// <param name="salida"></param>
        /// <returns></returns>
        public SalidaProductoInfo GuardarSegundoPesajeSalida(SalidaProductoInfo salida)
        {
            SalidaProductoInfo resultado;
            try
            {
                Logger.Info();
                var salidaProductoBl = new SalidaProductoBL();
                resultado = salidaProductoBl.GuardarSegundoPesajeSalida(salida);
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
            return resultado;
        }
        /// <summary>
        /// Termina la salida del producto
        /// </summary>
        /// <param name="salida"></param>
        /// <returns></returns>
        public MemoryStream TerminarSalidaProducto(SalidaProductoInfo salida)
        {
            MemoryStream resultado;
            try
            {
                Logger.Info();
                var salidaProductoBl = new SalidaProductoBL();
                resultado = salidaProductoBl.TerminarSalidaProducto(salida);
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
            return resultado;
        }

        /// <summary>
        /// Actualiza el almacen, almacen inventario lote y piezas.
        /// </summary>
        /// <param name="salida"></param>
        public void ActualizarAlmacenInventarioLote(SalidaProductoInfo salida)
        {
            try
            {
                var salidaProductoBl = new SalidaProductoBL();
                salidaProductoBl.ActualizarAlmacenInventarioLote(salida);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(),ex);
            }
        }

        /// <summary>
        /// Consulta los folios activos con el peso tara capturado.
        /// </summary>
        /// <param name="salida"></param>
        /// <returns></returns>
        public List<SalidaProductoInfo> ObtenerTraspasoFoliosActivos(SalidaProductoInfo salida)
        {

            List<SalidaProductoInfo> salidas = null;

            try
            {
                var salidaProductoBl = new SalidaProductoBL();
                salidas = salidaProductoBl.ObtenerTraspasoFoliosActivos(salida);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return salidas;
        }

        /// <summary>
        /// Obtiene un lista paginada de los folios de salida
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<SalidaProductoInfo> ObtenerFolioPorPaginaReimpresion(PaginacionInfo pagina, SalidaProductoInfo filtro)
        {
            ResultadoInfo<SalidaProductoInfo> salidas;
            try
            {
                Logger.Info();
                var entradaProductoBL = new SalidaProductoBL();
                salidas = entradaProductoBL.ObtenerFolioPorPaginaReimpresion(pagina, filtro);
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
            return salidas;
        }

        /// <summary>
        /// Obtiene un folio de salida
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public SalidaProductoInfo ObtenerFolioPorReimpresion(SalidaProductoInfo filtro)
        {
            SalidaProductoInfo salida;
            try
            {
                Logger.Info();
                var entradaProductoBL = new SalidaProductoBL();
                salida = entradaProductoBL.ObtenerFolioPorReimpresion(filtro);
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
            return salida;
        }

        /// <summary>
        /// Obtiene una lista de salidas de producto
        /// </summary>
        /// <param name="almacenesMovimiento"></param>
        /// <returns></returns>
        public IEnumerable<SalidaProductoInfo> ObtenerSalidasProductioConciliacionPorAlmacenMovimientoXML(List<AlmacenMovimientoInfo> almacenesMovimiento)
        {
            IEnumerable<SalidaProductoInfo> salidasProducto;
            try
            {
                Logger.Info();
                var entradaProductoBL = new SalidaProductoBL();
                salidasProducto =
                    entradaProductoBL.ObtenerSalidasProductioConciliacionPorAlmacenMovimientoXML(almacenesMovimiento);
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
            return salidasProducto;
        }
    }
}
