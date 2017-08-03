using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace SIE.Services.Info.Info
{
    public class IncidenciasInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private OrganizacionInfo organizacion;
        private AlertaInfo alerta;
        private EstatusInfo estatus;

        public int IncidenciasID { get; set; }

        public OrganizacionInfo Organizacion
        {
            get { return organizacion; }
            set
            {
                organizacion = value;
                NotifyPropertyChanged("Organizacion");
            }
        }

        public AlertaInfo Alerta
        {
            get { return alerta; }
            set
            {
                alerta = value;
                NotifyPropertyChanged("Alerta");
            }
        }

        public int Folio { get; set; }

        public XDocument XmlConsulta { get; set; }

        public DateTime Fecha { get; set; }

        public EstatusInfo Estatus
        {
            get { return estatus; }
            set
            {
                estatus = value;
                NotifyPropertyChanged("Estatus");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public NivelAlertaInfo NivelAlerta { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? FechaVencimiento { get; set; }




        /// <summary>
        /// 
        /// </summary>
        public UsuarioInfo UsuarioResponsable { get; set; }

        public string Comentarios { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public AccionInfo Accion { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IncidenciaSeguimientoInfo IncidenciaSeguimiento { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<IncidenciaSeguimientoInfo> ListaIncidenciaSeguimiento { get; set; } 

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
