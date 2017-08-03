
using System;
using SIE.Services.Info.Atributos;

namespace SIE.Services.Info.Info
{
	[BLToolkit.DataAccess.TableName("CamionReparto")]	public class CamionRepartoInfo : BitacoraInfo
	{
        [BLToolkit.Mapping.MapIgnore]
        private string numeroEconomico;
		/// <summary> 
		///	CamionRepartoID  
		/// </summary> 
		[BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
        [AtributoAyuda(Nombre = "PropiedadClaveCalidadMezcladoFormulasAlimento", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerCamionRepartoPorID", PopUp = false)]
		public int CamionRepartoID { get; set; }

        /// <summary>
        /// Identificador de la organizacion
        /// </summary>
	    public int OrganizacionID { get; set; }

		/// <summary> 
		///	Organizacion
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public OrganizacionInfo Organizacion { get; set; }

        /// <summary>
        /// Identificador del Centro de Costo
        /// </summary>
        public int CentroCostoID { get; set; }

        /// <summary> 
        ///	Entidad del Centro del Costo
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public CentroCostoInfo CentroCosto { get; set; }

	    /// <summary> 
	    ///	NumeroEconomico  
	    /// </summary> 
        [AtributoAyuda(Nombre = "PropiedadDescripcionCalidadMezcladoFormulasAlimento", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
        public string NumeroEconomico
        {
            get { return numeroEconomico != null ? numeroEconomico.Trim() : numeroEconomico; }
            set
            {
                if (value != numeroEconomico)
                {
                    string valor = value;
                    numeroEconomico = valor == null ? valor : valor.Trim();
                }
            }
        }

        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        [BLToolkit.DataAccess.NonUpdatable]
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Fecha de modificacion del registro
        /// </summary>
        public DateTime? FechaModificacion { get; set; }
	}
}
