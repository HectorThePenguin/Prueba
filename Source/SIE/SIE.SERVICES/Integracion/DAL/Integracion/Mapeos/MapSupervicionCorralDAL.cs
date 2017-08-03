
using System;
using System.Data;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapSupervicionCorralDAL
    {
        internal static NombreDetectorEvaluadorInfo ObtenerAnimal(DataSet ds)
        {
            
           NombreDetectorEvaluadorInfo nombreEvaluador = null;
           try
           {
                Logger.Info();
                DataTable dt = ds.Tables[ConstantesDAL.DtDatos];
                nombreEvaluador = new NombreDetectorEvaluadorInfo();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    nombreEvaluador.OperadorID = Convert.ToInt32("OperadorID");
                    nombreEvaluador.Nombre = Convert.ToString("Nombre");
                    nombreEvaluador.ApellidoPaterno = Convert.ToString("ApellidoPaterno");
                    nombreEvaluador.ApellidoMaterno = Convert.ToString("ApellidoMaterno");
                    nombreEvaluador.EnfermeriaID = Convert.ToInt32("EnfermeriaID");
                    nombreEvaluador.CorralID = Convert.ToInt32("CorralID");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return nombreEvaluador;
        }
    }
}
