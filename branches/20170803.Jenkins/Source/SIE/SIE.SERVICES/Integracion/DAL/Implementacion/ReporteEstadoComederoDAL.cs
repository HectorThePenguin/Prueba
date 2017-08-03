using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ReporteEstadoComederoDAL : DALBase
    {
        void MetodoBase(Action accion)
        {
            try
            {
                Logger.Info();
                accion();
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

        internal List<AlimentacionCorralPorFormulaModel> ObtenerCorralesPorFormula(int organizacionID)
        {
            var lista = new List<SIE.Services.Info.Modelos.AlimentacionCorralPorFormulaModel>();
            MetodoBase(() =>
            {
                Dictionary<string,object> parametros = new Dictionary<string,object>();
                parametros.Add("@OrganizacionID", organizacionID);
                DataSet ds = Retrieve("ReporteEstadoComederos_ObtenerCorralesPorFormula", parametros);
                if (ValidateDataSet(ds))
                {
                    lista = MapReporteEstadoComederoDAL.ObtenerCorralesPorFormula(ds);
                }
            });
            return lista;
        }

        internal List<AlimentacionCorralPorEstadoComederoModel> ObtenerCorralesPorEstadoComedero(int organizacionID)
        {
            var lista = new List<SIE.Services.Info.Modelos.AlimentacionCorralPorEstadoComederoModel>();
            MetodoBase(() =>
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@OrganizacionID", organizacionID);
                DataSet ds = Retrieve("ReporteEstadoComederos_ObtenerCorralesPorEstadoComedero", parametros);
                if (ValidateDataSet(ds))
                {
                    lista = MapReporteEstadoComederoDAL.ObtenerCorralesPorEstadoComedero(ds);
                }
            });
            return lista;
        }

        internal AlimentacionEstadoComederoModel Generar(int organizacionID)
        {
            var respuesta = new SIE.Services.Info.Modelos.AlimentacionEstadoComederoModel();
            MetodoBase(() =>
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@OrganizacionID", organizacionID);
                DataSet ds = Retrieve("ReporteEstadoComederos_Generar", parametros); //sp de prueba
                if (ValidateDataSet(ds))
                {
                    respuesta = MapReporteEstadoComederoDAL.Generar(ds);
                }
            });
            return respuesta;
        }
    }
}
