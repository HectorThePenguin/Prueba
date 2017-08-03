using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Auxiliar;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class AnimalDAL : DALBase
    {
        /// <summary>
        /// Metrodo Para Guardar en en la tabla Animal
        /// </summary>
        internal AnimalInfo GuardarAnimal(AnimalInfo animalInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosCrearAnimal(animalInfo);
                var ds = Retrieve("CorteGanado_GuardarAnimal", parameters);
                AnimalInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalDAL.ObtenerAnimal(ds);
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
        /// Metrodo Para obtener Peso
        /// </summary>
        internal TrampaInfo ObtenerPeso(int organizacionID, int folioEntrada)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosObtenerPeso(organizacionID, folioEntrada);
                var ds = Retrieve("CorteGanado_PesoCompraDirectaAnimal", parameters);
                //TrampaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    // result = MapAnimalDAL.ObtenerObtenerTrampa(ds);
                }
                return null;
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
        /// Metrodo Para obtener Peso
        /// </summary>
        internal List<AnimalMovimientoInfo> ObtenerFechasUltimoMovimiento(int organizacionID, List<int> listaLotes)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosFechasUltimoMovimiento(organizacionID, listaLotes);
                var ds = Retrieve("AnimalMovimiento_ObtenerFechaUltimoMovimiento", parameters);
                List<AnimalMovimientoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalDAL.ObtenerAnimalMovimientoFechaUltimoMovimiento(ds);
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
        /// Metrodo Para obtener Peso
        /// </summary>
        internal List<AnimalInfo> ObtenerAnimalesPorLote(int organizacionID, int loteID)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosAnimalesPorLote(organizacionID, loteID);
                var ds = Retrieve("AnimalMovimiento_ObtenerAnimalesPorLote", parameters);
                List<AnimalInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalDAL.ObtenerAnimalesPorLote(ds);
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
        /// Obtiene los Animales por Lotes Disponibles
        /// </summary>
        /// <param name="lotesDisponibles"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal List<AnimalInfo> ObtenerAnimalesPorLoteDisponibilidad(List<DisponibilidadLoteInfo> lotesDisponibles, int organizacionId)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosAnimalesPorLoteDisponibilidad(lotesDisponibles, organizacionId);
                var ds = Retrieve("Animal_ObtenerPorDisponibilidad", parameters);
                List<AnimalInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalDAL.ObtenerAnimalesPorLoteDisponibilidad(ds);
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
        /// Obtiene los Animales por Codigo de Corral
        /// </summary>
        /// <returns></returns>
        internal List<AnimalInfo> ObtenerAnimalesPorCodigoCorral(CorralInfo corralInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxCorralDAL.ObtenerParametrosObtenerAretesCorral(corralInfo);
                var ds = Retrieve("TraspasoGanado_ObtenerAretesCorral", parameters);
                List<AnimalInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalDAL.ObtenerAnimalesPorCodigoCorral(ds);
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
        /// Se almacena el Costo generado por el animal
        /// </summary>
        /// <param name="animalCosto"></param>
        /// <returns></returns>
        internal int GuardarAnimalCosto(AnimalCostoInfo animalCosto)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosGuardarAnimalCosto(animalCosto);
                Create("CorteGanado_GuardarAnimalCosto", parameters);
                //var result = 1;

                return 1;
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
        /// Se almacena el Costo generado por el animal
        /// </summary>
        /// <param name="listaAnimalCosto"></param>
        /// <returns></returns>
        internal int GuardarAnimalCostoXML(List<AnimalCostoInfo> listaAnimalCosto)
        {
            int tiempos = 0; //Variable de Control, para que el objeto BulkCopy, notifica el proceso
            try
            {

                Logger.Info();
                DataTable dtTable = CustomLINQtoDataSetMethods.CopyToDataTable(listaAnimalCosto);

                if (dtTable == null || dtTable.Rows.Count == 0)
                {
                    return 0;
                }

                var connection = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (var destinationConnection =
                      new SqlConnection(connection))
                {
                    destinationConnection.Open();
                    using (var bulkCopy =
                          new SqlBulkCopy(destinationConnection, SqlBulkCopyOptions.UseInternalTransaction, null))
                    {
                        bulkCopy.DestinationTableName = "AnimalCosto";

                        var mapAnimalId = new SqlBulkCopyColumnMapping("AnimalID", "AnimalID");
                        bulkCopy.ColumnMappings.Add(mapAnimalId);
                        var mapFechaCosto = new SqlBulkCopyColumnMapping("FechaCosto", "FechaCosto");
                        bulkCopy.ColumnMappings.Add(mapFechaCosto);
                        var mapCostoID = new SqlBulkCopyColumnMapping("CostoID", "CostoID");
                        bulkCopy.ColumnMappings.Add(mapCostoID);
                        var mapTipoReferencia = new SqlBulkCopyColumnMapping("TipoReferencia", "TipoReferencia");
                        bulkCopy.ColumnMappings.Add(mapTipoReferencia);
                        var mapFolioReferencia = new SqlBulkCopyColumnMapping("FolioReferencia", "FolioReferencia");
                        bulkCopy.ColumnMappings.Add(mapFolioReferencia);
                        var mapImporte = new SqlBulkCopyColumnMapping("Importe", "Importe");
                        bulkCopy.ColumnMappings.Add(mapImporte);
                        var mapUsuarioCreacion = new SqlBulkCopyColumnMapping("UsuarioCreacionID", "UsuarioCreacionID");
                        bulkCopy.ColumnMappings.Add(mapUsuarioCreacion);

                        bulkCopy.BulkCopyTimeout = 600;
                        bulkCopy.NotifyAfter = 5000;
                        bulkCopy.BatchSize = 5000;
                        //Funcion para que el BulkCopy notifique que esta trabajando y no marque error
                        bulkCopy.SqlRowsCopied += (sender, args) =>
                        {
                            tiempos++;
                        };
                        bulkCopy.WriteToServer(dtTable);
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
            return tiempos;
        }

        /// <summary>
        /// Obtiene los Animales por Codigo de Corral
        /// </summary>
        /// <returns></returns>
        internal List<AnimalInfo> ObtenerAnimalesPorCorral(CorralInfo corralInfo, int organizacionId)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosAnimalesPorCodigoCorral(corralInfo.Codigo, organizacionId);
                var ds = Retrieve("Animal_ObtenerAnimalesPorCorral", parameters);
                List<AnimalInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalDAL.ObtenerListadoAnimales(ds);
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
        /// Metrodo Para Guardar en en la tabla Animal
        /// </summary>
        internal AnimalMovimientoInfo ObtenerUltimoMovimientoAnimal(AnimalInfo animalInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosObtenerUltimoMovimientoAnimal(animalInfo);
                var ds = Retrieve("AnimalMovimiento_ObtenerUltimoMovimientoAnimal", parameters);
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

        /// <summary>
        /// Se busca animal en el inventario por arete y organizacion
        /// </summary>
        /// <param name="arete"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal AnimalInfo ObtenerAnimalPorArete(string arete, int organizacionId)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosObtenerAnimalPorArete(arete, organizacionId);
                var ds = Retrieve("SalidaIndividual_ObtenerAnimal", parameters);
                AnimalInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalMovimientosDAL.ObtenerAnimalPorArete(ds);
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
        /// Se busca animal en el inventario por arete y organizacion
        /// </summary>
        /// <param name="arete"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal List<AnimalInfo> ObtenerAnimalPorAretes(AnimalInfo arete, int organizacionId)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosObtenerAnimalPorAretes(arete, organizacionId);
                var ds = Retrieve("Animal_ObtenerAnimalPorAretes", parameters);
                List<AnimalInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalDAL.ObtenerAnimalPorAretes(ds);
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

        internal int ObtenerPesoProyectado(string arete, int organizacionId)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosObtenerPesoProyectado(arete, organizacionId);
                var ds = Retrieve("SalidaIndividual_ObtenerPeso", parameters);
                int result = 0;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalMovimientosDAL.ObtenerPesoProyectado(ds);
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
        /// Obtiene la existencia del corral
        /// </summary>
        /// <param name="animalID"></param>
        /// <returns></returns>
        internal int ObtenerExisteSalida(long animalID)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosObtenerExisteSalida(animalID);
                var ds = Retrieve("SalidaIndividual_ConsultaAnimalSalida", parameters);
                int result = 0;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalMovimientosDAL.ObtenerExisteSalida(ds);
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
        /// Guarda aninal en animal salida
        /// </summary>
        /// <param name="corralInfo"></param>
        /// <param name="animalMovimientoInfo"></param>
        /// <returns></returns>
        internal int GuardarCorralAnimalSalida(CorralInfo corralInfo, AnimalMovimientoInfo animalMovimientoInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosGuardarCorralAnimalSalida(corralInfo, animalMovimientoInfo);
                int result = Create("SalidaRecuperacionCorral_GuardarAnimalSalida", parameters);

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
        /// Obtiene animal salida 
        /// </summary>
        /// <param name="loteInfo"></param>
        /// <returns></returns>
        internal List<AnimalSalidaInfo> ObtenerAnimalSalidaAnimalID(LoteInfo loteInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosAnimalSalidaAnimalID(loteInfo);
                var ds = Retrieve("salidaRecuperacion_ObtenerAnimalPorLote", parameters);
                List<AnimalSalidaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalDAL.ObtenerListadoAnimalesSalida(ds);
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

        internal string ObtenerExisteVentaDetalle(string arete, string areteMetalico)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosObtenerExisteVentaDetalle(arete, areteMetalico);
                var ds = Retrieve("SalidaIndividualVenta_ConsultaAreteVentaDetalle", parameters);
                string result = "";
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalMovimientosDAL.ObtenerExisteVentaDetalle(ds);
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

        internal string obtenerExisteDeteccion(string arete)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosObtenerExisteDeteccion(arete);
                var ds = Retrieve("DeteccionGanado_ConsultaAreteDeteccion", parameters);
                string result = "";
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalMovimientosDAL.ObtenerExisteVentaDetalle(ds);
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

        internal string obtenerExisteMuerte(string arete)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosObtenerExisteDeteccion(arete);
                var ds = Retrieve("DeteccionGanado_ConsultaAreteMuerte", parameters);
                string result = "";
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalMovimientosDAL.ObtenerExisteVentaDetalle(ds);
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

        internal AnimalSalidaInfo ObtenerAnimalSalidaAnimalID(long animalID)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosAnimalSalidaAnimalID(animalID);
                var ds = Retrieve("CorteTransferenciaGanado_ObtenerAnimalSalida", parameters);
                AnimalSalidaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalDAL.ObtenerAnimalSalida(ds);
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
        /// Obtiene los Animales por Codigo de Corral
        /// </summary>
        /// <returns></returns>
        internal List<AnimalInfo> ObtenerAnimalesPorLoteID(LoteInfo loteInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosAnimalesPorLoteID(loteInfo);
                var ds = Retrieve("Animal_ObtenerAnimalesPorLoteID", parameters);
                List<AnimalInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalDAL.ObtenerAnimalesPorLoteID(ds);
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

        internal string ObtenerExisteAreteSalida(int salida, string arete)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosObtenerExisteAreteSalida(salida, arete);
                var ds = Retrieve("DeteccionGanado_ConsultaAreteRecepcionSalida", parameters);
                string result = "";
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalDAL.ObtenerExisteAreteSalida(ds);
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

        internal string obtenerExisteDeteccionTestigo(string areteTestigo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosObtenerExisteDeteccionTestigo(areteTestigo);
                var ds = Retrieve("DeteccionGanado_ConsultaAreteDeteccion", parameters);
                string result = "";
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalMovimientosDAL.ObtenerExisteVentaDetalle(ds);
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

        internal string obtenerExisteMuerteTestigo(string areteTestigo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosObtenerExisteDeteccionTestigo(areteTestigo);
                var ds = Retrieve("DeteccionGanado_ConsultaAreteMuerte", parameters);
                string result = "";
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalMovimientosDAL.ObtenerExisteVentaDetalle(ds);
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

        internal AnimalInfo ObtenerAnimalPorAreteTestigo(string areteTestigo, int organizacionID)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosObtenerAnimalPorAreteTestigo(areteTestigo, organizacionID);
                var ds = Retrieve("SalidaIndividual_ObtenerAnimalTestigo", parameters);
                AnimalInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalMovimientosDAL.ObtenerAnimalPorArete(ds);
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
        /// Se guarda lista de salida de recuperacion por corral
        /// </summary>
        /// <param name="animalSalida"></param>
        /// <returns></returns>
        internal int GuardarCorralAnimalSalidaLista(List<AnimalSalidaInfo> animalSalida)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosGuardarCorralAnimalSalidaLista(animalSalida);
                int result = Create("SalidaRecuperacionCorral_GuardarAnimalSalidaLista", parameters);

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


        internal int ObtenerLoteSalidaAnimal(string arete, string areteTestigo, int organizacionID)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosObtenerLoteSalidaAnimal(arete, areteTestigo, organizacionID);
                var ds = Retrieve("DeteccionGanado_ConsultaLoteAnimalSalida", parameters);
                int resultado = 0;
                if (ValidateDataSet(ds))
                {
                    resultado = MapAnimalMovimientosDAL.ObtenerLoteSalidaAnimal(ds);
                }
                return resultado;
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
        /// Obtener los animales del inventario para cada folioEntrada
        /// </summary>
        /// <param name="folioEntrada"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        public IList<AnimalInfo> ObtenerInventarioAnimalesPorFolioEntrada(string folioEntrada, int organizacionID)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosAnimalesPorFolioEntrada(folioEntrada, organizacionID);
                var ds = Retrieve("Animal_ObtenerInventarioAnimalesPorFolioEntrada", parameters);
                List<AnimalInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalDAL.ObtenerAnimalesPorFolioEntrada(ds);
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
        /// Metodo para enviar el animal a AnimalHistorico
        /// </summary>
        /// <param name="animalInactivo"></param>
        public void EnviarAnimalAHistorico(AnimalInfo animalInactivo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosAnimalID(animalInactivo);
                Create("Animal_EnviarAnimalAHistorico", parameters);
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
        /// Metodo para eliminar el Animal de ta labla Animal
        /// </summary>
        /// <param name="animalInactivo"></param>
        public void EliminarAnimal(AnimalInfo animalInactivo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosAnimalID(animalInactivo);
                Delete("Animal_EliminarAnimal", parameters);
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
        /// Consulta un animal por AnimalID
        /// </summary>
        /// <param name="animalID"></param>
        /// <returns>AnimalInfo</returns>
        internal AnimalInfo ObtenerAnimalAnimalID(long animalID)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosObtenerAnimalAnimalID(animalID);
                var ds = Retrieve("Animal_ConsultaPorAnimalID", parameters);
                AnimalInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalMovimientosDAL.ObtenerAnimalPorArete(ds);
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
        /// Inactivar animal del inventario
        /// </summary>
        /// <param name="animalInfo"></param>
        internal void InactivarAnimal(AnimalInfo animalInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosInactivarAnimal(animalInfo);
                Update("Animal_InactivarAnimal", parameters);
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

        internal void ActualizarArete(long animalId, string arete, int usuario)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosActualizarAnimal(animalId, arete, usuario);
                Update("Animal_ReemplazarArete", parameters);
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
        /// Eliminar el animal de animal Salida
        /// </summary>
        /// <param name="animalId"></param>
        /// <param name="loteId"></param>
        public void EliminarAnimalSalida(long animalId, int loteId)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object> { { "@AnimalID", animalId }, { "@LoteID", loteId } };
                Delete("AnimalSalida_Eliminar", parametros);
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
        /// Obtiene los animales que han tenido
        /// salida por muerte
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="animal"></param>
        /// <returns></returns>
        internal ResultadoInfo<AnimalInfo> ObtenerAnimalesMuertosPorPagina(PaginacionInfo pagina, AnimalInfo animal)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxAnimalDAL.ObtenerParametrosUltimoMovimientoPorPagina(pagina, animal);
                var ds = Retrieve("Animal_ObtenerUltimoMovimientoPaginado", parameters);
                ResultadoInfo<AnimalInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalDAL.ObtenerUltimoMovimientoPagina(ds);
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
        /// Obtiene el animal que este muerto
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        internal AnimalInfo ObtenerAnimalesMuertosPorAnimal(AnimalInfo animal)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxAnimalDAL.ObtenerParametrosAnimalesMuertos(animal);
                var ds = Retrieve("Animal_ObtenerUltimoMovimiento", parameters);
                AnimalInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalDAL.ObtenerAnimalesMuertos(ds);
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
        /// Metodo para reemplazar aretes cuando encuentras un animal en diferente corral
        /// y es de carga inicial
        /// </summary>
        /// <param name="animal"></param>
        /// <param name="deteccionGrabar"></param>
        internal void ReemplazarAretes(AnimalInfo animal, DeteccionInfo deteccionGrabar)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxAnimalDAL.ObtenerParametrosReemplazarAretes(animal, deteccionGrabar);
                Update("Animal_ReemplazarAretes", parameters);
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

        internal List<AnimalInfo> ObtenerAnimalesRecepcionPorCodigoCorral(CorralInfo corralInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxCorralDAL.ObtenerParametrosObtenerAretesCorral(corralInfo);
                var ds = Retrieve("TraspasoGanado_ObtenerAretesRecepcion", parameters);
                List<AnimalInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalDAL.ObtenerAnimalesRecepcionPorCodigoCorral(ds);
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
        /// Guarda un listado de la tabla animal consumo.
        /// </summary>
        /// <param name="listaAnimalConsumo"></param>
        internal int GuardarAnimalConsumoXml(List<AnimalConsumoInfo> listaAnimalConsumo)
        {
            int tiempos = 0; //Variable de Control, para que el objeto BulkCopy, notifica el proceso
            try
            {

                Logger.Info();
                DataTable dtTable = CustomLINQtoDataSetMethods.CopyToDataTable(listaAnimalConsumo);

                if (dtTable == null || dtTable.Rows.Count == 0)
                {
                    return 0;
                }

                var connection = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (var destinationConnection =
                      new SqlConnection(connection))
                {
                    
                    destinationConnection.Open();
                    //var trans = destinationConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
                    
                    using (var bulkCopy =
                          new SqlBulkCopy(destinationConnection, SqlBulkCopyOptions.Default, null))
                    {
                        bulkCopy.DestinationTableName = "AnimalConsumo";

                        var mapAnimalId = new SqlBulkCopyColumnMapping("AnimalId", "AnimalID");
                        bulkCopy.ColumnMappings.Add(mapAnimalId);
                        var mapRepartoID = new SqlBulkCopyColumnMapping("RepartoId", "RepartoID");
                        bulkCopy.ColumnMappings.Add(mapRepartoID);
                        var mapFormulaServidaID = new SqlBulkCopyColumnMapping("FormulaServidaId", "FormulaIDServida");
                        bulkCopy.ColumnMappings.Add(mapFormulaServidaID);
                        var mapCantidad = new SqlBulkCopyColumnMapping("Cantidad", "Cantidad");
                        bulkCopy.ColumnMappings.Add(mapCantidad);
                        var mapTipoServicio = new SqlBulkCopyColumnMapping("TipoServicio", "TipoServicioID");
                        bulkCopy.ColumnMappings.Add(mapTipoServicio);
                        var mapFecha = new SqlBulkCopyColumnMapping("Fecha", "Fecha");
                        bulkCopy.ColumnMappings.Add(mapFecha);
                        var mapActivo = new SqlBulkCopyColumnMapping("Activo", "Activo");
                        bulkCopy.ColumnMappings.Add(mapActivo);
                        var mapUsuarioCreacion = new SqlBulkCopyColumnMapping("UsuarioCreacionId", "UsuarioCreacionID");
                        bulkCopy.ColumnMappings.Add(mapUsuarioCreacion);
                        //var mapProcesado = new SqlBulkCopyColumnMapping("Procesado", "Procesado");
                        //bulkCopy.ColumnMappings.Add(mapProcesado);

                        bulkCopy.BulkCopyTimeout = 600;
                        bulkCopy.NotifyAfter = 5000;
                        bulkCopy.BatchSize = 5000;
                        //Funcion para que el BulkCopy notifique que esta trabajando y no marque error
                        bulkCopy.SqlRowsCopied += (sender, args) =>
                                                      {
                                                          tiempos++;
                                                      };
                        bulkCopy.WriteToServer(dtTable);
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
            return tiempos;
        }

        /// <summary>
        /// Obtiene los Animales por Lotes Disponibles
        /// </summary>
        /// <param name="lotesDisponibles"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal List<AnimalMovimientoInfo> ObtenerAnimalesPorLoteReimplante(List<DisponibilidadLoteInfo> lotesDisponibles, int organizacionId)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosAnimalesPorLoteReimplante(lotesDisponibles, organizacionId);
                var ds = Retrieve("Animal_ObtenerPorLoteReimplante", parameters);
                List<AnimalMovimientoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalDAL.ObtenerAnimalesPorLoteReimplante(ds);
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
        /// Metodo para actualizar el tipo de ganado de un animal
        /// </summary>
        /// <param name="animal"></param>
        public void ActializaTipoGanado(AnimalInfo animal)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object>
                {
                    { "@AnimalID", animal.AnimalID }, 
                    { "@TipoGanadoID", animal.TipoGanado.TipoGanadoID },
                    { "@UsuarioModificacionID", animal.UsuarioModificacionID }
                };
                Update("Animal_ActializaTipoGanado", parametros);
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
        /// Metodo para actualizar los aretes en el animal
        /// </summary>
        /// <param name="animal"></param>
        public void ActializaAretesEnAnimal(AnimalInfo animal)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object>
                {
                    { "@Arete", animal.Arete },
                    { "@AreteTestigo", animal.AreteMetalico },
                    { "@AnimalID", animal.AnimalID }, 
                    { "@UsuarioModificacionID", animal.UsuarioModificacionID }
                };

                Update("Animal_ActializaAretesEnAnimal", parametros);
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
        /// Obtiene la cantidad de dias que tiene el animal en enfermeria despues de la ultima deteccion
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        internal int ObtenerDiasUltimaDeteccion(AnimalInfo animal)
        {
            int dias = 0;

            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosObtenerDiasUltimaDeteccion(animal);
                var ds = Retrieve("Animal_ObtenerDiasUltimaDeteccion", parameters);
                if (ValidateDataSet(ds))
                {
                    dias = MapAnimalDAL.ObtenerDiasUltimaDeteccion(ds);
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

            return dias;
        }

        /// <summary>
        /// Se remplaza el arete por un arete del mismo corral
        /// </summary>
        /// <param name="animal"></param>
        internal void ReemplazarAreteMismoCorral(AnimalInfo animal)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxAnimalDAL.ReemplazarAreteMismoCorral(animal);
                Update("ReemplazarAreteMismoCorral", parameters);
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
        /// Se actualiza la clasificacion del animal
        /// </summary>
        /// <param name="animalInfo"></param>
        internal void ActualizaClasificacionGanado(AnimalInfo animalInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                   AuxAnimalDAL.ActualizaClasificacionGanado(animalInfo);
                Update("Animal_ActualizaClasificacionGanado", parameters);
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
        /// Metodo para actualziar el peso Compra de un animal
        /// </summary>
        /// <param name="animal"></param>
        internal void ActualizaPesoCompra(AnimalInfo animal)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object>
                                 {
                                     {"@AnimalID", animal.AnimalID},
                                     {"@PesoCompra", animal.PesoCompra},
                                     {"@UsuarioModificacionID", animal.UsuarioModificacionID}
                                 };
                Update("Animal_ActualizaPesoCompra", parametros);
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
        /// Metrodo Para Obtener los ultimos movimientos de la lista de animales
        /// </summary>
        internal List<AnimalMovimientoInfo> ObtenerUltimoMovimientoAnimalXML(List<AnimalInfo> animales, int organizacionID)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosObtenerUltimoMovimientoAnimalXML(animales, organizacionID);
                var ds = Retrieve("AnimalMovimiento_ObtenerUltimoMovimientoAnimalXML", parameters);
                List<AnimalMovimientoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalMovimientosDAL.ObtenerAnimalMovimientoXML(ds);
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
        /// Se almacena el Costo generado por el animal
        /// </summary>
        /// <param name="listaAnimalCosto"></param>
        /// <returns></returns>
        internal int GuardarAnimalCostoXMLManual(List<AnimalCostoInfo> listaAnimalCosto)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosGuardarAnimalCostoXML(listaAnimalCosto);
                Create("AnimalCosto_GuardarAnimalCostoXML", parameters);
                var result = 1;

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
        /// Guarda un listado de la tabla animal consumo.
        /// </summary>
        /// <param name="listaAnimalConsumo"></param>
        internal void GuardarAnimalConsumoXmlManual(List<AnimalConsumoInfo> listaAnimalConsumo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosGuardarAnimalConsumoXML(listaAnimalConsumo);
                Create("AnimalConsumo_GuardarAnimalConsumoXML", parameters);
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
        /// Obtener Animales por Lote
        /// </summary>
        /// <param name="lotes"></param>
        /// <returns></returns>
        internal List<AnimalInfo> ObtenerMovimientosPorLoteXML(List<LoteInfo> lotes)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxAnimalDAL.ObtenerParametrosPorLoteXML(lotes);
                using (IDataReader reader = RetrieveReader("AnimalMovimiento_ObtenerAnimalesPorLoteXML", parameters))
                {
                    List<AnimalInfo> result = null;
                    if (ValidateDataReader(reader))
                    {
                        result = MapAnimalDAL.ObtenerPorLoteXML(reader);
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

        internal List<AnimalInfo> ObtenerMovimientosSacrificioPorLoteXML(List<LoteInfo> lotes)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxAnimalDAL.ObtenerParametrosPorLoteXML(lotes);
                using (IDataReader reader = RetrieveReader("AnimalMovimientoHistorico_ObtenerAnimalesPorLoteXML", parameters))
                {
                    List<AnimalInfo> result = null;
                    if (ValidateDataReader(reader))
                    {
                        result = MapAnimalDAL.ObtenerPorLoteXML(reader);
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

        /// <summary>
        /// Obtiene los Animales por Lotes Disponibles
        /// </summary>
        /// <param name="lotes"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal List<AnimalInfo> ObtenerAnimalesPorLoteXML(List<LoteInfo> lotes, int organizacionId)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosAnimalesPorLoteXML(lotes, organizacionId);
                var ds = Retrieve("Animal_ObtenerPorLotesXML", parameters);
                List<AnimalInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalDAL.ObtenerAnimalesPorLoteXML(ds);
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
        /// Obtiene una lista de animales que han
        /// sido sacrificados para los lotes
        /// </summary>
        /// <param name="lotes"></param>
        /// <returns></returns>
        internal List<AnimalInfo> ObtenerMovimientosPorLoteSacrificadosXML(List<LoteInfo> lotes)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxAnimalDAL.ObtenerParametrosAnimalesPorLoteSacrificadosXML(lotes);
                DataSet ds = Retrieve("Animal_ObtenerEnviosSacrificioXML", parameters);
                List<AnimalInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalDAL.ObtenerAnimalesPorLoteSacrificadosXML(ds);
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
        /// Genera animales a partir de un XML
        /// </summary>
        /// <param name="animales"></param>
        internal void GuardarAnimal(List<AnimalInfo> animales)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxAnimalDAL.ObtenerParametrosGuardarAnimalXML(animales);
                Create("Animal_GuardarAnimalXML", parameters);
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
        /// Obtiene animales por arete y organizacion
        /// </summary>
        /// <param name="animales"></param>
        /// <returns></returns>
        internal List<AnimalInfo> ObtenerPorArete(List<AnimalInfo> animales)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxAnimalDAL.ObtenerParametrosAnimalesPorAreteOrganizacionXML(animales);
                DataSet ds = Retrieve("Animal_ObtenerPorAreteOrganizacionXML", parameters);
                List<AnimalInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalDAL.ObtenerAnimalesPorAreteOrganizacionXML(ds);
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
        /// Guarda un listado de la tabla animal consumo.
        /// </summary>
        /// <param name="listaAnimalConsumo"></param>
        internal int GuardarAnimalConsumoBulkCopy(List<AnimalConsumoInfo> listaAnimalConsumo)
        {
            int tiempos = 0; //Variable de Control, para que el objeto BulkCopy, notifica el proceso
            try
            {

                Logger.Info();
                DataTable dtTable = CustomLINQtoDataSetMethods.CopyToDataTable(listaAnimalConsumo);

                if (dtTable == null || dtTable.Rows.Count == 0)
                {
                    return 0;
                }

                var connection = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (var destinationConnection =
                      new SqlConnection(connection))
                {
                    destinationConnection.Open();
                    using (var bulkCopy =
                          new SqlBulkCopy(destinationConnection, SqlBulkCopyOptions.Default, null))
                    {
                        bulkCopy.DestinationTableName = "AnimalConsumoTemporal";

                        var mapAnimalId = new SqlBulkCopyColumnMapping("AnimalId", "AnimalID");
                        bulkCopy.ColumnMappings.Add(mapAnimalId);
                        var mapRepartoID = new SqlBulkCopyColumnMapping("RepartoId", "RepartoID");
                        bulkCopy.ColumnMappings.Add(mapRepartoID);
                        var mapFormulaServidaID = new SqlBulkCopyColumnMapping("FormulaServidaId", "FormulaIDServida");
                        bulkCopy.ColumnMappings.Add(mapFormulaServidaID);
                        var mapCantidad = new SqlBulkCopyColumnMapping("Cantidad", "Cantidad");
                        bulkCopy.ColumnMappings.Add(mapCantidad);
                        var mapTipoServicio = new SqlBulkCopyColumnMapping("TipoServicio", "TipoServicioID");
                        bulkCopy.ColumnMappings.Add(mapTipoServicio);
                        var mapFecha = new SqlBulkCopyColumnMapping("Fecha", "Fecha");
                        bulkCopy.ColumnMappings.Add(mapFecha);
                        var mapActivo = new SqlBulkCopyColumnMapping("Activo", "Activo");
                        bulkCopy.ColumnMappings.Add(mapActivo);
                        var mapUsuarioCreacion = new SqlBulkCopyColumnMapping("UsuarioCreacionId", "UsuarioCreacionID");
                        bulkCopy.ColumnMappings.Add(mapUsuarioCreacion);

                        bulkCopy.BulkCopyTimeout = 600;
                        bulkCopy.NotifyAfter = 5000;
                        bulkCopy.BatchSize = 5000;
                        //Funcion para que el BulkCopy notifique que esta trabajando y no marque error
                        bulkCopy.SqlRowsCopied += (sender, args) =>
                        {
                            tiempos++;
                        };
                        bulkCopy.WriteToServer(dtTable);
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
            return tiempos;
        }

        /// <summary>
        /// Se almacena el Costo generado por el animal
        /// </summary>
        /// <param name="listaAnimalCosto"></param>
        /// <returns></returns>
        internal int GuardarAnimalCostoBulkCopy(List<AnimalCostoInfo> listaAnimalCosto)
        {
            int tiempos = 0; //Variable de Control, para que el objeto BulkCopy, notifica el proceso
            try
            {

                Logger.Info();
                DataTable dtTable = CustomLINQtoDataSetMethods.CopyToDataTable(listaAnimalCosto);

                if (dtTable == null || dtTable.Rows.Count == 0)
                {
                    return 0;
                }

                var connection = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                using (var destinationConnection =
                      new SqlConnection(connection))
                {
                    destinationConnection.Open();
                    using (var bulkCopy =
                          new SqlBulkCopy(destinationConnection, SqlBulkCopyOptions.UseInternalTransaction, null))
                    {
                        bulkCopy.DestinationTableName = "AnimalCostoTemporal";

                        var mapAnimalId = new SqlBulkCopyColumnMapping("AnimalID", "AnimalID");
                        bulkCopy.ColumnMappings.Add(mapAnimalId);
                        var mapFechaCosto = new SqlBulkCopyColumnMapping("FechaCosto", "FechaCosto");
                        bulkCopy.ColumnMappings.Add(mapFechaCosto);
                        var mapCostoID = new SqlBulkCopyColumnMapping("CostoID", "CostoID");
                        bulkCopy.ColumnMappings.Add(mapCostoID);
                        var mapTipoReferencia = new SqlBulkCopyColumnMapping("TipoReferencia", "TipoReferencia");
                        bulkCopy.ColumnMappings.Add(mapTipoReferencia);
                        var mapFolioReferencia = new SqlBulkCopyColumnMapping("FolioReferencia", "FolioReferencia");
                        bulkCopy.ColumnMappings.Add(mapFolioReferencia);
                        var mapImporte = new SqlBulkCopyColumnMapping("Importe", "Importe");
                        bulkCopy.ColumnMappings.Add(mapImporte);
                        var mapUsuarioCreacion = new SqlBulkCopyColumnMapping("UsuarioCreacionID", "UsuarioCreacionID");
                        bulkCopy.ColumnMappings.Add(mapUsuarioCreacion);

                        bulkCopy.BulkCopyTimeout = 600;
                        bulkCopy.NotifyAfter = 5000;
                        bulkCopy.BatchSize = 5000;
                        //Funcion para que el BulkCopy notifique que esta trabajando y no marque error
                        bulkCopy.SqlRowsCopied += (sender, args) =>
                        {
                            tiempos++;
                        };
                        bulkCopy.WriteToServer(dtTable);
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
            return tiempos;
        }

        /// <summary>
        /// Mueve de la Tabla temporal AnimalConsumoTemporal a la tabla AnimalConsumo
        /// </summary>
        internal void EnviarAnimalConsumoDeTemporal()
        {
            try
            {
                Logger.Info();
                var parameters = new Dictionary<string, object>();
                Create("AnimalConsumo_MoverTablaTemporal", parameters);
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
        /// Mueve de la tabla AnimalCostoTemporal a la tabla AnimalCosto
        /// </summary>
        internal void EnviarAnimalCostoDeTemporal()
        {
            try
            {
                Logger.Info();
                var parameters = new Dictionary<string, object>();
                Create("AnimalCosto_MoverTablaTemporal", parameters);
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
        /// Obtener Animales por XML
        /// </summary>
        /// <param name="lotes"></param>
        /// <returns></returns>
        internal List<AnimalInfo> ObtenerMovimientosPorXML(List<AnimalInfo> animales)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAnimalDAL.ObtenerParametrosPorXML(animales);
                using (IDataReader reader = RetrieveReader("Animal_ObtenerAnimalesPorXML", parameters))
                {
                    List<AnimalInfo> result = null;
                    if (ValidateDataReader(reader))
                    {
                        result = MapAnimalDAL.ObtenerPorXML(reader);
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

        /// <summary>
        /// Metrodo que verifica si existe el nuevo arete, dejar en blanco el arete que no se requiera validar
        /// </summary>
        /// <param name="Arete">Arete capturado</param>
        /// <param name="AreteMetalico">Arete Metalico</param>
        /// <returns>Verdadero en caso de que se encuentre el arete registro</returns>
        internal Boolean VerificarExistenciaArete(string Arete, string AreteMetalico, int Organizacion)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosVerificarExisteArete(Arete, AreteMetalico, Organizacion);
                var ds = Retrieve("dbo.CorteGanado_ExisteArete", parameters);
                Boolean result = false;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalDAL.VerificarExistenciaArete(ds);
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

        //internal long PlancharAretes(List<AnimalInfo> animalesScp, long animalID, int loteID, int usuarioID)
        internal List<ControlSacrificioInfo.SincronizacionSIAP> PlancharAretes(ControlSacrificioInfo.Planchado_Arete_Request[] plancharArete, int usuarioID)
        {
            List<ControlSacrificioInfo.SincronizacionSIAP> result = new List<ControlSacrificioInfo.SincronizacionSIAP>();
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosPlancharAretes(plancharArete, usuarioID);
                var ds = Retrieve("Animal_PlancharArete", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalDAL.PlancharAretesObtenerAnimalId(ds);
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

        internal List<ControlSacrificioInfo.SincronizacionSIAP> PlancharAretes(ControlSacrificioInfo.Planchado_AreteLote_Request plancharArete, int usuarioID)
        {
            List<ControlSacrificioInfo.SincronizacionSIAP> result = new List<ControlSacrificioInfo.SincronizacionSIAP>();
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosPlancharAretes(plancharArete, usuarioID);
                var ds = Retrieve("Animal_PlancharAreteLote", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalDAL.PlancharAretesObtenerAnimalId(ds);
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
        /// Metrodo Para obtener los aretes del corral a detectar
        /// </summary>
        internal List<AnimalInfo> ObtenerAnimalesPorCorralDeteccion(int corralID, bool esPartida)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosAnimalesPorCorralDeteccion(corralID, esPartida);
                var ds = Retrieve("Animal_ObtenerAretesCorralDeteccion", parameters);
                List<AnimalInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalDAL.ObtenerAnimalesPorCorralDeteccion(ds);
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
        /// Metodo para obtener el Animal mas antiguo del corral
        /// </summary>
        internal AnimalInfo ObtenerAnimalAntiguoCorral(int corralID)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAnimalDAL.ObtenerParametrosAnimalAntiguoCorral(corralID);
                var ds = Retrieve("Animal_ObtenerAreteAntiguoCorral", parameters);
                AnimalInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAnimalDAL.ObtenerAnimalAntiguoCorral(ds);
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
