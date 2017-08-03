using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Filtros
{
    public class FiltroTraspasoMpPaMed
    {
        /// <summary>
        /// obtiene el folio del traspaso
        /// </summary>
        public int Folio { get; set; }

        /// <summary>
        /// Obtiene el estatus del Traspaso
        /// </summary>
        public EstatusEnum Activo { get; set; }

        /// <summary>
        /// obtiene la descripcion del producto
        /// </summary>
        public string DescripcionProducto { get; set; }
        /// <summary>
        /// obtiene la organizacion desde donde se creo el traspaso
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }
        /// <summary>
        /// Obtine los dias permitidos de cancelacion
        /// </summary>
        public int DiasPermitidos { get; set; }
        /// <summary>
        /// Obtiene el proveedor del filtro
        /// </summary>
        public ProveedorInfo Proveedor { get; set; }

        /// <summary>
        /// Obtiene el almacen del filtro
        /// </summary>
        public AlmacenInfo Almacen { get; set; }
    }
}
