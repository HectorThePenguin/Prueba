namespace SIE.Services.Info.Info
{
	[BLToolkit.DataAccess.TableName("Folio")]
	public class FolioInfo 
	{
		/// <summary> 
		///	Folio  
		/// </summary> 
		[BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
		public int FolioID { get; set; }

		/// <summary> 
		///	Organización  
		/// </summary> 
		public int OrganizacionID { get; set; }

		/// <summary> 
		///	Tipo Folio  
		/// </summary> 
		public byte TipoFolioID { get; set; }

		/// <summary> 
		///	Valor  
		/// </summary> 
		public int Valor { get; set; }

        /// <summary> 
        ///	Folio  (Se utiliza para los parámetros del SP Folio_Obtener ) 
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public int Folio { get; set; }
	}
}