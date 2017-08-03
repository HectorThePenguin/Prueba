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
    public class LectorRegistroDAL : DALBase
    {
        /// <summary>
        /// Obtiene un lector
        /// </summary>
        /// <param name="lote">Lote del cual se obtendra el lector</param>
        /// <param name="fecha">Fecha del lector</param>
        /// <returns></returns>
        internal LectorRegistroInfo ObtenerLectorRegistro(LoteInfo lote, DateTime fecha)
        {
            var resultado = new LectorRegistroInfo();
            try
            {
                Logger.Info();
                var parameters = AuxLectorRegistro.ObtenerParametroLectorRegistro(lote, fecha);
                var ds = Retrieve("Reparto_ObtenerLectorRegistro", parameters);
                if (ValidateDataSet(ds))
                {
                    resultado = MapLectorRegistro.ObtenerLectorRegistro(ds);
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
        /// Obtiene el detalle del lector
        /// </summary>
        /// <param name="lectorRegistro"></param>
        /// <returns></returns>
        internal List<LectorRegistroDetalleInfo> ObtenerDetalleLectorRegistro(LectorRegistroInfo lectorRegistro)
        {
            var resultado = new List<LectorRegistroDetalleInfo>();
            try
            {
                Logger.Info();
                var parameters = AuxLectorRegistro.ObtenerParametroDetalleLectorRegistro(lectorRegistro);
                var ds = Retrieve("Reparto_ObtenerDetalleLectorRegistro", parameters);
                if (ValidateDataSet(ds))
                {
                    resultado = MapLectorRegistro.ObtenerDetalleLectorRegistro(ds);
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
        /// Obtiene un lector
        /// </summary>
        /// <param name="lotesXml">Lote del cual se obtendra el lector</param>
        /// <param name="fecha">Fecha del lector</param>
        /// <param name="organizacionID">Fecha del lector</param>
        /// <returns></returns>
        internal IList<LectorRegistroInfo> ObtenerLectorRegistroXML(IList<LoteInfo> lotesXml, DateTime fecha, int organizacionID)
        {
            var resultado = new List<LectorRegistroInfo>();
            try
            {
                Logger.Info();
                var parameters = AuxLectorRegistro.ObtenerParametroLectorRegistroXML(lotesXml, fecha, organizacionID);
                var ds = Retrieve("Reparto_ObtenerLectorRegistroXML", parameters);
                if (ValidateDataSet(ds))
                {
                    resultado = MapLectorRegistro.ObtenerLectorRegistroXML(ds);
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
