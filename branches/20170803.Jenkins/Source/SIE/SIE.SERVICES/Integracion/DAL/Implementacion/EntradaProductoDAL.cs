using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using BLToolkit.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class EntradaProductoDAL : DALBase 
    {
        /// <summary>
        /// Obtiene todos las entrada de producto
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal List<EntradaProductoInfo> ObtenerEntradaProductosTodos(int organizacionId)
        {
            List<EntradaProductoInfo> listaEntradaProductos = null;

            try
            {
                Dictionary<string, object> parametros = AuxEntradaProductoDAL.ObtenerParametrosObtenerListaPorOrganizacionId(organizacionId);
                DataSet ds = Retrieve("EntradaProducto_ObtenerTodos", parametros);
                if (ValidateDataSet(ds))
                {
                    listaEntradaProductos = MapEntradaProductoDAL.ObtenerTodos(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaEntradaProductos;
        }

        /// <summary>
        /// Obtiene todos las entrada de producto por organizacion y activo
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="activo"></param>
        /// <returns></returns>
        internal List<EntradaProductoInfo> ObtenerEntradaProductosTodos(int organizacionId, int activo)
        {
            List<EntradaProductoInfo> listaEntradaProductos = null;

            try
            {
                Dictionary<string, object> parametros = AuxEntradaProductoDAL.ObtenerParametrosObtenerListaPorOrganizacionId(organizacionId, activo);
                DataSet ds = Retrieve("EntradaProducto_ObtenerTodos", parametros);
                if (ValidateDataSet(ds))
                {
                    listaEntradaProductos = MapEntradaProductoDAL.ObtenerTodos(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaEntradaProductos;
        }

        /// <summary>
        /// Obtiene todos las entrada de producto
        /// </summary>
        /// <param name="estatusId"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal List<EntradaProductoInfo> ObtenerEntradaProductosTodosPorEstatusId(int estatusId, int organizacionId)
        {
            List<EntradaProductoInfo> listaEntradaProductos = null;

            try
            {
                Dictionary<string, object> parametros = AuxEntradaProductoDAL.ObtenerParametrosObtenerListaPorEstatusId(estatusId, organizacionId);
                DataSet ds = Retrieve("EntradaProducto_ObtenerEntradaEstatus", parametros);
                if (ValidateDataSet(ds))
                {
                    listaEntradaProductos = MapEntradaProductoDAL.ObtenerTodosAyuda(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaEntradaProductos;
        }

        /// <summary>
        /// Actualiza la entrada de producto la primera vez que llega
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        internal bool ActualizarEntradaProductoLlegada(EntradaProductoInfo entradaProducto)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxEntradaProductoDAL.ObtenerParametrosActualizarEntradaProductoLlegada(entradaProducto);
                Create("EntradaProducto_ActualizarEntradaLlegadaPorEntradaId", parameters);
                return true;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return false;
        }

        /// <summary>
        /// Guarda una entrada producto
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <param name="tipoFolio"></param>
        /// <returns></returns>
        internal EntradaProductoInfo GuardarEntradaProducto(EntradaProductoInfo entradaProducto, int tipoFolio)
        {
            EntradaProductoInfo entrada = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxEntradaProductoDAL.ObtenerParametrosGuardarEntradaProducto(entradaProducto,tipoFolio);
                DataSet ds = Retrieve("BoletaRecepcion_EntradaProducto_Crear", parameters);
                if (ValidateDataSet(ds))
                {
                    entrada = MapEntradaProductoDAL.ObtenerPorFolioEntradaMateriaPrima(ds);
                }
                return entrada;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return entrada;
        }

        /// <summary>
        /// Autoriza la entrada de un producto.
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        internal bool AutorizaEntrada(EntradaProductoInfo entradaProducto)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxEntradaProductoDAL.ObtenerParametrosAutorizaEntrada(entradaProducto);
                Update("EntradaProducto_Autorizar", parameters);
                return true;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return false;
        }

        /// <summary>
        /// Obtiene las entradas producto por organizacion y folio para el filtro
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="folio"></param>
        /// <returns></returns>
        internal List<EntradaProductoInfo> ObtenerEntradaProductoValido(int organizacionId, int folio)
        {
            List<EntradaProductoInfo> listaEntradaProductos = null;

            try
            {
                Dictionary<string, object> parametros = AuxEntradaProductoDAL.ObtenerParametrosObtenerEntradaProductoValido(organizacionId, folio);
                DataSet ds = Retrieve("EntradaProducto_ObtenerTodosPorFiltroFolio", parametros);
                if (ValidateDataSet(ds))
                {
                    listaEntradaProductos = MapEntradaProductoDAL.ObtenerTodos(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaEntradaProductos;
        }

        /// <summary>
        ///     Obtiene un lista paginada de folios de entrada producto
        /// </summary>
        /// <param name="pagina">Configuracion de paginacion</param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<EntradaProductoInfo> ObtenerFoliosPorPaginaParaEntradaMateriaPrima(PaginacionInfo pagina, EntradaProductoInfo filtro)
        {
            ResultadoInfo<EntradaProductoInfo> foliosLista = null;
            try
            {
                Dictionary<string, object> parameters =
                    AuxEntradaProductoDAL.ObtenerParametrosFoliosPorPaginaParaEntradaMateriaPrima(pagina, filtro);
                DataSet ds = Retrieve("EntradaProducto_ObtenerFolioPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    foliosLista = MapEntradaProductoDAL.ObtenerFoliosPorPaginaParaEntradaMateriaPrima(ds);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
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
            return foliosLista;
        }

        /// <summary>
        ///     Obtiene un lista paginada de folios de entrada producto
        /// </summary>
        /// <param name="pagina">Configuracion de paginacion</param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<EntradaProductoInfo> ObtenerFoliosPorPaginaParaEntradaMateriaPrimaEstatus(PaginacionInfo pagina, EntradaProductoInfo filtro)
        {
            ResultadoInfo<EntradaProductoInfo> foliosLista = null;
            try
            {
                Dictionary<string, object> parameters =
                    AuxEntradaProductoDAL.ObtenerParametrosFoliosPorPaginaParaEntradaMateriaPrimaEstatus(pagina, filtro);
                DataSet ds = Retrieve("EntradaProducto_ObtenerFolioPorPaginaEstatus", parameters);
                if (ValidateDataSet(ds))
                {
                    foliosLista = MapEntradaProductoDAL.ObtenerFoliosPorPaginaParaEntradaMateriaPrimaEstatus(ds);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
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
            return foliosLista;
        }
        
        /// <summary>
        /// Obtiene la entrada de materia prima por folio
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal EntradaProductoInfo ObtenerPorFolioEntradaMateriaPrima(EntradaProductoInfo filtro)
        {
            EntradaProductoInfo folio = null;
            try
            {
                Dictionary<string, object> parameters = AuxEntradaProductoDAL.ObtenerParametroPorFolioEntradaMateriaPrima(filtro);
                DataSet ds = Retrieve("EntradaProducto_ObtenerPorFolio", parameters);
                if (ValidateDataSet(ds))
                {
                    folio = MapEntradaProductoDAL.ObtenerPorFolioEntradaMateriaPrima(ds);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
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
            return folio;
        }

		/// <summary>
        /// Obtiene la entrada de materia prima por folio
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal EntradaProductoInfo ObtenerPorFolioEntradaCancelacion(EntradaProductoInfo filtro)
        {
            EntradaProductoInfo folio = null;
            try
            {
                Dictionary<string, object> parameters = AuxEntradaProductoDAL.ObtenerParametroPorFolioEntradaMateriaPrima(filtro);
                DataSet ds = Retrieve("EntradaProducto_ObtenerPorFolioCancelacion", parameters);
                if (ValidateDataSet(ds))
                {
                    folio = MapEntradaProductoDAL.ObtenerPorFolioEntradaMateriaPrima(ds);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
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
            return folio;
        }

        /// <summary>
        /// Metodo para actualizar el lote en patio y las piezas en caso de que sea forraje
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        internal bool ActualizaLoteEnPatio(EntradaProductoInfo entradaProducto)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxEntradaProductoDAL.ObtenerParametrosActualizaLoteEnPatio(entradaProducto);
                Update("EntradaProducto_ActualizarLoteEntradaEnPatio", parameters);
                return true;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return false;
        }

        /// <summary>
        /// Guarda una entrada producto sin detalle
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <param name="tipoFolio"></param>
        /// <returns></returns>
        internal EntradaProductoInfo GuardarEntradaProductoSinDetalle(EntradaProductoInfo entradaProducto, int tipoFolio)
        {
            EntradaProductoInfo entradaProductoNuevo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxEntradaProductoDAL.ObtenerParametrosGuardarEntradaProductoSinDetalle(entradaProducto, tipoFolio);

                DataSet ds = Retrieve("EntradaProducto_Crear", parameters);
                if (ValidateDataSet(ds))
                {
                    entradaProductoNuevo = MapEntradaProductoDAL.ObtenerPorFolioEntradaMateriaPrima(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return entradaProductoNuevo;
        }


        /// <summary>
        /// Actualiza una entrada producto sin detalle
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        internal void ActualizarEntradaProductoSinDetalle(EntradaProductoInfo entradaProducto)
        {
            
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxEntradaProductoDAL.ObtenerParametrosActualizarEntradaProductoSinDetalle(entradaProducto);

                Update("EntradaProducto_Actualizar", parameters);

            }catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

        }
        /// <summary>
        /// Método para actualizar la fecha de inicio y fin de descarga
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        internal string ActualizaFechaDescargaPiezasEnPatio(EntradaProductoInfo entrada)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxEntradaProductoDAL.ObtenerParametrosActualizaFechaDescargaPiezasEnPatio(entrada);
                return RetrieveValue<string>("EntradaProducto_ActualizaFechaDescargaEnPatio", parameters);
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
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal ResultadoInfo<EntradaProductoInfo> ObtenerPorProductoEntradaMateriaPrima(PaginacionInfo pagina, EntradaProductoInfo filtro)
        {
            ResultadoInfo<EntradaProductoInfo> foliosLista = null;
            try
            {
                Dictionary<string, object> parameters =
                    AuxEntradaProductoDAL.ObtenerParametrosFoliosPorPaginaParaEntradaMateriaPrima(pagina, filtro);
                DataSet ds = Retrieve("EntradaProducto_ObtenerFolioPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    foliosLista = MapEntradaProductoDAL.ObtenerFoliosPorPaginaParaEntradaMateriaPrima(ds);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
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
            return foliosLista;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <param name="almacenMovimientoInfo"></param>
        /// <returns></returns>
        internal bool ActualizarAlmacenMovimiento(EntradaProductoInfo entradaProducto,AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            bool regreso = false;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxEntradaProductoDAL.ObtenerParametrosActualizarAlmacenMovimiento(entradaProducto, almacenMovimientoInfo);
                Update("EntradaProducto_ActualizarMovimiento", parameters);
                regreso = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return regreso;
        }
        /// <summary>
        /// Obtiene la entrada producto por folio y organizacion
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="folio"></param>
        /// <returns></returns>
        internal EntradaProductoInfo ObtenerEntradaProductoPorFolio(int organizacionId, int folio)
        {
            EntradaProductoInfo entradaProducto = null;

            try
            {
                Dictionary<string, object> parametros = AuxEntradaProductoDAL.ObtenerParametrosObtenerEntradaProductoValido(organizacionId, folio);
                DataSet ds = Retrieve("EntradaProducto_ObtenerEntradaPorFolio", parametros);
                if (ValidateDataSet(ds))
                {
                    entradaProducto = MapEntradaProductoDAL.ObtenerEntradaProductoPorFolio(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return entradaProducto;
        }

        //ObtenerEntradaProductoPorRegistroVigilancia
        /// <summary>
        /// Obtiene la entrada producto por folio y organizacion
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="folio"></param>
        /// <returns></returns>
        internal EntradaProductoInfo ObtenerEntradaProductoPorRegistroVigilanciaID(int organizacionId, int folio)
        {
            EntradaProductoInfo entradaProducto = null;

            try
            {
                Dictionary<string, object> parametros = AuxEntradaProductoDAL.ObtenerParametrosObtenerEntradaProductoValido(organizacionId, folio);
                DataSet ds = Retrieve("EntradaProducto_ObtenerEntradaPorRegistroVigilancia", parametros);
                if (ValidateDataSet(ds))
                {
                    entradaProducto = MapEntradaProductoDAL.ObtenerEntradaProductoPorFolio(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return entradaProducto;
        }
        /// <summary>
        /// Obtiene la entrada producto por su identificador
        /// </summary>
        /// <param name="entradaProductoId"></param>
        /// <returns></returns>
        internal EntradaProductoInfo ObtenerEntradaProductoPorId(int entradaProductoId)
        {
            EntradaProductoInfo entradaProducto = null;

            try
            {
                Dictionary<string, object> parametros = AuxEntradaProductoDAL.ObtenerParametrosObtenerEntradaProductoPorId(entradaProductoId);
                DataSet ds = Retrieve("EntradaProducto_ObtenerEntradaProductoPorId", parametros);
                if (ValidateDataSet(ds))
                {
                    entradaProducto = MapEntradaProductoDAL.ObtenerEntradaProductoPorFolio(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return entradaProducto;
        }
        /// <summary>
        /// Obtiene todos las entrada de producto para la ayuda de folios
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal List<EntradaProductoInfo> ObtenerEntradaProductosAyuda(int organizacionId)
        {
            List<EntradaProductoInfo> listaEntradaProductos = null;

            try
            {
                Dictionary<string, object> parametros = AuxEntradaProductoDAL.ObtenerParametrosObtenerListaPorOrganizacionId(organizacionId);
                DataSet ds = Retrieve("EntradaProducto_ObtenerEntradaProductosParaAyuda", parametros);
                if (ValidateDataSet(ds))
                {
                    listaEntradaProductos = MapEntradaProductoDAL.ObtenerEntradasAyuda(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaEntradaProductos;
        }

        /// <summary>
        /// Obtiene contrato por organizacion
        /// </summary>
        /// <param name="contratoID"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal List<EntradaProductoInfo> ObtenerPorContratoOrganizacion(int contratoID, int organizacionID)
        {
            List<EntradaProductoInfo> resultado = null;
            try
            {
                Dictionary<string, object> parametros =
                    AuxEntradaProductoDAL.ObtenerParametrosContratoOrganizacion(contratoID, organizacionID);
                DataSet ds = Retrieve("EntradaProducto_ObtenerCostosAlmacen", parametros);
                if (ValidateDataSet(ds))
                {
                    resultado = MapEntradaProductoDAL.ObtenerPorContratoOrganizacion(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene un contenedor de Entrada de Materia Prima
        /// </summary>
        /// <param name="folioEntrada"></param>
        /// <param name="contratoId"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal ContenedorEntradaMateriaPrimaInfo ObtenerPorFolioEntradaContrato(int folioEntrada, int contratoId, int organizacionID)
        {
            ContenedorEntradaMateriaPrimaInfo resultado = null;
            try
            {
                Dictionary<string, object> parametros =
                    AuxEntradaProductoDAL.ObtenerParametrosFolioEntradaContrato(folioEntrada, contratoId, organizacionID);
                DataSet ds = Retrieve("EntradaProducto_ObtenerFolioEntradaContrato", parametros);
                if (ValidateDataSet(ds))
                {
                    resultado = MapEntradaProductoDAL.ObtenerPorFolioEntradaContrato(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        internal List<DiferenciasIndicadoresMuestraContrato> ObtenerDiferenciasIndicadoresMuestraContratoPorEntradaID(int entradaProductoId)
        {
            List<DiferenciasIndicadoresMuestraContrato> resultado = null;
            try
            {
                Dictionary<string, object> parametros =
                    AuxEntradaProductoDAL.ObtenerParametrosObtenerEntradaProductoPorId(entradaProductoId);
                DataSet ds = Retrieve("EntradaProductoDetalle_ObtenerDiferenciasIndicadoresPorEntradaId", parametros);
                if (ValidateDataSet(ds))
                {
                    resultado = MapEntradaProductoDAL.ObtenerDiferenciasIndicadoresMuestraContratoPorEntradaID(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }


        /// <summary>
        /// Obtiene un listado de entradas por productoid
        /// </summary>
        /// <returns></returns>
        internal List<EntradaProductoInfo> ObtenerEntradaProductoPorContratoId(EntradaProductoInfo entradaProducto)
        {
            List<EntradaProductoInfo> listaEstatusInfo = null;

            try
            {
                Dictionary<string, object> parametros = AuxEntradaProductoDAL.ObtenerParametrosObtenerEntradaPorContratoId(entradaProducto);
                DataSet ds = Retrieve("EntradaProducto_ObtenerEntradaProductoPorContratoId", parametros);
                if (ValidateDataSet(ds))
                {
                    listaEstatusInfo = MapEntradaProductoDAL.ObtenerTodos(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaEstatusInfo;
        }

        /// <summary>
        /// Obtiene una lista de entradas de producto por contrato
        /// Esta funcion solo consulta EntradaProducto
        /// </summary>
        /// <param name="contratoInfo"></param>
        /// <returns></returns>
        internal List<EntradaProductoInfo> ObtenerEntradaProductoPorContrato(ContratoInfo contratoInfo)
        {
            List<EntradaProductoInfo> resultado = null;
            try
            {
                Dictionary<string, object> parametros =
                    AuxEntradaProductoDAL.ObtenerEntradaProductoPorContrato(contratoInfo);
                DataSet ds = Retrieve("EntradaProducto_ObtenerPorContratoID", parametros);
                if (ValidateDataSet(ds))
                {
                    resultado = MapEntradaProductoDAL.ObtenerEntradaProductoPorContrato(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene las entradas producto por organizacion y folio para el filtro de la ayuda
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        internal List<EntradaProductoInfo> ObtenerEntradaProductoValidoAyuda(EntradaProductoInfo entrada)
        {
            List<EntradaProductoInfo> listaEntradaProductos = null;

            try
            {
                Dictionary<string, object> parametros = AuxEntradaProductoDAL.ObtenerParametrosObtenerEntradaProductoValidoAyuda(entrada);
                DataSet ds = Retrieve("EntradaProducto_ObtenerEntradaProductoAyuda", parametros);
                if (ValidateDataSet(ds))
                {
                    listaEntradaProductos = MapEntradaProductoDAL.ObtenerEntradaProductoAyuda(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaEntradaProductos;
        }

        /// <summary>
        /// Obtiene una lista con las entradas de producto
        /// </summary>
        /// <param name="movimientosEntrada"></param>
        /// <returns></returns>
        internal IEnumerable<EntradaProductoInfo> ObtenerEntradasPorAlmacenMovimientoXML(List<AlmacenMovimientoInfo> movimientosEntrada)
        {
            try
            {
                Dictionary<string, object> parametros =
                    AuxEntradaProductoDAL.ObtenerParametrosEntradasPorAlmacenMovimientoXML(movimientosEntrada);
                IMapBuilderContext<EntradaProductoInfo> mapeo = MapEntradaProductoDAL.ObtenerMapeoEntradaProducto();
                IEnumerable<EntradaProductoInfo> listaEntradaProductos = GetDatabase().ExecuteSprocAccessor
                    <EntradaProductoInfo>(
                        "EntradaProducto_ObtenerConciliacionAlmacenMovimientoXML", mapeo.Build(),
                        new [] { parametros["@AlmacenMovimientoXML"] });
                return listaEntradaProductos;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de folios de entrada por compra para cancelar
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<EntradaProductoInfo> ObtenerFoliosPorPaginaParaCancelacionEntradaCompra(PaginacionInfo pagina, EntradaProductoInfo filtro)
        {
            ResultadoInfo<EntradaProductoInfo> foliosLista = null;
            try
            {
                Dictionary<string, object> parameters =
                    AuxEntradaProductoDAL.ObtenerFoliosPorPaginaParaCancelacionEntradaCompra(pagina, filtro);
                DataSet ds = Retrieve("EntradaProducto_ObtenerCancelacionPorDescripcionProducto", parameters);
                if (ValidateDataSet(ds))
                {
                    foliosLista = MapEntradaProductoDAL.ObtenerFoliosPorPaginaParaCancelacionEntradaCompra(ds);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
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
            return foliosLista;
        }

        /// <summary>
        /// Obtiene una lista de folios de entrada por compra para cancelar
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<EntradaProductoInfo> ObtenerFoliosPorPaginaParaCancelacionEntradaTraspaso(PaginacionInfo pagina, EntradaProductoInfo filtro)
        {
            ResultadoInfo<EntradaProductoInfo> foliosLista = null;
            try
            {
                Dictionary<string, object> parameters =
                    AuxEntradaProductoDAL.ObtenerFoliosPorPaginaParaCancelacionEntradaTraspaso(pagina, filtro);
                DataSet ds = Retrieve("EntradaProducto_ObtenerCancelacionPorDescripcionProducto", parameters);
                if (ValidateDataSet(ds))
                {
                    foliosLista = MapEntradaProductoDAL.ObtenerFoliosPorPaginaParaCancelacionEntradaCompra(ds);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
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
            return foliosLista;
        }

        /// <summary>
        /// Obtiene la cantidad de notificaciones autorizadas
        /// por autorizar
        /// </summary>
        /// <returns></returns>
        internal int ObtenerCantidadNotificacionesAutorizadas(int organizacionID)
        {
            int cantidadNotificacionesAutorizadas = 0;
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object> {{"OrganizacionID", organizacionID}};
                using (IDataReader reader = RetrieveReader("EntradaProducto_ObtenerCantidadNotificacionesAutorizadas", parametros))
                {
                    if (ValidateDataReader(reader))
                    {
                        while (reader.Read())
                        {
                            cantidadNotificacionesAutorizadas =
                                Convert.ToInt32(reader["CantidadNotificacionesAutorizadas"]);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return cantidadNotificacionesAutorizadas;
        }

        /// <summary>
        /// Obtiene las notificaciones autorizadas
        /// </summary>
        /// <returns></returns>
        internal List<EntradaProductoInfo> ObtenerNotificacionesAutorizadas(int organizacionID)
        {
            List<EntradaProductoInfo> resultado = null;
            try
            {
                var parametros = new Dictionary<string, object> { { "OrganizacionID", organizacionID } };
                using (IDataReader reader = RetrieveReader("EntradaProducto_ObtenerNotificacionesAutorizadas", parametros))
                {
                    if (ValidateDataReader(reader))
                    {
                        resultado = MapEntradaProductoDAL.ObtenerNotificacionesAutorizadas(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Actualiza bandera de revisado por generente de Planta de Alimentos
        /// </summary>
        /// <param name="entradaProductoId"></param>
        internal void ActualizaRevisionGerente(int entradaProductoId)
        {
            try
            {
                Logger.Info();
                var parameters = new Dictionary<string, object> {{"@EntradaProductoID", entradaProductoId}};
                Update("EntradaProducto_ActualizarRevisionGerente", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Cancela una entrada de producto
        /// </summary>
        /// <param name="entradaProducto"></param>
        internal bool Cancelar(EntradaProductoInfo entradaProducto)
        {
            try
            {
                Dictionary<string, object> parametros = AuxEntradaProductoDAL.ObtenerParametrosCancelar(entradaProducto);
                Update("EntradaProducto_Cancelar", parametros);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para actualizar el operador y la fecha de inicio de descarga en patio.
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        internal bool ActualizaOperadorFechaDescargaEnPatio(EntradaProductoInfo entradaProducto)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxEntradaProductoDAL.ObtenerParametrosActualizaOperadorFechaDescargaEnPatio(entradaProducto);
                Update("EntradaProducto_ActualizarOperadorFechaDescargaEnPatio", parameters);
                return true;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return false;
        }

        public decimal ObtenerHumedadOrigen(EntradaProductoInfo entradaProducto)
        {
            decimal result = 0;
            try
            {
                Dictionary<string, object> parameters =
                    AuxEntradaProductoDAL.ObtenerHumedadOrigen(entradaProducto);
                DataSet ds = Retrieve("EntradaProducto_ObtenerHumedadOrigenPorEntradaProductoID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapEntradaProductoDAL.ObtenerHumedadOrigen(ds);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
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
            return result;
        }

        public void ActualizarDescuentoEntradaProductoMuestra(EntradaProductoInfo entradaProducto, decimal descuento)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxEntradaProductoDAL.ActualizarDescuentoEntradaProductoMuestra(entradaProducto, descuento);
                Update("EntradaProducto_ActualizarDescuentoEntradaProductoMuestra", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
    }
}
