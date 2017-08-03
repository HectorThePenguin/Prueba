using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class DiferenciasDeInventarioDAL : DALBase
    {
        /// <summary>
        /// Obtener ajustes pendientes
        /// </summary>
        /// <returns></returns>
        internal List<DiferenciasDeInventariosInfo> ObtenerAjustesPendientesPorUsuario(List<EstatusInfo> listaEstatusInfo, List<TipoMovimientoInfo> listaTiposMovimiento, UsuarioInfo usuarioInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxDiferenciasDeInventarioDAL.ObtenerParametrosObtenerDiferenciasInventario(listaEstatusInfo, listaTiposMovimiento, usuarioInfo);
                var ds = Retrieve("DiferenciasDeInventario_ObtenerAjustesPendientes", parameters);
                List<DiferenciasDeInventariosInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapDiferenciasDeInventarioDAL.ObtenerDiferenciasInventario(ds);
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
