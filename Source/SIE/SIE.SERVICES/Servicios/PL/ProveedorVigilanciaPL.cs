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
    public class ProveedorVigilanciaPL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Organizacion
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(VigilanciaInfo info)
        {
            try
            {
                Logger.Info();
                var proveedorvigilanciaBL = new ProveedorVigilanciaBL();
                int result = proveedorvigilanciaBL.Guardar(info);
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
        public VigilanciaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var proveedorvigilanciaBL = new ProveedorVigilanciaBL();
                VigilanciaInfo result = proveedorvigilanciaBL.ObtenerPorDescripcion(descripcion);
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
        ///     Obtiene un lista paginada de organizaciones 
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<VigilanciaInfo> ObtenerProveedoresProductoPorPagina(PaginacionInfo pagina, VigilanciaInfo filtro)
        {
            ResultadoInfo<VigilanciaInfo> resultadoOrganizacion;
            try
            {
                Logger.Info();
                var proveedorvigilanciaBL = new ProveedorVigilanciaBL();
                resultadoOrganizacion = proveedorvigilanciaBL.ObtenerProveedoresProductoPorPagina(pagina, filtro);
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
            return resultadoOrganizacion;
        }

        /// <summary>
        /// Obtiene una lista de Lote filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<VigilanciaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var proveedorvigilanciaBL = new ProveedorVigilanciaBL();
                IList<VigilanciaInfo> lista = proveedorvigilanciaBL.ObtenerTodos(estatus);

                return lista;
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
        ///      Obtiene un organizacion por su Id
        /// </summary>
        /// <returns> </returns>
        public VigilanciaInfo ObtenerPorID(int organizacionId)
        {
            VigilanciaInfo info;
            try
            {
                Logger.Info();
                var proveedorvigilanciaBL = new ProveedorVigilanciaBL();
                info = proveedorvigilanciaBL.ObtenerPorID(organizacionId);
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

        public VigilanciaInfo ObtenerPorID(VigilanciaInfo organizacionInfo)
        {
            VigilanciaInfo resultadoOrganizacion;
            try
            {
                Logger.Info();
                var proveedorvigilanciaBL = new ProveedorVigilanciaBL();
                resultadoOrganizacion = proveedorvigilanciaBL.ObtenerPorID(organizacionInfo.ID);
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
            return resultadoOrganizacion;
        }
        /// <summary>
        /// Obtiene el proveedor por Codigo SAP
        /// </summary>
        /// <param name="filtroVigilancia"></param>
        /// <returns></returns>
        public VigilanciaInfo ObtenerPorClaveSap(VigilanciaInfo filtroVigilancia)
        {
            VigilanciaInfo resultadoOrganizacion;
            try
            {
                Logger.Info();
                var proveedorvigilanciaBL = new ProveedorVigilanciaBL();
                resultadoOrganizacion = proveedorvigilanciaBL.ObtenerPorCodigoSap(filtroVigilancia);
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
            return resultadoOrganizacion;
        }

        public ResultadoInfo<VigilanciaInfo> ObtenerPorDescripcion(PaginacionInfo pagina, VigilanciaInfo organizacionInfo)
        {
            try
            {
                Logger.Info();
                var resultadoOrganizacion = new ResultadoInfo<VigilanciaInfo>();

                return resultadoOrganizacion;
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
