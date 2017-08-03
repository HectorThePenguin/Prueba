
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
    public class SolicitudPremezclaDetalleDAL:DALBase
    {
        /// <summary>
        /// Guarda el detalle de una solicitud
        /// </summary>
        /// <param name="listaSolicitudPremezclaDetalle"></param>
        internal void Guardar(List<SolicitudPremezclaDetalleInfo> listaSolicitudPremezclaDetalle)
        {
            try
            {
                Logger.Info();
                var parameters = AuxSolicitudPremezclaDetalleDAL.ObtenerParametrosGuardar(listaSolicitudPremezclaDetalle);
                Create("SolicitudPremezclaDetalle_Guardar", parameters);
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
