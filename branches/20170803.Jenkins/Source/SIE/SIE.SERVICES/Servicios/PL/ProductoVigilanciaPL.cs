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
    public class ProductoVigilanciaPL
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
                var organizacionBL = new ProductoVigilanciaBL();
                int result = organizacionBL.Guardar(info);
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
                var organizacionBL = new ProductoVigilanciaBL();
                VigilanciaInfo result = organizacionBL.ObtenerPorDescripcion(descripcion);
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
        public ResultadoInfo<ProductoInfo> ObtenerProductosPorPagina(PaginacionInfo pagina, ProductoInfo filtro)
        {
            ResultadoInfo<ProductoInfo> resultadoOrganizacion;
            try
            {
                Logger.Info();
                var organizacionBL = new ProductoVigilanciaBL();
                resultadoOrganizacion = organizacionBL.ObtenerProductosPorPagina(pagina, filtro);
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
                var organizacionBL = new ProductoVigilanciaBL();
                IList<VigilanciaInfo> lista = organizacionBL.ObtenerTodos(estatus);

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
        public VigilanciaInfo ObtenerPorID(int id)
        {
            VigilanciaInfo info;
            try
            {
                Logger.Info();
                var productoBl = new ProductoVigilanciaBL();
                info = productoBl.ObtenerPorID(id);
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
        /// Obtiene producto por id
        /// </summary>
        /// <param name="organizacionInfo"></param>
        /// <returns></returns>
        public VigilanciaInfo ObtenerPorID(VigilanciaInfo organizacionInfo)
        {
            VigilanciaInfo resultadoOrganizacion;
            try
            {
                Logger.Info();
                var organizacionBL = new ProductoVigilanciaBL();
                resultadoOrganizacion = organizacionBL.ObtenerPorID(organizacionInfo.ID);
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
        /// Obtiene los productos por ID
        /// </summary>
        /// <param name="organizacionInfo"></param>
        /// <returns></returns>
        public ProductoInfo ObtenerPorID(ProductoInfo organizacionInfo)
        {

            try
            {
                Logger.Info();
                var productoBl = new ProductoVigilanciaBL();
                var producto = productoBl.ObtenerProductoPorID(organizacionInfo);
                return producto;
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
        /// Obtiene producto por descripcion
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="organizacionInfo"></param>
        /// <returns></returns>
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
