namespace SIE.Services.Info.Info
{
	public class FleteMermaPermitidaInfo : BitacoraInfo
	{
		/// <summary> 
		///	FleteMermaPermitidaID  
		/// </summary> 
		public int FleteMermaPermitidaID { get; set; }

		/// <summary> 
		///	OrganizacionID  
		/// </summary> 
		public OrganizacionInfo Organizacion { get; set; }

		/// <summary> 
		///	SubFamiliaID  
		/// </summary> 
		public SubFamiliaInfo SubFamilia { get; set; }

		/// <summary> 
		///	MermaPermitida  
		/// </summary> 
		public decimal MermaPermitida { get; set; }
	}
}
