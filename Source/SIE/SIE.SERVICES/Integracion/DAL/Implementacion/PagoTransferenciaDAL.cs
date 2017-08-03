using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Collections.Generic;
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
    internal class PagoTransferenciaDAL: DALBase
    {
        internal ResultadoInfo<PagoTransferenciaInfo> ObtenerPorPagina(PaginacionInfo pagina, PagoTransferenciaInfo filtro, int centroAcopio, int folio)
        {
            ResultadoInfo<PagoTransferenciaInfo> lista = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxPagoTransferenciaDAL.ObtenerParametrosPagoConsulta(pagina, centroAcopio, folio);
                DataSet ds = Retrieve("PagoTransferencia_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    lista = MapPagoTransferenciaDAL.ObtenerPorPagina(ds);
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

        internal int Guardar(PagoTransferenciaInfo info)
        {
            int folio = 0;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxPagoTransferenciaDAL.ObtenerParametrosPago(info);
                folio = Create("PagosPorTransferencia_Guardar", parameters);
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
            return folio;
        }
    }
}
