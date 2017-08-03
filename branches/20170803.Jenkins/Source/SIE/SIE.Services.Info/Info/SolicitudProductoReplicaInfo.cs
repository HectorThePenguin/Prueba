using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;

namespace SIE.Services.Info.Info
{
    
    public class SolicitudProductoReplicaInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private int interfaceTraspasoSAPID;
        private int organizacionID;               //Centro
        private long folioSolicitud;              //NumeroDocumento
        private System.DateTime fechaMovimiento;
        private bool guardar;
        private FolioSolicitudInfo solicitud;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public SolicitudProductoReplicaInfo()
	    {
	       Detalle = new List<SolicitudProductoReplicaDetalleInfo>();
           Solicitud = new FolioSolicitudInfo();
	    }

        /// <summary>
        /// Identificador del traspaso.
        /// </summary>
        public int InterfaceTraspasoSAPID
        {
            get { return interfaceTraspasoSAPID; }
            set
            {
                interfaceTraspasoSAPID = value;
                NotifyPropertyChanged("InterfaceTraspasoSAPID");
            }
        }

        /// <summary>
        /// Identificador AlmacenID
        /// InterfaceTraspasoSAP.Almacen
        /// </summary>
        public int? AlmacenID {get; set;}

        /// <summary>
        /// Identificador de la Organizacion.
        /// InterfaceTraspasoSAP.Centro
        /// </summary>
        public int OrganizacionID
        {
            get { return organizacionID; }
            set
            {
                organizacionID = value;
                NotifyPropertyChanged("OrganizacionID");
            }
        }

        /// <summary>
        /// Folio de la solicitud de producto.
        /// InterfaceTraspasoSAP.NumeroDocumento
        /// </summary>
        /// <summary>
        /// Folio
        /// </summary>
        public long FolioSolicitud
        {
            get { return folioSolicitud; }
            set
            {
                folioSolicitud = value;
                NotifyPropertyChanged("FolioSolicitud");
            }
        }

        public AlmacenInfo AlmacenDestino {get; set;}
        


        /// <summary>
        /// Fecha del movimiento
        /// InterfaceTraspasoSAP.FechaMovimiento
        /// </summary>
        public System.DateTime FechaMovimiento
        {
            get { return fechaMovimiento; }
            set
            {
                fechaMovimiento = value;
                NotifyPropertyChanged("FechaMovimiento");
            }
        }

        /// <summary>
        /// Objeto para detalle de productos de una solicitud
        /// </summary>
        public List<SolicitudProductoReplicaDetalleInfo> Detalle { get; set; }

        /// <summary>
        /// Contenedor para la ayuda
        /// </summary>
        public FolioSolicitudInfo Solicitud
        {
            get { return solicitud; }
            set
            {
                solicitud = value;
                NotifyPropertyChanged("Solicitud");
            }
        }

        /// <summary> 
        ///	Indica si se puede guardar la solicitud de productos.
        /// </summary> 
        public bool Guardar
        {
            get
            {
                //guardar = !isAutorizado && !(Detalle == null || Detalle.Count == 0);
                return guardar;
            }
            set
            {
                guardar = value;
                NotifyPropertyChanged("Guardar");
            }
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
