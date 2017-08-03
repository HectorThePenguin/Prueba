using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using SIE.Base.Infos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    public class RecepcionProductoDAL : DALBase
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="cadena"></param>
        public RecepcionProductoDAL(string cadena = "")
        {
            if(!cadena.Equals(string.Empty))
                this.ConnectionString = cadena;
        }
        /// <summary>
        /// Guarda la informacion de una recepcion de producto
        /// </summary>
        /// <param name="recepcionProducto"></param>
        /// <returns></returns>
        internal RecepcionProductoInfo Guardar(RecepcionProductoInfo recepcionProducto)
        {
            RecepcionProductoInfo recepcionProductoGuardado = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxRecepcionProductoDAL.ObtenerParametrosGuardar(recepcionProducto);

                DataSet ds = Retrieve("RecepcionProducto_Guardar", parameters);
                if (ValidateDataSet(ds))
                {
                    recepcionProductoGuardado = MapRecepcionProductoDAL.ObtenerRecepcionProducto(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return recepcionProductoGuardado;
        }

        internal RecepcionProductoInfo ObtenerRecepcionVista(RecepcionProductoInfo recepcionProductoCompra, int organizacionId)
        {
            RecepcionProductoInfo recepcionProductoGuardado = null;
            try
            {
                Logger.Info();
                string cadena =
                    string.Format(
                        "SELECT OrganizacionID,FolioSolicitud,FechaSolicitud,Proveedor,Producto,Cantidad,CostoUnitario,Importe,Unidad,CuentaGasto FROM VW_Interfaz_SIAP_ComprasWEB WHERE OrganizacionID = {0} AND FolioSolicitud = {1}",
                        organizacionId, recepcionProductoCompra.FolioOrdenCompra);
                var ds = new DataSet();
                using (SqlConnection connection = new SqlConnection(this.ConnectionString))
                {
                    SqlCommand command = new SqlCommand(cadena, connection);
                    command.Connection.Open();
                    var adapter = new SqlDataAdapter(command);
                    
                    adapter.Fill(ds);
                }
                if (ValidateDataSet(ds))
                {
                    recepcionProductoGuardado = MapRecepcionProductoDAL.ObtenerRecepcionProductoVista(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return recepcionProductoGuardado;
        }

        /// <summary>
        /// Obtiene una recepcion por folio de recepcion
        /// </summary>
        /// <param name="recepcionProductoCompra"></param>
        /// <returns></returns>
        internal RecepcionProductoInfo ObtenerRecepcionPorFolio(RecepcionProductoInfo recepcionProductoCompra)
        {
            RecepcionProductoInfo recepcionProducto = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxRecepcionProductoDAL.ObtenerParametrosObtenerRecepcionPorFolio(recepcionProductoCompra);

                DataSet ds = Retrieve("RecepcionProducto_ObtenerPorFolioOrdenCompra", parameters);
                if (ValidateDataSet(ds))
                {
                    recepcionProducto = MapRecepcionProductoDAL.ObtenerRecepcionProducto(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return recepcionProducto;
        }

        /// <summary>
        /// Obtiene una Recepcion por Folio Recepcion y Organizacion
        /// </summary>
        /// <param name="recepcionProducto"></param>
        /// <returns></returns>
        internal RecepcionProductoInfo ObtenerRecepcionPorFolioOrganizacion(RecepcionProductoInfo recepcionProducto)
        {
            RecepcionProductoInfo resultado = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxRecepcionProductoDAL.ObtenerParametrosObtenerPorFolioOrganizacion(recepcionProducto);

                DataSet ds = Retrieve("RecepcionProducto_ObtenerPorFolioOrganizacion", parameters);
                if (ValidateDataSet(ds))
                {
                    resultado = MapRecepcionProductoDAL.ObtenerRecepcionPorFolioOrganizacion(ds);
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
        /// Obtiene un objeto paginado con
        /// los datos de la recepcion de producto
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="recepcionProducto"></param>
        /// <returns></returns>
        internal ResultadoInfo<RecepcionProductoInfo> ObtenerRecepcionPorFolioOrganizacionPaginado(PaginacionInfo pagina, RecepcionProductoInfo recepcionProducto)
        {
            ResultadoInfo<RecepcionProductoInfo> resultado = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxRecepcionProductoDAL.ObtenerParametrosObtenerPorFolioOrganizacionPaginado(pagina,
                                                                                                 recepcionProducto);
                DataSet ds = Retrieve("RecepcionProducto_ObtenerPorFolioOrganizacionPaginado", parameters);
                if (ValidateDataSet(ds))
                {
                    resultado = MapRecepcionProductoDAL.ObtenerRecepcionPorFolioOrganizacionPaginado(ds);
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
        /// Obtiene una coleccion de recepcion producto
        /// </summary>
        /// <param name="almacenesMovimiento"></param>
        /// <returns></returns>
        internal List<RecepcionProductoInfo> ObtenerRecepcionProductoConciliacionPorAlmacenMovimiento(List<AlmacenMovimientoInfo> almacenesMovimiento)
        {
            try
            {
                Dictionary<string, object> parametros = AuxRecepcionProductoDAL.ObtenerParametrosPorAlmacenMovimientoXML(almacenesMovimiento);
                List<RecepcionProductoInfo> recepcionProductos = null;
                using (IDataReader reader = RetrieveReader("RecepcionProducto_ConciliacionObtenerPorXML", parametros))
                {
                    if (ValidateDataReader(reader))
                    {
                        recepcionProductos = MapRecepcionProductoDAL.ObtenerRecepcionProductoConciliacionPorAlmacenMovimientoXML(reader);
                    }
                }
                return recepcionProductos;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
