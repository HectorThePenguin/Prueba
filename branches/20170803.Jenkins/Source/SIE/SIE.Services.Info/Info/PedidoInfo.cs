using System;
using System.Collections.Generic;
using System.ComponentModel;
using SIE.Services.Info.Atributos;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class PedidoInfo : INotifyPropertyChanged
    {
        private int pedidoId;
        private int folioPedido;
        private OrganizacionInfo organizacion;
        private string descripcionOrganizacion = string.Empty;
        private IList<EstatusInfo> listaEstatus;
        private string observaciones;

        /// <summary>
        /// Identificador del pedido
        /// </summary>
        [AtributoInicializaPropiedad]
        public int PedidoID
        {
            get { return pedidoId; }
            set
            {
                pedidoId = value;
                NotifyPropertyChanged("PedidoID");
            }
        }

        /// <summary>
        /// Contiene el detalle del pedido
        /// </summary>
        public List<PedidoDetalleInfo> DetallePedido { get; set; }

        /// <summary>
        /// Organizacion del pedido
        /// </summary>
        public OrganizacionInfo Organizacion
        {
            get { return organizacion; }
            set
            {
                organizacion = value;
                NotifyPropertyChanged("Organizacion");
            }
        }

        /// <summary>
        /// Almacen del pedido
        /// </summary>
         public AlmacenInfo Almacen { get; set; }

        /// <summary>
        /// Folio del pedido
        /// </summary>
        [AtributoAyuda(Nombre = "FolioPedidoBusqueda", EncabezadoGrid = "Folio",
            MetodoInvocacion = "ObtenerPedidoPorFolioPedido", PopUp = false)]
        [AtributoAyuda(Nombre = "FolioPedidoBusquedaPorFolio", EncabezadoGrid = "Folio",
            MetodoInvocacion = "ObtenerPedidoPorFolio", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadFolio", EncabezadoGrid = "Folio", MetodoInvocacion = "ObtenerPedidoPorFolio",
            PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadFolio", EncabezadoGrid = "Folio",
            MetodoInvocacion = "ObtenerFoliosPorPaginaParaEntradaMateriaPrima", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadFolioSolicitud", EncabezadoGrid = "Folio",
            MetodoInvocacion = "ObtenerPedidosPorFiltro", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadFolioSolicitud", EncabezadoGrid = "Folio",
            MetodoInvocacion = "ObtenerPedidoPorFolioPedido", PopUp = false)]
        public int FolioPedido
        {
            get { return folioPedido; }
            set
            {
                folioPedido = value;
                NotifyPropertyChanged("FolioPedido");
            }
        }

        /// <summary>
        /// Fecha del pedido
        /// </summary>
        /// 
         public int FolioPedidoBusqueda { get; set; }

         public DateTime FechaPedido { get; set; }
        /// <summary>
        /// Estatus del pedido
        /// </summary>
         public EstatusInfo EstatusPedido { get; set; }
        /// <summary>
        /// Indica el estatus del registro
        /// </summary>
         public EstatusEnum Activo { get; set; } 
        /// <summary>
        /// Fecha de Creacion
        /// </summary>
         public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Usuario de Creacion
        /// </summary>
         public UsuarioInfo UsuarioCreacion { get; set; }
        /// <summary>
        /// Fecha de Modificacion
        /// </summary>
         public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Usuario de Modificacion
        /// </summary>
         public UsuarioInfo UsuarioModificacion { get; set; }

        /// <summary>
        /// Observaciones
        /// </summary>
        public string Observaciones
        {
            get { return observaciones == null ? string.Empty : observaciones.Trim(); }
            set
            {
                observaciones = value;
                NotifyPropertyChanged("Observaciones");
            }
        }

        public IList<EstatusInfo> ListaEstatusPedido
        {
            get { return listaEstatus; }
            set
            {
                listaEstatus = value;
                NotifyPropertyChanged("ListaEstatusPedido");
            }
        }

        /// <summary>
        /// Regresa la descripcio de la organizacion
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadDescripcionOrganizacion", EncabezadoGrid = "Organización", MetodoInvocacion = "ObtenerPedidosPorFolioPaginado", PopUp = true, EstaEnContenedor = true, NombreContenedor = "Folio")]
        [AtributoAyuda(Nombre = "BasculaMateriaPrimaDescripcionOrganizacion", EncabezadoGrid = "Organización", MetodoInvocacion = "ObtenerPedidosPorFiltro", PopUp = true, EstaEnContenedor = true, NombreContenedor = "Folio")]
        [AtributoAyuda(Nombre = "PropiedadDescripcionOrganizacionSolicitud", EncabezadoGrid = "Organización", MetodoInvocacion = "ObtenerPedidosPorFiltro", PopUp = true, EstaEnContenedor = true, NombreContenedor = "Folio")]
        [AtributoAyuda(Nombre = "PropiedadDescripcionOrganizacionCancelacionPedido", EncabezadoGrid = "Organización", MetodoInvocacion = "ObtenerPedidosPorFiltroCancelacion", PopUp = true, EstaEnContenedor = true, NombreContenedor = "Folio")]
        public string DescripcionOrganizacion
         {
             set
             {
                
                 int valorNumero = 0;
                 if (int.TryParse(value, out valorNumero))
                 {
                     FolioPedidoBusqueda = valorNumero;
                 }
                 else
                 {
                     
                    FolioPedidoBusqueda = -1;
                    if (value == "")
                    {
                        FolioPedidoBusqueda = 0;
                    }
                    if (value != descripcionOrganizacion)
                    {
                        string valor = value;
                        descripcionOrganizacion = valor == null ? valor : valor.Trim();
                        NotifyPropertyChanged("DescripcionOrganizacion");
                    }
                 }
             }
             get { return (Organizacion == null || Organizacion.Descripcion == null) ? null : Organizacion.Descripcion.Trim(); }
         }

         public event PropertyChangedEventHandler PropertyChanged;

         private void NotifyPropertyChanged(string propertyName)
         {
             if (PropertyChanged != null)
             {
                 PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
             }
         }
    }
}
