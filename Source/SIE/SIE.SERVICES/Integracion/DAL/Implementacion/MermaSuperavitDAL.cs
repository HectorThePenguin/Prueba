using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class MermaSuperavitDAL : DALBase
    {
        /// <summary>
        /// Obtiene un listado de registros por almacenid
        /// </summary>
        /// <param name="almacenInfo"></param>
        /// <param name="productoInfo"></param>
        /// <returns></returns>
        internal MermaSuperavitInfo ObtenerPorAlmacenIdProductoId(AlmacenInfo almacenInfo, ProductoInfo productoInfo)
        {
            MermaSuperavitInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxMermaSuperavitDAL.ObtenerParametrosObtenerPorAlmacenIdProductoId(almacenInfo, productoInfo);
                DataSet ds = Retrieve("MermaSuperavit_ObtenerPorAlmacenIDProductoID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapMermaSuperavitDAL.ObtenerPorAlmacenIdProductoId(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Obtiene un listado de registros por almacenid
        /// </summary>
        /// <param name="almacenID"></param>
        /// <returns></returns>
        internal List<MermaSuperavitInfo> ObtenerPorAlmacenID(int almacenID)
        {
            List<MermaSuperavitInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxMermaSuperavitDAL.ObtenerParametrosObtenerPorAlmacenID(almacenID);
                DataSet ds = Retrieve("MermaSuperavit_ObtenerPorAlmacenID", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapMermaSuperavitDAL.ObtenerPorAlmacenID(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Metodo para Crear un registro de MermaSuperavit
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(MermaSuperavitInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxMermaSuperavitDAL.ObtenerParametrosCrear(info);
                int result = Create("MermaSuperavit_Crear", parameters);
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo para actualizar un registro de MermaSuperavit
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(MermaSuperavitInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxMermaSuperavitDAL.ObtenerParametrosActualizar(info);
                Update("MermaSuperavit_Actualizar", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
                Dictionary<string, object> parameters = AuxMermaSuperavitDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("MermaSuperavit_ObtenerPorPagina", parameters);
                ResultadoInfo<MermaSuperavitInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapMermaSuperavitDAL.ObtenerPorPagina(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista de MermaSuperavit
        /// </summary>
        /// <returns></returns>
        internal IList<MermaSuperavitInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("MermaSuperavit_ObtenerTodos");
                IList<MermaSuperavitInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapMermaSuperavitDAL.ObtenerTodos(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
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
                Dictionary<string, object> parameters = AuxMermaSuperavitDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("MermaSuperavit_ObtenerTodos", parameters);
                IList<MermaSuperavitInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapMermaSuperavitDAL.ObtenerTodos(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un registro de MermaSuperavit
        /// </summary>
        /// <param name="mermaSuperavitID">Identificador de la MermaSuperavit</param>
        /// <returns></returns>
        internal MermaSuperavitInfo ObtenerPorID(int mermaSuperavitID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxMermaSuperavitDAL.ObtenerParametrosPorID(mermaSuperavitID);
                DataSet ds = Retrieve("MermaSuperavit_ObtenerPorID", parameters);
                MermaSuperavitInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapMermaSuperavitDAL.ObtenerPorID(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un registro de MermaSuperavit
        /// </summary>
        /// <param name="mermaSuperAvit">Descripción de la MermaSuperavit</param>
        /// <returns></returns>
        internal MermaSuperavitInfo ObtenerPorDescripcion(MermaSuperavitInfo mermaSuperAvit)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxMermaSuperavitDAL.ObtenerParametrosPorDescripcion(mermaSuperAvit);
                DataSet ds = Retrieve("MermaSuperavit_ObtenerPorDescripcion", parameters);
                MermaSuperavitInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapMermaSuperavitDAL.ObtenerPorDescripcion(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
