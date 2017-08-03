using System.Collections.Generic;
using System;
using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class ChequeraInfo : INotifyPropertyChanged
    {
        public int ChequeraId { get; set; }
        public int ChequeInicial { get; set; }
        public int ChequeFinal { get; set; }
        public int UsuarioCreacionID { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string NumeroChequera { get; set; }
        public int ChequesDisponibles { get; set; }
        public int ChequesGirados { get; set; }
        public int ChequesCancelados { get; set; }
        public string DivisionDescripcion { get; set; }
        public int DivisionId { get; set; }
        public int CentroAcopioId { get; set; }
        public string CentroAcopioDescripcion { get; set; }
        public int BancoId { get; set; }
        public string BancoDescripcion { get; set; }
        public int EstatusId { get; set; }
        public string EstatusIdString { get; set; }
        public string EstatusDescripcion { get; set; }
        
        public IList<OrganizacionInfo> ListDivision { get; set; }
        public IList<BancoInfo> ListBanco { get; set; }
        public IList<ChequeraEtapasInfo> ListEtapas { get; set; }

        public OrganizacionInfo Division { get; set; }
        public BancoInfo Banco { get; set; }
        public OrganizacionInfo CentroAcopio { get; set; }
        public TipoOrganizacionInfo TipoOrganizacion { get; set; }
        public ChequeraEtapasInfo ChequeraEtapas { get; set; }

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

