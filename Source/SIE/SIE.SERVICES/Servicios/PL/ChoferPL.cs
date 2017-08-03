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
    public class ChoferPL : IPaginador<ChoferInfo>
    {
        /// <summary>
        ///     Metodo que guarda un chofer
        /// </summary>
        /// <param name="info"></param>
        public void Guardar(ChoferInfo info)
        {
            try
            {
                Logger.Info();
                var choferBL = new ChoferBL();
                choferBL.Guardar(info);
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
        public ResultadoInfo<ChoferInfo> ObtenerPorPagina(PaginacionInfo pagina, ChoferInfo filtro)
        {
            ResultadoInfo<ChoferInfo> result;
            try
            {
                Logger.Info();
                var choferBL = new ChoferBL();
                result = choferBL.ObtenerPorPagina(pagina, filtro);
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ChoferInfo> ObtenerChoferesDeProveedorPorPagina(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            ResultadoInfo<ChoferInfo> result;
            try
            {
                Logger.Info();
                var choferBL = new ChoferBL();
                result = choferBL.ObtenerChoferesDeProveedorPorPagina(pagina, filtro);
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
        ///     Obtiene un lista de los choferes
        /// </summary>
        /// <returns> </returns>
        public IList<ChoferInfo> ObtenerTodos()
        {
            IList<ChoferInfo> lista;
            try
            {
                Logger.Info();
                var choferBL = new ChoferBL();
                lista = choferBL.ObtenerTodos();
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
        ///    Obtiene una lista de Choferes filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns> </returns>
        public IList<ChoferInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var choferBL = new ChoferBL();
                IList<ChoferInfo> lista = choferBL.ObtenerTodos(estatus);

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
        ///      Obtiene un chofer por su Id
        /// </summary>
        /// <returns> </returns>
        public ChoferInfo ObtenerPorID(int choferId)
        {
            ChoferInfo info;
            try
            {
                Logger.Info();
                var choferBL = new ChoferBL();
                info = choferBL.ObtenerPorID(choferId);
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
        ///      Obtiene un chofer por su Id
        /// </summary>
        /// <returns> </returns>
        public ChoferInfo ObtenerPorID(ChoferInfo choferInfo)
        {
            ChoferInfo info;
            try
            {
                Logger.Info();
                var choferBL = new ChoferBL();
                info = choferBL.ObtenerPorID(choferInfo);
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
        /// Obtiene una entidad por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public ChoferInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var choferBL = new ChoferBL();
                ChoferInfo result = choferBL.ObtenerPorDescripcion(descripcion);
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
        public ResultadoInfo<ChoferInfo> ObtenerFormulaChoferPorPagina(PaginacionInfo pagina, ChoferInfo filtro)
        {
            ResultadoInfo<ChoferInfo> result;
            try
            {
                Logger.Info();
                var choferBL = new ChoferBL();
                result = choferBL.ObtenerFormulaChoferPorPagina(pagina, filtro);
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
    }
}
