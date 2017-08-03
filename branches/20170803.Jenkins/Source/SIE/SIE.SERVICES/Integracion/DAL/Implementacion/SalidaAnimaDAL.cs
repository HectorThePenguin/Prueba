using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class SalidaAnimaDAL : DALBase
    {
        /// <summary>
        /// Metodo para almacenar la salidaAnimal
        /// </summary>
        /// <param name="salidaAnimalInfo"></param>
        /// <returns></returns>
        public bool GuardarAnimal(SalidaAnimalInfo salidaAnimalInfo)
        {
            bool resp = false;
            try
            {
                Logger.Info();
                var parameters = AuxSalidaAnimalDAL.ObtenerParametrosSalidaGanado(salidaAnimalInfo);
                Create("SalidaAnimal_Guardar", parameters);
                resp = true;

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
            return resp;
        }
    }
}
