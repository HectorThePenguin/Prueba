namespace SIE.Services.Info.Info
{
	[BLToolkit.DataAccess.TableName("CausaRechazo")]
	public class CausaRechazoInfo : BitacoraInfo
	{
        [BLToolkit.Mapping.MapIgnore]
	    private string descripcion;
		/// <summary> 
		///	CausaRechazoID  
		/// </summary> 
		[BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
		public int CausaRechazoID { get; set; }

		/// <summary> 
		///	Descripcion  
		/// </summary> 
        public string Descripcion
        {
            get { return descripcion != null ? descripcion.Trim() : descripcion; }
            set
            {
                if (value != descripcion)
                {
                    descripcion = value != null ? value.Trim() : null;
                }
            }
        }
	}
}
