using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class AlertaInfo : BitacoraInfo, INotifyPropertyChanged
    {
        #region miembros de la clase
        private int alerta_id;//identificador de la alerta
        private string descripcion;//nombre o descripcion de la alerta
        private int horasRespuesta;//limite de respuesta de la alerta en horas
        private EstatusEnum terminadoAutomatico;//
        private ModuloInfo modulo;//modulo donde se genero la alerta
        private ConfiguracionAlertasInfo configuracionalertas;
        private List<AccionInfo> listaAccionInfo; 
        private XDocument datos;
        #endregion
     
        #region propiedades
        /// <summary>
        /// esta propiedad da acceso al identificador de la alerta
        /// </summary>
        public int AlertaID
        {
            get{return alerta_id;}
            set
            {
                alerta_id = value;
                NotifyPropertyChanged("AlertaID");
            }
        }
        /// <summary>
        ///  descripcion de la alerta
        /// </summary>
        public string Descripcion 
        {
            get { return descripcion; }
            set
            {
                descripcion = value;
                NotifyPropertyChanged("Descripcion");
            }
        }
        /// <summary>
        /// cantidad de horas de respuesta para esta alarma
        /// </summary>
        public int HorasRespuesta
        {
        get{return horasRespuesta;}
            set
            {
                horasRespuesta=value;
                NotifyPropertyChanged("HorasRespuesta");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public EstatusEnum TerminadoAutomatico
        {
            get {return terminadoAutomatico; }
            set
            {
                terminadoAutomatico = value;
                NotifyPropertyChanged("TerminadoAutomatico");
            }
        }
  
        /// <summary>
        /// identificador del modulo al que pertenece la alerta
        /// </summary>
       
        public ModuloInfo Modulo 
        {
            get { return modulo; }
            set
            {   modulo = value;
            NotifyPropertyChanged("Modulo"); 
            }
        }
        #endregion

        public AlertaInfo()
        {
            descripcion = string.Empty;
            modulo = new ModuloInfo();
        }
        
        public ConfiguracionAlertasInfo ConfiguracionAlerta
        {
            get { return configuracionalertas; }
            set
            {
                configuracionalertas = value;
            } 
        }

        public List<AccionInfo> ListaAccionInfo
        {
            get { return listaAccionInfo; }
            set
            {
                listaAccionInfo = value;
                NotifyPropertyChanged("ListaAccionInfo");
            }
        }

		public XDocument Datos
        {
            get { return datos; }
            set { datos = value; }
        }
     
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
