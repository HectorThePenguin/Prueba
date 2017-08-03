namespace SIE.Services.Info.Reportes
{
    public class ReporteCorralesEnfermeriaTotalesInfo
    {
        /// <summary>
        /// Corralid
        /// </summary>
        [SIE.Services.Info.Atributos.AtributoIgnorarColumnaExcel]
        public int? CorralID { set; get; }
        /// <summary> 
        ///	CorralEnfermeria  
        /// </summary> 
        public string CorralEnfermeria { get; set; }
        /// <summary>
        /// Total de aretes en el corral
        /// </summary>
        public int Total { set; get; }
    }
}
