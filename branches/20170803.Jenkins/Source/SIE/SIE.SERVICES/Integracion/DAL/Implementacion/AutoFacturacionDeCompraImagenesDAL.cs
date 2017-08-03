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
    internal class AutoFacturacionDeCompraImagenesDAL : DALBase
    {
        internal AutoFacturacionInfo ObtenerImagenDocumento(AutoFacturacionInfo info)
        {
            try
            {
                Logger.Info();
                var parameters = AuxAutoFacturacionDeCompraImagenesDAL.ObtenerParametrosObtenerImagenDocumento(info);
                var ds = Retrieve("CacImagenCompra_ObtenerImagen", parameters);
                AutoFacturacionInfo result = null;
                if(ValidateDataSet(ds))
                {
                    if(!string.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString()) )
                    {
                        result = MapAutoFacturacionDeCompraImagenesDAL.ObtenerImagenDocumento(ds);
                    }
                }
                return result;
            }
            catch(SqlException ex)
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

        internal AutoFacturacionInfo ObtenerImagenIneCurp(AutoFacturacionInfo info)
        {
            try
            { 
                Logger.Info();
                var parameters = AuxAutoFacturacionDeCompraImagenesDAL.ObtenerParametrosObtenerImagenIneCurp(info);
                var ds = Retrieve("CacImagen_ObtenerImagenes", parameters);
                AutoFacturacionInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapAutoFacturacionDeCompraImagenesDAL.ObtenerImagenIneCurp(ds);
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
