using System;
using SIE.Services.Info.Enums;
using System.Collections.Generic;

namespace SIE.Services.Info.Info
{
    public class ControlSacrificioInfo
    {
        //SIAP
        public string CodigoCorral { get; set; }
        public int? LoteIdSiap { get; set; }
        public string Lote { get; set; }

        //SCP
        public string Arete { get; set; }
        public string FechaSacrificio { get; set; }
        public string NumeroCorral { get; set; }
        public string NumeroProceso { get; set; }
        public string TipoGanado { get; set; }
        public int? TipoGanadoId { get; set; }
        public int? LoteId { get; set; }
        public long? AnimalId { get; set; }
        public long? AnimalIdSiap { get; set; }
        public string CorralInnova { get; set; }
        public string Po { get; set; }
        public int Consecutivo { get; set; }
        public bool Noqueo { get; set; }
        public bool PielSangre { get; set; }
        public bool PielDescarnada { get; set; }
        public bool Viscera { get; set; }
        public bool Inspeccion { get; set; }
        public bool CanalCompleta { get; set; }
        public bool CanalCaliente { get; set; }
        public int OrganizacionID { get; set; }

        public class Planchado_Arete_Request
        {
            public int AnimalId { get; set; }
            public string Arete { get; set; }
            public int LoteID { get; set; }
            public string Corral { get; set;}
        }

        public class Planchado_AreteLote_Request
        {
            public List<Planchado_AreteLote_Sacrificio> Sacrificio { get; set; }
            public List<Planchado_AreteLote_Animal> Animal{ get; set; }
            public string FechaSacrificio { get; set; }
        }
        
        public class Planchado_AreteLote_Sacrificio
        {
            public string Arete { get; set; }
            public int LoteID { get; set; }
            public long AnimalId { get; set; }
        }

        public class Planchado_AreteLote_Animal
        {
            public string Arete { get; set; }
            public int LoteID { get; set; }
        }

        public class SincronizacionSIAP
        {
            public long? AnimalID { get; set; }
            public string Arete { get; set; }
            public int LoteID { get; set; }
            public string Corral { get; set;}
        }
    }
}