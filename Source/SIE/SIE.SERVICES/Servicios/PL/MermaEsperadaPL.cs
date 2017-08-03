using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SIE.Services.Servicios.PL
{
    public class MermaEsperadaPL
    {
        /// <summary>
        /// Guarda una lista de mermas esperadas
        /// </summary>
        /// <param name="mermas"></param>
        public bool Guardar(List<MermaEsperadaInfo> mermas)
        {
            bool result = false;
            try
            {
                MermaEsperadaBL mermaEsperadaBl = new MermaEsperadaBL();
                result = mermaEsperadaBl.Guardar(mermas);
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

        /// <summary>
        /// Obtiene una lista de las mermas registradas para la organizacion origen
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<MermaEsperadaInfo> ObtenerMermaPorOrganizacionOrigenID( MermaEsperadaInfo filtro)
        {
            List<MermaEsperadaInfo> result;
            try
            {
                Logger.Info();
                var mermaEsperadaBL = new MermaEsperadaBL();
                result = mermaEsperadaBL.ObtenerMermaPorOrganizacionOrigenID(filtro);
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
