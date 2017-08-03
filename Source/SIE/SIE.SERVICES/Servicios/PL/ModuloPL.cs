using System;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ModuloPL
    {
        /// <summary>
        /// consulta la informacion de un modulo especificado
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<ModuloInfo> ObtenerPorID(ModuloInfo filtro)
        {
            ResultadoInfo<ModuloInfo> result;
            try
            {
                Logger.Info();
                var moduloBL = new ModuloBL();
                result = moduloBL.ObtenerPorId(filtro);
            }
            catch (ExcepcionGenerica)
            {
                throw;
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
