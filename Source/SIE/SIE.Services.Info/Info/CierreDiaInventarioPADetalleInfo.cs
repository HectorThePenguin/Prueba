using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class CierreDiaInventarioPADetalleInfo : INotifyPropertyChanged
    {
        private int productoID;
        private string producto;
        private int subFamiliaID;
        private int lote;
        private decimal costoUnitario;
        private int tamanioLote;
        private int inventarioTeorico;
        private int inventarioFisico;
        private decimal porcentajeMermaSuperavit;
        private decimal porcentajeLote;
        private bool requiereAutorizacion;
        private long folio;
        private bool manejaLote;
        private int almacenInventarioLoteID;
        private int piezasTeoricas;
        private int piezasFisicas;
        private int? inventarioFisicoCaptura;

        /// <summary>
        /// Id del Producto
        /// </summary>
        public int ProductoID
        {
            get { return productoID; }
            set
            {
                if (value != productoID)
                {
                    productoID = value;
                    NotifyPropertyChanged("ProductoID");
                }
            }
        }

        /// <summary>
        /// Id del Producto
        /// </summary>
        public string Producto
        {
            get { return producto; }
            set
            {
                if (value != producto)
                {
                    producto = value;
                    NotifyPropertyChanged("Producto");
                }
            }
        }

        /// <summary>
        /// Id del Producto
        /// </summary>
        public int SubFamiliaID
        {
            get { return subFamiliaID; }
            set
            {
                if (value != subFamiliaID)
                {
                    subFamiliaID = value;
                    NotifyPropertyChanged("SubFamiliaID");
                }
            }
        }

        /// <summary>
        /// Lote en el que se encuentra el Producto
        /// </summary>
        public int Lote
        {
            get { return lote; }
            set
            {
                if (value != lote)
                {
                    lote = value;
                    NotifyPropertyChanged("Lote");
                }
            }
        }

        /// <summary>
        ///  Costo unitario del Producto
        /// </summary>
        public decimal CostoUnitario
        {
            get { return costoUnitario; }
            set
            {
                if (value != costoUnitario)
                {
                    costoUnitario = value;
                    NotifyPropertyChanged("CostoUnitario");
                }
            }
        }

        /// <summary>
        /// Tamaño del Lote
        /// </summary>
        public int TamanioLote
        {
            get { return tamanioLote; }
            set
            {
                if (value != tamanioLote)
                {
                    tamanioLote = value;
                    NotifyPropertyChanged("TamanioLote");
                }
            }
        }

        /// <summary>
        /// Inventario Teorico del Producto
        /// </summary>
        public int InventarioTeorico
        {
            get { return inventarioTeorico; }
            set
            {
                if (value != inventarioTeorico)
                {
                    inventarioTeorico = value;
                    NotifyPropertyChanged("InventarioTeorico");
                }
            }
        }

        /// <summary>
        /// Inventario Físico del Producto
        /// </summary>
        public int InventarioFisico
        {
            get { return inventarioFisico; }
            set
            {
                if (value != inventarioFisico)
                {
                    inventarioFisico = value;
                    NotifyPropertyChanged("InventarioFisico");
                }
            }
        }

        /// <summary>
        /// Inventario Físico del Producto
        /// </summary>
        public int? InventarioFisicoCaptura
        {
            get { return inventarioFisicoCaptura; }
            set
            {
                if (value != inventarioFisicoCaptura)
                {
                    inventarioFisicoCaptura = value;
                    NotifyPropertyChanged("InventarioFisicoCaptura");
                }
            }
        }

        /// <summary>
        /// Porcentaje de Merma o Superavit
        /// </summary>
        public decimal PorcentajeMermaSuperavit
        {
            get { return porcentajeMermaSuperavit; }
            set
            {
                if (value != porcentajeMermaSuperavit)
                {
                    porcentajeMermaSuperavit = value;
                    NotifyPropertyChanged("PorcentajeMermaSuperavit");
                }
            }
        }

        /// <summary>
        /// Porcentaje del Lote
        /// </summary>
        public decimal PorcentajeLote
        {
            get { return porcentajeLote; }
            set
            {
                if (value != porcentajeLote)
                {
                    porcentajeLote = value;
                    NotifyPropertyChanged("PorcentajeLote");
                }
            }
        }

        /// <summary>
        /// Porcentaje del Lote
        /// </summary>
        public bool RequiereAutorizacion
        {
            get { return requiereAutorizacion; }
            set
            {
                if (value != requiereAutorizacion)
                {
                    requiereAutorizacion = value;
                    NotifyPropertyChanged("RequiereAutorizacion");
                }
            }
        }

        /// <summary>
        /// Porcentaje del Lote
        /// </summary>
        public long Folio
        {
            get { return folio; }
            set
            {
                if (value != folio)
                {
                    folio = value;
                    NotifyPropertyChanged("Folio");
                }
            }
        }

        /// <summary>
        /// Porcentaje del Lote
        /// </summary>
        public bool ManejaLote
        {
            get { return manejaLote; }
            set
            {
                if (value != manejaLote)
                {
                    manejaLote = value;
                    NotifyPropertyChanged("ManejaLote");
                }
            }
        }

        /// <summary>
        /// Porcentaje del Lote
        /// </summary>
        public int AlmacenInventarioLoteID
        {
            get { return almacenInventarioLoteID; }
            set
            {
                if (value != almacenInventarioLoteID)
                {
                    almacenInventarioLoteID = value;
                    NotifyPropertyChanged("AlmacenInventarioLoteID");
                }
            }
        }

        /// <summary>
        /// Inventario Teorico del Producto
        /// </summary>
        public int PiezasTeoricas
        {
            get { return piezasTeoricas; }
            set
            {
                if (value != piezasTeoricas)
                {
                    piezasTeoricas = value;
                    NotifyPropertyChanged("PiezasTeoricas");
                }
            }
        }

        /// <summary>
        /// Inventario Físico del Producto
        /// </summary>
        public int PiezasFisicas
        {
            get { return piezasFisicas; }
            set
            {
                if (value != piezasFisicas)
                {
                    piezasFisicas = value;
                    NotifyPropertyChanged("PiezasFisicas");
                }
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
