using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Base.Exepciones;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal static class AuxEntradaGanadoSobranteDAL
    {
        /// <summary>
        /// Metodo que obtiene los parametros para guardar una entrada ganado sobrante
        /// </summary>
        /// <param name="entradaGanadoSobranteInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear(EntradaGanadoSobranteInfo entradaGanadoSobranteInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                //int? corralID = entradaGanado.CorralID;

                parametros = new Dictionary<string, object>
                    {                        
                        {"@EntradaGanadoID", entradaGanadoSobranteInfo.EntradaGanado.EntradaGanadoID},
                        {"@AnimalID", entradaGanadoSobranteInfo.Animal.AnimalID},
                        {"@Importe", entradaGanadoSobranteInfo.Importe},
                        {"@Costeado", entradaGanadoSobranteInfo.Costeado},
                        {"@UsuarioCreacionID", entradaGanadoSobranteInfo.UsuarioCreacionID}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }
    }
}
