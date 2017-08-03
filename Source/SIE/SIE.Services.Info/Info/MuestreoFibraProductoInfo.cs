using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class MuestreoFibraProductoInfo
    {
        public int MuestreoFibraIngredienteID { get; set; }
        public OrganizacionInfo Organizacion { get; set; }
        public ProductoInfo Producto { get; set; }
        public decimal PesoMuestra { get; set; }
        public bool Grueso { get; set; }
        public decimal PesoGranoGrueso { get; set; }
        public bool Mediano { get; set; }
        public decimal PesoGranoMediano { get; set; }
        public bool Fino { get; set; }
        public decimal PesoGranoFino { get; set; }
        public int CribaEntrada { get; set; }
        public int CribaSalida { get; set; }
        public string Observaciones { get; set; }
        public bool Activo { get; set; }
        public decimal PesoNeto { get; set; }
        public UsuarioInfo UsuarioCreacion { get; set; }
    }
}
