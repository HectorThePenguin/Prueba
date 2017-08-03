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
    internal class ProblemaDAL : DALBase
    {
        /// <summary>
        /// Obtiene una lista de problemas registrados para sanidad y necropsia
        /// </summary>
        /// <returns></returns>
        internal List<ProblemaInfo> ObtenerListaProblemasNecropsia()
        {
            List<ProblemaInfo> retValue = null;
            try
            {
                Logger.Info();

                DataSet ds = Retrieve("SalidaPorMuerte_ObtenerListaProblemasNecropsia");

                if (ValidateDataSet(ds))
                {
                    retValue = MapProblemaDAL.ObtenerListaProblemasNecropsia(ds);
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
        /// Obtiene los problemas de una deteccion
        /// </summary>
        /// <param name="deteccionId"></param>
        /// <param name="tratamiento"></param>
        /// <returns></returns>
        internal List<ProblemaInfo> ObtenerProblemasDeteccion(AnimalDeteccionInfo deteccionId, TratamientoInfo tratamiento)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxProblemaDAL.ObtenerParametrosObtenerProblemasDeteccion(deteccionId);
                 DataSet ds = Retrieve("Enfermeria_ObtenerProblemasDeteccion",parametros);
                List<ProblemaInfo> probelmas = null;

                if (ValidateDataSet(ds))
                {
                    probelmas = MapProblemaDAL.ObtenerProblemasPorDeteccion(ds, tratamiento);
                }
                return probelmas;
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
        /// Obtiene la lista de problemas secundarios para deteccion de ganado
        /// </summary>
        /// <returns></returns>
        internal IList<ProblemaInfo> ObtenerListaProblemas()
        {
            IList<ProblemaInfo> retValue = null;

            try
            {
                Logger.Info();

                DataSet ds = Retrieve("DeteccionGanado_ObtenerProblemasSecundarios");

                if (ValidateDataSet(ds))
                {
                    retValue = MapProblemaDAL.ObtenerListaProblemasSecundarios(ds);
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

        public List<ProblemaInfo> ObtenerProblemasDeteccionSinActivo(AnimalDeteccionInfo deteccionId, TratamientoInfo tratamiento)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxProblemaDAL.ObtenerParametrosObtenerProblemasDeteccion(deteccionId);
                DataSet ds = Retrieve("Enfermeria_ObtenerProblemasDeteccionSinActivo", parametros);
                List<ProblemaInfo> probelmas = null;

                if (ValidateDataSet(ds))
                {
                    probelmas = MapProblemaDAL.ObtenerProblemasPorDeteccion(ds, tratamiento);
                }
                return probelmas;
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

        public List<ProblemaInfo> ObtenerProblemasDeteccionUltimaDeteccion(AnimalDeteccionInfo deteccionId, TratamientoInfo tratamiento)
        {
            try
            {
                Logger.Info();
                //Dictionary<string, object> parametros = AuxProblemaDAL.ObtenerParametrosObtenerProblemasDeteccion(deteccionId);
                var parametros =
                    new Dictionary<string, object>
                    {
                        {"@DetecionID", deteccionId.DeteccionID}
                    };
                DataSet ds = Retrieve("Enfermeria_ObtenerProblemasDeteccionUltimaDeteccion", parametros);
                List<ProblemaInfo> probelmas = null;

                if (ValidateDataSet(ds))
                {
                    probelmas = MapProblemaDAL.ObtenerProblemasPorDeteccion(ds, tratamiento);
                }
                return probelmas;
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
