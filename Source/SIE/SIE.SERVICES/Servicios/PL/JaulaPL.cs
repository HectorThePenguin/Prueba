using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Interfaces;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class JaulaPL : IPaginador<JaulaInfo>
    {
        /// <summary>
        ///     Metodo que guarda un Jaula
        /// </summary>
        /// <param name="info"></param>
        public void Guardar(JaulaInfo info)
        {
            try
            {
                Logger.Info();
                var jaulaBL = new JaulaBL();
                jaulaBL.Guardar(info);
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
        public ResultadoInfo<JaulaInfo> ObtenerPorPagina(PaginacionInfo pagina, JaulaInfo filtro)
        {
            try
            {
                Logger.Info();
                var jaulaBL = new JaulaBL();
                ResultadoInfo<JaulaInfo> result = jaulaBL.ObtenerPorPagina(pagina, filtro);
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
        ///     Obtiene un lista de los Jaula
        /// </summary>
        /// <returns> </returns>
        public IList<JaulaInfo> ObtenerTodos()
        {
            IList<JaulaInfo> lista;
            try
            {
                Logger.Info();
                var jaulaBL = new JaulaBL();
                lista = jaulaBL.ObtenerTodos();
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
        ///     Obtiene un lista de los Jaula filtrando por su estatus
        /// </summary>
        /// <returns> </returns>
        public IList<JaulaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            IList<JaulaInfo> lista;
            try
            {
                Logger.Info();
                var jaulaBL = new JaulaBL();
                lista = jaulaBL.ObtenerTodos(estatus);
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
        ///      Obtiene un Jaula por su Id
        /// </summary>
        /// <returns> </returns>
        public JaulaInfo ObtenerPorID(int jaulaId)
        {
            JaulaInfo info;
            try
            {
                Logger.Info();
                var jaulaBL = new JaulaBL();
                info = jaulaBL.ObtenerPorID(jaulaId);
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
        ///      Obtiene las Jaulas de un Proveedor
        /// </summary>
        /// <param name="proveedorId"> Id del Proveedor del que se consultaran sus Jaulas</param>
        /// <returns> </returns>
        public List<JaulaInfo> ObtenerPorProveedorID(int proveedorId)
        {
            List<JaulaInfo> info;
            try
            {
                Logger.Info();
                var jaulaBL = new JaulaBL();
                info = jaulaBL.ObtenerPorProveedorID(proveedorId);
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
        /// Obtiene una entidad de JaulaInfo
        /// </summary>
        /// <returns> </returns>
        public JaulaInfo ObtenerJaula(JaulaInfo jaula)
        {
            JaulaInfo info;
            try
            {
                Logger.Info();
                var jaulaBL = new JaulaBL();
                info = jaulaBL.ObtenerJaula(jaula);
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
        /// Obtiene una entidad de JaulaInfo
        /// </summary>
        /// <returns> </returns>
        public JaulaInfo ObtenerJaula(JaulaInfo jaula, IList<IDictionary<IList<string>, object>> Dependencias)
        {
            JaulaInfo info;
            try
            {
                Logger.Info();
                var jaulaBL = new JaulaBL();
                info = jaulaBL.ObtenerJaula(jaula, Dependencias);
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
        public ResultadoInfo<JaulaInfo> ObtenerPorPagina(PaginacionInfo pagina, JaulaInfo filtro, IList<IDictionary<IList<string>, object>> Dependencias)
        {
            ResultadoInfo<JaulaInfo> result;
            try
            {
                Logger.Info();
                var jaulaBL = new JaulaBL();
                result = jaulaBL.ObtenerPorPagina(pagina, filtro, Dependencias);
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
        /// Obtiene una entidad por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public JaulaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var jaulaBL = new JaulaBL();
                JaulaInfo result = jaulaBL.ObtenerPorDescripcion(descripcion);
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
