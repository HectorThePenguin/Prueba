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
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    public class CheckListRoladoraDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de CheckListRoladora
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(CheckListRoladoraInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraDAL.ObtenerParametrosCrear(info);
                int result = Create("CheckListRoladora_Crear", parameters);
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
        /// Metodo para actualizar un registro de CheckListRoladora
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(CheckListRoladoraInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraDAL.ObtenerParametrosActualizar(info);
                Update("CheckListRoladora_Actualizar", parameters);
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
        public ResultadoInfo<CheckListRoladoraInfo> ObtenerPorPagina(PaginacionInfo pagina, CheckListRoladoraInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCheckListRoladoraDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("CheckListRoladora_ObtenerPorPagina", parameters);
                ResultadoInfo<CheckListRoladoraInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de CheckListRoladora
        /// </summary>
        /// <returns></returns>
        public IList<CheckListRoladoraInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("CheckListRoladora_ObtenerTodos");
                IList<CheckListRoladoraInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraDAL.ObtenerTodos(ds);
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
        public IList<CheckListRoladoraInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("CheckListRoladora_ObtenerTodos", parameters);
                IList<CheckListRoladoraInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de CheckListRoladora
        /// </summary>
        /// <param name="checkListRoladoraID">Identificador de la CheckListRoladora</param>
        /// <returns></returns>
        public CheckListRoladoraInfo ObtenerPorID(int checkListRoladoraID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraDAL.ObtenerParametrosPorID(checkListRoladoraID);
                DataSet ds = Retrieve("CheckListRoladora_ObtenerPorID", parameters);
                CheckListRoladoraInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de CheckListRoladora
        /// </summary>
        /// <param name="descripcion">Descripción de la CheckListRoladora</param>
        /// <returns></returns>
        public CheckListRoladoraInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("CheckListRoladora_ObtenerPorDescripcion", parameters);
                CheckListRoladoraInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraDAL.ObtenerPorDescripcion(ds);
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

        internal IList<CheckListRoladoraInfo> ObtenerNotificaciones(int organizacionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraDAL.ObtenerNotificaciones(organizacionID);
                DataSet ds = Retrieve("CheckListRoladora_ObtenerPorNotificacion", parameters);
                IList<CheckListRoladoraInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraDAL.ObtenerNotificaciones(ds);
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

        internal CheckListRoladoraInfo ObtenerPorTurno(int organizacionID, int turno)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraDAL.ObtenerPorTurno(organizacionID, turno);
                DataSet ds = Retrieve("CheckListRoladora_ObtenerPorTurno", parameters);
                CheckListRoladoraInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraDAL.ObtenerPorTurno(ds);
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

        internal dynamic ObtenerCheckList(int organizacionID, int turno, int roladoraId)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraDAL.ObtenerCheckList(organizacionID, turno, roladoraId);
                DataSet ds = Retrieve("CheckListRoladora_ObtenerCheckList", parameters);
                dynamic result = new {};
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraDAL.ObtenerCheckList(ds);
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

        internal ParametrosCheckListRoladoModel ObtenerGranoEnteroDieselCaldera(int organizacionID, DateTime fechaInicio)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraDAL.ObtenerPorGranoEnteroDieselCaldera(organizacionID, fechaInicio);
                DataSet ds = Retrieve("CheckListRoaldo_ObtenerGranoEnteroYDieselCalderas", parameters);
                ParametrosCheckListRoladoModel result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraDAL.ObtenerPorGranoEnteroDieselCaldera(ds);
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

        internal CheckListRoladoraInfo ValidarCheckList(int organizacionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraDAL.ObtenerPorValidarCheckList(organizacionID);
                DataSet ds = Retrieve("CheckListRoladora_ValidarCheckList", parameters);
                CheckListRoladoraInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraDAL.ObtenerPorValidarCheckList(ds);
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
        /// Obtiene los datos para la impresion del
        /// Check List de Rolado
        /// </summary>
        /// <param name="fechaInicial"></param>
        /// <param name="turno"> </param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal ImpresionCheckListRoladoModel ObtenerDatosImpresionCheckListRoladora(DateTime fechaInicial, int turno, int organizacionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxCheckListRoladoraDAL.ObtenerParametrosDatosImpresionCheckListRoladora(fechaInicial, turno,
                                                                                             organizacionID);
                DataSet ds = Retrieve("CheckListRolado_ObtenerDatosImpresion", parameters);
                ImpresionCheckListRoladoModel result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraDAL.ObtenerDatosImpresionCheckListRoladora(ds);
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
