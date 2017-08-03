using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.ORM
{
    internal class SupervisorEnfermeriaDAL : BaseDAL
    {
        SupervisorEnfermeriaAccessor supervisorEnfermeriaAccessor;

        protected override void inicializar()
        {
            supervisorEnfermeriaAccessor = da.inicializarAccessor<SupervisorEnfermeriaAccessor>();
        }

        protected override void destruir()
        {
            supervisorEnfermeriaAccessor = null;
        }

        /// <summary>
        /// Obtiene una lista paginada de SupervisorEnfermeria
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<SupervisorEnfermeriaInfo> ObtenerPorPagina(PaginacionInfo pagina, SupervisorEnfermeriaInfo filtro)
        {
            try
            {
                Logger.Info();
                var result = new ResultadoInfo<SupervisorEnfermeriaInfo>();

                List<SupervisorEnfermeriaInfo> condicion = supervisorEnfermeriaAccessor.ObtenerPorPagina(
                    filtro.Enfermeria.OrganizacionInfo.OrganizacionID,
                    filtro.SupervisorEnfermeriaID,
                    filtro.OperadorID,
                    filtro.EnfermeriaID,
                    filtro.Activo == EstatusEnum.Activo
                    );

                result.TotalRegistros = condicion.Count();
                
                int inicio = pagina.Inicio;
                int limite = pagina.Limite;
                if (inicio > 1)
                {
                    int limiteReal = (limite - inicio) + 1;
                    inicio = (limite / limiteReal);
                    limite = limiteReal;
                }
                var paginado = condicion
                                .OrderBy(e => e.SupervisorEnfermeriaID)
                                .Skip((inicio - 1) * limite)
                                .Take(limite);

                result.Lista = paginado.ToList();

                return result;
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de SupervisorEnfermeria
        /// </summary>
        /// <returns></returns>
        public IQueryable<SupervisorEnfermeriaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return da.Tabla<SupervisorEnfermeriaInfo>();
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de SupervisorEnfermeria filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IQueryable<SupervisorEnfermeriaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().Where(e=> e.Activo == estatus);
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad de SupervisorEnfermeria por su Id
        /// </summary>
        /// <param name="supervisorEnfermeriaId">Obtiene una entidad SupervisorEnfermeria por su Id</param>
        /// <returns></returns>
        public SupervisorEnfermeriaInfo ObtenerPorID(int supervisorEnfermeriaId)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().FirstOrDefault(e => e.SupervisorEnfermeriaID == supervisorEnfermeriaId);
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una entidad de SupervisorEnfermeria por su Id
        /// </summary>
        /// <param name="filtro">Obtiene una entidad SupervisorEnfermeria por EnfermeríaId y OperadorId</param>
        /// <returns></returns>
        public SupervisorEnfermeriaInfo ObtenerPorEnfermeriaOperador(SupervisorEnfermeriaInfo filtro)
        {
            try
            {
                Logger.Info();
                return this.ObtenerTodos().FirstOrDefault(e => e.EnfermeriaID == filtro.Enfermeria.EnfermeriaID
                                                               && e.OperadorID == filtro.Operador.OperadorID
                    );
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
        /// Metodo para Guardar/Modificar una entidad SupervisorEnfermeria
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(SupervisorEnfermeriaInfo info)
        {
            try
            {
                Logger.Info();
                var id = 0;
                if (info.SupervisorEnfermeriaID > 0)
                {
                    info.FechaModificacion = da.FechaServidor();
                    id = da.Actualizar<SupervisorEnfermeriaInfo>(info);

                    //da.Actualizar<SupervisorEnfermeriaInfo>(
                    //    e => e.SupervisorEnfermeriaID == info.SupervisorEnfermeriaID
                    //    , e => new SupervisorEnfermeriaInfo()
                    //               {
                    //                   EnfermeriaID = info.EnfermeriaID,
                    //                   OperadorID = info.OperadorID,
                    //                   Activo = info.Activo
                    //               }
                    //    );
                }
                else
                {
                    id = da.Insertar<SupervisorEnfermeriaInfo>(info);
                }
                return id;
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
