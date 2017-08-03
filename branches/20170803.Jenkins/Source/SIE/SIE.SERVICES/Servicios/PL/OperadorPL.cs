using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using System.Collections.Generic;

namespace SIE.Services.Servicios.PL
{
    public class OperadorPL
    {
        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<OperadorInfo> ObtenerPorPagina(PaginacionInfo pagina, OperadorInfo filtro)
        {
            ResultadoInfo<OperadorInfo> resultado;
            try
            {
                Logger.Info();
                var operadorBL = new OperadorBL();
                resultado = operadorBL.ObtenerPorPagina(pagina, filtro);
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
        ///     Obtiene un operador por Id
        /// </summary>
        /// <param name="operadorID"></param>
        /// <returns></returns>
        public OperadorInfo ObtenerPorID(int operadorID)
        {
            OperadorInfo operadorInfo;
            try
            {
                Logger.Info();
                var operadorBL = new OperadorBL();
                operadorInfo = operadorBL.ObtenerPorID(operadorID);
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
            return operadorInfo;
        }

        /// <summary>
        /// Obtiene un Operador
        /// </summary>
        /// <param name="operadorInfo"></param>
        /// <returns></returns>
        public OperadorInfo ObtenerPorOperador(OperadorInfo operadorInfo)
        {
            OperadorInfo resultado;
            try
            {
                Logger.Info();
                var operadorBL = new OperadorBL();
                resultado = operadorBL.ObtenerPorOperador(operadorInfo);
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
        /// Obtiene un Operador
        /// </summary>
        /// <param name="operadorInfo"></param>
        /// <returns></returns>
        public OperadorInfo ObtenerPorID(OperadorInfo operadorInfo)
        {
            OperadorInfo resultado;
            try
            {
                Logger.Info();
                var operadorBL = new OperadorBL();
                resultado = operadorBL.ObtenerPorID(operadorInfo.OperadorID);
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
        /// Obtiene los Operadores por Paginado
        /// </summary>
        /// <param name="Pagina"></param>
        /// <param name="operadorInfo"></param>
        /// <returns></returns>
        public ResultadoInfo<OperadorInfo> ObtenerPorOperadorPaginado(PaginacionInfo Pagina, OperadorInfo operadorInfo)
        {
            ResultadoInfo<OperadorInfo> resultado;
            try
            {
                Logger.Info();
                var operadorBL = new OperadorBL();
                resultado = operadorBL.ObtenerPorOperadorPaginado(Pagina, operadorInfo);
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
        /// Obtiene el Operdor por IDRol
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="rolID"></param>
        /// <returns></returns>
        public IList<OperadorInfo> ObtenerPorIDRol(int organizacionID, int rolID)
        {
            IList<OperadorInfo> operadorInfo;
            try
            {
                Logger.Info();
                var operadorBL = new OperadorBL();
                operadorInfo = operadorBL.ObtenerPorIDRol(organizacionID, rolID);
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
            return operadorInfo;
        }

        /// <summary>
        /// Obtiene los Operadores por Paginado
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="supervisorId"></param>
        /// <returns></returns>
        public IList<OperadorInfo> ObtenerPorIdRolDetector(int organizacionId, int supervisorId)
        {
            IList<OperadorInfo> operadorInfo;
            try
            {
                Logger.Info();
                var operadorBl = new OperadorBL();
                operadorInfo = operadorBl.ObtenerPorIdRolDetector(organizacionId, supervisorId);
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
            return operadorInfo;
        }

        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Operador
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(OperadorInfo info)
        {
            try
            {
                Logger.Info();
                var operadorBL = new OperadorBL();
                int result = operadorBL.Guardar(info);
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
        public OperadorInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var operadorBL = new OperadorBL();
                OperadorInfo result = operadorBL.ObtenerPorDescripcion(descripcion);
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
        /// Obtiene un Operador que es Usuario
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <param name="organizacionId"> </param>
        /// <param name="basculista"></param>
        /// <returns></returns>
        public OperadorInfo ObtenerPorUsuarioIdRol(int usuarioId, int organizacionId, Roles basculista)
        {
            try
            {
                Logger.Info();
                var operadorBL = new OperadorBL();
                OperadorInfo result = operadorBL.ObtenerPorUsuarioIdRol(usuarioId, organizacionId, basculista);
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

        public OperadorInfo ObtenerDetectorCorral(int organizacionId, int corralID, int operadorId)
        {
            try
            {
                Logger.Info();
                var operadorBL = new OperadorBL();
                OperadorInfo result = operadorBL.ObtenerDetectorCorral(organizacionId, corralID, operadorId);
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
        /// Obtiene el Operador del corral indicado
        /// </summary>
        /// <param name="codigoCorral"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public OperadorInfo ObtenerPorCodigoCorral(string codigoCorral, int organizacionId)
        {
            try
            {
                Logger.Info();
                var operadorBL = new OperadorBL();
                OperadorInfo result = operadorBL.ObtenerPorCodigoCorral(codigoCorral, organizacionId);
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

        public IList<NotificacionInfo> ObtenerNotificacionesDeteccionLista(int organizacionId, int operadorId)
        {
            IList<NotificacionInfo> operadorInfo;
            try
            {
                Logger.Info();
                var operadorBL = new OperadorBL();
                operadorInfo = operadorBL.ObtenerNotificacionesDeteccionLista(organizacionId, operadorId);
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
            return operadorInfo;
        }

        /// <summary>
        /// Obtiene un Operador
        /// </summary>
        /// <param name="operadorInfo"></param>
        /// <returns></returns>
        public OperadorInfo ObtenerPorOperadorOrganizacion(OperadorInfo operadorInfo)
        {
            OperadorInfo resultado;
            try
            {
                Logger.Info();
                var operadorBL = new OperadorBL();
                resultado = operadorBL.ObtenerPorOperadorOrganizacion(operadorInfo);
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
        /// Obtiene el Operador del corral indicado
        /// </summary>
        /// <param name="codigoCorral"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public OperadorInfo ObtenerLectorPorCodigoCorral(string codigoCorral, int organizacionId)
        {
            try
            {
                Logger.Info();
                var operadorBL = new OperadorBL();
                OperadorInfo result = operadorBL.ObtenerLectorPorCodigoCorral(codigoCorral, organizacionId);
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
        /// Obtiene los Operadores por Paginado
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="operadorInfo"></param>
        /// <returns></returns>
        public ResultadoInfo<OperadorInfo> ObtenerSoloRolSanidadPorPagina(PaginacionInfo pagina, OperadorInfo operadorInfo)
        {
            ResultadoInfo<OperadorInfo> resultado;
            try
            {
                Logger.Info();
                var operadorBL = new OperadorBL();
                resultado = operadorBL.ObtenerSoloRolSanidadPorPagina(pagina, operadorInfo);
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
        /// Obtiene los Operadores por Paginado
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="operadorInfo"></param>
        /// <returns></returns>
        public ResultadoInfo<OperadorInfo> ObtenerSoloRolBasculistaPorPagina(PaginacionInfo pagina, OperadorInfo operadorInfo)
        {
            ResultadoInfo<OperadorInfo> resultado;
            try
            {
                Logger.Info();
                var operadorBL = new OperadorBL();
                resultado = operadorBL.ObtenerSoloRolBasculistaPorPagina(pagina, operadorInfo);
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
        ///     Obtiene un operador por Id
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public OperadorInfo ObtenerSoloRolSanidadPorID(OperadorInfo filtro)
        {
            try
            {
                Logger.Info();
                var operadorBL = new OperadorBL();
                OperadorInfo operadorInfo = operadorBL.ObtenerSoloRolSanidadPorID(filtro);
                return operadorInfo;
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
        ///     Obtiene un operador por Id
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public OperadorInfo ObtenerSoloRolBasculistaID(OperadorInfo filtro)
        {
            try
            {
                Logger.Info();
                var operadorBL = new OperadorBL();
                OperadorInfo operadorInfo = operadorBL.ObtenerSoloRolBasculistaPorID(filtro);
                return operadorInfo;
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
        /// Obtiene el primer operador del usuario, independientemente del rol
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        public OperadorInfo ObtenerPorUsuarioId(int usuarioId, int organizacionID)
        {
            OperadorInfo operadorInfo;
            try
            {
                Logger.Info();
                var operadorBl = new OperadorBL();
                operadorInfo = operadorBl.ObtenerPorUsuarioId(usuarioId, organizacionID);
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
            return operadorInfo;
        }

        public OperadorInfo ObtenerBasculistaPorId(OperadorInfo operadorInfo)
        {
            OperadorInfo info;
            try
            {
                Logger.Info();
                var basculaMultipesajeBl = new OperadorBL();
                info = basculaMultipesajeBl.ObtenerBasculistaPorId(operadorInfo.OperadorID);
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
        /// Obtiene un lista paginada de organizaciones tipo centros, cadis, descansos
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<OperadorInfo> ObtenerPorPaginaOperadorBasculistaId(PaginacionInfo pagina, OperadorInfo filtro)
        {
            ResultadoInfo<OperadorInfo> resultadoFolios;
            try
            {
                Logger.Info();
                var basculaMultipesajeBl = new OperadorBL();
                resultadoFolios = basculaMultipesajeBl.ObtenerPorPaginaBasculista(pagina, filtro);
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
            return resultadoFolios;
        }
    }
}