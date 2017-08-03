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
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class EnfermeriaDAL : DALBase
    {
        /// <summary>
        /// Obtiene los animales enfermos de un corral determinado
        /// </summary>
        /// <param name="organizacionID">Organizacion a la que pertenecen los corrales</param>
        /// <param name="corralID">Corral que se quiere consultar</param>
        /// <returns>Lista de animales enfemos dentro del corral</returns>
        internal IList<AnimalDeteccionInfo> ObtenerAnimalesDetectadosEnfermosPorCorral(int organizacionID, int corralID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEnfermeriaDAL.ObtenerParametrosObtenerAnimalesEnfermeriaPorCorral(organizacionID, corralID);
                DataSet ds = Retrieve("Enfermeria_ObtenerGanadoEnfermoPorCorral", parametros);
                IList<AnimalDeteccionInfo> animalesEnfermeria = null;

                if (ValidateDataSet(ds))
                {
                    animalesEnfermeria = MapEnfermeriaDal.ObtenerAnimalesEnfermeriaPorCorral(ds,organizacionID);
                }
                return animalesEnfermeria;
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
        /// Obtiene los corrales en los cuales se detectaron animales enfermor
        /// </summary>
        /// <param name="organizacionId">Organizacion</param>
        /// <param name="pagina">Configuracion de paginacion</param>
        /// <returns>Lista de corrales en los que se detectaron animales enfermos</returns>
        internal ResultadoInfo<EnfermeriaInfo> ObtenerCorralesConGanadoDetectadoEnfermo(int organizacionId, PaginacionInfo pagina)
        {
            try
            {
                Logger.Info();
                var parametros = AuxEnfermeriaDAL.ObtenerParametrosCorralesConEnfermosPorPaginas(organizacionId, pagina);
                var ds = Retrieve("Enfermeria_ObtenerCorralesConEnfermosPorPagina", parametros);
                ResultadoInfo<EnfermeriaInfo> corralesConEnfermos = null;
                if (ValidateDataSet(ds))
                {
                    corralesConEnfermos = MapEnfermeriaDal.ObtenerCorralesConGanadoDetectadoEnfermo(ds,organizacionId);

                }
                return corralesConEnfermos;
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
        /// Obtiene las enfermerias de cada operador.
        /// </summary>
        /// <param name="operadorID">Organizacion</param>
        /// <returns>Lista de las enfermerias que le corresponden al operador</returns>
        internal List<EnfermeriaInfo> ObtenerEnfermeriasPorOperadorID(int operadorID)
        {
            List<EnfermeriaInfo> lista = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEnfermeriaDAL.ObtenerParametrosEnfermeriasPorOperadorID(operadorID);
                DataSet ds = Retrieve("Enfermeria_ObtenerPorOperadorID", parametros);

                if (ValidateDataSet(ds))
                {
                    lista = MapEnfermeriaDal.ObtenerEnfermeriasPorOperadorID(ds);
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
        /// Obtiene los animales en deteccion por arete
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        internal AnimalDeteccionInfo ObtenerAnimalDetectadoPorArete(AnimalInfo animal)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEnfermeriaDAL.ObtenerParametrosObtenerAnimalDetectadoPorArete(animal);
                DataSet ds = Retrieve("Enfermeria_ObtenerGanadoEnfermoPorArete", parametros);
                AnimalDeteccionInfo animalDetectado = null;

                if (ValidateDataSet(ds))
                {
                    animalDetectado = MapEnfermeriaDal.ObtenerAnimalesEnfermeriaPorCorral(ds,animal.OrganizacionIDEntrada)[0];
                }
                return animalDetectado;
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
        /// Se obtiene el ultimo movimiento por tipo
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        internal AnimalMovimientoInfo ObtenerUltimoMovimientoEnfermeria(AnimalInfo animal)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEnfermeriaDAL.ObtenerParametrosUltimoMovimientoEnfermeria(animal);
                DataSet ds = Retrieve("Movimientos_ObtenerUltimoMovimientoPorTipo", parametros);
                AnimalMovimientoInfo animalMovimiento = null;

                if (ValidateDataSet(ds))
                {
                    animalMovimiento = MapAnimalMovimientosDAL.ObtenerAnimalMovimiento(ds);
                }
                return animalMovimiento;
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
        /// Obtiene una lista de grados disponibles
        /// </summary>
        /// <returns></returns>
        internal IList<GradoInfo> ObtenerGrados()
        {
            try
            {
                Logger.Info();

                DataSet ds = Retrieve("Grados_Obtener");
                IList<GradoInfo> grados = null;

                if (ValidateDataSet(ds))
                {
                    grados = MapEnfermeriaDal.ObtenerGrados(ds);
                }
                return grados;
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
        /// Obtener el utimo movimiento de recuperacion
        /// </summary>
        /// <param name="animalInfo"></param>
        /// <returns></returns>
        internal AnimalMovimientoInfo ObtenerUltimoMovimientoRecuperacion(AnimalInfo animalInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    AuxEnfermeriaDAL.ObtenerParametrosUltimoMovimientoRecuperacion(animalInfo);
                DataSet ds = Retrieve("AnimalMovimiento_ObtenerUltimoMovimientoAnimal", parametros);
                AnimalMovimientoInfo animalMovimiento = null;

                if (ValidateDataSet(ds))
                {
                    animalMovimiento = MapAnimalMovimientosDAL.ObtenerAnimalMovimiento(ds);
                }
                return animalMovimiento;
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
        /// Obtiene el historial clinico
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        internal IList<HistorialClinicoInfo> ObtenerHistorialClinico(AnimalInfo animal)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxEnfermeriaDAL.ObtenerHistorialClinico(animal);
                DataSet ds = Retrieve("Enfermeria_ObtenerHistorialClinico", parametros);
                IList<HistorialClinicoInfo> historialClinico = null;
                var tratamiento = new TratamientoInfo
                {
                    OrganizacionId = animal.OrganizacionIDEntrada,
                    Sexo = animal.TipoGanado.Sexo
                };


                if (ValidateDataSet(ds))
                {
                    historialClinico =
                        MapEnfermeriaDal.ObtenerHistorialClinico(ds, tratamiento);
                }
                return historialClinico;
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
        /// Elimina una deteccion
        /// </summary>
        /// <param name="deteccion"></param>
        /// <returns></returns>
        internal int EliminarDeteccion(DeteccionInfo deteccion)
        {

            try
            {
                Logger.Info();
                var parametros = AuxEnfermeriaDAL.ObtenerParametrosEliminarDeteccion(deteccion);
                return Create("Deteccion_EliminarDeteccion", parametros);
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
        /// Guarda una deteccion
        /// </summary>
        /// <param name="deteccion"></param>
        /// <param name="problemasDetectados"></param>
        /// <returns></returns>
        internal int GurdarDeteccion(AnimalDeteccionInfo deteccion, IList<ProblemaInfo> problemasDetectados )
        {

            try
            {
                Logger.Info();
                var parametros = AuxEnfermeriaDAL.ObtenerParametrosGuardarDeteccion(deteccion, problemasDetectados);
                return Create("DeteccionAnalista_GuardarDeteccionEnfermeria", parametros);
  
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
        /// Obtiene El animal que se encuentren salida
        /// </summary>
        /// <param name="animalInfo"></param>
        /// <returns></returns>
        internal AnimalSalidaInfo AnimalSalidaEnfermeria(AnimalInfo animalInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxEnfermeriaDAL.ObtenerParametrosAnimalSalidaEnfermeria(animalInfo);
                DataSet ds = Retrieve("CorteTransferenciaGanado_AnimalSalidaEnfermeria", parametros);
                AnimalSalidaInfo animalResultadoInfo = null;

                if (ValidateDataSet(ds))
                {
                    animalResultadoInfo = MapAnimalDAL.ObtenerAnimalSalidaDatos(ds);
                }
                return animalResultadoInfo;
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
        /// Obtiene los datos de la compra por folio entrada
        /// </summary>
        /// <param name="folioEntrada"></param>
        /// <param name="organicacionId"></param>
        /// <returns></returns>
        internal DatosCompra ObtenerDatosCompra(int folioEntrada, int organicacionId)
        {
            DatosCompra resultado = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEnfermeriaDAL.ObtenerParametrosDatosCompra(folioEntrada, organicacionId);
                DataSet ds = Retrieve("Enfermeria_ObtenerDatosCompra", parametros);

                if (ValidateDataSet(ds))
                {
                    resultado = MapEnfermeriaDal.ObtenerDatosCompra(ds);
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<EnfermeriaInfo> ObtenerPorPagina(PaginacionInfo pagina, EnfermeriaInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxEnfermeriaDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("[dbo].[Enfermeria_ObtenerPorPagina]", parameters);
                ResultadoInfo<EnfermeriaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapEnfermeriaDal.ObtenerPorPagina(ds);
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
        /// Obtiene un registro de Enfermeria
        /// </summary>
        /// <param name="filtro">Identificador de la Enfermeria</param>
        /// <returns></returns>
        public EnfermeriaInfo ObtenerPorID(EnfermeriaInfo filtro)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEnfermeriaDAL.ObtenerParametrosPorID(filtro);
                DataSet ds = Retrieve("Enfermeria_ObtenerPorID", parameters);
                EnfermeriaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapEnfermeriaDal.ObtenerPorID(ds);
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
        /// Metodo para Crear un registro de Enfermeria
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(EnfermeriaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEnfermeriaDAL.ObtenerParametrosCrear(info);
                int result = Create("Enfermeria_Crear", parameters);
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
        /// Metodo para actualizar un registro de Enfermeria
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(EnfermeriaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEnfermeriaDAL.ObtenerParametrosActualizar(info);
                Update("Enfermeria_Actualizar", parameters);
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
        /// Obtiene un registro de Enfermeria
        /// </summary>
        /// <param name="descripcion">Descripción de la Enfermeria</param>
        /// <param name="organizacionID"> </param>
        /// <returns></returns>
        public EnfermeriaInfo ObtenerPorDescripcion(string descripcion, int organizacionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEnfermeriaDAL.ObtenerParametrosPorDescripcion(descripcion, organizacionID);
                DataSet ds = Retrieve("Enfermeria_ObtenerPorDescripcion", parameters);
                EnfermeriaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapEnfermeriaDal.ObtenerPorDescripcion(ds);
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

        internal AnimalDeteccionInfo ObtenerAnimalDetectadoPorAreteSinActivo(AnimalInfo animalEnfermo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEnfermeriaDAL.ObtenerParametrosObtenerAnimalDetectadoPorArete(animalEnfermo);
                DataSet ds = Retrieve("Enfermeria_ObtenerGanadoEnfermoPorAreteSinActivo", parametros);
                AnimalDeteccionInfo animalDetectado = null;

                if (ValidateDataSet(ds))
                {
                    animalDetectado = MapEnfermeriaDal.ObtenerAnimalesEnfermeriaPorCorralSinActivo(ds, animalEnfermo.OrganizacionIDEntrada)[0];
                }
                return animalDetectado;
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
        /// Metodo para obtener la ultima deteccion que sele realizo al animal
        /// </summary>
        /// <param name="animalEnfermo"></param>
        /// <returns></returns>
        internal AnimalDeteccionInfo ObtenerAnimalDetectadoPorAreteUltimaDeteccion(AnimalInfo animalEnfermo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxEnfermeriaDAL.ObtenerParametrosObtenerAnimalDetectadoPorArete(animalEnfermo);
                DataSet ds = Retrieve("Enfermeria_ObtenerAnimalDetectadoPorAreteUltimaDeteccion", parametros);
                AnimalDeteccionInfo animalDetectado = null;

                if (ValidateDataSet(ds))
                {
                    animalDetectado = MapEnfermeriaDal.ObtenerAnimalDetectadoPorAreteUltimaDeteccion(ds, animalEnfermo.OrganizacionIDEntrada)[0];
                }
                return animalDetectado;
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
