using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    /// <summary>
    /// Clase para administrar los eventos de implementacion para el acceso a la base de datos para Muertes
    /// </summary>
    internal class MuerteDAL : DALBase
    {
        /// <summary>
        /// Obtiene la lista de ganado detectado, recolectado e ingresado a necropsia para su salida
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal IList<MuerteInfo> ObtenerMuertosParaNecropsia(int organizacionId)
        {
            IList<MuerteInfo> retValue = null;
            try
            {
                Logger.Info();
                
                Dictionary<string, object> parameters = AuxMuerteDAL.ObtenerParametrosPorOrganizacion(organizacionId);
                DataSet ds = Retrieve("SalidaPorMuerte_ObtenerMuertosParaNecropsia", parameters);

                if (ValidateDataSet(ds))
                {
                    retValue = MapMuerteDAL.ObtenerListaParaSalidaNecropsia(ds);
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

            return retValue;
        }

        /// <summary>
        /// Obtiene de la base de datos la informacion de una cabeza de ganado muerta
        /// </summary>
        /// <param name="organizacionId">Id Organizacion</param>
        /// <param name="numeroArete">Numero de arete</param>
        /// <returns></returns>
        internal MuerteInfo ObtenerGanadoMuertoPorArete(int organizacionId, string numeroArete)
        {
            MuerteInfo retValue = null;
            try
            {
                Logger.Info();

                Dictionary<string, object> parameters = AuxMuerteDAL.ObtenerParametrosPorOrganizacionArete(organizacionId, numeroArete);
                DataSet ds = Retrieve("SalidaPorMuerte_ObtenerMuertePorArete", parameters);

                if (ValidateDataSet(ds))
                {
                    retValue = MapMuerteDAL.ObtenerGanadoMuertoPorArete(ds);
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

            return retValue;
        }


        /// <summary>
        /// Obtiene la lista de movimientos(Muertes) a cancelar
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal IList<MuerteInfo> ObtenerInformacionCancelarMovimiento(int organizacionId)
        {
            IList<MuerteInfo> retValue = null;
            try
            {
                Logger.Info();

                Dictionary<string, object> parameters = AuxMuerteDAL.ObtenerParametrosPorOrganizacion(organizacionId);
                DataSet ds = Retrieve("CancelarMovimiento_ObtenerMuertesCancelarMovimiento", parameters);

                if (ValidateDataSet(ds))
                {
                    retValue = MapMuerteDAL.ObtenerInformacionCancelarMovimiento(ds);
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

            return retValue;
        }

        /// <summary>
        /// Guardar en la base de datos la salida por necropsia
        /// </summary>
        /// <param name="muerte">Muerte a guardar</param>
        /// <returns></returns>
        internal int GuardarSalidaPorMuerteNecropsia(MuerteInfo muerte)
        {
            int retValue = -1;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxMuerteDAL.ObtenerParametrosGuardarSalidaPorMuerteNecropsia(muerte);
                Update("SalidaPorMuerte_GuardarSalidaPorMuerteNecropsia", parameters);

                retValue = 1;
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

            return retValue;
        }
        /// <summary>
        /// Metodo para Guardar el muerteInfo
        /// </summary>
        /// <param name="muerteInfo">contenedor donde se encuentra la información de la muerte</param>
        /// <returns></returns>
        internal void CancelarMovimientoMuerte(MuerteInfo muerteInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxMuerteDAL.ObtenerParametrosCancelarMovimientoMuerte(muerteInfo);
                Create("CancelarMovimiento_CancelarMuerte", parameters);
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
        /// Obtiene la lista de muertes listas para recepcion en necropsia
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal IList<MuerteInfo> ObtenerGanadoMuertoParaRecepcion(int organizacionId)
        {
            IList<MuerteInfo> retValue = null;
            try
            {
                Logger.Info();

                Dictionary<string, object> parameters = AuxMuerteDAL.ObtenerParametrosPorOrganizacion(organizacionId);
                DataSet ds = Retrieve("SalidaPorMuerte_ObtenerMuertosParaRecepcion", parameters);

                if (ValidateDataSet(ds))
                {
                    retValue = MapMuerteDAL.ObtenerListaParaRecepcionNecropsia(ds);
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

            return retValue;
        }

        /// <summary>
        /// Actualiza la muerte de un arete con la informacion de recepción en necropsia
        /// </summary>
        /// <param name="muerte"></param>
        /// <param name="operadorId"></param>
        /// <returns></returns>
        internal int GuardarRecepcionGanadoMuerto(MuerteInfo muerte, int operadorId)
        {
            int retValue = -1;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxMuerteDAL.ObtenerParametrosMuerteId(muerte, operadorId);
                Update("SalidaPorMuerte_GuardarRecepcionNecropsia", parameters);

                retValue = 1;
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

            return retValue;
        }

        /// <summary>
        /// Obtiene la lista de aretes muertos para recoleccion de ganado
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal IList<MuerteInfo> ObtenerAretesMuertosRecoleccion(int organizacionId)
        {
            IList<MuerteInfo> retValue = null;
            try
            {
                Logger.Info();

                Dictionary<string, object> parameters = AuxMuerteDAL.ObtenerParametrosPorOrganizacion(organizacionId);
                DataSet ds = Retrieve("CheckListGanadoMuerto_ObtenerAretesMuertosRecoleccion", parameters);

                if (ValidateDataSet(ds))
                {
                    retValue = MapMuerteDAL.ObtenerAretesMuertosRecoleccion(ds);
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

            return retValue;
        }
        /// <summary>
        /// Actualiza la tabla de muertes con la informacion de la recoleccion
        /// </summary>
        /// <param name="muerte">Identificador de la muerte</param>
        /// <param name="operadorId">Identificador del Operador</param>
        /// <returns></returns>
        internal int GuardarRecoleccionGanadoMuerto(MuerteInfo muerte, int operadorId)
        {
            int retValue = -1;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxMuerteDAL.ObtenerParametrosMuerteRecoleccion(muerte, operadorId);
                Update("CheckListGanadoMuerto_GuardarRecoleccion", parameters);

                retValue = 1;
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

            return retValue;
        }

        /// <summary>
        /// Almacena una muerte en la tabla de muertes desde deteccion de ganado
        /// </summary>
        /// <param name="muerte"></param>
        /// <returns></returns>
        internal int GuardarMuerte(MuerteInfo muerte)
        {
            int retValue = -1;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxMuerteDAL.ObtenerParametrosGuardarMuerte(muerte);
                retValue = Create("DeteccionGanado_GrabarMuerte", parameters);

                retValue = 1;
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

            return retValue;
        }

        /// <summary>
        /// Obtiene de la base de datos la informacion de una cabeza de ganado muerta
        /// </summary>
        /// <param name="organizacionId">Id Organizacion</param>
        /// <param name="numeroArete">Numero de arete</param>
        /// <returns></returns>
        public MuerteInfo ObtenerGanadoMuertoPorAreteRecepcion(int organizacionId, string numeroArete)
        {
            MuerteInfo retValue = null;
            try
            {
                Logger.Info();

                Dictionary<string, object> parameters = AuxMuerteDAL.ObtenerParametrosPorOrganizacionArete(organizacionId, numeroArete);
                DataSet ds = Retrieve("SalidaPorMuerte_ObtenerMuertePorAreteRecepcion", parameters);

                if (ValidateDataSet(ds))
                {
                    retValue = MapMuerteDAL.ObtenerGanadoMuertoPorArete(ds);
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

            return retValue;
        }

        /// <summary>
        /// Obtiene las muertes por fecha Necropsia
        /// </summary>
        /// <param name="muerteInfo"></param>
        /// <returns></returns>
        public IList<SalidaGanadoMuertoInfo> ObtenerMuertesFechaNecropsia(MuerteInfo muerteInfo)
        {
            IList<SalidaGanadoMuertoInfo> listaMuertesInfo = null;
            try
            {
                Logger.Info();

                Dictionary<string, object> parameters = AuxMuerteDAL.ObtenerParametrosMuertesFechaNecropsia(muerteInfo);
                DataSet ds = Retrieve("SalidaPorMuerte_ObtenerMuertosFechaNecropsia", parameters);

                if (ValidateDataSet(ds))
                {
                    listaMuertesInfo = MapMuerteDAL.ObtenerMuertesFechaNecropsia(ds);
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

            return listaMuertesInfo;
        }

        /// <summary>
        /// Obtiene la lista de muertes por lote
        /// </summary>
        /// <param name="loteID"></param>
        /// <returns></returns>
        internal IList<MuerteInfo> ObtenerGanadoMuertoPorLoteID(int loteID)
        {
            IList<MuerteInfo> retValue = null;
            try
            {
                Logger.Info();

                Dictionary<string, object> parameters = AuxMuerteDAL.ObtenerParametrosPorLoteID(loteID);
                DataSet ds = Retrieve("DeteccionGanado_ObtenerMuertesPorLoteID", parameters);

                if (ValidateDataSet(ds))
                {
                    retValue = MapMuerteDAL.ObtenerGanadoMuertoPorLoteID(ds);
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

            return retValue;
        }

        /// <summary>
        /// Obtiene el Registro de la tabla Muertes del Arete
        /// </summary>
        /// <param name="organizacionId">Id Organizacion</param>
        /// <param name="numeroArete">Numero de arete</param>
        /// <returns></returns>
        internal MuerteInfo ObtenerMuertoPorArete(int organizacionId, string numeroArete)
        {
            MuerteInfo retValue = null;
            try
            {
                Logger.Info();

                Dictionary<string, object> parameters = AuxMuerteDAL.ObtenerParametrosMuertoPorArete(organizacionId, numeroArete);
                DataSet ds = Retrieve("Muerte_ObtenerMuertePorArete", parameters);

                if (ValidateDataSet(ds))
                {
                    retValue = MapMuerteDAL.ObtenerMuertoPorArete(ds);
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

            return retValue;
        }

    }
    
}
