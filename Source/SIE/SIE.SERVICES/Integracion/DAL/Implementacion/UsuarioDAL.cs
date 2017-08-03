using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class UsuarioDAL : DALBase
    {
        /// <summary>
        ///     Obtiene un lista paginada de usuario por el filtro especificado
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<UsuarioInfo> ObtenerPorPagina(PaginacionInfo pagina, UsuarioInfo filtro)
        {
            ResultadoInfo<UsuarioInfo> usuarioLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxUsuarioDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Usuario_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    usuarioLista = MapUsuarioDAL.ObtenerPorPagina(ds);
                }
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
            return usuarioLista;
        }

        /// <summary>
        ///     Obtiene un Usuario por Id
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <returns></returns>
        internal UsuarioInfo ObtenerPorID(int usuarioID)
        {
            UsuarioInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxUsuarioDAL.ObtenerParametroPorID(usuarioID);
                DataSet ds = Retrieve("Usuario_ObtenerPorID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapUsuarioDAL.ObtenerPorID(ds);
                }
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
            return result;
        }

        /// <summary>
        /// Obtiene una lista de usuarios paginada por 
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <param name="dependencias"></param>
        /// <returns></returns>
        internal ResultadoInfo<UsuarioInfo> ObtenerPorDescripcion(PaginacionInfo pagina, UsuarioInfo filtro,
                                                                IList<IDictionary<IList<string>, object>> dependencias)
        {
            ResultadoInfo<UsuarioInfo> usuarioLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxUsuarioDAL.ObtenerParametrosPorPagina(pagina, filtro,
                                                                                                 dependencias);
                DataSet ds = Retrieve("Usuario_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    usuarioLista = MapUsuarioDAL.ObtenerPorPagina(ds);
                }
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
            return usuarioLista;
        }

        /// <summary>
        ///     Obtiene un Usuario por Active Directory
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        internal UsuarioInfo ObtenerPorActiveDirectory(string usuario)
        {
            UsuarioInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxUsuarioDAL.ObtenerParametroPorActiveDirectory(usuario);
                DataSet ds = Retrieve("Usuario_ObtenerPorActiveDirectory", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapUsuarioDAL.ObtenerPorActiveDirectory(ds);
                }
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
            return result;
        }

        /// <summary>
        /// Metodo para Crear un registro de Usuario
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(UsuarioInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxUsuarioDAL.ObtenerParametrosCrear(info);
                int result = Create("Usuario_Crear", parameters);
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
        /// Metodo para actualizar un registro de Usuario
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(UsuarioInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxUsuarioDAL.ObtenerParametrosActualizar(info);
                Update("Usuario_Actualizar", parameters);
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
        /// Obtiene un usuario por su clave
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <returns></returns>
        internal UsuarioInfo ObtenerSupervisorID(int usuarioID)
        {
            UsuarioInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxUsuarioDAL.ObtenerParametroPorID(usuarioID);
                DataSet ds = Retrieve("Usuario_ObtenerPorID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapUsuarioDAL.ObtenerPorSupervisorID(ds);
                }
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
            return result;
        }

                
        /// <summary>
        /// Obtiene nivel alerta por usuarioID
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <returns></returns>
        internal UsuarioInfo ObtenerNivelAlertaPorUsuarioID(int usuarioID)
        {
            UsuarioInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxUsuarioDAL.ObtenerParametroPorID(usuarioID);
                DataSet ds = Retrieve("Usuario_ObtenerNivelAlertaPorUsuarioID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapUsuarioDAL.ObtenerNivelAlertaPorUsuarioID(ds);
                }
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
            return result;
        }

        /// <summary>
        /// Obtiene el listado de usuarios a los que se enviará correo
        /// </summary>
        /// <param name="rolesXml">XML con un listado de roles</param>
        /// <returns>Listado de usuarios</returns>
        internal List<UsuarioInfo> ObtenerCorreos(string rolesXml)
        {
            try
            {
                Logger.Info();
                var parameters = AuxUsuarioDAL.ObtenerParametrosObtenerCorreos(rolesXml);
                var ds = Retrieve("CapturaPedido_ObtenerUsuariosCorreo", parameters);
                List<UsuarioInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapUsuarioDAL.ObtenerListado(ds);
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
    }
}