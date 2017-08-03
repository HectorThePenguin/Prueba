using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SIE.Services.Info.Enums;


namespace SIE.Services.Info.Info
{
    public class ConfiguracionAlertasGeneraInfo
    {
        private AlertaInfo alertaInfo;
        private ConfiguracionAlertasInfo configuracionAlertas;
        private List<AccionInfo> listaAccionInfo;
        private AccionInfo accionInfo;
        private List<AlertaAccionInfo> listaAlertaAccionInfo;

        public AccionInfo AccionInfo
        {
            get { return accionInfo; }
            set
            {
                accionInfo = value;
            }
        }

        public List<AlertaAccionInfo> ListaAlertaAccionInfo
        {
            get { return listaAlertaAccionInfo;}
            set { listaAlertaAccionInfo = value;}
        }

        public  AlertaInfo AlertaInfo { 
            get { return alertaInfo; } 
            set { 
                alertaInfo = value;
                }
        }

        public List<AccionInfo> ListaAccionInfo
        {
            get { return listaAccionInfo; }
            set
            {
                listaAccionInfo = value;
            }
        }

        public ConfiguracionAlertasInfo ConfiguracionAlertas
        {
            get { return configuracionAlertas; }
            set
            {
                configuracionAlertas = value;
            }
        }
    }
}
