using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class TransferenciaGanadoIndividualDAL : DALBase
    {
        /// <summary>
        /// Metodo para guardar la transferencia de ganado
        /// </summary>
        /// <param name="animal"></param>
        /// <param name="corralDestino"></param>
        /// <param name="usuario"></param>
        /// <param name="decrementaCabezas"></param>
        /// <returns></returns>
        internal bool GuardarTransferenciaGanado(AnimalInfo animal, CorralInfo corralDestino, int usuario, bool decrementaCabezas)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object>
                        {
                            {"@AnimalID", animal.AnimalID},
                            {"@CorralDestinoID", corralDestino.CorralID},
                            {"@UsuarioCreacionID", usuario},
                            {"@DecrementaCabezas",  decrementaCabezas}
                        };

                Create("TransferenciaGanadoIndividual_Guardar", parametros);

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

        internal bool GuardarTransferenciaGanado(long animalID, int loteID)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object>
                        {
                            {"@AnimalID", animalID},
                            {"@LoteID", loteID},
                        };

                Create("TransferenciaGanadoIndividual_GuardarPorLoteID", parametros);

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
    }
}
