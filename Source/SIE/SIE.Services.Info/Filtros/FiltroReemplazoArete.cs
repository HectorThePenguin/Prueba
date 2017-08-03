using System.Collections.Generic;
using System.ComponentModel;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Filtros
{
    public class FiltroReemplazoArete : INotifyPropertyChanged
    {
        private EntradaGanadoInfo entradaGanado;
        private int cabezasOrigen;
        private int cabezasRecibidas;
        private int cabezasCortadas;
        private List<FiltroAnimalesReemplazoArete> listaAretes;

        /// <summary>
        /// Entrada de Ganado de donde se van a reemplazar los Aretes
        /// </summary>
        public EntradaGanadoInfo EntradaGanado
        {
            get { return entradaGanado; }
            set
            {
                entradaGanado = value;
                NotifyPropertyChanged("EntradaGanado");
            }
        }
        /// <summary>
        /// Cabezas de Origen de la Entrada de Ganado
        /// </summary>
        public int CabezasOrigen
        {
            get { return cabezasOrigen; }
            set
            {
                cabezasOrigen = value;
                NotifyPropertyChanged("CabezasOrigen");
            }
        }
        /// <summary>
        /// Cabezas Recibidas de la Entrada de Ganado
        /// </summary>
        public int CabezasRecibidas
        {
            get { return cabezasRecibidas; }
            set
            {
                cabezasRecibidas = value;
                NotifyPropertyChanged("CabezasRecibidas");
            }
        }
        /// <summary>
        /// Cabezas Cortadas de la Entrada de Ganado
        /// </summary>
        public int CabezasCortadas
        {
            get { return cabezasCortadas; }
            set
            {
                cabezasCortadas = value;
                NotifyPropertyChanged("CabezasCortadas");
            }
        }
        /// <summary>
        /// Listado de los Aretes a reemplazar
        /// </summary>
        public List<FiltroAnimalesReemplazoArete> ListaAretes
        {
            get { return listaAretes; }
            set
            {
                listaAretes = value;
                NotifyPropertyChanged("ListaAretes");
            }
        }
        /// <summary>
        /// Id del Usuario
        /// </summary>
        public int UsuarioID { get; set; }

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
