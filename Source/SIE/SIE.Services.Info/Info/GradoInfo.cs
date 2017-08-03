using System;
using System.ComponentModel;
using SIE.Services.Info.Enums;
using System.Collections.Generic;

namespace SIE.Services.Info.Info
{
    public class GradoInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private string nivelGravedad = string.Empty;
        private string descripcion = string.Empty;

        public List<string> nivelesGravedad { get; set; }
        /// <summary>
        /// Identeficador del grado
        /// </summary>
        public int GradoID { get; set; }
        /// <summary>
        /// Descripcion del grado
        /// </summary>
        public String Descripcion
        {
            get { return descripcion == null ? null : descripcion.Trim(); }
            set
            {
                if (value != descripcion)
                {
                    string valor = value;
                    descripcion = valor == null ? valor : valor.Trim();
                    NotifyPropertyChanged("Descripcion");
                }
            }
        }
        /// <summary>
        /// Nivel de gravedad
        /// </summary>
        public String NivelGravedad
        {
            get { return nivelGravedad == null ? null : nivelGravedad.Trim(); }
            set
            {
                if (value != nivelGravedad)
                {
                    string valor = value;
                    nivelGravedad = valor == null ? valor : valor.Trim();
                    NotifyPropertyChanged("NivelGravedad");
                }
            }
        }

        /// <summary>
        /// indica si la opcion fue seleccionada
        /// </summary>
        public bool isChecked {get; set; }
        /// <summary>
        /// Sobrecarga para regresar la descripcion del grado
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return DescripcionCompleta;
        }

        /// <summary>
        /// Lista los niveles de gravedad permitidos
        /// </summary>
        public List<string> NivelesGravedad
        {
            get { return nivelesGravedad; }
            set
            {
                if (value != nivelesGravedad)
                {
                    nivelesGravedad = value;
                    NotifyPropertyChanged("NivelesGravedad");
                }
            }
        }

        public string DescripcionCompleta
        {
            get
            {
                var nivel = NivelGravedad == ((char)NivelGravedadEnum.Grave).ToString() ? NivelGravedadEnum.Grave.ToString() : NivelGravedadEnum.Leve.ToString();
                return !String.IsNullOrEmpty(Descripcion.Trim()) ? Descripcion + "-" + nivel: String.Empty;
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
