namespace SIE.Services.Info.Filtros
{
    public class FiltroCambiarEstatusInfo
    {
        public int AlmacenID { get; set; }
        public long FolioMovimiento { get; set; }
        public int EstatusAnterior { get; set; }
        public int EstatusNuevo { get; set; }
        public int UsuarioModificacionID { get; set; }
    }
}
