using System;
using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class RetencionInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private int? retencionId;
        private string descripcion;

        /// <summary>
        ///     Identificador Retencion
        /// </summary>
        public int? RetencionID
        {
            get { return retencionId; }
            set
            {
                if (value != retencionId)
                {
                    retencionId = value;
                    NotifyPropertyChanged("RetencionID");
                }
            }
        }

        /// <summary>
        ///     Retencion Description.
        /// </summary>
        public string Descripcion
        {
            get { return descripcion; }
            set
            {
                if (value != descripcion)
                {
                    descripcion = value;
                    NotifyPropertyChanged("Descripcion");
                }
            }
        }

        /// <summary>
        /// Tipo de Retencion
        /// </summary>
        public string TipoRetencion { get; set; }

        /// <summary>
        /// Indicador de la Retencion
        /// </summary>
        public string IndicadorRetencion { get; set; }

        /// <summary>
        /// Indicador de Impuesto
        /// </summary>
        public string IndicadorImpuesto { get; set; }

        /// <summary>
        /// Tasa de la Retencion
        /// </summary>
        public decimal Tasa { get; set; }

        /// <summary>
        /// Fecha de Creacion de la Retencion
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Fecha de Modificacion de la Retencion
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        /// <summary>
        /// Clave del Costo
        /// </summary>
        public int CostoID { get; set; }

        /// <summary>
        /// Obtiene el Indicador de la retencion
        /// con su tipo e indicador de impuesto
        /// </summary>
        public string DescripcionRetencion
        {
            get
            {
                var descripcionRetencion = string.Format(RetencionID == 0 ? "{0}{1}{2}" : "{0} | {1} | {2}",
                                                         TipoRetencion,
                                                         IndicadorRetencion, IndicadorImpuesto);
                return descripcionRetencion;
            }
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
