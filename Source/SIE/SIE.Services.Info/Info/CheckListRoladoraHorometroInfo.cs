namespace SIE.Services.Info.Info
{
    [BLToolkit.DataAccess.TableName("CheckListRoladoraHorometro")]
    public class CheckListRoladoraHorometroInfo : BitacoraInfo
    {
        /// <summary> 
        ///	Check List Roladora Horometro  
        /// </summary> 
        [BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
        public int CheckListRoladoraHorometroID { get; set; }

        /// <summary> 
        ///	Check List Roladora General  
        /// </summary> 
        public int CheckListRoladoraGeneralID { get; set; }

        /// <summary> 
        ///	Entidad Check List Roladora General  
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public CheckListRoladoraGeneralInfo CheckListRoladoraGeneral { get; set; }

        /// <summary> 
        ///	Roladora  
        /// </summary> 
        public int RoladoraID { get; set; }

        /// <summary> 
        ///	Entidad Roladora  
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public RoladoraInfo Roladora { get; set; }

        /// <summary> 
        ///	Horometro Inicial  
        /// </summary> 
        public string HorometroInicial { get; set; }

        /// <summary> 
        ///	Horometro Final  
        /// </summary> 
        public string HorometroFinal { get; set; }

        /// <summary> 
        ///	Fecha Creación  
        /// </summary> 
        [BLToolkit.DataAccess.NonUpdatable]
        public System.DateTime FechaCreacion { get; set; }

        /// <summary> 
        ///	Fecha Modificación  
        /// </summary> 
        public System.DateTime? FechaModificacion { get; set; }

        /// <summary> 
        ///	Fecha Modificación  
        /// </summary> 
        public System.DateTime FechaServidor { get; set; }

        /// <summary>
        /// Horas en operacion
        /// </summary>
        [BLToolkit.DataAccess.SqlIgnore, BLToolkit.Mapping.MapIgnore]
        public string HorasOperacion { get; set; }
    }
}
