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
    public class AlmacenProductoCuentaBL : IDisposable
    {
        AlmacenProductoCuentaDAL almacenProductoCuentaDAL;

        public AlmacenProductoCuentaBL()
        {
            almacenProductoCuentaDAL = new AlmacenProductoCuentaDAL();
        }

        public void Dispose()
        {
            almacenProductoCuentaDAL.Disposed += (s, e) =>
            {
                almacenProductoCuentaDAL = null;
            };
            almacenProductoCuentaDAL.Dispose();
        }
        
        /// <summary>
        /// Obtiene una lista paginada de AlmacenProductoCuenta
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<AlmacenProductoCuentaInfo> ObtenerPorPagina(PaginacionInfo pagina, AlmacenProductoCuentaInfo filtro)
        {
            try
            {
                Logger.Info();
                return almacenProductoCuentaDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una lista de AlmacenProductoCuenta
        /// </summary>
        /// <returns></returns>
        public IList<AlmacenProductoCuentaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                return almacenProductoCuentaDAL.ObtenerTodos().ToList();
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
        /// Obtiene una lista de AlmacenProductoCuenta filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public IList<AlmacenProductoCuentaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                return almacenProductoCuentaDAL.ObtenerTodos().Where(e=> e.Activo == estatus).ToList();
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
        /// Obtiene una entidad de AlmacenProductoCuenta por su Id
        /// </summary>
        /// <param name="almacenProductoCuentaId">Obtiene una entidad AlmacenProductoCuenta por su Id</param>
        /// <returns></returns>
        public AlmacenProductoCuentaInfo ObtenerPorID(int almacenProductoCuentaId)
        {
            try
            {
                Logger.Info();
                return almacenProductoCuentaDAL.ObtenerTodos().Where(e=> e.AlmacenProductoCuentaID == almacenProductoCuentaId).FirstOrDefault();
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
        /// Metodo para Guardar/Modificar una entidad AlmacenProductoCuenta
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Guardar(AlmacenProductoCuentaInfo info)
        {
            try
            {
                Logger.Info();
                return almacenProductoCuentaDAL.Guardar(info);
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
        /// Obtiene una entidad de AlmacenProductoCuenta almacen id y producto id
        /// </summary>
        /// <param name="info">Obtiene una entidad AlmacenProductoCuenta por almacen id y producto id</param>
        /// <returns></returns>
        public AlmacenProductoCuentaInfo ObtenerPorAlmacenIDProductoID(AlmacenProductoCuentaInfo info)
        {
            try
            {
                Logger.Info();
                return almacenProductoCuentaDAL.ObtenerPorAlmacenIDProductoID(info);
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
        /// Obtiene los productos cuenta por clave de
        /// almacen
        /// </summary>
        /// <param name="almacenID"></param>
        /// <returns></returns>
        public List<AlmacenProductoCuentaInfo> ObtenerPorAlmacenID(int almacenID)
        {
            try
            {
                Logger.Info();
                return almacenProductoCuentaDAL.ObtenerPorAlmacenID(almacenID);
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
    }
}
