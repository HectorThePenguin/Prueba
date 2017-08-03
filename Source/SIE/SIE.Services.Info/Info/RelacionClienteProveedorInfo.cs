using System;
using System.Collections.Generic;
using SIE.Services.Info.Atributos;
using System.ComponentModel;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class RelacionClienteProveedorInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private ClienteCreditoExcelInfo clienteCreditoExcel;
        private ProveedorInfo contextoProveedor;

        public  OrganizacionInfo CentroAcopio { get; set; }
        public IList<OrganizacionInfo> ListaOrganizacion { get; set; }
        public IList<OrganizacionInfo> ListaCentros { get; set; }
        public int CreditoID { get; set; }
        public bool Editable { set; get; }

        public ImportarSaldosSOFOMInfo Credito {get; set;}
        public ProveedorInfo Proveedor {get; set;}
        public OrganizacionInfo Centro  {get; set;}
        public OrganizacionInfo Ganadera  {get; set;}

        [BLToolkit.Mapping.MapIgnore]
        public ClienteCreditoExcelInfo ClienteCreditoExcel
        {
            get { return clienteCreditoExcel; }
            set 
            {
                clienteCreditoExcel = value;
                NotifyPropertyChanged("ClienteCreditoExcel");
            }
        }

        [BLToolkit.Mapping.MapIgnore]
        public ProveedorInfo ContextoProveedor
        {
            get { return contextoProveedor; }
            set {
                contextoProveedor = value;
                NotifyPropertyChanged("ContextoProveedor");
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
