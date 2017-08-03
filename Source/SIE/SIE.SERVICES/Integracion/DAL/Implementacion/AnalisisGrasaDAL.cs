using System;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using System.Data;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    public class AnalisisGrasaDAL:DALBase
    {
        // Guarda un analisis de grasa executando el sp
        internal void Guardar(AnalisisGrasaInfo analisisGrasaInfo)
        {
            try
            {
                Logger.Info();
                var parametros = AuxAnalisisGrasaDAL.ObtenerParametrosGuardar(analisisGrasaInfo);
                Create("AnalisisGrasa_Crear", parametros);
            }
            catch(SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(),ex);
            }
            catch(DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(),ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(),ex);
            }
        }

    }
}
