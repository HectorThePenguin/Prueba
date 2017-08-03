using System.ComponentModel;
namespace SIE.Services.Info.Info
{
    public class IndicadorProductoBoletaInfo : BitacoraInfo, INotifyPropertyChanged 
    {
        private int indicadorProductoBoletaID;
        private IndicadorProductoInfo indicadorProducto;
        private OrganizacionInfo organizacion;

        /// <summary> 
        ///	IndicadorProductoBoletaID  
        /// </summary> 
        public int IndicadorProductoBoletaID
        {
            get { return indicadorProductoBoletaID; }
            set
            {
                indicadorProductoBoletaID = value;
                NotifyPropertyChanged("IndicadorProductoBoletaID");
            }
        }

        /// <summary> 
        ///	IndicadorProductoID  
        /// </summary> 
        public IndicadorProductoInfo IndicadorProducto
        {
            get { return indicadorProducto; }
            set
            {
                indicadorProducto = value;
                NotifyPropertyChanged("IndicadorProducto");
            }
        }

        /// <summary> 
        ///	OrganizacionID  
        /// </summary> 
        public OrganizacionInfo Organizacion
        {
            get { return organizacion; }
            set
            {
                organizacion = value;
                NotifyPropertyChanged("Organizacion");
            }
        }

        private decimal rangoMinimo;

        /// <summary> 
        ///	RangoMinimo  
        /// </summary> 
        public decimal RangoMinimo
        {
            get { return rangoMinimo; }
            set
            {
                rangoMinimo = value;
                NotifyPropertyChanged("RangoMinimo");
            }
        }

        private decimal rangoMaximo;

        /// <summary> 
        ///	RangoMaximo  
        /// </summary> 
        public decimal RangoMaximo
        {
            get { return rangoMaximo; }
            set
            {
                rangoMaximo = value;
                NotifyPropertyChanged("RangoMaximo");
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
