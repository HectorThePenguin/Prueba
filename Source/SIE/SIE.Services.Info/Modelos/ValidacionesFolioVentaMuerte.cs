using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Modelos
{
    public class ValidacionesFolioVentaMuerte
    {
        public bool RegistroCondicionMuerte { get; set; }
        public bool FolioConMuertesRegistradas { get; set; }
        public bool EstatusLote { get; set; }
        public int Cabezas { get; set; }
        public string AretesInvalidos { get; set; }
        public string AretesActivos { get; set; }
    }
}
