
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    internal class MermaEsperadaBL
    {
        /// <summary>
        /// Guarda una lista de mermas esperadas
        /// </summary>
        /// <param name="mermas"></param>
        internal bool Guardar(List<MermaEsperadaInfo> mermas)
        {
            bool result = false;
            try
            {
                MermaEsperadaDAL mermaEsperadaDal = new MermaEsperadaDAL();
                result = mermaEsperadaDal.Guardar(mermas);
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
        internal List<MermaEsperadaInfo> ObtenerMermaPorOrganizacionOrigenID(MermaEsperadaInfo filtro)
        {
            List<MermaEsperadaInfo> result;
            try
            {
                Logger.Info();
                var mermaEsperadaDAL = new MermaEsperadaDAL();
                result = mermaEsperadaDAL.ObtenerMermaPorOrganizacionOrigenID(filtro);
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
