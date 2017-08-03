using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    public class ProgramacionEmbarqueDAL : DALBase
    {
        /// <summary>
        /// Método que accede a la base de datos para obtener los embarques de acuerdo a la organizacion ingresadad.
        /// </summary>
        /// <param name="organizacionId"> ID de la organización para filtrar </param>
        /// <returns> Listado con los embarques pertenecientes a la organización. </returns>
        internal List<EmbarqueInfo> ObtenerProgramacionPorOrganizacionId(int organizacionId)
        {
            var result = new List<EmbarqueInfo>();
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProgramacionEmbarqueDAL.ObtenerProgramacionPorOrganizacionId(organizacionId);
                DataSet ds = Retrieve("ProgramacionEmbarque_ObtenerProgramacionPorOrganizacionID", parameters);
                
                    if (ValidateDataSet(ds))
                    {
                        result = MapProgramacionEmbarqueDAL.ObtenerProgramacionPorOrganizacionId(ds);
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
        /// Método que obtiene los ruteos de acuerdo al origen y destino ingresado
        /// </summary>
        /// <param name="ruteoInfo"> Objeto con la información del ruteo </param>
        /// <returns> Ruteos obtenidos </returns>
        internal List<RuteoInfo> ObtenerRuteosPorOrganizacionId(RuteoInfo ruteoInfo)
        {
            var result = new List<RuteoInfo>();
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProgramacionEmbarqueDAL.ObtenerRuteosPorOrganizacionId(ruteoInfo);
                DataSet ds = Retrieve("ProgramacionEmbarque_ObtenerRuteosPorOrigenyDestino", parameters);

                if (ValidateDataSet(ds))
                {
                    result = MapProgramacionEmbarqueDAL.ObtenerRuteosPorOrganizacionId(ds);
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
        /// Método para obtener los detalles del ruteo seleccionado.
        /// </summary>
        /// <param name="embarqueInfo"> Objeto de tipo embarque que contiene la información para filtrar </param>
        /// <returns> Lista con los detalles del ruteo. </returns>
        internal List<RuteoDetalleInfo> ObtenerRuteoDetallesPorRuteoId(EmbarqueInfo embarqueInfo)
        {
            var result = new List<RuteoDetalleInfo>();
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProgramacionEmbarqueDAL.ObtenerRuteoDetallesPorRuteoId(embarqueInfo);
                DataSet ds = Retrieve("ProgramacionEmbarque_ObtenerRuteoDetallePorRuteoId", parameters);

                if (ValidateDataSet(ds))
                {
                    result = MapProgramacionEmbarqueDAL.ObtenerRuteoDetallesPorRuteoId(ds);
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
        /// Metodo que inactivo un registro de embarque
        /// </summary>
        /// <param name="info"></param>
        internal void Eliminar(EmbarqueInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProgramacionEmbarqueDAL.Eliminar(info);

                Delete("ProgramacionEmbarque_EliminarEmbarqueProgramacion", parameters);
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
        ///  Metodo que crea la programación de un embarque con tipo de embarque 'Ruteo'
        /// </summary>
        /// <param name="info"></param>
        internal void CrearProgramacionTipoEmbarqueRuteo(EmbarqueInfo info)
        {
            try
            {
                Logger.Info();
                // Informacion que se registrará en tabla Embarque y EmbarqueRuteo
                Dictionary<string, object> parameters = AuxProgramacionEmbarqueDAL.ObtenerParametrosCrearProgramacionEmbarqueTipoRuteo(info);
                Create("ProgramacionEmbarque_GuardarProgramacionEmbarqueTipoRuteo", parameters);
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
        /// Metodo que actualzia una observación para la programacion embarque
        /// </summary>
        /// <param name="info"></param>
        internal void ActualizarProgramacionEmbarqueObservacion(EmbarqueInfo info)
        {
            try
            {
                Logger.Info();
                // Informacion que se registrará en tabla EmbarqueObservaciones
                Dictionary<string, object> parametersObservaciones = AuxProgramacionEmbarqueDAL.ObtenerParametrosActualizarObservaciones(info);
                Update("ProgramacionEmbarque_ActualizarObservaciones", parametersObservaciones);
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
        /// Metodo que actualiza una programación de un embarque con tipo de embarque 'Ruteo'
        /// </summary>
        /// <param name="info"></param>
        internal void ActualizarEmbarqueProgramacion(EmbarqueInfo info)
        {
            try
            {
                // Informacion que se registrará en tabla Embarque
                Dictionary<string, object> parametersEmbarque = AuxProgramacionEmbarqueDAL.ObtenerParametrosActualizarProgramacionEmbarque(info);
                Update("ProgramacionEmbarque_ActualizarEmbarqueProgramacion", parametersEmbarque);
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
        /// Metodo que actualiza una programación de un embarque con tipo de embarque 'Ruteo'
        /// </summary>
        /// <param name="info"></param>
        internal void ActualizarProgramacionTipoEmbarqueRuteo(EmbarqueInfo info)
        {
            try
            {
                // Informacion que se registrará en tabla EmbarqueRuteo
                Dictionary<string, object> parametersEmbarqueRuteo = AuxProgramacionEmbarqueDAL.ObtenerParametrosActualizarProgramacionEmbarqueRuteo(info);
                Update("ProgramacionEmbarque_ActualizarEmbarqueRuteoProgramacion", parametersEmbarqueRuteo);
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
        /// Metodo que actualiza una programación de un embarque con tipo de embarque 'Directo'
        /// </summary>
        /// <param name="info"></param>
        internal void ActualizarProgramacionTipoEmbarqueDirecto(EmbarqueInfo info)
        {
            try
            {
                // Informacion que se registrará en tabla Embarque
                Dictionary<string, object> parametersEmbarque = AuxProgramacionEmbarqueDAL.ObtenerParametrosActualizarProgramacionEmbarque(info);

                Update("ProgramacionEmbarque_ActualizarEmbarqueProgramacion", parametersEmbarque);
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
        /// Metodo que actualiza una programación de un embarque
        /// </summary>
        /// <param name="info"></param>
        internal void ActualizarProgramacion(EmbarqueInfo info)
        {
            try
            {
                // Informacion que se registrará en tabla Embarque
                Dictionary<string, object> parametersEmbarque = AuxProgramacionEmbarqueDAL.ObtenerParametrosActualizarProgramacionEmbarque(info);

                Update("ProgramacionEmbarque_ActualizarEmbarqueProgramacion", parametersEmbarque);
                
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
        /// Metodo que Crea una programación de embarque
        /// </summary>
        /// <param name="info"></param>
        internal void CrearProgramacion(EmbarqueInfo info)
        {
            try
            {
                var result = new List<EmbarqueInfo>();
                // Informacion que se registrará en tabla Embarque
                Dictionary<string, object> parameters = AuxProgramacionEmbarqueDAL.ObtenerParametrosCrearProgramacionEmbarque(info);

                Create("ProgramacionEmbarque_GuardarProgramacionEmbarque", parameters);
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
        /// Método que obtiene los tipos de embarques registrados segun sus estatus.
        /// </summary>
        /// <returns> Lista con los tipos de embarques encontrados. </returns>
        public List<TipoEmbarqueInfo> ObtenerTiposEmbarque(EstatusEnum Activo)
        {
            List<TipoEmbarqueInfo> tipoEmbarqueInfos = null;
            try
            {

                Logger.Info();
                Dictionary<string, object> parameters = AuxProgramacionEmbarqueDAL.ObtenerTiposEmbarque(Activo);
                var ds = Retrieve("ProgramacionEmbarque_ObtenerTiposEmbarque", parameters);
                if (ValidateDataSet(ds))
                {
                    tipoEmbarqueInfos = MapProgramacionEmbarqueDAL.ObtenerTiposEmbarque(ds);
                }
                return tipoEmbarqueInfos;
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
        /// Método que accede a la base de datos para obtener las jaulas programadas de acuerdo a la organizacion y fecha.
        /// </summary>
        /// <param name="embarqueInfo"> filtros donde viene la OrganizacionID y la FechaInicio</param>
        /// <returns> Listado con las jaulas programadas pertenecientes a la organización. </returns>
        internal List<EmbarqueInfo> ObtenerJaulasProgramadas(EmbarqueInfo embarqueInfo)
        {
            var result = new List<EmbarqueInfo>();
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProgramacionEmbarqueDAL.ObtenerJaulasProgramadas(embarqueInfo);
                DataSet ds = Retrieve("ProgramacionEmbarque_ObtenerJaulasProgramadas", parameters);

                if (ValidateDataSet(ds))
                {
                    result = MapProgramacionEmbarqueDAL.ObtenerJaulasProgramadas(ds);
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
        /// Método que obtiene los ruteos activos registrados.
        /// </summary>
        /// <returns> Listado con los ruteos seleccionados. </returns>
        internal List<RuteoInfo> ObtenerRuteosActivos()
        {
            var result = new List<RuteoInfo>();
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("ProgramacionEmbarque_ObtenerRuteosActivos");

                if (ValidateDataSet(ds))
                {
                    result = MapProgramacionEmbarqueDAL.ObtenerRuteosPorOrganizacionId(ds);
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
        /// Método que obtiene los detalles del embarque ruteo seleccionado.
        /// </summary>
        /// <param name="embarqueInfo"> Información del embarque ruteo a editar. </param>
        /// <returns> Listado con los ruteos del embarque </returns>
        internal List<EmbarqueRuteoInfo> ObtenerDetallesEmbarqueRuteo(EmbarqueInfo embarqueInfo)
        {
            var result = new List<EmbarqueRuteoInfo>();
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProgramacionEmbarqueDAL.ObtenerDetallesEmbarqueRuteo(embarqueInfo);
                DataSet ds = Retrieve("ProgramacionEmbarque_ObtenerDetallesEmbarqueRuteo", parameters);

                if (ValidateDataSet(ds))
                {
                    result = MapProgramacionEmbarqueDAL.ObtenerDetallesEmbarqueRuteo(ds);
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
        /// Metodo para consultar la informacion si cuenta con folio embarque
        /// </summary>
        /// <param name="embarqueId"> Información del embarque </param>
        /// <returns> folio de embarque </returns>
        internal EmbarqueInfo CuentaConFolioEmbarque(int embarqueId)
        {
            var result = new EmbarqueInfo();
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProgramacionEmbarqueDAL.ObtenerFolioEmbarque(embarqueId);
                DataSet ds = Retrieve("ProgramacionEmbarque_ObtenerCuentaConFolioEmbarque", parameters);

                if (ValidateDataSet(ds))
                {
                    result = MapProgramacionEmbarqueDAL.ObtenerFolioEmbarque(ds);
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
        /// Método que valida que el origen y destino ingresado cuente con tarifa.
        /// </summary>
        /// <param name="ruteoInfo"> Objeto de ruteo que contiene los parametros </param>
        /// <returns> Variable booleana que indica si tiene o no tarifa. </returns>
        internal int ObtenerFleteOrigenDestino(RuteoInfo ruteoInfo)
        {
            var result = new List<ConfiguracionEmbarqueInfo>();
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProgramacionEmbarqueDAL.ObtenerRuteosPorOrganizacionId(ruteoInfo);
                DataSet ds = Retrieve("ProgramacionEmbarque_ObtenerFleteOrigenDestino", parameters);

                if (ValidateDataSet(ds))
                {
                    result = MapProgramacionEmbarqueDAL.ObtenerFleteOrigenDestino(ds);
                }
                if (result.Count > 0)
                {
                    return result[0].ConfiguracionEmbarqueID;
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
            if (result.Count > 0)
            {
                return result[0].ConfiguracionEmbarqueID;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Método que obtiene los datos necesarios para mostrar en la pestaña de transportes
        /// </summary>
        /// <param name="embarqueInfo"> Objeto que contiene los parametros necesarios. </param>
        /// <returns> Listado de embarques encontrados. </returns>
        internal List<EmbarqueInfo> ObtenerProgramacionTransporte(EmbarqueInfo embarqueInfo)
        {
            var result = new List<EmbarqueInfo>();
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProgramacionEmbarqueDAL.ObtenerProgramacionTransporte(embarqueInfo);
                DataSet ds = Retrieve("ProgramacionEmbarque_ObtenerProgramacionTransportePorOrganizacionID", parameters);

                if (ValidateDataSet(ds))
                {
                    result = MapProgramacionEmbarqueDAL.ObtenerProgramacionTransporte(ds);
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
        /// Metodo que Crea una informacion de trasporte para una programación de embarque
        /// </summary>
        /// <param name="info"></param>
        internal void CrearTransporteEmbarque(EmbarqueInfo info)
        {
            try
            {
                Dictionary<string, object> parameters = AuxProgramacionEmbarqueDAL.ObtenerParametrosCrearTransporteEmbarque(info);

                Create("ProgramacionEmbarque_GuardarTransporteEmbarque", parameters);
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
        /// Metodo que elimina la informacion del embarque registrada en la seccion de datos
        /// </summary>
        /// <returns></returns>
        internal void EliminarInformacionDatos(EmbarqueInfo info)
        {
            try
            {
                Dictionary<string, object> parameters = AuxProgramacionEmbarqueDAL.ObtenerParametrosEliminarInformacionDatos(info);

                Delete("ProgramacionEmbarque_EliminarInformacionDatos", parameters);
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
        /// Método que obtiene los datos necesarios para mostrar en la pestaña de datos de programación embarque.
        /// </summary>
        /// <param name="embarqueInfo"> Objeto de tipo embarque que contiene la organización y el embarque para consultar </param>
        /// <returns> Listado de embarques encontrados. </returns>
        internal List<EmbarqueInfo> ObtenerProgramacionDatos(EmbarqueInfo embarqueInfo)
        {
            var result = new List<EmbarqueInfo>();
            try
            {
                Logger.Info();

                Dictionary<string, object> parameters = AuxProgramacionEmbarqueDAL.ObtenerProgramacionDatos(embarqueInfo);
                DataSet ds = Retrieve("ProgramacionEmbarque_ObtenerProgramacionEmbarqueDatos", parameters);

                if (ValidateDataSet(ds))
                {
                    result = MapProgramacionEmbarqueDAL.ObtenerProgramacionDatos(ds);
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
        /// Metodo que actualiza la informacion del embarque registrada en la seccion de datos
        /// </summary>
        /// <returns></returns>
        internal void ActualizarTransporteEmbarque(EmbarqueInfo info)
        {
            try
            {
                Dictionary<string, object> parameters = AuxProgramacionEmbarqueDAL.ObtenerParametrosActualizarTransporteEmbarque(info);

                Update("ProgramacionEmbarque_ActualizarTransporteEmbarque", parameters);
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
        /// Metodo de guardado para seccion de datos
        /// </summary>
        /// <returns>Regresa embarque info con folio de embarque registrado</returns>
        internal EmbarqueInfo CrearDatosEmbarque(EmbarqueInfo info)
        {
            var result = new EmbarqueInfo();
            try
            {
                Dictionary<string, object> parameters = AuxProgramacionEmbarqueDAL.ObtenerParametrosCrearDatosEmbarque(info);
                DataSet ds = Retrieve("ProgramacionEmbarque_GuardarDatosEmbarque", parameters);

                if (ValidateDataSet(ds))
                {
                    result = MapProgramacionEmbarqueDAL.ObtenerFolioEmbarque(ds);
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
        /// Metodo de actualizado para seccion de datos
        /// </summary>
        /// <returns>Regresa embarque info con folio de embarque registrado</returns>
        internal EmbarqueInfo ActualizarDatosEmbarque(EmbarqueInfo info)
        {
            var result = new EmbarqueInfo();
            try
            {
                Dictionary<string, object> parameters = AuxProgramacionEmbarqueDAL.ObtenerParametrosActualizarDatosEmbarque(info);
                DataSet ds = Retrieve("ProgramacionEmbarque_ActualizarDatosEmbarque", parameters);

                if (ValidateDataSet(ds))
                {
                    result = MapProgramacionEmbarqueDAL.ObtenerFolioEmbarque(ds);
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
        /// Metodo para consultar el costo del flete 
        /// </summary>
        /// <param name="embarqueTarifa"> Información del embarque </param>
        /// <returns> Costo Flete</returns>
        internal EmbarqueTarifaInfo ObtenerCostoFlete(EmbarqueTarifaInfo embarqueTarifa)
        {
            var result = new EmbarqueTarifaInfo();
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProgramacionEmbarqueDAL.ObtenerCostoFlete(embarqueTarifa);
                DataSet ds = Retrieve("ProgramacionEmbarque_ObtenerCostoFlete", parameters);

                if (ValidateDataSet(ds))
                {
                    result = MapProgramacionEmbarqueDAL.ObtenerCostoFlete(ds);
                }
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
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
        /// Método que obtiene los gastos fijos de acuerdo a la ruta seleccionada.
        /// </summary>
        /// <param name="embarqueInfo"> Objeto contenedor de los filtros para buscar. </param>
        /// <returns> Objeto que contiene los gastos fijos asignados a la ruta seleccionada. </returns>
        internal AdministracionDeGastosFijosInfo ObtenerGastosFijos(EmbarqueInfo embarqueInfo)
        {
            var result = new AdministracionDeGastosFijosInfo();
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxProgramacionEmbarqueDAL.ObtenerGastosFijos(embarqueInfo);
                DataSet ds = Retrieve("ProgramacionEmbarque_ObtenerGastoTarifa", parameters);

                if (ValidateDataSet(ds))
                {
                    result = MapProgramacionEmbarqueDAL.ObtenerGastosFijos(ds);
                }
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
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
        /// Metodo que crea una observación para la programacion embarque
        /// </summary>
        /// <param name="info"></param>
        internal void CrearProgramacionEmbarqueObservacion(EmbarqueInfo info)
        {
            try
            {
                Logger.Info();
                // Informacion que se registrará en tabla EmbarqueObservaciones
                Dictionary<string, object> parametersObservaciones = AuxProgramacionEmbarqueDAL.ObtenerParametrosCrearObservaciones(info);
                Create("ProgramacionEmbarque_GuardarObservaciones", parametersObservaciones);
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
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
        }

        /// <summary>
        /// Método que obtiene la información que se enviará en el correo.
        /// </summary>
        /// <param name="info"> Objeto contenedor de los filtros de búsqueda. </param>
        /// <returns> Objeto que contiene la información para enviar al correo. </returns>
        internal EmbarqueInfo ObtenerInformacionCorreo(EmbarqueInfo info)
        {
            var result = new EmbarqueInfo();
            try
            {
                Dictionary<string, object> parameters = AuxProgramacionEmbarqueDAL.ObtenerInformacionCorreo(info);
                DataSet ds = Retrieve("ProgramacionEmbarque_ObtenerInformacionCorreoPorEmbarqueID", parameters);

                if (ValidateDataSet(ds))
                {
                    result = MapProgramacionEmbarqueDAL.ObtenerInformacionCorreo(ds);
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
        /// Metodo para validar estatus del embarque
        /// </summary>
        /// <returns>Regresa embarque info con folio, estatus de embarque registrado</returns>
        internal EmbarqueInfo ValidarEstatus(EmbarqueInfo info)
        {
            var result = new EmbarqueInfo();
            try
            {
                Dictionary<string, object> parameters = AuxProgramacionEmbarqueDAL.ObtenerParametrosPorEmbarqueId(info);
                DataSet ds = Retrieve("ProgramacionEmbarque_ObtenerEstatus", parameters);

                if (ValidateDataSet(ds))
                {
                    result = MapProgramacionEmbarqueDAL.ObtenerEstatusValido(ds);
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
    }
}
