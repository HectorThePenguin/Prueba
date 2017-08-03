using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    public class AlmacenInventarioLoteDAL:DALBase
    {
        /// <summary>
        /// Obtiene el almacen inventario lote indicado
        /// </summary>
        /// <returns></returns>
        internal AlmacenInventarioLoteInfo ObtenerAlmacenInventarioLotePorId(int almacenInventarioLoteId)
        {
            AlmacenInventarioLoteInfo almacen = null;
            try
            {
                Dictionary<string, object> parametros = AuxAlmacenInventarioLoteDAL.ObtenerParametrosObtenerAlmacenInventarioLotePorId(almacenInventarioLoteId);
                DataSet ds = Retrieve("AlmacenInventarioLote_ObtenerAlmacenPorId", parametros);
                if (ValidateDataSet(ds))
                {
                    almacen = MapAlmacenInventarioLoteDAL.ObtenerAlmacenInventarioLoteId(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return almacen;
        }

        /// <summary>
        /// Crea un registro en almacen inventario lote id
        /// </summary>
        /// <param name="almacenInventarioLoteInfo"></param>
        /// <param name="almacenInventarioInfo"></param>
        /// <returns></returns>
        internal int Crear(AlmacenInventarioLoteInfo almacenInventarioLoteInfo, AlmacenInventarioInfo almacenInventarioInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenInventarioLoteDAL.ObtenerParametrosCrear(almacenInventarioLoteInfo, almacenInventarioInfo);
                int result = Create("AlmacenInventarioLote_Crear", parameters);
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
        /// Obtiene los almacenes en base a la organizacion, el tipo de almacen y el producto
        /// </summary>
        /// <returns></returns>
        internal List<AlmacenInventarioLoteInfo> ObtenerListadoLotesPorOrganizacionTipoAlmacenProducto(ParametrosOrganizacionTipoAlmacenProductoActivo datosLotes)
        {
            List<AlmacenInventarioLoteInfo> almacen = null;
            try
            {
                Dictionary<string, object> parametros = AuxAlmacenInventarioLoteDAL.ObtenerParametroObtenerListadoLotesPorOrganizacionTipoAlmacenProducto(datosLotes);
                DataSet ds = Retrieve("AlmacenInventarioLote_ObtenerLotesPorOrganizacionProductoTipoAlmacen", parametros);
                if (ValidateDataSet(ds))
                {
                    almacen = MapAlmacenInventarioLoteDAL.ObtenerListadoAlmacenInventarioLoteTipoAlmacen(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return almacen;
        }

        /// <summary>
        /// Actualiza un registro almacen inventario lote
        /// </summary>
        /// <returns></returns>
        internal void Actualizar(AlmacenInventarioLoteInfo almacenInventarioLote)
        {
            try
            {
                Dictionary<string, object> parametros = AuxAlmacenInventarioLoteDAL.ObtenerParametrosActualizar(almacenInventarioLote);
                Update("AlmacenInventarioLote_Actualizar", parametros);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene la lista de almaceninventariolote
        /// </summary>
        /// <param name="almacenInventario"></param>
        /// <returns></returns>
        internal List<AlmacenInventarioLoteInfo> ObtenerPorAlmacenInventarioID(AlmacenInventarioInfo almacenInventario)
        {
            List<AlmacenInventarioLoteInfo> almacen = null;
            try
            {
                Dictionary<string, object> parametros = AuxAlmacenInventarioLoteDAL.ObtenerParametroObtenerPorAlmacenInventarioID(almacenInventario);
                DataSet ds = Retrieve("AlmacenInventarioLote_ObtenerPorAlmacenInventarioID", parametros);
                if (ValidateDataSet(ds))
                {
                    almacen = MapAlmacenInventarioLoteDAL.ObtenerListadoAlmacenInventarioLote(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return almacen;
        }


        /// <summary>
        /// Obtiene los lotes en los que se encuentra un producto.
        /// </summary>
        /// <param name="almacen"></param>
        /// <param name="producto"></param>
        /// <returns></returns>
        internal List<AlmacenInventarioLoteInfo> ObtenerPorAlmacenProducto(AlmacenInfo almacen,ProductoInfo producto)
        {
            List<AlmacenInventarioLoteInfo> lotes = null;
            try
            {
                Dictionary<string, object> parametros = AuxAlmacenInventarioLoteDAL.ObtenerParametrosObtenerPorAlmacenProducto(almacen,producto);
                DataSet ds = Retrieve("AlmacenInventarioLote_ObtenerPorAlmacenProducto", parametros);
                if (ValidateDataSet(ds))
                {
                    lotes = MapAlmacenInventarioLoteDAL.ObtenerListadoAlmacenInventarioLote(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lotes;
        }


        /// <summary>
        /// Actualiza los datos del lote despues de haber guardado la entrada del producto.
        /// </summary>
        /// <returns></returns>
        internal void DescontarAlmacenInventarioLoteProduccionDiaria(ProduccionDiariaInfo produccionDiaria)
        {
            try
            {
                Dictionary<string, object> parametros = AuxAlmacenInventarioLoteDAL.ObtenerParametrosDescontarAlmacenInventarioLoteProduccionDiaria(produccionDiaria);
                Update("AlmacenInventarioLote_DescontarProduccionDiaria", parametros);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de almaceninventariolote
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="almacenInventarioLote"></param>
        /// <returns></returns>
        internal ResultadoInfo<AlmacenInventarioLoteInfo> ObtenerPorOrganizacionTipoAlmacenProductoFamiliaPaginado(PaginacionInfo pagina, AlmacenInventarioLoteInfo almacenInventarioLote)
        {
            try
            {
                Dictionary<string, object> parameters = AuxAlmacenInventarioLoteDAL.ObtenerParametrosObtenerPorOrganizacionTipoAlmacenProductoFamiliaPaginado(pagina, almacenInventarioLote);
                DataSet ds = Retrieve("[dbo].[AlmacenInventarioLote_ObtenerLotesPorOrganizacionProductoTipoAlmacenPaginado]", parameters);
                ResultadoInfo<AlmacenInventarioLoteInfo> lista = null;
                if (ValidateDataSet(ds))
                {
                    lista = MapAlmacenInventarioLoteDAL.ObtenerPorOrganizacionTipoAlmacenProductoFamiliaPaginado(ds);
                }
                return lista;
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
        /// Actualiza un registro almacen inventario lote
        /// </summary>
        /// <returns></returns>
        internal void DesactivarLote(AlmacenInventarioLoteInfo almacenInventarioLote)
        {
            try
            {
                Dictionary<string, object> parametros = AuxAlmacenInventarioLoteDAL.ObtenerParametrosDesactivarLote(almacenInventarioLote);
                Update("AlmacenInventarioLote_DesactivarLote", parametros);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un almaceninventariolote por lote
        /// </summary>
        /// <param name="almacenInventarioLote"></param>
        /// <returns></returns>
        internal AlmacenInventarioLoteInfo ObtenerAlmacenInventarioLotePorLote(AlmacenInventarioLoteInfo almacenInventarioLote)
        {
            try
            {
                Dictionary<string, object> parametros = AuxAlmacenInventarioLoteDAL.ObtenerParametroObtenerAlmacenInventarioLotePorLote(almacenInventarioLote);
                DataSet ds = Retrieve("AlmacenInventarioLote_ObtenerPorLote", parametros);
                almacenInventarioLote = null;
                if (ValidateDataSet(ds))
                {
                    almacenInventarioLote = MapAlmacenInventarioLoteDAL.ObtenerAlmacenInventarioLotePorLote(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return almacenInventarioLote;
        }

        /// <summary>
        /// Actualiza los datos del lote despues de haber guardado la entrada del producto.
        /// </summary>
        /// <returns></returns>
        internal void AjustarAlmacenInventarioLote(List<AlmacenInventarioLoteInfo> listaAlmacenInventarioLote)
        {
            try
            {
                Dictionary<string, object> parametros = AuxAlmacenInventarioLoteDAL.ObtenerParametrosAjustarAlmacenInventarioLote(listaAlmacenInventarioLote);
                Update("AlmacenInventarioLote_DescontarCierreDiaInventario", parametros);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de almaceninventariolote donde esten activos y tengan movimientos
        /// </summary>
        /// <param name="almacenInfo"></param>
        /// <param name="productoInfo"></param>
        /// <returns></returns>
        internal List<AlmacenInventarioLoteInfo> ObtenerPorAlmacenProductoConMovimientos(AlmacenInfo almacenInfo, ProductoInfo productoInfo)
        {
            List<AlmacenInventarioLoteInfo> lotes = null;
            try
            {
                Dictionary<string, object> parametros = AuxAlmacenInventarioLoteDAL.ObtenerParametrosObtenerPorAlmacenProducto(almacenInfo, productoInfo);
                DataSet ds = Retrieve("AlmacenInventarioLote_ObtenerPorAlmacenProductoConMovimiento", parametros);
                if (ValidateDataSet(ds))
                {
                    lotes = MapAlmacenInventarioLoteDAL.ObtenerListadoAlmacenInventarioLote(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lotes;
        }
        /// <summary>
        /// Obtiene una lista paginada de almaceninventariolote por producto organizacion tipo almacen y que tenga cantidad mayor a cero
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="almacenInventarioLote"></param>
        /// <returns></returns>
        internal ResultadoInfo<AlmacenInventarioLoteInfo> ObtenerPorOrganizacionTipoAlmacenProductoFamiliaCantidadPaginado(PaginacionInfo pagina, AlmacenInventarioLoteInfo almacenInventarioLote)
        {
            try
            {
                Dictionary<string, object> parameters = AuxAlmacenInventarioLoteDAL.ObtenerParametrosObtenerPorOrganizacionTipoAlmacenProductoFamiliaPaginado(pagina, almacenInventarioLote);
                DataSet ds = Retrieve("AlmacenInventarioLote_ObtenerLotesPorOrganizacionProductoTipoAlmacenCantidadPaginado", parameters);
                ResultadoInfo<AlmacenInventarioLoteInfo> lista = null;
                if (ValidateDataSet(ds))
                {
                    lista = MapAlmacenInventarioLoteDAL.ObtenerPorOrganizacionTipoAlmacenProductoFamiliaPaginado(ds);
                }
                return lista;
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
        /// Obtiene una lista de Almacen Inventario Lote
        /// </summary>
        /// <param name="filtroAyudaLote"></param>
        /// <returns></returns>
        internal IList<AlmacenInventarioLoteInfo> ObtenerAlmacenInventarioLotePorLote(FiltroAyudaLotes filtroAyudaLote)
        {
            try
            {
                Dictionary<string, object> parameters =
                    AuxAlmacenInventarioLoteDAL.
                        ObtenerAlmacenInventarioLotePorLote(filtroAyudaLote);
                DataSet ds = Retrieve("AlmacenInventarioLote_ObtenerLotesProducto", parameters);
                IList<AlmacenInventarioLoteInfo> lista = null;
                if (ValidateDataSet(ds))
                {
                    lista = MapAlmacenInventarioLoteDAL.ObtenerAlmacenInventarioPorLote(ds);
                }
                return lista;
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
        /// Obtiene un lote por almacen
        /// </summary>
        /// <param name="almacenInventarioLote"></param>
        /// <returns></returns>
        internal AlmacenInventarioLoteInfo ObtenerAlmacenInventarioLotePorFolioLote(AlmacenInventarioLoteInfo almacenInventarioLote)
        {
            try
            {
                Dictionary<string, object> parametros = AuxAlmacenInventarioLoteDAL.ObtenerParametroObtenerAlmacenInventarioLotePorFolioLote(almacenInventarioLote);
                DataSet ds = Retrieve("AlmacenInventarioLote_ObtenerPorFolioLote", parametros);
                almacenInventarioLote = null;
                if (ValidateDataSet(ds))
                {
                    almacenInventarioLote = MapAlmacenInventarioLoteDAL.ObtenerAlmacenInventarioLotePorLote(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return almacenInventarioLote;
        }

        /// <summary>
        /// Obtiene una lista de almaceninventariolote donde esten activos y tengan movimientos
        /// </summary>
        /// <param name="almacenInfo"></param>
        /// <param name="productoInfo"></param>
        /// <returns></returns>
        internal List<AlmacenInventarioLoteInfo> ObtenerPorAlmacenProductoEnCeros(AlmacenInfo almacenInfo, ProductoInfo productoInfo)
        {
            List<AlmacenInventarioLoteInfo> lotes = null;
            try
            {
                Dictionary<string, object> parametros = AuxAlmacenInventarioLoteDAL.ObtenerPorAlmacenProductoEnCeros(almacenInfo, productoInfo);
                DataSet ds = Retrieve("AlmacenInventarioLote_ObtenerPorAlmacenProductoEnCeros", parametros);
                if (ValidateDataSet(ds))
                {
                    lotes = MapAlmacenInventarioLoteDAL.ObtenerPorAlmacenProductoEnCeros(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lotes;
        }
        /// <summary>
        /// Se obtiene lotes 
        /// </summary>
        /// <param name="datosLote"></param>
        /// <returns></returns>
        internal IList<AlmacenInventarioLoteInfo> ObtenerLotesUso(AlmacenInventarioLoteInfo datosLote)
        {
            try
            {
                Dictionary<string, object> parameters =
                    AuxAlmacenInventarioLoteDAL.
                        ObtenerParametrosObtenerLotesUso(datosLote);
                DataSet ds = Retrieve("AlmacenInventarioLote_ObtenerLotesUso", parameters);
                IList<AlmacenInventarioLoteInfo> lista = null;
                if (ValidateDataSet(ds))
                {
                    lista = MapAlmacenInventarioLoteDAL.ObtenerListadoAlmacenInventarioLote(ds);
                }
                return lista;
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
        /// Guardar autorizacion materia prima
        /// </summary>
        /// <param name="autorizacionMateriaPrimaInfo"></param>
        internal void GuardarAutorizacionMateriaPrima(AutorizacionMateriaPrimaInfo autorizacionMateriaPrimaInfo)
        {
            
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxDiferenciasDeInventarioDAL.ObtenerParametrosAutorizacionMateriaPrima(autorizacionMateriaPrimaInfo);
                Create("AutorizacionMateriaPrima_Registro", parameters);
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


        internal int GuardarAutorizacionMateriaPrimaProgramacion(AutorizacionMateriaPrimaInfo autorizacionMateriaPrimaInfo, ProgramacionMateriaPrimaInfo programacionMateriaPrimaInfo)
        {

            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxDiferenciasDeInventarioDAL.ObtenerParametrosAutorizacionMateriaPrima(autorizacionMateriaPrimaInfo);
                var ds = Retrieve("AutorizacionMateriaPrima_Registro", parameters);
                int result = 0;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenInventarioDAL.ObtenerAutorizacionMateriaPrimaID(ds);
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
        /// Obtiene un lote por almacen
        /// </summary>
        /// <param name="contrato"></param>
        /// <returns></returns>
        internal AlmacenInventarioLoteInfo ObtenerAlmacenInventarioLotePorContratoID(ContratoInfo contrato)
        {
            AlmacenInventarioLoteInfo almacenInventarioLote = null;
            try
            {
                Dictionary<string, object> parametros = AuxAlmacenInventarioLoteDAL.ObtenerParametroObtenerAlmacenInventarioLotePorContratoID(contrato);
                DataSet ds = Retrieve("AlmacenInventarioLote_ObtenerPorContratoID", parametros);
                if (ValidateDataSet(ds))
                {
                    almacenInventarioLote = MapAlmacenInventarioLoteDAL.ObtenerAlmacenInventarioLoteContratoID(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return almacenInventarioLote;
        }

        public void GuardarProgramacionMateria(ProgramacionMateriaPrimaInfo programacionMateriaPrimaInfo, int autorizacionMateriaPrimaID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxDiferenciasDeInventarioDAL.ObtenerParametrosGuardarProgramacionMateria(programacionMateriaPrimaInfo, autorizacionMateriaPrimaID);
                Create("AutorizacionMateriaPrima_RegistroProgramacionMateriaPrima", parameters);
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
        /// Obtiene un almaceninventariolote por lote
        /// </summary>
        /// <param name="almacenInventarioLote"></param>
        /// <returns></returns>
        internal AlmacenInventarioLoteInfo ObtenerAlmacenInventarioLoteAlmacenCodigo(AlmacenInventarioLoteInfo almacenInventarioLote)
        {
            try
            {
                Dictionary<string, object> parametros = AuxAlmacenInventarioLoteDAL.ObtenerParametroObtenerAlmacenInventarioLoteAlmacenCodigo(almacenInventarioLote);
                DataSet ds = Retrieve("AlmacenInventarioLote_ObtenerPorAlmacenLote", parametros);
                almacenInventarioLote = null;
                if (ValidateDataSet(ds))
                {
                    almacenInventarioLote = MapAlmacenInventarioLoteDAL.ObtenerAlmacenInventarioLotePorLote(ds);
                }
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
        internal ResultadoInfo<AlmacenInventarioLoteInfo> ObtenerAlmacenInventarioLoteAlmacenPaginado(PaginacionInfo pagina, AlmacenInventarioLoteInfo almacenInventarioLote)
        {
            try
            {
                Dictionary<string, object> parameters = AuxAlmacenInventarioLoteDAL.ObtenerParametrosAlmacenInventarioLoteAlmacenPaginado(pagina, almacenInventarioLote);
                DataSet ds = Retrieve("AlmacenInventarioLote_ObtenerLotesPorAlmacen", parameters);
                ResultadoInfo<AlmacenInventarioLoteInfo> lista = null;
                if (ValidateDataSet(ds))
                {
                    lista = MapAlmacenInventarioLoteDAL.ObtenerAlmacenInventarioLoteAlmacenPaginado(ds);
                }
                return lista;
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
        /// Se obtiene lotes 
        /// </summary>
        /// <param name="almacenesInventario"></param>
        /// <returns></returns>
        internal IList<AlmacenInventarioLoteInfo> ObtenerLotesPorAlmacenInventarioXML(List<AlmacenInventarioInfo> almacenesInventario)
        {
            try
            {
                Dictionary<string, object> parameters =
                    AuxAlmacenInventarioLoteDAL.
                        ObtenerParametrosLotesPorAlmacenInventarioXML(almacenesInventario);
                DataSet ds = Retrieve("AlmacenInventarioLote_ObtenerPorAlmacenInventarioXML", parameters);
                IList<AlmacenInventarioLoteInfo> lista = null;
                if (ValidateDataSet(ds))
                {
                    lista = MapAlmacenInventarioLoteDAL.ObtenerLotesPorAlmacenInventarioXML(ds);
                }
                return lista;
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
        /// Crea un registro en almacen inventario lote, mandandole todos los parametros sin afectar la tabla Folio Lote Producto
        /// </summary>
        /// <param name="almacenInventarioLoteInfo"></param>
        /// <returns></returns>
        internal int CrearConTodosParametros(AlmacenInventarioLoteInfo almacenInventarioLoteInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenInventarioLoteDAL.ObtenerParametrosCrearConTodosParametros(almacenInventarioLoteInfo);
                int result = Create("AlmacenInventarioLote_CrearTodosParametros", parameters);
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
        ///  Actualiza la infomracion del inventario - lote cuando se genera un envio de alimento
        /// </summary>
        /// <param name="almacenInventarioLoteInfo">Información del almácen lote - inventari</param>
        internal void ActualizarInventarioLoteEnvioMercancia(AlmacenInventarioLoteInfo almacenInventarioLoteInfo)
        {
            try{
                Dictionary<string, object> parametros = AuxAlmacenInventarioLoteDAL.ObtenerParametrosActualizarEnvioAlimento(almacenInventarioLoteInfo);
                Update("AlmacenInventarioLote_ActualizarEnvioAlimento", parametros);
            }
            catch (Exception ex){
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }


}
