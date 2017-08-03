using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class ControlSacrificioBL
    {
        public void SyncResumenSacrificio_planchado(List<ControlSacrificioInfo.SincronizacionSIAP> informacionSIAP, int usuarioID, int organizacionId)
        {
            var sincrinizacion = new ControlSacrificioDAL();
            sincrinizacion.SyncResumenSacrificio_planchado(informacionSIAP, usuarioID, organizacionId);
        }

        public void SyncResumenSacrificio_transferencia(List<ControlSacrificioInfo.SincronizacionSIAP> informacionSIAP, int usuarioID, int organizacionId)
        {
            var sincrinizacion = new ControlSacrificioDAL();
            sincrinizacion.SyncResumenSacrificio_transferencia(informacionSIAP, usuarioID, organizacionId);
        }
    }
}