using System.Collections.Generic;

namespace SIE.Services.Info.Info
{
    public class SolicitudAutorizacionInfo
    {
        public int SolicitudID { set; get; }
        public int EstatusSolicitud { set; get; }
        public decimal Precio { set; get; }

        public int OrganizacionID { set; get; }
        public int FolioSalida { set; get; }
        public string Justificacion { set; get; }
        public int ProductoID { set; get; }
        public int AlmacenID { set; get; }
        public int UsuarioCreacionID { set; get; }
    }
}
