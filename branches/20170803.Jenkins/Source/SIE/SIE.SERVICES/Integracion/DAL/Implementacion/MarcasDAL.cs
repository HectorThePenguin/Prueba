using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SIE.Base.Infos;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Base.Log;
using BLToolkit.Data.Sql;
using SIE.Services.Integracion.DAL.Excepciones;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using DataException = BLToolkit.Data.DataException;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    public class MarcasDAL : DALBase
    {
        /// <summary>
        /// Método que conecta a la base de datos para realizar el guardado de la información
        /// </summary>
        /// <param name="marcasInfo"> Objeto con la información de la marca a registrar. </param>
        /// <returns> Objeto con la información de la marca registrada </returns>
        public MarcasInfo GuardarMarca(MarcasInfo marcasInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxMarcasDAL.ObtenerParametrosGuardarMarca(marcasInfo);
                var ds = Retrieve("Marcas_GuardaMarca", parameters);
                if (ValidateDataSet(ds))
                {
                    marcasInfo = MapMarcasDAL.GuardaMarca(ds);
                }
                return marcasInfo;
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
        /// Método que se utiliza para actualizar la información de una marca.
        /// </summary>
        /// <param name="marcasInfo"> Objeto con la información de la marca a guardar. </param>
        /// <returns> Objeto con la información de la marca guardada. </returns>
        public MarcasInfo ActualizarMarca(MarcasInfo marcasInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxMarcasDAL.ObtenerParametrosActualizarMarca(marcasInfo);
                var ds = Retrieve("Marcas_ActualizaMarca", parameters);
                if (ValidateDataSet(ds))
                {
                    marcasInfo = MapMarcasDAL.ActulizarMarca(ds);
                }
                return marcasInfo;
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
        /// Método para obtener las marcas registradas delimitadas por el paginador.
        /// </summary>
        /// <param name="pagina"> Información del filtro del paginador </param>
        /// <param name="filtro"> Información del filtro de la marca </param>
        /// <returns></returns>
        public ResultadoInfo<MarcasInfo> ObtenerPorPagina(PaginacionInfo pagina, MarcasInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxMarcasDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Marcas_ObtenerPorPagina", parameters);
                ResultadoInfo<MarcasInfo> lista = null;
                if (ValidateDataSet(ds))
                {
                    lista = MapMarcasDAL.ObtenerPorPagina(ds);
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
        /// Método que obtiene todas las marcas registradas en la base de datos.
        /// </summary>
        /// <returns> Lista con las marcas encontradas. </returns>
        public IList<MarcasInfo> ObtenerMarcas(EstatusEnum Tipo, EstatusEnum Activo)
        {
            IList<MarcasInfo> marcasInfo = null;
            try 
            {
                
                Logger.Info();
                Dictionary<string, object> parameters = AuxMarcasDAL.ObtenerMarcas(Tipo, Activo);
                var ds = Retrieve("Marcas_ObtenerMarcas", parameters);
                if (ValidateDataSet(ds))
                {
                    marcasInfo = MapMarcasDAL.ObtenerMarcas(ds);
                }
                return marcasInfo;
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
        /// Método que selecciona una marca para comprobar para la existencia.
        /// </summary>
        /// <param name="marcasInfo"> Objeto con la información de la marca a buscar. </param>
        /// <returns> Objeto con la información de la marca encontrada. </returns>
        public MarcasInfo VerificaExistenciaMarca(MarcasInfo marcasInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxMarcasDAL.VerificaExistenciaMarca(marcasInfo);
                marcasInfo = new MarcasInfo();
                var ds = Retrieve("Marcas_VerificaExistenciaMarca", parameters);
                if (ValidateDataSet(ds))
                {
                    marcasInfo = MapMarcasDAL.VerificaExistenciaMarca(ds);
                }
                return marcasInfo;
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
