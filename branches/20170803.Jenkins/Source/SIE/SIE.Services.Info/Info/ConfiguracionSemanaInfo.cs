using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
	public class ConfiguracionSemanaInfo : BitacoraInfo
	{
		/// <summary> 
		///	ConfiguracionSemanaID  
		/// </summary> 
		public int ConfiguracionSemanaID { get; set; }

		/// <summary> 
		///	OrganizacionID  
		/// </summary> 
		public OrganizacionInfo Organizacion { get; set; }

		/// <summary> 
		///	InicioSemana  
		/// </summary> 
		public DiasSemana InicioSemana { get; set; }

		/// <summary> 
		///	FinSemana  
		/// </summary> 
        public DiasSemana FinSemana { get; set; }

        /// <summary> 
        ///	Objeto para los filtros de busqueda de la pantalla Configuración de Semana
        /// </summary> 
        public OrganizacionInfo OrganizacionFiltro { get; set; }
        

	}
}
