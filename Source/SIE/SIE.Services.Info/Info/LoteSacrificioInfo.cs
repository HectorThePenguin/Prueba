using System;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class LoteSacrificioInfo
    {
        public int LoteSacrificioId { get; set; }
        public int LoteId { get; set; }
        public int OrdenSacrificioId { get; set; }
        public int FolioOrdenSacrificio { get; set; }
        public DateTime Fecha { get; set; }
        public decimal ImporteCanal { get; set; }
        public decimal ImportePiel { get; set; }
        public decimal ImporteVisera { get; set; }
        public string Serie { get; set; }
        public int Folio { get; set; }
        public string FolioFactura { get; set; }
        public string Observaciones { get; set; }
        public ClienteInfo Cliente { get; set; }
        public int Cabezas { get; set; }
        public int Corrales { get; set; }
        public EstatusEnum Activo { get; set; }
        public int OrganizacionId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int UsuarioCreacionId { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioModificacionId { get; set; }
        public bool Cancelacion { get; set; }
    }
}
