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
    internal class MenuDAL : DALBase
    {
        /// <summary>
        ///     Obtiene un lista de las opciones que el usuario puede ver
        /// </summary>
        /// <param name="usuario"></param>        
        /// <param name="aplicacionWeb">Indica si se esta accediento desde la aplicacion Web</param>
        /// <returns></returns>
        internal IList<MenuInfo> ObtenerPorUsuario(string usuario, bool aplicacionWeb)
        {
            IList<MenuInfo> menuLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxMenuDAL.ObtenerParametrosPorUduario(usuario, aplicacionWeb);
                DataSet ds = Retrieve("Menu_ObtenerPorUsuario", parameters);
                if (ValidateDataSet(ds))
                {
                    menuLista = MapMenuDAL.ObtenerTodos(ds);
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
            return menuLista;
        }
    }
}