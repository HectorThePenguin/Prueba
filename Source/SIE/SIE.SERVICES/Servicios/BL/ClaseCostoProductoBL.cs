using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class ClaseCostoProductoBL
    {        
        /// <summary>
        ///  Obtiene una entidad de ClaseCostoProducto por producto y almacén
        /// </summary>
        /// <param name="productoId"></param>
        /// <param name="almacenId"></param>
        /// <returns></returns>
        public ClaseCostoProductoInfo ObtenerPorProductoAlmacen(int productoId, int almacenId)
        {
            try
            {
                Logger.Info();
                var claseCostoProductoDAL = new Integracion.DAL.Implementacion.ClaseCostoProductoDAL();
                ClaseCostoProductoInfo result = claseCostoProductoDAL.ObtenerPorProductoAlmacen(productoId, almacenId);
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
        ///  Obtiene una entidad de ClaseCostoProducto por almacén
        /// </summary>
        /// <param name="almacenId"></param>
        /// <returns></returns>
        public IList<ClaseCostoProductoInfo> ObtenerPorAlmacen(int almacenId)
        {
            try
            {
                Logger.Info();
                var claseCostoProductoDAL = new Integracion.DAL.Implementacion.ClaseCostoProductoDAL();
                IList<ClaseCostoProductoInfo> claseCostosProductos = claseCostoProductoDAL.ObtenerPorAlmacen(almacenId);
                return claseCostosProductos;
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
        /// Metodo para Guardar/Modificar una entidad ClaseCostoProducto
        /// </summary>
        /// <param name="info"></param>
        public int Guardar(ClaseCostoProductoInfo info)
        {
            try
            {
                Logger.Info();
                var claseCostoProductoDAL = new ClaseCostoProductoDAL();
                int result = info.ClaseCostoProductoID;
                if (info.ClaseCostoProductoID == 0)
                {
                    result = claseCostoProductoDAL.Crear(info);
                }
                else
                {
                    claseCostoProductoDAL.Actualizar(info);
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
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ClaseCostoProductoInfo> ObtenerPorPagina(PaginacionInfo pagina, ClaseCostoProductoInfo filtro)
        {
            try
            {
                Logger.Info();
                var claseCostoProductoDAL = new ClaseCostoProductoDAL();
                ResultadoInfo<ClaseCostoProductoInfo> result = claseCostoProductoDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de ClaseCostoProducto
        /// </summary>
        /// <returns></returns>
        public IList<ClaseCostoProductoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var claseCostoProductoDAL = new ClaseCostoProductoDAL();
                IList<ClaseCostoProductoInfo> result = claseCostoProductoDAL.ObtenerTodos();
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
        /// Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<ClaseCostoProductoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var claseCostoProductoDAL = new ClaseCostoProductoDAL();
                IList<ClaseCostoProductoInfo> result = claseCostoProductoDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad ClaseCostoProducto por su Id
        /// </summary>
        /// <param name="claseCostoProductoID">Obtiene una entidad ClaseCostoProducto por su Id</param>
        /// <returns></returns>
        public ClaseCostoProductoInfo ObtenerPorID(int claseCostoProductoID)
        {
            try
            {
                Logger.Info();
                var claseCostoProductoDAL = new ClaseCostoProductoDAL();
                ClaseCostoProductoInfo result = claseCostoProductoDAL.ObtenerPorID(claseCostoProductoID);
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
        /// Obtiene una entidad ClaseCostoProducto por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public ClaseCostoProductoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var claseCostoProductoDAL = new ClaseCostoProductoDAL();
                ClaseCostoProductoInfo result = claseCostoProductoDAL.ObtenerPorDescripcion(descripcion);
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
    }
}
