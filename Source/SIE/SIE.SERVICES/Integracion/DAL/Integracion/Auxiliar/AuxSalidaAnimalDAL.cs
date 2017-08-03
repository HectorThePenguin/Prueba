using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxSalidaAnimalDAL
    {
        /// <summary>
        /// Metodo para crear los parametros paraguardar Salida Animal
        /// </summary>
        /// <param name="salidaAnimalInfo"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosSalidaGanado(SalidaAnimalInfo salidaAnimalInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@SalidaGanadoID", salidaAnimalInfo.SalidaGanado.SalidaGanadoID},
                            {"@AnimalID", salidaAnimalInfo.Animal.AnimalID},
                            {"@LoteID", salidaAnimalInfo.Lote.LoteID},
                            {"@Activo", salidaAnimalInfo.Activo},
                            {"@UsuarioCreacionID", salidaAnimalInfo.UsuarioCreacionID}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
