using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class AlmacenInventarioLotePL
    {
        /// <summary>
        /// Crea un registro en almacen inventario lote id
        /// </summary>
        /// <param name="almacenInventarioLoteInfo"></param>
        /// <param name="almacenInventarioInfo"></param>
        /// <returns></returns>
        public int Crear(AlmacenInventarioLoteInfo almacenInventarioLoteInfo, AlmacenInventarioInfo almacenInventarioInfo)
        {
            try
            {
                Logger.Info();
                var almcenInventarioLoteBl = new AlmacenInventarioLoteBL();
                return almcenInventarioLoteBl.Crear(almacenInventarioLoteInfo, almacenInventarioInfo);
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
        /// Obtiene el almacen en base al id
        /// </summary>
        /// <param name="almacenInventarioLoteId"></param>
        /// <returns></returns>
        public AlmacenInventarioLoteInfo ObtenerAlmacenInventarioLotePorId(int almacenInventarioLoteId)
        {
            AlmacenInventarioLoteInfo almacenInventarioLote = null;

            try
            {
                var almacenDAL = new AlmacenInventarioLoteBL();
                almacenInventarioLote = almacenDAL.ObtenerAlmacenInventarioLotePorId(almacenInventarioLoteId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return almacenInventarioLote;
        }

        /// <summary>
        /// Obtiene el almacen en base al folio
        /// </summary>
        /// <param name="almacenInventarioLote"></param>
        /// <returns></returns>
        public AlmacenInventarioLoteInfo ObtenerAlmacenInventarioLotePorFolio(AlmacenInventarioLoteInfo almacenInventarioLote)
        {
            try
            {
                var almacenDAL = new AlmacenInventarioLoteBL();
                almacenInventarioLote = almacenDAL.ObtenerAlmacenInventarioLotePorLote(almacenInventarioLote);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return almacenInventarioLote;
        }

        /// <summary>
        /// Obtiene los almacenes en base a la organizacion, el tipo de almacen y el producto
        /// </summary>
        /// <param name="datosLotes"></param>
        /// <returns></returns>
        public List<AlmacenInventarioLoteInfo> ObtenerListadoLotesPorOrganizacionTipoAlmacenProducto(ParametrosOrganizacionTipoAlmacenProductoActivo datosLotes)
        {
            List<AlmacenInventarioLoteInfo> almacenInventarioLote = null;

            try
            {
                var almacenBl = new AlmacenInventarioLoteBL();
                almacenInventarioLote = almacenBl.ObtenerListadoLotesPorOrganizacionTipoAlmacenProducto(datosLotes);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return almacenInventarioLote;
        }

        /// <summary>
        /// Obtiene el almacen en base al lote,organizacion, el tipo de almacen y el producto
        /// </summary>
        /// <param name="lote"></param>
        /// <param name="datosLotes"></param>
        /// <returns></returns>
        public AlmacenInventarioLoteInfo ObtenerPorLoteOrganizacionTipoAlmacenProducto(int lote,ParametrosOrganizacionTipoAlmacenProductoActivo datosLotes)
        {
            AlmacenInventarioLoteInfo almacenInventarioLote = null;

            try
            {
                var almacenBl = new AlmacenInventarioLoteBL();
                almacenInventarioLote = almacenBl.ObtenerPorLoteTipoAlmacenProducto(lote,datosLotes);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return almacenInventarioLote;
        }


        /// <summary>
        /// Obtiene el almacen en base al lote,organizacion, el tipo de almacen y el producto
        /// </summary>
        /// <param name="almacen"></param>
        /// <param name="producto"></param>
        /// <returns></returns>
        public List<AlmacenInventarioLoteInfo> ObtenerPorAlmacenProducto(AlmacenInfo almacen,ProductoInfo producto)
        {
            List<AlmacenInventarioLoteInfo> listaLotes = null;

            try
            {
                var almacenBl = new AlmacenInventarioLoteBL();
                listaLotes = almacenBl.ObtenerPorAlmacenProducto(almacen, producto);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaLotes;
        }

        /// <summary>
        /// Obtiene una lista de almaceninventariolote por paginado
        /// </summary>
        /// <param name="almacenInventarioLote"></param>
        /// <returns></returns>
        public ResultadoInfo<AlmacenInventarioLoteInfo> ObtenerPorOrganizacionTipoAlmacenProductoFamiliaPaginado(PaginacionInfo pagina,AlmacenInventarioLoteInfo almacenInventarioLote)
        {
            ResultadoInfo<AlmacenInventarioLoteInfo> result;
            try
            {
                Logger.Info();
                var almacenInventarioLoteBl = new AlmacenInventarioLoteBL();
                result = almacenInventarioLoteBl.ObtenerPorOrganizacionTipoAlmacenProductoFamiliaPaginado(pagina, almacenInventarioLote);
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

        public AlmacenInventarioLoteInfo CrearLotePorOrganizacionTipoAlmacenProducto(ParametrosOrganizacionTipoAlmacenProductoActivo parametro)
        {
            AlmacenInventarioLoteInfo almacenInventarioLote = null;

            try
            {
                var almacenBl = new AlmacenInventarioLoteBL();
                almacenInventarioLote = almacenBl.CrearLotePorOrganizacionTipoAlmacenProducto(parametro);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return almacenInventarioLote;
        }

        /// <summary>
        /// Obtiene una lista de almaceninventariolote por producto,almacen donde esten activos y tengan movimientos
        /// </summary>
        /// <param name="almacenInfo"></param>
        /// <param name="productoInfo"></param>
        /// <returns></returns>
        public List<AlmacenInventarioLoteInfo> ObtenerPorAlmacenProductoConMovimientos(AlmacenInfo almacenInfo, ProductoInfo productoInfo)
        {
            List<AlmacenInventarioLoteInfo> listaLotes = null;

            try
            {
                var almacenBl = new AlmacenInventarioLoteBL();
                listaLotes = almacenBl.ObtenerPorAlmacenProductoConMovimientos(almacenInfo, productoInfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaLotes;
        }
        /// <summary>
        /// Obtiene una lista paginada de almaceninventariolote por producto organizacion tipo almacen y que tenga cantidad mayor a cero
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="almacenInventarioLote"></param>
        /// <returns></returns>
        public ResultadoInfo<AlmacenInventarioLoteInfo> ObtenerPorOrganizacionTipoAlmacenProductoFamiliaCantidadPaginado(PaginacionInfo pagina, AlmacenInventarioLoteInfo almacenInventarioLote)
        {
            ResultadoInfo<AlmacenInventarioLoteInfo> result;
            try
            {
                Logger.Info();
                var almacenInventarioLoteBl = new AlmacenInventarioLoteBL();
                result = almacenInventarioLoteBl.ObtenerPorOrganizacionTipoAlmacenProductoFamiliaCantidadPaginado(pagina, almacenInventarioLote);
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
        /// Obtiene una lista de Almacen Inventario Lote
        /// </summary>
        /// <param name="filtroAyudaLote"></param>
        /// <returns></returns>
        public IList<AlmacenInventarioLoteInfo> ObtenerAlmacenInventarioLotePorLote(FiltroAyudaLotes filtroAyudaLote)
        {
            IList<AlmacenInventarioLoteInfo> result;
            try
            {
                Logger.Info();
                var almacenInventarioLoteBl = new AlmacenInventarioLoteBL();
                result = almacenInventarioLoteBl.ObtenerAlmacenInventarioLotePorLote(filtroAyudaLote);
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
        /// Obtiene el almacen en base al folio
        /// </summary>
        /// <param name="almacenInventarioLote"></param>
        /// <returns></returns>
        public AlmacenInventarioLoteInfo ObtenerAlmacenInventarioLotePorFolioLote(AlmacenInventarioLoteInfo almacenInventarioLote)
        {
            try
            {
                var almacenDAL = new AlmacenInventarioLoteBL();
                almacenInventarioLote = almacenDAL.ObtenerAlmacenInventarioLotePorFolioLote(almacenInventarioLote);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return almacenInventarioLote;
        }

        /// <summary>
        /// Obtiene una lista de almaceninventariolote por producto,almacen donde esten activos y tengan movimientos
        /// </summary>
        /// <param name="almacenInfo"></param>
        /// <param name="productoInfo"></param>
        /// <returns></returns>
        public List<AlmacenInventarioLoteInfo> ObtenerPorAlmacenProductoEnCeros(AlmacenInfo almacenInfo, ProductoInfo productoInfo)
        {
            List<AlmacenInventarioLoteInfo> listaLotes = null;

            try
            {
                var almacenBl = new AlmacenInventarioLoteBL();
                listaLotes = almacenBl.ObtenerPorAlmacenProductoEnCeros(almacenInfo, productoInfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaLotes;
        }
        /// <summary>
        /// Obtiene una lista lotes para verificacion de lote en uso.
        /// </summary>
        /// <param name="datosLote"></param>
        /// <returns></returns>
        public IList<AlmacenInventarioLoteInfo> ObtenerLotesUso(AlmacenInventarioLoteInfo datosLote)
        {
            IList<AlmacenInventarioLoteInfo> listaLotes = null;

            try
            {
                var almacenBl = new AlmacenInventarioLoteBL();
                listaLotes = almacenBl.ObtenerLotesUso(datosLote);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaLotes;
        }
        /// <summary>
        /// Guardar autorizacion
        /// </summary>
        /// <param name="autorizacionMateriaPrimaInfo"></param>
        public void GuardarAutorizacionMateriaPrima(AutorizacionMateriaPrimaInfo autorizacionMateriaPrimaInfo)
        {
            try
            {
                Logger.Info();
                var almacenInventarioLoteBl = new AlmacenInventarioLoteBL();
                almacenInventarioLoteBl.GuardarAutorizacionMateriaPrima(autorizacionMateriaPrimaInfo);
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
        /// 
        /// </summary>
        /// <param name="autorizacionMateriaPrimaInfo"></param>
        /// <param name="programacionMateriaPrimaInfo"></param>
        public void GuardarAutorizacionMateriaPrimaProgramacion(AutorizacionMateriaPrimaInfo autorizacionMateriaPrimaInfo, ProgramacionMateriaPrimaInfo programacionMateriaPrimaInfo)
        {
            try
            {
                Logger.Info();
                var almacenInventarioLoteBl = new AlmacenInventarioLoteBL();
                almacenInventarioLoteBl.GuardarAutorizacionMateriaPrimaProgramacion(autorizacionMateriaPrimaInfo, programacionMateriaPrimaInfo);
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
        /// Obtiene el almacen en base al folio
        /// </summary>
        /// <param name="contrato"></param>
        /// <returns></returns>
        public AlmacenInventarioLoteInfo ObtenerAlmacenInventarioLotePorContratoID(ContratoInfo contrato)
        {
            AlmacenInventarioLoteInfo almacenInventarioLote = null;
            try
            {
                var almacenDAL = new AlmacenInventarioLoteBL();
                almacenInventarioLote = almacenDAL.ObtenerAlmacenInventarioLotePorContratoID(contrato);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return almacenInventarioLote;
        }

        /// <summary>
        /// Obtiene el almacen en base al folio
        /// </summary>
        /// <param name="almacenInventarioLote"></param>
        /// <returns></returns>
        public AlmacenInventarioLoteInfo ObtenerAlmacenInventarioLoteAlmacenCodigo(AlmacenInventarioLoteInfo almacenInventarioLote)
        {
            try
            {
                var almacenDAL = new AlmacenInventarioLoteBL();
                almacenInventarioLote = almacenDAL.ObtenerAlmacenInventarioLoteAlmacenCodigo(almacenInventarioLote);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return almacenInventarioLote;
        }

        /// <summary>
        /// Obtiene una lista paginada de almaceninventariolote por producto organizacion tipo almacen y que tenga cantidad mayor a cero
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="almacenInventarioLote"></param>
        /// <returns></returns>
        public ResultadoInfo<AlmacenInventarioLoteInfo> ObtenerAlmacenInventarioLoteAlmacenPaginado(PaginacionInfo pagina, AlmacenInventarioLoteInfo almacenInventarioLote)
        {
            ResultadoInfo<AlmacenInventarioLoteInfo> result;
            try
            {
                Logger.Info();
                var almacenInventarioLoteBl = new AlmacenInventarioLoteBL();
                result = almacenInventarioLoteBl.ObtenerAlmacenInventarioLoteAlmacenPaginado(pagina, almacenInventarioLote);
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
        /// Se obtiene lotes 
        /// </summary>
        /// <param name="almacenesInventario"></param>
        /// <returns></returns>
        public IList<AlmacenInventarioLoteInfo> ObtenerLotesPorAlmacenInventarioXML(List<AlmacenInventarioInfo> almacenesInventario)
        {
            IList<AlmacenInventarioLoteInfo> almacenInventarioLote;

            try
            {
                Logger.Info();
                var almacenBl = new AlmacenInventarioLoteBL();
                almacenInventarioLote = almacenBl.ObtenerLotesPorAlmacenInventarioXML(almacenesInventario);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return almacenInventarioLote;
        }

        /// <summary>
        /// Obtiene una lista de almaceninventariolote
        /// </summary>
        /// <param name="almacenInventario"></param>
        /// <returns></returns>
        public List<AlmacenInventarioLoteInfo> ObtenerPorAlmacenInventarioID(AlmacenInventarioInfo almacenInventario)
        {
            List<AlmacenInventarioLoteInfo> almacenInventarioLote;
            try
            {
                Logger.Info();
                var almacenBl = new AlmacenInventarioLoteBL();
                almacenInventarioLote = almacenBl.ObtenerPorAlmacenInventarioID(almacenInventario);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return almacenInventarioLote;
        }
    }
}
