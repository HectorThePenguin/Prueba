namespace SIE.Services.Info.Info
{
    public class ProduccionFormulaResumenInfo
    {
        /// <summary>
        /// Hace referencia a la organizacionID del usuario
        /// </summary>
        public int OrganizacionID { get; set; }
        /// <summary>
        /// Hace referecnia al ID de la Formula
        /// </summary>
        public  int FormulaID { get; set; }
        /// <summary>
        /// Hace referencia al <TipoAlmacenID> de la tabla Almacen
        /// </summary>
        public int TipoAlmacenID { get; set; }
    }
}
