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
    internal class MermaSuperavitBL
    {
        /// <summary>
        /// Obtiene un listado de registros por almacenid
        /// </summary>
        /// <returns></returns>
        internal MermaSuperavitInfo ObtenerPorAlmacenIdProductoId(AlmacenInfo almacenInfo, ProductoInfo productoInfo)
        {
            try
            {
                Logger.Info();
                var almacenDal = new MermaSuperavitDAL();
                MermaSuperavitInfo result = almacenDal.ObtenerPorAlmacenIdProductoId(almacenInfo, productoInfo);
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
        /// Obtiene un listado de registros por almacenid
        /// </summary>
        /// <returns></returns>
        internal List<MermaSuperavitInfo> ObtenerPorAlmacenID(int almacenID)
        {
            try
            {
                Logger.Info();
                var almacenDal = new MermaSuperavitDAL();
                List<MermaSuperavitInfo> result = almacenDal.ObtenerPorAlmacenID(almacenID);
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
        /// Metodo para Guardar/Modificar una entidad MermaSuperavit
        /// </summary>
        /// <param name="info"></param>
        internal int Guardar(MermaSuperavitInfo info)
        {
            try
            {
                Logger.Info();
                var mermaSuperavitDAL = new MermaSuperavitDAL();
                int result = info.MermaSuperavitId;
                if (result == 0)
                {
                    result = mermaSuperavitDAL.Crear(info);
                }
                else
                {
                    mermaSuperavitDAL.Actualizar(info);
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
        internal ResultadoInfo<MermaSuperavitInfo> ObtenerPorPagina(PaginacionInfo pagina, MermaSuperavitInfo filtro)
        {
            try
            {
                Logger.Info();
                var mermaSuperavitDAL = new MermaSuperavitDAL();
                ResultadoInfo<MermaSuperavitInfo> result = mermaSuperavitDAL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un lista de MermaSuperavit
        /// </summary>
        /// <returns></returns>
        internal IList<MermaSuperavitInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var mermaSuperavitDAL = new MermaSuperavitDAL();
                IList<MermaSuperavitInfo> result = mermaSuperavitDAL.ObtenerTodos();
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
        internal IList<MermaSuperavitInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var mermaSuperavitDAL = new MermaSuperavitDAL();
                IList<MermaSuperavitInfo> result = mermaSuperavitDAL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad MermaSuperavit por su Id
        /// </summary>
        /// <param name="mermaSuperavitID">Obtiene una entidad MermaSuperavit por su Id</param>
        /// <returns></returns>
        internal MermaSuperavitInfo ObtenerPorID(int mermaSuperavitID)
        {
            try
            {
                Logger.Info();
                var mermaSuperavitDAL = new MermaSuperavitDAL();
                MermaSuperavitInfo result = mermaSuperavitDAL.ObtenerPorID(mermaSuperavitID);
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
        /// Obtiene una entidad MermaSuperavit por su descripción
        /// </summary>
        /// <param name="mermaSuperAvit"></param>
        /// <returns></returns>
        internal MermaSuperavitInfo ObtenerPorDescripcion(MermaSuperavitInfo mermaSuperAvit)
        {
            try
            {
                Logger.Info();
                var mermaSuperavitDAL = new MermaSuperavitDAL();
                MermaSuperavitInfo result = mermaSuperavitDAL.ObtenerPorDescripcion(mermaSuperAvit);
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
