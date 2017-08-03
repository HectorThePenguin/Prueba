using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class OperadorDAL : DALBase
    {
        /// <summary>
        ///     Obtiene un lista paginada de operadores por el filtro especificado
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<OperadorInfo> ObtenerPorPagina(PaginacionInfo pagina, OperadorInfo filtro)
        {
            ResultadoInfo<OperadorInfo> operadorLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxOperadorDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Operador_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    operadorLista = MapOperadorDAL.ObtenerPorPagina(ds);
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
            return operadorLista;
        }

        /// <summary>
        ///     Obtiene un operador por Id
        /// </summary>
        /// <param name="operadorID"></param>
        /// <returns></returns>
        internal OperadorInfo ObtenerPorID(int operadorID)
        {
            OperadorInfo operadorInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxOperadorDAL.ObtenerParametroPorID(operadorID);
                DataSet ds = Retrieve("Operador_ObtenerPorID", parameters);
                if (ValidateDataSet(ds))
                {
                    operadorInfo = MapOperadorDAL.ObtenerPorID(ds);
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
            return operadorInfo;
        }

        /// <summary>
        ///     Obtiene  operadores por Id rol
        /// </summary>
        /// <param name="rolID"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal IList<OperadorInfo> ObtenerPorIDRol(int organizacionID, int rolID)
        {
            IList<OperadorInfo> operadorInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxOperadorDAL.ObtenerParametrosPorIdRol(organizacionID,rolID);
                DataSet ds = Retrieve("Operadores_ObtenerPorIDRol", parameters);
                if (ValidateDataSet(ds))
                {
                    operadorInfo = MapOperadorDAL.ObtenerPorIdRol(ds);
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
            return operadorInfo;
        }

        /// <summary>
        ///  Obtiene  operadores por Id rol
        /// </summary>
        /// <param name="supervisorId"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal IList<OperadorInfo> ObtenerPorIdRolDetector(int organizacionId, int supervisorId)
        {
            IList<OperadorInfo> operadorInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxOperadorDAL.ObtenerParametrosPorSupervisorDetector(organizacionId, supervisorId);
                DataSet ds = Retrieve("SupervicionTecnicaDetectores_Detectores", parameters);
                if (ValidateDataSet(ds))
                {
                    operadorInfo = MapOperadorDAL.ObtenerPorIdRol(ds);
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
            return operadorInfo;
        }

        /// <summary>
        /// Obtiene un Operador
        /// </summary>
        /// <param name="operadorInfo"></param>
        /// <returns></returns>
        internal OperadorInfo ObtenerPorOperador(OperadorInfo operadorInfo)
        {
            OperadorInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxOperadorDAL.ObtenerParametroPorOperador(operadorInfo);
                DataSet ds = Retrieve("Operador_ObtenerPorRolOrganizacion", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapOperadorDAL.ObtenerPorOperador(ds);
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
        /// Metodo para Crear un registro de Operador
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(OperadorInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxOperadorDAL.ObtenerParametrosCrear(info);
                int result = Create("Operador_Crear", parameters);
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
        /// Metodo para actualizar un registro de Operador
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(OperadorInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxOperadorDAL.ObtenerParametrosActualizar(info);
                Update("Operador_Actualizar", parameters);
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
        /// Obtiene un registro de Operador
        /// </summary>
        /// <param name="descripcion">Descripción de la Operador</param>
        /// <returns></returns>
        internal OperadorInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxOperadorDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("Operador_ObtenerPorDescripcion", parameters);
                OperadorInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapOperadorDAL.ObtenerPorDescripcion(ds);
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
                Dictionary<string, object> parameters = AuxOperadorDAL.ObtenerParametrosPorUsuarioIdRol(usuarioId, organizacionId, basculista);
                DataSet ds = Retrieve("Operador_ObtenerPorUsuarioIdRol", parameters);
                OperadorInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapOperadorDAL.ObtenerPorUsuarioIdRol(ds);
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
        ///     Obtiene un operador por IdUsuario
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <returns></returns>
        internal OperadorInfo ObtenerPorUsuarioID(int usuarioID, int OrganizacionID)
        {
            OperadorInfo operadorInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxOperadorDAL.ObtenerParametroPorUsuarioID(usuarioID, OrganizacionID);
                DataSet ds = Retrieve("Operador_ObtenerPorUsuarioID", parameters);
                if (ValidateDataSet(ds))
                {
                    operadorInfo = MapOperadorDAL.ObtenerPorUsuarioID(ds);
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
            return operadorInfo;
        }

        internal OperadorInfo ObtenerDetectorCorral(int organizacionId, int corralID, int operadorId)
        {
            OperadorInfo operadorInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxOperadorDAL.ObtenerParametroObtenerDetectorCorral(organizacionId, corralID, operadorId);
                DataSet ds = Retrieve("DeteccionGanado_ObtenerDetectorCorral", parameters);
                if (ValidateDataSet(ds))
                {
                    operadorInfo = MapOperadorDAL.ObtenerPorUsuarioID(ds);
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
            return operadorInfo;
        }

        /// <summary>
        /// Obtiene el operador del corral indicado
        /// </summary>
        /// <param name="codigoCorral"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal OperadorInfo ObtenerPorCodigoCorral(string codigoCorral, int organizacionID)
        {
            OperadorInfo operadorInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxOperadorDAL.ObtenerParametroPorCodigoCorral(codigoCorral, organizacionID);
                DataSet ds = Retrieve("Operador_ObtenerPorCodigoCorral", parameters);
                if (ValidateDataSet(ds))
                {
                    operadorInfo = MapOperadorDAL.ObtenerPorUsuarioID(ds);
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
            return operadorInfo;
        }

        internal IList<NotificacionInfo> ObtenerNotificacionesDeteccionLista(int organizacionId, int operadorId)
        {
            IList<NotificacionInfo> operadorInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxOperadorDAL.ObtenerParametroNotificacionesDeteccionLista(organizacionId, operadorId);
                DataSet ds = Retrieve("DeteccionGanado_ObtenerNotificaciones", parameters);
                if (ValidateDataSet(ds))
                {
                    operadorInfo = MapOperadorDAL.ObtenerNotificacionesDeteccionLista(ds);
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
            return operadorInfo;
        }

        /// <summary>
        /// Obtiene un Operador
        /// </summary>
        /// <param name="operadorInfo"></param>
        /// <returns></returns>
        internal OperadorInfo ObtenerPorOperadorOrganizacion(OperadorInfo operadorInfo)
        {
            OperadorInfo resultado = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxOperadorDAL.ObtenerParametroOperadorPorOrganizacion(operadorInfo);
                DataSet ds = Retrieve("Operador_ObtenerPorOrganizacion", parameters);
                if (ValidateDataSet(ds))
                {
                    resultado = MapOperadorDAL.ObtenerOperadorPorOrganizacion(ds);
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
            return resultado;
        }
        /// <summary>
        /// Obtiene el Operador que esta asignado al Corral 
        /// </summary>
        /// <param name="codigoCorral"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal OperadorInfo ObtenerLectorPorCodigoCorral(string codigoCorral, int organizacionId)
        {
            OperadorInfo operadorInfo = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxOperadorDAL.ObtenerParametroPorCodigoCorral(codigoCorral, organizacionId);
                DataSet ds = Retrieve("Operador_ObtenerLectorPorCodigoCorral", parameters);
                if (ValidateDataSet(ds))
                {
                    operadorInfo = MapOperadorDAL.ObtenerPorUsuarioID(ds);
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
            return operadorInfo;
        }

        /// <summary>
        /// Obtiene una lista de Operadores
        /// </summary>
        /// <returns></returns>
        public IList<OperadorInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Operador_ObtenerTodos");
                IList<OperadorInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapOperadorDAL.ObtenerTodos(ds);
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
        /// Obtiene un lista paginada de operadores por el filtro especificado
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<OperadorInfo> ObtenerSoloRolSanidadPorPagina(PaginacionInfo pagina, OperadorInfo filtro)
        {
            try
            {
                string nombre = filtro.NombreCompleto.ToLower().Trim();

                var result = new ResultadoInfo<OperadorInfo>();
                var roles = new List<RolInfo>
                                          {
                                              new RolInfo {RolID = Roles.SupervisorSanidad.GetHashCode() },
                                              new RolInfo {RolID = Roles.JefeSanidad.GetHashCode()},
                                          };

                IList<OperadorInfo> operadores = ObtenerTodos();
                
                var condicion = (from o in operadores
                                 join r in roles on o.Rol.RolID equals r.RolID
                                 where o.Activo == filtro.Activo
                                       && o.Organizacion.OrganizacionID == filtro.Organizacion.OrganizacionID
                                 select o);

                
                if (!string.IsNullOrWhiteSpace(nombre))
                {
                    condicion = condicion.Where(e => e.NombreCompleto.ToLower().Contains(nombre));
                }

                var operadorInfos = condicion as List<OperadorInfo> ?? condicion.ToList();

                result.TotalRegistros = operadorInfos.Count();

                int inicio = pagina.Inicio;
                int limite = pagina.Limite;
                if (inicio > 1)
                {
                    int limiteReal = (limite - inicio) + 1;
                    inicio = (limite / limiteReal);
                    limite = limiteReal;
                }
                var paginado = operadorInfos
                                .OrderBy(e => e.NombreCompleto)
                                .Skip((inicio - 1) * limite)
                                .Take(limite);

                result.Lista = paginado.ToList();


                var listaRoles = new RolDAL().ObtenerTodos();

                foreach (var registro in result.Lista)
                {
                    registro.Rol = listaRoles.FirstOrDefault(e => e.RolID == registro.Rol.RolID);
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
        /// Obtiene un lista paginada de operadores por el filtro especificado
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<OperadorInfo> ObtenerSoloRolBasculistaPorPagina(PaginacionInfo pagina, OperadorInfo filtro)
        {
            try
            {
                string nombre = filtro.NombreCompleto.ToLower().Trim();

                var result = new ResultadoInfo<OperadorInfo>();
                var roles = new List<RolInfo>
                                          {
                                              new RolInfo {RolID = Roles.Basculista.GetHashCode() },
                                          };

                IList<OperadorInfo> operadores = ObtenerTodos();

                var condicion = (from o in operadores
                                 join r in roles on o.Rol.RolID equals r.RolID
                                 where o.Activo == filtro.Activo
                                       && o.Organizacion.OrganizacionID == filtro.Organizacion.OrganizacionID
                                 select o);


                if (!string.IsNullOrWhiteSpace(nombre))
                {
                    condicion = condicion.Where(e => e.NombreCompleto.ToLower().Contains(nombre));
                }

                var operadorInfos = condicion as List<OperadorInfo> ?? condicion.ToList();

                result.TotalRegistros = operadorInfos.Count();

                int inicio = pagina.Inicio;
                int limite = pagina.Limite;
                if (inicio > 1)
                {
                    int limiteReal = (limite - inicio) + 1;
                    inicio = (limite / limiteReal);
                    limite = limiteReal;
                }
                var paginado = operadorInfos
                                .OrderBy(e => e.NombreCompleto)
                                .Skip((inicio - 1) * limite)
                                .Take(limite);

                result.Lista = paginado.ToList();


                var listaRoles = new RolDAL().ObtenerTodos();

                foreach (var registro in result.Lista)
                {
                    registro.Rol = listaRoles.FirstOrDefault(e => e.RolID == registro.Rol.RolID);
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
        /// Obtiene un operador por Id solo los roles que pertenecen a sanidad
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal OperadorInfo ObtenerSoloRolBasculistaPorID(OperadorInfo filtro)
        {
            try
            {
                Logger.Info();

                var roles = new List<RolInfo>
                                          {
                                              new RolInfo {RolID = Roles.Basculista.GetHashCode() }
                                          };

                IList<OperadorInfo> operadores = new List<OperadorInfo>();
                OperadorInfo operadorInfo = ObtenerPorID(filtro.OperadorID);

                if(operadorInfo!=null)
                {
                    operadores.Add(operadorInfo);
                }

                var condicion = (from o in operadores
                                 join r in roles on o.Rol.RolID equals r.RolID
                                 where o.Activo == filtro.Activo
                                       && o.Organizacion.OrganizacionID == filtro.Organizacion.OrganizacionID
                                 select o).FirstOrDefault();

                OperadorInfo result = condicion;
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
        /// Obtiene un Folio por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal OperadorInfo ObtenerBasculistaPorId(long id)
        {
            OperadorInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxOperadorDAL.ObtenerBasculistaPorId(id);
                DataSet ds = Retrieve("Operador_ObtenerPorRolIDUsuarioID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapOperadorDAL.ObtenerBasculistaPorId(ds);
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
        /// Obtiene los folios por pagina
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<OperadorInfo> ObtenerPorPaginaFiltroBasculista(PaginacionInfo pagina, OperadorInfo filtro)
        {
            ResultadoInfo<OperadorInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxOperadorDAL.ObtenerParametrosPorPaginaFiltroBasculista(pagina, filtro);
                DataSet ds = Retrieve("Operador_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapOperadorDAL.ObtenerPorPaginaCompletoBasculista(ds);
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

            return lista;
        }

        /// <summary>
        /// Obtiene un operador por Id solo los roles que pertenecen a sanidad
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal OperadorInfo ObtenerSoloRolSanidadPorID(OperadorInfo filtro)
        {
            try
            {
                Logger.Info();

                var roles = new List<RolInfo>
                                          {
                                              new RolInfo {RolID = Roles.SupervisorSanidad.GetHashCode() },
                                              new RolInfo {RolID = Roles.JefeSanidad.GetHashCode()},
                                          };

                IList<OperadorInfo> operadores = new List<OperadorInfo>();
                OperadorInfo operadorInfo = ObtenerPorID(filtro.OperadorID);

                if(operadorInfo!=null)
                {
                    operadores.Add(operadorInfo);
                }

                var condicion = (from o in operadores
                                 join r in roles on o.Rol.RolID equals r.RolID
                                 where o.Activo == filtro.Activo
                                       && o.Organizacion.OrganizacionID == filtro.Organizacion.OrganizacionID
                                 select o).FirstOrDefault();

                OperadorInfo result = condicion;
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