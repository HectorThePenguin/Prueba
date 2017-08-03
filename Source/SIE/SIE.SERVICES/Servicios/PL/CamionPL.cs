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
    public class CamionPL : IPaginador<CamionInfo>
    {
        /// <summary>
        ///     Metodo que guarda un Camion
        /// </summary>
        /// <param name="info"></param>
        public void Guardar(CamionInfo info)
        {
            try
            {
                Logger.Info();
                var camionBL = new CamionBL();
                camionBL.Guardar(info);
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
        ResultadoInfo<CamionInfo> IPaginador<CamionInfo>.ObtenerPorPagina(PaginacionInfo pagina, CamionInfo filtro)
        {
            ResultadoInfo<CamionInfo> result;
            try
            {
                Logger.Info();
                var camionBL = new CamionBL();
                result = camionBL.ObtenerPorPagina(pagina, filtro);
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
        ///     Obtiene un lista de los Camiones
        /// </summary>
        /// <returns> </returns>
        public IList<CamionInfo> ObtenerTodos()
        {
            IList<CamionInfo> lista;
            try
            {
                Logger.Info();
                var camionBL = new CamionBL();
                lista = camionBL.ObtenerTodos();
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
        ///     Obtiene un lista de los Camiones filtrando por su estatus
        /// </summary>
        /// <returns> </returns>
        public IList<CamionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            IList<CamionInfo> lista;
            try
            {
                Logger.Info();
                var camionBL = new CamionBL();
                lista = camionBL.ObtenerTodos(estatus);
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
        ///      Obtiene un Camion por su Id
        /// </summary>
        /// <returns> </returns>
        public CamionInfo ObtenerPorID(int camionId)
        {
            CamionInfo info;
            try
            {
                Logger.Info();
                var camionBL = new CamionBL();
                info = camionBL.ObtenerPorID(camionId);
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
        ///     Obtiene los Camiones de un Proveedor
        /// </summary>
        /// <param name="proveedorId">Id del Proveedor del que se consultaran sus camiones</param>
        /// <returns></returns>
        public List<CamionInfo> ObtenerPorProveedorID(int proveedorId)
        {
            List<CamionInfo> lista;
            try
            {
                Logger.Info();
                var camionBL = new CamionBL();
                lista = camionBL.ObtenerPorProveedorID(proveedorId);
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
        ///      Obtiene un Camion
        /// </summary>
        /// <returns> </returns>
        public CamionInfo ObtenerPorInfo(CamionInfo camion, IList<IDictionary<IList<string>, object>> Dependencias)
        {
            CamionInfo info;
            try
            {
                Logger.Info();
                var camionBL = new CamionBL();
                info = camionBL.ObtenerPorInfo(camion, Dependencias);
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
        public ResultadoInfo<CamionInfo> ObtenerPorPagina(PaginacionInfo pagina, CamionInfo filtro
                                                        , IList<IDictionary<IList<string>, object>> Dependencias)
        {
            ResultadoInfo<CamionInfo> result;
            try
            {
                Logger.Info();
                var camionBL = new CamionBL();
                result = camionBL.ObtenerPorPagina(pagina, filtro, Dependencias);
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
        public CamionInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var camionBL = new CamionBL();
                CamionInfo result = camionBL.ObtenerPorDescripcion(descripcion);
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
        public ResultadoInfo<CamionInfo> ObtenerPorPagina(PaginacionInfo pagina, CamionInfo filtro)
        {
            try
            {
                Logger.Info();
                var camionBL = new CamionBL();
                ResultadoInfo<CamionInfo> result = camionBL.ObtenerPorPagina(pagina, filtro);
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
        /// Obtiene un camion por su clave
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public CamionInfo ObtenerPorCamion(CamionInfo filtro)
        {
            try
            {
                Logger.Info();
                var camionBL = new CamionBL();
                CamionInfo result = camionBL.ObtenerPorCamion(filtro);
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
        /// Obtiene un camion por id enviando un parametro CamionInfo
        /// </summary>
        /// <returns></returns>
        public CamionInfo ObtenerPorId(CamionInfo camionInfo)
        {
            CamionInfo info;
            try
            {
                Logger.Info();
                var camionBL = new CamionBL();
                info = camionBL.ObtenerPorId(camionInfo);
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
        /// Obtiene un camion por proveedorID y camionID
        /// </summary>
        /// <param name="camionInfo"></param>
        /// <returns></returns>
        public CamionInfo ObtenerPorProveedorIdCamionId(CamionInfo camionInfo)
        {
            CamionInfo info;
            try
            {
                Logger.Info();
                var camionBL = new CamionBL();
                info = camionBL.ObtenerPorProveedorIdCamionId(camionInfo);
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
