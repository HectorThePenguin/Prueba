using System.Collections.Generic;
using System.ComponentModel;
using SIE.Services.Info.Info;
using System.Collections.ObjectModel;

namespace SIE.Services.Info.Modelos
{
    public class ConciliacionSAPModel : INotifyPropertyChanged
    {
        #region PROPIEDADES

        private string ruta;
        private ObservableCollection<PolizaInfo> polizas;
        private bool ganado;
        private bool materiaPrima;
        private bool almacen;
        /// <summary>
        /// Ruta del Archivo a conciliar
        /// </summary>
        public string Ruta
        {
            get { return ruta; }
            set
            {
                ruta = value;
                NotifyPropertyChanged("Ruta");
                NotifyPropertyChanged("Conciliar");
            }
        }

        /// <summary>
        /// Obtiene las polizas a regenerar
        /// </summary>
        public ObservableCollection<PolizaInfo> Polizas
        {
            get { return polizas; }
            set
            {
                polizas = value;
                NotifyPropertyChanged("Polizas");
                NotifyPropertyChanged("Guardar");
            }
        }

        /// <summary>
        /// Son todas las polizas obtenidas en la consulta
        /// </summary>
        public List<PolizaInfo> PolizasCompletas { get; set; }

        /// <summary>
        /// Indica si se conciliara Ganado
        /// </summary>
        public bool Ganado
        {
            get { return ganado; }
            set
            {
                ganado = value;
                NotifyPropertyChanged("Ganado");
            }
        }

        /// <summary>
        /// Indica si se conciliara Materia Prima
        /// </summary>
        public bool MateriaPrima
        {
            get { return materiaPrima; }
            set
            {
                materiaPrima = value;
                NotifyPropertyChanged("MateriaPrima");
            }
        }

        /// <summary>
        /// Indica si se conciliara Almacen
        /// </summary>
        public bool Almacen
        {
            get { return almacen; }
            set
            {
                almacen = value;
                NotifyPropertyChanged("Almacen");
            }
        }

        /// <summary>
        /// Indica si se puede conciliar
        /// </summary>
        public bool Conciliar
        {
            get { return !string.IsNullOrWhiteSpace(ruta); }
        }

        /// <summary>
        /// Indica si se permitira guardar
        /// </summary>
        public bool Guardar
        {
            get { return polizas.Count > 0; }
        }

        /// <summary>
        /// Obtiene el tipo de cuenta a buscar
        /// </summary>
        public int TipoCuenta
        {
            get
            {
                var tipo = 0;
                if (ganado)
                {
                    tipo = 1;
                }
                if (almacen)
                {
                    tipo = 3;
                }
                if (materiaPrima)
                {
                    tipo = 2;
                }
                return tipo;
            }
        }

        #endregion PROPIEDADES

        #region METODOS

        #endregion METODOS

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
