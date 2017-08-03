using System;
using System.Collections.Generic;
using System.ComponentModel;
using SIE.Services.Info.Atributos;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    [BLToolkit.DataAccess.TableName("Pais")]
    public class PaisInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private int paisID;
        private string descripcion;
        private string descripcionCorta;

        /// <summary>
        /// Identificador PaisID.
        /// </summary>
        /// <summary>
        /// Id del pais
        /// </summary>
        [BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
        public int PaisID
        {
            get { return paisID; }
            set
            {
                paisID = value;
                NotifyPropertyChanged("PaisID");
            }
        }

        /// <summary>
        /// Descripción del pais.
        /// </summary>
        public string Descripcion
        {
            get { return descripcion == null ? null : descripcion.Trim(); }
            set
            {
                string valor = value;
                descripcion = valor == null ? string.Empty : valor.Trim();
                NotifyPropertyChanged("Descripcion");
            }
        }

        /// <summary>
        /// Descripción corta del país.
        /// </summary>
        public string DescripcionCorta
        {
            get { return descripcionCorta == null ? null : descripcionCorta.Trim(); }
            set
            {
                string valor = value;
                descripcionCorta = valor == null ? string.Empty : valor.Trim();
                NotifyPropertyChanged("DescripcionCorta");
            }
        }

        public PaisInfo()
        {
            UsuarioModificacionID = null;
        }

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
