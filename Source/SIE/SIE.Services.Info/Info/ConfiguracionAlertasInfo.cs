using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
   public class ConfiguracionAlertasInfo :BitacoraInfo ,
       INotifyPropertyChanged
    {
       private int alertaConfiguracionID;
       private string datos;
       private string fuentes;
       private string condiciones;
       private NivelAlertaInfo nivelAlertaInfo;
     
       //private DateTime? fechaDeCreacion;
       private string agrupador;

       public NivelAlertaInfo NivelAlerta
       {
           get { return nivelAlertaInfo; }
           set
           {
               nivelAlertaInfo = value;
               NotifyPropertyChanged("NivelAlertaInfo");
           }
       }

       public int AlertaConfiguracionID { 
           get { return alertaConfiguracionID; }
           set { 
               alertaConfiguracionID = value;
               NotifyPropertyChanged("AlertaConfiguracionID");
           } 
       }

       public string Datos { 
           get { return datos; }
           set {
               datos = value;
               NotifyPropertyChanged("Datos");
           }
       }
       public string Fuentes { 
           get { return fuentes; }
           set {
               fuentes = value;
               NotifyPropertyChanged("Fuentes");
           }
       }
       public string Condiciones { 
           get { return condiciones; }
           set {
               condiciones = value;
               NotifyPropertyChanged("Condiciones");
           }
       }

       public string Agrupador
       {
           get { return agrupador; }
           set
           {
               agrupador = value;
               NotifyPropertyChanged("Agrupador");
           }
       }


       public DateTime FechaCreacion { get; set; }
       public DateTime? FechaModificacion { get; set; }

        #region Miembros de INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
