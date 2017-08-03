using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class TipoGanadoDAL : DALBase
    {
        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<TipoGanadoInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoGanadoInfo filtro)
        {
            ResultadoInfo<TipoGanadoInfo> tipoGanadoLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxTipoGanadoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("TipoGanado_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    tipoGanadoLista = MapTipoGanadoDAL.ObtenerPorPagina(ds);
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
            return tipoGanadoLista;
        }


        /// <summary>
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<TipoGanadoInfo> Centros_ObtenerPorPagina(PaginacionInfo pagina, TipoGanadoInfo filtro)
        {
            ResultadoInfo<TipoGanadoInfo> tipoGanadoLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxTipoGanadoDAL.Centros_ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("TipoGanadoCentros_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    tipoGanadoLista = MapTipoGanadoDAL.Centros_ObtenerPorPagina(ds);
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
            return tipoGanadoLista;
        }

        /// <summary>
        ///     Metodo que crear un Tipo Ganado
        /// </summary>
        /// <param name="info"></param>
        internal int Crear(TipoGanadoInfo info)
        {
            int infoId;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoGanadoDAL.ObtenerParametrosCrear(info);
                infoId = Create("[dbo].[TipoGanado_Crear]", parameters);
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

            return infoId;
        }

        /// <summary>
        ///     Metodo que crear un Tipo Ganado
        /// </summary>
        /// <param name="info"></param>
        internal int Centros_Crear(TipoGanadoInfo info)
        {
            int infoId;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoGanadoDAL.Centros_ObtenerParametrosCrear(info);
                infoId = Create("[dbo].[TipoGanadoCentros_Crear]", parameters);
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

            return infoId;
        }

        /// <summary>
        ///     Metodo que actualiza un Tipo Ganado
        /// </summary>
        /// <param name="info"></param>
        internal void Actualizar(TipoGanadoInfo info)
        {
            try
            {
                Dictionary<string, object> parameters = AuxTipoGanadoDAL.ObtenerParametrosActualizar(info);
                Update("[dbo].[TipoGanado_Actualizar]", parameters);
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
        ///     Metodo que actualiza un Tipo Ganado
        /// </summary>
        /// <param name="info"></param>
        internal void Centros_Actualizar(TipoGanadoInfo info)
        {
            try
            {
                Dictionary<string, object> parameters = AuxTipoGanadoDAL.Centros_ObtenerParametrosActualizar(info);
                Update("[dbo].[TipoGanadoCentros_Actualizar]", parameters);
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
        ///     Obtiene un TipoGanadoInfo por Id
        /// </summary>
        /// <param name="infoId"></param>
        /// <returns></returns>
        internal TipoGanadoInfo ObtenerPorID(int infoId)
        {
            TipoGanadoInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoGanadoDAL.ObtenerParametroPorID(infoId);
                DataSet ds = Retrieve("[dbo].[TipoGanado_ObtenerPorID]", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapTipoGanadoDAL.ObtenerPorID(ds);
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
        ///     Obtiene una lista de todos los TipoOrganizaciones
        /// </summary>
        /// <returns></returns>
        internal List<TipoGanadoInfo> ObtenerTodos()
        {
            List<TipoGanadoInfo> result = null;
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("[dbo].[TipoGanado_ObtenerTodos]");
                if (ValidateDataSet(ds))
                {
                    result = MapTipoGanadoDAL.ObtenerTodos(ds);
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
        ///   Obtiene una lista de TipoGanado filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal List<TipoGanadoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            List<TipoGanadoInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoGanadoDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("TipoGanado_ObtenerTodos", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapTipoGanadoDAL.ObtenerTodos(ds);
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
        /// Metodo que obtiene los rangos iniciales por el sexo del ganado
        /// </summary>
        /// <param name="sexo"></param>
        /// <returns></returns>
        internal List<TipoGanadoInfo> ObtenerPorSexo(String sexo)
        {
            List<TipoGanadoInfo> lista = null;
            try
            {
                Dictionary<string, object> parameters = AuxTipoGanadoDAL.ObtenerParametrosPorSexo(sexo);
                DataSet ds = Retrieve("[dbo].[TipoGanado_ObtenerPorSexo]", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapTipoGanadoDAL.ObtenerPorSexo(ds);
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
            return lista;
        }

        /// <summary>
        /// Metodo que regresa un TipoGanadoInfo con el rango final y tipo de ganado
        /// por el sexo del ganado y el rango inicial 
        /// </summary>
        /// <param name="sexo"></param>
        /// <param name="rangoInicial"></param>
        /// <returns></returns>
        internal TipoGanadoInfo ObtenerPorSexoRangoInicial(String sexo, decimal rangoInicial)
        {
            TipoGanadoInfo tipoGanadoInfo = null;
            try
            {
                Dictionary<string, object> parameters = AuxTipoGanadoDAL.ObtenerParametrosPorSexoRangoInicial(sexo, rangoInicial);
                DataSet ds = Retrieve("[dbo].[TipoGanado_ObtenerPorSexoRangoInicial]", parameters);
                if (ValidateDataSet(ds))
                {
                    tipoGanadoInfo = MapTipoGanadoDAL.ObtenerPorSexoRangoInicial(ds);
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
            return tipoGanadoInfo;
        }

        /// <summary>
        /// Metodo que obtiene El tipo de ganado en base al sexo y el pedo obtenido en la bascula
        /// </summary>
        /// <param name="sexo"></param>
        /// <param name="peso"></param>
        /// <returns></returns>
        internal TipoGanadoInfo ObtenerTipoGanadoSexoPeso(String sexo, int peso)
        {
            TipoGanadoInfo tipoGanadoInfo = null;
            try
            {
                Dictionary<string, object> parameters = AuxTipoGanadoDAL.ObtenerParametrosTipoGanadoSexoPeso(sexo, peso);
                DataSet ds = Retrieve("[dbo].[CorteGanado_ObtenerTipoGanadoSexoPeso]", parameters);
                if (ValidateDataSet(ds))
                {
                    tipoGanadoInfo = MapTipoGanadoDAL.ObtenerTipoGanadoSexoPeso(ds);
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
            return tipoGanadoInfo;
        }

        /// <summary>
        /// Metodo que obtiene El tipo de ganado en base al TipoGanadoID que se encuentra en al tabla de InterfaceSalidaAnimal
        /// se filtrara por OrganizacionID, SalidaID, Arete y se llenaran en un objeto del tipo InterfaceSalidaInfo
        /// </summary>
        /// <param name="interfaceSalidaInfo"></param>
        /// <returns></returns>
        internal TipoGanadoInfo ObtenerTipoGanadoDeInterfaceSalida(InterfaceSalidaInfo interfaceSalidaInfo)
        {
            TipoGanadoInfo tipoGanadoInfo = null;
            try
            {
                var parameters = AuxTipoGanadoDAL.ObtenerParametrosTipoGanadoDeInterfaceSalida(interfaceSalidaInfo);
                var ds = Retrieve("[dbo].[CorteGanado_ObtenerTipoGanadoDeInterfaceSalida]", parameters);
                if (ValidateDataSet(ds))
                {
                    tipoGanadoInfo = MapTipoGanadoDAL.ObtenerTipoGanadoSexoPeso(ds);
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
            return tipoGanadoInfo;
        }

        /// <summary>
        /// Obtiene un registro de TipoGanado
        /// </summary>
        /// <param name="descripcion">Descripción de la TipoGanado</param>
        /// <returns></returns>
        internal TipoGanadoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoGanadoDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("TipoGanado_ObtenerPorDescripcion", parameters);
                TipoGanadoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoGanadoDAL.ObtenerPorDescripcion(ds);
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
        /// Obtiene un registro de TipoGanado
        /// </summary>
        /// <param name="descripcion">Descripción de la TipoGanado</param>
        /// <returns></returns>
        internal TipoGanadoInfo Centros_ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoGanadoDAL.Centros_ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("TipoGanadoCentros_ObtenerPorDescripcion", parameters);
                TipoGanadoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoGanadoDAL.Centros_ObtenerPorDescripcion(ds);
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
        /// Obtiene un tipoGanado por la entradaGanado
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        internal TipoGanadoInfo ObtenerTipoGanadoIDPorEntradaGanado(int entradaGanadoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoGanadoDAL.ObtenerParametrosTipoGanadoIDPorEntradaGanado(entradaGanadoID);
                DataSet ds = Retrieve("TipoGanadoCentros_ObtenerTipoGanadoIDPorEntradaGanado", parameters);
                TipoGanadoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoGanadoDAL.ObtenerTipoGanadoIDPorEntradaGanado(ds);
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

        internal List<TipoGanadoInfo> ObtenerDescripcionesPorIDs(List<int> tiposGanadoIDs)
        {
            try
            {
                Logger.Info();
                
                var tipos = string.Join("|", tiposGanadoIDs.ToArray());

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@tiposGanadoID", tipos);

                DataSet ds = Retrieve("TipoGanado_ObtenerDescripcionesPorIDs", parameters);
                var result = new List<TipoGanadoInfo>();
                if (ValidateDataSet(ds))
                {
                    result = MapTipoGanadoDAL.ObtenerDescripcionesPorIDs(ds);
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

        internal List<ContenedorTipoGanadoPoliza> ObtenerTipoPorAnimal(List<AnimalInfo> animales, TipoMovimiento tipoMovimiento)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoGanadoDAL.ObtenerParametrosPorAnimal(animales, tipoMovimiento);
                DataSet ds = Retrieve("TipoGanado_ObtenerPorAnimalXML", parameters);
                var result = new List<ContenedorTipoGanadoPoliza>();
                if (ValidateDataSet(ds))
                {
                    result = MapTipoGanadoDAL.ObtenerTipoPorAnimal(ds);
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
