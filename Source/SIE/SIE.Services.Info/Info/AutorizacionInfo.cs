using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class AutorizacionInfo
    {
        public UsuarioInfo Usuario { get; set; }
        public string Justificacion { get; set; }
        public bool Autorizado { get; set; }
    }
}
