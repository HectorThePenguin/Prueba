using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Base.Atributos
{
    [AttributeUsage(AttributeTargets.All)]
    public class AtributosAyuda : Attribute
    {
        public String Descripcion { get; set; }

        public AtributosAyuda()
        {
        }
    }
}
