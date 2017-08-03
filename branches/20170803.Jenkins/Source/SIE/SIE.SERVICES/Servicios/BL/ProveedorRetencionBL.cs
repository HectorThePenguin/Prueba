using System;
using System.Collections.Generic;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    public class ProveedorRetencionBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad ProveedorRetencion
        /// </summary>
        /// <param name="info"></param>
        public int Guardar(ProveedorRetencionInfo info)
        {
            try
            {
                Logger.Info();
                var proveedorRetencionDAL = new ProveedorRetencionDAL();
                int result = info.ProveedorRetencionID;
                if (info.ProveedorRetencionID == 0)
                {
                    result = proveedorRetencionDAL.Crear(info);
                }
                else
                {
                    proveedorRetencionDAL.Actualizar(info);
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
        public ResultadoInfo<ProveedorRetencionInfo> ObtenerPorPagina(PaginacionInfo pagina, ProveedorRetencionInfo filtro)
        {
            try
            {
                Logger.Info();
                var proveedorRetencionDAL = new ProveedorRetencionDAL();
                ResultadoInfo<ProveedorRetencionInfo> result = proveedorRetencionDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de ProveedorRetencion
        /// </summary>
        /// <returns></returns>
        public IList<ProveedorRetencionInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var proveedorRetencionDAL = new ProveedorRetencionDAL();
                IList<ProveedorRetencionInfo> result = proveedorRetencionDAL.ObtenerTodos();
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
        public IList<ProveedorRetencionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var proveedorRetencionDAL = new ProveedorRetencionDAL();
                IList<ProveedorRetencionInfo> result = proveedorRetencionDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad ProveedorRetencion por su Id
        /// </summary>
        /// <param name="proveedorRetencionID">Obtiene una entidad ProveedorRetencion por su Id</param>
        /// <returns></returns>
        public ProveedorRetencionInfo ObtenerPorID(int proveedorRetencionID)
        {
            try
            {
                Logger.Info();
                var proveedorRetencionDAL = new ProveedorRetencionDAL();
                ProveedorRetencionInfo result = proveedorRetencionDAL.ObtenerPorID(proveedorRetencionID);
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
        /// Obtiene una entidad ProveedorRetencion por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public ProveedorRetencionInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var proveedorRetencionDAL = new ProveedorRetencionDAL();
                ProveedorRetencionInfo result = proveedorRetencionDAL.ObtenerPorDescripcion(descripcion);
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
        /// Guarda a los proveedores y sus choferes
        /// </summary>
        /// <param name="listaRetenciones">Lista que contiene al proveedor y al chofer</param>
        /// <returns></returns>
        public void GuardarLista(List<ProveedorRetencionInfo> listaRetenciones)
        {
            try
            {
                Logger.Info();
                var proveedorRetencionDAL = new ProveedorRetencionDAL();
                proveedorRetencionDAL.GuardarLista(listaRetenciones);
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
        /// Obtiene una lista de las retenciones configuradas para el proveedor
        /// </summary> 
        /// <param name="proveedorID">Representa el ID del Proveedor </param>
        /// <returns></returns>
        public IList<ProveedorRetencionInfo> ObtenerPorProveedorID(int proveedorID)
        {
            try
            {
                Logger.Info();
                var proveedorRetencionDAL = new ProveedorRetencionDAL();
                IList<ProveedorRetencionInfo> result = proveedorRetencionDAL.ObtenerPorProveedorID(proveedorID);
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

