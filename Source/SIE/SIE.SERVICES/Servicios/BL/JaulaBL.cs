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
    internal class JaulaBL
    {
        /// <summary>
        ///     Metodo que crear un tipo de embarque
        /// </summary>
        /// <param name="info"></param>
        internal void Guardar(JaulaInfo info)
        {
            try
            {
                Logger.Info();
                var jaulaDAL = new JaulaDAL();
                if (info.JaulaID != 0)
                {
                    jaulaDAL.Actualizar(info);
                }
                else
                {
                    jaulaDAL.Crear(info);
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
        }

        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<JaulaInfo> ObtenerPorPagina(PaginacionInfo pagina, JaulaInfo filtro)
        {
            ResultadoInfo<JaulaInfo> result;
            try
            {
                Logger.Info();
                var jaulaDAL = new JaulaDAL();
                result = jaulaDAL.ObtenerPorPagina(pagina, filtro);
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
        ///     Obtiene un lista de los tipos de embarque
        /// </summary>
        /// <param> <name></name> </param>        
        /// <returns></returns>
        internal IList<JaulaInfo> ObtenerTodos()
        {
            IList<JaulaInfo> lista;
            try
            {
                Logger.Info();
                var jaulaDAL = new JaulaDAL();
                lista = jaulaDAL.ObtenerTodos();
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
        ///     Obtiene una lista de jaulas  filtrando por su estatus
        /// </summary>
        /// <param name="estatus"> </param>
        /// <returns></returns>
        internal IList<JaulaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            IList<JaulaInfo> lista;
            try
            {
                Logger.Info();
                var jaulaDAL = new JaulaDAL();
                lista = jaulaDAL.ObtenerTodos(estatus);
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
        ///     Obtiene un lista de los tipos de embarque
        /// </summary>
        /// <param> <name></name> </param>
        /// <param name="jaulaId"> </param>
        /// <returns></returns>
        internal JaulaInfo ObtenerPorID(int jaulaId)
        {
            JaulaInfo info;
            try
            {
                Logger.Info();
                var jaulaDAL = new JaulaDAL();
                info = jaulaDAL.ObtenerPorID(jaulaId);
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
        ///    Obtiene las Jaulas de un Proveedor
        /// </summary>
        /// <param name="proveedorId"> </param>
        /// <returns></returns>
        internal List<JaulaInfo> ObtenerPorProveedorID(int proveedorId)
        {
            List<JaulaInfo> info;
            try
            {
                Logger.Info();
                var jaulaDAL = new JaulaDAL();
                info = jaulaDAL.ObtenerPorProveedorID(proveedorId);
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
        /// Obtiene una Entidad de JaulaInfo
        /// </summary>
        /// <param name="jaula"></param>
        /// <returns></returns>
        internal JaulaInfo ObtenerJaula(JaulaInfo jaula)
        {
            JaulaInfo info;
            try
            {
                Logger.Info();
                var jaulaDAL = new JaulaDAL();
                info = jaulaDAL.ObtenerJaula(jaula);
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <param name="Dependencias"> </param>
        /// <returns></returns>
        internal ResultadoInfo<JaulaInfo> ObtenerPorPagina(PaginacionInfo pagina, JaulaInfo filtro, IList<IDictionary<IList<string>, object>> Dependencias)
        {
            ResultadoInfo<JaulaInfo> result;
            try
            {
                Logger.Info();
                var jaulaDAL = new JaulaDAL();
                result = jaulaDAL.ObtenerPorPagina(pagina, filtro, Dependencias);
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
        /// Obtiene una Entidad de JaulaInfo
        /// </summary>
        /// <param name="jaula"></param>
        /// <param name="Dependencias"> </param>
        /// <returns></returns>
        internal JaulaInfo ObtenerJaula(JaulaInfo jaula, IList<IDictionary<IList<string>, object>> Dependencias)
        {
            JaulaInfo info;
            try
            {
                Logger.Info();
                var jaulaDAL = new JaulaDAL();
                info = jaulaDAL.ObtenerJaula(jaula, Dependencias);
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
        /// Obtiene una entidad Jaula por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal JaulaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var jaulaDAL = new JaulaDAL();
                JaulaInfo result = jaulaDAL.ObtenerPorDescripcion(descripcion);
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
