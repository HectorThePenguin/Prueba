using System;
using System.ComponentModel;
using SIE.Services.Info.Atributos;

namespace SIE.Services.Info.Info
{
    public class FamiliaInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private string descripcion = string.Empty;
        private int familiaId;
        
        /// <summary>
        /// Clave con que se identifica
        /// la Familia
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadClaveFamilia", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorID", PopUp = false)]
        public int FamiliaID
        {
            get
            {
                return familiaId;
            }
            set
            {
                familiaId = value;
                NotifyPropertyChanged("FamiliaID");
            }
        }

        /// <summary>
        /// Descripcion de la Familia
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadDescripcionFamilia", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
        public string Descripcion
        {
            get { return descripcion == null ? null : descripcion.Trim(); }
            set
            {
                string valor = value;
                descripcion = valor == null ? valor : valor.Trim();
                NotifyPropertyChanged("Descripcion");
            }
        }

        /// <summary>
        /// Fecha de Creacion de la Familia
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha en que se modifico la Familia
        /// </summary>
        public DateTime FechaModificacion { get; set; }

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
