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
    internal class AdministracionParametroChequeDAL : DALBase
    {

        /// <summary>
        /// Metodo para Crear un registro de parametro cheque
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int CrearParanetroCheque(List<CatParametroBancoInfo> info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAdministracionParametroChequeDAL.ObtenerParametroGuardarParametroCheque(info);
                int result = Create("CatParametroBanco_GuardarXML", parameters);
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
        /// Metodo para Crear un registro de parametro cheque
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int EditarParametroCheque(List<CatParametroBancoInfo> info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAdministracionParametroChequeDAL.ObtenerParametroEditarParametroCheque(info);
                int result = Create("CatParametroBanco_EditarXML", parameters);
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
        /// Obtiene diferencias de inventario
        /// </summary>
        /// <returns></returns>
        internal ResultadoInfo<CatParametroBancoInfo> ObtenerCatParamatroCheque(PaginacionInfo pagina, CatParametroBancoInfo catParametroBancoInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAdministracionParametroChequeDAL.ObtenerCatParametroBanco(pagina, catParametroBancoInfo);
                DataSet ds = Retrieve("CatParametroBanco_ObtenerPorPagina", parameters);
                ResultadoInfo<CatParametroBancoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAdministracionParametroBancoDAL.ObtenerParametroBancoPorPagina(ds);
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
        /// Obtiene administracion de parametro de cheque por descripcion
        /// </summary>
        /// <returns></returns>
        internal CatParametroBancoInfo ObtenerCatParamatroChequePorDescripcion(CatParametroBancoInfo catParametroBancoInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAdministracionParametroChequeDAL.ObtenerCatParametroBancoPorDescripcion(catParametroBancoInfo);
                DataSet ds = Retrieve("CatParametroBanco_ObtenerPorDescripcion", parameters);
                CatParametroBancoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAdministracionParametroBancoDAL.ObtenerParametroBancoPorDescripcion(ds);
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
