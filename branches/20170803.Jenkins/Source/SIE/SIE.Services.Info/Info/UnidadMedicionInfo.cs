namespace SIE.Services.Info.Info
{
    [BLToolkit.DataAccess.TableName("UnidadMedicion")]
    public class UnidadMedicionInfo : BitacoraInfo
    {
        /// <summary>
        /// Clave con la que se identifica la Unidad
        /// </summary>
        [BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
        public int UnidadID { get; set; }
        /// <summary>
        /// Descripcion de la Unidad
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Clave de la Unidad
        /// </summary>
        public string ClaveUnidad { get; set; }
        /// <summary>
        /// Fecha de Creacion de la Unidad
        /// </summary>
        [BLToolkit.DataAccess.NonUpdatable]
        public System.DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de Modificacion de la Unidad
        /// </summary>
        public System.DateTime? FechaModificacion { get; set; }
    }
}
