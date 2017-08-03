using System;
using System.Collections.Generic;
using System.ComponentModel;
using SIE.Services.Info.Atributos;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    [BLToolkit.DataAccess.TableName("Zona")]
    public class ZonaInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private int? zonaID;
        private string descripcion;
        private PaisInfo pais;

        /// <summary>
        /// Identificador ZonaID.
        /// </summary>
        /// <summary>
        /// Id de zona
        /// </summary>
        [BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
        public int? ZonaID
        {
            get { return zonaID; }
            set
            {
                zonaID = value;
                NotifyPropertyChanged("ZonaID");
            }
        }
        /// <summary>
        /// Descripción.
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
        /// Identificador Pais.
        /// </summary>
        /// <summary>
        /// Clase Pais
        /// </summary>
        [BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
        public PaisInfo Pais
        {
            get { return pais; }
            set
            {
                pais = value;
                NotifyPropertyChanged("Pais");
            }
        }

        public ZonaInfo()
        {
            Pais = new PaisInfo();
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
