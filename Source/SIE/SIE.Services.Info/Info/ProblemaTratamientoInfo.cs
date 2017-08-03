using System;
using System.ComponentModel;
using BLToolkit.Mapping;

namespace SIE.Services.Info.Info
{
	[BLToolkit.DataAccess.TableName("ProblemaTratamiento")]
    public class ProblemaTratamientoInfo : BitacoraInfo, INotifyPropertyChanged
	{
	    private TratamientoInfo tratamiento;

		/// <summary> 
		///	ProblemaTratamientoID  
		/// </summary> 
		[BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
		public int ProblemaTratamientoID { get; set; }

        /// <summary> 
        ///	entidad de Organizacion
        /// </summary> 
        [MapIgnore]
        public OrganizacionInfo Organizacion { get; set; }

		/// <summary> 
		///	entidad de Problema
		/// </summary> 
        [MapIgnore]
		public ProblemaInfo Problema { get; set; }

        /// <summary> 
        ///	ProblemaID  
        /// </summary> 
        public int ProblemaID { get; set; }

		/// <summary> 
		///	TratamientoID  
		/// </summary> 
        [MapIgnore]
		public TratamientoInfo Tratamiento
        {
            get { return tratamiento; }
            set
            {
                if (value != tratamiento)
                {
                    tratamiento = value;
                    NotifyPropertyChanged("Tratamiento");
                }
            }
        }
        
        /// <summary> 
		///	Entidad de Tratamiento
		/// </summary> 
		public int TratamientoID { get; set; }

		/// <summary> 
		///	Dias  
		/// </summary> 
		public int Dias { get; set; }

		/// <summary> 
		///	Orden  
		/// </summary> 
		public int Orden { get; set; }

        /// <summary>
        /// Fecha de Creacion
        /// </summary>
        [BLToolkit.DataAccess.NonUpdatable]
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Fecha de Modificacion
        /// </summary>
        public DateTime? FechaModificacion { get; set; }

        #region Miembros de INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
	}
}
