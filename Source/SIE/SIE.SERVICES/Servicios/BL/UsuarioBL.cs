using System;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Reflection;
using System.Transactions;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Interfaces;
using System.Collections.Generic;

namespace SIE.Services.Servicios.BL
{
    internal class UsuarioBL : IAyudaDependencia<UsuarioInfo>
    {
        /// <summary>
        ///     Obtiene un lista paginada de usuarios
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<UsuarioInfo> ObtenerPorPagina(PaginacionInfo pagina, UsuarioInfo filtro)
        {
            ResultadoInfo<UsuarioInfo> result;
            try
            {
                Logger.Info();
                var usuarioDAL = new UsuarioDAL();
                result = usuarioDAL.ObtenerPorPagina(pagina, filtro);
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
        ///     Obtiene un Usuario por Id
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <returns></returns>
        internal UsuarioInfo ObtenerPorID(int usuarioID)
        {
            UsuarioInfo usuarioInfo;
            try
            {
                Logger.Info();
                var usuarioDAL = new UsuarioDAL();
                usuarioInfo = usuarioDAL.ObtenerPorID(usuarioID);
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
            return usuarioInfo;
        }

        ResultadoInfo<UsuarioInfo> IAyudaDependencia<UsuarioInfo>.ObtenerPorId(int id, IList<IDictionary<IList<string>, object>> dependencias)
        {
            ResultadoInfo<UsuarioInfo> usuarioInfoResult = null;
            try
            {
                Logger.Info();
                var usuarioDAL = new UsuarioDAL();
                var usuarioInfo = usuarioDAL.ObtenerPorID(id);
                if (usuarioInfo != null)
                {
                    usuarioInfoResult = new ResultadoInfo<UsuarioInfo>();
                    usuarioInfoResult.Lista = new List<UsuarioInfo>();

                    usuarioInfoResult.Lista.Add(usuarioInfo);
                    usuarioInfoResult.TotalRegistros = 1;
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
            return usuarioInfoResult;
        }

        ResultadoInfo<UsuarioInfo> IAyudaDependencia<UsuarioInfo>.ObtenerPorDescripcion(PaginacionInfo pagina, string descripcion, IList<IDictionary<IList<string>, object>> dependencias)
        {
            ResultadoInfo<UsuarioInfo> result;
            try
            {
                Logger.Info();
                var usuarioDAL = new UsuarioDAL();
                var filtro = new UsuarioInfo { Nombre = descripcion };
                result = usuarioDAL.ObtenerPorDescripcion(pagina, filtro, dependencias);
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
        /// Método que valida un usuario en el active directory
        /// </summary>
        /// <param name="usuarioActiveDirectory"></param>
        /// <param name="contraseña"></param>
        /// <param name="configuracion"></param>
        /// <returns></returns>
        internal UsuarioInfo ValidarUsuario(string usuarioActiveDirectory, string contraseña, ConfiguracionInfo configuracion)
        {
            UsuarioInfo usuarioInfo = null;
            try
            {
                Logger.Info();
                string contenedor = ConfigurationManager.AppSettings["Contenedor"];
                string servidorActiveDirectory = ConfigurationManager.AppSettings["ServidorActiveDirectory"];

#if DEBUG
                bool produccion = Convert.ToBoolean(ConfigurationManager.AppSettings["Produccion"]);
                if (!produccion)
                {
                    return new UsuarioInfo { Nombre = usuarioActiveDirectory };
                }
#endif

                using (var context = new PrincipalContext(ContextType.Domain, servidorActiveDirectory, contenedor))
                {
                    bool credencialesValidas = context.ValidateCredentials(usuarioActiveDirectory, contraseña);
                    if (credencialesValidas)
                    {
                        usuarioInfo = new UsuarioInfo { Nombre = usuarioActiveDirectory };
                        //UserPrincipal usuario = UserPrincipal.FindByIdentity(context, usuarioActiveDirectory);
                        //if (usuario != null)
                        //{
                        //    bool grupoValido = false; 
                        //    PrincipalSearchResult<Principal> gruposUsuario = usuario.GetGroups();
                        //    if (gruposUsuario.Any(grupo => grupo.Name == configuracion.GrupoAD))
                        //    {
                        //        grupoValido = true;
                        //    }
                        //    if (grupoValido)
                        //    {
                        //        usuarioInfo = new UsuarioInfo {Nombre = usuario.Name};
                        //    }
                        //}                        
                    }
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
            return usuarioInfo;
        }

        /// <summary>
        /// Método que valida un usuario en el active directory
        /// </summary>
        /// <param name="usuarioActiveDirectory"></param>
        /// <param name="contraseña"></param>
        /// <returns></returns>
        internal FiltroUsuarioInfo ValidarUsuarioCompleto(string usuarioActiveDirectory, string contraseña)
        {
            var filtroUsuario = new FiltroUsuarioInfo();
            var usuarioInfo = new UsuarioInfo();
            try
            {
                Logger.Info();
                string contenedor = ConfigurationManager.AppSettings["Contenedor"];
                string servidorActiveDirectory = ConfigurationManager.AppSettings["ServidorActiveDirectory"];

                using (var context = new PrincipalContext(ContextType.Domain, servidorActiveDirectory, contenedor))
                {
                    bool credencialesValidas = context.ValidateCredentials(usuarioActiveDirectory, contraseña);
                    if (credencialesValidas)
                    {
                        usuarioInfo = new UsuarioInfo { Nombre = usuarioActiveDirectory };
                    }
                    else
                    {
                        filtroUsuario.Mensaje = ConstantesBL.MensajeUsuarioNoExiste;
                        return filtroUsuario;
                    }
                }
                UsuarioInfo usuarioCompleto = ObtenerPorActiveDirectory(usuarioInfo.Nombre);
                if (usuarioCompleto == null)
                {
                    filtroUsuario.Mensaje = ConstantesBL.MensajeUsuarioNoExisteSistema;
                    return filtroUsuario;
                }
                
                if (usuarioCompleto.Operador == null || usuarioCompleto.Operador.Rol == null
                    ||(usuarioCompleto.Operador.Rol.RolID != Roles.SupervisorPlantaAlimentos.GetHashCode()))
                {
                    filtroUsuario.Mensaje = ConstantesBL.MensajeUsuarioRol;
                    return filtroUsuario;
                }
                filtroUsuario.Usuario = usuarioCompleto;
                filtroUsuario.Mensaje = "OK";
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
            return filtroUsuario;
        }

        /// <summary>
        ///     Obtiene un Usuario por Active Directory
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        internal UsuarioInfo ObtenerPorActiveDirectory(string usuario)
        {
            UsuarioInfo usuarioInfo;
            try
            {
                Logger.Info();
                var usuarioDAL = new UsuarioDAL();
                var organizacionDAL = new OrganizacionDAL();
                var operadorDAL = new OperadorDAL();
                var almacenUsuarioDal = new AlmacenUsuarioDAL();

                usuarioInfo = usuarioDAL.ObtenerPorActiveDirectory(usuario);
                if (usuarioInfo != null && usuarioInfo.Organizacion != null)
                {
                    usuarioInfo.Organizacion = organizacionDAL.ObtenerPorID(usuarioInfo.Organizacion.OrganizacionID);
                }

                if (usuarioInfo != null && usuarioInfo.UsuarioID != 0)
                {
                    usuarioInfo.Operador = operadorDAL.ObtenerPorUsuarioID(usuarioInfo.UsuarioID, usuarioInfo.Organizacion.OrganizacionID);
                    usuarioInfo.AlmacenUsuario = almacenUsuarioDal.ObtenerPorUsuarioId(usuarioInfo.UsuarioID);
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
            return usuarioInfo;
        }

        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Usuario
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(UsuarioInfo info)
        {
            try
            {
                Logger.Info();
                var usuarioDAL = new UsuarioDAL();
                int result = info.UsuarioID;

                using (var transaction = new TransactionScope())
                {
                    if (info.UsuarioID == 0)
                    {
                        result = usuarioDAL.Crear(info);
                    }
                    else
                    {
                        usuarioDAL.Actualizar(info);
                    }

                    if (info.UsuarioGrupo.Any())
                    {
                        //relacionar el usuario con el grupo  
                        foreach (var usuarioGrupo in info.UsuarioGrupo)
                        {
                            usuarioGrupo.Usuario.UsuarioID = result;
                        }

                        var usuarioGrupoDAL = new UsuarioGrupoDAL();
                        usuarioGrupoDAL.Guardar(info.UsuarioGrupo);
                    }
                    transaction.Complete();
                }
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
        /// Obtiene un usuario por su clave
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <returns></returns>
        internal UsuarioInfo ObtenerSupervisorID(int usuarioID)
        {
            UsuarioInfo usuarioInfo;
            try
            {
                Logger.Info();
                var usuarioDAL = new UsuarioDAL();
                usuarioInfo = usuarioDAL.ObtenerSupervisorID(usuarioID);
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
            return usuarioInfo;
        }

        
        /// <summary>
        /// Obtiene nivel alerta por usuarioID
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <returns></returns>
        internal UsuarioInfo ObtenerNivelAlertaPorUsuarioID(int usuarioID)
        {
            UsuarioInfo usuarioInfo;
            try
            {
                Logger.Info();
                var usuarioDAL = new UsuarioDAL();
                usuarioInfo = usuarioDAL.ObtenerNivelAlertaPorUsuarioID(usuarioID);
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
            return usuarioInfo;
        }

        /// <summary>
        /// Obtiene el listado de usuarios a los que se enviará correo
        /// </summary>
        /// <param name="roles">Listado de roles</param>
        /// <returns>Regresa un listado de usuarios</returns>
        internal List<UsuarioInfo> ObtenerCorreos(List<int> roles)
        {
            var usuarioDAL = new UsuarioDAL();
            var correos = new List<UsuarioInfo>();
            try
            {
                XElement xml = new XElement("IntList", roles.Select(x => new XElement("Int", x)));
                string rolesXml = xml.ToString();

                correos = usuarioDAL.ObtenerCorreos(rolesXml);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return correos;
        }
    }
}