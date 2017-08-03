using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using System.Xml.Linq;
using System.Linq;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class EntradaGanadoTransitoDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de EntradaGanadoTransito
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(EntradaGanadoTransitoInfo info)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in info.EntradasGanadoTransitoDetalles
                                 select new XElement("Detalle",
                                                     new XElement("CostoID", detalle.Costo.CostoID),
                                                     new XElement("Importe", detalle.Importe)));
                var parameters = new Dictionary<string, object>
                                     {
                                         {"@LoteID", info.Lote.LoteID},
                                         {"@Cabezas", info.Cabezas},
                                         {"@Activo", info.Activo},
                                         {"@Peso", info.Peso},
                                         {"@UsuarioCreacionID", info.UsuarioCreacionID},
                                         {"@XmlDetalle", xml.ToString()},
                                     };
                int result = Create("EntradaGanadoTransito_Crear", parameters);
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
        /// Metodo para actualizar un registro de EntradaGanadoTransito
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(EntradaGanadoTransitoInfo info)
        {
            try
            {
                Logger.Info();
                bool sobrante = info.Sobrante;
                var xml =
                    new XElement("ROOT",
                                 from detalle in info.EntradasGanadoTransitoDetalles
                                 select new XElement("Detalle",
                                                     new XElement("CostoID", detalle.Costo.CostoID),
                                                     new XElement("EntradaGanadoTransitoID", detalle.EntradaGanadoTransitoID),
                                                     new XElement("Importe", sobrante ? detalle.Importe * -1 : detalle.Importe)));
                var parameters = new Dictionary<string, object>
                                     {
                                         {"@EntradaGanadoTransitoID", info.EntradaGanadoTransitoID},
                                         {"@LoteID", info.Lote.LoteID},
                                         {"@Cabezas", info.Cabezas},
                                         {"@Activo", info.Activo},
                                         {"@Peso", info.Peso},
                                         {"@UsuarioModificacionID", info.UsuarioModificacionID},
                                         {"@XmlDetalle", xml.ToString()},
                                     };
                Update("EntradaGanadoTransito_Actualizar", parameters);
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
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<EntradaGanadoTransitoInfo> ObtenerPorPagina(PaginacionInfo pagina, EntradaGanadoTransitoInfo filtro)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                                     {
                                         {"@Lote", filtro.Lote.Lote},
                                         {"@Activo", filtro.Activo},
                                         {"@Inicio", pagina.Inicio},
                                         {"@Limite", pagina.Limite},
                                     };
                DataSet ds = Retrieve("EntradaGanadoTransito_ObtenerPorPagina", parameters);
                ResultadoInfo<EntradaGanadoTransitoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapEntradaGanadoTransitoDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de EntradaGanadoTransito
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<EntradaGanadoTransitoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                IEnumerable<EntradaGanadoTransitoInfo> entradasGanadoTransito = null;
                using (IDataReader reader = RetrieveReader("EntradaGanadoTransito_ObtenerTodos"))
                {
                    if (ValidateDataReader(reader))
                    {
                        entradasGanadoTransito = MapEntradaGanadoTransitoDAL.ObtenerMapeoEntradaGanadoTransito(reader);
                    }
                }
                return entradasGanadoTransito;
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
        /// Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<EntradaGanadoTransitoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                IEnumerable<EntradaGanadoTransitoInfo> entradasGanadoTransito = null;
                var parametros = new Dictionary<string, object> { { "@Activo", estatus } };
                using (IDataReader reader = RetrieveReader("EntradaGanadoTransito_ObtenerTodos", parametros))
                {
                    if (ValidateDataReader(reader))
                    {
                        entradasGanadoTransito = MapEntradaGanadoTransitoDAL.ObtenerMapeoEntradaGanadoTransito(reader);
                    }
                }
                return entradasGanadoTransito;
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
        /// Obtiene un registro de EntradaGanadoTransito
        /// </summary>
        /// <param name="entradaGanadoTransitoID">Identificador de la EntradaGanadoTransito</param>
        /// <returns></returns>
        internal IEnumerable<EntradaGanadoTransitoInfo> ObtenerPorID(int entradaGanadoTransitoID)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object> { { "@EntradaGanadoTransitoID", entradaGanadoTransitoID } };
                IEnumerable<EntradaGanadoTransitoInfo> entradasGanadoTransito = null;
                using (IDataReader reader = RetrieveReader("EntradaGanadoTransito_ObtenerPorID", parametros))
                {
                    if (ValidateDataReader(reader))
                    {
                        entradasGanadoTransito = MapEntradaGanadoTransitoDAL.ObtenerMapeoEntradaGanadoTransito(reader);
                    }
                }
                return entradasGanadoTransito;
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
        /// Ejecuta procedimiento almacenado con sus parametros
        /// </summary>
        /// <param name="corral"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal IEnumerable<EntradaGanadoTransitoInfo> ObtenerPorCorralOrganizacion(string corral, int organizacionID)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object> { { "@Corral", corral }, { "@OrganizacionID", organizacionID } };
                IEnumerable<EntradaGanadoTransitoInfo> entradasGanadoTransito = null;
                using (IDataReader reader = RetrieveReader("EntradaGanadoTransito_ObtenerPorCorralOrganizacion", parametros))
                {
                    if (ValidateDataReader(reader))
                    {
                        entradasGanadoTransito = MapEntradaGanadoTransitoDAL.ObtenerMapeoEntradaGanadoTransito(reader);
                    }
                }
                return entradasGanadoTransito;
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
    }
}
