using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Modelos
{
    public class ContextoCierreDiaInventarioModel
    {
        public AlmacenInfo Almacen { get; set; }

        public TipoAlmacenInfo TipoAlmacen { get; set; }
    }
}
