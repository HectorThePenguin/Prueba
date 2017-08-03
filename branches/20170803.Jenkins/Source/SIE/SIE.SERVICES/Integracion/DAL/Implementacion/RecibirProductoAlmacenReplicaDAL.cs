using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class RecibirProductoAlmacenReplicaDAL : DALBase
    {
        internal List<string> GuardarAretes(List<string> aretes, int organizacionId, int usuarioId)
        {
            var result = new List<string>();
            try
            {
                Logger.Info();
                var parameters = AuxRecibirProductoAlmacenReplicaDAL.ObtenerParametros(aretes, organizacionId, usuarioId);
                DataSet ds = Retrieve("CatAreteSukarne_Insertar", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapRecibirProductoAlmacenReplicaDAL.Guardar(ds);
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

            return result;
        }

        internal List<string> ConsultarAretes(List<string> aretes, int organizacionId)
        {
            var result = new List<string>();
            try
            {
                Logger.Info();
                var parameters = AuxRecibirProductoAlmacenReplicaDAL.ObtenerParametros(aretes, organizacionId);
                DataSet ds = Retrieve("CatAreteSukarne_Consultar", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapRecibirProductoAlmacenReplicaDAL.Consultar(ds);
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

            return result;
        }
    }
}
