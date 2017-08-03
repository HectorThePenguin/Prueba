﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class TipoRetencionDAL : DALBase 
    {
        internal List<TipoRetencionInfo> ObtenerTodos(EstatusEnum estatus) 
        {
            List<TipoRetencionInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoRetencionDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("TipoRetencion_ObtenerTodos", parameters);
                if(ValidateDataSet(ds))
                {
                    result = MapTipoRetencionDAL.ObtenerTodos(ds);
                }
                return result;
            }
            catch(SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch(DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                    throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(),ex);
            }

        }
    }
}
