
using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxRegistroVigilanciaDAL
    {
        /// <summary>
        /// Obtiene los parametros para consultar por id
        /// </summary>
        /// <param name="registroVigilanciaInfo"></param>
        /// <returns>Dictionary</returns>
        internal static Dictionary<string, object> ObtenerParametroObtenerPorId(RegistroVigilanciaInfo registroVigilanciaInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@RegistroVigilanciaID", registroVigilanciaInfo.RegistroVigilanciaId}
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene los parametros para consultar por FolioTurno
        /// </summary>
        /// <param name="registroVigilanciaInfo"></param>
        /// <returns>Dictionary</returns>
        internal static Dictionary<string, object> ObtenerParametroObtenerPorFolioTurno(RegistroVigilanciaInfo registroVigilanciaInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@FolioTurno", registroVigilanciaInfo.FolioTurno},
                                     {"@OrganizacionID", registroVigilanciaInfo.Organizacion.OrganizacionID}
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }
        /// <summary>
        /// crea un registro en la tabla RegistroVigilancia
        /// </summary>
        /// <param name="registroVigilanciaInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarDatos(RegistroVigilanciaInfo registroVigilanciaInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@ProductoID", registroVigilanciaInfo.Producto.ProductoId},
                                     {"@ProveedorMateriasPrimas", registroVigilanciaInfo.ProveedorMateriasPrimas.ProveedorID},
                                     {"@Transportista",registroVigilanciaInfo.ProveedorChofer.Proveedor.Descripcion},
                                     {"@Chofer",registroVigilanciaInfo.ProveedorChofer.Chofer.NombreCompleto},
                                     {"@ContratoID",registroVigilanciaInfo.Contrato.ContratoId},
                                     {"@ProveedorChoferID", registroVigilanciaInfo.ProveedorChofer.ProveedorChoferID},
                                     {"@CamionID", registroVigilanciaInfo.Camion.CamionID},
                                     {"@Camion",registroVigilanciaInfo.CamionCadena},
                                     {"@Marca", registroVigilanciaInfo.Marca},
                                     {"@Color", registroVigilanciaInfo.Color},
                                     {"@Activo", registroVigilanciaInfo.Activo },
                                     {"@OrganizacionID", registroVigilanciaInfo.Organizacion.OrganizacionID},
                                     {"@UsuarioCreacionID", registroVigilanciaInfo.UsuarioCreacionID},
                                     {"@TipoFolio", registroVigilanciaInfo.TipoFolioID},
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }
        /// <summary>
        /// Modifica los campos de fecha salida y activo = 0 en la tabla "RegistroVigilancia". de esta forma se registra a que hora salio el camion
        /// </summary>
        /// <param name="registroVigilanciaInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> RegistroSalida(RegistroVigilanciaInfo registroVigilanciaInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionID", registroVigilanciaInfo.Organizacion.OrganizacionID},
                                     {"@FolioTurno", registroVigilanciaInfo.FolioTurno},
                                     {"@Activo", registroVigilanciaInfo.Activo },
                                     {"@UsuarioModificacionID", registroVigilanciaInfo.UsuarioModificacionID}
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene los parametros para consultar por FolioTurno
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="pagina"></param>
        /// <returns>Dictionary</returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorPagina(PaginacionInfo pagina, RegistroVigilanciaInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@FolioTurno", filtro.FolioTurno},
                                     {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                                     {"@Activo", filtro.Activo},
                                     {"@Inicio", pagina.Inicio},
                                     {"@Limite", pagina.Limite}
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }
        /// <summary>
        /// Obtiene los parametros para consultar Disponibilidad de Camion por placas
        /// </summary>
        /// <param name="camion"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerDisponibilidadCamion(CamionInfo camion)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@Camion", camion.PlacaCamion}
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }
    }
}
