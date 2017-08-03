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
    internal class IvaBL
    {
        /// <summary>
        /// Metodo para Guardar/Modificar una entidad Iva
        /// </summary>
        /// <param name="info"></param>
        internal void Guardar(IvaInfo info)
        {
            try
            {
                Logger.Info();
                var ivaDAL = new IvaDAL();
                if (info.IvaID == 0)
                {
                    ivaDAL.Crear(info);
                }
                else
                {
                    ivaDAL.Actualizar(info);
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
        internal ResultadoInfo<IvaInfo> ObtenerPorPagina(PaginacionInfo pagina, IvaInfo filtro)
        {
            try
            {
                Logger.Info();
                var ivaDAL = new IvaDAL();
                ResultadoInfo<IvaInfo> result = ivaDAL.ObtenerPorPagina(pagina, filtro);
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
        ///     Obtiene un lista de Ivas
        /// </summary>
        /// <returns></returns>
        internal IList<IvaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var ivaDAL = new IvaDAL();
                IList<IvaInfo> result = ivaDAL.ObtenerTodos();
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
        ///     Obtiene un lista de Ivas
        /// </summary>
        /// <returns></returns>
        internal IList<IvaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var ivaDAL = new IvaDAL();
                IList<IvaInfo> result = ivaDAL.ObtenerTodos(estatus);
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
        ///     Obtiene una entidad Iva por su Id
        /// </summary>
        /// <param name="ivaID">Obtiene uan entidad Iva por su Id</param>
        /// <returns></returns>
        internal IvaInfo ObtenerPorID(int ivaID)
        {
            try
            {
                Logger.Info();
                var ivaDAL = new IvaDAL();
                IvaInfo result = ivaDAL.ObtenerPorID(ivaID);
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

