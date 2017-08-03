using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class ProveedorVigilanciaBL
    {
        /// <summary>
        ///     Metodo que guarda una organización
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(VigilanciaInfo info)
        {
            try
            {
                Logger.Info();
                var proveedorvigilanciaDAL = new ProveedorVigilanciaDAL();
                int result = info.ID;
                if (info.ID == 0)
                {
                    result = proveedorvigilanciaDAL.Crear(info);
                }
                else
                {
                    proveedorvigilanciaDAL.Actualizar(info);
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
        /// Obtiene una entidad Organizacion por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal VigilanciaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var proveedorvigilanciaDAL = new ProveedorVigilanciaDAL();
                VigilanciaInfo result = proveedorvigilanciaDAL.ObtenerPorDescripcion(descripcion);
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<VigilanciaInfo> ObtenerProveedoresProductoPorPagina(PaginacionInfo pagina, VigilanciaInfo filtro)
        {
            ResultadoInfo<VigilanciaInfo> result;
            try
            {
                Logger.Info();
                var proveedorvigilanciaDAL = new ProveedorVigilanciaDAL();
                result = proveedorvigilanciaDAL.ObtenerProveedoresProductoPorPagina(pagina, filtro);
                
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
            return result;
        }

        /// <summary>
        ///     Obtiene un lista paginada de organizaciones 
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="dependencias"> </param>
        /// <returns></returns>
        internal VigilanciaInfo ObtenerPorDependencias(VigilanciaInfo filtro
                                                     , IList<IDictionary<IList<String>, Object>> dependencias)
        {
            VigilanciaInfo resultadoOrganizacion;
            try
            {
                Logger.Info();
                var proveedorvigilanciaDAL = new ProveedorVigilanciaDAL();
                resultadoOrganizacion = proveedorvigilanciaDAL.ObtenerPorDependencias(filtro, dependencias);
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
        ///     Obtiene un lista paginada de organizaciones 
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <param name="dependencias"> </param>
        /// <returns></returns>
        internal ResultadoInfo<VigilanciaInfo> ObtenerPorDependencias(PaginacionInfo pagina, VigilanciaInfo filtro
                                                                    , IList<IDictionary<IList<String>, Object>> dependencias)
        {
            ResultadoInfo<VigilanciaInfo> resultadoOrganizacion;
            try
            {
                Logger.Info();
                var proveedorvigilanciaDAL = new ProveedorVigilanciaDAL();
                resultadoOrganizacion = proveedorvigilanciaDAL.ObtenerPorDependencias(pagina, filtro, dependencias);
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
        ///     Obtiene una lista de las Organizaciones
        /// </summary>
        /// <param> <name></name> </param>        
        /// <returns></returns>
        internal IList<VigilanciaInfo> ObtenerTodos()
        {
            IList<VigilanciaInfo> lista;
            try
            {
                Logger.Info();
                var proveedorvigilanciaDAL = new ProveedorVigilanciaDAL();
                lista = proveedorvigilanciaDAL.ObtenerTodos();
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
            return lista;
        }


        /// <summary>
        /// Obtiene una lista de Organizacion filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <param name="estatus"> </param>
        /// <returns></returns>
        internal IList<VigilanciaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var proveedorvigilanciaDAL = new ProveedorVigilanciaDAL();
                IList<VigilanciaInfo> lista = proveedorvigilanciaDAL.ObtenerTodos(estatus);

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
        ///     Obtiene un organización por su Id
        /// </summary>
        /// <param> <name></name> </param>
        /// <param name="organizacionId"> </param>
        /// <returns></returns>
        internal VigilanciaInfo ObtenerPorID(int Id)
        {
            VigilanciaInfo info;
            try
            {
                Logger.Info();
                var proveedorvigilanciaDAL = new ProveedorVigilanciaDAL();
                info = proveedorvigilanciaDAL.ObtenerPorID(Id);
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
        /// 
        /// </summary>
        /// <param name="filtroVigilancia"></param>
        /// <returns></returns>
        internal VigilanciaInfo ObtenerPorCodigoSap(VigilanciaInfo filtroVigilancia)
        {
            VigilanciaInfo info = null;
            try
            {
                Logger.Info();
                
                var resultado = ObtenerProveedoresProductoPorPagina(new PaginacionInfo{Inicio = 1,Limite = 1},  filtroVigilancia);

                if (resultado != null)
                {
                    info = resultado.Lista.First();
                }
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

       

    }
}