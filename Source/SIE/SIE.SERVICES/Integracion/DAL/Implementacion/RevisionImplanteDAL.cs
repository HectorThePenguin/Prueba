using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Data.SqlClient;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    /// <summary>
    /// The revision implante dal.
    /// </summary>
    internal class RevisionImplanteDAL : DALBase
    {
        /// <summary>
        /// The obtener por id.
        /// </summary>
        /// <param name="almacenID">
        /// The almacen id.
        /// </param>
        /// <returns>
        /// The <see cref="ResultadoInfo"/>.
        /// </returns>
        /// <exception cref="ExcepcionServicio">
        /// </exception>
        /// <exception cref="ExcepcionDesconocida">
        /// </exception>
        internal ResultadoInfo<AreaRevisionInfo> ObtenerLugaresValidacion()
        {
            ResultadoInfo<AreaRevisionInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRevisionImplanteDAL.ObtenerParametrosAreaRevision();
                using (DataSet reader = Retrieve("RevisionReimplante_ObtenerLugaresValidacion", parameters))
                {
                    if (ValidateDataSet(reader))
                    {
                        result = MapRevisionImplanteDAL.ObtenerLugaresValidacion(reader);
                    }
                    if (Connection.State == ConnectionState.Open)
                    {
                        Connection.Close();
                    }
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
        /// The obtener causas.
        /// </summary>
        /// <returns>
        /// The <see cref="ResultadoInfo"/>.
        /// </returns>
        /// <exception cref="ExcepcionServicio">
        /// </exception>
        /// <exception cref="ExcepcionDesconocida">
        /// </exception>
        internal ResultadoInfo<CausaRevisionImplanteInfo> ObtenerCausas()
        {
            ResultadoInfo<CausaRevisionImplanteInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRevisionImplanteDAL.ObtenerParametrosCausas();
                using (DataSet reader = Retrieve("RevisionReimplante_ObtenerCausas", parameters))
                {
                    if (ValidateDataSet(reader))
                    {
                        result = MapRevisionImplanteDAL.ObtenerCausas(reader);
                    }
                    if (Connection.State == ConnectionState.Open)
                    {
                        Connection.Close();
                    }
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
        /// The guardar revision.
        /// </summary>
        /// <param name="listaRevision">
        /// The lista revision.
        /// </param>
        /// <param name="usuario">
        /// The usuario.
        /// </param>
        /// <exception cref="ExcepcionServicio">
        /// </exception>
        /// <exception cref="ExcepcionDesconocida">
        /// </exception>
        internal void GuardarRevision(List<RevisionImplanteInfo> listaRevision, UsuarioInfo usuario)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxRevisionImplanteDAL.ObtenerGuardarRevisionImplante(listaRevision, usuario);
                Create("RevisionReimplante_GuardarRevision", parameters);
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
        /// Obtiene la revision Actual del un animal
        /// </summary>
        /// <param name="listaRevision">
        /// The lista revision.
        /// </param>
        /// <param name="usuario">
        /// The usuario.
        /// </param>
        /// <exception cref="ExcepcionServicio">
        /// </exception>
        /// <exception cref="ExcepcionDesconocida">
        /// </exception>
        internal RevisionImplanteInfo ObtenerRevisonActual(AnimalInfo animal)
        {
            RevisionImplanteInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRevisionImplanteDAL.ObtenerParametrosRevisionActual(animal);
                using (DataSet reader = Retrieve("RevisionImplante_ObtenerRevisionActual", parameters))
                {
                    if (ValidateDataSet(reader))
                    {
                        result = MapRevisionImplanteDAL.ObtenerRevisionAnimalActual(reader);
                    }
                    if (Connection.State == ConnectionState.Open)
                    {
                        Connection.Close();
                    }
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
