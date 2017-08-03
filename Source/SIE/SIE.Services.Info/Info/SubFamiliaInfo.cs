using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    [BLToolkit.DataAccess.TableName("SubFamilia")]
    public class SubFamiliaInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private int subFamiliaID;
        private string descripcion;

        /// <summary>
        /// Clave con la que se identificara
        /// a la Subfamilia
        /// </summary>
        [BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
        public int SubFamiliaID
        {
            get { return subFamiliaID; }
            set
            {
                subFamiliaID = value;
                NotifyPropertyChanged("SubFamiliaID");
            }
        }

        /// <summary>
        /// Identificador de la familia
        /// </summary>
        public int FamiliaID { get; set; }

        /// <summary>
        /// Familia a la que pertenece
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public FamiliaInfo Familia { get; set; }
        /// <summary>
        /// Descripcion de la subfamilia
        /// </summary>
        public string Descripcion
        {
            get { return string.IsNullOrEmpty(descripcion) ? string.Empty : descripcion.Trim(); }
            set
            {
                string valor = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
                if (valor.Trim() != descripcion)
                {
                    descripcion = value;
                    NotifyPropertyChanged("Descripcion");
                }
            }
        }
        /// <summary>
        /// Fecha de creacion de la subfamilia
        /// </summary>
        [BLToolkit.DataAccess.NonUpdatable]
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificacion de la subfamilia
        /// </summary>
        public DateTime? FechaModificacion { get; set; }

        [BLToolkit.Mapping.MapIgnore]
        public CuentaSAPInfo CuentaSAP { get; set; }

        [BLToolkit.Mapping.MapIgnore]
        public List<FamiliaInfo> Familias { get; set; }

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
