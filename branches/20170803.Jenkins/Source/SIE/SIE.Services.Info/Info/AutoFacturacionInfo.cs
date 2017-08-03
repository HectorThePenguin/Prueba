using System.Collections.Generic;
using System;
using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class AutoFacturacionInfo : INotifyPropertyChanged
    {
        /* Pantalla Procesar AutoFacturacion*/
        public int OrganizacionId { get; set; }
        public string Organizacion { get; set; }
        public int CompraId { get; set; }
        public int ProveedorId { get; set; }
        public string Proveedor { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Importe { get; set; }
        public int FormaPagoId { get; set; }
        public string FormasPago { get; set; }
        public int ChequeId { get; set; }
        
        public IList<OrganizacionInfo> ListCentrosAcopio { get; set; }
        public IList<FormaPagoInfo> ListFormasPago { get; set; }
        public IList<EstatusInfo> ListEstatus { get; set; }
        public OrganizacionInfo CentroAcopio { get; set; }
        public FormaPagoInfo FormaPago { get; set; }
        public EstatusInfo Estatus { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        /* Pantalla Registrar AutoFacturacion*/
        public DateTime FechaCompra { get; set; }
        public DateTime FechaFactura { get; set; }
        public string Factura { get; set; }
        public int FolioCompra { get; set; }
        public int Folio { get; set; }
        public bool Facturado { get; set; }
        public int ProductoCabezas { get; set; }
        public string TipoCompra { get; set; }
        public int UsuarioId { get; set; }
        public int EstatusId { get; set; }

        /*Imagenes*/
        public byte[] ImgDocmento { get; set; }
        
        public byte[] ImgINE { get; set; }
        public int ImgINEId { get; set; }
        public int ImgINECount { get; set; }
        public int ImgINEMax { get; set; }

        public byte[] ImgCURP { get; set; }
        public int ImgCURPId { get; set; }
        public int ImgCURPCount { get; set; }
        public int ImgCURPMax { get; set; }

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
