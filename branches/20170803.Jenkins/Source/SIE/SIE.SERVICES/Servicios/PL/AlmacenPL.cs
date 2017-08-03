using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class AlmacenPL
    {

        /// <summary>
        ///      Obtiene un Almacen por su Id
        /// </summary>
        /// <returns> </returns>
        public AlmacenInfo ObtenerPorID(int almacenID)
        {
            AlmacenInfo info;
            try
            {
                Logger.Info();
                var almacenBL = new AlmacenBL();
                info = almacenBL.ObtenerPorID(almacenID);
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
            return info;
        }

        /// <summary>
        /// Funcion para almacenar los tratamientos en almacenMovimiento y Descontarlos del inventario
        /// </summary>
        /// <param name="listaTratamientos"></param>
        /// <param name="almacenMovimientoInfo"></param>
        public int GuardarDescontarTratamientos(List<TratamientoInfo> listaTratamientos,
                                                AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            int info;
            try
            {
                Logger.Info();
                var almacenBL = new AlmacenBL();
                info = almacenBL.GuardarDescontarTratamientos(listaTratamientos, almacenMovimientoInfo);
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
            return info;
        }

        /// <summary>
        /// Validar que no queden ajustes pendientes por aplicar para el almacen(Diferencias de inventario)
        /// </summary>
        /// <param name="almacenMovimientoInfo"></param>
        /// <returns></returns>
        public bool ExistenAjustesPendientesParaAlmacen(AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            bool info;
            try
            {
                Logger.Info();
                var almacenBL = new AlmacenBL();
                info = almacenBL.ExistenAjustesPendientesParaAlmacen(almacenMovimientoInfo);
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
            return info;
        }

        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Almacen
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(AlmacenInfo info)
        {
            try
            {
                Logger.Info();
                var almacenBL = new AlmacenBL();
                int result = almacenBL.Guardar(info);
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
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<AlmacenInfo> ObtenerPorPagina(PaginacionInfo pagina, AlmacenInfo filtro)
        {
            try
            {
                Logger.Info();
                var almacenBL = new AlmacenBL();
                ResultadoInfo<AlmacenInfo> result = almacenBL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una lista
        /// </summary>
        /// <returns></returns>
        public IList<AlmacenInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var almacenBL = new AlmacenBL();
                IList<AlmacenInfo> result = almacenBL.ObtenerTodos();
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
        ///  Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<AlmacenInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var almacenBL = new AlmacenBL();
                IList<AlmacenInfo> result = almacenBL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public AlmacenInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var almacenBL = new AlmacenBL();
                AlmacenInfo result = almacenBL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene una entidad por su descripción
        /// </summary>
        /// <param name="almacenMovimientoInfo"></param>
        /// <returns></returns>
        public AlmacenMovimientoInfo ObtenerAlmacenMovimiento(AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            try
            {
                Logger.Info();
                var almacenBL = new AlmacenBL();
                AlmacenMovimientoInfo result = almacenBL.ObtenerAlmacenMovimiento(almacenMovimientoInfo);
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
        /// Obtiene los almacenes por organizacion
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public IList<AlmacenInfo> ObtenerAlmacenPorOrganizacion(int organizacionId)
        {
            try
            {
                Logger.Info();
                var almacenBL = new AlmacenBL();
                IList<AlmacenInfo> result = almacenBL.ObtenerAlmacenPorOrganizacion(organizacionId);
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
        /// ObtenerDatosAlmacenInventario
        /// </summary>
        /// <param name="cierreInventarioInfo"> </param>
        /// <returns></returns>
        public AlmacenCierreDiaInventarioInfo ObtenerDatosAlmacenInventario(
            AlmacenCierreDiaInventarioInfo cierreInventarioInfo)
        {
            try
            {
                Logger.Info();
                var almacenBL = new AlmacenBL();
                AlmacenCierreDiaInventarioInfo result = almacenBL.ObtenerDatosAlmacenInventario(cierreInventarioInfo);
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
        /// Obtener productos del almacen
        /// </summary>
        /// <param name="almacenId"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public IList<AlmacenCierreDiaInventarioInfo> ObtenerProductosAlamcen(int almacenId, int organizacionId)
        {
            try
            {
                Logger.Info();
                var almacenBL = new AlmacenBL();
                IList<AlmacenCierreDiaInventarioInfo> result = almacenBL.ObtenerProductosAlamcen(almacenId,
                                                                                                 organizacionId);
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
        /// obtener lista de almacen  movimiento
        /// </summary>
        /// <param name="almacenMovimientoInfo"></param>
        /// <param name="activo"></param>
        /// <returns></returns>
        public IList<AlmacenMovimientoInfo> ObtenerListaAlmacenMovimiento(AlmacenMovimientoInfo almacenMovimientoInfo,
                                                                          int activo)
        {
            try
            {
                Logger.Info();
                var almacenBL = new AlmacenBL();
                IList<AlmacenMovimientoInfo> result = almacenBL.ObtenerListaAlmacenMovimiento(almacenMovimientoInfo,
                                                                                              activo);
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
        /// Obtiene la lista de movimientos de un almacen
        /// </summary>
        /// <param name="almacen"></param>
        /// <returns></returns>
        public IList<AlmacenMovimientoDetalle> ObtenerAlmacenMovimientoPorAlmacenID(AlmacenInfo almacen)
        {
            try
            {
                Logger.Info();
                var almacenBL = new AlmacenBL();
                IList<AlmacenMovimientoDetalle> result = almacenBL.ObtenerAlmacenMovimientoPorAlmacenID(almacen);
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
        /// Obtiene la lista de movimientos de un almacen
        /// (resc)
        /// </summary>
        /// <param name="contrato"></param>
        /// <returns></returns>
        public IList<AlmacenMovimientoDetalle> ObtenerAlmacenMovimientoPorContrato(ContratoInfo contrato)
        {
            try
            {
                Logger.Info();
                var almacenBL = new AlmacenBL();
                IList<AlmacenMovimientoDetalle> result = almacenBL.ObtenerAlmacenMovimientoPorContrato(contrato);
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
        /// Obtiene los almacenes por organizacion y tipo de almacen
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        public AlmacenInventarioInfo ObtenerAlmacenInventarioPorOrganizacionTipoAlmacen(
            ParametrosOrganizacionTipoAlmacenProductoActivo datos)
        {
            try
            {
                Logger.Info();
                var almacenBl = new AlmacenBL();
                return almacenBl.ObtenerAlmacenInventarioPorOrganizacionTipoAlmacen(datos);
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
        /// Obtiene los almacenes por organizacion y tipo de almacen
        /// </summary>
        /// <param name="organizacionId"> </param>
        /// <returns></returns>
        public AlmacenInfo ObtenerPorOrganizacionId(int organizacionId)
        {
            try
            {
                Logger.Info();
                var almacenBl = new AlmacenBL();
                return almacenBl.ObtenerPorOrganizacionId(organizacionId);
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
        /// Obtiene todos los almacenes por los tipo de almacen y organizacion.
        /// </summary>
        /// <param name="tiposAlmacen"></param>
        /// <param name="organizacion"></param>
        /// <returns></returns>
        public List<AlmacenInfo> ObtenerAlmacenPorTiposAlmacen(List<TipoAlmacenEnum> tiposAlmacen,
                                                               OrganizacionInfo organizacion)
        {
            List<AlmacenInfo> almacenes = null;
            try
            {
                if (tiposAlmacen != null && tiposAlmacen.Count > 0)
                {
                    Logger.Info();
                    var almacenBl = new AlmacenBL();
                    almacenes = almacenBl.ObtenerAlmacenPorTiposAlmacen(tiposAlmacen, organizacion);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return almacenes;
        }

        /// <summary>
        /// Obtiene el valor del folio para ese almacen
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public int ObtenerFolioAlmacenConsulta(FiltroCierreDiaInventarioInfo filtros)
        {
            try
            {
                Logger.Info();
                var almacenBl = new AlmacenBL();
                return almacenBl.ObtenerFolioAlmacenConsulta(filtros);
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
        /// Obtiene el valor del folio para ese almacen
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public List<AlmacenesCierreDiaInventarioPAModel> ObtenerAlmacenesOrganizacion(int organizacionID)
        {
            try
            {
                Logger.Info();
                var almacenBl = new AlmacenBL();
                return almacenBl.ObtenerAlmacenesOrganizacion(organizacionID);
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
        /// Obtiene un almacen por su clave
        /// </summary>
        /// <param name="almacen"></param>
        /// <returns></returns>
        public AlmacenInfo ObtenerPorAlmacenPoliza(AlmacenInfo almacen)
        {
            try
            {
                Logger.Info();
                var almacenBL = new AlmacenBL();
                AlmacenInfo result = almacenBL.ObtenerPorAlmacenPoliza(almacen);
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
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<AlmacenInfo> ObtenerPorPaginaPoliza(PaginacionInfo pagina, AlmacenInfo filtro)
        {
            try
            {
                Logger.Info();
                var almacenBL = new AlmacenBL();
                ResultadoInfo<AlmacenInfo> result = almacenBL.ObtenerPorPaginaPoliza(pagina, filtro);
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
        /// Obtiene un lista paginada
        /// filtrando por varios tipos de almacen.
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<AlmacenInfo> ObtenerPorOrganizacionTipoAlmacen(PaginacionInfo pagina, AlmacenInfo filtro)
        {
            try
            {
                Logger.Info();
                var almacenBL = new AlmacenBL();
                ResultadoInfo<AlmacenInfo> result = almacenBL.ObtenerPorOrganizacionTipoAlmacen(pagina, filtro);
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
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<AlmacenInfo> ObtenerPorPaginaTipoAlmacen(PaginacionInfo pagina, AlmacenInfo filtro)
        {
            try
            {
                Logger.Info();
                var almacenBL = new AlmacenBL();
                ResultadoInfo<AlmacenInfo> result = almacenBL.ObtenerPorPaginaTipoAlmacen(pagina, filtro);
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
        /// Obtiene un almacen por id y tipos almacen
        /// </summary>
        /// <returns></returns>
        public AlmacenInfo ObtenerPorIdFiltroTipoAlmacen(AlmacenInfo almacenInfo)
        {
            AlmacenInfo info;
            try
            {
                Logger.Info();
                var almacenBL = new AlmacenBL();
                info = almacenBL.ObtenerPorIdFiltroTipoAlmacen(almacenInfo);
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
            return info;
        }

        /// <summary>
        ///      Obtiene un Almacen por su Id
        /// </summary>
        /// <returns> </returns>
        public AlmacenInfo ObtenerPorID(AlmacenInfo almacen)
        {
            AlmacenInfo info;
            try
            {
                Logger.Info();
                var almacenBL = new AlmacenBL();
                info = almacenBL.ObtenerPorID(almacen.AlmacenID);
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
            return info;
        }

        /// <summary>
        /// Validar que si tiene por lo menos algun producto con existencia en el inventario
        /// </summary>
        /// <param name="almacenInfo"></param>
        /// <returns></returns>
        public bool ValidarProductosEnAlmacen(AlmacenInfo almacenInfo)
        {
            bool info;
            try
            {
                Logger.Info();
                var almacenBL = new AlmacenBL();
                info = almacenBL.ValidarProductosEnAlmacen(almacenInfo);
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
            return info;
        }
        /// <summary>
        ///  Validar si el producto tiene existencias en algun Almacen
        /// </summary>
        /// <param name="productoID"></param>
        /// <returns></returns>
        public bool ValidarExistenciasProductoEnAlmacen(int productoID)
        {
            bool info;
            try
            {
                Logger.Info();
                var almacenBL = new AlmacenBL();
                info = almacenBL.ValidarExistenciasProductoEnAlmacen(productoID);
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
            return info;
        }

        /// <summary>
        /// Obtiene el valor del folio para ese almacen
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public List<AlmacenInfo> ObtenerAlmacenesPorOrganizacion(int organizacionID)
        {
            try
            {
                Logger.Info();
                var almacenBl = new AlmacenBL();
                return almacenBl.ObtenerAlmacenesPorOrganizacion(organizacionID);
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
        ///      Obtiene un Almacen por su Id
        /// </summary>
        /// <returns> </returns>
        public AlmacenInfo ObtenerPorIDOrganizacion(AlmacenInfo almacen)
        {
            AlmacenInfo info;
            try
            {
                Logger.Info();
                var almacenBL = new AlmacenBL();
                info = almacenBL.ObtenerPorIDOrganizacion(almacen);
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
            return info;
        }
    }
}