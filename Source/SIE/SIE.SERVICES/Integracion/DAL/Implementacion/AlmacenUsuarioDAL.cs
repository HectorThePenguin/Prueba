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
    public class AlmacenUsuarioDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de AlmacenUsuario
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(AlmacenUsuarioInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenUsuarioDAL.ObtenerParametrosCrear(info);
                int result = Create("AlmacenUsuario_Crear", parameters);
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
        /// Metodo para Crear un registro de AlmacenUsuario
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int GuardarXML(List<AlmacenUsuarioInfo> info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenUsuarioDAL.ObtenerParametrosGuardarXML(info);
                int result = Create("AlmacenUsuario_GuardarXml", parameters);
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
        /// Metodo para actualizar un registro de AlmacenUsuario
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(AlmacenUsuarioInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenUsuarioDAL.ObtenerParametrosActualizar(info);
                Update("AlmacenUsuario_Actualizar", parameters);
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
        public ResultadoInfo<AlmacenUsuarioInfo> ObtenerPorPagina(PaginacionInfo pagina, AlmacenUsuarioInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxAlmacenUsuarioDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("AlmacenUsuario_ObtenerPorPagina", parameters);
                ResultadoInfo<AlmacenUsuarioInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenUsuarioDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de AlmacenUsuario
        /// </summary>
        /// <returns></returns>
        public IList<AlmacenUsuarioInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("AlmacenUsuario_ObtenerTodos");
                IList<AlmacenUsuarioInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenUsuarioDAL.ObtenerTodos(ds);
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
        public IList<AlmacenUsuarioInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenUsuarioDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("AlmacenUsuario_ObtenerTodos", parameters);
                IList<AlmacenUsuarioInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenUsuarioDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de AlmacenUsuario
        /// </summary>
        /// <param name="almacenUsuarioID">Identificador de la AlmacenUsuario</param>
        /// <returns></returns>
        public AlmacenUsuarioInfo ObtenerPorID(int almacenUsuarioID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenUsuarioDAL.ObtenerParametrosPorID(almacenUsuarioID);
                DataSet ds = Retrieve("AlmacenUsuario_ObtenerPorID", parameters);
                AlmacenUsuarioInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenUsuarioDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de AlmacenUsuario
        /// </summary>
        /// <param name="descripcion">Descripción de la AlmacenUsuario</param>
        /// <returns></returns>
        public AlmacenUsuarioInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenUsuarioDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("AlmacenUsuario_ObtenerPorDescripcion", parameters);
                AlmacenUsuarioInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenUsuarioDAL.ObtenerPorDescripcion(ds);
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
        /// Obtiene una lista de AlmacenUsuario
        /// </summary>
        /// <returns></returns>
        public List<AlmacenUsuarioInfo> ObtenerPorAlmacenID(AlmacenInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenUsuarioDAL.ObtenerParametrosPorAlmacenID(info);
                DataSet ds = Retrieve("AlmacenUsuario_ObtenerPorAlmacenID", parameters);
                List<AlmacenUsuarioInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenUsuarioDAL.ObtenerPorAlmacenID(ds);
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
        /// Obtiene un AlmacenUsuario
        /// </summary>
        /// <returns></returns>
        internal AlmacenUsuarioInfo ObtenerPorUsuarioId(int usuarioId)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenUsuarioDAL.ObtenerParametrosPorUsuarioId(usuarioId);
                DataSet ds = Retrieve("AlmacenUsuario_ObtenerPorUsuarioID", parameters);
                AlmacenUsuarioInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenUsuarioDAL.ObtenerPorUsuarioId(ds);
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

