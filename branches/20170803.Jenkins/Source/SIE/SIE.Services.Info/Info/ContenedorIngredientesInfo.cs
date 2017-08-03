using SIE.Services.Info.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class ContenedorIngredientesInfo
    {
        public OrganizacionInfo Organizacion {get; set;}

        public FormulaInfo Formula { get; set; }

        public List<IngredienteInfo> ListaIngredientes { get; set; }

        public int UsuarioCreacionID { get; set; }

        public EstatusEnum Activo { get; set; }

        //public TipoFormulaInfo TipoFormula { get; set; }

    }
}
