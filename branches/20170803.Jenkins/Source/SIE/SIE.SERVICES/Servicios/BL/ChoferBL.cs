using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Interfaces;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class ChoferBL
    {
        /// <summary>
        ///     Metodo que guarda un chofer
        /// </summary>
        /// <param name="info"></param>
        internal void Guardar(ChoferInfo info)
        {
            try
            {
                Logger.Info();
                var choferDAL = new ChoferDAL();
                if (info.ChoferID != 0)
                {
                    choferDAL.Actualizar(info);
                }
                else
                {
                    choferDAL.Crear(info);
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
        internal ResultadoInfo<ChoferInfo> ObtenerPorPagina(PaginacionInfo pagina, ChoferInfo filtro)
        {
            ResultadoInfo<ChoferInfo> result;
            try
            {
                Logger.Info();
                var choferDAL = new ChoferDAL();
                result = choferDAL.ObtenerPorPagina(pagina, filtro);
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
        internal ResultadoInfo<ChoferInfo> ObtenerChoferesDeProveedorPorPagina(PaginacionInfo pagina, ProveedorInfo filtro)
        {
            ResultadoInfo<ChoferInfo> result;
            try
            {
                Logger.Info();
                var choferDAL = new ChoferDAL();
                result = choferDAL.ObtenerChoferesDeProveedorPorPagina(pagina, filtro);
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
        ///     Obtiene un lista de choferes
        /// </summary>
        /// <returns></returns>
        internal IList<ChoferInfo> ObtenerTodos()
        {
            IList<ChoferInfo> lista;
            try
            {
                Logger.Info();
                var choferDAL = new ChoferDAL();
                lista = choferDAL.ObtenerTodos();
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
        ///     Obtiene una lista de Choferes filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal IList<ChoferInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var choferDAL = new ChoferDAL();
                IList<ChoferInfo> lista = choferDAL.ObtenerTodos(estatus);

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
        ///     Obtiene un chofer por su Id
        /// </summary>
        /// <param name="choferId"> </param>
        /// <returns></returns>
        internal ChoferInfo ObtenerPorID(int choferId)
        {
            ChoferInfo info;
            try
            {
                Logger.Info();
                var choferDAL = new ChoferDAL();
                info = choferDAL.ObtenerPorID(choferId);
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
        /// Obtiene el chofer por su identificador
        /// </summary>
        /// <param name="choferInfo"></param>
        /// <returns></returns>
        internal ChoferInfo ObtenerPorID(ChoferInfo choferInfo)
        {
            ChoferInfo info;
            try
            {
                Logger.Info();
                var choferDAL = new ChoferDAL();
                info = choferDAL.ObtenerPorID(choferInfo);
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
        /// Obtiene una entidad Chofer por su descripción
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal ChoferInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var choferDAL = new ChoferDAL();
                ChoferInfo result = choferDAL.ObtenerPorDescripcion(descripcion);
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
        internal ResultadoInfo<ChoferInfo> ObtenerFormulaChoferPorPagina(PaginacionInfo pagina, ChoferInfo filtro)
        {
            ResultadoInfo<ChoferInfo> result;
            try
            {
                Logger.Info();
                var choferDAL = new ChoferDAL();
                result = choferDAL.ObtenerFormulaChoferPorPagina(pagina, filtro);
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