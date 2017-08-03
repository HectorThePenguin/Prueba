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
    internal class PrecioProductoDAL : DALBase
    {
        internal void Guardar(PrecioProductoInfo info)
        {
            try
            {
                Dictionary<string, object> parameters = AuxPrecioProductoDAL.ObtenerParametrosGuardar(info);
                Create("[dbo].[PrecioProducto_Guardar]", parameters);
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

        

        internal ResultadoInfo<PrecioProductoInfo> ObtenerPorPagina(PaginacionInfo pagina, PrecioProductoInfo filtros)
        {
            try
            {
                Dictionary<string, object> parameters = AuxPrecioProductoDAL.ObtenerParametrosPorPagina(pagina, filtros);
                DataSet ds = Retrieve("[dbo].[PrecioProducto_ObtenerPorPagina]", parameters);
                ResultadoInfo<PrecioProductoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapPrecioProductoDAL.ObtenerPorPagina(ds);
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
