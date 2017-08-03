using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using SIE.Base.Interfaces;
using System.Collections.Generic;

namespace SIE.Services.Servicios.PL
{
    public class UsuarioPL : IAyudaDependencia<UsuarioInfo>
    {
        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<UsuarioInfo> ObtenerPorPagina(PaginacionInfo pagina, UsuarioInfo filtro)
        {
            ResultadoInfo<UsuarioInfo> resultado;
            try
            {
                Logger.Info();
                var usuarioBL = new UsuarioBL();
                resultado = usuarioBL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un Usuario por Id
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <returns></returns>
        public UsuarioInfo ObtenerPorID(int usuarioID)
        {
            UsuarioInfo usuarioInfo;
            try
            {
                Logger.Info();
                var usuarioBL = new UsuarioBL();
                usuarioInfo = usuarioBL.ObtenerPorID(usuarioID);
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
        /// Obtiene un Usuario por Id para implementar la ayuda.
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public UsuarioInfo ObtenerPorID(UsuarioInfo info)
        {
            UsuarioInfo usuarioInfo;
            try
            {
                Logger.Info();
                var usuarioBL = new UsuarioBL();
                usuarioInfo = usuarioBL.ObtenerPorID(info.UsuarioID);
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
            ResultadoInfo<UsuarioInfo> usuarioInfo;
            try
            {
                Logger.Info();
                IAyudaDependencia<UsuarioInfo> usuarioBL = new UsuarioBL();
                usuarioInfo = usuarioBL.ObtenerPorId(id, dependencias);
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

        ResultadoInfo<UsuarioInfo> IAyudaDependencia<UsuarioInfo>.ObtenerPorDescripcion(PaginacionInfo pagina, string descripcion, IList<IDictionary<IList<string>, object>> dependencias)
        {
            ResultadoInfo<UsuarioInfo> resultado;
            try
            {
                Logger.Info();
                IAyudaDependencia<UsuarioInfo> usuarioBL = new UsuarioBL();
                resultado = usuarioBL.ObtenerPorDescripcion(pagina, descripcion, dependencias);
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
        /// Método que valida un usuario en el active directory
        /// </summary>
        /// <param name="usuarioActiveDirectory"></param>
        /// <param name="contraseña"></param>
        /// <param name="configuracion"></param>
        /// <returns></returns>
        public UsuarioInfo ValidarUsuario(string usuarioActiveDirectory, string contraseña, ConfiguracionInfo configuracion)
        {
            UsuarioInfo usuarioInfo;
            try
            {
                Logger.Info();
                var usuarioBL = new UsuarioBL();
                usuarioInfo = usuarioBL.ValidarUsuario(usuarioActiveDirectory, contraseña, configuracion);
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
        ///     Obtiene un Usuario por Active Directory
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public UsuarioInfo ObtenerPorActiveDirectory(string usuario)
        {
            UsuarioInfo usuarioInfo;
            try
            {
                Logger.Info();
                var usuarioBL = new UsuarioBL();
                usuarioInfo = usuarioBL.ObtenerPorActiveDirectory(usuario);
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
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(UsuarioInfo info)
        {
            try
            {
                Logger.Info();
                var usuarioBL = new UsuarioBL();
                int result = usuarioBL.Guardar(info);
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
        /// Método que valida un usuario en el active directory
        /// </summary>
        /// <param name="usuarioActiveDirectory"></param>
        /// <param name="contraseña"></param>
        /// <returns></returns>
        public FiltroUsuarioInfo ValidarUsuarioCompleto(string usuarioActiveDirectory, string contraseña)
        {
            FiltroUsuarioInfo usuarioInfo;
            try
            {
                Logger.Info();
                var usuarioBL = new UsuarioBL();
                usuarioInfo = usuarioBL.ValidarUsuarioCompleto(usuarioActiveDirectory, contraseña);
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
        /// Obtiene un usuario con su rol
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <returns></returns>
        public UsuarioInfo ObtenerSupervisorID(int usuarioID)
        {
            UsuarioInfo usuarioInfo;
            try
            {
                Logger.Info();
                var usuarioBL = new UsuarioBL();
                usuarioInfo = usuarioBL.ObtenerSupervisorID(usuarioID);
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

        public UsuarioInfo ObtenerNivelAlertaPorUsuarioID(int usuarioID)
        {
            UsuarioInfo usuarioInfo = new UsuarioInfo();
            try
            {
                Logger.Info();
                var usuarioBL = new UsuarioBL();
                usuarioInfo = usuarioBL.ObtenerNivelAlertaPorUsuarioID(usuarioID);
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
        public List<UsuarioInfo> ObtenerCorreos(List<int> roles)
        {
            var usuarioBL = new UsuarioBL();
            var correos = new List<UsuarioInfo>();

            try
            {
                correos = usuarioBL.ObtenerCorreos(roles);
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
