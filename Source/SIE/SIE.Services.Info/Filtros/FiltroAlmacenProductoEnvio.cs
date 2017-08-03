using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Filtros
{
    public class FiltroAlmacenProductoEnvio
    {
        public int UsaurioID { get; set; }
        public int ProductoID { get; set; }
        public bool Cantidad { get; set; }
        public bool Activo { get; set; }
    }
}
