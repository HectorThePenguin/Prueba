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
    internal class CamionBL
    {
        /// <summary>
        ///     Metodo que guarda un camion
        /// </summary>
        /// <param name="info"></param>
        internal void Guardar(CamionInfo info)
        {
            try
            {
                Logger.Info();
                var camionDAL = new CamionDAL();
                if (string.IsNullOrEmpty(info.ObservacionesEnviar))
                {
                    info.ObservacionesEnviar = info.ObservacionesObtener;
                }
                if (info.CamionID != 0)
                {
                    camionDAL.Actualizar(info);
                }
                else
                {
                    camionDAL.Crear(info);
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
        internal ResultadoInfo<CamionInfo> ObtenerPorPagina(PaginacionInfo pagina, CamionInfo filtro)
        {
            ResultadoInfo<CamionInfo> result;
            try
            {
                Logger.Info();
                var camionDAL = new CamionDAL();
                result = camionDAL.ObtenerPorPagina(pagina, filtro);
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
        ///     Obtiene una lista de los camiones
        /// </summary>
        /// <param> <name></name> </param>        
        /// <returns></returns>
        internal IList<CamionInfo> ObtenerTodos()
        {
            IList<CamionInfo> lista;
            try
            {
                Logger.Info();
                var camionDAL = new CamionDAL();
                lista = camionDAL.ObtenerTodos();
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
        ///     Obtiene una lista de los camiones filtrando por su estatus
        /// </summary>
        /// <param name="estatus"> </param>
        /// <returns></returns>
        internal IList<CamionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            IList<CamionInfo> lista;
            try
            {
                Logger.Info();
                var camionDAL = new CamionDAL();
                lista = camionDAL.ObtenerTodos(estatus);
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
        ///     Obtiene un camión por su Id
        /// </summary>
        /// <param> <name></name> </param>
        /// <param name="camionId"> </param>
        /// <returns></returns>
        internal CamionInfo ObtenerPorID(int camionId)
        {
            CamionInfo info;
            try
            {
                Logger.Info();
                var camionDAL = new CamionDAL();
                info = camionDAL.ObtenerPorID(camionId);
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
        internal List<CamionInfo> ObtenerPorProveedorID(int proveedorId)
        {
            List<CamionInfo> lista;
            try
            {
                Logger.Info();
                var camionDAL = new CamionDAL();
                lista = camionDAL.ObtenerPorProveedorID(proveedorId);
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
        internal CamionInfo ObtenerPorInfo(CamionInfo camion, IList<IDictionary<IList<string>, object>> dependencias)
        {
            CamionInfo info;
            try
            {
                Logger.Info();
                var camionDAL = new CamionDAL();
                info = camionDAL.ObtenerPorInfo(camion, dependencias);
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
        internal ResultadoInfo<CamionInfo> ObtenerPorPagina(PaginacionInfo pagina, CamionInfo filtro, IList<IDictionary<IList<string>, object>> Dependencias)
        {
            ResultadoInfo<CamionInfo> result;
            try
            {
                Logger.Info();
                var camionDAL = new CamionDAL();
                result = camionDAL.ObtenerPorPagina(pagina, filtro, Dependencias);
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
        /// Obtiene una entidad Camion por su descripcion
        /// </summary>
        /// <param name="descripcion">Obtiene una entidad Camion por su Id</param>
        /// <returns></returns>
        internal CamionInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                var camionDAL = new CamionDAL();
                CamionInfo result = camionDAL.ObtenerPorDescripcion(descripcion);
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
        internal CamionInfo ObtenerPorCamion(CamionInfo filtro)
        {
            try
            {
                Logger.Info();
                var camionDAL = new CamionDAL();
                CamionInfo result = camionDAL.ObtenerPorCamion(filtro);
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

      /*  public object ObtenerCamionPorProveedor(int proveedorId)
        {
            try
            {
                Logger.Info();
                var camionDAL = new CamionDAL();
                CamionInfo result = camionDAL.ObtenerCamionPorProveedor(proveedorId);
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
        }*/

        /// <summary>
        /// Obtiene un camion por id enviando un parametro CamionInfo
        /// </summary>
        /// <returns></returns>
        internal CamionInfo ObtenerPorId(CamionInfo camionInfo)
        {
            CamionInfo info;
            try
            {
                Logger.Info();
                var camionDAL = new CamionDAL();
                info = camionDAL.ObtenerPorId(camionInfo);
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
        internal CamionInfo ObtenerPorProveedorIdCamionId(CamionInfo camionInfo)
        {
            CamionInfo info;
            try
            {
                Logger.Info();
                var camionDAL = new CamionDAL();
                info = camionDAL.ObtenerPorProveedorIdCamionId(camionInfo);
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
        /// Obtiene un camion por placa
        /// </summary>
        /// <param name="placaCamion"></param>
        internal CamionInfo ObtenerPorPlaca(string placaCamion)
        {
            CamionInfo info;
            try
            {
                Logger.Info();
                var camionDAL = new CamionDAL();
                info = camionDAL.ObtenerPorPlaca(placaCamion);
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
