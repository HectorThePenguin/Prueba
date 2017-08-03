using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.ORM;

namespace SIE.Services.Servicios.BL
{
    public class SupervisorEnfermeriaBL : IDisposable
    {
        SupervisorEnfermeriaDAL supervisorEnfermeriaDAL;

        public SupervisorEnfermeriaBL()
        {
            supervisorEnfermeriaDAL = new SupervisorEnfermeriaDAL();
        }

        public void Dispose()
        {
            supervisorEnfermeriaDAL.Disposed += (s, e) =>
            {
                supervisorEnfermeriaDAL = null;
            };
            supervisorEnfermeriaDAL.Dispose();
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
                ResultadoInfo<SupervisorEnfermeriaInfo> lista = supervisorEnfermeriaDAL.ObtenerPorPagina(pagina, filtro);

                var listaOperador = new OperadorBL().ObtenerTodos();

                foreach (var registro in lista.Lista)
                {
                    registro.Enfermeria = new EnfermeriaBL().ObtenerPorID(registro.EnfermeriaID);
                    registro.Operador = listaOperador.FirstOrDefault(e => e.OperadorID == registro.OperadorID);
                }
                return lista;
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
        public IList<SupervisorEnfermeriaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return supervisorEnfermeriaDAL.ObtenerTodos().ToList();
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
        public IList<SupervisorEnfermeriaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return supervisorEnfermeriaDAL.ObtenerTodos().Where(e=> e.Activo == estatus).ToList();
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
                return supervisorEnfermeriaDAL.ObtenerTodos().FirstOrDefault(e => e.SupervisorEnfermeriaID == supervisorEnfermeriaId);
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
        /// <param name="filtro">Obtiene una entidad SupervisorEnfermeria por su Id</param>
        /// <returns></returns>
        public SupervisorEnfermeriaInfo ObtenerPorEnfermeriaOperador(SupervisorEnfermeriaInfo filtro)
        {
            try
            {
                Logger.Info();
                return supervisorEnfermeriaDAL.ObtenerPorEnfermeriaOperador(filtro);
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

        ///// <summary>
        ///// Obtiene una entidad de SupervisorEnfermeria por su descripcion
        ///// </summary>
        ///// <param name="descripcion">Obtiene una entidad SupervisorEnfermeria por su descripcion</param>
        ///// <returns></returns>
        //public SupervisorEnfermeriaInfo ObtenerPorDescripcion(string descripcion)
        //{
        //    try
        //    {
        //        Logger.Info();
        //        return supervisorEnfermeriaDAL.ObtenerTodos().Where(e=> e.SupervisorEnfermeriaID.ToLower() == descripcion.ToLower()).FirstOrDefault();
        //    }
        //    catch(ExcepcionGenerica)
        //    {
        //        throw;
        //    }
        //    catch(Exception ex)
        //    {
        //        Logger.Error(ex);
        //        throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
        //    }
        //}

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
                return supervisorEnfermeriaDAL.Guardar(info);
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

        ///// <summary>
        ///// Obtiene una entidad de SupervisorEnfermeria por su Id
        ///// </summary>
        ///// <param name="supervisorEnfermeriaId">Obtiene una entidad SupervisorEnfermeria por su Id</param>
        ///// <returns></returns>
        //public SupervisorEnfermeriaInfo ObtenerPorEnfermeria(int supervisorEnfermeriaId)
        //{
        //    try
        //    {
        //        Logger.Info();
        //        return supervisorEnfermeriaDAL.ObtenerTodos().FirstOrDefault(e => e.SupervisorEnfermeriaID == supervisorEnfermeriaId);
        //    }
        //    catch (ExcepcionGenerica)
        //    {
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //        throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
        //    }
        //}
    }
}
