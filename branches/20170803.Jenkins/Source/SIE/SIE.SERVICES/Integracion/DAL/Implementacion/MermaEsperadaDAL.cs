
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class MermaEsperadaDAL : DALBase
    {
        /// <summary>
        /// Guarda una lista de mermas esperadas
        /// </summary>
        /// <param name="mermas"></param>
        internal bool Guardar(List<MermaEsperadaInfo> mermas)
        {
            try
            {
                Dictionary<string, object> parameters = AuxMermaEsperadaDAL.ObtenerParametrosGuardar(mermas);
                Create("MermaEsperada_GuardarLista", parameters);

                return true;
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
        /// Obtiene una lista paginada de las mermas registradas para la organizacion origen
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal List<MermaEsperadaInfo> ObtenerMermaPorOrganizacionOrigenID(MermaEsperadaInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxMermaEsperadaDAL.ObtenerMermaPorOrganizacionOrigenID(filtro);
                DataSet ds = Retrieve("MermaEsperada_ObtenerPorOrganizacionOrigenID", parameters);
                List<MermaEsperadaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapMermaEsperadaDAL.ObtenerMermaPorOrganizacionOrigenID(ds);
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
