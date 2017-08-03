using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;

namespace SIE.Services.Info.Info
{
    public class SolicitudProductoReplicaDetalleInfo : INotifyPropertyChanged
    {
        /// <summary> 
        ///	identificador traspaso
        /// </summary> 
        public int InterfaceTraspasoSAPID { get; set; }

        /// <summary> 
        ///	Folio de la Solicitud
        /// </summary> 
        public long FolioSolicitud { get; set; }

        /// <summary> 
        ///	Producto  
        /// </summary> 
        public int ProductoID { get; set; }

        /// <summary> 
        ///	Codigo del producto SAP
        /// </summary> 
        public string Material { get; set; }

        /// <summary> 
        ///	Cantidad solicitado del producto
        /// </summary> 
        public decimal Cantidad { get; set; }

        /// <summary> 
        ///	Precio unitario del producto
        /// </summary> 
        public decimal PrecioUnitario { get; set; }

        /// <summary> 
        ///	Entidad Producto  
        /// </summary> 
        public ProductoInfo Producto { get; set; }

        /// <summary> 
        ///	Estatus  
        /// </summary> 
        public int EstatusID { get; set; }

        /// <summary> 
        ///	Identificador asignado en el que se registro el almacen movimiento 
        /// </summary> 
        public long AlmacenMovimientoID { get; set; }

        /// <summary> 
        ///	Cuenta SAP 
        /// </summary> 
        public string CuentaSAP { get; set; }

        /// <summary> 
        ///	Fecha Creacion
        /// </summary> 
        public System.DateTime FechaCreacion { get; set; }

        private int _totalAretes ;

        public int TotalAretes 
        {
            get { return _totalAretes; }  
            set 
            {
                _totalAretes = value;
                NotifyPropertyChanged("TotalAretes");
            }

        }

        private List<AreteInfo> _listadoAretes;

        public List<AreteInfo> ListadoAretes
        {
            get { return _listadoAretes; }
            set
            {
                _listadoAretes = value;
                NotifyPropertyChanged("ListadoAretes");
            }

        }
        
        public SolicitudProductoReplicaDetalleInfo()
        {
            TotalAretes = 0;
        }

        /// <summary> 
        ///	Activo o Inactivo
        /// </summary> 
        private bool activo;
        private bool editar;


        /// <summary> 
        ///	Indica si se puede guardar la solicitud de productos.
        /// </summary> 
        public bool Activo
        {
            get
            {
                return activo;
            }
            set
            {
                activo = value;
                NotifyPropertyChanged("Activo");
            }
        }

        /// <summary>
        /// Indica si el registro se puede editar
        /// </summary>
        public bool Editar
        {
            get
            {
                return editar;
            }
            set
            {
                editar = value;
                NotifyPropertyChanged("Editar");
            }
        }

        public decimal PrecioPromedio { get; set; }

        public int? UsuarioCreacionID { get; set; }
        public int? UsuarioModificacionID { get; set; }

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
