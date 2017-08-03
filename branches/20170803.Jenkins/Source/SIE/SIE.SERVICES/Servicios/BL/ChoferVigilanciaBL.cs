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
    internal class ChoferVigilanciaBL
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
                var chofervigilanciaDAL = new ChoferVigilanciaDAL();
                if (info.ChoferID != 0)
                {
                    chofervigilanciaDAL.Actualizar(info);
                }
                else
                {
                    chofervigilanciaDAL.Crear(info);
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
                var chofervigilanciaDAL = new ChoferVigilanciaDAL();
                result = chofervigilanciaDAL.ObtenerPorPagina(pagina, filtro);
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
                var chofervigilanciaDAL = new ChoferVigilanciaDAL();
                lista = chofervigilanciaDAL.ObtenerTodos();
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
                var chofervigilanciaDAL = new ChoferVigilanciaDAL();
                IList<ChoferInfo> lista = chofervigilanciaDAL.ObtenerTodos(estatus);

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
        /// <param name="chofer"> </param>
        /// <returns></returns>
        internal ChoferInfo ObtenerPorID(ChoferInfo chofer)
        {
            ChoferInfo info;
            try
            {
                Logger.Info();
                var chofervigilanciaDAL = new ChoferVigilanciaDAL();
                info = chofervigilanciaDAL.ObtenerPorID(chofer);
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
                var chofervigilanciaDAL = new ChoferVigilanciaDAL();
                ChoferInfo result = chofervigilanciaDAL.ObtenerPorDescripcion(descripcion);
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