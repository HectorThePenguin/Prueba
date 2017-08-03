using System.ComponentModel;
using SIE.Services.Info.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class TrazabilidadAnimalInfo : BitacoraInfo , INotifyPropertyChanged
    {
        /// <summary>
        /// Propiedades
        /// </summary>
        private AnimalInfo animal;
        private List<AnimalInfo> animalesDuplicados;
        private bool existeDuplicados;


        /// <summary>
        /// Organizacion
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary>
        /// Animal
        /// </summary>
        public AnimalInfo Animal
        {
            get { return animal; }
            set
            {
                animal = value;
                NotifyPropertyChanged("Animal");
            }
        }

        /// <summary>
        /// AnimalesDuplicados
        /// </summary>
        public List<AnimalInfo> AnimalesDuplicados
        {
            get { return animalesDuplicados; }
            set
            {
                animalesDuplicados = value;
                NotifyPropertyChanged("AnimalesDuplicados");
            }
        }

        /// <summary>
        /// AnimalesDuplicados
        /// </summary>
        public bool ExisteDuplicados
        {
            get { return existeDuplicados; }
            set
            {
                existeDuplicados = value;
                NotifyPropertyChanged("ExisteDuplicados");
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
