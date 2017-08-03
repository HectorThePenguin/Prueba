using SIE.Services.Info.Atributos;

namespace SIE.Services.Info.Info
{
    public class MuertesEnTransitoInfo : BitacoraInfo
    {
        public int EntradaGanadoID { get; set; }

        [AtributoAyuda(Nombre = "PropiedadFolioEntrada", EncabezadoGrid = "Folio Entrada", MetodoInvocacion = "ObtenerPorFolioEntrada", PopUp = false)]
        public int FolioEntrada { get; set; }

        [AtributoAyuda(Nombre = "PropiedadDescripcion", EncabezadoGrid = "Origen", MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
        public string Origen { get; set; }

        public int TipoOrigenID { get; set; }

        public string TipoOrigen { get; set; }

        public string CodigoProveedor { get; set; }

        public string Proveedor { get; set; }

        public ClienteInfo Cliente { get; set; }

        public int CorralID { get; set; }

        public string Corral { get; set; }

        public int LoteID { get; set; }
       
        public string Lote { get; set; }

        public int Cabezas { get; set; }

        public int CabezasLote { get; set; }

        public int MuertesTransito { get; set; }

        public int MuertesRegistradas { get; set; }

        public int OrganizacionID { get; set; }

        public int EmbarqueID { get; set; }
    }
}
