
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class ProgramaciondeFletesInfo
    {
       public FleteInfo Flete { get; set; }
       /// <summary>
       /// Estatus
       /// </summary>
        public EstatusEnum Activo { get; set; }
        /// <summary>
        /// Indentificador Fletes
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }
       /// <summary>
       /// Costo info
       /// </summary>
        public CostoInfo Costo { get; set; }
        /// <summary>
        /// Flete detalle info
        /// </summary>
        public FleteDetalleInfo FleteDetalle { get; set; }
        /// <summary>
        /// Codigo sap del proveedor
        /// </summary>
       public ProveedorInfo Proveedor { get; set; }
        /// <summary>
        /// CostoInfo111
        /// </summary>
        public CostoInfo CostoInfo { get; set; }
        /// <summary>
        /// Contrato info
        /// </summary>
        public ContratoInfo ContratoInfo { get; set; }
        /// <summary>
        /// Camion ID
        /// </summary>
        public int CamionID { get; set; }
        /// <summary>
        /// Ocion
        /// </summary>
        public int Opcion { get; set; }

    }
}
