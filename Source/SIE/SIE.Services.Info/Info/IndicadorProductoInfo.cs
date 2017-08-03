using System.ComponentModel;
namespace SIE.Services.Info.Info
{
    public class IndicadorProductoInfo : BitacoraInfo, INotifyPropertyChanged 
    {
        private int indicadorProductoId;
        private IndicadorInfo indicador;

        /// <summary>
        /// Id del indicador producto
        /// </summary>
        public int IndicadorProductoId
        {
            get { return indicadorProductoId; }
            set
            {
                indicadorProductoId = value;
                NotifyPropertyChanged("IndicadorProductoId");
            }
        }

        /// <summary>
        /// IndicadorInfo de indicador producto info
        /// </summary>
        public IndicadorInfo IndicadorInfo
        {
            get { return indicador; }
            set
            {
                indicador = value;
                NotifyPropertyChanged("IndicadorInfo");
            }
        }

        /// <summary>
        /// Id del producto
        /// </summary>
        public int ProductoId { get; set; }

        private ProductoInfo producto;

        /// <summary>
        /// Representa un Producto
        /// </summary>
        public ProductoInfo Producto
        {
            get { return producto; }
            set
            {
                producto = value;
                NotifyPropertyChanged("Producto");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether is editable.
        /// </summary>
        public bool IsEditable { get; set; }

        /// <summary>
        /// Gets or sets the minimo.
        /// </summary>
        public decimal Minimo { get; set; }

        /// <summary>
        /// Gets or sets the maximo.
        /// </summary>
        public decimal Maximo { get; set; }

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
