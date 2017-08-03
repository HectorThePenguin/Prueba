using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Base.Infos;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using System.Xml.Linq;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class AutoFacturacionDAL: DALBase
    {
        internal ResultadoInfo<AutoFacturacionInfo> ObtenerAutoFacturacion(PaginacionInfo pagina, AutoFacturacionInfo info)
        {
            ResultadoInfo<AutoFacturacionInfo> result = null;
            try
            {
                Logger.Info();
                var parameters = AuxAutoFacturacionDAL.ObtenerParametrosAutoFacturacion(pagina,info);
                DataSet ds = Retrieve("AutoFacturacion_ObtenerTodos", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapAutoFacturacionDAL.ObtenerAutoFacturacion(ds);
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

        internal int Guardar(AutoFacturacionInfo info)
        {
           var result = 0;
            try
            {
                Logger.Info();
                var parameters = AuxAutoFacturacionDAL.ObtenerParametrosGuardar(info);
                DataSet ds = Retrieve("CacCompraGanadoFactura_Guardar", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapAutoFacturacionDAL.ObtenerFolio(ds);
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

        internal List<AutoFacturacionInfo> ObtenerImagenes(AutoFacturacionInfo info)
        {
            var result = new List<AutoFacturacionInfo>();
            try
            {
                Logger.Info();
                var parameters = AuxAutoFacturacionDAL.ObtenerParametrosImagenes(info);
                DataSet ds = Retrieve("AutoFacturacion_ObtenerImagenes", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapAutoFacturacionDAL.ObtenerImagenes(ds);
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
