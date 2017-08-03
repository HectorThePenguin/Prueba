using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class AnimalMovimientoDAL : DALBase
    {
        /// <summary>
        /// Se envia el animal de AnimalMovimiento a AnimalMovimientoHistorico
        /// </summary>
        /// <param name="animalInactivo"></param>
        public void EnviarAnimalMovimientoAHistorico(AnimalInfo animalInactivo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalMovimientoDAL.ObtenerParametrosAnimalID(animalInactivo);
                Create("AnimalMovimiento_EnviarAnimalMovimientoAHistorico", parameters);
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
        /// Se elimina el Animal Movimiento
        /// </summary>
        /// <param name="animalInactivo"></param>
        public void EliminarAnimalMovimiento(AnimalInfo animalInactivo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalCostoDAL.ObtenerParametrosAnimalCostoID(animalInactivo);
                Delete("AnimalMovimiento_EliminarAnimalMovimiento", parameters);
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
        /// Metrodo Para Guardar en en la tabla AnimalMovimiento
        /// </summary>
        internal AnimalMovimientoInfo GuardarAnimalMovimiento(AnimalMovimientoInfo animalMovimientoInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalMovimientoDAL.ObtenerParametrosCrearAnimalMovimiento(animalMovimientoInfo);
                var ds = Retrieve("AnimalMovimiento_Guardar", parameters);
                AnimalMovimientoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalMovimientosDAL.ObtenerAnimalMovimiento(ds);
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

        internal List<AnimalMovimientoInfo> ObtenerUltimoMovimientoAnimal(List<AnimalInfo> animales)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxAnimalMovimientoDAL.ObtenerParametrosUltimoMovimientoAnimal(animales);
                using (IDataReader reader = RetrieveReader("AnimalMovimiento_ObtenerUltimoMovimientoXML", parameters))
                {
                    List<AnimalMovimientoInfo> result = null;
                    if (ValidateDataReader(reader))
                    {
                        result = MapAnimalMovimientosDAL.ObtenerUltimoMovimientoAnimal(reader);
                    }
                    if (Connection.State == ConnectionState.Open)
                    {
                        Connection.Close();
                    }
                    return result;
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

        internal List<AnimalMovimientoInfo> ObtenerAnimalesNoReimplantados(int loteId, int OrganizacionID)
        {
            try
            {
                Logger.Info();
                var parameters = new Dictionary<string, object>
                {
                    { "@OrganizacionID", OrganizacionID },
                    { "@LoteID", loteId }
                };
                var ds = Retrieve("AnimalMovimiento_ObtenerAnimalesNoReimplantados", parameters);
                List<AnimalMovimientoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalMovimientosDAL.ObtenerAnimalesNoReimplantados(ds);
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
        /// Validar si el animal es carga inicial
        /// </summary>
        /// <param name="animalId"></param>
        /// <returns></returns>
        internal bool ObtenerEsCargaInicialAnimal(long animalId)
        {
            try
            {
                Logger.Info();
                var parameters = new Dictionary<string, object> {{ "@AnimalID", animalId }};
                var cargaInicial = RetrieveValue<int>("AnimalMovimiento_ObtenerEsCargaInicialAnimal", parameters);
                bool result = cargaInicial>0;
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

        internal List<AnimalMovimientoInfo> ObtenerMovimientosPorArete(int organizacionID, string arete)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxAnimalMovimientoDAL.ObtenerParametrosMovimientosPorArete(organizacionID, arete);
                DataSet ds = Retrieve("[dbo].[AnimalMovimiento_ObtenerMovimientosPorArete]", parameters);
                List<AnimalMovimientoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalMovimientosDAL.ObtenerMovimientosPorArete(ds);
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
        /// Obtiene una lista con los animales muertos a conciliar
        /// </summary>
        /// <param name="organizacionID"> </param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        internal IEnumerable<AnimalMovimientoInfo> ObtenerAnimalesMuertos(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                IMapBuilderContext<AnimalMovimientoInfo> mapeo =
                    MapAnimalMovimientosDAL.ObtenerAnimalMovimientoMuertos();
                IEnumerable<AnimalMovimientoInfo> almacenAnimalMovimientosMuertes = GetDatabase().
                    ExecuteSprocAccessor
                    <AnimalMovimientoInfo>(
                        "MuerteGanado_ObtenerMuertesPolizaConciliacion", mapeo.Build(),
                        new object[] { organizacionID, fechaInicial, fechaFinal });
                return almacenAnimalMovimientosMuertes;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metrodo Para Guardar en en la tabla AnimalMovimiento
        /// </summary>
        internal void GuardarAnimalMovimientoXML(List<AnimalMovimientoInfo> listaAnimalesMovimiento)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalMovimientoDAL.ObtenerParametrosCrearAnimalMovimientoXML(listaAnimalesMovimiento);
                Create("AnimalMovimiento_GuardarXML", parameters);
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
        /// Obtiene los animales reimplantados por XML
        /// </summary>
        /// <returns></returns>
        internal List<AnimalMovimientoInfo> ObtenerAnimalesNoReimplantadosXML(int organizacionID, List<LoteInfo> lotes)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = 
                    AuxAnimalMovimientoDAL.ObtenerParametrosAnimalesNoReimplantadosXML(organizacionID, lotes);
                var ds = Retrieve("AnimalMovimiento_ObtenerAnimalesNoReimplantadosXML", parameters);
                List<AnimalMovimientoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalMovimientosDAL.ObtenerAnimalesNoReimplantadosXML(ds);
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
        /// Metodo para mapear y obtener la trazabilidad de animalMovimiento
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        internal AnimalInfo ObtenerTrazabilidadAnimalMovimiento(AnimalInfo animal)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxAnimalMovimientoDAL.ObtenerTrazabilidadAnimalMovimiento(animal);
                var ds = Retrieve("AnimalMovimiento_ObtenerTrazabilidadAnimalMovimiento", parameters);
                AnimalInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalMovimientosDAL.ObtenerTrazabilidadAnimalMovimiento(ds, animal);
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
