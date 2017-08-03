using System;
using System.Collections.Generic;
using SIE.Base.Integracion.DAL;
using SIE.Services.Info.Info;
using SIE.Base.Infos;
using System.Data;
using System.Data.SqlClient;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Excepciones;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class DeteccionDAL: DALBase
    {
        internal int Guardar(DeteccionInfo deteccionGrabar)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxDeteccionDAL.ObtenerParametrosGrabar(deteccionGrabar);
                int result = Create("DeteccionGanado_GrabarDeteccion", parameters);
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
        /// Actualiza el numero de arete de una deteccion con foto a la entrada a enfermeria
        /// </summary>
        /// <param name="animalDetectado"></param>
        /// <returns></returns>
        public bool ActualizarAreteDeteccionConFoto(AnimalDeteccionInfo animalDetectado)
        {
            bool retValue = false;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxDeteccionDAL.ObtenerParametrosActualizadDeteccionFoto(animalDetectado);
                Update("DeteccionGanado_ActualizarDeteccionConFoto", parameters);
                retValue = true;
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
        /// Metodo para crear ubna deteccion generica
        /// </summary>
        /// <param name="deteccionId"></param>
        /// <returns></returns>
        internal int GuardarDeteccionGenerica(int deteccionId)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object>{{"@DeteccionID", deteccionId}};
                var result = RetrieveValue<int>("[dbo].[DeteccionGanado_GrabarDeteccionGenerica]", parametros);
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
