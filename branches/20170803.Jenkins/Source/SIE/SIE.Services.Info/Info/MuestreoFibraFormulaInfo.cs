using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class MuestreoFibraFormulaInfo
    {
        public int MuestreoFibraFormulaID { get; set; }
        public OrganizacionInfo Organizacion { get; set; }
        public FormulaInfo Formula { get; set; }
        public decimal PesoInicial { get; set; }
        public bool Grande { get; set; }
        public decimal PesoFibraGrande { get; set; }
        public bool Mediana { get; set; }
        public decimal PesoFibraMediana { get; set; }
        public bool FinoTamiz { get; set; }
        public decimal PesoFinoTamiz { get; set; }
        public bool FinoBase { get; set; }
        public decimal PesoFinoBase { get; set; }
        public string Origen { get; set; }
        public bool Activo { get; set; }
        public decimal PesoNeto { get; set; }
        public UsuarioInfo UsuarioCreacion { get; set; }
    }
}
