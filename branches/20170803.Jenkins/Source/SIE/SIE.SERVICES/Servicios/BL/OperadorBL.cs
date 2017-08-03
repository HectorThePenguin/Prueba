using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using System.Collections.Generic;

namespace SIE.Services.Servicios.BL
{
    internal class OperadorBL
    {
        /// <summary>
        ///     Obtiene un lista paginada de operadores por el filtro especificado
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<OperadorInfo> ObtenerPorPagina(PaginacionInfo pagina, OperadorInfo filtro)
        {
            ResultadoInfo<OperadorInfo> resultado;
            try
            {
                Logger.Info();
                var operadorDAL = new OperadorDAL();
                resultado = operadorDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un Operador por Id
        /// </summary>
        /// <param name="operadorID"></param>
        /// <returns></returns>
        internal OperadorInfo ObtenerPorID(int operadorID)
        {
            OperadorInfo operadorInfo;
            try
            {
                Logger.Info();
                var operadorDAL = new OperadorDAL();
                operadorInfo = operadorDAL.ObtenerPorID(operadorID);
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
        
        ///     Obtiene un Operadores por Id Rol
        internal IList<OperadorInfo> ObtenerPorIDRol(int organizacionID, int rolID)
        {
            IList<OperadorInfo> operadorInfo;
            try
            {
                Logger.Info();
                var operadorDAL = new OperadorDAL();
                operadorInfo = operadorDAL.ObtenerPorIDRol(organizacionID, rolID);
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
        /// Obtiene la lista de operadores detectores
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="supervisorId"></param>
        /// <returns></returns>
        internal IList<OperadorInfo> ObtenerPorIdRolDetector(int organizacionId, int supervisorId)
        {
            IList<OperadorInfo> operadorInfo;
            try
            {
                Logger.Info();
                var operadorDal = new OperadorDAL();
                operadorInfo = operadorDal.ObtenerPorIdRolDetector(organizacionId, supervisorId);
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
        internal OperadorInfo ObtenerPorOperador(OperadorInfo operadorInfo)
        {
            try
            {
                Logger.Info();
                var operadorDAL = new OperadorDAL();
                OperadorInfo result = operadorDAL.ObtenerPorOperador(operadorInfo);
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
        /// Obtiene una lista de Operadores paginado
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="operadorInfo"></param>
        /// <returns></returns>
        internal ResultadoInfo<OperadorInfo> ObtenerPorOperadorPaginado(PaginacionInfo pagina, OperadorInfo operadorInfo)
        {
            ResultadoInfo<OperadorInfo> resultado;
            try
            {
                Logger.Info();
                var operadorDAL = new OperadorDAL();
                resultado = operadorDAL.ObtenerPorPagina(pagina, operadorInfo);
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
        /// Metodo para Guardar/Modificar una entidad Operador
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(OperadorInfo info)
        {
            try
            {
                Logger.Info();
                var operadorDAL = new OperadorDAL();
                int result = info.OperadorID;
                if (info.OperadorID == 0)
                {
                    result = operadorDAL.Crear(info);
                }
                else
                {
                    operadorDAL.Actualizar(info);
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
        /// Obtiene una entidad Operador por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad Operador por su descripción</param>
        /// <returns></returns>
        internal OperadorInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var operadorDAL = new OperadorDAL();
                OperadorInfo result = operadorDAL.ObtenerPorDescripcion(descripcion);
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
        internal OperadorInfo ObtenerPorUsuarioIdRol(int usuarioId, int organizacionId, Roles basculista)
        {
            try
            {
                Logger.Info();
                var operadorDAL = new OperadorDAL();
                OperadorInfo result = operadorDAL.ObtenerPorUsuarioIdRol(usuarioId, organizacionId, basculista);
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

        internal OperadorInfo ObtenerDetectorCorral(int organizacionId, int corralID, int operadorId)
        {
            try
            {
                Logger.Info();
                var operadorDAL = new OperadorDAL();
                OperadorInfo result = operadorDAL.ObtenerDetectorCorral(organizacionId, corralID, operadorId);
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
        internal OperadorInfo ObtenerPorCodigoCorral(string codigoCorral, int organizacionId)
        {
            try
            {
                Logger.Info();
                var operadorDAL = new OperadorDAL();
                OperadorInfo result = operadorDAL.ObtenerPorCodigoCorral(codigoCorral, organizacionId);
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

        internal IList<NotificacionInfo> ObtenerNotificacionesDeteccionLista(int organizacionId, int operadorId)
        {
            IList<NotificacionInfo> operadorInfo;
            try
            {
                Logger.Info();
                var operadorDAL = new OperadorDAL();
                operadorInfo = operadorDAL.ObtenerNotificacionesDeteccionLista(organizacionId, operadorId);
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
        internal OperadorInfo ObtenerPorOperadorOrganizacion(OperadorInfo operadorInfo)
        {
            try
            {
                Logger.Info();
                var operadorDAL = new OperadorDAL();
                OperadorInfo result = operadorDAL.ObtenerPorOperadorOrganizacion(operadorInfo);
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
        internal OperadorInfo ObtenerLectorPorCodigoCorral(string codigoCorral, int organizacionId)
        {
            try
            {
                Logger.Info();
                var operadorDAL = new OperadorDAL();
                OperadorInfo result = operadorDAL.ObtenerLectorPorCodigoCorral(codigoCorral, organizacionId);
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
        /// Obtiene un lista de Operador
        /// </summary>
        /// <returns></returns>
        public IList<OperadorInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var operadorDAL = new OperadorDAL();
                IList<OperadorInfo> result = operadorDAL.ObtenerTodos();
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
        /// Obtiene una lista de Operadores paginado
        /// solo con los roles de sanidad.
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<OperadorInfo> ObtenerSoloRolSanidadPorPagina(PaginacionInfo pagina, OperadorInfo filtro)
        {
            ResultadoInfo<OperadorInfo> resultado;
            try
            {
                Logger.Info();
                var operadorDAL = new OperadorDAL();
                resultado = operadorDAL.ObtenerSoloRolSanidadPorPagina(pagina, filtro);
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
        /// Obtiene una lista de Operadores paginado
        /// solo con los roles de sanidad.
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<OperadorInfo> ObtenerSoloRolBasculistaPorPagina(PaginacionInfo pagina, OperadorInfo filtro)
        {
            ResultadoInfo<OperadorInfo> resultado;
            try
            {
                Logger.Info();
                var operadorDAL = new OperadorDAL();
                resultado = operadorDAL.ObtenerSoloRolBasculistaPorPagina(pagina, filtro);
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
        /// Obtiene un Operador por Id solo rol sanidad
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal OperadorInfo ObtenerSoloRolSanidadPorID(OperadorInfo filtro)
        {
            OperadorInfo operadorInfo;
            try
            {
                Logger.Info();
                var operadorDAL = new OperadorDAL();
                operadorInfo = operadorDAL.ObtenerSoloRolSanidadPorID(filtro);
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
        /// Obtiene un Operador por Id solo rol sanidad
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal OperadorInfo ObtenerSoloRolBasculistaPorID(OperadorInfo filtro)
        {
            OperadorInfo operadorInfo;
            try
            {
                Logger.Info();
                var operadorDAL = new OperadorDAL();
                operadorInfo = operadorDAL.ObtenerSoloRolBasculistaPorID(filtro);
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
        /// Obtiene el primer operador del usuario, independientemente del rol
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        internal OperadorInfo ObtenerPorUsuarioId(int usuarioId, int organizacionID)
        {
            OperadorInfo operadorInfo;
            try
            {
                Logger.Info();
                var operadorDAL = new OperadorDAL();
                operadorInfo = operadorDAL.ObtenerPorUsuarioID(usuarioId, organizacionID);
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
        /// Obtiene los datos del folio para la ayuda consultado de basculamultipesaje
        /// </summary>
        /// <param name="folioId"></param>
        /// <returns></returns>
        public OperadorInfo ObtenerBasculistaPorId(long folioId)
        {
            OperadorInfo info;
            try
            {
                Logger.Info();
                var basculaMultipesajeDal = new OperadorDAL();
                info = basculaMultipesajeDal.ObtenerBasculistaPorId(folioId);
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
        /// Obtiene una lista de los folios de la tabla BasculaMultipesaje
        /// </summary>
        /// <param name="pagina"></param>   
        /// <param name="filtro"></param>    
        /// <returns></returns>
        internal ResultadoInfo<OperadorInfo> ObtenerPorPaginaBasculista(PaginacionInfo pagina, OperadorInfo filtro)
        {
            ResultadoInfo<OperadorInfo> result;
            try
            {
                Logger.Info();
                OperadorDAL basculaMultipesajeDal = new OperadorDAL();
                result = basculaMultipesajeDal.ObtenerPorPaginaFiltroBasculista(pagina, filtro);
            }
            catch (ExcepcionGenerica ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return result;
        }
    }
}
