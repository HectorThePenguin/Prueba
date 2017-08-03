using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SIE.Base.Integracion.DAL;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Infos;
using System.Data;
using System.Data.SqlClient;
using SIE.Base.Log;
using SIE.Services.Info.Modelos;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Excepciones;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class CorralDAL : DALBase
    {
        internal ResultadoInfo<CorralInfo> ObtenerPorPagina(PaginacionInfo pagina, CorralInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("[dbo].[Corral_ObtenerPorPagina]", parameters);
                ResultadoInfo<CorralInfo> lista = null;
                if (ValidateDataSet(ds))
                {
                    lista = MapCorralDAL.ObtenerPorPagina(ds);
                }
                return lista;
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

        internal ResultadoInfo<CorralInfo> ObtenerPorDependencia(PaginacionInfo pagina, CorralInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosPorOrganizacion(pagina, filtro);
                DataSet ds = Retrieve("[dbo].[Corral_ObtenerPorOrganizacion]", parameters);
                ResultadoInfo<CorralInfo> corralInfo = null;
                if (ValidateDataSet(ds))
                {
                    corralInfo = MapCorralDAL.ObtenerPorOrganizacion(ds);
                }
                return corralInfo;
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
        ///     Metodo que crear un corral
        /// </summary>
        /// <param name="info"></param>
        internal int Crear(CorralInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosCrear(info);
                int result = Create("[dbo].[Corral_Crear]", parameters);
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
        /// Metodo para Actualizar Corral
        /// </summary>
        /// <param name="info"></param>
        internal void Actualizar(CorralInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosActualizar(info);
                Create("[dbo].[Corral_Actualizar]", parameters);
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
        /// Obtiene un Corral por ID
        /// </summary>
        /// <param name="corralID">Clave del Corral</param>
        /// <param name="dependencias">Dependencias de Catalogos que Tenga el Corral</param>
        /// <returns></returns>
        internal CorralInfo ObtenerPorId(int corralID, IList<IDictionary<IList<string>, object>> dependencias)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosPorId(corralID, dependencias);
                DataSet ds = Retrieve("[dbo].[Corral_ObtenerPorID]", parameters);
                CorralInfo corralInfo = null;
                if (ValidateDataSet(ds))
                {
                    corralInfo = MapCorralDAL.ObtenerPorId(ds);
                }
                return corralInfo;
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
        /// Metodo que Indica si el Corral Seleccionado
        /// Es Usando en Ruteo
        /// </summary>
        /// <param name="embarqueID"></param>
        /// <param name="corralID"></param>
        /// <returns>
        /// true - En caso de que Si ha sido Seleccionado para Ruteo
        /// flase - En caso de que No ha sido Seleccionado para Ruteo
        /// </returns>
        internal bool CorralSeleccionadoParaRuteo(int embarqueID, int corralID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxCorralDAL.ObtenerParametrosCorralEnRuteo(embarqueID, corralID);
                var corralEnRuteo = RetrieveValue<int>("[dbo].[Corral_ObtenerCorralRuteo]", parametros);
                bool corralEstaEnRuteo = corralEnRuteo > 0;
                return corralEstaEnRuteo;
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

        internal CorralInfo ObtenerPorDependencia(CorralInfo corralInfo)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosPorFolio(corralInfo);
                DataSet ds = Retrieve("[dbo].[Corral_ObtenerPorCodigo]", parameters);
                CorralInfo corralInfoResult = null;
                if (ValidateDataSet(ds))
                {
                    corralInfoResult = MapCorralDAL.ObtenerPorCodigo(ds);
                }
                return corralInfoResult;
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

        internal CorralInfo ObtenerPorId(int corralID)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosPorId(corralID);
                DataSet ds = Retrieve("[dbo].[Corral_ObtenerPorID]", parameters);
                CorralInfo corralInfoResult = null;
                if (ValidateDataSet(ds))
                {
                    corralInfoResult = MapCorralDAL.ObtenerPorId(ds);
                }
                return corralInfoResult;
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
        /// Metodo para validar la existencia de un corral segun su codigo y que no tenga un lote asignado
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="codigoCorral"></param>
        internal CorralInfo ObtenerValidacionCorral(int organizacionID, string codigoCorral)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxCorralDAL.ObtenerParametrosValidacion(organizacionID,
                                                                                                 codigoCorral);
                DataSet ds = Retrieve("Corral_ObtenerValidacionCorral", parametros);
                CorralInfo corral = null;

                if (ValidateDataSet(ds))
                {
                    corral = MapCorralDAL.ObtenerId(ds);

                }
                return corral;
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
        /// Obtiene el Corral por ID
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <returns></returns>
        internal CorralInfo ObtenerPorCodicoCorral(CorralInfo corralInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxCorralDAL.ObtenerParametrosValidacion(corralInfo);
                DataSet ds = Retrieve("Corral_ObtenerCorralPorCodigo", parametros);
                CorralInfo corral = null;

                if (ValidateDataSet(ds))
                {
                    corral = MapCorralDAL.ObtenerPorId(ds);

                }
                return corral;
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
        /// Obtiene el Corral por ID
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <returns></returns>
        internal CorralInfo ObtenerPorCodicoCorraleta(CorralInfo corralInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxCorralDAL.ObtenerParametrosValidacionCorraleta(corralInfo);
                DataSet ds = Retrieve("SalidaRecuperacion_ObtenerCorralPorCodigo", parametros);
                CorralInfo corral = null;

                if (ValidateDataSet(ds))
                {
                    corral = MapCorralDAL.ObtenerPorId(ds);

                }
                return corral;
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
        /// Obtiene la lista de corrales destino disponibles para programacion de reimplantes
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<CorralInfo> ObtenerParaProgramacionReimplanteDestino(PaginacionInfo pagina, CorralInfo filtro)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxCorralDAL.ObtenerParametrosPorOrganizacionId( pagina, filtro);
                DataSet ds = Retrieve("OrdenReimplante_ObtenerCorralesDestinoPorPagina", parametros);
                ResultadoInfo<CorralInfo> corrales = null;

                if (ValidateDataSet(ds))
                {
                    corrales = MapCorralDAL.ObtenerPorPagina(ds);
                }
                return corrales;
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
        /// Obtiene la lista de corrales destino disponibles para programacion de reimplantes
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal CorralInfo ObtenerPorCodigoParaProgramacionReimplanteDestino(CorralInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object>
                {
                        {"@OrganizacionID", filtro.OrganizacionId},
                        {"@Codigo", filtro.Codigo}
                    };
                DataSet ds = Retrieve("OrdenReimplante_ObtenerCorralesDestinoPorCodigo", parametros);
                CorralInfo corrales = null;

                if (ValidateDataSet(ds))
                {
                    corrales = MapCorralDAL.ObtenerPorCodigoCorral(ds);
                }
                return corrales;
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
        /// Obtiene el corral que ya fue asignado a un embarque
        /// </summary>
        /// <param name="folioEmbarque"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal CorralInfo ObtenerPorEmbarqueRuteo(int folioEmbarque, int organizacionID)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosPorEmbarqueRuteo(folioEmbarque,
                                                                                                       organizacionID);
                DataSet ds = Retrieve("Corral_ObtenerPorEmbarqueRuteo", parameters);
                CorralInfo corralInfo = null;

                if (ValidateDataSet(ds))
                {
                    corralInfo = MapCorralDAL.ObtenerCorralPorEmbarqueRuteo(ds);
                }
                return corralInfo;
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
        /// Obtiene un registro de Corral
        /// </summary>
        /// <param name="descripcion">Descripción de la Corral</param>
        /// <returns></returns>
        internal CorralInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("Corral_ObtenerPorDescripcion", parameters);
                CorralInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCorralDAL.ObtenerPorDescripcion(ds);
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
        /// Obtiene un registro de Corral
        /// </summary>
        /// <param name="corral">objeto que debe de tener codigo de corral, grupo y organizacion</param>
        /// <returns>Informacion del corral</returns>
        internal CorralInfo ObtenerPorCodigoGrupo(CorralInfo corral)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosPorCodigoGrupo(corral);
                DataSet ds = Retrieve("Corral_ObtenerCorralPorCodigoGrupo", parameters);
                CorralInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCorralDAL.ObtenerPorCodigoGrupo(ds);
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
        /// Obtiene los corrales por tipo
        /// </summary>
        /// <param name="corral"></param>
        /// <param name="listaTiposCorral"></param>
        /// <returns></returns>
        internal ResultadoInfo<CorralInfo> ObtenerCorralesPorTipo(CorralInfo corral, List<int> listaTiposCorral)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerCorralesPorTipo(corral, listaTiposCorral);
                DataSet ds = Retrieve("[dbo].[Corral_ObtenerCorralesPorTiposGrupo]", parameters);
                ResultadoInfo<CorralInfo> corralInfo = null;
                if (ValidateDataSet(ds))
                {
                    corralInfo = MapCorralDAL.ObtenerCorralesPorTipo(ds);
                }
                return corralInfo;
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
        /// Obtiene los corrales por tipo
        /// </summary>
        /// <param name="corral"></param>
        /// <returns></returns>
        internal ResultadoInfo<CorralInfo> ObtenerCorralesPorTipo(CorralInfo corral)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerCorralesPorTipo(corral);
                DataSet ds = Retrieve("[dbo].[Corral_ObtenerCorralesPorTipo]", parameters);
                ResultadoInfo<CorralInfo> corralInfo = null;
                if (ValidateDataSet(ds))
                {
                    corralInfo = MapCorralDAL.ObtenerCorralesPorTipo(ds);
                }
                return corralInfo;
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
        /// Obtiene los corrales por tipo
        /// </summary>
        /// <param name="corral"></param>
        /// <returns></returns>
        internal ResultadoInfo<CorralInfo> ObtenerCorralesPorTipoCorralDetector(CorralInfo corral)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerCorralesPorTipoCorralDetector(corral);
                DataSet ds = Retrieve("[dbo].[Corral_ObtenerCorralesPorTipoCorralDetector]", parameters);
                ResultadoInfo<CorralInfo> corralInfo = null;
                if (ValidateDataSet(ds))
                {
                    corralInfo = MapCorralDAL.ObtenerCorralesPorTipo(ds);
                }
                return corralInfo;
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
        /// Obtiene un corral por codigo
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="corralCodigo"></param>
        /// <returns></returns>
        internal CorralInfo ObtenerCorralPorCodigo(int organizacionId, string corralCodigo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosCorralPorCodigo(organizacionId,
                                                                                                      corralCodigo);
                DataSet ds = Retrieve("DeteccionGanado_ObtenerCorral", parameters);
                CorralInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCorralDAL.ObtenerCorralPorCodigo(ds);
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
        /// Obtiene la partida de un corral
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="corralID"></param>
        /// <returns></returns>
        internal EntradaGanadoInfo ObtenerPartidaCorral(int organizacionId, int corralID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxCorralDAL.ObtenerParametrosObtenerPartidaCorral(organizacionId, corralID);
                DataSet ds = Retrieve("DeteccionGanado_ObtenerPartida", parameters);
                EntradaGanadoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCorralDAL.ObtenerPartidaCorral(ds);
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
        /// Validar codigoCorral por enfermeria
        /// </summary>
        /// <param name="codigoCorral"></param>
        /// <param name="enfermeria"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>

        internal bool ValidarCodigoCorralPorEnfermeria(string codigoCorral, int enfermeria, int organizacionId)
        {
            try
            {
                Dictionary<string, object> parameters =
                    AuxCorralDAL.ObtenerParametrosPorCodigoCorralEnfermeria(codigoCorral, enfermeria, organizacionId);
                DataSet ds = Retrieve("Corral_ValidarCorralPorEnfermeria", parameters);

                bool esValido = true;

                if (ValidateDataSet(ds))
                {
                    esValido = MapCorralDAL.ObtenerValidarCodigoCorralPorEnfermeria(ds);
                }
                return esValido;
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
        /// Actualizar corrales cabeza
        /// </summary>
        /// <param name="animalMovimiento"></param>
        /// <param name="loteOrigen"></param>
        /// <returns></returns>
        internal string ActualizarCorralesCabezas(AnimalMovimientoInfo animalMovimiento, int loteOrigen)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxCorralDAL.ObtenerParametrosActualizarCorrales(animalMovimiento, loteOrigen);
                DataSet ds = Retrieve("DeteccionGanado_ObtenerPartida", parameters);
                string result = "";
                if (ValidateDataSet(ds))
                {
                    result = MapCorralDAL.ObtenerDetectorCorral(ds);
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
        /// Existencia de corrales
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="corralCodigo"></param>
        /// <returns></returns>
        internal CorralInfo ObtenerExistenciaCorral(int organizacionId, string corralCodigo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxCorralDAL.ObtenerParametrosObtenerExistenciaCorral(organizacionId, corralCodigo);
                DataSet ds = Retrieve("Corral_ObtenerExistenciaCorral", parameters);
                CorralInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCorralDAL.ObtenerCorralPorCodigoExiste(ds);
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
        /// Obtiene la información para el Reporte Proyector
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal List<ReporteProyectorInfo> ObtenerReporteProyectorComportamiento(int organizacionID)
        {
            try
            {
                List<ReporteProyectorInfo> listaReporteProyectorInfo = null;
                Dictionary<string, object> parameters =
                    AuxCorralDAL.ObtenerParametrosReporteProyectorComportamiento(organizacionID);
                DataSet ds = Retrieve("ReporteProyectorComportamiento_ObtenerDatosReporte", parameters);
                if (ValidateDataSet(ds))
                {
                    listaReporteProyectorInfo = MapCorralDAL.ObtenerReporteProyectorComportamiento(ds);
                }
                return listaReporteProyectorInfo;
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
        /// Contar cabezas
        /// </summary>
        /// <param name="corral"></param>
        /// <returns></returns>
        internal int ContarCabezas(CorralInfo corral)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosContarCabezas(corral);
                DataSet ds = Retrieve("Corral_ContarCabezas", parameters);
                int result = 0;
                if (ValidateDataSet(ds))
                {
                    result = MapCorralDAL.ObtenerContarCabezas(ds);
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
        /// Obtener lista de aretes de corraletas
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <param name="loteOrigen"></param>
        /// <returns></returns>
        internal List<AnimalInfo> ObtenerAretesCorraleta(CorralInfo corralInfo, LoteInfo loteOrigen)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosObtenerAretesCorraleta(corralInfo, loteOrigen);
                DataSet ds = Retrieve("salidaRecuperacion_ObtenerAretesCorraleta", parameters);
                List<AnimalInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalDAL.ObtenerAreteCorraleta(ds);
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
        /// Traspasar animales a enfermeria
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <param name="loteOrigen"> </param>
        internal void TraspasarAnimalSalidaEnfermeria(int corralInfo, int loteOrigen)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosTraspasarAnimalSalidaEnfermeria(corralInfo, loteOrigen);
                Update("SalidaRecuperacionCorral_EliminarAnimalSalidaPorCorraleta", parameters);
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
        /// Obtiene el Corral por Organizacion y Codigo
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <returns></returns>
        internal CorralInfo ObtenerPorCodicoOrganizacionCorral(CorralInfo corralInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxCorralDAL.ObtenerParametrosCodigoOrganizacion(corralInfo);
                DataSet ds = Retrieve("Corral_ObtenerPorOrganizacionCodigo", parametros);
                CorralInfo corral = null;

                if (ValidateDataSet(ds))
                {
                    corral = MapCorralDAL.ObtenerPorCodigoOrganizacion(ds);

                }
                return corral;
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
        /// Valicar corral de enfermeria
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <returns></returns>
        internal CorralInfo ValidarCorralEnfermeria(CorralInfo corralInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxCorralDAL.ObtenerParametrosObtenerAretesCorral(corralInfo);
                DataSet ds = Retrieve("SalidaRecuperacion_ObtenerCorralEnfermeria", parameters);
                CorralInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCorralDAL.ObtenerCorralPorCodigoExiste(ds);
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
        /// Obtiene un corral por codigo
        /// </summary>
        /// <param name="corral"></param>
        /// <returns></returns>
        internal CorralInfo ObtenerPorCodigoGrupoCorral(CorralInfo corral)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosPorCodigoGrupoCorral(corral);
                DataSet ds = Retrieve("TraspasoGanado_ObtenerCorralPorCodigoGrupo", parameters);
                CorralInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCorralDAL.ObtenerPorCodigoGrupo(ds);
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
        /// Verifica si existe interfaces salida
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="corralCodigo"></param>
        /// <param name="arete"></param>
        /// <returns></returns>
        internal int ObtenerExisteInterfaceSalida(int organizacionID,string corralCodigo, string arete)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxCorralDAL.ObtenerParametrosObtenerExisteInterfaceSalida(organizacionID, corralCodigo, arete);
                DataSet ds = Retrieve("DeteccionGanado_ConsultaCorralRecepcion", parametros);
                int retorno = 0;

                if (ValidateDataSet(ds))
                {
                    retorno = MapCorralDAL.ObtenerExisteInterfaceSalida(ds);
                }
                return retorno;
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

        internal CorralInfo ObtenerCorralPorLoteID(int loteID, int organizacionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosObtenerCorralPorLoteID(loteID, organizacionID);
                DataSet ds = Retrieve("DeteccionGanado_ConsultaCorralCorraleta", parameters);
                CorralInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCorralDAL.ObtenerCorralPorLoteID(ds);
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
        /// Obtiene corrales validos para reparto
        /// </summary>
        /// <param name="organicacionId"></param>
        /// <returns></returns>
        internal ResultadoInfo<CorralInfo> ObtenerCorralesParaReparto(int organicacionId)
        {
            try
            {
                var parameters = AuxCorralDAL.ObtenerParametrosCorralesParaReparto(organicacionId);
                var ds = Retrieve("[Reparto_ObtenerCorralesReparto]", parameters);
                ResultadoInfo<CorralInfo> corralInfo = null;
                if (ValidateDataSet(ds))
                {
                    corralInfo = MapCorralDAL.ObtenerCorralesParaReparto(ds);
                }
                return corralInfo;
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
        /// Obtiene el listado de corrales dependiendo del Grupo Corral al que pertenecen
        /// </summary>
        /// <param name="grupo"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal IList<CorralInfo> ObtenerPorCorralesPorGrupo(GrupoCorralInfo grupo, int organizacionId)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosCorralesPorGrupoCorral(grupo, organizacionId);
                DataSet ds = Retrieve("Corral_ObtenerCorralesPorGrupoCorral", parameters);
                IList<CorralInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCorralDAL.ObtenerPorCorralesPorGrupo(ds);
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
        /// Obtiene el listado de corrales dependiendo del Grupo Corral Enfermeria y que tengan programacion en servicio de alimentos.
        /// </summary>
        /// <param name="grupo"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal IList<CorralInfo> ObtenerPorCorralesPorGrupoConProgramacionDeAlimentos(GrupoCorralInfo grupo, int organizacionId)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosCorralesPorGrupoCorral(grupo, organizacionId);
                DataSet ds = Retrieve("Corral_ObtenerCorralesPorGrupoCorralEnfermeria", parameters);
                IList<CorralInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCorralDAL.ObtenerPorCorralesPorGrupo(ds);
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
        /// Obtiene un corral por su grupo corral
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <returns></returns>
        internal CorralInfo ObtenerPorGrupoCorral(CorralInfo corralInfo)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosPorGrupoCorral(corralInfo);
                DataSet ds = Retrieve("[dbo].[Corral_ObtenerPorCodigoGrupoCorral]", parameters);
                CorralInfo corralInfoResult = null;
                if (ValidateDataSet(ds))
                {
                    corralInfoResult = MapCorralDAL.ObtenerPorGrupoCorral(ds);
                }
                return corralInfoResult;
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
        /// Obtiene una lista de corrales paginada
        /// por su grupo corral
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<CorralInfo> ObtenerPorPaginaGrupoCorral(PaginacionInfo pagina, CorralInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosPorPaginaGrupoCorral(pagina, filtro);
                DataSet ds = Retrieve("[dbo].[Corral_ObtenerPorPaginaGrupoCorral]", parameters);
                ResultadoInfo<CorralInfo> lista = null;
                if (ValidateDataSet(ds))
                {
                    lista = MapCorralDAL.ObtenerPorPaginaGrupoCorral(ds);
                }
                return lista;
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
        /// Obtiene un corral por codigo que este activo y tenga lote activo.
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="corralCodigo"></param>
        /// <returns></returns>
        internal CorralInfo ObtenerCorralActivoPorCodigo(int organizacionId, string corralCodigo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosCorralPorCodigo(organizacionId,
                                                                                                      corralCodigo);
                DataSet ds = Retrieve("Corral_ObtenerCorralActivosPorCodigo", parameters);
                CorralInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCorralDAL.ObtenerCorralPorCodigo(ds);
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
        /// Obtiene un registro de Corral
        /// </summary>
        /// <param name="descripcion">Descripción de la Corral</param>
        /// <param name="organizacionID">Organizacion del Corral </param>
        /// <returns></returns>
        internal CorralInfo ObtenerPorDescripcionOrganizacion(string descripcion, int organizacionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosPorDescripcionOrganizacion(descripcion, organizacionID);
                DataSet ds = Retrieve("Corral_ObtenerPorDescripcionOrganizacion", parameters);
                CorralInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCorralDAL.ObtenerPorDescripcionOrganizacion(ds);
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

        internal List<CorralInfo> ObtenerCorralesPorTipoCorral(TipoCorralInfo tipoCorral, int organizacionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerCorralesPorTipoCorral(tipoCorral.TipoCorralID, organizacionID);
                DataSet ds = Retrieve("Corral_ObtenerCorralesPorTipo", parameters);
                List<CorralInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCorralDAL.ObtenerCorralesPorTipoCorral(ds);
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


        internal List<CorralInfo> ObtenerCorralesPorCodigosCorral(List<string> codigosCorral, int organizacionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosCorralesPorCodigosCorral(codigosCorral,organizacionID);
                DataSet ds = Retrieve("Corral_ObtenerCorralesPorCodigosCorral", parameters);
                List<CorralInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCorralDAL.ObtenerCorralesPorTipoCorral(ds);
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
        /// Obtiene de la base de datos una lista de corrales paginada para la ayuda filtrada por uno o mas grupo de corrales por organizacionid
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CorralInfo> ObtenerPorPaginaGruposCorrales(PaginacionInfo pagina, CorralInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosPorPaginaGruposCorrales(pagina, filtro);
                DataSet ds = Retrieve("[dbo].[Corral_ObtenerPorPaginaGruposCorrales]", parameters);
                ResultadoInfo<CorralInfo> lista = null;
                if (ValidateDataSet(ds))
                {
                    lista = MapCorralDAL.ObtenerPorPaginaGruposCorrales(ds);
                }
                return lista;
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
        /// Obtiene una lista de corrales por xml
        /// </summary>
        /// <param name="corrales"></param>
        /// <returns></returns>
        internal IList<CorralInfo> ObtenerPorCorralIdXML(List<CorralInfo> corrales)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosPorIdXML(corrales);
                DataSet ds = Retrieve("Corral_ObtenerPorCorralIDXML", parameters);
                List<CorralInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCorralDAL.ObtenerCorralesPorCorralIdXML(ds);
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
        /// Valida que el corral tenga existencia
        /// </summary>
        /// <param name="corralID"></param>
        /// <returns></returns>
        internal CorralInfo ValidaCorralConLoteConExistenciaActivo(int corralID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxCorralDAL.ObtenerParametrosValidaCorralConLoteConExistenciaActivo(corralID);
                DataSet ds = Retrieve("Corral_ObtenerValidaCorralConLoteConExistenciaActivo", parameters);
                CorralInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCorralDAL.ObtenerValidaCorralConLoteConExistenciaActivo(ds);
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
        /// Verifica si existe interfaces salida
        /// </summary>
        /// <param name="lote"></param>
        /// <returns></returns>
        internal int ObtenerDiasEngordaPorLote(LoteInfo lote)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxCorralDAL.ObtenerParametrosObtenerDiasEngordaPorLote(lote);
                var retorno = RetrieveValue<int>("Corral_ObtenerDiasEngordaPorLote", parametros);
                return retorno;
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
        /// Obtiene las Secciones de los corrales
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal List<SeccionModel> ObtenerSeccionesCorral(int organizacionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxCorralDAL.ObtenerSeccionesCorral(organizacionID);
                DataSet ds = Retrieve("Corral_ObtenerSeccionesCorral", parameters);
                List<SeccionModel> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCorralDAL.ObtenerSeccionesCorral(ds);
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
        internal ResultadoInfo<CorralInfo> ObtenerFormulaPorDependencia(PaginacionInfo pagina, CorralInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosPorFolio(pagina, filtro);
                DataSet ds = Retrieve("[dbo].[Corral_FormulaObtenerPorOrganizacion]", parameters);
                ResultadoInfo<CorralInfo> corralInfo = null;
                if (ValidateDataSet(ds))
                {
                    corralInfo = MapCorralDAL.ObtenerPorOrganizacion(ds);
                }
                return corralInfo;
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
        /// Obtiene Parametros para Obtener los corrales improductivos para la pantalla Corte por Transferencia
        /// </summary>
        /// <param name="tipoCorralID"></param>
        /// <returns></returns>
        internal List<CorralInfo> ObtenerCorralesImproductivos(int tipoCorralID)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosCorralesImproductivos(tipoCorralID);
                DataSet ds = Retrieve("[dbo].[Corral_ObtenerPorCorralesImproductivos]", parameters);
                List<CorralInfo> corralInfo = null;
                if (ValidateDataSet(ds))
                {
                    corralInfo = MapCorralDAL.ObtenerCorralesImproductivos(ds);
                }
                return corralInfo;
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

        public CorralInfo ObtenerFormulaCorralPorID(CorralInfo corral)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosObtenerFormulaCorralPorID(corral);
                DataSet ds = Retrieve("[dbo].[Corral_FormulaObtenerPorCorralID]", parameters);
                CorralInfo corralInfo = null;
                if (ValidateDataSet(ds))
                {
                    corralInfo = MapCorralDAL.ObtenerCorralFormulaPorId(ds);
                }
                return corralInfo;
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
        /// Obtener lista de corralestas configuradas para sacrificio
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="codigoCorraletas"></param>
        /// <returns></returns>
        internal ResultadoInfo<CorralInfo> ObtenerInformacionCorraletasDisponiblesSacrificio(int organizacionId, string codigoCorraletas)
        {
            try
            {
                var lista = codigoCorraletas.Split('|');
                

                var xml =
                   new XElement("ROOT",from codigo in lista select new XElement("Corral", new XElement("Corraleta", codigo.Trim())));
                var parametros = new Dictionary<string, object>{{"@OrganizacionID",organizacionId},{"@CodigosCorraletaXML", xml.ToString()}};
                DataSet ds = Retrieve("[dbo].[Corral_ObtenerInformacionCorraletasDisponiblesSacrificio]", parametros);
                ResultadoInfo<CorralInfo> corralInfo = null;
                if (ValidateDataSet(ds))
                {
                    corralInfo = MapCorralDAL.ObtenerInformacionCorraletasDisponiblesSacrificio(ds);
                }
                return corralInfo;
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
        /// Verifica si existe interfaces salida
        /// </summary>
        /// <param name="lotesXml"></param>
        /// <returns></returns>
        internal IList<DiasEngordaLoteModel> ObtenerDiasEngordaPorLoteXML(IList<LoteInfo> lotesXml)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxCorralDAL.ObtenerParametrosObtenerDiasEngordaPorLoteXML(lotesXml);
                DataSet ds = Retrieve("[dbo].[Corral_ObtenerDiasEngordaPorLoteXML]", parametros);
                IList<DiasEngordaLoteModel> retorno = null;
                if (ValidateDataSet(ds))
                {
                    retorno = MapCorralDAL.ObtenerDiasEngordaPorLoteXML(ds);
                }
                return retorno;
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
        /// Obtener corral por codigo
        /// </summary>
        /// <param name="corral"></param>
        /// <returns></returns>
        public CorralInfo ObtenerCorralesPorTipos(CorralInfo corral)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosCorralesPorTipos(corral);
                DataSet ds = Retrieve("[dbo].[Corral_ObtenerPorOrganizacionCodigoLoteActivo]", parameters);
                CorralInfo corralInfo = null;
                if (ValidateDataSet(ds))
                {
                    corralInfo = MapCorralDAL.ObtenerCorralesPorTipos(ds);
                }
                return corralInfo;
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
        /// Obtiene lista de corrales por pagina
        /// </summary>
        /// <param name="paginacion"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CorralInfo> ObtenerCorralesPorPagina(PaginacionInfo paginacion, CorralInfo filtro)
        {
            ResultadoInfo<CorralInfo> listaCorrales = null;
            try
            {
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosPorTiposCorrales(paginacion, filtro);
                DataSet ds = Retrieve("[dbo].[Corral_ObtenerPorPaginaPorTipos]", parameters);
                if (ValidateDataSet(ds))
                {
                    listaCorrales = MapCorralDAL.ObtenerPorPagina(ds);
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
            return listaCorrales;
        }

        /// <summary>
        /// Consulta todos los costos de una Entrada de Ganado Transito por id
        /// </summary>
        /// <param name="entradaGanadoTransitoId">Entrada de ganado en transito al cual se le buscaran los costos</param>
        /// <returns>Regresa la lista de costos de la entrada de ganado en transito</returns>
        public IList<CostoCorralInfo> ObtenerCostosCorralPorEntradaGanadoTransito(int entradaGanadoTransitoId)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosPorEntradaGanadoTransitoID(entradaGanadoTransitoId);
                DataSet ds = Retrieve("SalidaGanadoTransito_ObtenerCostoCorralByEGTID", parameters);
                IList<CostoCorralInfo> result;

                if (ValidateDataSet(ds))
                {
                    result = MapCorralDAL.ObtenerPorEntradaGanadoTransito(ds);
                }
                else
                {
                    result = new List<CostoCorralInfo>();
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
        /// Consulta todos los corrales validos para registrar una salida por venta o muerte
        /// </summary>
        /// <param name="organizacionId">Organizacion en en el cual se busca el corral </param>
        /// <returns>Regresa la lista corrales configurados para salida de ganado en transito por venta o muerte</returns>
        public IList<CorralesPorOrganizacionInfo> ObtenerCorralesPorOrganizacionID(int organizacionId)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCorralDAL.ObtenerParametrosPorOrganizacionId(organizacionId);
                DataSet ds = Retrieve("SalidaGanadoTransito_CorralesSalidaByOrganizacionID", parameters);
                IList<CorralesPorOrganizacionInfo> result;

                if (ValidateDataSet(ds))
                {
                    result = MapCorralDAL.ObtenerPorUsuarioId(ds);
                }
                else
                {
                    result = new List<CorralesPorOrganizacionInfo>();
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
