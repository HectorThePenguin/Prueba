using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ComisionDAL : DALBase
    {

        internal List<TipoComisionInfo> ObtenerTiposComisiones()
        {
            List<TipoComisionInfo> resultado = new List<TipoComisionInfo>();
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Comision_ObtenerTipos");
               
                if (ValidateDataSet(ds))
                {
                    resultado = MapComisionDAL.ObtenerTiposComisiones(ds);
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
            return resultado;
        }

        /// <summary>
        /// Guarda la comision para el proveedor
        /// </summary>
        /// <returns></returns>
        internal bool GuardarComisiones(List<ComisionInfo> Comisiones)
        {
            bool result = false;

            try
            {
                Dictionary<string, object> parameters = AuxComisionDAL.ObtenerParametrosGuardar(Comisiones);
                Create("Comision_GuardarLista", parameters);

                result = true;
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

        internal List<ComisionInfo> obtenerComisionesProveedor(int ProveedorID)
        {
            List<ComisionInfo> resultado = new List<ComisionInfo>();

            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxComisionDAL.ObtenerParametrosComisionesProveedor(ProveedorID);
                DataSet ds = Retrieve("Proveedor_ObtenerComisiones", parameters);

                if (ValidateDataSet(ds))
                {
                    resultado = MapComisionDAL.obtenerResultadoComisionesProveedor(ds);
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

            return resultado;
        }

    }
}
