using System.Collections.Generic;

namespace SIE.Services.Info.Info
{
    public class DeteccionInfo
    {
        public int DeteccionID { get; set; }

        public string CorralCodigo { get; set; }

        public int CorralID { get; set; }

        public string Arete { get; set; }

        public string AreteMetalico{ get; set; }

        public string FotoDeteccion{ get; set; }
        
        public int LoteID { get; set; }
        
        public int OperadorID { get; set; }
    
        public int TipoDeteccion { get; set; }

        public int GradoID { get; set; }

        public string Observaciones { get; set; }

        public string DescripcionGanado { get; set; }
        
        public string NoFierro { get; set; }

        public int Activo{ get; set; }

        public int UsuarioCreacionID { get; set; }

        public List<SintomaInfo> Sintomas { get; set; }

        public List<ProblemaInfo> Problemas { get; set; }

        public int GrupoCorral { get; set; }

        public int DescripcionGanadoID { get; set; }
    }
}
