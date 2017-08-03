using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class MarcasInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private int marcaId;
        private string descripcion;

        /// <summary>
        /// Constructor simple de clase
        /// </summary>
        public MarcasInfo()
        {
            MarcaId = 0;
            Descripcion = string.Empty;
            Activo = EstatusEnum.Activo;
        }

        /// <summary>
        /// Identificador de la marca
        /// </summary>
        public int MarcaId 
        {
            get { return marcaId; }
            set
            {
                marcaId = value;
                NotifyPropertyChanged("MarcaId");
            }
        }

        /// <summary>
        /// Nombre o descripción de la marca
        /// </summary>
        public string Descripcion
        {
            get { return descripcion; }
            set
            {
                descripcion = value;
                NotifyPropertyChanged("MarcaId");
            }
        }

        /// <summary>
        /// Identificador si es tracto o no
        /// </summary>
        public TractoEnum EsTracto { get; set; }

        /// <summary>
        /// Fecha de registro de la marca
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Fecha de modificación de la marca
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
