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
    internal class ListaPreciosCentrosDAL : DALBase
    {
        internal List<ListaPreciosCentrosInfo> ObtenerListaPreciosCentros(ListaPreciosCentrosInfo info)
        {
            try
            {
                Logger.Info();
                var parameters = AuxListaPreciosCentrosDAL.ObtenerParametrosListaPreciosCentros(info);
                var ds = Retrieve("CatPrecioGanadoOrganizacion_ObtenerListaPrecio", parameters);
                var result = new List<ListaPreciosCentrosInfo>();
                if (ValidateDataSet(ds))
                {
                    result = MapListaPreciosCentrosDAL.ObtenerListaPreciosCentros(ds);
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

        internal bool Guardar(List<ListaPreciosCentrosInfo> precios, int usuarioId)
        {
            try
            {
                Logger.Info();
                var result = false;
                var parameters = AuxListaPreciosCentrosDAL.ObtenerParametrosListaPreciosCentrosGuardar(precios, usuarioId);
                var ds = Retrieve("CatPrecioGanadoOrganizacion_Guardar", parameters);

                if (ValidateDataSet(ds))
                {
                    result = MapListaPreciosCentrosDAL.Guardar(ds);
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
