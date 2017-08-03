using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class MermaSuperavitPL
    {
        /// <summary>
        /// Obtiene un listado de registros por almacenid
        /// </summary>
        /// <returns></returns>
        public MermaSuperavitInfo ObtenerPorAlmacenIdProductoId(AlmacenInfo almacenInfo, ProductoInfo productoInfo)
        {
            MermaSuperavitInfo info;
            try
            {
                Logger.Info();
                var mermaSuperavitBl = new MermaSuperavitBL();
                info = mermaSuperavitBl.ObtenerPorAlmacenIdProductoId(almacenInfo, productoInfo);
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
        /// Obtiene un listado de registros por almacenid
        /// </summary>
        /// <returns></returns>
        public List<MermaSuperavitInfo> ObtenerPorAlmacenID(int almacenID)
        {
            List<MermaSuperavitInfo> info;
            try
            {
                Logger.Info();
                var mermaSuperavitBl = new MermaSuperavitBL();
                info = mermaSuperavitBl.ObtenerPorAlmacenID(almacenID);
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
        /// Metodo para Guardar/Modificar una entidad MermaSuperavit
        /// </summary>
        /// <param name="info">Representa la entidad que se va a grabar</param>
        public int Guardar(MermaSuperavitInfo info)
        {
            try
            {
                Logger.Info();
                var mermaSuperavitBL = new MermaSuperavitBL();
                int result = mermaSuperavitBL.Guardar(info);
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
        public ResultadoInfo<MermaSuperavitInfo> ObtenerPorPagina(PaginacionInfo pagina, MermaSuperavitInfo filtro)
        {
            try
            {
                Logger.Info();
                var mermaSuperavitBL = new MermaSuperavitBL();
                ResultadoInfo<MermaSuperavitInfo> result = mermaSuperavitBL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene una lista
        /// </summary>
        /// <returns></returns>
        public IList<MermaSuperavitInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                var mermaSuperavitBL = new MermaSuperavitBL();
                IList<MermaSuperavitInfo> result = mermaSuperavitBL.ObtenerTodos();
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
        ///  Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        public IList<MermaSuperavitInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var mermaSuperavitBL = new MermaSuperavitBL();
                IList<MermaSuperavitInfo> result = mermaSuperavitBL.ObtenerTodos(estatus);
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
        /// Obtiene una entidad por su Id
        /// </summary>
        /// <param name="mermaSuperavitID"></param>
        /// <returns></returns>
        public MermaSuperavitInfo ObtenerPorID(int mermaSuperavitID)
        {
            try
            {
                Logger.Info();
                var mermaSuperavitBL = new MermaSuperavitBL();
                MermaSuperavitInfo result = mermaSuperavitBL.ObtenerPorID(mermaSuperavitID);
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
        /// <param name="mermaSuperAvit"></param>
        /// <returns></returns>
        public MermaSuperavitInfo ObtenerPorDescripcion(MermaSuperavitInfo mermaSuperAvit)
        {
            try
            {
                Logger.Info();
                var mermaSuperavitBL = new MermaSuperavitBL();
                MermaSuperavitInfo result = mermaSuperavitBL.ObtenerPorDescripcion(mermaSuperAvit);
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
