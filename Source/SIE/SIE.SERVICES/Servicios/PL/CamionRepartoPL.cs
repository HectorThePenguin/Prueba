using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Interfaces;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class CamionRepartoPL
    {
        /// <summary>
        ///      Obtiene camion reparto por su Id
        /// </summary>
        /// <returns> </returns>
        public CamionRepartoInfo ObtenerCamionRepartoPorID(CamionRepartoInfo camionReparto)
        {
            CamionRepartoInfo info;
          // var  organizacionID = AuxConfiguracion.ObtenerOrganizacionUsuario();
            try
            {
                Logger.Info();
                var camionRepartoBL = new CamionRepartoBL();
                info = camionRepartoBL.ObtenerCamionRepartoPorID(camionReparto);
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
            return info;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<CamionRepartoInfo> ObtenerPorPagina(PaginacionInfo pagina, CamionRepartoInfo filtro)
        {
            try
            {
                Logger.Info();
                var camionRepartoBL = new CamionRepartoBL();
                return camionRepartoBL.ObtenerPorPagina(pagina, filtro);
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
        }
    }
}
