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
    internal class ReimplanteDAL : DALBase
    {
        /// <summary>
        /// Metrodo Para Guardar en en la tabla Animal
        /// </summary>
        internal AnimalInfo ReasignarAreteMetalico(AnimalInfo animalInfo, int banderaGuardar)
        {
            try
            {
                Logger.Info();
                var parameters = AuxReimplante.ObtenerParametrosReasignarAreteMetalico(animalInfo, banderaGuardar);
                var ds = Retrieve("ReimplanteGanado_ReasignarAreteMetalico", parameters);
                AnimalInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapReimplanteDAL.ObtenerDatosReimplante(ds); 
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

        internal DatosCompra ObtenerDatosCompra(AnimalInfo animalInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxReimplante.ObtenerParametrosDatosCompra(animalInfo.AnimalID,
                    animalInfo.OrganizacionIDEntrada);
                var ds = Retrieve("ReimplanteGanado_ObtenerDatosCompra", parameters);
                DatosCompra result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapReimplanteDAL.ObtenerDatosCompra(ds);
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
        /// Metodo Para Obtener lo el animal de reimplante
        /// </summary>
        internal ReimplanteInfo ObtenerAreteIndividual(AnimalInfo animalInfo, TipoMovimiento corte)
        {
            try
            {
                Logger.Info();
                var parameters = AuxReimplante.ObtenerParametrosObtenerAreteIndividual(animalInfo, corte);
                var ds = Retrieve("ReimplanteGanado_ObtenerArete", parameters);
                ReimplanteInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapReimplanteDAL.ObtenerAreteIndividual(ds);
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

        internal ReimplanteInfo ObtenerAreteMetalico(AnimalInfo animalInfo, TipoMovimiento corte)
        {
            try
            {
                Logger.Info();
                var parameters = AuxReimplante.ObtenerParametrosObtenerAreteMetalico(animalInfo, corte);
                var ds = Retrieve("ReimplanteGanado_ObtenerAreteMetalico", parameters);
                ReimplanteInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapReimplanteDAL.ObtenerAreteIndividual(ds);
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
        /// Metodo Para validar el corral destino
        /// </summary>
        internal int ValidarCorralDestino(string corralOrigen, string corralDestino, int idOrganizacion)
        {
            try
            {
                var result = 0;
                Logger.Info();
                var parameters = AuxReimplante.ObtenerParametrosValidarCorralDestino( corralOrigen, corralDestino, idOrganizacion);
                var ds = Retrieve("ReimplanteGanado_ValidarCorralDestino", parameters);
              
                if (ValidateDataSet(ds))
                {
                    result = MapReimplanteDAL.ObtenerExisteCorralDestino(ds);
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
        /// Funcion que permite obtener de la base de datos la Existencia de reimplantes configurados
        /// </summary>
        /// <returns></returns>
        internal bool ExisteProgramacionReimplate(int idOrganizacion)
        {
            var resp = false;
            try
            {
                Logger.Info();
                var parameters = AuxReimplante.ObtenerParametrosExisteProgramacionReimplante(idOrganizacion);
                DataSet ds = Retrieve("ReimplanteGanado_ExisteProgramacionReimplante", parameters);
                if (ValidateDataSet(ds))
                {
                    resp = MapReimplanteDAL.ObtenerExisteProgramacionReimplante(ds);
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
            return resp;
        }

        /// <summary>
        /// Funcion que permite obtener de la base de datos la Existencia de reimplantes configurados
        /// </summary>
        /// <returns></returns>
        internal bool ValidarReimplate(AnimalInfo animal)
        {
            var resp = false;
            try
            {
                Logger.Info();
                var parameters = AuxReimplante.ObtenerParametrosValidarReimplante(animal);
                DataSet ds = Retrieve("ReimplanteGanado_ValidaReimplante", parameters);
                if (ValidateDataSet(ds))
                {
                    resp = MapReimplanteDAL.ObtenerValidarReimplante(ds);
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
            return resp;
        }

        internal bool ValidarReimplatePorAreteMetalico(AnimalInfo animal)
        {
            var resp = false;
            try
            {
                Logger.Info();
                var parameters = AuxReimplante.ObtenerParametrosValidarReimplantePorAreteMetalico(animal);
                DataSet ds = Retrieve("ReimplanteGanado_ValidaReimplantePorAreteMetalico", parameters);
                if (ValidateDataSet(ds))
                {
                    resp = MapReimplanteDAL.ObtenerValidarReimplante(ds);
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
            return resp;
        }

        /// <summary>
        /// Obtiene de la base de datos el numero de cabezas en enfermeria para un lote
        /// </summary>
        /// <param name="ganadoEnfermeria"></param>
        /// <param name="tipoMovimiento"></param>
        /// <returns></returns>
        internal int ObtenerCabezasEnEnfermeria(EntradaGanadoInfo ganadoEnfermeria, int tipoMovimiento)
        {
            var resp = 0;
            try
            {
                Dictionary<string, object> parametros = AuxReimplante.ObtenerParametrosCabezasEnEnfermeria(ganadoEnfermeria, tipoMovimiento);
                DataSet ds = Retrieve("[dbo].[ReimplanteGanado_ObtenerTotalGanadoEnEnfermeria]", parametros);
                if (ValidateDataSet(ds))
                {
                    resp = MapReimplanteDAL.ObtenerCabezasEnEnfermeria(ds);
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
            return resp;
        }

        /// <summary>
        /// Obtiene el numero de cabezas reimplantadas para un lote
        /// </summary>
        /// <param name="cabezas"></param>
        /// <returns></returns>
        internal List<CabezasCortadas> ObtenerCabezasReimplantadas(CabezasCortadas cabezas)
        {
            List<CabezasCortadas> resp = null;
            try
            {
                Dictionary<string, object> parametros = AuxReimplante.ObtenerParametrosCabezasReimplantadas(cabezas);
                DataSet ds = Retrieve("[dbo].[ReimplanteGanado_ObtenerCabezasReimplantadas]", parametros);
                if (ValidateDataSet(ds))
                {
                    resp = MapReimplanteDAL.ObtenerCabezasReimplantadas(ds);
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
            return resp;
        }

        /// <summary>
        /// Obtiene de la base de datos el numero de cabezas muertas para un lote
        /// </summary>
        /// <param name="cabezas"></param>
        /// <returns></returns>
        internal int ObtenerCabezasMuertas(CabezasCortadas cabezas)
        {
            var resp = 0;
            try
            {
                Dictionary<string, object> parametros = AuxReimplante.ObtenerParametrosCabezasMuertas(cabezas);
                DataSet ds = Retrieve("[dbo].[ReimplanteGanado_ObtenerCabezasMuertas]", parametros);
                if (ValidateDataSet(ds))
                {
                    resp = MapReimplanteDAL.ObtenerCabezasMuertas(ds);
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
            return resp;
        }

        /// <summary>
        /// Obtener los corrales programados para reimplante q no han sido cerrados
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal List<ProgramacionReinplanteInfo> ObtenerCorralesParaAjuste(int organizacionId)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object> {{"@OrganizacionID", organizacionId}};
                var ds = Retrieve("ReimplanteGanado_ObtenerCorralesParaAjuste", parametros);
                List<ProgramacionReinplanteInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapReimplanteDAL.ObtenerObtenerCorralesParaAjuste(ds);
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

        internal ReimplanteInfo ObtenerAreteIndividualReimplantar(LoteInfo lote)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object>{{"@LoteID", lote.LoteID},{"@CorralID", lote.CorralID}};
                var ds = Retrieve("ReimplanteGanado_ObtenerAreteIndividualReimplantar", parametros);
                ReimplanteInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapReimplanteDAL.ObtenerAreteIndividual(ds);
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
        /// Metodo para obtener una lista de corrales que fueron reimpantados
        /// </summary>
        /// <param name="organizacion"></param>
        /// <returns></returns>
        internal List<LoteCorralReimplanteInfo> ObtenerCorralesReimplantados(OrganizacionInfo organizacion)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object> { { "@OrganizacionID", organizacion.OrganizacionID } };
                var ds = Retrieve("ReimplanteGanado_ObtenerCorralesReimplantados", parametros);
                List<LoteCorralReimplanteInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapReimplanteDAL.ObtenerCorralesReimplantados(ds);
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
        /// Obtener la fecha zilmax de un lote
        /// </summary>
        /// <param name="lote"></param>
        /// <param name="tipoGanadoInfo"></param>
        /// <param name="diasEngorda"></param>
        /// <returns></returns>
        public DateTime ObtenerFechaZilmax(LoteInfo lote, TipoGanadoInfo tipoGanadoInfo, int diasEngorda)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object>
                {
                    { "@OrganizacionID", lote.OrganizacionID},
                    { "@Sexo",  Convert.ToChar(tipoGanadoInfo.Sexo) },
                    { "@FechaInicio", lote.FechaInicio},
                    { "@DiasEngorda", diasEngorda}
                };
                 return RetrieveValue<DateTime>("ReimplanteGanado_ObtenerFechaZilmax", parametros);
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
        ///  Se valida corral destino si tiene punta chica
        /// </summary>
        /// <param name="corralOrigen"></param>
        /// <param name="corralDestino"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public CorralInfo ValidarCorralDestinoPuntaChica(string corralOrigen, string corralDestino, int organizacionId)
        {
            try
            {
                CorralInfo result = null;
                Logger.Info();
                var parameters = AuxReimplante.ObtenerParametrosValidarCorralDestino(corralOrigen, corralDestino, organizacionId);
                var ds = Retrieve("ReimplanteGanado_ValidarCorralDestinoPuntaChica", parameters);

                if (ValidateDataSet(ds))
                {
                    result = MapReimplanteDAL.ObtenerExisteCorralDestinoPuntaChica(ds);
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
