using System.ComponentModel;

namespace SIE.Services.Info.Filtros
{
    public class FiltroAnimalesReemplazoArete : INotifyPropertyChanged
    {
        private string areteCentro;
        private int pesoOrigen;
        private string areteCorte;
        private bool permiteEditar;
        private string areteMetalicoCentro;
        private string areteMetalicoCorte;

        /// <summary>
        /// Arete Actual
        /// </summary>
        public string AreteCentro
        {
            get { return areteCentro; }
            set
            {
                areteCentro = value;
                NotifyPropertyChanged("AreteCentro");
            }
        }
        /// <summary>
        /// Peso de Compra del Animal
        /// </summary>
        public int PesoOrigen
        {
            get { return pesoOrigen; }
            set
            {
                pesoOrigen = value;
                NotifyPropertyChanged("PesoOrigen");
            }
        }
        /// <summary>
        /// Arete Nuevo
        /// </summary>
        public string AreteCorte
        {
            get { return areteCorte; }
            set
            {
                areteCorte = value;
                NotifyPropertyChanged("AreteCorte");
            }
        }
        /// <summary>
        /// Indica si el Arete se puede reemplazar
        /// </summary>
        public bool PermiteEditar
        {
            get { return permiteEditar; }
            set
            {
                permiteEditar = value;
                NotifyPropertyChanged("PermiteEditar");
            }
        }
        /// <summary>
        /// Arete Metalico del Centro
        /// </summary>
        public string AreteMetalicoCentro
        {
            get { return areteMetalicoCentro; }
            set
            {
                areteMetalicoCentro = value;
                NotifyPropertyChanged("AreteMetalicoCentro");
            }
        }
        /// <summary>
        /// Arete Metalico del Inventario
        /// </summary>
        public string AreteMetalicoCorte
        {
            get { return areteMetalicoCorte; }
            set
            {
                areteMetalicoCorte = value;
                NotifyPropertyChanged("AreteMetalicoCorte");
            }
        }
        /// <summary>
        /// Cabezas de Origen de la Entrada de Ganado
        /// </summary>
        public int CabezasOrigen { get; set; }
        /// <summary>
        /// Cabezas Recibidas de la Entrada de Ganado
        /// </summary>
        public int CabezasRecibidas { get; set; }
        /// <summary>
        /// ID de la entrada de Ganado
        /// </summary>
        public int EntradaGanadoID { get; set; }
        /// <summary>
        /// Id del usuario que realiza el Reemplazo de Aretes
        /// </summary>
        public int UsuarioID { get; set; }
        /// <summary>
        /// Folio de la Entrada del Centro
        /// </summary>
        public int FolioEntradaCentro { get; set; }
        /// <summary>
        /// Folio de entrada de Corte del Arete
        /// </summary>
        public long FolioEntradaCorte { get; set; }
        /// <summary>
        /// Id del Animal
        /// </summary>
        public long AnimalID { get; set; }

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
